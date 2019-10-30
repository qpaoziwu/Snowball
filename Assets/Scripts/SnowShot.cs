using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnowShot : MonoBehaviour
{
    public float LifeTime = 4f;


    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, LifeTime);
    }
}
