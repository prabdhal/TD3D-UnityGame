using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TowerDefence
{
    public class TowerCostUI : MonoBehaviour
    {
        public TowerData[] towers;

        public TextMeshProUGUI towerCost01;
        public TextMeshProUGUI towerCost02;
        public TextMeshProUGUI towerCost03;
        public TextMeshProUGUI towerCost04;

        public void Start()
        {
            towerCost01.text = towers[0].cost.ToString();
            towerCost02.text = towers[1].cost.ToString();
            towerCost03.text = towers[2].cost.ToString();
            towerCost04.text = towers[3].cost.ToString();
        }
    }
}