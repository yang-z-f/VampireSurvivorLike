using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> weaponStats = new List<WeaponStats>();
    public int weaponLevel = 0;
    public bool isLevelUp;
    public Sprite icon;
    public EnemyDamager damager;
    protected float timeBetweenActive;
    protected float timer;
    protected virtual void Start()
    {
        SetStats();
    }
    protected virtual void Update()
    {
        if (isLevelUp)
        {
            SetStats();
            isLevelUp = false;
        }
    }
    public void LevelUp()
    {
        if (weaponLevel < weaponStats.Count-1)
        {
            weaponLevel++;
            isLevelUp = true;
        }
       if(weaponLevel >= weaponStats.Count - 1)
        {
            PlayerController.instance.maxLevelWeapon.Add(this);
            PlayerController.instance.assignedWeapon.Remove(this);
        }
    }
    protected virtual void SetStats()
    {
        damager.damage = weaponStats[weaponLevel].damage;
        transform.localScale = Vector3.one * weaponStats[weaponLevel].range;
        damager.weaponDuration = weaponStats[weaponLevel].duration;
        timeBetweenActive = weaponStats[weaponLevel].timeBetweenAttacks;
        timer = 0;
    }
}
[System.Serializable]
public class WeaponStats
{
    public float speed;
    public float damage;
    public float range;
    public float timeBetweenAttacks;
    public float amount;
    public float duration;
    public string updateText;
}