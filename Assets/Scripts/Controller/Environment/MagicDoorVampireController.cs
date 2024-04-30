using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoorVampireController : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _vampire;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnVampireShow += GetGuardEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnVampireShow -= GetGuardEvent;
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
        StartCoroutine(GetGuard());
    }

    IEnumerator GetGuard()
    {
        _vampire = null;
        // 从已使用物体列表中按位置获取物体
        while(null == _vampire)
        {
            GameManager.Instance.PoolManager.UseList.ForEach(obj =>
            {
                if ((Vector2)obj.transform.position == new Vector2(0, 0))
                {
                    if (obj.GetComponent<EnemyController>() != null)
                    {
                        _vampire = obj.GetComponent<EnemyController>();
                        _vampire.OnDeath += () => { _animator.SetTrigger("open"); };
                    }
                }
            });
            yield return null;
        }
        yield break;
    }
}
