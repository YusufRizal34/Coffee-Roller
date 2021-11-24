using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
    [Header("CHARACTER CONTROLLER")]
    private CharacterController _controller;
    public bool isAndroid = true;
    private float currentXPosition;
    private float currentYPosition;

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
    [SerializeField] private float speed;
<<<<<<< HEAD

    private void Awake()
    {
        
    }

    private void Start(){
        controller = GetComponent<CharacterController>();
=======
    [SerializeField] private float dodgeSpeed;

    [Header("GRAVITY SETTING")]
    public float gravity = -0.5f;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        SetupJump();
>>>>>>> f721ae2444540826cf77342e5b37ec9ed23e97d0
    }

    private void Update()
    {
        HandleGravity();
        MovementController();
    }

    private void MovementController()
    {

        if(isAndroid){
            currentXPosition = Input.acceleration.x;

            if(Input.GetKeyDown("space")){
                if(_controller.isGrounded){
                    currentYPosition = initialJumpVelocity;
                }
            }
        }
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

    // public void StumbleController(GameObject objects)
    // {
    //     if(objects.name == centerCollider.name){
    //         gameObject.SetActive(false);
    //     }
    //     else if(objects.name == rightCollider.name){
    //         StartCoroutine(StumbleDelay(stumbleDelay));
    //         currentXPosition = -newPosition;
    //         positions = Position.Right;
    //         _controller.Move(moving);
    //     }
    //     else if(objects.name == leftCollider.name){
    //         StartCoroutine(StumbleDelay(stumbleDelay));
    //         currentXPosition = newPosition;
    //         positions = Position.Right;
    //         _controller.Move(moving);
    //     }
    // }

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
}
