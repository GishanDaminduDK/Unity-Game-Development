using UnityEngine;

public class ScriptB : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the onDataReady event
        GameManager.Instance.onDataReady += HandleDataReady;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        GameManager.Instance.onDataReady -= HandleDataReady;
    }

    private void HandleDataReady()
    {
        // Access the shared value when it's ready
        int value = GameManager.Instance.SharedValue;
        Debug.Log("Received value in ScriptB: " + value);

        // Perform further initializations that depend on the received value
    }
}
