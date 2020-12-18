using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class SpawnScene : MonoBehaviour
    {
        public GameObject pf;
        public GameObject spawnPoint;

        float countdown = 5;
        

        public void Update()
        {
            if (countdown <= 0)
            {
                countdown = 5;
                Instantiate();
            }
            else { countdown -= Time.deltaTime; }
        }

        public void Instantiate()
        {
            GameObject e = Instantiate(pf, spawnPoint.transform.position, pf.transform.rotation);
        }
    }
}
