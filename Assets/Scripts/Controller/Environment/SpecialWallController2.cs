using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallController2 : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _guard1;
    private EnemyController _guard2;
    private EnemyController _guard3;
    private EnemyController _guard4;
    private EnemyController _guard5;
    private EnemyController _guard6;
    private EnemyController _guard7;
    private EnemyController _guard8;

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
        _guard1 = null;
        _guard2 = null;
        _guard3 = null;
        _guard4 = null;
        _guard5 = null;
        _guard6 = null;
        _guard7 = null;
        _guard8 = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        Vector2[] points = new Vector2[] {
        new Vector2(-1, 2),
        new Vector2(1,2),
        new Vector2(3,2),
        new Vector2(5,2),
        new Vector2(-1,-2),
        new Vector2(1,-2),
        new Vector2(3,-2),
        new Vector2(5,-2),
        };
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-1,2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard1 = obj.GetComponent<EnemyController>();
                    _guard1.OnDeath += () => { _guard1 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(1, 2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard2 = obj.GetComponent<EnemyController>();
                    _guard2.OnDeath += () => { _guard2 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(3, 2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard3 = obj.GetComponent<EnemyController>();
                    _guard3.OnDeath += () => { _guard3 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(5, 2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard4 = obj.GetComponent<EnemyController>();
                    _guard4.OnDeath += () => { _guard4 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(-1, -2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard5 = obj.GetComponent<EnemyController>();
                    _guard5.OnDeath += () => { _guard5 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(1, -2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard6 = obj.GetComponent<EnemyController>();
                    _guard6.OnDeath += () => { _guard6 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(3, -2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard7 = obj.GetComponent<EnemyController>();
                    _guard7.OnDeath += () => { _guard7 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(5, -2))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard8 = obj.GetComponent<EnemyController>();
                    _guard8.OnDeath += () => { _guard8 = null; DetectionOpen(); };
                }
            }
        });
    }

    /// <summary>
    /// ������Ƿ��ܴ�
    /// </summary>
    private void DetectionOpen()
    {
        if (_guard1 == null && _guard2 == null && _guard3 == null && _guard4 == null && _guard5 == null && _guard6 == null && _guard7 == null && _guard8 == null)
        {
            _animator.SetTrigger("open");
            // ���ɻ�Կ��
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = (Vector2)transform.position + Vector2.up + Vector2.left;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = (Vector2)transform.position + Vector2.up + Vector2.right;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = (Vector2)transform.position + Vector2.down + Vector2.left;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = (Vector2)transform.position + Vector2.down + Vector2.right;
            // ���ɺ�Կ��
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 3).transform.position = transform.position;
            // ��������
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
