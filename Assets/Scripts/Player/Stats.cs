using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject topParent;

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
    public SpriteRenderer sprite;
    public AudioClip deathSound;
    public AudioClip takeDamageSound;
    
    public AudioSource audioSource;

    public enum CharacterType { 
        Player,
        Hobo,
        TaxCollector,
        Police
    }
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

        if (gameObject.CompareTag(GameManager.gameManager.player.tag))
        { // why reference the tag and not the player?
            LoseEnergy(Time.deltaTime);
            displayedEnergy = Convert.ToInt16(energy);
            if(energy <= 0)
            {
                Death();
            }
        }
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        maxHealth = stats.maxHealth;
        speedModifier = stats.speedModifier;
        damageModifier = stats.damageModifier;
        attackSpeedModifier = stats.attackSpeedModifier;
        maxEnergy = stats.maxEnergy;
        totalMoney = stats.totalMoney;
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
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || health <= 0 || energy <= 0)
            return;
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public void LoseHealth(int value, bool attack)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame || health <= 0)
            return;
        if (health - value < 0)
        {
            health = 0;
        }
        else // take damage 
        {
            audioSource.PlayOneShot(takeDamageSound);
            GameManager.gameManager.DisplayGUIPopup("-"+value, this.transform.position, Color.red);
            health -= value;
            //Debug.Log($"STATS: {-value}, pos: {this.transform.position}, colour: {Color.red}");
        }

        if (attack)
        { 
            /*sprite.color = Color.red;*/
        }

        if (health < 0)
            health = 0;
    }
    #endregion

    private void Death()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        alive = false;
        //calculating drop pos / positioning from world scale
        float itemDropPosY = transform.position.y - (transform.localPosition.y + transform.localPosition.y / 2);
        //Debug.LogError($" itemDropPosY: {transform.position.y - (transform.localPosition.y / 2)}, topOfSpritePosY: {transform.position.y}, localY {transform.localPosition.y}");
        Vector3 itemDropPos = new Vector3(transform.position.x, itemDropPosY, transform.position.z);

        switch (type)
        {
            case CharacterType.Hobo:
                CoinManager.coinManager.SpawnBronzeCoin(itemDropPos);
                topParent.SetActive(false);
                break;
            case CharacterType.TaxCollector:
                CoinManager.coinManager.SpawnGoldCoin(itemDropPos);
                topParent.SetActive(false);
                break;
            case CharacterType.Police:
                CoinManager.coinManager.SpawnBill(itemDropPos);
                topParent.SetActive(false);
                break;
        }
       

        audioSource.PlayOneShot(deathSound);
        // Color colour = GetComponent<SpriteRenderer>().color;
      //  colour.a = 0; 
       // GetComponent<SpriteRenderer>().color = colour;
    }

    private float CheckValue(float value, float maxValue)
    {
        if (value > maxValue)
            value = maxValue;

        return value;

       
        

    }
}
