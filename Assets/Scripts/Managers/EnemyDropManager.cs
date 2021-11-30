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

    public void SpawnHoboDrop(Vector3 position)
    {
        GameObject drop = GameObject.Instantiate(hoboDrop);
        drop.transform.position = position;
    }
    public void SpawnTaxCollectorDrop(Vector3 position)
    {
        GameObject drop = GameObject.Instantiate(taxCollectorDrop);
        drop.transform.position = position;
    }
    public void SpawnPoliceDrop(Vector3 position)
    {
        GameObject drop = GameObject.Instantiate(policeDrop);
        drop.transform.position = position;
    }
}
