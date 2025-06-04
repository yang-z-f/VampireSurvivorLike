using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1.2f;
    private Transform target;
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    private float KnockBackTime = 0.5f;
    [SerializeField] private float currentHealth;
    public int coinValue = 1;
    public float coinDropRate = .5f;
    public SpriteRenderer sr;
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform; 
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
        if (!PlayerController.instance.gameObject.activeSelf)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("Damage", 0, 1);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke("Damage");
        }
    }
    public void Damage()
    {
        PlayerController.instance.playerHealth(damage);
    }
    public void Hit(float damage,bool canKnockBack)
    {
        currentHealth -= damage;
        StartCoroutine(HitAnimation());
        if (canKnockBack)
        {
            StartCoroutine(KnockBack()); 
        }
        DamageNumberController.instance.Spawn(damage,transform.position);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SFXManager.instance.Pitch(4);
            ExpLevelController.instance.SpawnExp(transform.position);
            if(Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
        }
        else
        {
            SFXManager.instance.Pitch(0);
        }
    }
    private IEnumerator KnockBack()
    {
        float speed = moveSpeed;
        if (moveSpeed > 0)
        {
            float KnockBackSpeed = moveSpeed * -2;
            moveSpeed = KnockBackSpeed;
            yield return new WaitForSeconds(KnockBackTime);
        }
        moveSpeed = speed;
    }
    public IEnumerator HitAnimation()
    {
        if (sr.color == Color.white)
        {
            sr.color = Color.red;
        }
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
    }
}
