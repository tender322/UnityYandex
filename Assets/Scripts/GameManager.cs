using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public Text NameText;
    public Text CoinText;
    private YandexSDK SDK;


    public GameObject ScrollView;
    public GameObject _fab;

    [SerializeField] private int coins;

    private void Start()
    {
        SDK = YandexSDK.Instance;
        SDK.AuthSuccess += SettingMame;
        SDK.RewardGet += Rewarded;
        SDK.DataGet += SettingData;
        SDK.LeaderBoardReady += AddEntri;
        UpdateCoinsText();
    }
    

    public void UpdateCoinsText()
    {
        CoinText.text = coins.ToString();
    }
    public void Auth()
    {
        SDK.Authenticate();
    }
    public void GetData()
    {
        SDK.GettingData();
    }
    public void SetData()
    {
        UserGameData UD = new UserGameData(coins);
        SDK.SettingData(JsonUtility.ToJson(UD));
    }
    public void ShowCommon()
    {
        SDK.ShowCommonAdvertisment();
    }
    public void ShowReward()
    {
        SDK.ShowRewardAdvertisment();
    }
    
    public void GetLeaderBoardEntries() => SDK.getLeaderEntries();
    public void SetLeaderBoard() => SDK.setLeaderScore(coins);
    
    public void AddCoin()
    {
        coins++;
        UpdateCoinsText();
        SetLeaderBoard();
    }
    private void SettingData()
    {
        coins = SDK.GetUserGameData.Coin;
        UpdateCoinsText();
    }
    private void SettingMame()
    {
        NameText.text = SDK.GetUserData.Name;
    }
    private void Rewarded()
    {
        coins++;
        UpdateCoinsText();
    }
    
    private void AddEntri(string _json)
    {
        ClearEntri();
        var json = JSON.Parse(_json);
        var _count = (int)json["entries"].Count;
        string url = "https://api.icons8.com/download/52b8de2a7f8ac69966499fad5fd564202f9b130f/iOS7/PNG/512/Logos/desura_filled-512.png";

        for (int i = 0; i < _count; i++)
        {
            var _entries = Instantiate(_fab, ScrollView.transform);
            var raw = _entries.transform.GetChild(0).GetComponent<RawImage>();
            StartCoroutine(LoadIMG(url, raw));
            _entries.transform.GetChild(1).GetComponent<Text>().text = json["entries"][i]["player"]["publicName"];
            _entries.transform.GetChild(2).GetComponent<Text>().text = json["entries"][i]["score"].ToString();
        }
    }
    private void ClearEntri()
    {
        if (ScrollView.transform.childCount > 0)
        {
            foreach (Transform child in ScrollView.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator LoadIMG(string _url,RawImage _img)
    {
        WWW www = new WWW(_url);
        yield return www;
        _img.texture = www.texture;
    }
}
