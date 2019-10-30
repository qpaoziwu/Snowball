using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball_Stack : MonoBehaviour
{
    //Snowball Stacking
    public Snowball_Roll thisSnowball;
    public Snowball_Stack incSnowball;
    public Vector3 stackPos;
    public float stackOffset;
    public bool stacked;
    public bool top;
    public Vector3 lockPos;

    public bool inSafeZone;

    void Start()
    {
        if (thisSnowball != null)
        {
            thisSnowball = gameObject.GetComponent<Snowball_Roll>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        StackToPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!stacked)
        {
            if (other.gameObject.tag == "Snowball")
            {
                incSnowball = other.gameObject.GetComponent<Snowball_Stack>();

                if (thisSnowball.moving && !incSnowball.stacked)
                // if (thisSnowball.moving && !incSnowball.stacked)

                {
                    stacked = true;
                    lockPos = gameObject.transform.position;
                }
            }
        }
        //if (stacked)
        //{
        //    this.enabled = false;
        //}

    }

    void StackToPos()
    {
        if (incSnowball != null)
        {
            if (incSnowball.transform.position.y > gameObject.transform.position.y+1f)
            {
                stacked = true;
            }
        }
        if (stacked)
        {
            
            stackPos = incSnowball.transform.position;


            if (thisSnowball.moving && !incSnowball.stacked)
            {
                top = true;

                stackOffset = 1.4f;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                lockPos = new Vector3(stackPos.x + 0.3f, gameObject.transform.localScale.y / 2f + incSnowball.transform.localScale.y / stackOffset, stackPos.z);

            }
            if (!thisSnowball.moving)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                //Lock Position to self
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                //lockPos = gameObject.transform.position;

            }

            thisSnowball.enabled = false;
            gameObject.transform.position = lockPos;

        }


    }
}
