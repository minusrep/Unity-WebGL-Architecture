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
        private static extern void RateGameExtern();


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
#else
            string json = JsonUtility.ToJson(data);
            SaveDataExtern(json);
#endif
        }

        public void LoadData() => LoadDataExtern();
    }

}
