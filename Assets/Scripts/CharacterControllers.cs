using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControllers : MonoBehaviour
{
    [Header("CHARACTER CONTROLLER")]
    private CharacterController _controller;
    public bool isAndroid = true;
    private float currentXPosition;
    private float currentYPosition;

    [Header("ADNROID CONTROLLER")]
    public static bool tap, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    [Header("COLLIDER CONTROLLER")]
    public GameObject leftCollider;
    public GameObject centerCollider;
    public GameObject rightCollider;
    public float stumbleDelay = 1f;

    [Header("JUMP CONTROLLER")]
    public float initialJumpVelocity;
    public float maxJumpHeight = 4f;
    [Range(0.1f, 1f)] public float maxJumpTime = 0.5f;
    
    [Header("MOVEMENT CONTROLLER")]
    public float speed;
    [SerializeField] private float dodgeSpeed;

    [Header("GRAVITY SETTING")]
    public float gravity = -0.5f;

    [Header("PLAYER DISTANCE")]
    public Text distancemove;
    float distanceunit = 0;
    public string display = "Score : {0}";

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        SetupJump();
        InvokeRepeating("distance", 0, 1 / speed);
    }

    private void Update()
    {
        HandleGravity();
        MovementController();
    }

    private void MovementController()
    {
        //USING ANDROID
        if(isAndroid){
            currentXPosition = Input.acceleration.x;

            //CHECK IF TOUCHED
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }

            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                // if (Input.touches.Length < 0)
                //     swipeDelta = Input.touches[0].position - startTouch;
                if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if (swipeDelta.magnitude > 100)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) < Mathf.Abs(y))
                {
                    if (y > 0) currentYPosition = initialJumpVelocity;
                }
                Reset();
            }
        }
        //USING KEYBOARD
        else{
            currentXPosition = 0;

            if(Input.GetKey("left"))
            {
                currentXPosition = -speed;
            }

            if(Input.GetKey("right"))
            {
                currentXPosition = speed;
            }

            if(Input.GetKeyDown("space")){
                if(_controller.isGrounded){
                    currentYPosition = initialJumpVelocity;
                }
            }
        }

        Vector3 moving = new Vector3(currentXPosition * dodgeSpeed, currentYPosition, speed);

        _controller.Move(moving * Time.deltaTime);
    }

    private IEnumerator StumbleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private void HandleGravity()
    {
        if(!_controller.isGrounded)
        {
            currentYPosition += gravity;
        }
    }

    private void SetupJump()
    {
        float timeToApex = maxJumpTime / 2;
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    void distance()
    {
        display = "Score = {0}";
        distanceunit = distanceunit + 1;
        distancemove.text = distanceunit.ToString();
    }
}
