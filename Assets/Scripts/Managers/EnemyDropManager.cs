using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    public static EnemyDropManager coinManager;
    public GameObject hoboDrop;
    public GameObject taxCollectorDrop;
    public GameObject policeDrop;
    void Start()
    {
        coinManager = this; 
    }

    public void SpawnBronzeCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(hoboDrop);
        coin.transform.position = position;
    }
    public void SpawnGoldCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(taxCollectorDrop);
        coin.transform.position = position;
    }
    public void SpawnBill(Vector3 position)
    {
        GameObject bill = GameObject.Instantiate(policeDrop);
        bill.transform.position = position;
    }
}
