using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // private GameObject parent;
    private Rigidbody rb;
    public float force;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            // print("yes");
            // gameObject.GetComponentInParent<CharacterControllers>().StumbleController(gameObject);
            rb.AddForce(transform.forward * force, ForceMode.Force);
            rb.useGravity = true;
        }
    }

    private void OnMouseDown() {
        rb.AddForce(transform.forward * force, ForceMode.Force);
        rb.useGravity = true;
    }
}
