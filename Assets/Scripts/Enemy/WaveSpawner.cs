using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TowerDefence
{
    public class WaveSpawner : MonoBehaviour
    {
        WaveManager waveManager;
        public Transform spawnPoint;
        public int route = 0;

        [Header("Enemy Icons")]
        public GameObject iconBackground;
        public GameObject goblinIcon;
        public GameObject wolfIcon;
        public GameObject beeIcon;
        public GameObject batIcon;
        public GameObject spiderlingIcon;
        public GameObject treantIcon;
        public GameObject magmaIcon;
        public GameObject cobraIcon;
        public GameObject golemIcon;
        public GameObject dragonIcon;


        private void Start()
        {
            waveManager = FindObjectOfType<WaveManager>();
            iconBackground.SetActive(false);
        }

        private void Update()
        {
            if (waveManager.beginDownTimer)
            {
                InitWave();
            }
        }

        public void InitWave()
        {
            Wave wave = waveManager.waves[waveManager.waveIndex];
            
            for (int i = 0; i < wave.enemyInfo.Length; i++)
            {
                if (wave.enemyInfo[i].route.Equals(route) && 
                    wave.enemyInfo[i].downTime <= 0 && 
                    wave.enemyInfo[i].waveHasBegun == false)
                {
                    wave.enemyInfo[i].waveHasBegun = true;
                    StartCoroutine(SpawnWave(wave, i));
                }
            }
        }

        IEnumerator SpawnWave(Wave wave, int index)
        {
            for (int i = 0; i < wave.enemyInfo[index].count; i++)
            {
                SpawnEnemy(wave.enemyInfo[index].prefab);
                yield return new WaitForSeconds(1f / wave.enemyInfo[index].rate);
            }
        }
       
        public void SpawnEnemy(GameObject enemy)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }

        public void DisplayEnemyIcon(EnemyType type)
        {
            iconBackground.SetActive(true);

            switch (type)
            {
                case EnemyType.Goblin:
                    goblinIcon.SetActive(true);
                    break;
                case EnemyType.Wolf:
                    wolfIcon.SetActive(true);
                    break;
                case EnemyType.Spiderling:
                    spiderlingIcon.SetActive(true);
                    break;
                case EnemyType.Bee:
                    beeIcon.SetActive(true);
                    break;
                case EnemyType.Treant:
                    treantIcon.SetActive(true);
                    break;
                case EnemyType.Bat:
                    batIcon.SetActive(true);
                    break;
                case EnemyType.Magma:
                    magmaIcon.SetActive(true);
                    break;
                case EnemyType.Cobra:
                    cobraIcon.SetActive(true);
                    break;
                case EnemyType.Golem:
                    golemIcon.SetActive(true);
                    break;
                case EnemyType.Dragon:
                    dragonIcon.SetActive(true);
                    break;
            }
        }

        public void CloseEnemyIcons()
        {
            goblinIcon.SetActive(false);
            wolfIcon.SetActive(false);
            spiderlingIcon.SetActive(false);
            beeIcon.SetActive(false);
            treantIcon.SetActive(false);
            batIcon.SetActive(false);
            magmaIcon.SetActive(false);
            cobraIcon.SetActive(false);
            golemIcon.SetActive(false);
            dragonIcon.SetActive(false);
            iconBackground.SetActive(false);
        }
    }
}
