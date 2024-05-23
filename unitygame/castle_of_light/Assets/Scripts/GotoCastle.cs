using UnityEngine;
using UnityEngine.SceneManagement; // Import this to access SceneManager

public class GotoCastle : MonoBehaviour
{
    // Name of the scene to load
    private string sceneToLoad = "Castle";



    // Alternatively, you can use OnTriggerEnter2D if you are using trigger colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the trigger object has a specific tag, e.g., "Building"
        if (collision.gameObject.tag == "Castle")
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
