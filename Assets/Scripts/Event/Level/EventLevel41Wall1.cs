using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel41Wall1 : MonoBehaviour
{
    private EnemyController _enemy;
    private EnvironmentController _wall;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void GetGameObjectEvent()
    {
        _enemy = null;
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-4, 4))
            {
                _enemy = obj.GetComponent<EnemyController>();
                _enemy.OnDeath += () =>
                {
                    // 创建可开墙
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 12).transform.position = _wall.transform.position;
                    // 创建法师
                    _enemy = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 27).GetComponent<EnemyController>();
                    _enemy.transform.position = _wall.transform.position;
                    _enemy.OnDeath += () =>
                    {
                        // 创建墙壁
                        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 6).transform.position = Vector2.up;
                        // 创建飞行器
                        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 22).transform.position = Vector2.up * 2;
                    };
                    // 删除墙壁
                    GameManager.Instance.PoolManager.RecycleResource(_wall.gameObject);
                };
            }
            else if ((Vector2)obj.transform.position == new Vector2(4, 4)) _wall = obj.GetComponent<EnvironmentController>();
        });
    }
}
