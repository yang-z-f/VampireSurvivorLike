using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int ExpValue;
    private float moveSpeed = 10f;
    private bool isMovingToPlayer;
    public float timeBetweenCheck = .2f;
    void Start()
    {
        InvokeRepeating("PickupCheck", 0, timeBetweenCheck);
    }
    void Update()
    {
        MoveToPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExpLevelController.instance.GetExp(ExpValue);
            Destroy(gameObject);
        }
    }
    private void PickupCheck()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position)< PlayerController.instance.pickupRange)
        {
            isMovingToPlayer = true;
        }
    }
    private void MoveToPlayer()
    {
        if (isMovingToPlayer) 
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
    }
}
