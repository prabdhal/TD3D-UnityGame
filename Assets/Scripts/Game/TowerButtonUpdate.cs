using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TowerButtonUpdate : MonoBehaviour
    {
        public Image image;
        public Text nameText;
        public Text goldText;

        public string turretName;
        public int gold;

        void OnValidate()
        {
            UpdateUI();
        }

        void Start()
        {
            UpdateUI();
        }

        void Update()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            nameText.text = turretName;
            goldText.text = gold.ToString();
        }
    }
}
