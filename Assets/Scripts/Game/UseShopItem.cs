using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UseShopItem : MonoBehaviour
    {
        public bool isGoldItem;
        public bool isBombItem;
        public bool isHealthItem;
        public bool isFreezeItem;

        public Image image;
        public Sprite icon;
        public TextMeshProUGUI itemAmountText;

        private void Start()
        {
            image.sprite = icon;

            if (isGoldItem)
                itemAmountText.text = PlayerDataManager.GoldItemAmount.ToString();
            if (isBombItem)
                itemAmountText.text = PlayerDataManager.BombItemAmount.ToString();
            if (isHealthItem)
                itemAmountText.text = PlayerDataManager.HealthItemAmount.ToString();
            if (isFreezeItem)
                itemAmountText.text = PlayerDataManager.FreezeTimeItemAmount.ToString();
        }

        private void Update()
        {
            if (isBombItem)
                itemAmountText.text = PlayerDataManager.BombItemAmount.ToString();
        }

        public void GoldItem()
        {
            if (PlayerDataManager.GoldItemAmount <= 0 || GameManager.BeginTutorial)
                return;

            PlayerDataManager.GoldItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.GoldItemAmount.ToString();
            PlayerStats.Gold += 200;
        }

        public void BombItem()
        {
            if (PlayerDataManager.BombItemAmount <= 0 || GameManager.BeginTutorial)
                return;

            GameItemManager.activeItem = true;
        }

        public void HealthItem()
        {
            if (PlayerDataManager.HealthItemAmount <= 0 || GameManager.BeginTutorial)
                return;

            PlayerDataManager.HealthItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.HealthItemAmount.ToString();
            PlayerStats.Health += 25;
        }

        public void FreezeItem()
        {
            if (PlayerDataManager.FreezeTimeItemAmount <= 0 || GameManager.BeginTutorial)
                return;

            PlayerDataManager.FreezeTimeItemAmount -= 1;
            itemAmountText.text = PlayerDataManager.FreezeTimeItemAmount.ToString();
            GameItemManager.freezeEnemies = true;
        }
    }
}
