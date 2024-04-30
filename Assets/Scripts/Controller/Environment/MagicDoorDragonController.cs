using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoorDragonController : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _dragon;

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

    public void RecycleSelf()
    {
        // ������Դ
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }

    /// <summary>
    /// ��ȡ�����¼�
    /// </summary>
    private void GetGuardEvent()
    {
        _dragon = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == (Vector2)transform.position + Vector2.down * 3)
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _dragon = obj.GetComponent<EnemyController>();
                    _dragon.OnDeath += () => { _animator.SetTrigger("open"); };
                }
            }
        });
    }
}
