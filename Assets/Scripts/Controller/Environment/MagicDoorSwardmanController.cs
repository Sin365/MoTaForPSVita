using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoorSwardmanController : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _guard1;
    private EnemyController _guard2;
    private EnemyController _guard3;
    private EnemyController _guard4;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GetGuardEvent();
    }

    public void RecycleSelf()
    {
        // 回收资源
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }

    /// <summary>
    /// 获取守卫事件
    /// </summary>
    private void GetGuardEvent()
    {
        _guard1 = null;
        _guard2 = null;
        _guard3 = null;
        _guard4 = null;
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(3, 1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard1 = obj.GetComponent<EnemyController>();
                    _guard1.OnDeath += () => { _guard1 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(3, -1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard2 = obj.GetComponent<EnemyController>();
                    _guard2.OnDeath += () => { _guard2 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(5, 1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard3 = obj.GetComponent<EnemyController>();
                    _guard3.OnDeath += () => { _guard3 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(5, -1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard4 = obj.GetComponent<EnemyController>();
                    _guard4.OnDeath += () => { _guard4 = null; DetectionOpen(); };
                }
            }
        });
        DetectionOpen();
    }

    /// <summary>
    /// 检测门是否能打开
    /// </summary>
    private void DetectionOpen()
    {
        if (_guard1 == null && _guard2 == null && _guard3 == null && _guard4 == null) _animator.SetTrigger("open");
    }
}
