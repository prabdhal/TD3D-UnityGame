using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class TowerLaser : MonoBehaviour
    {
        AudioManager audioManager;
        [HideInInspector]
        public Tower tower;
        [HideInInspector]
        public Transform target;
        public float spreadRadius = 0f;
        public Transform aoePoint;

        [Header("Effect")]
        public GameObject impactParticle; // laser impact

        [Header("Audio")]
        public AudioSource impactAudio;

        Vector3 impactNormal;

        Vector3 hitPosOffset = new Vector3(0, 1, 0);
        

        void Update()
        {
            if (target == null)  return;

            impactNormal = transform.position;

            HitTarget();

            transform.LookAt(target.transform);
        }

        void HitTarget()
        {
            if (spreadRadius > 0f)
            {
                AOE();
            }
            else { Damage(target); }
        }

        void AOE()
        {
            print("AOE ATTACKING");
            Collider[] colliders = Physics.OverlapSphere(aoePoint.position, spreadRadius);
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

                e.TakeLaserDamage(dmg * tD.fireRate, tD.damageType);

                //impactParticle = Instantiate(impactParticle, impactNormal, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
                //impactParticle.transform.parent = target.transform;
                //Destroy(impactParticle, 3);
            }
        }
    }
}