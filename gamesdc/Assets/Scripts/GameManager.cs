using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Event to notify when the data is ready
    public event Action onDataReady;

    public int SharedValue { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSharedValue(int value)
    {
        SharedValue = value;
        onDataReady?.Invoke(); // Trigger the event
    }
}
