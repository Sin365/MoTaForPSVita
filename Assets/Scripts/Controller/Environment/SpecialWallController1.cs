using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallController1 : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _guard1;
    private EnemyController _guard2;
    private EnemyController _guard3;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGuardEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGuardEvent;
    }

    public void TriggerClose()
    {
        _animator.SetTrigger("close");
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
        // 从已使用物体列表中按位置获取物体
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(transform.position.x + 1, transform.position.y + 1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard1 = obj.GetComponent<EnemyController>();
                    _guard1.OnDeath += () => { _guard1 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(transform.position.x + 2, transform.position.y + 2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard2 = obj.GetComponent<EnemyController>();
                    _guard2.OnDeath += () => { _guard2 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(transform.position.x, transform.position.y + 2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard3 = obj.GetComponent<EnemyController>();
                    _guard3.OnDeath += () => { _guard3 = null; DetectionOpen(); };
                }
            }
        });
    }

    /// <summary>
    /// 检测门是否能打开
    /// </summary>
    private void DetectionOpen()
    {
        if (_guard1 == null && _guard2 == null && _guard3 == null) _animator.SetTrigger("open");
    }
}
