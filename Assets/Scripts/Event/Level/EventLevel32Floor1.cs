using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel32Floor1 : MonoBehaviour
{
    GameObject _obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 锁定音频播放
            GameManager.Instance.SoundManager.LockEnable = true;
            // 禁用人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 骑士首领出现
            StartCoroutine(TriggerTrap());
        }
    }

    IEnumerator TriggerTrap()
    {
        // 生成骑士队长
        _obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        _obj.transform.position = new Vector2(5, 5);
        // 骑士队长移动
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { new Vector2(0, 5) }));
        // 对话
        GameManager.Instance.UIManager.ShowDialog(_obj.GetComponent<ResourceController>().Name, new List<string> { "不错嘛，竟然打败了两个头目。", "但是现在游戏结束了，我将亲手杀死你！" }, () =>
        {
            StartCoroutine(AttackPlayer());
        });
        yield break;
    }

    IEnumerator AttackPlayer()
    {
        // 锁定人物移动
        GameManager.Instance.PlayerManager.LockEnable = true;
        _obj.GetComponent<EnemyController>().OnDeath += () =>
        {
            // 骑士队长说话
            GameManager.Instance.UIManager.ShowDialog(_obj.GetComponent<ResourceController>().Name, new List<string> { "你以为你非常强大吗？", "我只是今天状态不佳而已。", "有本事到 40 层我们再战。" }, (() =>
            {
                StartCoroutine(RunAway());
            }));
        };
        // 骑士队长移动
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { transform.position }, 40));
    }

    IEnumerator RunAway()
    {
        _obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        _obj.transform.position = (Vector2)transform.position + Vector2.up;
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { new Vector2(0, 5), new Vector2(5, 5) }, 40));
        // 回收物体
        GameManager.Instance.PoolManager.RecycleResource(_obj);
        // 解锁音频播放
        GameManager.Instance.SoundManager.LockEnable = false;
        // 解除锁定人物移动
        GameManager.Instance.PlayerManager.LockEnable = false;
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 回收物体
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints, float speed = 20f)
    {
        float timer = 0;
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
