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

   // public Animator anim;
   // public Collider c;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
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
        //if (stacked)
        //{
        //    lockPos = gameObject.transform.position;
        //}

        if (!stacked)
        {
            if (other.gameObject.tag == "Snowball")
            {
                incSnowball = other.gameObject.GetComponent<Snowball_Stack>();
                if (!incSnowball.stacked)
                {
                    if (thisSnowball.moving)
                    {
                        top = true;
                    }
                    stackPos = incSnowball.transform.position;
                    stacked = true;
                }
            }
        }
    }

    void StackToPos()
    {
        if (stacked)
        {
            lockPos = incSnowball.transform.position;
            gameObject.transform.position = lockPos;
            //c.enabled = false;
            if (thisSnowball.moving)
            {
                if (top)
                {
                    stackOffset = 1.4f;
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    lockPos = new Vector3(stackPos.x + 0.3f, gameObject.transform.localScale.y / 2f + incSnowball.transform.localScale.y / stackOffset, stackPos.z);
                    
                }
            }
            if (!thisSnowball.moving)
            {
                //Lock Position to self
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                lockPos = gameObject.transform.position;
                //anim.SetBool(1, true);
                //c.enabled = false;
            }


            
        }
    }
}
