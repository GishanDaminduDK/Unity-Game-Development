using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    
    [SerializeField] private Text coinsCount;
    public static int coins = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        coins = int.Parse(coinsCount.text);
        if (collision.gameObject.CompareTag("coins")) {
            Destroy(collision.gameObject);
            coins++;
            coinsCount.text = ""  + coins;
        }
    }
}
