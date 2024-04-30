using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel37Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "你需要用 地震卷轴 取出 37 层内的宝物。";
        // 老头说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
        {
            // 记笔记
            GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "31-39");
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
