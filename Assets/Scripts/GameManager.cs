using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Cinemachine;

public enum CanvasType{
    SplashScene,
    OpeningScene,
    MainMenu,
    PlayScene,
    ResultScene,
    ShopScene,
    PlayerSelectionScene,
    CafeScene,
}

[System.Serializable]
public class Characters{
    public int id;
    public bool isUnlock;
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [Header("CANVAS CONTROLLER")]
    public CanvasType type;
    public GameObject pauseMenu;
    public bool isPause;
    public bool isTutorial;

    [Header("CHARACTER CONTROLLER")]
    public Character[] character;
    private Characters characters;
    private CharacterControllers characterControllers;
    public int specialModeCoin = 100;
    public bool isSpecialMode;

    [Header("GAME OVER CONTROLLER")]
	public GameObject characterPosition;
	public float fallPositionY;

    [Header("UI ITEM CONTROLLER")]
    public int currentCoin;
    public int coinFromTrack;
    public int currentScore;

    private Text coinText;
    private Text currentCoinText;

    private Text highScoreText;
    private Text currentScoreText;
    
    private Text totalScoreDoppio;
    private Text totalLongBlack;
    private Text levelScoreDoppio;
    private Text levelLongBlack;

    public Slider specialMode;

/*    public List<IBuffable> buff = new List<IBuffable>();*/
    public GameObject toggleMuteOn;
    private bool isMute = true;

    [Header("CUTSCENE CONTROLLER")]
    public CinemachineVirtualCamera gameCamera1;
    public CinemachineVirtualCamera gameCamera2;
    
    private float currentDuration;
    public float duration = 3f; ///DEFAULT 3
    private float CutSceneDuration{
        get{ return currentDuration; }
        set{ currentDuration = Mathf.Clamp(value, 0, duration); }
    }

    public bool isCutscene = true;

    private void Awake(){
        isPause = false;
        isTutorial = false;
        Application.targetFrameRate = 120;
        
        SwitchCanvas();

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
        if(specialMode != null){
            specialMode.maxValue   = specialModeCoin;
        }

        if(gameCamera1 != null && gameCamera2 != null){
            print(gameCamera1.transform.position);
            print(gameCamera2.transform.position);
        }
    }

    private void Update(){
        UIUpdate();
/*        if(coinFromTrack == specialModeCoin && isSpecialMode == false){
            isSpecialMode = !isSpecialMode;
            SpecialMode();
        }*/
 /*       BuffUpdate();*/
    }

    private void SwitchCanvas(){
        switch(type){
            case CanvasType.OpeningScene:
                AudioManager.instance.Play("BGM Main");
                UserDataManager.Remove();
                UserDataManager.Load();
                GameManager.Instance.AddCoin(100000);
            break;
            case CanvasType.MainMenu :
                UserDataManager.Load();
                if(UserDataManager.Progress.character == null || UserDataManager.Progress.character.Count < character.Length){
                    GameManager.Instance.LoadCharacter();
                }
                toggleMuteOn = GameObject.FindWithTag("ToggleOn");
            break;
            case CanvasType.PlayScene :
                if(FindObjectOfType<AudioManager>()){
                    AudioManager.instance.Stop("BGM Main");
                    AudioManager.instance.Play("BGM Gameplay");
                }
                UserDataManager.Load();
                int currentCharacter    = GameManager.Instance.ShowUsedCharacter();
                CharacterControllers players = Instantiate(character[currentCharacter].GetComponent<CharacterControllers>(), transform.position, character[currentCharacter].transform.rotation);
                characterControllers = players;
                if(character[currentCharacter].Name == "Liberica"){
                    players.IncreaseStumble(1);
                }
                characterPosition       = GameObject.FindWithTag("Player");
		        gameCamera1             = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
                gameCamera1.LookAt      = characterControllers.transform;
                gameCamera1.Follow      = characterControllers.transform;
		        gameCamera2             = GameObject.FindWithTag("CutSceneCamera").GetComponent<CinemachineVirtualCamera>();
                gameCamera2.LookAt      = characterControllers.transform;
                currentCoinText         = GameObject.FindWithTag("CurrentCoin").GetComponent<Text>();
                currentScoreText        = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                break;
            case CanvasType.ResultScene :
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
            case CanvasType.CafeScene :
                UserDataManager.Load();
                coinText = GameObject.FindWithTag("Coin").GetComponent<Text>();
                break;
            default :
            break;
        }
    }

