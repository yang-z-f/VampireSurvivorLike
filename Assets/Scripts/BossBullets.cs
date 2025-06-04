using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : MonoBehaviour
{
    private float aliveTime = 5f;
    void Update()
    {
        transform.position += transform.right * Time.deltaTime;
    }
    private void OnEnable()
    {
        Invoke("Die", aliveTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
         GetComponentInParent<EnemyController>().Damage();
         GetComponentInParent<EnemyBulletsSpawner>().Release(this);
        }
    }
    public void Die()
    {
        GetComponentInParent<EnemyBulletsSpawner>().Release(this);
    }
}
