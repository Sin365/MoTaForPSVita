using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public UIManager UIManager;
    [HideInInspector]
    public CombatManager CombatManager;
    [HideInInspector]
    public PlayerManager PlayerManager;
    [HideInInspector]
    public SoundManager SoundManager;

    [HideInInspector]
    public ResourceManager ResourceManager;
    [HideInInspector]
    public PoolManager PoolManager;
    [HideInInspector]
    public EventManager EventManager;
    [HideInInspector]
    public LevelManager LevelManager;
    [HideInInspector]
    public BackpackManager BackpackManager;
    [HideInInspector]
    public PlotManager PlotManager;

    private new void Awake()
    {
        Application.targetFrameRate = 30;


		base.Awake();

        // 不继承 mono 的组件
        EventManager = new EventManager();
        ResourceManager = new ResourceManager();
        LevelManager = new LevelManager();
        PoolManager = new PoolManager();
        BackpackManager = new BackpackManager();
        PlayerManager = new PlayerManager();
        PlotManager = new PlotManager();

        // 获取组件
        CombatManager = GetComponent<CombatManager>();
        UIManager = GetComponent<UIManager>();
        SoundManager = GetComponent<SoundManager>();
    }

    private void Start()
    {
        // 绑定输入事件
        EventManager.OnBackHomeInput += BackHomeEvent;
        // 绑定其它事件
        ResourceManager.BindEvent();
        UIManager.BindEvent();
        // 视情况加载或者新建游戏
        if (ResourceManager.GetNewGameStatus()) ResourceManager.NewGameInfo();
        else ResourceManager.LoadGameArchive();
        // 初始化背包 UI
        UIManager.InitBackpackUI();
    }

    private void Update()
    {
        PlayerManager.CheckInput();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerManager.PlayerInfo.Health += 10000;
            PlayerManager.PlayerInfo.Attack += 1000;
            PlayerManager.PlayerInfo.Defence += 1000;
            PlayerManager.PlayerInfo.Gold += 1000;
            BackpackManager.PickUp(PoolManager.GetResourceInFreePool(EResourceType.Item, 1).GetComponent<ItemController>());
            BackpackManager.PickUp(PoolManager.GetResourceInFreePool(EResourceType.Item, 2).GetComponent<ItemController>());
            BackpackManager.PickUp(PoolManager.GetResourceInFreePool(EResourceType.Item, 3).GetComponent<ItemController>());
            BackpackManager.PickUp(PoolManager.GetResourceInFreePool(EResourceType.Item, 9).GetComponent<ItemController>());
            BackpackManager.PickUp(PoolManager.GetResourceInFreePool(EResourceType.Item, 11).GetComponent<ItemController>());
        }
    }

    /// <summary>
    /// 回到首页
    /// </summary>
    public void BackHomeEvent()
    {
        SceneManager.LoadScene("Home");
    }
}
