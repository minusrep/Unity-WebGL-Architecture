using System.Collections;
using UnityEngine;
using Core;
using System;
using Yandex;

public sealed class GameManager : MonoBehaviour
{
    public static event Action OnControllersCreatedEvent;
    public static event Action OnGameLoadedEvent;


    private static GameManager instance;

    private Game game;
    private Data data;
    private YandexSDK yandexSDK;


    public static T GetController<T>() where T : Controller => instance.game.GetController<T>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log($"{this.gameObject.name}: Сreated.");
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start() => this.Initialize();

    private void Initialize() => this.StartCoroutine(this.InitializeRoutine());

    private IEnumerator InitializeRoutine()
    {
        Debug.Log($"{this.gameObject.name}: Initializing started.");
        this.yandexSDK = new YandexSDK();
        var language = "ru";
#if UNITY_EDITOR
        this.data = new Data();
#else
        this.yandexSDK.LoadData();
        var language = this.yandexSDK.GetLanguage();
#endif
        yield return new WaitUntil(() => this.data != null);

        this.game = new Game(this.data);
        OnControllersCreatedEvent?.Invoke();
        yield return new WaitUntil(() => this.game.isInitialized);
        OnGameLoadedEvent?.Invoke();
        Debug.Log($"{this.gameObject.name}: Initialized.");

    }

    public void SetData(string value)
    {
        var data = JsonUtility.FromJson<Data>(value);
        this.data = data;
        Debug.Log($"{this.gameObject.name}: Data loaded.");
    }

    public void OnAdvRewarded()
    {
        Debug.Log($"{this.gameObject.name}: Adv rewarded.");
    }

    public void RateGame() => this.yandexSDK.RateGame();
}
