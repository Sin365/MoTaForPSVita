using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireController : EnemyController
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnVampireShow?.Invoke();
           OnDeath += () =>
        {
            // 说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "噢，上帝！我做梦也没想到自己会输给一个人类。", "虽然你获得了暂时的胜利，但对于大法师来说你还是太弱了。" }, () =>
            {
                // 死亡时创建物品
                Vector2 point = new Vector2();
                // 生成大血瓶
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-1 + i, -2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = point;
                }
                // 生成红宝石
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-2, 1 - i);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = point;
                }
                // 生成蓝宝石
                for (int i = 0; i < 3; i++)
                {
                    point.Set(2, 1 - i);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = point;
                }
                // 生成黄钥匙
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-1 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = point;
                }
                // 改变剧情状态
                GameManager.Instance.PlotManager.PlotDictionary[15] = 2;
                // 打开人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // 解锁音频播放
                GameManager.Instance.SoundManager.LockEnable = false;
                // 音频播放
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
            });
        };
    }
}
