using System.Collections.Generic;

[System.Serializable]
public class UserProgressData
{
    private int coin;
    private float currentCoin;
    private int highScore;
    private int currentScore;

    private int totalScoreDoppio;
    private int totalLongBlack;
    private int levelScoreDoppio;
    private int levelLongBlack;

    public int Coin{ get{ return coin; } set{ coin += value; } }
    public float CurrentCoin{ get{ return currentCoin; } set{ currentCoin += value; } }
    public int HighScore{ get{ return highScore; } set{ if(highScore > value) highScore = value; } }
    public int CurrentScore{ get{ return currentScore; } set{ currentScore = value; } }
    
    public int TotalScoreDoppio{ get{ return totalScoreDoppio; } set{ totalScoreDoppio += value; } }
    public int TotalLongBlack{ get{ return totalLongBlack; } set{ totalLongBlack += value; } }
    public int LevelScoreDoppio{ get{ return levelScoreDoppio; } set{ levelScoreDoppio += value; } }
    public int LevelLongBlack{ get{ return levelLongBlack; } set{ levelLongBlack += value; } }
}