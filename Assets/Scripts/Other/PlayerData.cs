using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [System.Serializable]
    public class PlayerData
    {
        public bool tutorialFinished;

        public int money;
        public int gem;

        public int enemyLogIndex;

        public int goldItemAmount;
        public int healthItemAmount;
        public int bombItemAmount;
        public int freezeTimeItemAmount;

        [Header("Level Scores")]
        public static List<int> starScoresPerLevel = new List<int>();
        public int levelsUnlocked = 1;

        public int arrowTowerIndex01;
        public int arrowTowerIndex02;
        public int arrowTowerIndex03;
        public int fireTowerIndex01;
        public int fireTowerIndex02;
        public int fireTowerIndex03;
        public int cannonTowerIndex01;
        public int cannonTowerIndex02;
        public int cannonTowerIndex03;
        public int poisonTowerIndex01;
        public int poisonTowerIndex02;
        public int poisonTowerIndex03;

        public List<int> crossbowUpgradeShopButtonIndexes = new List<int>(3);
        public List<int> fireUpgradeShopButtonIndexes = new List<int>(3);
        public List<int> cannonUpgradeShopButtonIndexes = new List<int>(3);
        public List<int> poisonUpgradeShopButtonIndexes = new List<int>(3);

        public List<int> levelStars = new List<int>();
        [Range(0, 1)]
        public float soundValue;
        [Range(0, 1)]
        public float musicValue;

        public PlayerData(PlayerDataManager playerDataManager)
        {
            tutorialFinished = PlayerDataManager.TutorialFinished;

            money = PlayerDataManager.Money;
            gem = PlayerDataManager.Gem;

            soundValue = PlayerDataManager.SoundValue;
            musicValue = PlayerDataManager.MusicValue;

            Debug.Log("Saving Sound Value: " + soundValue);
            Debug.Log("Saving Music Value: " + musicValue);

            enemyLogIndex = PlayerDataManager.EnemyLogIndex;

            goldItemAmount = PlayerDataManager.GoldItemAmount;
            healthItemAmount = PlayerDataManager.HealthItemAmount;
            bombItemAmount = PlayerDataManager.BombItemAmount;
            freezeTimeItemAmount = PlayerDataManager.FreezeTimeItemAmount;

            levelsUnlocked = PlayerDataManager._levelsUnlocked;

            for (int i = 0; i < PlayerDataManager.starScoresPerLevel.Count; i++)
            {
                levelStars.Add(PlayerDataManager.starScoresPerLevel[i]);
            }

            arrowTowerIndex01 = PlayerDataManager.arrowTowerIndex01;
            arrowTowerIndex02 = PlayerDataManager.arrowTowerIndex02;
            arrowTowerIndex03 = PlayerDataManager.arrowTowerIndex03;
            fireTowerIndex01 = PlayerDataManager.fireTowerIndex01;
            fireTowerIndex02 = PlayerDataManager.fireTowerIndex02;
            fireTowerIndex03 = PlayerDataManager.fireTowerIndex03;
            cannonTowerIndex01 = PlayerDataManager.cannonTowerIndex01;
            cannonTowerIndex02 = PlayerDataManager.cannonTowerIndex02;
            cannonTowerIndex03 = PlayerDataManager.cannonTowerIndex03;
            poisonTowerIndex01 = PlayerDataManager.poisonTowerIndex01;
            poisonTowerIndex02 = PlayerDataManager.poisonTowerIndex02;
            poisonTowerIndex03 = PlayerDataManager.poisonTowerIndex03;

            //soundOn = main.turnOn;
        }
    }
}

