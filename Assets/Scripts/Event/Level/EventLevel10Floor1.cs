using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel10Floor1 : MonoBehaviour
{
    private EnemyController _enemyBone3;
    private EnemyController _enemyBone11;
    private EnemyController _enemyBone12;
    private EnemyController _enemyBone13;
    private EnemyController _enemyBone14;
    private EnemyController _enemyBone15;
    private EnemyController _enemyBone16;
    private EnemyController _enemyBone21;
    private EnemyController _enemyBone22;
    private EnvironmentController _environmentMagicDoor1;
    private EnvironmentController _environmentMagicDoor2;
    private EnvironmentController _environmentWall1;
    private EnvironmentController _environmentWall2;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 状态一触发对话
        if (GameManager.Instance.PlotManager.PlotDictionary[4] == 1)
        {
            if (collision.CompareTag("Player"))
            {
                // 禁用人物控制器
                GameManager.Instance.PlayerManager.Enable = false;
                // 锁定音乐
                GameManager.Instance.SoundManager.LockEnable = true;
                // 骷髅队长说话
                GameManager.Instance.UIManager.ShowDialog(_enemyBone3.Name, new List<string> { "好小子，竟然能来到这里。", "不过你的好日子到头了！", "关门，放狗！！！" }, (() =>
                {
                    // 播放动画协程
                    StartCoroutine(TriggerTrap());
                }));
            }
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    IEnumerator TriggerTrap()
    {
        // 打开墙壁
        _environmentWall1.Open(null);
        _environmentWall2.Open(null);
        // 打开门
        _environmentMagicDoor1.Open(null);
        _environmentMagicDoor2.Open(null);
        // 队长移动
        yield return StartCoroutine(Move(_enemyBone3.transform, new List<Vector2> { new Vector2(0, 5) }));
        // 生成魔法门
        _environmentMagicDoor1 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 4).GetComponent<EnvironmentController>();
        _environmentMagicDoor1.transform.position = new Vector2(0, 3);
        _environmentMagicDoor2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 4).GetComponent<EnvironmentController>();
        _environmentMagicDoor2.transform.position = new Vector2(0, -1);
        // 怪物移动
        StartCoroutine(Move(_enemyBone21.transform, new List<Vector2> { new Vector2(0, 2) }));
        yield return StartCoroutine(Move(_enemyBone22.transform, new List<Vector2> { new Vector2(1, 2), new Vector2(1, 0), new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBone13.transform, new List<Vector2> { new Vector2(-3, 2), new Vector2(-1, 2), new Vector2(-1, 0) }));
        StartCoroutine(Move(_enemyBone12.transform, new List<Vector2> { new Vector2(-3, 3), new Vector2(-3, 2), new Vector2(-1, 2), new Vector2(-1, 1) }));
        StartCoroutine(Move(_enemyBone11.transform, new List<Vector2> { new Vector2(-3, 3), new Vector2(-3, 2), new Vector2(-1, 2), new Vector2(-1, 2) }));
        StartCoroutine(Move(_enemyBone14.transform, new List<Vector2> { new Vector2(3, 2), new Vector2(1, 2), new Vector2(1, 0) }));
        StartCoroutine(Move(_enemyBone15.transform, new List<Vector2> { new Vector2(3, 3), new Vector2(3, 2), new Vector2(1, 2), new Vector2(1, 1) }));
        yield return StartCoroutine(Move(_enemyBone16.transform, new List<Vector2> { new Vector2(3, 3), new Vector2(3, 2), new Vector2(1, 2), new Vector2(1, 2) }));
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        // 改变剧情状态
        GameManager.Instance.PlotManager.PlotDictionary[4] = 2;
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

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void GetGameObjectEvent()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[4])
        {
            case 1:
                _enemyBone3 = null;
                _enemyBone11 = null;
                _enemyBone12 = null;
                _enemyBone13 = null;
                _enemyBone14 = null;
                _enemyBone15 = null;
                _enemyBone16 = null;
                _enemyBone21 = null;
                _enemyBone22 = null;
                _environmentMagicDoor1 = null;
                _environmentMagicDoor2 = null;
                _environmentWall1 = null;
                _environmentWall2 = null;
                // 从已使用物体列表中按位置获取物体
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if ((Vector2)obj.transform.position == new Vector2(0, 2))
                    {
                        _enemyBone3 = obj.GetComponent<EnemyController>();
                        _enemyBone3.OnDeath += () => { _enemyBone3 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(-5, 3))
                    {
                        _enemyBone11 = obj.GetComponent<EnemyController>();
                        _enemyBone11.OnDeath += () => { _enemyBone11 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(-4, 3))
                    {
                        _enemyBone12 = obj.GetComponent<EnemyController>();
                        _enemyBone12.OnDeath += () => { _enemyBone12 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(-3, 3))
                    {
                        _enemyBone13 = obj.GetComponent<EnemyController>();
                        _enemyBone13.OnDeath += () => { _enemyBone13 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(3, 3))
                    {
                        _enemyBone14 = obj.GetComponent<EnemyController>();
                        _enemyBone14.OnDeath += () => { _enemyBone14 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(4, 3))
                    {
                        _enemyBone15 = obj.GetComponent<EnemyController>();
                        _enemyBone15.OnDeath += () => { _enemyBone15 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(5, 3))
                    {
                        _enemyBone16 = obj.GetComponent<EnemyController>();
                        _enemyBone16.OnDeath += () => { _enemyBone16 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(-4, 2))
                    {
                        _enemyBone21 = obj.GetComponent<EnemyController>();
                        _enemyBone21.OnDeath += () => { _enemyBone21 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(4, 2))
                    {
                        _enemyBone22 = obj.GetComponent<EnemyController>();
                        _enemyBone22.OnDeath += () => { _enemyBone22 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(-2, 2)) _environmentMagicDoor1 = obj.GetComponent<EnvironmentController>();
                    else if ((Vector2)obj.transform.position == new Vector2(2, 2)) _environmentMagicDoor2 = obj.GetComponent<EnvironmentController>();
                    else if ((Vector2)obj.transform.position == new Vector2(-1, 0)) _environmentWall1 = obj.GetComponent<EnvironmentController>();
                    else if ((Vector2)obj.transform.position == new Vector2(1, 0)) _environmentWall2 = obj.GetComponent<EnvironmentController>();
                });
                break;
            case 2:
                _enemyBone3 = null;
                _enemyBone11 = null;
                _enemyBone12 = null;
                _enemyBone13 = null;
                _enemyBone14 = null;
                _enemyBone15 = null;
                _enemyBone16 = null;
                _enemyBone21 = null;
                _enemyBone22 = null;
                _environmentMagicDoor1 = null;
                _environmentMagicDoor2 = null;
                // 从已使用物体列表中按位置获取物体
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(0, 5))
                    {
                        _enemyBone3 = obj.GetComponent<EnemyController>();
                        _enemyBone3.OnDeath += () => { _enemyBone3 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(-1, 0))
                    {
                        _enemyBone11 = obj.GetComponent<EnemyController>();
                        _enemyBone11.OnDeath += () => { _enemyBone11 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(-1, 1))
                    {
                        _enemyBone12 = obj.GetComponent<EnemyController>();
                        _enemyBone12.OnDeath += () => { _enemyBone12 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(-1, 2))
                    {
                        _enemyBone13 = obj.GetComponent<EnemyController>();
                        _enemyBone13.OnDeath += () => { _enemyBone13 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(1, 0))
                    {
                        _enemyBone14 = obj.GetComponent<EnemyController>();
                        _enemyBone14.OnDeath += () => { _enemyBone14 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(1, 1))
                    {
                        _enemyBone15 = obj.GetComponent<EnemyController>();
                        _enemyBone15.OnDeath += () => { _enemyBone15 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(1, 2))
                    {
                        _enemyBone16 = obj.GetComponent<EnemyController>();
                        _enemyBone16.OnDeath += () => { _enemyBone16 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(0, 2))
                    {
                        _enemyBone21 = obj.GetComponent<EnemyController>();
                        _enemyBone21.OnDeath += () => { _enemyBone21 = null; DetectionOpen(); };
                    }
                    else if (obj.GetComponent<EnemyController>() != null && (Vector2)obj.transform.position == new Vector2(0, 0))
                    {
                        _enemyBone22 = obj.GetComponent<EnemyController>();
                        _enemyBone22.OnDeath += () => { _enemyBone22 = null; DetectionOpen(); };
                    }
                    else if ((Vector2)obj.transform.position == new Vector2(0, 3)) _environmentMagicDoor1 = obj.GetComponent<EnvironmentController>();
                    else if ((Vector2)obj.transform.position == new Vector2(0, -1)) _environmentMagicDoor2 = obj.GetComponent<EnvironmentController>();
                });
                break;
        }
    }

    /// <summary>
    /// 检测门是否能打开
    /// </summary>
    private void DetectionOpen()
    {
        if (_enemyBone11 == null && _enemyBone12 == null && _enemyBone13 == null && _enemyBone14 == null && _enemyBone15 == null && _enemyBone16 == null && _enemyBone21 == null && _enemyBone22 == null)
        {
            _environmentMagicDoor1.Open(null);
            // 启用人物控制器
            GameManager.Instance.PlayerManager.Enable = true;
            // 改变剧情状态
            GameManager.Instance.PlotManager.PlotDictionary[4] = 3;
            // 回收剧情资源
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
