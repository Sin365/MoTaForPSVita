using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel29Actor1 : ActorController
{
    public override bool Interaction()
    {
        if (GameManager.Instance.PlotManager.PlotDictionary[7] != 43)
        {
            // 小偷说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "你先去其它地方走走，我还在挖暗道。" }, () =>
            {
                // 启用人物控制器
                GameManager.Instance.PlayerManager.Enable = true;
                // 音频播放
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
            });
        }
        else
        {
            // 小偷说话
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "我刚完成暗道，你每次都及时赶到。", "看在朋友的份上，你可以免费使用。", "我先走了，下次再见~" }, () =>
            {
                // 打开墙壁
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if (obj.GetComponent<EnvironmentController>() != null && (Vector2)obj.transform.position == new Vector2(transform.position.x, transform.position.y - 1)) obj.GetComponent<EnvironmentController>().Open(() =>
                    {
                        // 移动
                        StartCoroutine(Moving());
                    });
                });
            });
        }
        return false;
    }

    /// <summary>
    /// 移动
    /// </summary>
    IEnumerator Moving()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(0, -5) }));
        // 变更剧情状态
        GameManager.Instance.PlotManager.PlotDictionary[7] = 44;
        // 小偷消失
        _animator.SetTrigger("hide");
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
        yield break;
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints)
    {
        float timer = 0;
        float speed = 30f;
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
