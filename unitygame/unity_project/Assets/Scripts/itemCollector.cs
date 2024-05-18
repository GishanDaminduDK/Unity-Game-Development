using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    
    [SerializeField] private Text coinsCount;
    int coin = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        coin = int.Parse(coinsCount.text);
        if (collision.gameObject.CompareTag("coins")) {
            Destroy(collision.gameObject);
            coin++;
            coinsCount.text = ""  + coin;
        }
    }
}
