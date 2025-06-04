using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
     private float throwPower = 5;
    private float rotateSpeed = 1;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed * 360f * Time.deltaTime*Mathf.Sign(rb.velocity.x));
    }
}
