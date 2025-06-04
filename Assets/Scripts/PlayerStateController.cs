using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController instance;
    public List<PlayerStateValue> moveSpeed, health, pickUpRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickUpRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickUpRangeLevel,maxWeaponLevel;
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
    void Start()
    {
        for(int i =moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStateValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStateValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = pickUpRange.Count - 1; i < pickUpRangeLevelCount; i++)
        {
            pickUpRange.Add(new PlayerStateValue(pickUpRange[i].cost + pickUpRange[1].cost, pickUpRange[i].value + (pickUpRange[1].value - pickUpRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.Instance.LevelUpPanel.activeSelf)
        {
            PlayerStatDisplay();
        }
    }
    public void PlayerStatDisplay()
    {
        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.Instance.moveSpeedButton.UpdateDisplay(moveSpeed[moveSpeedLevel+1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel+1].value);
        }
       
        else
        {
            UIController.Instance.moveSpeedButton.MaxLevel();
        }
        if(healthLevel<health.Count - 1)
        {
            UIController.Instance.healthButton.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.Instance.healthButton.MaxLevel();
        }
        if (pickUpRangeLevel< pickUpRange.Count - 1)
        {
            UIController.Instance.pickUpRangeButton.UpdateDisplay(pickUpRange[pickUpRangeLevel + 1].cost, pickUpRange[pickUpRangeLevel].value, pickUpRange[pickUpRangeLevel + 1].value);
        }
        else
        {
            UIController.Instance.pickUpRangeButton.MaxLevel();
        }
        if (maxWeaponLevel < maxWeapons.Count - 1)
        {
            UIController.Instance.maxWeaponButton.UpdateDisplay(maxWeapons[maxWeaponLevel + 1].cost, maxWeapons[maxWeaponLevel].value, maxWeapons[maxWeaponLevel + 1].value);
        }
        else
        {
            UIController.Instance.maxWeaponButton.MaxLevel();
        }
        
    }
    public void BuyMoveSpeed()
    {
        moveSpeedLevel++;
        SFXManager.instance.Pitch(7);
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        PlayerStatDisplay();
        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }
    public void BuyHealth()
    {
        healthLevel++;
        SFXManager.instance.Pitch(7);
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        PlayerStatDisplay();
        PlayerController.instance.maxHealth = health[healthLevel].value;
        PlayerController.instance.currentHealth += health[healthLevel].value - health[healthLevel-1].value;
    }
    public void BuyPickUpRange()
    {
        pickUpRangeLevel++;
        SFXManager.instance.Pitch(7);
        CoinController.instance.SpendCoins(pickUpRange[pickUpRangeLevel].cost);
        PlayerStatDisplay();
        PlayerController.instance.pickupRange = pickUpRange[pickUpRangeLevel].value;
    }
    public void BuyMaxWeapon()
    {
        maxWeaponLevel++;
        SFXManager.instance.Pitch(7);
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponLevel].cost);
        PlayerStatDisplay();
        PlayerController.instance.maxWeapon = maxWeapons[maxWeaponLevel].value;
    }
}
[System.Serializable]
public class PlayerStateValue
{
    public int cost;
    public float value;
    public PlayerStateValue(int cost, float value)
    {
        this.cost = cost;
        this.value = value;
    }
}