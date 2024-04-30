using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 环境类型
/// </summary>
public enum EEnvironmentType
{
    None,
    Wall,
    Door,
    Stairs,
}

public class EnvironmentController : ResourceController, IInteraction
{
    public EEnvironmentType type = EEnvironmentType.None;  // 环境类型

    public bool HasDirection = false;  // 是否有开门方向
    public EDirectionType OpenDirection;  // 开门方向
    public int KeyId;  // 钥匙 id

    public bool isUp = false;  // 是否上楼

    public Action OnOpened;  // 当墙壁打开时

    protected Animator _animator;

    protected bool _opening = false;  // 打开状态

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void OnEnable()
    {
        _opening = false;
    }
    protected void OnDisable()
    {
        OnOpened = null;
    }

    public virtual bool Interaction()
    {
        if (_opening) return false;
        switch (type)
        {
            case EEnvironmentType.None:
                return true;
            case EEnvironmentType.Wall:
                return false;
            case EEnvironmentType.Door:
                // 是否能够开门
                bool canOpen = false;
                // 钥匙判断
                if (KeyId != 0)
                {
                    if (GameManager.Instance.BackpackManager.ConsumeItem(KeyId))
                    {
                        canOpen = true;
                        GameManager.Instance.UIManager.ShowInfo($"打开 {Name} 消耗 {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, KeyId).Name} 1 个。");
                    }
                    else return false;
                }
                // 如果设置开门方向
                if (HasDirection)
                {
                    // 计算开门方向
                    Vector2 direction = GameManager.Instance.PlayerManager.PlayerController.transform.position - transform.position;
                    switch (OpenDirection)
                    {
                        case EDirectionType.UP:
                            if (direction.y == 1) canOpen = true;
                            break;
                        case EDirectionType.DOWN:
                            if (direction.y == -1) canOpen = true;
                            break;
                        case EDirectionType.LEFT:
                            if (direction.x == -1) canOpen = true;
                            break;
                        case EDirectionType.RIGHT:
                            if (direction.x == 1) canOpen = true;
                            break;
                    }
                }
                else canOpen = true;
                if (canOpen)
                {
                    Open(null);
                    _opening = canOpen;
                }
                return false;
            case EEnvironmentType.Stairs:
                // 获取下一层序号
                int nextIndex = isUp ? GameManager.Instance.LevelManager.Level + 1 : GameManager.Instance.LevelManager.Level - 1;
                // 44 层跳跃
                int addNumber = nextIndex == 44 ? 2 : 1;
                nextIndex = isUp ? GameManager.Instance.LevelManager.Level + addNumber : GameManager.Instance.LevelManager.Level - addNumber;
                // 获取下一层信息
                LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
                // 修改人物下一层位置用于传送
                GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, isUp ? nextLevelInfo.DownStairPoint : nextLevelInfo.UpStairPoint);
                // 传送到下一层
                GameManager.Instance.LevelManager.Level = nextIndex;
                return false;
        }
        return false;
    }

    /// <summary>
    /// 打开
    /// </summary>
    public void Open(Action callback)
    {
        // 播放动画
        _animator.SetTrigger("open");
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "OpenTheDoor");
        // 添加打开墙壁回调
        OnOpened += callback;
    }

    public void RecycleSelf()
    {
        // 执行打开回调
        OnOpened?.Invoke();
        // 回收资源
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }
}
