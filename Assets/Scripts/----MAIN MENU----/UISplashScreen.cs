using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISplashScreen : MonoBehaviour
{

	public float time;

	private void Start()
	{
		Invoke("LoadGame", 5f);
	}

	private void Update()
	{
		if (time < 4f)
		{
			time += Time.deltaTime;
		}
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("OpeningScene");
	}
}
