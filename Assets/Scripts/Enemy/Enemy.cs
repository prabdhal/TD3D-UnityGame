using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        [HideInInspector]
        public Animator anim;
        Rigidbody rb;

        [Header("Health Stats")]
        public float maxHealth = 100;
        private float currentHealth;
        public GameObject healthBar;
        public Image healthFill;

        [Space]

        [Header("Movement speed")]
        [HideInInspector]
        public float speed;
        public float startSpeed = 3f;
        public float rotSpeed = 5f;
        [HideInInspector]
        public float slowDuration;

        [Space]

        [Header("Combat Stats")]
        public Immunities immunity;
        public int attackDamage = 1;
        public float armor;
        private float startArmor;
        public float magicResist;
        private float startMagicResist;
        public int goldValue = 10;

        [Header("Effects")]
        public ParticleSystem poisonEffect;
        public GameObject deathEffect;

        [Space]

        // Enemy Status
        [HideInInspector]
        public bool isPoisoned;
        private bool applyCrit;
        private float critRatio;
        [HideInInspector]
        public bool isDead;
        
        void Start()
        {
            if (anim == null)
                anim = GetComponent<Animator>();
            if (rb == null)
                rb = GetComponent<Rigidbody>();

            if (immunity != Immunities.Poison)
                poisonEffect.gameObject.SetActive(false);

            startArmor = armor;
            startMagicResist = magicResist;

            healthBar.SetActive(false);
            currentHealth = maxHealth;
            speed = startSpeed;
            isDead = false;
        }

        public void ApplyEffects(Effect effect)
        {
            if (effect.effectType == EffectType.ArmorPenetration)
            {
                armor -= effect.effectValue;
                if (armor <= 0)
                    armor = 0;
            }
            if (effect.effectType == EffectType.MagicPenetration)
            {
                magicResist -= effect.effectValue;
                if (magicResist <= 0)
                    magicResist = 0;
            }
            if (effect.effectType == EffectType.CriticalHitChance)
            {
                int randGen = Random.Range(0, 100);
                if (randGen <= effect.effectValue)
                {
                    applyCrit = true;
                }
            }
            if (effect.effectType == EffectType.CriticalHitDamage)
            {
                if (effect.effectValue <= 0) return;

                critRatio = effect.effectValue;
            }
            if (effect.effectType == EffectType.DamageOverTime)
            {
                TakeDamageOverTime(effect.effectValue, 1, effect.effectDuration);

                // ensure tower changes targets
                float totalDot = effect.effectValue * effect.effectDuration;
                if (totalDot >= currentHealth)
                {
                    isPoisoned = true;
                }
            }
            if (effect.effectType == EffectType.Slow)
            {
                speed = startSpeed * (1f - effect.effectValue);
            }
            if (effect.effectType == EffectType.SlowOverDuration)
            {
                speed = startSpeed * (1f - effect.effectValue);
                slowDuration = effect.effectDuration;
            }
        }

        public void TakeDamage(float damage, DamageType damageType)
        {
            // display health bar
            if (!healthBar.activeSelf && !isDead)
                healthBar.SetActive(true);

            if (applyCrit)
            {
                applyCrit = false;
                damage *= critRatio;
            }

            // apply damage 
            if (damageType == DamageType.Magical)
            {
                damage -= magicResist;
                if (damage <= 0)
                    damage = 0;

                currentHealth -= damage;
            }
            else if (damageType == DamageType.Physical)
            {
                damage -= armor;
                if (damage <= 0)
                    damage = 0;

                currentHealth -= damage;
            }
            else if (damageType == DamageType.True)
            {
                currentHealth -= damage;
            }


            // reset armor/magic resist after damage deductions
            armor = startArmor;
            magicResist = startMagicResist;
            // update health UI
            healthFill.fillAmount = currentHealth / maxHealth;

            // enemy is dead
            if (currentHealth <= 0 & !isDead)
            {
                Die();
            }
        }

        public void TakeLaserDamage(float damage, DamageType damageType)
        {
            damage *= Time.deltaTime;
            // display health bar
            if (!healthBar.activeSelf && !isDead)
                healthBar.SetActive(true);

            if (applyCrit)
            {
                applyCrit = false;
                damage *= critRatio;
            }

            // apply damage 
            if (damageType == DamageType.Magical)
            {
                damage -= magicResist * Time.deltaTime;
                if (damage <= 0)
                    damage = 0;

                currentHealth -= damage;
            }
            else if (damageType == DamageType.Physical)
            {
                damage -= armor * Time.deltaTime;
                if (damage <= 0)
                    damage = 0;

                currentHealth -= damage;
            }
            else if (damageType == DamageType.True)
            {
                currentHealth -= damage;
            }


            // reset armor/magic resist after damage deductions
            armor = startArmor;
            magicResist = startMagicResist;
            // update health UI
            healthFill.fillAmount = currentHealth / maxHealth;

            // enemy is dead
            if (currentHealth <= 0 & !isDead)
            {
                Die();
            }
        }

        public void TakeDamageOverTime(float damage, float rate = 1, float duration = 1)
        {
            StartCoroutine(DamageOverTime(damage, rate, duration));
        }

        IEnumerator DamageOverTime(float damage, float rate, float duration)
        {
            float t = 0;

            while (t < duration)
            {
                currentHealth -= damage;
                healthFill.fillAmount = currentHealth / maxHealth;
                t += 1;

                poisonEffect.gameObject.SetActive(true);
                poisonEffect.Play();

                if (currentHealth <= 0 & !isDead)
                {
                    poisonEffect.Stop();
                    Die();
                    yield break;
                }
                yield return new WaitForSeconds(rate);
            }
        }

        void Die()
        {
            anim.SetBool(StringData.death, true);
            PlayerStats.Gold += goldValue;
            healthBar.SetActive(false);
            isDead = true;
            
            //Add death effect
            
            WaveManager.EnemiesAlive--;

            Destroy(gameObject, 5f);
        }
    }
}