using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball_Roll : MonoBehaviour
{
    //Snowball Rolling
    public Move Move_Player;
    public float snowballPushCD;
    public float timer;
    public bool onCD;

    //Snowball Roll Check
    private Vector3 currentPos;
    private Vector3 lastPos;
    public bool moving;


    //Snowball Stacking
    //public Snowball_Roll incSnowball;
    //public Vector3 stackPos;
    //public float stackOffset;
    //public bool stacked;
    //public bool top;
    //public Vector3 lockPos;

    void Update()
    {
        PushCDTimer();
        MoveCheck();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (stacked)
    //    {
    //        lockPos = gameObject.transform.position;
    //    }

    //    if (!stacked)
    //    {
    //        if (other.gameObject.tag == "Snowball")
    //        {
    //            incSnowball = other.gameObject.GetComponent<Snowball_Roll>();

    //            if (moving)
    //            {
    //                top = true;
    //            }
    //            stackPos = incSnowball.transform.position;
    //            stacked = true;
    //        }
    //    }
    //}


    //


    //MovementCheck
    void MoveCheck()
    {
        currentPos = new Vector3(transform.position.x, 0f, transform.position.z);
        float deltaDistance = Vector3.Distance(currentPos, lastPos);
        if (deltaDistance < 0.05f)
        {
            moving = false;
        }
        else moving = true;

        lastPos = currentPos;

    }

    //Repush Cooldown
    void PushCDTimer()
    {
        if (!onCD)
        {
            timer = snowballPushCD;
        }
        if (onCD)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Move_Player = null;
            }
        }
    }

    //Player Check
    private void OnCollisionExit(Collision collision)
    {
        if (Move_Player != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                onCD = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (Move_Player == null)
        {
            if (collision.gameObject.tag == "Player")
            {
                Move_Player = collision.gameObject.GetComponent<Move>();
                onCD = false;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (Move_Player != null)
        {

            if (other.gameObject.tag == "Player")
            {
                this.transform.position += Move_Player.newDir * Time.deltaTime * Move_Player.moveSpeed * 0.8f;
            }
        }
    }

}