using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    [System.Serializable]
    public class Wave
    {
        public EnemyInfo[] enemyInfo;
    }

    [System.Serializable]
    public class EnemyInfo
    {
        public string Name = "Goblin Route 0";
        public EnemyType enemyType;
        public GameObject prefab;  //the enemy prefab
        public int count = 5;         //the number of enemies to spawn
        public float rate = 1;        //how quick should the enemy spawn one after another
        public float downTime = 0;  //time before enemy spawns after wave has started
        public int route = 0;   //spawn portal and route path index
        [HideInInspector]
        public bool waveHasBegun = false;
    }
}
