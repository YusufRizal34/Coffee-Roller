using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
	[Header("CHARACTER CONTROLLER")]
	public GameObject character;

	[Header("CAMERA")]
	public FollowedCamera gameCamera;

	[Header("GAME OVER")]
	public GameObject gameOverScreen;
	public float fallPositionY;

	private void Awake() {
		character = GameObject.FindWithTag("Player");
		gameCamera = GameObject.FindWithTag("MainCamera").GetComponent<FollowedCamera>();
	}

	private void Update()
	{
		// UNTUK POSISI JATUHNYA KARAKTER
		if (character.transform.position.y < fallPositionY)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		// BUAT CHARACTER BERHENTI KETIKA GAME OVER
		character.GetComponent<CharacterControllers>().enabled = false;

		// BUAT CAMERA BERHENTI KETIKA GAME OVER
		gameCamera.enabled = false;

		// BUAT NAMPILIN UI GAME OVER
		gameOverScreen.SetActive(true);
		this.enabled = false;
	}
}