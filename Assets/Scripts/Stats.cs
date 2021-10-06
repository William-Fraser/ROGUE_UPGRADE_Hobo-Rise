using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public bool alive = true;
    public int maxHealth;
    public int health;
    public float speedModifier;
    public float damageModifier;
    public float attackSpeedModifier;
    public int maxEnergy;
    public float energy;
    public int displayedEnergy; // used to display in place of energy to show better looking scale
    public float collectedMoney; // money collected in a single run
    public float totalMoney; // total collected money in game

    public Text displayedHealth;

    private void Update()
    {

        displayedHealth.text = $"{health}";
        if (health <= 0)
            Death();
    }
    private void Awake()
    {
        /*if(this.gameObject.tag == "Player")
        {
            maxHealth = GameManager.gameManager.data.maxHealth;
            speedModifier = GameManager.gameManager.data.speedModifier;
            damageModifier = GameManager.gameManager.data.damageModifier;
            attackSpeedModifier = GameManager.gameManager.data.attackSpeedModifier;
            maxEnergy = GameManager.gameManager.data.maxEnergy;
            totalMoney = GameManager.gameManager.data.totalMoney;
        }*/
    }

    #region Set, Obtain, and Lose resources
    public float CollectedMoney { set { collectedMoney = value; } }
    public void ObtainCoins(float value)
    {
        collectedMoney += value;
    }
    public void LoseCoins(float value)
    {
        collectedMoney -= value;
        if (collectedMoney < 0)
            collectedMoney = 0;
    }
    public int EnergyAmount { set { energy = value; } }
    public void ObtainEnergy(int value)
    {
        energy += value;
    }
    public void LoseEnergy(int value)
    {
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public int Health { set { health = value; } }
    public void ObtainHealth(int value)
    {
        health += value;
    }
    public void LoseHealth(int value)
    {
        health -= value;
        if (health < 0)
            health = 0;
    }
    #endregion

    private void Death()
    {
        alive = false;
        /*if (this.gameObject.tag == "Player")
        {
            GameManager.gameManager.data.maxHealth = maxHealth;
            GameManager.gameManager.data.speedModifier = speedModifier;
            GameManager.gameManager.data.damageModifier = damageModifier;
            GameManager.gameManager.data.attackSpeedModifier = attackSpeedModifier;
            GameManager.gameManager.data.maxEnergy = maxEnergy;
            GameManager.gameManager.data.totalMoney = totalMoney;
        }*/
        Color colour = GetComponent<SpriteRenderer>().color;
        colour.a = 0; 
        GetComponent<SpriteRenderer>().color = colour;
        if (GetComponent<Rigidbody2D>() != null)
        { transform.gameObject.SetActive(false); }
        else if (GetComponentInParent<Rigidbody2D>())
        { transform.parent.gameObject.SetActive(false); }
        
    }
}
