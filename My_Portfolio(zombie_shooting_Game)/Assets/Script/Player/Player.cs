using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private float shotSpeed = 1.0f;
    private float speed = 5.0f;
    private float reloadSpeed = 1.0f;
    private float damage = 10.0f;
    private float spread = 10.0f;

    // current state
    private bool isReloading = false;

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
    private Vector3 moveDirection;

    // references
    private CharacterController characterController;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject status;
    [SerializeField] private GameObject grenade;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject thorwPos;

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
        MoveMent();
        MoveMouse();

        if (Input.GetMouseButton(0))
            BulletFire();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        if (Input.GetKeyDown(KeyCode.Space))
            GrenadeThrow();

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            PauseGame();

        if (Input.GetKey(KeyCode.Tab))
            OpenStaus();
        else if(Input.GetKeyUp(KeyCode.Tab))
            CloseStaus();

        LevelUp();
        if (Input.GetKeyDown(KeyCode.Z)) HPest();
    }

    private void MoveMent()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");

        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && breath > 0) 
        {
            moveSpeed = speed * 1.75f;

            if(moveDirection.x != 0 || moveDirection.z != 0) // player is moveing
                breath -= 0.1f;
        }
        else
        {
            moveSpeed = speed;
            if(breath < maxBreath && !Input.GetKey(KeyCode.LeftShift))
                breath += 0.5f;
        }

        moveDirection.Normalize();

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }

    private void MoveMouse()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * Time.timeScale;

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    private void BulletFire()
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

    private void Reload()
    {
        ammo = maxAmmo;
    }

    private void GrenadeThrow()
    {
        GameObject instantGrenade = Instantiate(grenade, thorwPos.transform.position, transform.rotation);
        Rigidbody grenadeRB = instantGrenade.GetComponent<Rigidbody>();
        grenadeRB.AddForce(mainCam.transform.forward * 15f, ForceMode.Impulse);
    }

    private void LevelUp()
    {
        if(exp >= maxExp)
        {
            exp -= maxExp;
            level++;

            levelPanel.SetActive(true);

            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    private void OpenStaus()
    {
        status.SetActive(true);
    }
    private void CloseStaus()
    {
        status.SetActive(false);
    }

    private void ExpTest()
    {
        exp += 5;
    }
    private void HPest()
    {
        health -= 20;
        if(health <= 0) GameManager.Instance.isDead = true;
    }
}
