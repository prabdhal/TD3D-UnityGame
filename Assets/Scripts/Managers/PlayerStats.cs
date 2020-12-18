using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one PlayerStats in scene");
            return;
        }
        instance = this;
    }
    #endregion
    
    public static int Gold;
    public static int Rounds;
    public static float Health;
    public static float Score;

    [Header("Player Gold")]
    public int startGold = 400;
    public TextMeshProUGUI goldText;
    [Space]
    [Header("Player Health")]
    public float startHealth = 300;
    public Image healthFill;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        Gold = startGold;
        Health = startHealth;
        
        healthFill.fillAmount = Health / startHealth;
        healthText.text = Health.ToString();

        Rounds = 0;
    }

    private void Update()
    {
        goldText.text = Gold.ToString("0");
    }

    public void UpdateHealth(float damage)
    {
        Health = Mathf.Clamp(Health, 0, startHealth);
        Health -= damage;
        healthFill.fillAmount = Health / startHealth;
        healthText.text = Health.ToString();

    }
}
