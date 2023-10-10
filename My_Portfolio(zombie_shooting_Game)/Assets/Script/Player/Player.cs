using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    // player status
    private int exp;
    private int ammo;
    private int level = 1;
    private float speed = 5.0f;
    private float reloadSpeed = 1.0f;
    private float damage = 10.0f;
    private float range = 10.0f;

    // current state
    private bool sprint = false;
    private bool reloading = false;

    // max value
    private int maxAmmo = 15;
    private int maxHP = 100;
    private int maxExp = 10;

    // mouse movement
    private float mouseX;

    // fixed value
    private float gravity = 100.0f;

    // parameters
    private Vector3 direction;

    // references
    private CharacterController characterController;
    [SerializeField] Text ammoText;

    private void Awake()
    {
        health = maxHP;
        characterController = GetComponent<CharacterController>();

    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ammo = maxAmmo;
    }

    void Update()
    {
        moveMent();
        moveMouse();
        
        Debug.Log(level + " " + ammo);

        if (Input.GetKey(KeyCode.LeftShift)) sprint = true;
        playerSprint();

        if (Input.GetMouseButton(0)) bulletFire();
        if (Input.GetKeyDown(KeyCode.R)) reload();

        ammoText.text = ammo.ToString() + " / " + maxAmmo.ToString();
    }

    private void moveMent()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        direction.y -= gravity * Time.deltaTime;

        characterController.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

    private void playerSprint()
    {
        if (sprint)
        {
            speed = 10f;
            Debug.Log("달림");
        }
        else
        {
            speed = 5f;
        }

        sprint = false;
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

            Debug.Log("fire");
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
}
