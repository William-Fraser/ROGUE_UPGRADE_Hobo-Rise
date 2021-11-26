using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager coinManager;
    public GameObject bronzeCoinPrefab;
    public GameObject goldCoinPrefab;
    public GameObject billPrefab;
    void Start()
    {
        coinManager = this; 
    }

    public void SpawnBronzeCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(bronzeCoinPrefab);
        coin.transform.position = position;
    }
    public void SpawnGoldCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(goldCoinPrefab);
        coin.transform.position = position;
    }
    public void SpawnBill(Vector3 position)
    {
        GameObject bill = GameObject.Instantiate(billPrefab);
        bill.transform.position = position;
    }
}
