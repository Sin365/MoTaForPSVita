using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMasterController : ExplosionproofController
{
    private void OnEnable()
    {
        OnDeath += DeathEvent;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        OnDeath -= DeathEvent;
    }

    private void DeathEvent()
    {
        // 生成红钥匙
        for (int i = 0; i < 5; i++)
        {
            if (i == 2) continue;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 3).transform.position = new Vector2(-2 + i, -2);
        }
        // 改变剧情
        GameManager.Instance.PlotManager.PlotDictionary[17] = 2;
        // 解锁音乐
        GameManager.Instance.SoundManager.LockEnable = false;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
    }
}
