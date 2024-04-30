using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallController3 : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += OnLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= OnLoaded;
    }

    private void OnLoaded()
    {
        // 人物到达 35 层 剧情状态 1
        if (GameManager.Instance.PlotManager.PlotDictionary[9] == 1)
        {
            // 生成 2 楼小偷
            GameManager.Instance.ResourceManager.MakeResourceForLevel(2, EResourceType.Actor, 30, new Vector2(4, -5));
            // 改变剧情状态
            GameManager.Instance.PlotManager.PlotDictionary[9] = 2;
        }
        else if (GameManager.Instance.PlotManager.PlotDictionary[9] == 2)
        {
            return;
        }
        else
        {
            // 回收资源
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
