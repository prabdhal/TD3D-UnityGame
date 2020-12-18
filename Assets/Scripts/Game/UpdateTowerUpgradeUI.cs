using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace TowerDefence
{
    public class UpdateTowerUpgradeUI : MonoBehaviour
    {
        #region Singleton
        public static UpdateTowerUpgradeUI instance;

        public void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one UpdateTowerUpgradeUI instance");
                return;
            }
            instance = this;
        }
        #endregion

        GameManager gameManager;
        public Button upgradeButton;
        public Button sellButon;
        public Button closeButton;
        public Button swapButton;

        [Header("Upgrade Tower")]
        public Image towerSprite;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI fireRateText;
        public TextMeshProUGUI rangeText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI upgradeCostText;
        public TextMeshProUGUI sellValueText;
        public TextMeshProUGUI messageText;
        [Space]
        [Header("Current Tower")]
        public GameObject currentStatWindow;
        public Image currentTowerSprite;
        public TextMeshProUGUI currentNameText;
        public TextMeshProUGUI currentDamageText;
        public TextMeshProUGUI currentFireRateText;
        public TextMeshProUGUI currentRangeText;
        public TextMeshProUGUI currentDescriptionText;
        [Space]
        public Image swap;


        private void OnEnable()
        {
            currentStatWindow.SetActive(false);
        }

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            gameObject.SetActive(false);
        }

        public void UpdateUI(TowerData upgradeData, TowerData currentData)
        {
            messageText.text = null;

            if (upgradeData != null)
            {
                towerSprite.sprite = upgradeData.icon;
                nameText.text = upgradeData.towerName;
                damageText.text = "Damage: " + upgradeData.minDamage + " - " + upgradeData.maxDamage;
                fireRateText.text = "Fire Rate: " + upgradeData.fireRate;
                rangeText.text = "Range: " + upgradeData.range;
                descriptionText.text = upgradeData.description;
                upgradeCostText.text = upgradeData.cost.ToString();
                sellValueText.text = upgradeData.sellValue.ToString();
            } else
            {
                messageText.text = "Tower upgrade not unlocked yet! Buy tower upgrades from the upgrade shop found on the main screen";
            }

            currentTowerSprite.sprite = currentData.icon;
            currentNameText.text = currentData.towerName;
            currentDamageText.text = "Damage: " + currentData.minDamage + " - " + currentData.maxDamage;
            currentFireRateText.text = "Fire Rate: " + currentData.fireRate;
            currentRangeText.text = "Range: " + currentData.range;
            currentDescriptionText.text = currentData.description;
        }

        public void NullifyUI(TowerData upgradeData, TowerData currentData)
        {
            towerSprite.sprite = null;
            nameText.text = null;
            damageText.text = null;
            fireRateText.text = null;
            rangeText.text = null;
            descriptionText.text = null;
            upgradeCostText.text = null;
            sellValueText.text = null;

            currentTowerSprite.sprite = null;
            currentNameText.text = null;
            currentDamageText.text = null;
            currentFireRateText.text = null;
            currentRangeText.text = null;
            currentDescriptionText.text = null;
        }

        public void SwapButton()
        {
            currentStatWindow.SetActive(!currentStatWindow.activeInHierarchy);
            if (currentStatWindow.activeInHierarchy)
                swap.color = Color.gray;
            else
                swap.color = Color.white;
        }

        public void UpgradeTowerButton01()
        {
            gameManager.UpgradeTowerButton01();
        }
        
        public void SellTowerButton()
        {
            gameManager.SellTowerButton();
        }
    }
}
