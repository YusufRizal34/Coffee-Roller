using UnityEngine;

public class UserDataManager
{
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress;

    public static void Load(){
        if (!PlayerPrefs.HasKey(PROGRESS_KEY)){
            Progress = new UserProgressData ();
            Save();
        }
        else{
            string json = PlayerPrefs.GetString (PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData> (json);
        }
    }

    public static void Save (){
        string json = JsonUtility.ToJson(Progress);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
    }

    public static void Remove(){
        if (PlayerPrefs.HasKey(PROGRESS_KEY)){
            PlayerPrefs.DeleteKey(PROGRESS_KEY);
            Save();
        }
    }
}
