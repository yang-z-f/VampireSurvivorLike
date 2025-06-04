using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUpgradeButton : MonoBehaviour
{
    public Text valueText, costText;
   public void UpdateDisplay(int cost ,float oldValue,float newValue)
    {
        valueText.text = oldValue.ToString("F1") + " => " + newValue.ToString("F1");
        costText.text = cost.ToString();
        if (cost > CoinController.instance.currentCoins)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
    public void MaxLevel()
    {
        valueText.text = "Âú¼¶";
        costText.text = "";
        gameObject.GetComponent<Button>().interactable = false;
    }
}
