using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWallController : EnvironmentController
{
    public override bool Interaction()
    {
        if (_opening) return false;
        Open(() =>
        {// 打开后创建墙壁
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 6).transform.position = transform.position;
            // 改变剧情
            if (GameManager.Instance.LevelManager.Level == 23) GameManager.Instance.PlotManager.PlotDictionary[7] += 1;
        });
        _opening = true;
        return false;
    }
}
