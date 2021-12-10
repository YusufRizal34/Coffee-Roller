using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Play");
		Debug.Log("PLAY!!!");
	}
    public void QuitGame()
	{
		Application.Quit();
		Debug.Log("QUIT!!!");
	}

	public void QuitToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
		Debug.Log("METUOOO");
	}

	public void RetryGame()
	{
		SceneManager.LoadScene("Play");
		Debug.Log("MAIN NEH!!!");
	}
}
