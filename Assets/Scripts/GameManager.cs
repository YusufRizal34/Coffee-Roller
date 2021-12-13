using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CanvasType{
    MainMenu,
    PlayScene,
    ResultScene,
}

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

    public CanvasType type;

    public static int coin;
    public static int currentCoin;

    public static int highScore;
    public static int currentScore;

    public Text coinText;
    public Text currentCoinText;

    public Text highScoreText;
    public Text currentScoreText;

    private void Awake() {
        Application.targetFrameRate = 120;

        SwithCanvas();

        UserDataManager.Load();

        if(coinText != null){
            coinText.text   = UserDataManager.Progress.Coin.ToString();
        }
        if(currentCoinText != null){
            currentCoinText.text   = UserDataManager.Progress.CurrentCoin.ToString();
        }
        if(highScoreText != null){
            highScoreText.text   = UserDataManager.Progress.HighScore.ToString();
        }
        if(currentScoreText != null){
            currentScoreText.text   = UserDataManager.Progress.CurrentScore.ToString();
        }
    }

    private void Update() {
        UIOnUpdate();
    }

    private void SwithCanvas(){
        switch(type){
            case CanvasType.MainMenu :
                //NOTHING
            break;
            case CanvasType.PlayScene :
                currentCoinText  = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
            case CanvasType.ResultScene :
                currentCoinText  = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
        }
    }

    private void UIOnUpdate(){
        if(type == CanvasType.MainMenu){

        }
        else if(type == CanvasType.PlayScene){
            currentCoinText.text    = currentCoin.ToString();
            currentScoreText.text   = currentScore.ToString();
        }
        else if(type == CanvasType.ResultScene){
            currentCoinText.text    = currentCoin.ToString();
            currentScoreText.text   = currentScore.ToString();
        }
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

    private void AddCoin(){
        UserDataManager.Progress.Coin = coin;
    }

    private void AddHighScore(){
        UserDataManager.Progress.CurrentScore = highScore;
    }
    private void AddCurrentCoin(){
        UserDataManager.Progress.Coin = currentCoin;
    }

    private void AddCurrentScore(){
        UserDataManager.Progress.CurrentScore = currentScore;
    }
}