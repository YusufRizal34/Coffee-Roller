using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowedCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 4.5f;
    public Vector3 offset;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        if(target != null){
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
        }
    }
}
