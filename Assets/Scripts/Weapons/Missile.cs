using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float moveSpeed;
    void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
    
}
