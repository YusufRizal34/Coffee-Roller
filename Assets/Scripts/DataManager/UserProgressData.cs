using System.Collections.Generic;

[System.Serializable]
public class UserProgressData
{
    public int currentCharacterID;
    public int usedCharacter;
    public bool isTutorialDone;
    public bool isSoundMuted;
    public List<Characters> character;

    public double coin;
    public double currentCoin;

    public double highScore;
    public double currentScore;

    public int totalScoreDoppio;
    public int totalLongBlack;
    public int levelScoreDoppio;
    public int levelLongBlack;
    public int levelExtraShot;
    public int levelSecondShot;

    public int UsedCharacter{ get{ return usedCharacter; } set{ usedCharacter = value; } }
    public bool IsTutorialDone{ get{ return isTutorialDone; } set{ isTutorialDone = value; } }
    public bool IsSoundMuted{ get{ return isSoundMuted; } set{ isSoundMuted = value; } }
    public int CurrentCharacter{ get{ return currentCharacterID; } set{ currentCharacterID = value; } }

    public double Coin{ get{ return coin; } set{ coin = value; } }
    public double CurrentCoin{ get{ return currentCoin; } set{ currentCoin = value; } }
    public double HighScore{ get{ return highScore; } set{ if(highScore > value) highScore = value; } }
    public double CurrentScore{ get{ return currentScore; } set{ currentScore = value; } }
    
    public int TotalScoreDoppio{ get{ return totalScoreDoppio; } set{ totalScoreDoppio = value; } }
    public int TotalLongBlack{ get{ return totalLongBlack; } set{ totalLongBlack = value; } }
    
    public int LevelScoreDoppio{ get{ return levelScoreDoppio; } set{ levelScoreDoppio = value; } }
    public int LevelLongBlack{ get{ return levelLongBlack; } set{ levelLongBlack = value; } }
    public int LevelExtraShot{ get{ return levelExtraShot; } set{ levelExtraShot = value; } }
    public int LevelSecondShot{ get{ return levelSecondShot; } set{ levelSecondShot = value; } }
}