    private void UIUpdate(){
        if(type == CanvasType.SplashScene){
            Invoke("LoadGame", 5f);
        }
        else if(type == CanvasType.OpeningScene){
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")){
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if(type == CanvasType.PlayScene){
            if(isTutorial == true){
                Time.timeScale = 0;
            }
            else if(isPause == true && isTutorial != true){
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else{
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }

            CutSceneDuration += Time.deltaTime;
            if(CutSceneDuration >= duration){
                gameCamera2.gameObject.SetActive(false);
                isCutscene = false;
                GroundGenerator.Instance.isCutscene = false;
            }

            specialMode.value = coinFromTrack;

            if (characterPosition.transform.position.y < fallPositionY) Result();
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
        else if(type == CanvasType.CafeScene){
            coinText.text = ShowCoin().ToString();
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

    public void MuteToggle(){
        isMute = !isMute;

        if (isMute == false){
            AudioListener.volume = 0;
        }
        else{
            AudioListener.volume = 1;
        }

        toggleMuteOn.SetActive(isMute);
    }

    public void MainMenu(){
        if(FindObjectOfType<AudioManager>()){
            AudioManager.instance.Stop("BGM Gameplay");
        }
        AudioManager.instance.Play("BGM Main");
        BackMainMenu();
    }

    public void BackMainMenu(){
        LoadScene("MainMenu");
    }

    public void PauseControl(){
        isPause = !isPause;
    }

    public void Retry(){
        AudioManager.instance.Play("BGM Gameplay");
        SceneManager.LoadScene("Play");
    }

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}

    public void LoadGame(){
        SceneManager.LoadScene("OpeningScene");
	}
    public void Cafe()
    {
        SceneManager.LoadScene("Cafe");
    }
/*
    ///BUFF SYSTEM
    #region BUFF

    public void BuffUpdate(){
        for(int i = 0; i < buff.Count; i++){
            buff[i].FinishTime -= Time.deltaTime;
            Debug.Log(buff[i].FinishTime);

            if(buff[i].FinishTime <= 0){
                buff[i].Finished(characterControllers);
                buff.Remove(buff[i]);
            }
        }
    }*/

/*    public void AddBuff(IBuffable buffs){
        bool isOnList = false;
        for(int i = 0; i < buff.Count; i++){
            if(buff[i].BuffName == buffs.BuffName){
                isOnList = true;
                buff[i].FinishTime = FinishTimeWithLevel(buffs);
                break;
            }
        }

        if(isOnList != true){
            buffs.FinishTime = FinishTimeWithLevel(buffs);
            buff.Add(buffs);
            buffs.Apply(characterControllers);
        }
    }*/

/*    private float FinishTimeWithLevel(IBuffable buffs){
        if(buffs.BuffName == "SecondShot"){
            return buffs.FinishTime *= ShowLevelSecondShot();
        }
        else if(buffs.BuffName == "ExtrShot"){
            return buffs.FinishTime *= ShowLevelExtraShot();
        }
        else if(buffs.BuffName == "LongBlack"){
            return buffs.FinishTime *= ShowLevelLongBlack();
        }
        else if(buffs.BuffName == "ScoreDoppio"){
            return buffs.FinishTime *= ShowLevelScoreDoppio();
        }

        return buffs.FinishTime;
    }*/

/*    private void SpecialMode(){
        GameManager.Instance.AddBuff(new CaffeineBoost());
    }

    #endregion*/

    ///GET USER DATA MANAGER VALUE
    #region GET USERDATA

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
        return (int)UserDataManager.Progress.CurrentCoin;
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

    public bool ShowIsTutorialDone(){
        return UserDataManager.Progress.IsTutorialDone;
    }

    #endregion

    ///SET USER DATA MANAGER VALUE
    #region SET USERDATA

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

    public void AddTutorialDone(){
        UserDataManager.Progress.IsTutorialDone = true;
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

    #endregion
}