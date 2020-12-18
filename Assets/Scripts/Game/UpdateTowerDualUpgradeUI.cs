using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace TowerDefence
{
    public class UpdateTowerDualUpgradeUI : MonoBehaviour
    {
        #region Singleton
        public static UpdateTowerDualUpgradeUI instance;

        public void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one UpdateTowerDualUpgradeUI instance");
                return;
            }
            instance = this;
        }
        #endregion
        GameManager gameManager;

        public Button upgradeButton01;
        public Button upgradeButton02;
        public Button sellButon;
        public Button closeButton;
        public Button swapButton;

        [Header("Upgrade Tower One")]
        public Image towerSprite01;
        public TextMeshProUGUI nameText01;
        public TextMeshProUGUI damageText01;
        public TextMeshProUGUI fireRateText01;
        public TextMeshProUGUI rangeText01;
        public TextMeshProUGUI descriptionText01;
        public TextMeshProUGUI upgradeCostText01;
        public TextMeshProUGUI sellValueText01;
        [Space]
        [Header("Upgrade Tower Two")]
        public Image towerSprite02;
        public TextMeshProUGUI nameText02;
        public TextMeshProUGUI damageText02;
        public TextMeshProUGUI fireRateText02;
        public TextMeshProUGUI rangeText02;
        public TextMeshProUGUI descriptionText02;
        public TextMeshProUGUI upgradeCostText02;
        [Space]
        [Header("Current Tower")]
        public GameObject currentStatWindow;
        public Image towerSprite;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI fireRateText;
        public TextMeshProUGUI rangeText;
        public TextMeshProUGUI descriptionText;
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

        public void UpdateUI(TowerData upgradeData01, TowerData upgradeData02, TowerData currentData)
        {
            if (upgradeData01 != null)
            {
                towerSprite01.sprite = upgradeData01.icon;
                nameText01.text = upgradeData01.towerName;
                damageText01.text = "Damage: " + upgradeData01.minDamage + " - " + upgradeData01.maxDamage;
                fireRateText01.text = "Fire Rate: " + upgradeData01.fireRate;
                rangeText01.text = "Range: " + upgradeData01.range;
                descriptionText01.text = upgradeData01.description;
                upgradeCostText01.text = upgradeData01.cost.ToString();
                sellValueText01.text = upgradeData01.sellValue.ToString();
            }

            if (upgradeData02 != null)
            {
                towerSprite02.sprite = upgradeData02.icon;
                nameText02.text = upgradeData02.towerName;
                damageText02.text = "Damage: " + upgradeData02.minDamage + " - " + upgradeData02.maxDamage;
                fireRateText02.text = "Fire Rate: " + upgradeData02.fireRate;
                rangeText02.text = "Range: " + upgradeData02.range;
                descriptionText02.text = upgradeData02.description;
                upgradeCostText02.text = upgradeData02.cost.ToString();
            }
            
            towerSprite.sprite = currentData.icon;
            nameText.text = currentData.towerName;
            damageText.text = "Damage: " + currentData.minDamage + " - " + currentData.maxDamage;
            fireRateText.text = "Fire Rate: " + currentData.fireRate;
            rangeText.text = "Range: " + currentData.range;
            descriptionText.text = currentData.description;
        }

        public void SwapButton()
        {
            currentStatWindow.SetActive(!currentStatWindow.activeInHierarchy);
            if (currentStatWindow.activeInHierarchy)
                swap.color = Color.gray;
            else
                swap.color = Color.white;
        }

        public void UpgradeButton01()
        {
            gameManager.UpgradeTowerButton01();
        }

        public void UpgradeButton02()
        {
            gameManager.UpgradeTowerButton02();
        }

        public void SellButton()
        {
            gameManager.SellTowerButton();
        }
    }
}
