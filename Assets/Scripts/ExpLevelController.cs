using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpLevelController : MonoBehaviour
{
    public static ExpLevelController instance;
    public int currentExp;
    [SerializeField] private ExpPickup pickups;
    public List<Weapon> weaponsToUpgrade;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int maxLevel = 10;
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
        while(expLevels.Count <= maxLevel)
        {
            expLevels.Add(expLevels.Count*(expLevels.Count+5)*5);
        }
    }
    void Update()
    {
        
    }
    public void GetExp(int Exp)
    {
        if (currentLevel < maxLevel)
        {
            currentExp += Exp;
        }
        if (currentLevel == maxLevel)
        {
            currentExp += 0;
        }
        UIController.Instance.UpdateExp(currentExp, expLevels[currentLevel], currentLevel);
        SFXManager.instance.Pitch(5);
        if (currentExp > expLevels[currentLevel] && currentLevel < maxLevel)
            LevelUp();
    }
    public void SpawnExp(Vector3 position)
    {
        Instantiate(pickups, position, Quaternion.identity);
    }
    public void LevelUp()
    {
        currentExp -= expLevels[currentLevel];
         currentLevel++;
        SFXManager.instance.Pitch(6);
        UIController.Instance.LevelUpPanel.SetActive(true);
        Time.timeScale = 0;
        weaponsToUpgrade.Clear();
        List<Weapon> avaliableWeapons = new List<Weapon>();
        avaliableWeapons.AddRange(PlayerController.instance.assignedWeapon);
        if (avaliableWeapons.Count > 0)
        {
            int selected = Random.Range(0, avaliableWeapons.Count);
            weaponsToUpgrade.Add(avaliableWeapons[selected]);
            avaliableWeapons.RemoveAt(selected);
        }
        if (PlayerController.instance.assignedWeapon.Count+ PlayerController.instance.maxLevelWeapon.Count< PlayerController.instance.maxWeapon)
        {
            avaliableWeapons.AddRange(PlayerController.instance.unassignedWeapon);
        }
        for (int i = weaponsToUpgrade.Count; i < PlayerController.instance.maxWeapon; i++)
        {
            if (avaliableWeapons.Count > 0)
            {
                int selected = Random.Range(0, avaliableWeapons.Count);
                weaponsToUpgrade.Add(avaliableWeapons[selected]);
                avaliableWeapons.RemoveAt(selected);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            UIController.Instance.buttons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }
        for(int i = 0; i < UIController.Instance.buttons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.Instance.buttons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.buttons[i].gameObject.SetActive(false);
            }
        }
        PlayerStateController.instance.PlayerStatDisplay();
    }
}
