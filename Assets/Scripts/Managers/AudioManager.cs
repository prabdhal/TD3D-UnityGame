using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of AudioManager in scene!");
                return;
            }

            instance = this;
        }

        #endregion

        [Header("Music Audio")]
        public AudioSource musicAudio;
        public Scrollbar musicBar;
        public Image musicFill;
        public GameObject musicOn;
        public GameObject musicOff;
        bool musicTurnOn;
        [Space]
        [Header("Sound Audio")]
        public Scrollbar soundBar;
        public Image soundFill;
        public GameObject soundOn;
        public GameObject soundOff;
        bool soundTurnOn;
        [Space]
        [Header("Tower Audio")]
        public AudioSource buildAudio;
        public AudioSource sellAudio;


        private void Start()
        {
            soundBar.value = PlayerDataManager.SoundValue;
            musicBar.value = PlayerDataManager.MusicValue;
            Debug.Log(PlayerDataManager.SoundValue);
            Debug.Log(PlayerDataManager.MusicValue);
        }

        // Update is called once per frame
        void Update()
        {
            PlayerDataManager.SoundValue = soundBar.value;
            PlayerDataManager.MusicValue = musicBar.value;

            AudioHandler();

            buildAudio.volume = soundBar.value;
            sellAudio.volume = soundBar.value;
        }

        private void AudioHandler()
        {
            soundBar.value = Mathf.Clamp(soundBar.value, 0, 0.8f);
            soundFill.fillAmount = soundBar.value;

            if (soundFill.fillAmount > 0)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
            else
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
            }

            musicBar.value = Mathf.Clamp(musicBar.value, 0, 0.8f);
            musicFill.fillAmount = musicBar.value;
            musicAudio.volume = musicFill.fillAmount;

            if (musicFill.fillAmount > 0)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
            }
            else
            {
                musicOn.SetActive(false);
                musicOff.SetActive(true);
            }
        }

        public void ToggleGameSound()
        {
            soundTurnOn = !soundTurnOn;
            if (soundBar.value > 0)
                soundTurnOn = false;
            else
                soundTurnOn = true;

            if (soundTurnOn)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
                soundBar.value = 100;
            }
            else
            {
                soundOff.SetActive(true);
                soundOn.SetActive(false);
                soundBar.value = 0;
            }
        }

        public void ToggleGameMusic()
        {
            musicTurnOn = !musicTurnOn;
            if (musicBar.value > 0)
                musicTurnOn = false;
            else
                musicTurnOn = true;

            if (musicTurnOn)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
                musicBar.value = 100;
                musicAudio.volume = 100;
            }
            else
            {
                musicOff.SetActive(true);
                musicOn.SetActive(false);
                musicBar.value = 0;
                musicAudio.volume = 0;
            }
        }
    }
}
