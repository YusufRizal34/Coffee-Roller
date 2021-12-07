using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	[Header("CHARACTER CONTROLLER")]
	public CharacterControllers _charController;

	[Header("CAMERA")]
	public FollowedCamera gameCamera;

	[Header("GAME OVER")]
	public GameObject gameOverScreen;
	public float fallPositionY;

	private void Update()
	{
		// UNTUK POSISI JATUHNYA KARAKTER
		if (transform.position.y < fallPositionY)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		// BUAT CHARACTER BERHENTI KETIKA GAME OVER
		_charController.enabled = false;

		// BUAT CAMERA BERHENTI KETIKA GAME OVER
		gameCamera.enabled = false;

		// BUAT NAMPILIN UI GAME OVER
		gameOverScreen.SetActive(true);
		this.enabled = false;
	}
}
