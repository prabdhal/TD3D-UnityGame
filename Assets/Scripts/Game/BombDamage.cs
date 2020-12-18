using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class BombDamage : MonoBehaviour
    {
        public int damage;

        public float lifeTime = 1;

        private void Update()
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
                Destroy(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StringData.enemyTag))
            {
                other.GetComponent<Enemy>().TakeDamage(damage, DamageType.True);
            }
        }
    }
}