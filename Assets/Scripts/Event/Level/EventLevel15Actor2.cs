using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel15Actor2 : ActorController
{
    public override bool Interaction()
    {
        // 禁用人物控制器
        GameManager.Instance.PlayerManager.Enable = false;
        // 小偷长说话
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "哎呦~又是你。", "这大章鱼挡住了我的去路，我挖了一条暗道，你最好也躲着点。", "我要走了，拜拜~" }, () =>
        {
            // 小偷消失
            StartCoroutine(HideNPC());
        });
        return false;
    }

    IEnumerator HideNPC()
    {
        // 打开墙
        yield return StartCoroutine(OpenWall());
        yield return new WaitForSeconds(1 / 3);
        // 移动
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(0, 5) }));
        GetComponent<Animator>().SetTrigger("hide");
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
        // 资源回收
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        yield break;
    }

    IEnumerator OpenWall()
    {
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(transform.position.x - 1, transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null)
                {
                    obj.GetComponent<EnvironmentController>().Open(null);
                }
            }
        });
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
