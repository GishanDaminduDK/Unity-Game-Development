using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private SpriteRenderer playerSprite; // Reference to the player's sprite renderer
    private bool isPaused = false; // Track whether the game is paused
    public static string dateLastplay;
    public static string timeLastplay;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadPosition();  // Load the position at the start
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
            date = dateLastplay,
            time = timeLastplay
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log("Saving Data: " + json); // Check what is being saved
        PlayerPrefs.SetString("PlayerPosition", json);
        PlayerPrefs.Save();
    }

    private void LoadPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            string json = PlayerPrefs.GetString("PlayerPosition");
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Loaded Data: " + json); // Check what is being loaded
            Vector2 position = new Vector2(data.playerPositionX, data.playerPositionY);
            rb.position = position;
            ItemCollector.coins = data.coinscount; // Make sure to restore the coins count
            DateTime now_time = DateTime.Now;

            // Log the current time to the console
            Debug.Log("Current Time: " + now_time.ToShortTimeString());
            DateTime now_date = DateTime.Now;

            // Log the current date to the console
            Debug.Log("Current Date: " + now_date.ToShortDateString());
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
