using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed = 2;
    public float pickupRange = 1f;
    private Animator animator;
    public float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private Slider healthSlider;
    public List<Weapon> unassignedWeapon, assignedWeapon,maxLevelWeapon;
    private SpriteRenderer sr;
    public float maxWeapon = 3;
    public float inputX;
    public float inputY;
    public float moveScale;
    public Vector3 move;
    public Vector2 lookDirection;
    public GameObject HitEffect;
    public GameObject DeathEffect;

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
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        healthSlider = GetComponentInChildren<Slider>();
        sr = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        moveSpeed = PlayerStateController.instance.moveSpeed[0].value;
        pickupRange = PlayerStateController.instance.pickUpRange[0].value;
        maxHealth = PlayerStateController.instance.health[0].value;
        maxWeapon = PlayerStateController.instance.maxWeapons[0].value;
    }
    void Update()
    {
        playerMove();
    }
    private void playerMove()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        move = new Vector3(inputX, inputY,0);
        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        move.Normalize();
        moveScale = move.magnitude;  
        animator.SetFloat("inputX", lookDirection.x);
        animator.SetFloat("inputY", lookDirection.y);
        animator.SetFloat("moveValue", moveScale);   
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    public void playerHealth(float damage)
    {
        currentHealth -= damage;
        Instantiate(HitEffect, transform.position - new Vector3(0,0.1f,0), transform.rotation, transform);   
        healthSlider.value = currentHealth;
        //StartCoroutine(HitAnimation());
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SFXManager.instance.Pitch(3);
            Instantiate(DeathEffect, transform.position, transform.rotation);
            TimerController.instance.EndTimer();
        }
        else
        {
            SFXManager.instance.Pitch(1);
        }
    }
    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapon.Count)
        {
            assignedWeapon.Add(unassignedWeapon[weaponNumber]);
            unassignedWeapon[weaponNumber].gameObject.SetActive(true);
            unassignedWeapon.RemoveAt(weaponNumber);
        }
    }
    public void AddWeapon(Weapon weaponForAdd)
    {
        assignedWeapon.Add(weaponForAdd);
        weaponForAdd.gameObject.SetActive(true);
        unassignedWeapon.Remove(weaponForAdd);
    }
    public IEnumerator HitAnimation()
    {
        if(sr.color == Color.white)
        {
            sr.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.red;
    }
}