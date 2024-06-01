using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class GemGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    [SerializeField] private float generationTime = 2f; // Time in seconds to generate a gem
    [SerializeField] private int gemsPerGeneration = 1; // Number of gems generated each time
    [SerializeField] private float radius = 1f; // Radius of the circle
    [SerializeField] private Slider progressBar; // UI element to show progress

    private float timer = 0f;
    private bool isGenerating = true;

    void Start()
    {
        if (progressBar != null)
        {
            progressBar.maxValue = generationTime;
            progressBar.value = 0;
        }
    }

    void Update()
    {
        if (isGenerating)
        {
            timer += Time.deltaTime;

            if (progressBar != null)
            {
                progressBar.value = timer;
            }

            if (timer >= generationTime)
            {
                GenerateGems();
                timer = 0f;

                if (progressBar != null)
                {
                    progressBar.value = 0;
                }
            }
        }
    }

    private void GenerateGems()
    {
        for (int i = 0; i < gemsPerGeneration; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;

            // Instantiate the coin prefab at the calculated position
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }

        Debug.Log(gemsPerGeneration + " gems generated!");
    }
}
