using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public static int coin;
    public static int currentCoin;

    public static int highScore;
    public static int currentScore;

    public Text coinText;
    public Text currentScoreText;

    private void Awake() {
        Application.targetFrameRate = 120;

        coinText            = GameObject.FindWithTag("Coin").GetComponent<Text>();
        currentScoreText    = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();

        if(coinText != null && currentScoreText != null){
            UserDataManager.Load();

            coinText.text           = UserDataManager.Progress.CurrentCoin.ToString();
            currentScoreText.text   = UserDataManager.Progress.CurrentScore.ToString();
        }
    }

    private void Update() {
        coinText.text           = currentCoin.ToString();
        currentScoreText.text   = currentScore.ToString();
    }

    public void Result(){
        AddCoin();
        AddCurrentScore();
        UserDataManager.Save();
        SceneManager.LoadScene("Result Scene", LoadSceneMode.Single);
    }

    public void Retry(){
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void AddCoin(){
        UserDataManager.Progress.Coin = currentCoin;
    }

    public void AddCurrentScore(){
        UserDataManager.Progress.CurrentScore = currentScore;
    }
}
