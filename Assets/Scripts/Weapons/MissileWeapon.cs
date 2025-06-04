using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon
{
    public Missile missile;
    public  float weaponRange;
    public LayerMask whatIsEnemy;
    [SerializeField] private Collider2D[] enemies;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        WeaponSpawn();
    }
    protected override void SetStats()
    {
        base.SetStats();
        missile.moveSpeed = weaponStats[weaponLevel].speed;
        timer = 0;
    }
    private void WeaponSpawn()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeBetweenActive;
            enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * weaponStats[weaponLevel].range, whatIsEnemy);
            if (enemies.Length > 0)
            {
                for (int i = 0; i < weaponStats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    missile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Instantiate(missile, missile.transform.position, missile.transform.rotation).gameObject.SetActive(true);
                }
            }
        }
    }
}
