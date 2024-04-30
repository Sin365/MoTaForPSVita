using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel31Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "双手剑士攻击力太高了，你最好能够一击必杀时再与他交手。";
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
