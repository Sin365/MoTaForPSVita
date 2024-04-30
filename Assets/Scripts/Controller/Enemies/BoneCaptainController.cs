using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCaptainController : EnemyController, IInteraction, IExplosionproof
{
    private void OnEnable()
    {
        OnDeath += () =>
        {
            // 关闭人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 锁定人物移动
            GameManager.Instance.PlayerManager.LockEnable = true;
            // 说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "不，不可能！！！", "我怎么会输？", "你别得意，后面还有许多强大的对手，你必死无疑！" }, () =>
            {
                // 开门
                EnvironmentController ec = null;
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if (obj.GetComponent<MagicDoorController>() != null && (Vector2)obj.transform.position == new Vector2(0, -1)) ec = obj.GetComponent<EnvironmentController>();
                });
                ec.Open(null);
                // 死亡时创建物品
                Vector2 point = new Vector2();
                // 生成大血瓶
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-5 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = point;
                }
                // 生成红宝石
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-5 + i, 3);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = point;
                }
                // 生成蓝宝石
                for (int i = 0; i < 3; i++)
                {
                    point.Set(3 + i, 3);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = point;
                }
                // 生成黄钥匙
                for (int i = 0; i < 3; i++)
                {
                    point.Set(3 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = point;
                }
                // 生成楼梯
                point.Set(0, -5);
                GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 7).transform.position = point;
                // 生成剧情地板
                point.Set(0, -3);
                GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 14).transform.position = point;
                // 解锁人物移动
                GameManager.Instance.PlayerManager.LockEnable = false;
                // 打开人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // 解锁音乐
                GameManager.Instance.SoundManager.LockEnable = false;
                // 音频播放
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
                // 变更剧情状态
                GameManager.Instance.PlotManager.PlotDictionary[4] = 5;
            });
        };
    }

    public bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[4])
        {
            // 状态 1 对话
            case 3:
                // 商人说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "竟然能逃出我设的陷阱？", "我是不会让你过去的，来决一死战吧！" }, () =>
                {
                    // 打开人物控制器
                    GameManager.Instance.PlayerManager.Enable = true;
                    // 变更剧情状态
                    GameManager.Instance.PlotManager.PlotDictionary[4] = 4;
                });
                break;
            case 4:
                return true;

        }
        return false;
    }
}
