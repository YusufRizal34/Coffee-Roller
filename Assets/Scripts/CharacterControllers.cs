using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
	[Header("CHARACTER CONTROLLER")]
	private CharacterController _controller;
    private Rigidbody rb;
	public bool isAndroid = true;
	private float currentXPosition;
	private float currentYPosition;

	[Header("ANDROID CONTROLLER")]
	public static bool tap, swipeUp, swipeDown;
	private bool isDraging = false;
	private Vector2 startTouch, swipeDelta;

	[Header("JUMP CONTROLLER")]
	public float initialJumpVelocity;
	public float maxJumpHeight = 4f;
	[Range(0.1f, 1f)] public float maxJumpTime = 0.5f;

    [Header("MOVEMENT CONTROLLER")]
    public float speed;
    [SerializeField] private float maxSpeed = 100; ///DEFAULT 100
    [SerializeField] private float acceleration = 1; ///DEFAULT 1
    public bool isIncreaseSpeed = false;

	[Header("PC MOVEMENT CONTROLLER")]
	[SerializeField] private float dodgeSpeed = 0.2f; ///DEFAULT 0.2

	[Header("ANDROID MOVEMENT CONTROLLER")]
    [SerializeField] private float tiltingDodgeSpeed;

    [Header("GRAVITY SETTING")]
    public float gravity = -0.5f;

    [Header("CHARACTER DEAD")]
    public int maxStumble = 0; ///DEFAULT IS 0

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
        SetupJump();
	}

    private void Update() {
        HandleGravity();
		MovementController();
    }

    private void FixedUpdate() {
        if((int)transform.position.z % 25 == 0 && isIncreaseSpeed == false && speed < maxSpeed){
            IncreaseSpeed();
        }
    }

    public void IncreaseStumble(int stumble){
        maxStumble = stumble;
    }

    private void IncreaseSpeed(){
        speed += acceleration;
        StartCoroutine(ResetCooldown());
    }

    private IEnumerator ResetCooldown(){
        isIncreaseSpeed = true;
        yield return new WaitForSeconds(0.1f);
        isIncreaseSpeed = false;
    }

    private void MovementController(){
        Vector3 moving;
        
        //USING ANDROID
        if(isAndroid){
            moving = AndroidMovement();
        }
        //USING KEYBOARD
        else{
            moving = KeyboardMovement();
        }

        rb.AddForce(0,-Mathf.Abs(moving.y),0, ForceMode.Impulse);
        _controller.Move(moving * Time.deltaTime);
    }

    private Vector3 KeyboardMovement(){
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

        Vector3 moving = new Vector3(currentXPosition * dodgeSpeed, currentYPosition, speed);
        return moving;
    }

    private Vector3 AndroidMovement(){
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
            if (Mathf.Abs(x) < Mathf.Abs(y) && _controller.isGrounded)
            {
                if (y > 0) currentYPosition = initialJumpVelocity;
            }
            Reset();
        }

        Vector3 moving = new Vector3(currentXPosition * tiltingDodgeSpeed, currentYPosition, speed);
        return moving;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle" && maxStumble > 0){
            maxStumble -= 1;
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Obstacle" && maxStumble < 1){
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}
