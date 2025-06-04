using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SpinWeapon : Weapon
{
    public Transform holder;
    private float rotateSpeed = 180f;
    public Transform weaponToSpawn;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        holder.rotation = Quaternion.Euler(0, 0, holder.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime * weaponStats[weaponLevel].speed);
        SpawnWeapon();
       
    }
    private void SpawnWeapon()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeBetweenActive;
              for (int i = 0; i < weaponStats[weaponLevel].amount; i++)
            {
                float rot = (360f / weaponStats[weaponLevel].amount)*i;
                Instantiate(weaponToSpawn, weaponToSpawn.position,Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }
       
    }
}
