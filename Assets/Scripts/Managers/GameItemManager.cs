using UnityEngine;

namespace TowerDefence
{
    public class GameItemManager : MonoBehaviour
    {
        #region Singleton

        public static GameItemManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of GameItemManager found!");
                return;
            }

            instance = this;
        }
        #endregion

        public static bool activeItem;
        public static bool freezeEnemies;

        public float startTimer = 5;
        private float timer;

        private void Start()
        {
            timer = startTimer;
        }
        private void Update()
        {
            if (freezeEnemies)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = startTimer;
                    freezeEnemies = false;
                }
            }
        }
    }
}
