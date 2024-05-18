using UnityEngine;
using UnityEngine.UI;

public class SpawnItem : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject spawnButton;

    public float Radius = 1;
    void Start()
    {
        spawnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SpawnObject);
    }

    void Update()
    {
        
    }

    void SpawnObject()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Generate distance from the player
        float randomXOffset = Random.Range(-4*Radius, 4*Radius);
        float randomYOffset = Random.Range(3*Radius, 4*Radius);

        Vector3 spawnPosition = playerPosition + new Vector3(randomXOffset, randomYOffset, 0f);
        Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
    }


}



