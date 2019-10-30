using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Move))]
public class StatusChange : MonoBehaviour
{
    public Move PlayerMovement;

    public float MaxSpeed;
    public float slowedMoveSpeed;
    public float slowDuration;
    
    [Range (0,1)]
    public float currentSpeed;

    public bool timing;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 1f;
        PlayerMovement = GetComponent<Move>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            timing = true;
        }
    }

    void Movement()
    {
        if (timing)
        {
            timer += Time.deltaTime;
            currentSpeed = 1f - ((slowDuration - timer) / slowDuration);
            if (timer > slowDuration)
            {
                timing = false;
                timer = 0;
            }
        }
        else currentSpeed = 1f;

        PlayerMovement.moveSpeed = Mathf.Lerp(slowedMoveSpeed, MaxSpeed, currentSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
