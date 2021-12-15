using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CanvasType{
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
    public static float currentCoin;

    public static int highScore;
    public static int currentScore;

    public Text coinText;
    public Text currentCoinText;

    public Text highScoreText;
    public Text currentScoreText;

    [Header("Skill Character")]
    private static bool arabicaSkill = false;
    private static bool libericaSkill = false;

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

        arabicaSkill    = false;
        libericaSkill   = false;
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
            case CanvasType.ShopScene :
                coinText  = GameObject.FindWithTag("Coin").GetComponent<Text>();
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
        else if(type == CanvasType.ShopScene){
            coinText.text    = coin.ToString();
        }
    }

    public void Result(){
        SkillController();
        AddCoin();
        AddCurrentScore();
        UserDataManager.Save();
        SceneManager.LoadScene("Result Scene", LoadSceneMode.Single);
    }

    public void Retry(){
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public static void CastSkill(string characterName){
        if(characterName == "Arabica"){
            arabicaSkill = true;
        }
    }

    private void SkillController(){
        if(arabicaSkill == true){
            currentCoin *= 1.5f;
        }
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