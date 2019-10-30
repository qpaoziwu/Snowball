using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Snowball_Roll))]
public class Snowball_IncreaseWithSize : MonoBehaviour
{
    //Size increases relative to increaseRate
    public float size;
    public bool rollingOnSnow;
    public float increaseRate;
    public Vector3 rollStart;

    //Snowball Roll Check
    private Vector3 currentPos;
    private Vector3 lastPos;
    public float rollDistance;
    public bool moving;

    void Update()
    {
        MoveCheck();
        IncreaseSizeWhenMoving();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snow")
        {
            rollingOnSnow = true;
            rollStart = gameObject.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Snow")
        {
            rollingOnSnow = false;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
            if (collision.gameObject.tag == "Snow")
            {
                rollingOnSnow = true;
            }
    }

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

    private void IncreaseSizeWhenMoving()
    {
        // Lock Size to float size
        gameObject.transform.localScale = new Vector3(size, size, size);

        // Set Min Max
        size = Mathf.Clamp(size, 3f, 5f);

        // Distance Rolled
        rollDistance = Vector3.Distance(rollStart, gameObject.transform.position);

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
