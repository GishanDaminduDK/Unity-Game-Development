using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab1; // Public field to drag your
    public GameObject coinPrefab2;
    public GameObject coinPrefab3;
    public GameObject coinPrefab4;
    public GameObject coinPrefab5;
    public GameObject coinPrefab6;
    public GameObject coinPrefab7;
    public GameObject coinPrefab8;
    public GameObject coinPrefab9;
    public GameObject coinPrefab10;

    private void Start()
    {
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        // Position where you want to spawn the coin
        Vector3 position1 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab1, position1, Quaternion.identity);
        Vector3 position2 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab2, position2, Quaternion.identity);
        Vector3 position3 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab3, position3, Quaternion.identity);
        Vector3 position4 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab4, position4, Quaternion.identity);
        Vector3 position5 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab5, position5, Quaternion.identity);
        Vector3 position6 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab6, position6, Quaternion.identity);
        Vector3 position7 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab7, position7, Quaternion.identity);
        Vector3 position8 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab8, position8, Quaternion.identity);
        Vector3 position9 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab9, position9, Quaternion.identity);
        Vector3 position10 = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        // Instantiate the coin at the position
        Instantiate(coinPrefab10, position10, Quaternion.identity);

    }
}
