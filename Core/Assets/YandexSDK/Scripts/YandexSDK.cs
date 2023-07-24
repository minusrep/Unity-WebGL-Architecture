using System.Runtime.InteropServices;
using UnityEngine;

namespace Yandex
{
    public class YandexSDK
    {
        [DllImport("__Internal")]
        private static extern void SaveDataExtern(string data);
        [DllImport("__Internal")]
        private static extern void LoadDataExtern();
        [DllImport("__Internal")]
        private static extern string GetLanguageExtern();
        [DllImport("__Internal")]
        private static extern void ShowFullscreenAdvExtern();
        [DllImport("__Internal")]
        private static extern void ShowRewardedAdvExtern();
        [DllImport("__Internal")]
        private static extern void RateGameExtern();
        [DllImport("__Internal")]
        private static extern void UpdateLeaderboardExtern(int value);

        public void RateGame()
        {
#if UNITY_EDITOR

#else
        RateGameExtern();
#endif
        }

        public void SaveData(Data data)
        {
#if UNITY_EDITOR
            Debug.Log($"YandexSDK: Save data.");

#else
            string json = JsonUtility.ToJson(data);
            SaveDataExtern(json);
#endif
        }

        public void LoadData() => LoadDataExtern();

        public void UpdateLeaderboard(int value)
        {
#if UNITY_EDITOR
            Debug.Log($"YandexSDK: Update leaderboard {value}.");
#else
            UpdateLeaderboardExtern(value);
#endif
        }

        public void ShowFullscreenAdv() 
        {
#if UNITY_EDITOR
            Debug.Log($"YandexSDK: Show fullscreen adv");

#else
            ShowFullscreenAdvExtern();
#endif
        }
        public void ShowRewardedAdv() 
        {
#if UNITY_EDITOR
            Debug.Log($"YandexSDK: Show rewarded adv");
#else
        ShowRewardedAdvExtern();
#endif
        }
        public string GetLanguage() => GetLanguageExtern();
    }

}
