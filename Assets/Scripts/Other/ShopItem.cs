using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefence
{
    public class ShopItem : MonoBehaviour
    {
        [Header("Shop UI")]
        public Image image;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI costText;
        public Button button;

        [Header("Shop Item Info")]
        public Sprite icon;
        public string itemName;
        [TextArea(1,3)]
        public string description;
        public int cost;

        public void Start()
        {
            image.sprite = icon;
            itemNameText.text = itemName;
            descriptionText.text = description;
            costText.text = cost.ToString();
            if (button == null)
                button = GetComponentInChildren<Button>();

            if (PlayerDataManager.Gem < cost)
                button.interactable = false;
            else
                button.interactable = true;
        }
    }
}