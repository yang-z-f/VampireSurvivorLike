using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.WSA;

public class CloseAttackWeapon : Weapon
{
    private float inputX;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        WeaponSpawn();
    }
    protected override void SetStats()
    {
        base.SetStats();  
    }
    private void WeaponSpawn()
    {
        timer -= Time.deltaTime;
        inputX = Input.GetAxisRaw("Horizontal");
        if (timer <= 0)
        {
            timer = timeBetweenActive;
            if (inputX != 0)
            {
                if (inputX < 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
            for (int i = 1; i < weaponStats[weaponLevel].amount; i++)
            {
                float rot = (360f / weaponStats[weaponLevel].amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f,damager.transform.rotation.eulerAngles.z+ rot), transform).gameObject.SetActive(true);
            }
        }
    }
}
