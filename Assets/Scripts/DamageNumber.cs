using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]private TMP_Text damageText;
    private float damageLifeTime = 1f;
    void Awake()
    {
        damageText = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }
    public void Display(int damage)
    {
        damageText.text = damage.ToString();
        Invoke("Die", damageLifeTime);
    }
    public void Die() { 
        DamageNumberController.instance.Release(this);
    }
}
