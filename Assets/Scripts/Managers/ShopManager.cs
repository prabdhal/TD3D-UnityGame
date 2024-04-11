using UnityEngine;

namespace TowerDefence
{
    public class ShopManager : MonoBehaviour
    {
        MainMenuManager mainManager;
        PlayerDataManager playerDataManager;

        public int goldCost;
        public int healthCost;
        public int bombCost;
        public int freezeTimeCost;

        private void Start()
        {
            playerDataManager = PlayerDataManager.instance;
            mainManager = MainMenuManager.instance;
            //PlayerDataManager.Gem += 100;
        }

        public void BuyStartGold()
        {
            if (PlayerDataManager.Gem < goldCost)
                return;

            PlayerDataManager.Gem -= goldCost;

            PlayerDataManager.GoldItemAmount += 1;

            mainManager.UpdateCurrency();
            SaveSystem.SaveData(playerDataManager);
        }

        public void BuyHealth()
        {
            if (PlayerDataManager.Gem < healthCost)
                return;

            PlayerDataManager.Gem -= healthCost;

            PlayerDataManager.HealthItemAmount += 1;

            mainManager.UpdateCurrency();
            SaveSystem.SaveData(playerDataManager);
        }

        public void BuyBomb()
        {
            if (PlayerDataManager.Gem < bombCost)
                return;

            PlayerDataManager.Gem -= bombCost;

            PlayerDataManager.BombItemAmount += 1;

            mainManager.UpdateCurrency();
            SaveSystem.SaveData(playerDataManager);
        }

        public void BuyFreezeTime()
        {
            if (PlayerDataManager.Gem < freezeTimeCost)
                return;

            PlayerDataManager.Gem -= freezeTimeCost;

            PlayerDataManager.FreezeTimeItemAmount += 1;

            mainManager.UpdateCurrency();
            SaveSystem.SaveData(playerDataManager);
        }
    }
}
