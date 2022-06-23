using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camface : MonoBehaviour
{
    public GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>().gameObject;
        transform.LookAt(transform.position + cam.transform.forward);
    }

}
