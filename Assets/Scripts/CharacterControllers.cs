using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
    private CharacterController controller;
    public Position positions =  Position.Center;
    private float newPosition = 2f;
    private float currentPosition;
    
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float speed;

    private void Awake()
    {
        
    }

    private void Start(){
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if(Input.GetKeyDown("left")){
            if(positions != Position.Left){
                if(positions == Position.Center){
                    currentPosition = -newPosition;
                    positions = Position.Left;
                }
                else if(positions == Position.Right){
                    currentPosition = 0;
                    positions = Position.Center;
                }
            }
        }

        if(Input.GetKeyDown("right")){
            if(positions != Position.Right){
                if(positions == Position.Center){
                    currentPosition = newPosition;
                    positions = Position.Right;
                }
                else if(positions == Position.Left){
                    currentPosition = 0;
                    positions = Position.Center;
                }
            }
        }

        Vector3 moving = new Vector3((currentPosition - transform.position.x) * dodgeSpeed * Time.deltaTime, 0, speed * Time.deltaTime);
        controller.Move(moving);
    }
}

public enum Position{
    Right,
    Center,
    Left
}
