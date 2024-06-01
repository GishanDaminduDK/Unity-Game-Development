//using UnityEngine;
//using UnityEngine.UI;
//using static UnityEngine.UI.Image;

//public class GemGenerator : MonoBehaviour
//{
//    public GameObject coinPrefab;
//    [SerializeField] private float generationTime = 2f; // Time in seconds to generate a gem
//    [SerializeField] private int gemsPerGeneration = 1; // Number of gems generated each time
//    [SerializeField] private float radius = 1f; // Radius of the circle
//    [SerializeField] private Slider progressBar; // UI element to show progress

//    private float timer = 0f;
//    private bool isGenerating = true;

//    void Start()
//    {
//        if (progressBar != null)
//        {
//            progressBar.maxValue = generationTime;
//            progressBar.value = 0;
//        }
//    }

//    void Update()
//    {
//        if (isGenerating)
//        {
//            timer += Time.deltaTime;

//            if (progressBar != null)
//            {
//                progressBar.value = timer;
//            }

//            if (timer >= generationTime)
//            {
//                GenerateGems();
//                timer = 0f;

//                if (progressBar != null)
//                {
//                    progressBar.value = 0;
//                }
//            }
//        }
//    }

//    private void GenerateGems()
//    {
//        for (int i = 0; i < gemsPerGeneration; i++)
//        {
//            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;

//            // Instantiate the coin prefab at the calculated position
//            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
//        }

//        Debug.Log(gemsPerGeneration + " gems generated!");
//    }
//}
using UnityEngine;
using UnityEngine.UI;

public class GemGenerator : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab to be instantiated
    [SerializeField] private float generationTime = 2f; // Time in seconds to generate a gem
    [SerializeField] private int gemsPerGeneration = 1; // Number of gems generated each time
    [SerializeField] private float radius = 1f; // Radius of the circle for random positioning
    [SerializeField] private Slider progressBar; // UI element to show progress

    private float timer = 0f; // Timer to track time until next generation
    private bool isGenerating = true; // Flag to control gem generation

    void Start()
    {
        if (progressBar == null)
        {
            // If no progressBar is set, we simply don't set it up.
        }
        else
        {
            progressBar.maxValue = generationTime;
            progressBar.value = 0;
        }
    }

    void Update()
    {
        if (!isGenerating || coinPrefab == null)
        {
            return; // Exit the update if not generating or if prefab is null or destroyed.
        }

        timer += Time.deltaTime;

        if (progressBar != null)
        {
            progressBar.value = timer;
        }

        if (timer >= generationTime)
        {
            GenerateGems();
            timer = 0f; // Reset timer

            if (progressBar != null)
            {
                progressBar.value = 0;
            }
        }
    }

    private void GenerateGems()
    {
        // Checking if coinPrefab is still valid. If it's null, no error is logged and no action is taken.
        if (coinPrefab == null)
        {
            return; // Skip gem generation if the prefab is null or destroyed.
        }

        for (int i = 0; i < gemsPerGeneration; i++)
        {
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;
            GameObject instance = Instantiate(coinPrefab, randomPosition, Quaternion.identity);

            // Here we could also check if instance is not null if needed.
        }
    }
}
