using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private SpriteRenderer playerSprite; // Reference to the player's sprite renderer
    private bool isPaused = false; // Track whether the game is paused
    public static string dateLastplay;
    public static string timeLastplay;
    public static int coinscount;
    [SerializeField] private Text coinsCount; // SerializeField for editor access
    [SerializeField] private Text gemsCount; // SerializeField for editor access
    public Button pauseButton;
    public Button resumeButton;
    public static int initial_gems_value;
    public static int initial_coins_value;
    public static int condition_check_value;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadPosition();  // Load the position at the start
        Debug.Log("The spring token is token is" + CheckinPlayDirectly.playerLoginJWTToken);
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);

        // Add listeners to the buttons
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
    }
    private void OnPauseButtonClick()
    {
        // Execute the pause functionality
        PauseAndSaveGame();

        // Hide the pause button and show the resume button
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
    }

    private void OnResumeButtonClick()
    {
        // Execute the resume functionality
        ResumeGame();

        // Hide the resume button and show the pause button
        resumeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!isPaused)
        {
            moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveV = Input.GetAxisRaw("Vertical") * moveSpeed;

            // Debugging output
            // Debug.Log("Horizontal Movement: " + moveH);

            // Flip the sprite based on horizontal movement
            if (moveH < 0)
            {
                playerSprite.flipX = true;
                //Debug.Log("Flipping sprite to the left.");
            }
            else if (moveH > 0)
            {
                playerSprite.flipX = false;
                //Debug.Log("Flipping sprite to the right.");
            }
        }
        coinsCount.text = ItemCollector.coins.ToString();
        gemsCount.text=ItemCollector.gems.ToString();
    }


    private void FixedUpdate()
    {
        if (!isPaused) // Only apply movement if the game is not paused
        {
            rb.velocity = new Vector2(moveH, moveV);
        }
    }

    public void SavePosition()
    {
        GameData data = new GameData
        {
            playerPositionX = transform.position.x,
            playerPositionY = transform.position.y,
            coinscount = ItemCollector.coins,
            gemscount = ItemCollector.gems,
            date = dateLastplay,
            time = timeLastplay
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log("Saving Data: " + json); // Check what is being saved
        PlayerPrefs.SetString("PlayerPosition", json);
        PlayerPrefs.Save();
        updatePlayerStatus();



    }
    public void updatePlayerStatus()
    {
        string url = "http://localhost:8081/api/playerstatus/updateStatus/" + CheckinPlayDirectly.playerIDvalue;

        StartCoroutine(SendPlayerStatusUpdateRequest(url, CheckinPlayDirectly.playerLoginJWTToken));
    }
    public IEnumerator SendPlayerStatusUpdateRequest(string url, string jwt_newone)
    {
        string id_value = CheckinPlayDirectly.playerIDvalue;
        int totalCoins_Value = 90;
        int gemsValue = 10;

        // Define the individual game data variables
        double playerPosition_X = transform.position.x;
        double playerPosition_Y = transform.position.y;
        int coinscount_val = ItemCollector.coins;
        int gemscount_val= ItemCollector.gems;
        string time_json = timeLastplay;
        string date_json = dateLastplay;

        // Construct game data object using variables
        var gameData = new
        {
            playerPositionX = playerPosition_X,
            playerPositionY = playerPosition_Y,
            coinscount = coinscount_val,
            gemscount = gemscount_val,
            time = time_json,
            date = date_json
        };

        string jsonGameData = JsonConvert.SerializeObject(gameData);

        // Construct the outer JSON object
        var outerObject = new
        {
            id = id_value,
            coins = totalCoins_Value,
            gems = gemsValue,
            resources = new string[] { jsonGameData }  // Array containing the serialized game data
        };

        // Serialize the outer object to a JSON string
        string jsonProfileUpdate = JsonConvert.SerializeObject(outerObject);

        // Convert JSON string to byte array
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonProfileUpdate);
        using (UnityWebRequest requestForUpdatingStatus = UnityWebRequest.Put(url, data))
        {
            requestForUpdatingStatus.SetRequestHeader("Content-Type", "application/json");
            requestForUpdatingStatus.SetRequestHeader("Authorization", "Bearer " + jwt_newone);
            yield return requestForUpdatingStatus.SendWebRequest();

            if (requestForUpdatingStatus.result == UnityWebRequest.Result.Success)
            {
                if (requestForUpdatingStatus.responseCode == 200)
                {
                    Debug.Log("Profile updated successfully.");
                }
            }
            else
            {
                Debug.Log("Error updating profile: " + requestForUpdatingStatus.error);
            }
        }
    }

    private void LoadPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            
            DateTime now_time = DateTime.Now;


            // Log the current time to the console
            Debug.Log("Current Time: " + now_time.ToShortTimeString());
            DateTime now_date = DateTime.Now;

            // Log the current date to the console
            Debug.Log("Current Date: " + now_date.ToShortDateString());
            Debug.Log("Test Loading Condition");
            StartGettingSavedData(CheckinPlayDirectly.playerIDvalue, CheckinPlayDirectly.playerLoginJWTToken);
        }
        
    }

    void StartGettingSavedData(string id_value_string, string jwt_newone)
    {
        string url = "http://localhost:8081/api/v1/player/answer/" + id_value_string;
        StartCoroutine(SendPlayerStatusGettingRequest(url, jwt_newone));
    }


    //IEnumerator SendPlayerStatusGettingRequest(string url, string jwt_newone)
    //{
    //    using (UnityWebRequest requestforcheckingQAdone = UnityWebRequest.Get(url))
    //    {
    //        requestforcheckingQAdone.SetRequestHeader("Authorization", "Bearer " + jwt_newone);

    //        yield return requestforcheckingQAdone.SendWebRequest();

    //        if (requestforcheckingQAdone.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.LogError(requestforcheckingQAdone.error);
    //            yield break;
    //        }

    //        string jsonResponse = requestforcheckingQAdone.downloadHandler.text;
    //        Debug.Log("Response: " + jsonResponse);

    //        try
    //        {
    //            JObject responseJson = JObject.Parse(jsonResponse);

    //            if (responseJson.TryGetValue("content", out JToken contentToken) &&
    //                responseJson.TryGetValue("code", out JToken codeToken))
    //            {
    //                if (contentToken.Type == JTokenType.Null && codeToken.ToString() == "01")
    //                {

    //                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    //                }
    //                else if (contentToken.Type != JTokenType.Null && codeToken.ToString() == "00")
    //                {

    //                }
    //                else
    //                {
    //                    Debug.Log("Content is null but code is not '01'");
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log("No 'content' or 'code' field found in the response.");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogError("Error parsing JSON response: " + e.Message);
    //        }
    //    }
    //}
    public IEnumerator SendPlayerStatusGettingRequest(string url, string jwt_newone)
    {
        using (UnityWebRequest requestForCheckingQADone = UnityWebRequest.Get(url))
        {
            requestForCheckingQADone.SetRequestHeader("Authorization", "Bearer " + jwt_newone);

            yield return requestForCheckingQADone.SendWebRequest();

            if (requestForCheckingQADone.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(requestForCheckingQADone.error);
                yield break;
            }

            string jsonResponse = requestForCheckingQADone.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            try
            {
                JObject responseJson = JObject.Parse(jsonResponse);
                //Debug.Log("This is all json object parse" + responseJson);
                int totalCoins = (int)responseJson["content"]["totalCoins"];
                //Debug.Log("Total Coins: " + totalCoins);
                initial_gems_value = totalCoins;
                //Debug.Log("after updated value of gems"+ initial_gems_value);

                // Correcting the path to access resources within playerStatus
                if (responseJson["playerStatus"] ==null) {// I want to player Status was null check valued assigned as 1
                    condition_check_value = 1;
                    gemsCount.text=totalCoins.ToString();
                    initial_coins_value = 100;
                    coinsCount.text = "100";
                    
                }
                else
                {
                   condition_check_value = 0;
                }
                if (responseJson["content"]?["playerStatus"]?["resources"] != null)
                {
                    JArray resourcesArray = (JArray)responseJson["content"]["playerStatus"]["resources"];
                    if (resourcesArray.Count > 0)
                    {
                        string resourcesString = resourcesArray[0].ToString();  // Assuming there's at least one item in the array
                        JObject resourceObject = JObject.Parse(resourcesString);
                        

                        // Extracting specific values from the 'resourceObject'
                        double playerPositionX = (double)resourceObject["playerPositionX"];
                        double playerPositionY = (double)resourceObject["playerPositionY"];
                        float playerPositionX_floatvalue = Convert.ToSingle(playerPositionX);
                        float playerPositionY_floatvalue = Convert.ToSingle(playerPositionY);
                        int coinscount = (int)resourceObject["coinscount"];
                        int gemscount = (int)resourceObject["gemscount"];
                        string time = (string)resourceObject["time"];
                        string date = (string)resourceObject["date"];

                        // Debugging the values
                        Debug.Log($"Extracted Values:\nPlayer Position X: {playerPositionX}\nPlayer Position Y: {playerPositionY}\nCoins Count: {coinscount}\nTime: {time}\nDate: {date}");
                        string json = PlayerPrefs.GetString("PlayerPosition");
                        GameData data = JsonUtility.FromJson<GameData>(json);
                        Debug.Log("Loaded Data: " + json); // Check what is being loaded
                        Vector2 position = new Vector2(playerPositionX_floatvalue, playerPositionY_floatvalue);
                        rb.position = position;

                        //ItemCollector.coins = data.coinscount; // Make sure to restore the coins count
                        ItemCollector.coins = coinscount; // Make sure to restore the coins count
                        ItemCollector.gems = gemscount;
                        gemsCount.text = ItemCollector.gems.ToString();
                        coinsCount.text = ItemCollector.coins.ToString();
                        initial_gems_value = gemscount;
                        initial_coins_value= coinscount;

                        DateTime now_time = DateTime.Now;
                        DateTime now_date = DateTime.Now;


                        // Log the current time to the console
                        //Debug.Log("Current Time: " + now_time.ToShortTimeString());

                        // Log the current date to the console
                        //Debug.Log("Current Date: " + now_date.ToShortDateString());

                    }
                    else
                    {
                        Debug.Log("No resources found in the response.");
                    }
                }
                else
                {
                    Debug.Log("No 'resources' field found in the response or 'playerStatus' is null.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing JSON response: " + e.Message);
            }
        }
    }
    public void PauseAndSaveGame()
    {
        isPaused = true;  // Set the game as paused
        Debug.Log("Check when pause is true");


        DateTime now_time = DateTime.Now;

        // Log the current time to the console
        Debug.Log("Current Time: " + now_time.ToShortTimeString());
        DateTime now_date = DateTime.Now;
        timeLastplay = now_time.ToShortTimeString();

        // Log the current date to the console
        Debug.Log("Current Date: " + now_date.ToShortDateString());
        dateLastplay = now_date.ToShortDateString();
        SavePosition();
        Time.timeScale = 0; // Pause the game
    }

    public void ResumeGame()
    {
        Debug.Log("Check when pause is flase");
        isPaused = false;  // Set the game as not paused
        Debug.Log("Check when pause is flase");
        Time.timeScale = 1; // Resume the game
    }
}
