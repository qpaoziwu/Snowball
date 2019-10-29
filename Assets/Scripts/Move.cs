﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Range(1f, 10f)]
    public float moveSpeed;
    [Range(1f, 10f)]
    public float xSpeedTuner;

    public bool moving;
    void Update()
    {
        MoveObjectWithRotation();
    }

    public void MoveObjectWithRotation()
    {

        Vector3 dir = new Vector3((Input.GetAxis("Horizontal")), 0f, (Input.GetAxis("Vertical")));
        Vector3 dirRelativeToCamera = Camera.main.transform.TransformDirection(dir);
        Vector3 newDir = new Vector3 (dirRelativeToCamera.x * xSpeedTuner, 0f , dirRelativeToCamera.z);

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
