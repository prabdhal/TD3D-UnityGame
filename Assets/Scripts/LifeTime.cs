using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class LifeTime : MonoBehaviour
    {
        public float lifeTime = 3;

        public void Update()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
