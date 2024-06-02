
using System.Collections;
using System.Collections.Generic;
using EasyUI.popupmessages;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static int coins;
    public static int gems;

    void Update()
    {
        if (PlayerMovements.condition_check_value==1)
        {
            coins = PlayerMovements.initial_coins_value;
            gems = PlayerMovements.initial_gems_value;
            //gems= 100;
            //Debug.Log("Set player games values"+PlayerMovements.initial_gems_value);
        }
        else {
            coins = PlayerMovements.initial_coins_value;
            //gems = PlayerMovements.initial_gems_value;
            gems = PlayerMovements.initial_gems_value; ;
            //Debug.Log("Set player games values" + PlayerMovements.initial_gems_value);


        }

    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //coins = int.Parse(coinsCount.text);
        if (collision.gameObject.CompareTag("coins"))
        {
            Destroy(collision.gameObject);
            PlayerMovements.initial_coins_value++;
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
            PlayerMovements.initial_gems_value++;
            //gems++;

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
