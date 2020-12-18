using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TowerDefence
{
    public class EnemyLogInfoContainer : MonoBehaviour
    {
        public EnemyLogInfo enemyLogInfo;
        public Image enemyIcon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI immunityText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI armourText;
        public TextMeshProUGUI magicResistText;
        public TextMeshProUGUI goldValueText;

        public void Start()
        {
            UpdateEnemyInfo();
        }

        public void UpdateEnemyInfo()
        {
            enemyIcon.sprite = enemyLogInfo.icon;
            nameText.text = enemyLogInfo.name;
            speedText.text = "Speed: " + enemyLogInfo.enemySpeed.ToString();
            immunityText.text = "Immunity: " + enemyLogInfo.immunity.ToString();
            healthText.text = "Health: " + enemyLogInfo.health.ToString();
            armourText.text = "Armour: " + enemyLogInfo.armour.ToString();
            magicResistText.text = "Magic Resist " + enemyLogInfo.magicResist.ToString();
            goldValueText.text = "Gold Value: " + enemyLogInfo.goldValue.ToString();
        }
    }
}


