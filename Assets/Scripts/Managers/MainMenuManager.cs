using TMPro;
using UnityEngine;

namespace TowerDefence
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Singleton
        public static MainMenuManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of MainMenuManager found!");
                return;
            }
            instance = this;
        }
        #endregion

        PlayerDataManager playerDataManager;

        public SceneFader fader;
        [Space]
        [Header("Currency")]
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI gemText;
        [Space]
        [Header("Window UIs")]
        public GameObject backgroundWindow;
        public GameObject mainWindow;
        public GameObject levelWindow;
        public GameObject turretUpgrade;
        public GameObject shopWindow;
        public GameObject menuWindow;
        public GameObject achievementsWindow;
        public GameObject enemyLogWindow;
        public GameObject settingsWindow;
        public GameObject confirmationWindow;


        private void Start()
        {
            if (playerDataManager == null)
                playerDataManager = PlayerDataManager.instance;

            if (fader == null)
                fader = FindObjectOfType<SceneFader>();
            fader.gameObject.SetActive(true);
        }

        #region Main Menu Window Functions

        public void OpenLevelSelector()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            levelWindow.SetActive(true);
        }

        public void OpenTowerUpgrade()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            turretUpgrade.SetActive(true);
        }

        public void OpenShop()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            shopWindow.SetActive(true);
        }

        public void OpenMenu()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            menuWindow.SetActive(true);
        }

        public void OpenAchievements()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            achievementsWindow.SetActive(true);
        }

        public void OpenEnemyLogWindow()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            enemyLogWindow.SetActive(true);
        }

        public void OpenSettings()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            settingsWindow.SetActive(true);
        }

        public void OpenConfirmationWindow()
        {
            CloseAll();
            mainWindow.SetActive(false);
            backgroundWindow.SetActive(true);
            confirmationWindow.SetActive(true);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void CloseAll()
        {
            levelWindow.SetActive(false);
            turretUpgrade.SetActive(false);
            shopWindow.SetActive(false);
            menuWindow.SetActive(false);
            achievementsWindow.SetActive(false);
            enemyLogWindow.SetActive(false);
            settingsWindow.SetActive(false);
            backgroundWindow.SetActive(false);
            confirmationWindow.SetActive(false);
            mainWindow.SetActive(true);
        }
        #endregion

        #region Button Functions

        public void QuitGame()
        {
            Application.Quit();
        }

        public void UpdateCurrency()
        {
            moneyText.text = PlayerDataManager.Money.ToString();
            gemText.text = PlayerDataManager.Gem.ToString();
        }
        #endregion
    }
}
