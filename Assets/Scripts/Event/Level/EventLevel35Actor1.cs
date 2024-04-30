using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel35Actor1 : ActorController
{
    public override bool Interaction()
    {
        // 小偷说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "暗道挖好了，你可以绕过魔龙了。", "我听说骑士队长实力差又爱吹牛，所以被魔法警卫们讨厌。", "魔塔太危险了，我可不想再次被抓，我要走了，再见！" }, () =>
        {
            StartCoroutine(GoOut());
        });
        return false;
    }

    IEnumerator GoOut()
    {
        // 移动
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-1, -5), new Vector2(0, -5) }));
        // 打开人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "31-39");
        // NPC 回收
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        yield break;
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints)
    {
        float timer = 0;
        float speed = 10f;
        Vector2 beginPoint = transform.position;
        for (int i = 0; i < targetPoints.Count; i++)
        {
            timer = 0;
            beginPoint = transform.position;
            while ((Vector2)transform.position != targetPoints[i])
            {
                timer += Time.deltaTime;
                float t = timer / (beginPoint - targetPoints[i]).sqrMagnitude * speed;
                transform.position = Vector2.Lerp(beginPoint, targetPoints[i], t);
                yield return null;
            }
        }
        yield break;
    }
}
