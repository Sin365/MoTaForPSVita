using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShopComm;

public class EventManager : Singleton<EventManager>
{
    /// <summary>
    /// 移动输入事件
    /// </summary>
    public Action<EDirectionType> OnMoveInput;

    /// <summary>
    /// 玩家移动完成事件
    /// </summary>
    public Action<Vector2> OnPlayerArrive;

    /// <summary>
    /// 保存游戏输入事件
    /// </summary>
    public Action OnSaveGameInput;
    /// <summary>
    /// 回到菜单输入事件
    /// </summary>
    public Action OnBackHomeInput;

    /// <summary>
    /// 玩家生命值变动事件
    /// </summary>
    public Action<int> OnHealthChanged;
    /// <summary>
    /// 玩家攻击力变动事件
    /// </summary>
    public Action<int> OnAttackChanged;
    /// <summary>
    /// 玩家防御力变动事件
    /// </summary>
    public Action<int> OnDefenceChanged;
    /// <summary>
    /// 玩家金钱变动事件
    /// </summary>
    public Action<int> OnGoldChanged;
    /// <summary>
    /// 玩家武器变动事件
    /// </summary>
    public Action<int> OnWeaponChanged;
    /// <summary>
    /// 玩家防具变动事件
    /// </summary>
    public Action<int> OnArmorChanged;

    /// <summary>
    /// 笔记本变动事件
    /// </summary>
    public Action<string> OnNotepadChanged;

    /// <summary>
    /// 背包物品变动事件
    /// </summary>
    public Action<int, ItemInfo> OnItemChanged;

    /// <summary>
    /// 对战敌人改变事件 用于更新 UI
    /// </summary>
    public Action<EnemyController> OnEnemyCombated;

    /// <summary>
    /// 关卡改变事件 参数 1 是旧值 参数 2 是新值
    /// </summary>
    public Action<int, int> OnLevelChanged;

    /// <summary>
    /// 资源加载完成事件
    /// </summary>
    public Action OnResourceLoaded;

	/// <summary>
	/// 打开商店事件
	/// </summary>
	public Action<string, E_ShopFloor, ActorController,Action, Action,Action> OnShopShow;
	//public Action<string, int, Action> OnShopShow;

	/// <summary>
	/// 法老权杖上楼事件
	/// </summary>
	public Action OnArtifactUp;
    /// <summary>
    /// 法老权杖下楼事件
    /// </summary>
    public Action OnArtifactDown;

    /// <summary>
    /// 吸血鬼出现事件
    /// </summary>
    public Action OnVampireShow;

    public void RemoveAllEvent(Action action)
    {
        if (null != action)
        {
            Delegate[] ds = action?.GetInvocationList();
            for (int i = 0; i < ds.Length; i++)
            {
                action -= ds[i] as Action;
            }
        }
    }
}
