
using System.Collections;
using System.Collections.Generic;
using EasyUI.popupmessages;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static int coins = 10;
    //[SerializeField] private Text coinsCount;
    public static int gems = 5;
    //[SerializeField] private Text gemsCount;
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
        //gems = int.Parse(gemsCount.text);
        if (collision.gameObject.CompareTag("gems"))
        {
            Destroy(collision.gameObject);
            gems++;
            //gemsCount.text = "" + gems;

        }
    }

}



/*using System.Collections;
using System.Collections.Generic;
using EasyUI.popupmessages;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static int coins = 0;
    [SerializeField] private Text coinsCount;
    public static int gems = 0;
    [SerializeField] private Text gemsCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        coins = int.Parse(coinsCount.text);
        if (collision.gameObject.CompareTag("coins"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinsCount.text = "" + coins;

        }

        gems = int.Parse(gemsCount.text);
        if (collision.gameObject.CompareTag("gems"))
        {
            Destroy(collision.gameObject);
            gems++;
            gemsCount.text = "" + gems;

        }
    }

}*/
