using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace TowerDefence
{
    public class TowerUpgradeShopButton : MonoBehaviour
    {
        public TowerUpgradeShopButton refTowerButton;
        PlayerDataManager playerDataManager;
        MainMenuManager main;
        public Sprite interactableButton;
        public Sprite disabledButton;

        public Button button;
        [Header("Tower Upgrade Type")]
        [Range(1, 12)]
        public int towerTypeIndex = 1;      // Arrow, Fire, Cannon, Poison
        public TowerData[] towers;
        public int[] upgradeCosts;

        [Space]
        public int indexLimit;
        public int requiredTowerIndex;
        [Space]
        public string towerName;
        [TextArea(7, 8)]
        public string[] statIncreases;
        [TextArea(7, 8)]
        public string[] statDescription;

        [Range(1,2)]
        public int upgradeIndex;

        public bool isComplete;

        [Header("Tower UI")]
        public TextMeshProUGUI costText;
        public TextMeshProUGUI indexText;
        public TextMeshProUGUI towerNameText;
        public TextMeshProUGUI statIncreasesText;
        public TextMeshProUGUI statDescriptionText;

        public int buttonIndex = 0;

        void OnEnable()
        {
            switch (towerTypeIndex)
            {
                case 1:
                    buttonIndex = PlayerDataManager.arrowTowerIndex01;
                    break;
                case 2:
                    buttonIndex = PlayerDataManager.arrowTowerIndex02;
                    break;
                case 3:
                    buttonIndex = PlayerDataManager.arrowTowerIndex03;
                    break;
                case 4:
                    buttonIndex = PlayerDataManager.fireTowerIndex01;
                    break;
                case 5:
                    buttonIndex = PlayerDataManager.fireTowerIndex02;
                    break;
                case 6:
                    buttonIndex = PlayerDataManager.fireTowerIndex03;
                    break;
                case 7:
                    buttonIndex = PlayerDataManager.cannonTowerIndex01;
                    break;
                case 8:
                    buttonIndex = PlayerDataManager.cannonTowerIndex02;
                    break;
                case 9:
                    buttonIndex = PlayerDataManager.cannonTowerIndex03;
                    break;
                case 10:
                    buttonIndex = PlayerDataManager.poisonTowerIndex01;
                    break;
                case 11:
                    buttonIndex = PlayerDataManager.poisonTowerIndex02;
                    break;
                case 12:
                    buttonIndex = PlayerDataManager.poisonTowerIndex03;
                    break;
            }

            UpdateBaseTextUI();
        }

        void Start()
        {
            playerDataManager = PlayerDataManager.instance;
            main = MainMenuManager.instance;
        }

        public void OnClickBaseUpgrade()
        {
            if (refTowerButton != null)
            {
                if (refTowerButton.isComplete == false)
                    return;
            }
            if (buttonIndex >= indexLimit) return;

            if (PlayerDataManager.Money < upgradeCosts[buttonIndex]) return;

            PlayerDataManager.Money -= upgradeCosts[buttonIndex];
            main.UpdateCurrency();

            // add upgrade tower
            if (upgradeIndex == 1)
                towers[buttonIndex].upgrade01 = towers[buttonIndex + 1];
            if (upgradeIndex == 2)
                towers[buttonIndex].upgrade02 = towers[buttonIndex + 1];
            
            buttonIndex++;

            // keeps track of upgrade index for loading data
            switch (towerTypeIndex)
            {
                case 1:
                    PlayerDataManager.arrowTowerIndex01++;
                    break;
                case 2:
                    PlayerDataManager.arrowTowerIndex02++;
                    break;
                case 3:
                    PlayerDataManager.arrowTowerIndex03++;
                    break;
                case 4:
                    PlayerDataManager.fireTowerIndex01++;
                    break;
                case 5:
                    PlayerDataManager.fireTowerIndex02++;
                    break;
                case 6:
                    PlayerDataManager.fireTowerIndex03++;
                    break;
                case 7:
                    PlayerDataManager.cannonTowerIndex01++;
                    break;
                case 8:
                    PlayerDataManager.cannonTowerIndex02++;
                    break;
                case 9:
                    PlayerDataManager.cannonTowerIndex03++;
                    break;
                case 10:
                    PlayerDataManager.poisonTowerIndex01++;
                    break;
                case 11:
                    PlayerDataManager.poisonTowerIndex02++;
                    break;
                case 12:
                    PlayerDataManager.poisonTowerIndex03++;
                    break;
            }

            indexText.text = buttonIndex + "/" + indexLimit;
            
            // prevents index error from occuring by returning out 
            if (buttonIndex >= indexLimit)
            {
                isComplete = true;
                button.interactable = false;
                button.image.sprite = disabledButton;
                playerDataManager.SaveProgress(playerDataManager);
                return;
            }

            UpdateBaseTextUI();
            playerDataManager.SaveProgress(playerDataManager);
        }

        public void UpdateBaseTextUI()
        {
            int index = buttonIndex;

            if (index >= indexLimit)
            {
                isComplete = true;
                index = indexLimit - 1;
                button.interactable = false;
                button.image.sprite = disabledButton;
            }
            else
            {
                button.interactable = true;
                button.image.sprite = interactableButton;
            }

            indexText.text = buttonIndex.ToString() + "/" + indexLimit;
            towerNameText.text = towerName.ToString();
            statIncreasesText.text = statIncreases[index].ToString();
            statDescriptionText.text = statDescription[index].ToString();
            costText.text = upgradeCosts[index].ToString();
        }
        
    }
}