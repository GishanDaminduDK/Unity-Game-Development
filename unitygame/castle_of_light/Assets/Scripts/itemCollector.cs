using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private Text coinsCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coins")) {
            Destroy(collision.gameObject);
            coins++;
            coinsCount.text = ""  + coins;
        }
    }
}
