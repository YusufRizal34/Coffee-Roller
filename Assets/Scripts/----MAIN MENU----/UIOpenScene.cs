using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOpenScene : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
