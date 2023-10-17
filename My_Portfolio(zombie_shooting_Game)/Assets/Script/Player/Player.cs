using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // player status
    public float exp;
    public int ammo;
    public float health;
    public float breath;
    public int level = 1;
    private float speed = 5.0f;
    private float reloadSpeed = 1.0f;
    private float damage = 10.0f;
    private float spread = 10.0f;

    // current state
    private bool reloading = false;

    // max value
    public int maxAmmo = 15;
    public float maxHP = 100f;
    public float maxExp = 10f;
    public float maxBreath = 100f;
    
    // value limit
    public float limitSpeed = 10.0f;

    // mouse movement
    private float mouseX;

    // fixed value
    private float gravity = 100.0f;

    // parameters
    private Vector3 direction;

    // references
    private CharacterController characterController;

    private void Awake()
    {
        health = maxHP;
        ammo = maxAmmo;
        breath = maxBreath;
        exp = 0f;
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        moveMent();
        moveMouse();

        if (Input.GetMouseButton(0)) bulletFire();
        if (Input.GetKeyDown(KeyCode.R)) reload();
        if (Input.GetKeyDown(KeyCode.Space)) expTest();
        levelUp();
    }

    private void moveMent()
    {
        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && breath > 0) 
        {
            moveSpeed = speed * 1.75f;
            breath -= 0.1f;
        }
        else
        {
            moveSpeed = speed;
            if(breath < maxBreath && !Input.GetKey(KeyCode.LeftShift))
                breath += 0.5f;
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        direction.y -= gravity * Time.deltaTime;

        characterController.Move(transform.TransformDirection(direction) * moveSpeed * Time.deltaTime);
    }

    private void moveMouse()
    {
        mouseX += Input.GetAxisRaw("Mouse X");

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    private void bulletFire()
    {
        if (ammo > 0)
        {
            ammo--;

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //닿은 곳의 정보
            RaycastHit hit;
            //바라본다
            //Raycast는 기본적으로 bool형을 반환하게 된다. 그런데 인자 값에 out형으로 인자하나를 더 반환하게 된다.
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
        else
        {
            Debug.Log("ammo is empty");
        }
    }

    private void reload()
    {
        ammo = maxAmmo;
    }

    private void levelUp()
    {
        if(exp >= maxExp)
        {
            exp -= maxExp;
            level++;
            maxBreath += 50f;
        }
    }

    private void expTest()
    {
        exp += 5;
    }
}
