using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
	public GameObject quitPopupUI;
	public bool active;

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

	public void QuitPoopup()
	{
		SceneManager.LoadScene("QuitPopup");
		quitPopupUI.SetActive(active);
		Debug.Log("PIYE ?");
	}

}
