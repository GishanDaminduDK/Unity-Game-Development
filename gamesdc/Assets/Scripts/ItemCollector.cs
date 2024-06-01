//using EasyUI.popupmessages;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ItemCollector : MonoBehaviour
//{
//    public static int coins = 0; // Initialize static coins variable
//    //[SerializeField] private Text coinsCount; // SerializeField for editor access



//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("coins"))
//        {
//            Destroy(collision.gameObject); // Destroy the coin object
//            coins++; // Increment coins count
//            //UpdateCoinsDisplay(); // Update UI display
//            ShowPopup(); // Show popup message
//        }
//    }

//    //private void UpdateCoinsDisplay()
//    //{
//    //    coinsCount.text = coins.ToString(); // Convert coins count to string and update the text UI
//    //}

//    private void ShowPopup()
//    {
//        PopupMessageUI.Instance
//            .SetTitle("Coin Collected")
//            .SetMessage("You've collected a coin!")
//            .SetReward(false)
//            .Show();
//    }
//}
using System.Collections;
using System.Collections.Generic;
using EasyUI.popupmessages;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static int coins = 0;
    //[SerializeField] private Text coinsCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //coins = int.Parse(coinsCount.text);
        if (collision.gameObject.CompareTag("coins"))
        {
            Destroy(collision.gameObject);
            coins++;
            //coinsCount.text = "" + coins;
            PopupMessageUI.Instance
            .SetTitle("Example Title")
            .SetMessage("This is an example message.").SetReward(false)
            .Show();
        }
    }

}