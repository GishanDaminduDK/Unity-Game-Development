using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 1.0f;
    private bool isPaused = false; // Track whether the game is paused
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadPosition();  // Load the position at the start
    }
    private void Update()
    {
        if (!isPaused) // Only process input if the game is not paused
        {
            moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveV = Input.GetAxisRaw("Vertical") * moveSpeed;
        }

    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        { 
        moveH = Input.GetAxis("Horizontal")*moveSpeed;
        moveV = Input.GetAxis("Vertical")*moveSpeed;
        rb.velocity = new Vector2(moveH, moveV);

        Vector2 direction = new Vector2(moveH, moveV);

        FindObjectOfType<Knight_animation>().SetDirection(direction);
        }

    }
    public void SavePosition()
    {
        GameData data = new GameData
        {
            playerPositionX = transform.position.x,
            playerPositionY = transform.position.y,
            coinscount = itemCollector.coins
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
            itemCollector.coins = data.coinscount; // Make sure to restore the coins count
        }
    }

    public void PauseAndSaveGame()
    {
        isPaused = true;  // Set the game as paused
        Debug.Log("Check when pause is true");
        SavePosition();
        Time.timeScale = 0; // Pause the game
    }

    public void ResumeGame()
    {
        isPaused = false;  // Set the game as not paused
        Debug.Log("Check when pause is flase");
        Time.timeScale = 1; // Resume the game
    }





}
