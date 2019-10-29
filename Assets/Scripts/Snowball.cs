using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    //Snowball Rolling
    public Move Move_Player;
    public float size;
    public bool rollingOnSnow;
    public float increaseRate;
    public Vector3 rollStart;
    public float snowballPushCD;
    public float timer;
    public bool onCD;

    //Snowball Roll Check
    private Vector3 currentPos;
    private Vector3 lastPos;
    public bool moving;

    //Snowball Stacking
    public Snowball incSnowball;
    public Vector3 stackPos;
    public float stackOffset;
    public bool stacked;
    public bool top;
    public Transform snowball;
    public Transform snowballWaypoint;
    public Transform TargetSnowball;

    void Start()
    {
    }
    
    void Update()
    {
        PushCDTimer();
        IncreaseSizeWhenMoving();
        MoveCheck();
        LockStackedPosition();
    }

    //
    void LockStackedPosition()
    {

        if (stacked)
        {
            if (moving)
            {
               stackOffset = 1.4f;
               gameObject.GetComponent<SphereCollider>().enabled = false;
               gameObject.transform.position = new Vector3(stackPos.x, 1f+ stackPos.y/stackOffset, stackPos.z);

            }
            if(!moving)
            {
                gameObject.transform.position = gameObject.transform.position;
            }
 
            
        }
        else
        {
            //Lock Y Position relative to Scale
            stackOffset = 3f;
            gameObject.transform.position = new Vector3(transform.position.x, size / stackOffset, transform.position.z);
        }
    }
    //Runs in OnTriggerEnter();
    void CheckIncomingSnowball(Collider c)
    {
        if (!stacked)
        {
            if (c.gameObject.tag == "Snowball")
            {
                incSnowball = c.gameObject.GetComponent<Snowball>();

                if (moving)
                {
                    top = true;
                }
                stackPos = incSnowball.transform.position;
                stacked = true;
            }
        }
    }

    //MovementCheck
    void MoveCheck()
    {
        currentPos = new Vector3(transform.position.x,0f, transform.position.z);
        float deltaDistance = Vector3.Distance(currentPos, lastPos);
        if (deltaDistance <0.05f)
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
        if (!stacked)
        {
            if (Move_Player != null)
            {
                if (other.gameObject.tag == "Player")
                {
                    this.transform.position += Move_Player.newDir * Time.deltaTime * Move_Player.moveSpeed * 0.5f;
                }
            }
        }
    }
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

    //Rolling On Snow Check
    private void OnTriggerEnter(Collider other)
    {
        CheckIncomingSnowball(other);
        if (Move_Player == null)
        {
            if (other.gameObject.tag == "Snow")
            {
                rollingOnSnow = true;
                rollStart = gameObject.transform.position;

            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (Move_Player != null)
        {
            if (collision.gameObject.tag == "Snow")
            {
                rollingOnSnow = true;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Snow")
        {
            rollingOnSnow = false;

        }

    }

    //Increase Size When Moving
    private void IncreaseSizeWhenMoving()
    {
        // Lock Size to float size
        gameObject.transform.localScale = new Vector3(size, size, size);

        // Set Min Max
        size = Mathf.Clamp(size, 3f, 5f);

        // Distance Rolled
        float rollDistance = Vector3.Distance(rollStart, gameObject.transform.position);

        if (moving)
        {
            if (rollingOnSnow)
            {
                if (size < 5)
                {
                    size += increaseRate * Time.deltaTime;
                }
            }
        }
        
    }
}
