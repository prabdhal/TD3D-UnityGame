using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TowerDefence
{
    public class LevelSelector : MonoBehaviour
    {
        public SceneFader fader;

        public Button[] levelButtons;
        public Sprite inactive;

        private void Start()
        {
            if (fader == null)
                fader = FindObjectOfType<SceneFader>();
            
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (PlayerDataManager._levelsUnlocked - 2 >= i)
                {
                    LevelStarProgress star = levelButtons[i].GetComponentInChildren<LevelStarProgress>();
                    star.Init(PlayerDataManager.starScoresPerLevel[i]);
                }
                if (i + 1 > PlayerDataManager._levelsUnlocked)
                {
                    levelButtons[i].interactable = false;
                    levelButtons[i].image.sprite = inactive;
                }
            }
        }

        public void Select(string levelName)
        {
            fader.FadeTo(levelName);
        }
    }
}
