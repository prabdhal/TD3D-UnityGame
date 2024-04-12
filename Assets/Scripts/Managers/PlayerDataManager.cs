using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class PlayerDataManager : MonoBehaviour
    {
        #region Singleton
        public static PlayerDataManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one PlayerDataManager in scene");
                return;
            }
            instance = this;
        }
        #endregion

        MainMenuManager main;

        private Scene currentScene;

        public static bool TutorialFinished;

        [Header("Currency")]
        public static int Money;
        public static int Gem;

        [Header("Audio")]
        [Range(0, 1)]
        public static float SoundValue;
        [Range(0, 1)]
        public static float MusicValue;

        [Header("Enemy Log Infos")]
        public static int EnemyLogIndex;

        [Header("Shop Items")]
        public static int GoldItemAmount;
        public static int HealthItemAmount;
        public static int BombItemAmount;
        public static int FreezeTimeItemAmount;

        [Header("Tower Upgrade Indexes")]
        public static int arrowTowerIndex01;
        public static int arrowTowerIndex02;
        public static int arrowTowerIndex03;
        public static int fireTowerIndex01;
        public static int fireTowerIndex02;
        public static int fireTowerIndex03;
        public static int cannonTowerIndex01;
        public static int cannonTowerIndex02;
        public static int cannonTowerIndex03;
        public static int poisonTowerIndex01;
        public static int poisonTowerIndex02;
        public static int poisonTowerIndex03;

        [Header("Level Progression")]
        public static List<int> starScoresPerLevel = new List<int>();
        public static int _levelsUnlocked = 1;

        public TowerData[] towerDatasToReset;
        public TowerUpgradeShopButton[] towerUpgradeShopButtons;
        [Space]
        public TowerUpgradeShopButton[] initCrossbowButtonIndexes;
        public TowerUpgradeShopButton[] initFireButtonIndexes;
        public TowerUpgradeShopButton[] initCannonButtonIndexes;
        public TowerUpgradeShopButton[] initPoisonButtonIndexes;

        public static List<TowerUpgradeShopButton> crossbowButtonIndexes = new List<TowerUpgradeShopButton>(3);
        public static List<TowerUpgradeShopButton> fireButtonIndexes = new List<TowerUpgradeShopButton>(3);
        public static List<TowerUpgradeShopButton> cannonButtonIndexes = new List<TowerUpgradeShopButton>(3);
        public static List<TowerUpgradeShopButton> poisonButtonIndexes = new List<TowerUpgradeShopButton>(3);

        void Start()
        {
            main = MainMenuManager.instance;

            if (crossbowButtonIndexes.Count <= 0)
            {
                for (int i = 0; i < initCrossbowButtonIndexes.Length; i++)
                {
                    crossbowButtonIndexes.Add(initCrossbowButtonIndexes[i]);
                    fireButtonIndexes.Add(initFireButtonIndexes[i]);
                    cannonButtonIndexes.Add(initCannonButtonIndexes[i]);
                    poisonButtonIndexes.Add(initPoisonButtonIndexes[i]);
                }
            }

            LoadProgress();
            main.UpdateCurrency();
            SaveProgress(this);

            currentScene = SceneManager.GetActiveScene();
        }

        public void SaveProgress(PlayerDataManager data)
        {
            SaveSystem.SaveData(data);
        }

        public void LoadProgress()
        {
            PlayerData data = SaveSystem.LoadData();

            Debug.Log("Loading Data: " + data);

            TutorialFinished = data.tutorialFinished;

            // currency progress
            Money = data.money;
            Gem = data.gem;

            SoundValue = data.soundValue;
            MusicValue = data.musicValue;

            Debug.Log("Loaded Sound Value " + data.soundValue);
            Debug.Log("Loaded Music Value " + data.musicValue);

            EnemyLogIndex = data.enemyLogIndex;

            GoldItemAmount = data.goldItemAmount;
            HealthItemAmount = data.goldItemAmount;
            BombItemAmount = data.bombItemAmount;
            FreezeTimeItemAmount = data.freezeTimeItemAmount;

            // game level progress
            _levelsUnlocked = data.levelsUnlocked;

            if (data.levelStars == null) return;

            starScoresPerLevel = null;
            starScoresPerLevel = new List<int>();
            for (int i = 0; i < data.levelStars.Count; i++)
            {
                starScoresPerLevel.Add(data.levelStars[i]);
            }

            // tower upgrade progress
            arrowTowerIndex01 = data.arrowTowerIndex01;
            arrowTowerIndex02 = data.arrowTowerIndex02;
            arrowTowerIndex03 = data.arrowTowerIndex03;
            fireTowerIndex01 = data.fireTowerIndex01;
            fireTowerIndex02 = data.fireTowerIndex02;
            fireTowerIndex03 = data.fireTowerIndex03;
            cannonTowerIndex01 = data.cannonTowerIndex01;
            cannonTowerIndex02 = data.cannonTowerIndex02;
            cannonTowerIndex03 = data.cannonTowerIndex03;
            poisonTowerIndex01 = data.poisonTowerIndex01;
            poisonTowerIndex02 = data.poisonTowerIndex02;
            poisonTowerIndex03 = data.poisonTowerIndex03;

            // update tower button indexes and UI
            crossbowButtonIndexes[0].buttonIndex = arrowTowerIndex01;
            crossbowButtonIndexes[1].buttonIndex = arrowTowerIndex02;
            crossbowButtonIndexes[2].buttonIndex = arrowTowerIndex03;
            fireButtonIndexes[0].buttonIndex = fireTowerIndex01;
            fireButtonIndexes[1].buttonIndex = fireTowerIndex02;
            fireButtonIndexes[2].buttonIndex = fireTowerIndex03;
            cannonButtonIndexes[0].buttonIndex = cannonTowerIndex01;
            cannonButtonIndexes[1].buttonIndex = cannonTowerIndex02;
            cannonButtonIndexes[2].buttonIndex = cannonTowerIndex03;
            poisonButtonIndexes[0].buttonIndex = poisonTowerIndex01;
            poisonButtonIndexes[1].buttonIndex = poisonTowerIndex02;
            poisonButtonIndexes[2].buttonIndex = poisonTowerIndex03;

            for (int i = 0; i < towerUpgradeShopButtons.Length; i++)
            {
                towerUpgradeShopButtons[i].UpdateBaseTextUI();
            }
        }

        public void ResetProgressButton()
        {
            SaveSystem.DeleteData();

            #region Progress Reset
            ResetScriptableObjects();

            TutorialFinished = false;
            EnemyLogIndex = 0;

            // Player Currency Reset
            Money = 0;
            Gem = 0;

            SoundValue = 1;
            MusicValue = 1;

            Debug.Log("Setting static var of SoundValue to: " + SoundValue);
            Debug.Log("Setting static var of MusicValue to: " + MusicValue);

            // Player Level Progress Reset
            _levelsUnlocked = 1;
            starScoresPerLevel = null;
            starScoresPerLevel = new List<int>();

            // Shop Items Reset
            GoldItemAmount = 0;
            BombItemAmount = 1;
            HealthItemAmount = 0;
            FreezeTimeItemAmount = 0;

            // Tower Upgrade Index Reset
            arrowTowerIndex01 = 0;
            arrowTowerIndex02 = 0;
            arrowTowerIndex03 = 0;
            fireTowerIndex01 = 0;
            fireTowerIndex02 = 0;
            fireTowerIndex03 = 0;
            cannonTowerIndex01 = 0;
            cannonTowerIndex02 = 0;
            cannonTowerIndex03 = 0;
            poisonTowerIndex01 = 0;
            poisonTowerIndex02 = 0;
            poisonTowerIndex03 = 0;

            // Tower Upgrade Button Index
            for (int i = 0; i < crossbowButtonIndexes.Count; i++)
            {
                crossbowButtonIndexes[i].buttonIndex = 0;
            }
            for (int i = 0; i < fireButtonIndexes.Count; i++)
            {
                fireButtonIndexes[i].buttonIndex = 0;
            }
            for (int i = 0; i < cannonButtonIndexes.Count; i++)
            {
                cannonButtonIndexes[i].buttonIndex = 0;
            }
            for (int i = 0; i < poisonButtonIndexes.Count; i++)
            {
                poisonButtonIndexes[i].buttonIndex = 0;
            }
            #endregion

            UpdateCurrency();

            SaveProgress(this);

            SceneManager.LoadScene(currentScene.buildIndex);
        }

        public void UpdateCurrency()
        {
            main.moneyText.text = Money.ToString();
            main.gemText.text = Gem.ToString();
        }

        void ResetScriptableObjects()
        {
            for (int i = 0; i < towerDatasToReset.Length; i++)
            {
                towerDatasToReset[i].upgrade01 = null;
                towerDatasToReset[i].upgrade02 = null;
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
                SaveProgress(this);
        }

        public void OnApplicationQuit()
        {
            SaveProgress(this);
        }
    }
}