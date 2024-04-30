using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel26Actor1 : ActorController
{
    public override bool Interaction()
    {
        // 禁用人物控制器
        GameManager.Instance.PlayerManager.Enable = false;
        // 锁定音乐
        GameManager.Instance.SoundManager.LockEnable = true;
        // 开始对话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "被选召的孩子，这就是你的命运。如果你不惧怕死亡，你将穿越时空来到我这里！" }, () =>
        {
            // 说话
            GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "什么？这是个洋娃娃？" }, () =>
            {
                // 启用人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // 解锁音乐
                GameManager.Instance.SoundManager.LockEnable = false;
                if (GameManager.Instance.PlotManager.PlotDictionary[18] == 1)
                {
                    // 改变剧情
                    GameManager.Instance.PlotManager.PlotDictionary[18] = 2;
                }
            });
        });
        return false;
    }
}
