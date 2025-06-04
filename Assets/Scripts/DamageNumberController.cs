using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    [SerializeField] private DamageNumber damageNumber;
    [SerializeField] private Queue<DamageNumber> damageNumberPool = new Queue<DamageNumber>();
    private DamageNumber damage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Spawn(float damageAmount, Vector3 location)
    {
        if (damageNumberPool.Count == 0)
        {
            Create();
            int number = Mathf.RoundToInt(damageAmount);
            damage.Display(number);
            damage.transform.position = location;
        }
        else
        {
            damage =  damageNumberPool.Dequeue();
            int number = Mathf.RoundToInt(damageAmount);
            damage.Display(number);
            damage.transform.position = location;
            damage.gameObject.SetActive(true);
        }
    }
    public void Release(DamageNumber number)
    {
        number.gameObject.SetActive(false);
        damageNumberPool.Enqueue(number);

    }
    public void Create()
    {
        damage = Instantiate(damageNumber,transform);
        damage.gameObject.SetActive(true);
        damageNumberPool.Enqueue(damage);
        damageNumberPool.Dequeue();
    }
}
