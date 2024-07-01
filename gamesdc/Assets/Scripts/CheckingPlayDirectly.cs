using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;


public class CheckinPlayDirectly : MonoBehaviour
{
    private string jwt_newone;
    private string id_value_string;
    private string profileEndpoint = "http://20.15.114.131:8080/api/user/profile/view";
    public static string playerLoginJWTToken;
    public static string playerIDvalue;

    public static string firstname_var;
    public static string lastname_var;
    public static string username_var;
    public static string nic_var;
    public static string phonenumber_var;
    public static string email_var;
    public static string profilePictureUrl_var;
    public static string all_fields_condition_check = "True";
    [SerializeField] Text resultText;

    public void get_profile_details()
    {
        StartCoroutine(GetProfile());
        //string url = "http://localhost:8081/api/playerstatus/updateStatus/" + id_value_string;
        //StartCoroutine(SendPlayerStatusUpdateRequest(url, jwt_newone));
    }
    /*public void updatePlayerStatus()
    {
        string url = "http://localhost:8081/api/playerstatus/updateStatus/" + id_value_string;
        Debug.Log(jwt_newone);
        StartCoroutine(SendPlayerStatusUpdateRequest(url, jwt_newone));
    }*/

    public IEnumerator GetProfile()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(profileEndpoint))
        {
            request.SetRequestHeader("Authorization", "Bearer " + postmethod.jwtToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                JObject jsonResponse = JObject.Parse(response);

                // Extracting data from JSON response
                firstname_var = (string)jsonResponse["user"]["firstname"];
                lastname_var = (string)jsonResponse["user"]["lastname"];
                username_var = (string)jsonResponse["user"]["username"];
                nic_var = (string)jsonResponse["user"]["nic"];
                phonenumber_var = (string)jsonResponse["user"]["phoneNumber"];
                email_var = (string)jsonResponse["user"]["email"];
                profilePictureUrl_var = (string)jsonResponse["user"]["profilePictureUrl"];

                // Check if any field is empty
                bool anyFieldEmpty = string.IsNullOrEmpty(firstname_var) || string.IsNullOrEmpty(lastname_var) || string.IsNullOrEmpty(username_var)
                    || string.IsNullOrEmpty(nic_var) || string.IsNullOrEmpty(phonenumber_var) || string.IsNullOrEmpty(email_var)
                    || string.IsNullOrEmpty(profilePictureUrl_var);

                // Print whether any field is empty or not
                Debug.Log("Any field empty: " + !anyFieldEmpty);
                if (!anyFieldEmpty)
                {
                    all_fields_condition_check = "True";
                    Debug.Log(all_fields_condition_check);
                    resultText.text = "Can't go forward" + "\n" + "Please fill in all required information";
                }
                else
                {
                    all_fields_condition_check = "False";
                    Debug.Log(all_fields_condition_check);
                    StartCoroutine(SendLoginRequest());

                }



            }
            else if (request.responseCode == 401)
            {
                Debug.LogError("Failed to get profile data: Unauthorized (401). Check the JWT token validity and permissions.");
            }
            else
            {
                Debug.LogError("Failed to get profile data: " + request.error);
            }
        }
    }

    IEnumerator SendLoginRequest()
    {
        string username = username_var;
        string password = postmethod.password;

        var data = new
        {
            username = username,
            password = password
        };

        string jsonData = JsonConvert.SerializeObject(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm("http://localhost:8081/api/v1/player/log", jsonData))
        {
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string response = request.downloadHandler.text;
                JObject jsonResponse = JObject.Parse(response);
                jwt_newone = (string)jsonResponse["token"];
                playerLoginJWTToken = jwt_newone;
                int id_value = (int)jsonResponse["id"];
                id_value_string = id_value.ToString();
                playerIDvalue = id_value_string;
                StartCheckingQAdone(id_value_string, jwt_newone);
                //updatePlayerStatus();
            }
        }
    }

    void StartCheckingQAdone(string id_value_string, string jwt_newone)
    {
        string url = "http://localhost:8081/api/v1/player/answer/" + id_value_string;
        StartCoroutine(SendRequest(url, jwt_newone));
    }


    IEnumerator SendRequest(string url, string jwt_newone)
    {
        using (UnityWebRequest requestforcheckingQAdone = UnityWebRequest.Get(url))
        {
            requestforcheckingQAdone.SetRequestHeader("Authorization", "Bearer " + jwt_newone);

            yield return requestforcheckingQAdone.SendWebRequest();

            if (requestforcheckingQAdone.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(requestforcheckingQAdone.error);
                yield break;
            }

            string jsonResponse = requestforcheckingQAdone.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);

            try
            {
                JObject responseJson = JObject.Parse(jsonResponse);

                if (responseJson.TryGetValue("content", out JToken contentToken) &&
                    responseJson.TryGetValue("code", out JToken codeToken))
                {
                    if (contentToken.Type == JTokenType.Null && codeToken.ToString() == "01")
                    {
                        string reviewUrl_start = "http://localhost:5173/main/" + id_value_string + "/" + jwt_newone;
                        Debug.Log(reviewUrl_start);
                        Application.OpenURL(reviewUrl_start);
                        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
                    }
                    else if (contentToken.Type != JTokenType.Null && codeToken.ToString() == "00")
                    {
                        string reviewUrl_start = "http://localhost:5173/main/quiz/review/" + id_value_string + "/" + jwt_newone;
                        Debug.Log(reviewUrl_start);
                        //Application.OpenURL(reviewUrl_start);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
                    }
                    else
                    {
                        Debug.Log("Content is null but code is not '01'");
                    }
                }
                else
                {
                    Debug.Log("No 'content' or 'code' field found in the response.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing JSON response: " + e.Message);
            }
        }
    }

    //public IEnumerator SendPlayerStatusUpdateRequest(string url, string jwt_newone)
    //{
    //    int id_value = 1;
    //    int totalCoins_Value = 90;
    //    int gemsValue = 10;

    //    // Define the individual game data variables
    //    double playerPositionX = 12.554312705993653;
    //    double playerPositionY = 1.9378679990768433;
    //    int coinscount = 12;
    //    string time = "9:55 AM";
    //    string date = "6/1/2024";

    //    // Construct game data object using variables
    //    var gameData = new
    //    {
    //        playerPositionX = playerPositionX,
    //        playerPositionY = playerPositionY,
    //        coinscount = coinscount,
    //        time = time,
    //        date = date
    //    };

    //    string jsonGameData = JsonConvert.SerializeObject(gameData);

    //    // Construct the outer JSON object
    //    var outerObject = new
    //    {
    //        id = id_value,
    //        coins = totalCoins_Value,
    //        gems = gemsValue,
    //        resources = new string[] { jsonGameData }  // Array containing the serialized game data
    //    };

    //    // Serialize the outer object to a JSON string
    //    string jsonProfileUpdate = JsonConvert.SerializeObject(outerObject);

    //    // Convert JSON string to byte array
    //    byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonProfileUpdate);
    //    using (UnityWebRequest requestForUpdatingStatus = UnityWebRequest.Put(url, data))
    //    {
    //        requestForUpdatingStatus.SetRequestHeader("Content-Type", "application/json");
    //        requestForUpdatingStatus.SetRequestHeader("Authorization", "Bearer " + jwt_newone);
    //        yield return requestForUpdatingStatus.SendWebRequest();

    //        if (requestForUpdatingStatus.result == UnityWebRequest.Result.Success)
    //        {
    //            if (requestForUpdatingStatus.responseCode == 200)
    //            {
    //                Debug.Log("Profile updated successfully.");
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("Error updating profile: " + requestForUpdatingStatus.error);
    //        }
    //    }
    //}



    public void ReviewCheck()
    {

        get_profile_details();
        Debug.Log(all_fields_condition_check);

    }

}