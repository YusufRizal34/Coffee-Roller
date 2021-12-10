using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    void Update()
	{
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
		{
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
    
}
