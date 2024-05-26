using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darken : MonoBehaviour
{
    public Image blackImage;
    private float alpha;

    private void Start()
    {
        StartCoroutine(ChangeFade());
    }

    IEnumerator ChangeFade()
    {
        while (true)
        {
            alpha = Random.Range(0f, 1f); // Generate a random alpha value between 0 and 1
            blackImage.color = new Color(0, 0, 0, alpha); // Set alpha for the color
            yield return new WaitForSeconds(1f); // Wait for 1 second before changing alpha again
        }
    }
}
