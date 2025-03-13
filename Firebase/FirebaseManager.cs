using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

[Serializable]
public class GameData
{
    public float FinalTime = 10000f;
    public bool Playing = false;
    public int Icon = (int)ECharacter.None;
}

[Serializable]
public class PlayerData
{
    public string Name = "";
    public string Time = "";
    public GameData KelpLevel     = new GameData();
    public GameData SpicyLevel    = new GameData();
    public GameData MushroomLevel = new GameData();
}

[Serializable]
public class ScoreRateData
{
    public string deviceUUID = "";
    public string Name       = "";
    public string Time       = "";
    public int Icon          = (int)ECharacter.None;
    public int GameType      = (int)EGameType.None;
    public float FinalTime   = 10000f;
}

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance = null;
    private string deviceUUID = "";
    private DatabaseReference dbReference;
    private PlayerData currentPlayerData = new PlayerData();

    private void Awake()
    {
        if(instance == null)
        {
            InitFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Call Function -------------------------------------------------------------------------------------
    public void OnCheckPlayerName(string playerName, Action<bool> callback) { CheckPlayerName(playerName, callback); }

    public void OnUpdatePlayerNameList(string strName) { UpdatePlayerNameList(strName); }
    public void OnSaveDefaultPlayerData() { SaveDefaultPlayerData(); }
    public void OnUpdatePlayerData() { UpdatePlayerData(); }

    public void OnStartKelpLevel(ECharacter eCharacter) { StartKelpLevel(eCharacter); }
    public void OnSetKelpLevelScore(float Score) { SetKelpLevelScore(Score); }

    public void OnStartSpicyLevel(ECharacter eCharacter) { StartSpicyLevel(eCharacter); }
    public void OnSetSpicyLevelScore(float Score) { SetSpicyLevelScore(Score); }

    public void OnStartMushroomLevel(ECharacter eCharacter) { StartMushroomLevel(eCharacter); }
    public void OnSetMushroomLevelScore(float Score) { SetMushroomLevelScore(Score); }

    //Core -------------------------------------------------------------------s---------------------------
    private void InitFirebase()
    {
        if (PlayerPrefs.HasKey("CurrentPlayerData"))
        {
            currentPlayerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("CurrentPlayerData"));
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase Initialized!");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Exception);
            }
        });
    }

    private void SaveDefaultPlayerData()
    {
        deviceUUID = PlayerPrefs.GetString("customUUID", null);
        if (string.IsNullOrEmpty(deviceUUID))
        {
            deviceUUID = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("customUUID", deviceUUID);
            PlayerPrefs.Save();
        }
        currentPlayerData.Name = PlayerPrefs.GetString("PlayerName");
        currentPlayerData.Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        ResetPlayState(ref currentPlayerData);
        UpdatePlayerData();
    }

    private void UpdatePlayerData()
    {
        currentPlayerData.Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        string PlayerData = JsonUtility.ToJson(currentPlayerData);
        PlayerPrefs.SetString("CurrentPlayerData", PlayerData);
        dbReference.Child("PlayerData").Child(deviceUUID).SetRawJsonValueAsync(PlayerData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Score saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save score: " + task.Exception.Message);
            }
        });
    }

    private void UpdateScoreRate(ScoreRateData kData)
    {
        string strScoreRateData = JsonUtility.ToJson(kData);
        dbReference.Child("ScoreRateData").Push().SetRawJsonValueAsync(strScoreRateData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Score saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save score: " + task.Exception.Message);
            }
        });
    }

    private void UpdatePlayerNameList(string strName)
    {
        dbReference.Child("PlayerNameList").Push().SetValueAsync(strName).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Score saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save score: " + task.Exception.Message);
            }
        });
    }

    private void ResetPlayState(ref PlayerData kPlayerData)
    {
        kPlayerData.KelpLevel.Playing     = false;
        kPlayerData.MushroomLevel.Playing = false;
        kPlayerData.SpicyLevel.Playing    = false;
    }

    private void StartKelpLevel(ECharacter eCharacter)
    {
        ResetPlayState(ref currentPlayerData);
        currentPlayerData.KelpLevel.Icon = (int)eCharacter;
        currentPlayerData.KelpLevel.Playing = true;
        UpdatePlayerData();
    }

    private void SetKelpLevelScore(float FinalTime)
    {
        currentPlayerData.KelpLevel.Playing = false;
        if(FinalTime < currentPlayerData.KelpLevel.FinalTime)
        {
            currentPlayerData.KelpLevel.FinalTime = FinalTime;
        }

        ScoreRateData kData = new ScoreRateData();
        {
            kData.deviceUUID = deviceUUID;
            kData.Name       = currentPlayerData.Name;
            kData.Icon       = currentPlayerData.KelpLevel.Icon;
            kData.FinalTime  = FinalTime;
            kData.GameType   = (int)EGameType.昆布鍋;
            kData.Time       = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            UpdateScoreRate(kData);
        }

        UpdatePlayerData();
    }

    private void StartSpicyLevel(ECharacter eCharacter)
    {
        ResetPlayState(ref currentPlayerData);
        currentPlayerData.SpicyLevel.Icon = (int)eCharacter;
        currentPlayerData.SpicyLevel.Playing = true;
        UpdatePlayerData();
    }

    private void SetSpicyLevelScore(float FinalTime)
    {
        currentPlayerData.SpicyLevel.Playing = false;
        if (FinalTime < currentPlayerData.SpicyLevel.FinalTime)
        {
            currentPlayerData.SpicyLevel.FinalTime = FinalTime;
        }

        ScoreRateData kData = new ScoreRateData();
        {
            kData.deviceUUID = deviceUUID;
            kData.Name       = currentPlayerData.Name;
            kData.Icon       = currentPlayerData.SpicyLevel.Icon;
            kData.FinalTime  = FinalTime;
            kData.GameType   = (int)EGameType.麻辣鍋;
            kData.Time       = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            UpdateScoreRate(kData);
        }

        UpdatePlayerData();
    }

    private void StartMushroomLevel(ECharacter eCharacter)
    {
        ResetPlayState(ref currentPlayerData);
        currentPlayerData.MushroomLevel.Icon = (int)eCharacter;
        currentPlayerData.MushroomLevel.Playing = true;
        UpdatePlayerData();
    }

    private void SetMushroomLevelScore(float FinalTime)
    {
        currentPlayerData.MushroomLevel.Playing = false;
        if (FinalTime < currentPlayerData.MushroomLevel.FinalTime)
        {
            currentPlayerData.MushroomLevel.FinalTime = FinalTime;
        }

        ScoreRateData kData = new ScoreRateData();
        {
            kData.deviceUUID = deviceUUID;
            kData.Name       = currentPlayerData.Name;
            kData.Icon       = currentPlayerData.MushroomLevel.Icon;
            kData.FinalTime  = FinalTime;
            kData.GameType   = (int)EGameType.香菇鍋;
            kData.Time       = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            UpdateScoreRate(kData);
        }

        UpdatePlayerData();
    }

    private void CheckPlayerName(string playerName, Action<bool> callback)
    {
        dbReference.Child("PlayerNameList").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                bool nameExists = false;
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    string existingName = childSnapshot.Value.ToString();
                    if (existingName == playerName)
                    {
                        nameExists = true;
                        break;
                    }
                }
                callback(!nameExists);
            }
            else
            {
                callback(false);
            }
        });
    }

    /*public void GetPlayerScore(string playerId)
    {
        if (dbReference == null)
        {
            Debug.LogError("Firebase not initialized yet.");
            return;
        }

        dbReference.Child("scores").Child(playerId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int playerScore = int.Parse(snapshot.Value.ToString());
                    Debug.Log("Player Score: " + playerScore);
                }
                else
                {
                    Debug.Log("No score found for player: " + playerId);
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve score: " + task.Exception);
            }
        });
    }*/
}
