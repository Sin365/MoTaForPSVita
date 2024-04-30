using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel50Actor1 : ActorController
{
    public override bool Interaction()
    {
        // 锁定音乐
        GameManager.Instance.SoundManager.LockEnable = true;
        // 勇士说话
        GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "你怎么会在这里？你到底是谁！" }, () =>
        {
            // 小偷说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "理由就是……" }, () =>
            {
                StartCoroutine(Hide());
            });
        });
        return false;
    }


    /// <summary>
    /// 移动 2
    /// </summary>
    IEnumerator Hide()
    {
        // 小偷消失
        _animator.SetTrigger("hide");
        // 生成魔王
        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 34).transform.position = transform.position;
        yield break;
    }

}
