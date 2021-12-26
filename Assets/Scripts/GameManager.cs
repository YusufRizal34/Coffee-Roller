using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum CanvasType{
    SplashScene,
    OpeningScene,
    MainMenu,
    PlayScene,
    ResultScene,
    ShopScene,
    PlayerSelectionScene,
}

[System.Serializable]
public class Characters{
    public int id;
    public bool isUnlock;
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

    [Header("CANVAS CONTROLLER")]
    public CanvasType type;

    [Header("CHARACTER CONTROLLER")]
    public Character[] character;
    private Characters characters;

    [Header("GAME OVER CONTROLLER")]
	public GameObject characterPosition;
	public FollowedCamera gameCamera;
	public GameObject gameOverScreen;
	public float fallPositionY;

    [Header("ITEM CONTROLLER")]
    public int currentCoin;
    public int currentScore;

    private Text coinText;
    private Text currentCoinText;

    private Text highScoreText;
    private Text currentScoreText;
    
    private Text totalScoreDoppio;
    private Text totalLongBlack;
    private Text levelScoreDoppio;
    private Text levelLongBlack;

    private void Awake() {
        Application.targetFrameRate = 120;
        
        SwithCanvas();

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
        if(totalScoreDoppio != null){
            totalScoreDoppio.text   = UserDataManager.Progress.TotalScoreDoppio.ToString();
        }
        if(totalLongBlack != null){
            totalLongBlack.text   = UserDataManager.Progress.TotalLongBlack.ToString();
        }
    }

    private void Update() {
        UIOnUpdate();
    }

    private void SwithCanvas(){
        switch(type){
            case CanvasType.OpeningScene:
                UserDataManager.Remove();
                UserDataManager.Load();
                GameManager.Instance.AddCoin(100000);
            break;
            case CanvasType.MainMenu :
                AudioManager.instance.Play("BGM Main");
                UserDataManager.Load();
                if(UserDataManager.Progress.character == null || UserDataManager.Progress.character.Count < character.Length){
                    GameManager.Instance.LoadCharacter();
                }
            break;
            case CanvasType.PlayScene :
                UserDataManager.Load();
                int currentCharacter    = GameManager.Instance.ShowUsedCharacter();
                CharacterControllers players = Instantiate(character[currentCharacter].GetComponent<CharacterControllers>());
                if(character[currentCharacter].Name == "Liberica"){
                    players.IncreaseStumble(1);
                }
                characterPosition       = GameObject.FindWithTag("Player");
		        gameCamera              = GameObject.FindWithTag("MainCamera").GetComponent<FollowedCamera>();
                currentCoinText         = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText        = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                AudioManager.instance.Stop("BGM Main");
                AudioManager.instance.Play("BGM Gameplay");
                break;
            case CanvasType.ResultScene :
                AudioManager.instance.Stop("BGM Gameplay");
                UserDataManager.Load();
                currentCoinText  = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
            case CanvasType.ShopScene :
                UserDataManager.Load();
                coinText            = GameObject.FindWithTag("Coin").GetComponent<Text>();
                totalScoreDoppio    = GameObject.FindWithTag("TotalScoreDopio").GetComponent<Text>();
                totalLongBlack      = GameObject.FindWithTag("TotalLongBlack").GetComponent<Text>();
            break;
            case CanvasType.PlayerSelectionScene :
                UserDataManager.Load();
                coinText            = GameObject.FindWithTag("Coin").GetComponent<Text>();
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
            if (characterPosition.transform.position.y < fallPositionY) GameOver();
            currentCoinText.text    = currentCoin.ToString();
            currentScoreText.text   = currentScore.ToString();
        }
        else if(type == CanvasType.ResultScene){
            currentCoinText.text    = ShowCurrentCoin().ToString();
            currentScoreText.text   = ShowCurrentScore().ToString();
        }
        else if(type == CanvasType.ShopScene){
            coinText.text           = ShowCoin().ToString();
            totalScoreDoppio.text   = ShowTotalScoreDoppio().ToString();
            totalLongBlack.text     = ShowTotalLongBlack().ToString();
        }
        else if(type == CanvasType.PlayerSelectionScene){
            coinText.text           = ShowCoin().ToString();
        }
    }

    public void Result(){
        if(character[GameManager.Instance.ShowUsedCharacter()].Name == "Arabica"){
            GameManager.Instance.AddCurrentCoin(currentCoin * 1.5f);
        }
        else{
            GameManager.Instance.AddCurrentCoin(currentCoin);
        }
        GameManager.Instance.AddCoin(currentCoin);
        GameManager.Instance.AddCurrentScore(currentScore);
        UserDataManager.Save();
        SceneManager.LoadScene("Result Scene", LoadSceneMode.Single);
    }

