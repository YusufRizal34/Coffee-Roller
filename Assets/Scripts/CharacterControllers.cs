using System;
using System.Threading.Tasks;
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

    public bool Invisible{ get; set; }
    public bool IsShielded{ get; set; }
    public bool IsSpeedUp{ get; set; }

    public bool isCutScene;

	[Header("ANDROID CONTROLLER")]
	private bool swipeUp, swipeDown;
	private bool isDraging = false;
	private Vector2 startTouch, swipeDelta;

	[Header("JUMP CONTROLLER")]
	public float initialJumpVelocity;
	public float maxJumpHeight = 4f;
	[Range(0.1f, 1f)] public float maxJumpTime = 0.5f;

    [Header("MOVEMENT CONTROLLER")]
    public float initialSpeed;
    public float speedAfterBuff;
    public float currentSpeed;
    public float CurrentSpeed{
        get{ return currentSpeed; }
        set{ currentSpeed = (float)Math.Round(Mathf.Clamp(value, initialSpeed, maxSpeed), 2); }
    }
    [SerializeField] private float maxSpeed = 50; ///DEFAULT 100
    [SerializeField] private float acceleration = 1; ///DEFAULT 1
    public int increaseSpeedModulo = 100; ///DEFAUL 100
    public bool isIncreaseSpeed = false;
    public float force = 20f;

	[Header("PC MOVEMENT CONTROLLER")]
	[SerializeField] private float dodgeSpeed = 0.2f; ///DEFAULT 0.2

	[Header("ANDROID MOVEMENT CONTROLLER")]
    [SerializeField] private float tiltingDodgeSpeed;

    [Header("GRAVITY SETTING")]
    public float gravity = -0.5f;

    [Header("CHARACTER DEAD")]
    public int maxStumble = 0; ///DEFAULT IS 0
    public bool isDead = false;
    public Vector3 beforeDeadPosition;
    public Vector3 afterDeadPosition;

    private void Start()
    {
        rb          = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
        SetupJump();
        CurrentSpeed = initialSpeed;
	}

    private void Update() {
        HandleGravity();
        if(isDead != true){
            MovementController();
        }
    }

    private void FixedUpdate() {
        if((int)transform.position.z % increaseSpeedModulo == 0 &&
            isIncreaseSpeed == false &&
            CurrentSpeed < maxSpeed &&
            isDead != true && IsSpeedUp != true){
            IncreaseSpeed();
        }
    }

    public void IncreaseStumble(int stumble){
        maxStumble = stumble;
    }

    private void IncreaseSpeed(){
        CurrentSpeed += acceleration;
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

        transform.Rotate(new Vector3(currentSpeed / 2, 0, 0), Space.Self);
        _controller.Move(moving * Time.deltaTime);
        
    }

    private Vector3 KeyboardMovement(){
        currentXPosition = 0;

        if(Input.GetKey("left") && GameManager.Instance.isCutscene != true)
        {
            currentXPosition = -CurrentSpeed;
        }

        if(Input.GetKey("right") && GameManager.Instance.isCutscene != true)
        {
            currentXPosition = CurrentSpeed;
        }

        if(Input.GetKeyDown("space") && GameManager.Instance.isCutscene != true){
            if(_controller.isGrounded){
                currentYPosition = initialJumpVelocity;
                AudioManager.instance.Play("Character Jump");
            }
        }
        Vector3 moving = new Vector3(currentXPosition * dodgeSpeed, currentYPosition, CurrentSpeed);
        return moving;
    }

    private Vector3 AndroidMovement(){
        currentXPosition = Input.acceleration.x;

        //CHECK IF TOUCHED
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
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
            if (Mathf.Abs(x) < Mathf.Abs(y) && _controller.isGrounded && GameManager.Instance.isCutscene != true)
            {
                if (y > 0) currentYPosition = initialJumpVelocity;
                AudioManager.instance.Play("Character Jump");
            }
            Reset();
        }

        Vector3 moving = new Vector3(currentXPosition * tiltingDodgeSpeed, currentYPosition, CurrentSpeed);
        return moving;
    }

    private void HandleGravity(){
        if(!_controller.isGrounded)
        {
            currentYPosition += gravity;
        }
    }

    private void SetupJump(){
        float timeToApex = maxJumpTime / 2;
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void Reset(){
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public IEnumerator CharacterDeadDuration(){
        yield return new WaitForSeconds(5);
    }

    public void Dead(){
        isDead = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(0,5,-1, ForceMode.Impulse);
        AudioManager.instance.Play("Character Crash");
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Interactable"){
            var objects = other.GetComponent<IInteractable>();
            if(objects != null) objects.Interaction();
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Interactable"){
            var objects = other.gameObject.GetComponent<IInteractable>();
            if(IsShielded){
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
            else{
                if(objects != null) objects.Interaction();
            }
        }
    }
}