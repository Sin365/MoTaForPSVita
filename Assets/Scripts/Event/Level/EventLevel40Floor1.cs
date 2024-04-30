using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel40Floor1 : MonoBehaviour
{
    private EnemyController _enemy1;  // 鬼战士
    private EnemyController _enemy2;
    private EnemyController _enemy3;
    private EnemyController _enemy4;  // 战士
    private EnemyController _enemy5;
    private EnemyController _enemy6;
    private EnemyController _enemy7;  // 剑士
    private EnemyController _enemy8;
    private EnemyController _enemy9;
    private EnemyController _enemy10;  // 骑士
    private EnemyController _enemy11;
    private EnemyController _enemy12;
    private EnemyController _enemy13;  // 骑士队长

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 禁用人物控制器
            GameManager.Instance.PlayerManager.Enable = false;
            // 锁定人物控制器
            GameManager.Instance.PlayerManager.LockEnable = true;
            // 锁定音频播放
            GameManager.Instance.SoundManager.LockEnable = true;
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
        // 骷髅队长说话
        GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "我还在担心你来不到这一层，看来我低估你了。", "这次一定能打败你。", "鬼战士，给我上！" }, (() =>
        {
            Attack();
        }));
    }

    private void Attack()
    {
        // 死亡事件
        _enemy1.OnDeath += () =>
        {
            _enemy2.OnDeath += () =>
            {
                _enemy3.OnDeath += () =>
                {
                    // 骷髅队长说话
                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "哼，没关系，战士们，给我上！" }, (() =>
                    {
                        // 死亡事件
                        _enemy4.OnDeath += () =>
                        {
                            _enemy5.OnDeath += () =>
                            {
                                _enemy6.OnDeath += () =>
                                {
                                    // 骷髅队长说话
                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "真正的战斗才刚刚开始！剑士们，给我上！" }, (() =>
                                    {
                                        // 死亡事件
                                        _enemy7.OnDeath += () =>
                                        {
                                            _enemy8.OnDeath += () =>
                                            {
                                                _enemy9.OnDeath += () =>
                                                {
                                                    // 骷髅队长说话
                                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "你，你怎么打得过的？", "骑士们，给我冲！" }, (() =>
                                                    {
                                                        // 死亡事件
                                                        _enemy10.OnDeath += () =>
                                                        {
                                                            _enemy11.OnDeath += () =>
                                                            {
                                                                _enemy12.OnDeath += () =>
                                                                {
                                                                    // 骷髅队长说话
                                                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "你是怎么击败我的手下的！？", "我和你势不两立！（失去理智）" }, (() =>
                                                                    {
                                                                        _enemy13.OnDeath += () =>
                                                                        {
                                                                            // 骷髅队长说话
                                                                            GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "这次先饶了你，下次再和你正式决斗，你最好投降！（逃跑）" }, (() =>
                                                                            {
                                                                                // 播放动画协程
                                                                                StartCoroutine(GoOut());
                                                                            }));
                                                                        };
                                                                        StartCoroutine(Move(_enemy13.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }, 30));
                                                                    }));
                                                                };
                                                                StartCoroutine(Move(_enemy12.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                            };
                                                            StartCoroutine(Move(_enemy11.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                        };
                                                        // 移动
                                                        StartCoroutine(Move(_enemy10.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                    }));
                                                };
                                                StartCoroutine(Move(_enemy9.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                            };
                                            StartCoroutine(Move(_enemy8.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                        };
                                        // 移动
                                        StartCoroutine(Move(_enemy7.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                    }));
                                };
                                StartCoroutine(Move(_enemy6.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                            };
                            StartCoroutine(Move(_enemy5.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                        };
                        // 移动
                        StartCoroutine(Move(_enemy4.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                    }));
                };
                StartCoroutine(Move(_enemy3.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
            };
            StartCoroutine(Move(_enemy2.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
        };
        // 移动
        StartCoroutine(Move(_enemy1.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
    }

    IEnumerator GoOut()
    {
        // 创建骑士队长
        GameObject obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        obj.transform.position = (Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up;
        // 创建楼梯
        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 7).transform.position = new Vector2(0, 5);
        // 移动
        yield return StartCoroutine(Move(obj.transform, new List<Vector2> { new Vector2(0, 5) }, 40));
        // 回收资源
        GameManager.Instance.PoolManager.RecycleResource(obj);
        // 创建黄钥匙
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = new Vector2(-4 + i, 4);
        }
        // 创建红宝石
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = new Vector2(2 + i, 4);
        }
        // 创建大血瓶
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = new Vector2(-3 + i, 2);
        }
        // 创建蓝宝石
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = new Vector2(1 + i, 2);
        }
        // 改变剧情状态
        GameManager.Instance.PlotManager.PlotDictionary[16] = 2;
        // 解锁人物控制器
        GameManager.Instance.PlayerManager.LockEnable = false;
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 解锁音频播放
        GameManager.Instance.SoundManager.LockEnable = false;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
        // 回收剧情资源
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
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
        _enemy2 = null;
        _enemy3 = null;
        _enemy4 = null;
        _enemy5 = null;
        _enemy6 = null;
        _enemy7 = null;
        _enemy8 = null;
        _enemy9 = null;
        _enemy10 = null;
        _enemy11 = null;
        _enemy12 = null;
        _enemy13 = null;
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-1, 2)) _enemy1 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-2, 2)) _enemy2 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-3, 2)) _enemy3 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 2)) _enemy4 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(2, 2)) _enemy5 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(3, 2)) _enemy6 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-2, 4)) _enemy7 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-3, 4)) _enemy8 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-4, 4)) _enemy9 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(2, 4)) _enemy10 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(3, 4)) _enemy11 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(4, 4)) _enemy12 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, 5)) _enemy13 = obj.GetComponent<EnemyController>();
        });
    }
}
