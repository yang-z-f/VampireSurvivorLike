using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class HaloWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        SpawnWeapon();
    }
    protected override void SetStats()
    {
        base.SetStats();
        damager.timeBetweenDamage = weaponStats[weaponLevel].speed;
    }
    private void SpawnWeapon()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeBetweenActive;
            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }

    }
}
