using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject floatingText;

    private void Start()
    {
        StartCoroutine(StartFloatingText());
    }

    void Update()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator StartFloatingText()
    {
        yield return new WaitForSeconds(1.2f);
        floatingText.SetActive(true);
    }

    IEnumerator SelfDestruct()
    {
        
        yield return new WaitForSeconds(5f);
        floatingText.SetActive(false);
        //Destroy(floatingText);
    }
}
