using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSnow : MonoBehaviour
{
    //public Move PlayerReference;
    //public SnowShot SnowShot;
    public GameObject Snowshot;
    public GameObject Snowball;
    public Transform shootLocation;
    //[SerializeField]
    //float Fire_Delay;
    //[SerializeField]
    //float Fire_Rate;
    [SerializeField]
    int ammoCap;
    [SerializeField]
    int currentAmmo;
    [SerializeField]
    int ThrowForce;

    public GameObject tempBall;

    public float shootTimer;
    public float coolDown;


    void Awake() {
        //PlayerReference = GetComponent<Move>();
        //SnowShot = GetComponent<SnowShot>();
    }

    void Update()
    {
        Timer();
        PlayerInput();
    } // Update End

    void Timer()
    {
        shootTimer -= Time.deltaTime;

    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && shootTimer < 0) Shoot();
        if (Input.GetKeyDown(KeyCode.F) && shootTimer < 0) Gather();

        //else
        //{
        //if (Input.GetKeyDown(KeyCode.E) && Time.time > Fire_Delay && Fire_Rate > 0 && Current_Magazine_Cap > 0)
        //if (Input.GetKeyDown(KeyCode.E) && shootTimer < 0 && Fire_Rate > 0 && Current_Magazine_Cap > 0)
        //{
        //    Debug.Log("Throw");
        //    shootTimer = coolDown;
        //    Shoot();
        //    Current_Magazine_Cap--;
        //}
        //else if (Current_Magazine_Cap == 0) Debug.Log("No Snowball!");

        //Fire_Rate = Fire_Delay;
        //} // else end


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
                Instantiate(Snowball, transform.position, Quaternion.identity);
                currentAmmo--;
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
