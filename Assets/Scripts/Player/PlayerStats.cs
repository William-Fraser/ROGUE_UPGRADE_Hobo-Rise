using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Public Fields
    public SpriteRenderer sprite;
    public AudioClip deathSound;
    public AudioClip takeDamageSound;
    public AudioSource audioSource;
    #endregion

    #region Private Fields
    private bool alive = true;
    private float maxHealth;
    private float health;
    private float speedModifier;
    private float attackSpeedModifier;
    private float maxEnergy;
    private float energy;
    private int displayedEnergy; // used to display in place of energy to show better looking scale
    #endregion

    #region Unity Messages
    private void Start()
    {
        if (GetComponentInChildren<SpriteRenderer>())
            sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;

        if (health == 0)
        { 
            Death();
        }
        LoseEnergy(Time.deltaTime);
        displayedEnergy = Convert.ToInt16(energy);
        if(energy <= 0)
        {
            Death();
        }
    }
    #endregion

    #region Resets
    public void ResetInGameStats()
    {
        health = maxHealth;
        energy = maxEnergy;
        alive = true;
    }
    public void ResetMainStats()
    {
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        maxHealth = stats.maxHealth;
        speedModifier = stats.speedModifier;
        attackSpeedModifier = stats.attackSpeedModifier;
        maxEnergy = stats.maxEnergy;
    }
    #endregion

    #region Obtain and Lose resources
    public void ObtainEnergy(int value)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || health <= 0 || energy <= 0)
            return;

        energy += value;

        energy = CheckValue(energy, maxEnergy);

        GameManager.gameManager.DisplayGUIPopup("MAX NRG", transform.position, Color.green);
    }
    public void LoseEnergy(float value)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || health <= 0 || energy <= 0)
            return;
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public void LoseHealth(int value, bool attack)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || health <= 0 || energy <= 0)
            return;
        if (health - value < 0)
        {
            health = 0;
        }
        else // take damage 
        {
            audioSource.PlayOneShot(takeDamageSound);
            health -= value;
        }
        GameManager.gameManager.DisplayGUIPopup("-" + value, this.transform.position, Color.red);

        if (health < 0)
        {
            audioSource.Stop();
            health = 0;
        }
    }
    #endregion

    #region Get Stats
    public float GetSpeed() { return speedModifier; }
    public float GetAttackSpeed() { return attackSpeedModifier; }
    public float GetMaxHealth() { return maxHealth; }
    public float GetHealth() { return health; }
    public float GetMaxEnergy() { return maxEnergy; }
    public float GetEnergy() { return energy; }
    public int GetDisplayedEnergy() { return displayedEnergy; }
    public bool GetAlive() { return alive; }
    #endregion

    #region Misc
    private void Death()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        alive = false;
        
        if (gameObject.tag != "Player")
        audioSource.PlayOneShot(deathSound);
    }

    private float CheckValue(float value, float maxValue)
    {
        if (value > maxValue)
            value = maxValue;

        return value;
    }
    #endregion
}
