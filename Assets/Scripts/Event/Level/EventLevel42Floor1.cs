using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel42Floor1 : MonoBehaviour
{
    private EnemyController _enemy1;  // 骑士队长
    private EnemyController _enemy2;  // 魔王
    private EnemyController _enemy3;  // 魔法警卫
    private EnemyController _enemy4;
    private EnemyController _enemy5;
    private EnemyController _enemy6;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 锁定音频播放
            GameManager.Instance.SoundManager.LockEnable = true;
            // 禁用人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 锁定人物控制器
            GameManager.Instance.PlayerManager.LockEnable = true;
            // 开始对话
            ShowTalk();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    /// <summary>
    /// 说话
    /// </summary>
    private void ShowTalk()
    {
        // 骑士队长说话
        GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "啊？又是你！！！（逃跑）" }, (() =>
        {
            StartCoroutine(GoOut());
        }));
    }

    IEnumerator GoOut()
    {
        // 移动
        yield return StartCoroutine(Move(_enemy1.transform, new List<Vector2> { (Vector2)_enemy1.transform.position + Vector2.up * 2 }, 20));
        // 魔王出现
        _enemy2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 33).GetComponent<EnemyController>();
        _enemy2.transform.position = Vector2.zero;
        GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "你敢临阵脱逃？" }, (() =>
        {
            GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "我的王，我打不过这个二五仔，饶了我吧！" }, (() =>
            {
                GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "你再说一次？", "魔塔不需要你这样的败类！", "来人呐，给我处决他。" }, (() =>
                  {
                      // 创建魔法警卫
                      _enemy3 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy3.transform.position = (Vector2)_enemy1.transform.position + Vector2.up;
                      _enemy4 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy4.transform.position = (Vector2)_enemy1.transform.position + Vector2.down;
                      _enemy5 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy5.transform.position = (Vector2)_enemy1.transform.position + Vector2.left;
                      _enemy6 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy6.transform.position = (Vector2)_enemy1.transform.position + Vector2.right;
                      GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "我的王，再给我一次机会……" }, (() =>
                      {
                          // 显示特效
                          GameObject obj1 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj1.transform.position = _enemy1.transform.position;
                          obj1.GetComponent<MagicController>().ShowMagic(EDirectionType.UP);
                          GameObject obj2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj2.transform.position = _enemy1.transform.position;
                          obj2.GetComponent<MagicController>().ShowMagic(EDirectionType.DOWN);
                          GameObject obj3 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj3.transform.position = _enemy1.transform.position;
                          obj3.GetComponent<MagicController>().ShowMagic(EDirectionType.LEFT);
                          GameObject obj4 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj4.transform.position = _enemy1.transform.position;
                          obj4.GetComponent<MagicController>().ShowMagic(EDirectionType.RIGHT);
                          // 骑士队长死亡
                          StartCoroutine(KnightCaptainDeath());
                      }));
                  }));
            }));
        }));
        yield break;
    }

    IEnumerator KnightCaptainDeath()
    {
        yield return new WaitForSeconds(1);
        // 回收剧情资源
        GameManager.Instance.PoolManager.RecycleResource(_enemy1.gameObject);
        // 魔王说话
        GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "刚刚只是教训手下，放心，和你决斗我是不会以多欺少的。", "再见！" }, (() =>
        {
            // 回收剧情资源
            GameManager.Instance.PoolManager.RecycleResource(_enemy2.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy3.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy4.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy5.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy6.gameObject);
            // 回收剧情资源
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
            // 解锁人物控制器
            GameManager.Instance.PlayerManager.LockEnable = false;
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 解锁音频播放
            GameManager.Instance.SoundManager.LockEnable = false;
        }));
        yield break;
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

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void GetGameObjectEvent()
    {
        _enemy1 = null;
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(0, -4)) _enemy1 = obj.GetComponent<EnemyController>();
        });
    }
}
