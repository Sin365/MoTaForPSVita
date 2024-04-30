using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel4Actor2 : ActorController
{
    public override bool Interaction()
    {
        string talk = "有些门无法用钥匙打开，但是击败守卫可以自动打开。";
        // 老头说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
        {
            // 记笔记
            GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
            // 打开人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // NPC 回收
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
