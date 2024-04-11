using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class GameButtonManager : MonoBehaviour
    {
        public Button settingsButton;
        public Button playButton;
        public Button fastForwardButton;
        [Space]
        public Sprite pauseSprite;
        public Sprite playSprite;
        public Image playButtonSprite;
        [HideInInspector]
        public bool isFastFowarded;

        public void Start()
        {
            // Enable interactability once round one begins
            playButton.interactable = false;
            fastForwardButton.interactable = false;
            fastForwardButton.image.color = Color.white;

            playButton.onClick.AddListener(TogglePause);
            fastForwardButton.onClick.AddListener(ToggleFastForward);
        }

        public void EnableButtons()
        {
            playButton.interactable = true;
            fastForwardButton.interactable = true;
            settingsButton.interactable = true;
        }

        public void TogglePause()
        {
            if (!GameManager.IsPaused)
            {
                // update pause button
                playButtonSprite.sprite = pauseSprite;

                // update fastforward button
                fastForwardButton.image.color = Color.white;
                isFastFowarded = false;

                Time.timeScale = 0;
            }
            else
            {
                playButtonSprite.sprite = playSprite;
                Time.timeScale = 1;
            }

            GameManager.IsPaused = !GameManager.IsPaused;
        }

        public void ToggleFastForward()
        {
            if (GameManager.IsPaused)
            {
                GameManager.IsPaused = false;
                isFastFowarded = false;
            }

            if (!isFastFowarded)
            {
                fastForwardButton.image.color = Color.grey;
                Time.timeScale = 2;
            }
            else
            {
                fastForwardButton.image.color = Color.white;
                Time.timeScale = 1;
            }

            isFastFowarded = !isFastFowarded;
        }
    }
}
