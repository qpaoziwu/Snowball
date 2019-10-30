using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSnow : MonoBehaviour
{
    //public Move PlayerReference;
    //public SnowShot SnowShot;
    public GameObject Snowshot;
    public GameObject Snowball;
    public BoxCollider checkBox;
    public Transform shootLocation;
    public bool blocked;

    //[SerializeField]
    //float Fire_Delay;
    //[SerializeField]
    //float Fire_Rate;
    public KeyCode shoot;
    public KeyCode gather;

    public int ammoCap;
    public int currentAmmo;
    public int ThrowForce;

    public GameObject tempBall;

    public float shootTimer;
    public float coolDown;


    void Start() {
        ammoCap = 1;

        //PlayerReference = GetComponent<Move>();
        //SnowShot = GetComponent<SnowShot>();
    }
    void Update()
    {
        Timer();
        PlayerInput();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject) blocked = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject) blocked = false;
    }

    void Timer()
    {
        shootTimer -= Time.deltaTime;

    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(shoot) && shootTimer < 0) Shoot();
        if (Input.GetKeyDown(gather) && shootTimer < 0) Gather();
    }
    void Gather()
    {
        if (currentAmmo > ammoCap)
        {
            currentAmmo = ammoCap;
        }

        if(currentAmmo == ammoCap)
        {
            if (Snowball != null)
            {
                if (!blocked) {
                    Instantiate(Snowball, transform.position + Vector3.up, Quaternion.identity);
                    currentAmmo--;
                }
            }
        } else if (currentAmmo < ammoCap)
        {
            currentAmmo++;
        }

    }
    void Shoot()
    {
        //if (tempBall == null)
        {
            if (currentAmmo > 0)
            {
                shootTimer = coolDown;
                tempBall = Instantiate(Snowshot, shootLocation.position, Quaternion.identity);
                Vector3 SnowPath = (transform.forward + transform.up*0.7f) * ThrowForce;
                tempBall.GetComponent<Rigidbody>().AddForce(SnowPath, ForceMode.Impulse);
                currentAmmo--;
            }
            else print("Gather Snow First");
        }
    }
}
