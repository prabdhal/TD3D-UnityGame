using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace TowerDefence
{
    public class ConfirmationTowerUI : MonoBehaviour
    {
        #region Singleton
        public static ConfirmationTowerUI instance;

        public void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one ConfirmationTowerUI instance");
                return;
            }
            instance = this;
        }
        #endregion
        GameManager gameManager;

        public Button yesButton;
        public Button noButton;
        public Button closeButton;

        [Header("Active Shop Tower")]
        public Image towerSprite;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI fireRateText;
        public TextMeshProUGUI rangeText;
        public TextMeshProUGUI description;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            gameObject.SetActive(false);
        }

        public void UpdateUI(TowerData data)
        {
            towerSprite.sprite = data.icon;
            nameText.text = data.towerName;
            damageText.text = "Damage: " + data.minDamage + " - " + data.maxDamage;
            fireRateText.text = "Fire Rate: " + data.fireRate;
            rangeText.text = "Range: " + data.range;
            description.text = data.description;
        }

        public void YesButton()
        {
            gameManager.InstantiateTowerButton();
        }

        public void NoButton()
        {
            gameManager.CloseConfirmationWindow();
        }
    }
}
