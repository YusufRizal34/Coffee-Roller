using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public CharacterControllers player;
    private float speed;

    private void Start() {
        speed = player.speed;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
