using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject topParent;

    public bool alive = true;
    public float maxHealth;
    public float health;
    public float speedModifier;
    public float attackSpeedModifier;
    public float maxEnergy;
    public float energy;
    public int displayedEnergy; // used to display in place of energy to show better looking scale
    public SpriteRenderer sprite;
    public AudioClip deathSound;
    public AudioClip takeDamageSound;
    
    public AudioSource audioSource;
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

    #region Set, Obtain, and Lose resources
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
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || (health <= 0 && energy <= 0))
            return;
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public void LoseHealth(int value, bool attack)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || (health <= 0 && energy <= 0))
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
            health = 0;
    }
    #endregion

    private void Death()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        alive = false;
        audioSource.PlayOneShot(deathSound);
    }

    private float CheckValue(float value, float maxValue)
    {
        if (value > maxValue)
            value = maxValue;

        return value;
    }
}
