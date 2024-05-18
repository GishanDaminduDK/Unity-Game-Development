using UnityEngine;
using UnityEngine.UI;

public class ShopItemSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject spawnButton;
    public RectTransform spawnArea;

    public float Radius = 100;
    void Start()
    {
        spawnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SpawnObject);
       
    }

    void Update()
    {
        
    }

    void SpawnObject()
     {
        // Generate random coordinates within the panel boundaries
        float randomX = Random.Range(450,1550) ;//450,1550
        float randomY = Random.Range(920,980);//920,980
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        Debug.Log($"{spawnPosition},");
        // Spawn the object at the random position within the panel
        Instantiate(ItemPrefab, spawnPosition, Quaternion.identity,spawnArea);

        /*
        Vector3 randomPos = Random.insideUnitCircle * Radius;

        Instantiate(ItemPrefab, randomPos, Quaternion.identity,spawnArea);*/
    }

}