    public void LoadCharacter(){
        UserDataManager.Load();
        if(UserDataManager.Progress.character == null){
            for(int i = 0; i < character.Length; i++){
                characters          = new Characters();
                characters.id       = character[i].characterID;
                characters.isUnlock = character[i].isUnlock;
                GameManager.Instance.AddNewCharacter(characters);
            }
        }
        else{
            int current = UserDataManager.Progress.character.Count;
            for(int i = current; i < character.Length; i++){
                characters          = new Characters();
                characters.id       = character[i].characterID;
                characters.isUnlock = character[i].isUnlock;
                GameManager.Instance.AddNewCharacter(characters);
            }
        }
    }

    public void GameOver()
	{
		characterPosition.GetComponent<CharacterControllers>().enabled = false;
		gameCamera.enabled = false;
		gameOverScreen.SetActive(true);
		this.enabled = false;
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

    public int ShowUsedCharacter(){
        return UserDataManager.Progress.UsedCharacter;
    }

    public double ShowCoin(){
        return UserDataManager.Progress.Coin;
    }

    public double ShowHighScore(){
        return UserDataManager.Progress.HighScore;
    }

    public double ShowCurrentCoin(){
        return UserDataManager.Progress.CurrentCoin;
    }

    public double ShowCurrentScore(){
        return UserDataManager.Progress.CurrentScore;
    }

    public int ShowTotalScoreDoppio(){
        return UserDataManager.Progress.TotalScoreDoppio;
    }

    public int ShowTotalLongBlack(){
        return UserDataManager.Progress.totalLongBlack;
    }

    public int ShowLevelLongBlack(){
        return UserDataManager.Progress.levelLongBlack;
    }

    public int ShowLevelScoreDoppio(){
        return UserDataManager.Progress.LevelScoreDoppio;
    }

    public int ShowLevelExtraShot(){
        return UserDataManager.Progress.LevelExtraShot;
    }

    public int ShowLevelSecondShot(){
        return UserDataManager.Progress.LevelSecondShot;
    }

    public int ShowCurrentCharacter(){
        return UserDataManager.Progress.CurrentCharacter;
    }

    public bool ShowUnlockCharacter(int current){
        return UserDataManager.Progress.character[current].isUnlock;
    }

    public void CurrentCharacter(int current){
        UserDataManager.Progress.CurrentCharacter = current;
        UserDataManager.Save();
    }

    public void UsedCharacter(){
        UserDataManager.Progress.UsedCharacter = GameManager.Instance.ShowCurrentCharacter();
        UserDataManager.Save();
    }

    public void AddNewCharacter(Characters character){
        UserDataManager.Progress.character.Add(character);
        UserDataManager.Save();
    }

    public void UnlockCharacter(int id){
        UserDataManager.Progress.character[id].isUnlock = true;
        UserDataManager.Save();
    }

    public void AddCoin(double coin){
        UserDataManager.Progress.Coin += coin;
        UserDataManager.Save();
    }

    public void AddHighScore(double highScore){
        UserDataManager.Progress.HighScore = highScore;
        UserDataManager.Save();
    }
    public void AddCurrentCoin(double currentCoin){
        UserDataManager.Progress.CurrentCoin = currentCoin;
        UserDataManager.Save();
    }

    public void AddCurrentScore(double currentScore){
        UserDataManager.Progress.CurrentScore = currentScore;
        UserDataManager.Save();
    }
    
    public void AddBooster(string booster, int add){
        if(booster == "Score Doppio"){
            UserDataManager.Progress.TotalScoreDoppio += add;
        }
        else if(booster == "Long Black"){
            UserDataManager.Progress.TotalLongBlack += add;
        }
        UserDataManager.Save();
    }

    public void LevelUpItem(string items, int incLevel){
        if(items == "Score Doppio"){
            UserDataManager.Progress.LevelScoreDoppio += incLevel;
        }
        else if(items == "Long Black"){
            UserDataManager.Progress.LevelLongBlack += incLevel;
        }
        else if(items == "Extra Shot"){
            UserDataManager.Progress.LevelExtraShot += incLevel;
        }
        else if(items == "Second Shot"){
            UserDataManager.Progress.LevelSecondShot += incLevel;
        }
        UserDataManager.Save();
    }
}