using System;
using System.Collections;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EnvironmentController : MonoBehaviour
{
    // Reference to the GameObject tree_40
    GameObject tree_40;
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIzOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOQ";
    private string loginEndpoint = "http://20.15.114.131:8080/api/login";
    private string profileEndpoint = "http://20.15.114.131:8080/api/power-consumption/current/view";
    public static string password = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIzOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOQ";
    private float timeSinceLastRequest = 0f;
    private float requestInterval = 10f; // Time in seconds between requests

    public static string jwtToken = "nulljwtToken";  // To pass jwt token to forward scene
    public string jwt = "";

    public IEnumerator PostRequest()
    {
        string json = "{\"apiKey\":\"" + apiKey + "\"}";

        // Use Unity's built-in JSON utility to convert JSON string to bytes
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, "POST")) // request handling
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(data);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                jwt = response;
                Debug.Log("You are successfully authenticated. Your JWT token is: " + jwt);

                

                try
                {
                    // Parse the JSON string
                    LoginResponse jsonResponse = JsonUtility.FromJson<LoginResponse>(response);

                    // Extract the JWT token value
                    jwtToken = jsonResponse.token;

                    Debug.Log(jwtToken);

                }
                catch (Exception ex)
                {
                    Debug.Log("Error parsing JSON: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                SceneManager.LoadScene("ErrorPage");
            }
        }
    }

    public IEnumerator getCurrentPowerConsumption()
    {
        using (UnityWebRequest requestnew = UnityWebRequest.Get(profileEndpoint))
        {
            // Properly formatted authorization header
            requestnew.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            // Send the request
            yield return requestnew.SendWebRequest();

            // Check if the request was successful
            if (requestnew.result == UnityWebRequest.Result.Success)
            {
                // Successfully received response
                string jsonResponse = requestnew.downloadHandler.text;

                try
                {
                    // Parse the JSON using JsonUtility
                    PowerConsumptionResponse response = JsonUtility.FromJson<PowerConsumptionResponse>(jsonResponse);

                    // Log the current power consumption
                    Debug.Log("Current Power Consumption is: " + response.currentConsumption);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("JSON Parse Error: " + ex.Message);
                }
            }
            else if (requestnew.result == UnityWebRequest.Result.ConnectionError ||
                     requestnew.result == UnityWebRequest.Result.ProtocolError)
            {
                // Log error
                Debug.LogError("Error: " + requestnew.error);
            }
            else
            {
                // Handle other types of errors
                Debug.LogError("Request failed: " + requestnew.error);
            }
        }
    }

    [System.Serializable]
    public class PowerConsumptionResponse
    {
        public float currentConsumption;  // Adjust the type if necessary
    }


    // Example of a class structure for your JSON response


    // Start is called before the first frame update
    void Start()
    {
        // Finding the GameObject tree_40
        tree_40 = GameObject.Find("tree_40");
        StartCoroutine(PostRequest());
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer every frame
        timeSinceLastRequest += Time.deltaTime;

        // Check if 10 seconds have passed
        if (timeSinceLastRequest >= requestInterval)
        {
            // Reset the timer
            timeSinceLastRequest = 0f;

            // Start the GET request coroutine
            StartCoroutine(getCurrentPowerConsumption());
        }

        // Getting the SpriteRenderer component attached to this GameObject
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Changing the color of tree_40 to red
        if (tree_40 != null)
        {
            SpriteRenderer treeRenderer = tree_40.GetComponent<SpriteRenderer>();
            if (treeRenderer != null)
            {
                treeRenderer.color = Color.red;
            }
            else
            {
                Debug.LogError("tree_40 doesn't have a SpriteRenderer component!");
            }
        }
        else
        {
            Debug.LogError("GameObject tree_40 not found!");
        }
    }

    // Class to represent login response
    [Serializable]
    public class LoginResponse
    {
        public string token;
    }
}
