using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class TowerBullet : MonoBehaviour
    {
        AudioManager audioManager;
        [HideInInspector]
        public Tower tower;
        [HideInInspector]
        public Transform target;
        [HideInInspector]
        public bool isHit;

        public float life = 3;
        public float speed;
        public float explosionRadius = 0f;

        [Header("Effects")]
        public GameObject impactParticle; // bullet impact

        [Header("Audio")]
        public AudioSource impactSound;


        Vector3 impactNormal;
        float i = 0.05f; // delay time of bullet destruction
        Vector3 hitPosOffset = new Vector3(0, 1, 0);

        private void Start()
        {
            audioManager = AudioManager.instance;
            if (impactSound == null)
                impactSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            impactSound.volume = audioManager.soundBar.value;

            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            life -= Time.deltaTime;

            // Destroy rocket after life time is 0
            if (life < 0) Destroy(gameObject);

            BulletMovement();
        }

        private void BulletMovement()
        {
            Vector3 dir = target.position + hitPosOffset - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame && !isHit)
            {
                isHit = true;
                impactNormal = transform.position;
                HitTarget();
                return;
            }

            transform.LookAt(target.transform);
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        void HitTarget()
        {
            if (explosionRadius > 0f)
            {
                Explode();
            }
            else { Damage(target); }

            impactSound.Play();

            Destroy(gameObject, i);
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag.Equals(StringData.enemyTag))
                {
                    Damage(collider.transform);
                }
            }
        }

        void Damage(Transform enemy)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
            {
                TowerData tD = tower.tD;

                // enemies can be immune to towers
                if (e.immunity == tD.towerType)
                    return;

                float dmg = Random.Range(tower.tD.minDamage, tower.tD.maxDamage);
                
                for (int i = 0; i < tD.effects.Count; i++)
                {
                    e.ApplyEffects(tD.effects[i]);
                }

                e.TakeDamage(dmg, tD.damageType);

                ImpactParticleEffects();
            }
        }

        private void ImpactParticleEffects()
        {
            impactParticle = Instantiate(impactParticle, impactNormal, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            impactParticle.transform.parent = target.transform;
            Destroy(impactParticle, 3);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}