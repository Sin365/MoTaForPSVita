using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel10Floor2 : MonoBehaviour
{
    private GameObject _npc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 状态一触发对话
        if (GameManager.Instance.PlotManager.PlotDictionary[4] == 5)
        {
            if (collision.CompareTag("Player"))
            {
                // 禁用人物控制器
                GameManager.Instance.PlayerManager.Enable = false;
                // 小偷出现
                StartCoroutine(ShowNPC());
            }
        }
    }

    IEnumerator ShowNPC()
    {
        // 创建小偷
        _npc = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Actor, 12);
        _npc.transform.position = new Vector2(-5, -5);
        _npc.GetComponent<Animator>().SetTrigger("show");
        yield return new WaitForSeconds(1 / 3);
        // 小偷移动
        Vector2[] points = new Vector2[]
        {
            new Vector2(-5,-5),
            new Vector2(-5,-2),
            new Vector2(-3,-2),
            new Vector2(-3,-5),
            new Vector2(-1,-5),
            new Vector2(-1,-4),
            new Vector2(0,-4),
        };
        yield return StartCoroutine(Move(_npc.transform, points.ToList()));
        // 小偷长说话
        GameManager.Instance.UIManager.ShowDialog(_npc.GetComponent<ResourceController>().Name, new List<string> { "嘿！我们又见面了！", "你竟然击败了此区域的头目，真了不起！", "我正在发愁怎么去更高的楼层，现在终于可以上去了。", "听说银盾在 11 楼，银剑在 17 楼，祝你好运~" }, () =>
        {
            // 小偷消失
            StartCoroutine(HideNPC());
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

    IEnumerator HideNPC()
    {
        yield return StartCoroutine(Move(_npc.transform, new List<Vector2> { new Vector2(0, -5) }));
        _npc.GetComponent<Animator>().SetTrigger("hide");
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
        // 资源回收
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        yield break;
    }
}
