using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CanvasType{
    SplashScene,
    OpeningScene,
    MainMenu,
    PlayScene,
    ResultScene,
    ShopScene,
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

    public static int curentScoreDopio;
    public static int currentLongBlack;

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
            // case CanvasType.OpeningScene :
            //     //NOTHING
            // case CanvasType.MainMenu :
            //     //NOTHING
            // break;
            case CanvasType.PlayScene :
                currentCoinText  = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
            case CanvasType.ResultScene :
                currentCoinText  = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
            case CanvasType.ShopScene :
                coinText  = GameObject.FindWithTag("Coin").GetComponent<Text>();
            break;
            default :
            break;
        }
    }

    private void UIOnUpdate(){
        if(type == CanvasType.SplashScene){
            Invoke("LoadGame", 5f);
        }
        else if(type == CanvasType.OpeningScene){
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if(type == CanvasType.PlayScene){
            currentCoinText.text    = currentCoin.ToString();
            currentScoreText.text   = currentScore.ToString();
        }
        else if(type == CanvasType.ResultScene){
            currentCoinText.text    = currentCoin.ToString();
            currentScoreText.text   = currentScore.ToString();
        }
        else if(type == CanvasType.ShopScene){
            coinText.text    = coin.ToString();
        }
    }

    public void Result(){
        GameManager.AddCurrentCoin(currentCoin);
        GameManager.AddCurrentScore(currentScore);
        UserDataManager.Save();
        SceneManager.LoadScene("Result Scene", LoadSceneMode.Single);
    }

    public void Retry(){
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}

    public void LoadGame(){
		SceneManager.LoadScene("OpeningScene");
	}

    public static int ShowCoin(){
        return UserDataManager.Progress.Coin;
    }

    public static int ShowHighScore(){
        return UserDataManager.Progress.HighScore;
    }
    public static float ShowCurrentCoin(){
        return UserDataManager.Progress.CurrentCoin;
    }

    public static int ShowCurrentScore(){
        return UserDataManager.Progress.CurrentScore;
    }

    public static void AddCoin(int coin){
        UserDataManager.Progress.Coin = coin;
        UserDataManager.Save();
    }

    public static void AddHighScore(int highScore){
        UserDataManager.Progress.HighScore = highScore;
        UserDataManager.Save();
    }
    public static void AddCurrentCoin(int currentCoin){
        UserDataManager.Progress.Coin = currentCoin;
        UserDataManager.Save();
    }

    public static void AddCurrentScore(int currentScore){
        UserDataManager.Progress.CurrentScore = currentScore;
        UserDataManager.Save();
    }

    public static void AddBooster(string booster, int incLevel){
        if(booster == "Score Doppio"){
            UserDataManager.Progress.TotalScoreDoppio += incLevel;
        }
        else if(booster == "Long Black"){
            UserDataManager.Progress.TotalLongBlack += incLevel;
        }
        UserDataManager.Save();
    }

    public static void LevelUpItem(string booster, int incLevel){
        if(booster == "Score Doppio"){
            UserDataManager.Progress.LevelScoreDoppio += incLevel;
        }
        else if(booster == "Long Black"){
            UserDataManager.Progress.LevelLongBlack += incLevel;
        }
        UserDataManager.Save();
    }
}