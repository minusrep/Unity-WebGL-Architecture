using System.Collections;
using UnityEngine;
using Core;
using System;
using Yandex;

public sealed class GameManager : MonoBehaviour
{
    public static event Action OnControllersCreatedEvent;
    public static event Action OnGameLoadedEvent;
    public static GameManager instance;

    private Game game;
    private Data data;
    private YandexSDK yandexSDK;
    

    public static T GetController<T>() where T : Controller => instance.game.GetController<T>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            Debug.Log($"{this.gameObject.name}: Сreated.");
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
        this.yandexSDK = new YandexSDK();
        this.yandexSDK.LoadData();
        yield return new WaitUntil(() => this.data != null);

        this.game = new Game();
        OnControllersCreatedEvent?.Invoke();
        Debug.Log($"{this.gameObject.name}: Initializing started.");
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

    public void RateGame() => this.yandexSDK.RateGame();



}
