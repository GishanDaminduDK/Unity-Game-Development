using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class EnergySavingsAlgorithm : MonoBehaviour
{
    // API Key should be stored securely and not hard-coded
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIzOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOQ";
    private readonly string loginEndpoint = "http://20.15.114.131:8080/api/login";
    private readonly string profileEndpoint = "http://20.15.114.131:8080/api/power-consumption/current/view";
    private readonly string dailyConsumptionEndpoint = "http://20.15.114.131:8080/api/power-consumption/month/daily/view";
    private readonly string dailyConsumptionCurrentMonthEndpoint = "http://20.15.114.131:8080/api/power-consumption/current-month/daily/view";

    private float timeSinceLastRequest = 0f;
    private float requestInterval = 10f;
    public static string jwtToken;  // JWT Token to be used for API requests
    public List<float> powerConsumptionValuesArray = new List<float>(); // List to store power consumption values
    public int year1 = 2024;   // Example year for API requests
    public string month1 = "JANUARY"; // Example month for API requests
    public static string key_val_test="0";

    void Start()
    {
        InitializeTrees();
        StartCoroutine(PostRequest());  // Start the login process to retrieve JWT
        Debug.Log("Current Time: " + DateTime.Now.ToShortTimeString());
        Debug.Log("Current Date: " + DateTime.Now.ToShortDateString());
          // Fetch daily power consumption

    }

    // Placeholder for future tree-related initialization
    private void InitializeTrees()
    {
        // TODO: Implement any necessary initial setup for trees here
    }
    [Serializable]
    public class LoginResponse
    {
        public string token;
    }

    void Update()
    {
        timeSinceLastRequest += Time.deltaTime;

        if (timeSinceLastRequest >= requestInterval)
        {
            timeSinceLastRequest = 0f;
            StartCoroutine(GetCurrentPowerConsumption()); // Periodically fetch current power consumption
        }
      
    }

    // Method to handle user login and JWT retrieval
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
                 string jwt = response;
                //jwtToken = jwt.Trim();
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
        StartCoroutine(GetDailyPowerConsumption());
        StartCoroutine(getdailyConsumptionCurrentMonthEndpoint());
    }


    // Method to fetch daily power consumption data
    public IEnumerator GetDailyPowerConsumption()
    {
        string url = $"{dailyConsumptionEndpoint}?year={year1}&month={month1.ToUpper()}";
        Debug.Log("Complete URL for request: " + url);
        Debug.Log("JWT Token at the time of request: " + jwtToken);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Daily power consumption data: " + jsonResponse);
                Dictionary<string, float> dailyConsumptionInAMonth_1 = ExtractDailyConsumption(jsonResponse);
            }
            else
            {
                Debug.LogError("Daily consumption request failed: " + request.error);
                Debug.LogError("Response code: " + request.responseCode);
            }
        }
    }


    // Method to fetch current power consumption data (needs implementation)
    private IEnumerator GetCurrentPowerConsumption()
    {
        string url = profileEndpoint;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Current power consumption data: " + jsonResponse);
            }
            else
            {
                Debug.LogError("Current consumption request failed: " + request.error);
            }
        }
    }

    public IEnumerator getdailyConsumptionCurrentMonthEndpoint()
    {
        using (UnityWebRequest requestnew2 = UnityWebRequest.Get(dailyConsumptionCurrentMonthEndpoint))
        {
            // Properly formatted authorization header
            requestnew2.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            // Send the request
            yield return requestnew2.SendWebRequest();

            // Check if the request was successful
            if (requestnew2.result == UnityWebRequest.Result.Success)
            {
                string jsonResponseNew = requestnew2.downloadHandler.text;
                Debug.Log("Current daily power consumption data: " + jsonResponseNew);
                Dictionary<string, float> dailyConsumptionInAMonth_2 = ExtractDailyConsumption(jsonResponseNew);
            }
            else
            {
                Debug.LogError("Current daily consumption request failed: " + requestnew2.error);
            }
           
        }
    }
    [Serializable]
    public class DailyConsumptionResponse
    {
        public DailyPowerConsumptionView dailyPowerConsumptionView;
    }

    [Serializable]
    public class DailyPowerConsumptionView
    {
        public int year;
        public int month;
        public Dictionary<string, float> dailyUnits;
    }
    private Dictionary<string, float> ExtractDailyConsumption(string jsonResponse)
    {
        JObject parsedJson = JObject.Parse(jsonResponse);

        // Attempt to access the nested dictionary safely
        var dailyUnits = parsedJson["dailyPowerConsumptionView"]?["dailyUnits"]?.ToObject<Dictionary<string, float>>();
        Debug.Log(dailyUnits);
        if (dailyUnits == null)
        {
            Debug.LogError("Failed to extract daily consumption data: One or more properties are null.");
            return new Dictionary<string, float>(); // Return an empty dictionary to avoid further errors
        }

        // Log the extracted data
        foreach (var day in dailyUnits)
        {
            Debug.Log("Day " + day.Key + ": " + day.Value + " units");
            key_val_test= day.Key;
        }
        Debug.Log("Updated Key Value" + key_val_test);

        return dailyUnits;
    }

}
