using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damage;
    public Vector3 targetSize;
    private float growSpeed = 3f;
    public float weaponDuration;
    public bool destroyParent;
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageTimer;
    public bool destroyOnImpact;
    [SerializeField] private List<EnemyController> enemyInRange = new List<EnemyController>();
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        if (UIController.Instance.LevelUpPanel.activeSelf)
        {
            Destroy();
        }
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        Die();
        DamageOverTime();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemyInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().Hit(damage,true);
                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime)
        {
            enemyInRange.Remove(collision.GetComponent<EnemyController>());
        }
    }
    private void Die()
    {
        weaponDuration -= Time.deltaTime;
        if (weaponDuration <= 0)
        {
            targetSize = Vector3.zero;
            if(transform.localScale.x == 0)
            {
                Destroy();
            }
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
        if (destroyParent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    private void DamageOverTime()
    {
        if (damageOverTime)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                damageTimer = timeBetweenDamage;
                for (int i = 0; i < enemyInRange.Count; i++)
                {
                    if (enemyInRange[i] != null)
                    {
                        enemyInRange[i].Hit(damage, false);
                    }
                    else
                    {
                        enemyInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
