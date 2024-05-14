using UnityEngine;
using UnityEngine.UI;

public class PanelTransparencyController : MonoBehaviour
{
    public float transparentAlpha = 0.5f; // Set your desired transparency value here
    public GameObject panel;

    void Start()
    {
        //panel.SetActive(false);
    }

    void Update()
    {

        CheckChildren();
    }

    void CheckChildren()
    {
        Debug.Log("children count"+transform.childCount);
        if (transform.childCount > 0)
        {
            // Panel has children, decrease transparency
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false );
        }
    }
}
