using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace TowerDefence
{
    public class GameOver : MonoBehaviour
    {
        GameManager gameManager;
        public TextMeshProUGUI roundsText;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void OnEnable()
        {
            int rounds = PlayerStats.Rounds - 1;
            if (rounds <= 0)
                rounds = 0;
            roundsText.text = rounds.ToString();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            SceneManager.LoadScene(StringData.mainScene);
        }
    }
}
