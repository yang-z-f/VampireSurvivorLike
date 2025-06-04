using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.WSA;

public class EnemyBulletsSpawner : MonoBehaviour
{
    [SerializeField] private BossBullets bullet;
    [SerializeField] private Transform holder;
    [SerializeField] private float spawnDuration = 1.8f;
    [SerializeField] private float angleOffset = 10f;
    [SerializeField] private int bulletCount = 4;
    [SerializeField] private Queue<BossBullets> bossBulletsPool = new Queue<BossBullets>();
    private BossBullets bossBullets;
    void Update()
    {
        spawnDuration -= Time.deltaTime;
        if (spawnDuration <= 0)
        {
            FireCircle();
            spawnDuration = 1.8f;
            angleOffset += 10f;
        }
    }
    public void FireCircle()
    {
        float angle = 360 / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            Spawn();
            bossBullets.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * angle + angleOffset));
        }
    }
    private void OnDisable()
    {
        TimerController.instance.EndTimer();
    }
    public void Spawn()
    { 
        if (bossBulletsPool.Count == 0)
        {
            Create();
            bossBullets.transform.position = transform.position;
        }
        else
        {
            bossBullets = bossBulletsPool.Dequeue();
            bossBullets.transform.position = transform.position;
            bossBullets.gameObject.SetActive(true);
        }
    }
    public void Release(BossBullets bossBullets)
    {
        bossBullets.gameObject.SetActive(false);
        bossBulletsPool.Enqueue(bossBullets);

    }
    public void Create()
    {
        bossBullets = Instantiate(bullet,holder);
        bossBullets.gameObject.SetActive(true);
        bossBulletsPool.Enqueue(bossBullets);
        bossBulletsPool.Dequeue();
    }
}
