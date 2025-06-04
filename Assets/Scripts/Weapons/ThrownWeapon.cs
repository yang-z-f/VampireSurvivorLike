using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : Weapon
{
    protected override void SetStats()
    {
        base.SetStats();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        SpawnWeapon();
    }
     private void SpawnWeapon()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timeBetweenActive;
            for(int i = 0; i < weaponStats[weaponLevel].amount; i++)
            {
                Instantiate(damager,damager.transform.position,damager.transform.rotation).gameObject.SetActive(true);
            }
        }
    }
}
