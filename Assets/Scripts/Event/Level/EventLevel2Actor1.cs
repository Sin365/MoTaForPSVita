using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[1])
        {
            // 状态 1 未逃出牢笼
            case 1:
                // 小偷说话
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "你醒了？", "你被魔法警卫关进来时还处于昏迷中。", "我刚刚挖了一条暗道，咱们一起逃出去吧。" }, () =>
                {
                    // 打开墙
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-1, 0), Vector2.left, .1f);
                    hit.collider.GetComponent<EnvironmentController>().Open(() =>
                    {
                        // 移动
                        StartCoroutine(Moving1());
                    });
                });
                break;
            case 2:
                // 小偷说话
                GameManager.Instance.UIManager.ShowDialog("小偷", new List<string> { "终于逃出来了！", "你的装备被警卫拿走了，建议你先找点趁手的装备。", "铁剑在 5 楼，铁盾在 9 楼。", "我还有事先走了，祝你好运~" }, () =>
                {
                    // 移动
                    StartCoroutine(Moving2());
                });
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// 移动 1
    /// </summary>
    IEnumerator Moving1()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-5, -1), new Vector2(-5, -3) }));
        // 变更剧情状态
        GameManager.Instance.PlotManager.PlotDictionary[1] = 2;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        yield break;
    }

    /// <summary>
    /// 移动 2
    /// </summary>
    IEnumerator Moving2()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-5, -5) }));
        // 小偷消失
        _animator.SetTrigger("hide");
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
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
