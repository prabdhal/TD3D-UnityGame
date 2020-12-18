using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TabController : MonoBehaviour
    {
        public Sprite activeTab;
        public Sprite inactiveTab;
        public Image[] tabs;
        public GameObject[] skillPages;

        private void Start()
        {
            TabClick(0);
        }

        public void TabClick(int index)
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                tabs[i].sprite = inactiveTab;
                skillPages[i].SetActive(false);
            }

            tabs[index].sprite = activeTab;
            skillPages[index].SetActive(true);
        }
    }
}

