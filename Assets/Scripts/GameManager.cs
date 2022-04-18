using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


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

    public Text tampilCoin;

    private void Awake(){
        Application.targetFrameRate = 120;
        UserDataManager.Load();
        tampilCoin.text = showCoin().ToString();
    }

    public void updateCoin()
    {
        tampilCoin.text = showCoin().ToString();
    }

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}

    ///GET USER DATA MANAGER VALUE
    #region GET USERDATA

    public int showCoin()
    {
        return UserDataManager.Progress.Coin;
    }

    #endregion

    ///SET USER DATA MANAGER VALUE
    #region SET USERDATA


    public void addCoin(int coin)
    {
        UserDataManager.Progress.Coin += coin;
        UserDataManager.Save();
    }

    #endregion
}
