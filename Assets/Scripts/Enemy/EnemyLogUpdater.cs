using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    /// <summary>
    /// This class will be responsible for updating enemy log information as the player 
    /// progresses through levels and encounters new enemies
    /// </summary>
    public class EnemyLogUpdater : MonoBehaviour
    {
        public GameObject[] enemyLogContainers;

        public void Start()
        {
            HideAllEnemyLogs();
            DisplayEnemyLogs();
        }
        
        public void DisplayEnemyLogs()
        {
            for (int i = 0; i < PlayerDataManager.EnemyLogIndex + 1; i++)
            {
                enemyLogContainers[i].SetActive(true);
            }
        }

        public void HideAllEnemyLogs()
        {
            for (int i = 0; i < enemyLogContainers.Length; i++)
            {
                enemyLogContainers[i].SetActive(false);
            }
        }
    }
}


// pop up will appear in game as soon as new enemy is encountered

// the pop up will signal to this class the specific enemy that
// the user has encountered

// this class will instantiate/SetActive the new enemy info container 
// within the enemy log 

// options 1. we could set a bool for each enemy which starts false
//            when that specific enemy is encountered, we can set bool to true

// option 2. we could set an index = 0, then every time we encounter a new enemy,
//           we can index++, which will display the next enemy
