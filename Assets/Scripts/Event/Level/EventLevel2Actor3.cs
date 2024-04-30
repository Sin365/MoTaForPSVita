using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor3 : ActorController
{
    public override bool Interaction()
    {
        // 老头说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "感谢你救了我，这 1000 金币务必收下。" }, () =>
        {
            // 获得金币
            GameManager.Instance.PlayerManager.PlayerInfo.Gold += 1000;
            // 提示信息
            GameManager.Instance.UIManager.ShowInfo("获得 1000 金币。");
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
