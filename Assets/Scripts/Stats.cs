using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public bool alive = true;
    public CharacterType type;
    public float maxHealth;
    public float health;
    public float speedModifier;
    public float damageModifier;
    public float attackSpeedModifier;
    public float maxEnergy;
    public float energy;
    public int displayedEnergy; // used to display in place of energy to show better looking scale
    public float collectedMoney; // money collected in a single run
    public float totalMoney; // total collected money in game

    private SpriteRenderer sprite;

    public enum CharacterType { 
        Player,
        Hobo,
        TaxCollector,
        Police
    }

    private void Update()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        if (health <= 0)
            Death();

        /*if (sprite.color.r < 255)
        {
            sprite.color = new Color(255, 255, 255);
        }*/

        if(this.gameObject.tag == GameManager.gameManager.player.tag)
        {
            LoseEnergy(Time.deltaTime);
            displayedEnergy = Convert.ToInt16(energy);
            if(energy <= 0)
            {
                Death();
            }
        }
    }
    private void Awake()
    {
        /*if (GetComponent<SpriteRenderer>())
            sprite = GetComponent<SpriteRenderer>();
        else if (GetComponentInChildren<SpriteRenderer>())
            sprite = GetComponentInChildren<SpriteRenderer>();
        else if (GetComponentInParent<SpriteRenderer>())
            sprite = GetComponentInParent<SpriteRenderer>();*/

        if (GameManager.gameManager == null)
            return;
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
    }
    public void ResetInGameStats()
    {
        health = maxHealth;
        energy = maxEnergy;
        collectedMoney = 0;
        alive = true;
    }
    public void ResetMainStats()
    {
        maxHealth = GameManager.gameManager.stats.maxHealth;
        speedModifier = GameManager.gameManager.stats.speedModifier;
        damageModifier = GameManager.gameManager.stats.damageModifier;
        attackSpeedModifier = GameManager.gameManager.stats.attackSpeedModifier;
        maxEnergy = GameManager.gameManager.stats.maxEnergy;
        totalMoney = GameManager.gameManager.stats.totalMoney;
    }

    #region Set, Obtain, and Lose resources
    public void ObtainEnergy(int value)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        energy += value;
        GameManager.gameManager.DisplayGUIPopup("+"+value+"%", transform.position, Color.green);
    }
    public void LoseEnergy(float value)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public void LoseHealth(int value, bool attack)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (health - value < 0)
        {
            health = 0;
        }
        else
        {
            health -= value;
        }

        if (attack)
        { 
            //sprite.color = Color.red;
        }

        Debug.Log($"STATS: {-value}, pos: {this.transform.position}, colour: {Color.red}");
        GameManager.gameManager.DisplayGUIPopup("-"+value.ToString(), this.transform.position, Color.red);

        if (health < 0)
            health = 0;
    }
    #endregion

    private void Death()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        alive = false;
        if (this.gameObject.tag == "Player")
        {
            GameManager.gameManager.stats.totalMoney += collectedMoney;
        } else
        {
            GameManager.gameManager.EnemyKilled();
            switch (type)
            {
                case CharacterType.Hobo:
                    GameManager.gameManager.SpawnBronzeCoin(this.transform.position);
                    break;
                case CharacterType.TaxCollector:
                    GameManager.gameManager.SpawnGoldCoin(this.transform.position);
                    break;
                case CharacterType.Police:
                    GameManager.gameManager.SpawnBill(this.transform.position);
                    break;
            }
        }
       // Color colour = GetComponent<SpriteRenderer>().color;
      //  colour.a = 0; 
       // GetComponent<SpriteRenderer>().color = colour;
        if (GetComponent<Rigidbody2D>() != null && this.tag != GameManager.gameManager.player.tag)
        { transform.gameObject.SetActive(false); }
        else if (GetComponentInParent<Rigidbody2D>() && this.tag != GameManager.gameManager.player.tag)
        { transform.parent.gameObject.SetActive(false); }
        
    }
}
