using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Rewards : MonoBehaviour
    {
        public static Rewards instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of Rewards found!");
                return;
            }

            instance = this;
        }

        public int rewardMoney = 25;
        public int rewardGem = 4;
        public float scoreRatio = 10f;
        public float fillSpeed = 5;
    }
}
