using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor2 : ActorController
{
    public override bool Interaction()
    {
        // 小偷说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "哈哈，我们真是有缘。", "谢谢你救了我。", "我会在魔龙旁边开一条暗道，去 35 层找我吧。" }, () =>
        {
            // 生成 35 楼小偷
            GameManager.Instance.ResourceManager.MakeResourceForLevel(35, EResourceType.Actor, 31, new Vector2(-1, -4));
            // 改变剧情
            GameManager.Instance.PlotManager.PlotDictionary[9] = 3;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
