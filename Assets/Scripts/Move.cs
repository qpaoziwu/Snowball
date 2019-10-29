using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Range(1f, 10f)]
    public float moveSpeed;
    [Range(1f, 10f)]
    public float xSpeedTuner;


    public Vector3 dir;
    public Vector3 dirRelativeToCamera;
    public Vector3 newDir;

    public bool moving;
    void Update()
    {
        MoveObjectWithRotation();
    }

    public void MoveObjectWithRotation()
    {

        dir = new Vector3((Input.GetAxis("Horizontal")), 0f, (Input.GetAxis("Vertical")));
        dirRelativeToCamera = Camera.main.transform.TransformDirection(dir);
        newDir = new Vector3 (dirRelativeToCamera.x * xSpeedTuner, 0f , dirRelativeToCamera.z);

        if (moving)
        {
            transform.position += newDir * Time.deltaTime * moveSpeed;
        }

        if (Input.GetAxis("Horizontal")!=0f|| (Input.GetAxis("Vertical")!=0f)) 
        {
            transform.rotation = Quaternion.LookRotation(newDir);

        }
    }
}
