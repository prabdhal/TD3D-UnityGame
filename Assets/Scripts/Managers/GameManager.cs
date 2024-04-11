using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance;

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one GameManager in scene");
                return;
            }
            Instance = this;
        }
        #endregion

        // Script References
        //PlayerDataManager playerDataManager;
        GameButtonManager buttonManager;
        AudioManager audioManager;
        PlayerStats playerStats;
        WaveManager waveManager;
        Rewards rewards;
        [SerializeField]
        private SceneFader fader;
        Camera cam;

        // UI Scripts
        UpdateTowerDualUpgradeUI dualTowerUpgradeUI;
        UpdateTowerUpgradeUI towerUpgradeUI;
        ConfirmationTowerUI confirmationTowerUI;

        // Selected Build Node Info
        TowerData towerToBuild;
        [HideInInspector]
        public BuildNode selectedBuildNode;

        [Header("New Enemy Log Information")]
        public EnemyLogInfoContainer newEnemyAlertWindow;
        public EnemyLogInfo newEnemyThisLevel;
        public int newEnemyIndex;

        //[Header("Battery Save Mode")]
        //public GameObject highEndPath;
        //public GameObject lowEndPath;
        //public TextMeshProUGUI toggleText;
        //public Image batteryButtonImage;
        //public Sprite onButtonSprite;
        //public Sprite offButtonSprite;

        // Booleans
        public static bool GameIsOver;
        public static bool IsPaused;
        public static bool BeginGame;
        public static bool BeginTutorial;
        public int activeTutorialWindow = 0;
        bool buttonActive;
        //bool isBatterySaving;
        [Space]
        [Header("Victory Window UIs")]
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI gemText;
        public TextMeshProUGUI scoreText;
        public Image star01;
        public Image star02;
        public Image star03;
        public GameObject continueButton;
        private bool getScore;
        private int starScore;
        public int levelToUnlock = 2;
        [Space]
        #region UI Variables
        [Header("Window UIs")]
        public GameObject backgroundImage;
        public GameObject confirmationWindow;
        public GameObject settingsWindow;
        public GameObject victoryWindow;
        public GameObject gameOverWindow;
        public GameObject upgradeWindow;
        public GameObject dualUpgradeWindow;
        public GameObject shopWindow;
        public GameObject[] tutorialWindow;
        public GameObject[] tutorialBackgroundImages;
        public GameObject enemyInfoWindow;
        #endregion
        [Space]
        [Header("Effects")]
        public GameObject buildEffect;
        public GameObject sellEffect;

        [Space]
        private Scene currentScene;

        public bool CanBuild { get { return towerToBuild != null; } }
        public bool CanPurchase { get { return PlayerStats.Gold >= towerToBuild.cost; } }

        public void Start()
        {
            #region Game Components
            cam = Camera.main;

            if (fader == null)
                fader = FindObjectOfType<SceneFader>();
            fader.gameObject.SetActive(true);

            audioManager = FindObjectOfType<AudioManager>();
            waveManager = FindObjectOfType<WaveManager>();
            buttonManager = FindObjectOfType<GameButtonManager>();

            rewards = Rewards.instance;
            playerStats = PlayerStats.instance;
            dualTowerUpgradeUI = UpdateTowerDualUpgradeUI.instance;
            towerUpgradeUI = UpdateTowerUpgradeUI.instance;
            confirmationTowerUI = ConfirmationTowerUI.instance;

            confirmationTowerUI.yesButton.onClick.RemoveAllListeners();
            confirmationTowerUI.noButton.onClick.RemoveAllListeners();
            towerUpgradeUI.upgradeButton.onClick.RemoveAllListeners();
            towerUpgradeUI.sellButon.onClick.RemoveAllListeners();
            towerUpgradeUI.swapButton.onClick.RemoveAllListeners();
            dualTowerUpgradeUI.upgradeButton01.onClick.RemoveAllListeners();
            dualTowerUpgradeUI.upgradeButton02.onClick.RemoveAllListeners();
            dualTowerUpgradeUI.sellButon.onClick.RemoveAllListeners();
            dualTowerUpgradeUI.swapButton.onClick.RemoveAllListeners();

            confirmationTowerUI.yesButton.onClick.AddListener(InstantiateTowerButton);
            confirmationTowerUI.noButton.onClick.AddListener(CloseConfirmationWindow);
            towerUpgradeUI.upgradeButton.onClick.AddListener(UpgradeTowerButton01);
            towerUpgradeUI.sellButon.onClick.AddListener(SellTowerButton);
            towerUpgradeUI.swapButton.onClick.AddListener(towerUpgradeUI.SwapButton);
            dualTowerUpgradeUI.upgradeButton01.onClick.AddListener(UpgradeTowerButton01);
            dualTowerUpgradeUI.upgradeButton02.onClick.AddListener(UpgradeTowerButton02);
            dualTowerUpgradeUI.sellButon.onClick.AddListener(SellTowerButton);
            dualTowerUpgradeUI.swapButton.onClick.AddListener(dualTowerUpgradeUI.SwapButton);

            #endregion

            currentScene = SceneManager.GetActiveScene();

            CloseAllWindowsButton();

            //isBatterySaving = true;
            //BatterySaveMode();

            // Victory Window UI's
            getScore = false;
            star01.fillAmount = 0;
            star02.fillAmount = 0;
            star03.fillAmount = 0;
            continueButton.SetActive(false);
            moneyText.gameObject.SetActive(false);
            gemText.gameObject.SetActive(false);

            waveManager.waveButton.SetActive(true);
            GameIsOver = false;
            BeginGame = false;
            //TogglePause();    //toggle pause after scene fader FIX
        }

        private void Update()
        {
            if (fader.doneFading)
                fader.doneFading = false;

            TutorialHandler();

            if (newEnemyIndex > PlayerDataManager.EnemyLogIndex)
            {
                backgroundImage.SetActive(true);
                enemyInfoWindow.SetActive(true);
            }

            if (BeginGame && buttonActive == false && PlayerDataManager.TutorialFinished)
            {
                buttonActive = true;
                buttonManager.EnableButtons();
            }

            if (getScore) ScoreCalculator();

            if (GameIsOver) return;

            if (PlayerStats.Health <= 0)
            {
                PlayerStats.Score = 0;
                ShowGameOverWindow();
            }
        }

        #region Tutorial Handlers

        private void TutorialHandler()
        {
            if (!PlayerDataManager.TutorialFinished && levelToUnlock == 2 && !BeginTutorial)
            {
                BeginTutorial = true;
                activeTutorialWindow = 0;
                UpdateTutorialWindow();
            }
        }

        private void UpdateTutorialWindow()
        {
            for (int i = 0; i < tutorialWindow.Length; i++)
            {
                tutorialWindow[i].SetActive(false);
                tutorialBackgroundImages[i].SetActive(false);
            }

            tutorialWindow[activeTutorialWindow].SetActive(true);
            tutorialBackgroundImages[activeTutorialWindow].SetActive(true);
        }

        public void ContinueTutorialButton()
        {
            activeTutorialWindow++;
            UpdateTutorialWindow();
        }

        public void FinishTutorialButton()
        {
            PlayerDataManager.TutorialFinished = true;
            for (int i = 0; i < tutorialWindow.Length; i++)
            {
                tutorialWindow[i].SetActive(false);
                tutorialBackgroundImages[i].SetActive(false);
            }
            backgroundImage.SetActive(false);
            BeginTutorial = false;
        }

        #endregion

        #region Score Handlers

        public void LevelCompleted()
        {
            ShowVictoryWindow();

            // Pass Save Data if level not already completed
            if (levelToUnlock > PlayerDataManager._levelsUnlocked)
                PlayerDataManager._levelsUnlocked = levelToUnlock;

            // Check if previously completed level
            if (PlayerDataManager.starScoresPerLevel.Count - 1 >= PlayerDataManager._levelsUnlocked - 2)
            {
                int prevScore = PlayerDataManager.starScoresPerLevel[levelToUnlock - 2];

                // already full completed 
                if (prevScore >= GetScore())
                {
                    rewards.rewardMoney = 0;
                    rewards.rewardGem = 0;
                    rewards.scoreRatio = 0;
                }
                else
                {
                    // if prev score was <= 2
                    if (GetScore().Equals(3) && prevScore <= 2)
                    {
                        ThreeStarRewards(prevScore);
                    }
                    else if (GetScore().Equals(2) && prevScore <= 1)
                    {
                        TwoStarRewards(prevScore);
                    }
                    else if (GetScore().Equals(1) && prevScore == 0)
                    {
                        OneStarRewards(prevScore);
                    }

                    // Pass Save Data
                    PlayerDataManager.starScoresPerLevel[levelToUnlock - 2] = starScore;
                }
            }
            else // First time attempting level
            {
                if (GetScore().Equals(3))
                {
                    ThreeStarRewards(0);
                }
                else if (GetScore().Equals(2))
                {
                    TwoStarRewards(0);
                }
                else if (GetScore().Equals(1))
                {
                    OneStarRewards(0);
                }

                // Pass Save Data
                PlayerDataManager.starScoresPerLevel.Add(starScore);
            }

            // Pass Save Data
            PlayerDataManager.Money += rewards.rewardMoney;
            PlayerDataManager.Gem += rewards.rewardGem;

            // Save Passed Data
            SaveSystem.SaveData(PlayerDataManager.instance);

            // update score values
            getScore = true;
        }

        private void OneStarRewards(int prevScore)
        {
            starScore = 1;

            rewards.rewardMoney *= 1;
            rewards.rewardGem *= 1;
        }

        private void TwoStarRewards(int prevScore)
        {
            starScore = 2;
            if (prevScore == 0)
            {
                rewards.rewardMoney *= 2;
                rewards.rewardGem *= 2;
            }
            else if (prevScore == 1)
            {
                rewards.rewardMoney *= 1;
                rewards.rewardGem *= 1;
            }
        }

        private void ThreeStarRewards(int prevScore)
        {
            starScore = 3;
            if (prevScore == 0)
            {
                rewards.rewardMoney *= 4;
                rewards.rewardGem *= 4;
            }
            else if (prevScore == 1)
            {
                rewards.rewardMoney *= 3;
                rewards.rewardGem *= 3;
            }
            else
            {
                rewards.rewardMoney *= 2;
                rewards.rewardGem *= 2;
            }
        }

        private int GetScore()
        {
            float healthDif = PlayerStats.Health / playerStats.startHealth;

            if (healthDif == 1)
            {
                return 3;
            }
            else if (healthDif > 0.55 && healthDif < 1)
            {
                return 2;
            }
            else { return 1; }
        }

        private void ScoreCalculator()
        {
            if (GetScore().Equals(1))
            {
                OneStarFill();
            }
            else if (GetScore().Equals(2))
            {
                TwoStarFill();
            }
            else
            {
                ThreeStarFill();
            }

            // gives player rewards
            UpdateScoreUI();
        }

        private void ThreeStarFill()
        {
            star01.fillAmount = Mathf.Clamp01(star01.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            if (star01.fillAmount >= 1)
            {
                star02.fillAmount = Mathf.Clamp01(star02.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            }
            if (star02.fillAmount >= 1)
            {
                star03.fillAmount = Mathf.Clamp01(star03.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            }
            if (star03.fillAmount >= 1)
            {
                continueButton.SetActive(true);
                moneyText.gameObject.SetActive(true);
                gemText.gameObject.SetActive(true);
            }
        }

        private void TwoStarFill()
        {
            star01.fillAmount = Mathf.Clamp01(star01.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            if (star01.fillAmount >= 1)
            {
                star02.fillAmount = Mathf.Clamp01(star02.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            }
            if (star02.fillAmount >= 1)
            {
                continueButton.SetActive(true);
                moneyText.gameObject.SetActive(true);
                gemText.gameObject.SetActive(true);
            }
        }

        private void OneStarFill()
        {
            star01.fillAmount = Mathf.Clamp01(star01.fillAmount += 0.1f * rewards.fillSpeed * Time.deltaTime);
            if (star01.fillAmount >= 1)
            {
                continueButton.SetActive(true);
                moneyText.gameObject.SetActive(true);
                gemText.gameObject.SetActive(true);
            }
        }

        private void UpdateScoreUI()
        {
            moneyText.text = "+" + rewards.rewardMoney.ToString();
            gemText.text = "+" + rewards.rewardGem.ToString();
            PlayerStats.Score = Mathf.Clamp(PlayerStats.Score += 10f * rewards.fillSpeed * Time.deltaTime, 0, GetScore() * rewards.scoreRatio);
            scoreText.text = "Score: " + PlayerStats.Score.ToString("0");
        }

        #endregion

        #region Node UI Handler

        /// <summary>
        /// Build Node passes itself to Game Manager in order to access the Tower Data on that specific node.
        /// </summary>
        /// <param name="ground"></param>
        public void SelectBuildGround(BuildNode ground)
        {
            //if (selectedBuildNode == ground)
            //{
            //    DeselectBuildGround();
            //    return;
            //}

            DeselectBuildGround();
            selectedBuildNode = ground;
            selectedBuildNode.SetColor(selectedBuildNode.activeColor);

            towerToBuild = null;
            SetBuildTarget(ground);
        }

        public void DeselectBuildGround()
        {
            if (selectedBuildNode != null)
            {
                selectedBuildNode.SetColor(selectedBuildNode.startColor);
                selectedBuildNode.isSelected = false;

                if (selectedBuildNode.tower != null)
                {
                    selectedBuildNode.towerScript.HideRangeUI();
                }
            }

            selectedBuildNode = null;

            // close all tower UIs
            CloseAllWindowsButton();
        }

        /// <summary>
        /// Shows the appropriate UI (Shop or Upgrade) depending on whether a tower currently exists on the Build Node.
        /// </summary>
        /// <param name="_target"></param>
        public void SetBuildTarget(BuildNode _target)
        {
            selectedBuildNode = _target;

            if (_target.tower != null)
            {
                selectedBuildNode.towerScript.ShowRangeUI();

                if (_target.towerBlueprint.upgrade02 != null)
                {
                    InitDualUpgradeUI();
                    ShowDualUpgradeWindow();
                }
                else
                {
                    InitUpgradeUI();
                    ShowUpgradeWindow();
                }
            }
            else
            {
                ShowShopWindow();
            }
        }

        #endregion

        #region Button Functions

        public void OpenSettings()
        {
            if (BeginTutorial) return;
            Time.timeScale = 0;

            backgroundImage.SetActive(true);
            settingsWindow.SetActive(true);
        }

        public void ResumeGame()
        {
            if (buttonManager.isFastFowarded)
                Time.timeScale = 2;
            else
                Time.timeScale = 1;

            backgroundImage.SetActive(false);
            settingsWindow.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1;

            fader.FadeTo(StringData.mainScene);
        }

        public void RetryLevel()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(currentScene.buildIndex);
        }

        public void InstantiateTowerButton()
        {
            // return out if tower already exits
            if (selectedBuildNode.tower != null)
                return;

            if (CanBuild == false)
                return;

            if (CanPurchase)
            {
                PlayerStats.Gold -= towerToBuild.cost;
                GameObject _tower = Instantiate(towerToBuild.towerPrefab, selectedBuildNode.GetBuildPosition(), towerToBuild.towerPrefab.transform.rotation);

                Instantiate(buildEffect, selectedBuildNode.GetBuildPosition(), towerToBuild.towerPrefab.transform.rotation);
                audioManager.buildAudio.Play();

                selectedBuildNode.towerBlueprint = towerToBuild;
                selectedBuildNode.tower = _tower;
                selectedBuildNode.towerScript = _tower.GetComponent<Tower>();
                selectedBuildNode.rend.material.color = selectedBuildNode.startColor;
            }

            CloseAllWindowsButton();
        }

        public void CloseAllWindowsButton()
        {
            settingsWindow.SetActive(false);
            shopWindow.SetActive(false);
            upgradeWindow.SetActive(false);
            dualUpgradeWindow.SetActive(false);
            confirmationWindow.SetActive(false);
        }

        public void CloseConfirmationWindow()
        {
            shopWindow.SetActive(true);
            confirmationWindow.SetActive(false);
        }

        public void UpgradeTowerButton01()
        {
            selectedBuildNode.UpgradeTurret01();
            DeselectBuildGround();
        }

        public void UpgradeTowerButton02()
        {
            selectedBuildNode.UpgradeTurret02();
            DeselectBuildGround();
        }

        public void SellTowerButton()
        {
            selectedBuildNode.SellTower();
            SellTowerEffect();

            DeselectBuildGround();
        }

        public void SellTowerEffect()
        {
            Instantiate(sellEffect, selectedBuildNode.GetBuildPosition(), sellEffect.transform.rotation);
            audioManager.sellAudio.Play();
        }

        public void NewEnemyAlertButton()
        {
            if (newEnemyAlertWindow == null || newEnemyThisLevel == null) return;

            if (newEnemyAlertWindow.enemyLogInfo.Equals(newEnemyThisLevel))
                PlayerDataManager.EnemyLogIndex = newEnemyIndex;

            backgroundImage.SetActive(false);
            enemyInfoWindow.SetActive(false);
        }

        //public void BatterySaveMode()
        //{
        //    isBatterySaving = !isBatterySaving;

        //    if (isBatterySaving)
        //    {
        //        lowEndPath.SetActive(true);
        //        highEndPath.SetActive(false);
        //        batteryButtonImage.sprite = onButtonSprite;
        //        toggleText.text = "ON";
        //    }
        //    else
        //    {
        //        highEndPath.SetActive(true);
        //        lowEndPath.SetActive(false);
        //        batteryButtonImage.sprite = offButtonSprite;
        //        toggleText.text = "OFF";
        //    }
        //}

        #endregion

        #region Upgrade UI Initializer's

        private void InitUpgradeUI()
        {
            if (!selectedBuildNode.isUpgraded)
            {
                TowerData t = selectedBuildNode.towerBlueprint.upgrade01;

                if (t == null)
                {
                    towerUpgradeUI.NullifyUI(t, selectedBuildNode.towerBlueprint);
                    towerUpgradeUI.towerSprite.sprite = selectedBuildNode.towerBlueprint.icon;
                    towerUpgradeUI.sellValueText.text = selectedBuildNode.towerBlueprint.sellValue.ToString();
                    towerUpgradeUI.UpdateUI(null, selectedBuildNode.towerBlueprint);
                    towerUpgradeUI.upgradeButton.interactable = false;
                    towerUpgradeUI.upgradeButton.gameObject.SetActive(false);
                    return;
                }

                towerUpgradeUI.UpdateUI(t, selectedBuildNode.towerBlueprint);
                towerUpgradeUI.upgradeButton.gameObject.SetActive(true);
                towerUpgradeUI.upgradeButton.interactable = true;
            }
            else
            {
                towerUpgradeUI.upgradeButton.gameObject.SetActive(false);
            }
        }

        private void InitDualUpgradeUI()
        {
            if (!selectedBuildNode.isUpgraded)
            {
                TowerData t01 = selectedBuildNode.towerBlueprint.upgrade01;
                TowerData t02 = selectedBuildNode.towerBlueprint.upgrade02;

                if (t01 == null || t02 == null)
                {
                    dualTowerUpgradeUI.towerSprite.sprite = selectedBuildNode.towerBlueprint.icon;
                    dualTowerUpgradeUI.sellValueText01.text = selectedBuildNode.towerBlueprint.sellValue.ToString();
                    dualTowerUpgradeUI.UpdateUI(null, null, selectedBuildNode.towerBlueprint);
                    dualTowerUpgradeUI.upgradeButton01.gameObject.SetActive(false);
                    dualTowerUpgradeUI.upgradeButton02.gameObject.SetActive(false);
                    dualTowerUpgradeUI.upgradeButton01.interactable = false;
                    dualTowerUpgradeUI.upgradeButton02.interactable = false;
                    return;
                }
                dualTowerUpgradeUI.UpdateUI(t01, t02, selectedBuildNode.towerBlueprint);
                dualTowerUpgradeUI.upgradeButton01.gameObject.SetActive(true);
                dualTowerUpgradeUI.upgradeButton02.gameObject.SetActive(true);
                dualTowerUpgradeUI.upgradeButton01.interactable = true;
                dualTowerUpgradeUI.upgradeButton02.interactable = true;
            }
            else
            {
                dualTowerUpgradeUI.upgradeButton01.gameObject.SetActive(false);
                dualTowerUpgradeUI.upgradeButton02.gameObject.SetActive(false);
            }
        }

        #endregion

        #region WindowUI

        public void ShowConfirmationWindowButton(TowerData data)
        {
            CloseAllWindowsButton();
            towerToBuild = data;

            confirmationWindow.SetActive(true);

            confirmationTowerUI.UpdateUI(data);
        }

        private void ShowUpgradeWindow()
        {
            upgradeWindow.SetActive(true);
        }

        private void ShowDualUpgradeWindow()
        {
            dualUpgradeWindow.SetActive(true);
        }

        private void ShowShopWindow()
        {
            shopWindow.SetActive(true);
        }

        private void ShowVictoryWindow()
        {
            backgroundImage.SetActive(true);
            victoryWindow.SetActive(true);
        }

        private void ShowGameOverWindow()
        {
            Time.timeScale = 1;

            GameIsOver = true;
            backgroundImage.SetActive(true);
            gameOverWindow.SetActive(true);
        }

        #endregion
    }
}