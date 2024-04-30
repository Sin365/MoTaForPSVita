using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel33Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "别着急，放慢速度。";
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
