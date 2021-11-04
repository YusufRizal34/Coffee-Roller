using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; //0:kiri 1:tengah 2:kanan

    public float forwardSpeed;
    public float laneDistance = 4;
    public float jumpForce;
    public float Gravity = -20;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;
      //direction.y += Gravity * Time.fixedDeltaTime;
        //input
        if(controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.fixedDeltaTime;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(desiredLane==0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane==2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
      //transform.position = targetPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
