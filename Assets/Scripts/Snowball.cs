using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public MoveTo MoveTo_Snowball;
    public Move Move_Player;
    
    public float size;

    public Transform snowball;
    public Transform snowballWaypoint;
    public Transform TargetSnowball;
    //Snowball stacking
    public float stackOffset;
    public bool stacking;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }





    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (Move_Player = null)
    //        {
    //            Move_Player = other.gameObject.GetComponent<Move>();

    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Move_Player = null)
            {
                Move_Player = collision.gameObject.GetComponent<Move>();

            }
        }

    }


        private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Move_Player != null)
            {
                this.transform.position += Move_Player.newDir * Time.deltaTime * Move_Player.moveSpeed;
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Move_Player != null)
            {
                Move_Player = null;
            }
        }

    }
}
