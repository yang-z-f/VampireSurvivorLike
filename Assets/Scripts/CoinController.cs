using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    public CoinPickUp coin;
    public int currentCoins;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void AddCoins(int coins)
    {
        currentCoins += coins;
        UIController.Instance.UpdateCoins();
        SFXManager.instance.Pitch(5);
    }
    public void DropCoin(Vector3 position,int value)
    {
        CoinPickUp newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0), Quaternion.identity);
        newCoin.coinValue = value;
        newCoin.gameObject.SetActive(true);
    }
    public void SpendCoins(int coins)
    {
         currentCoins -=coins;
        UIController.Instance.UpdateCoins();
    }
}
