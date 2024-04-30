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
        // ������Դ
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }

    /// <summary>
    /// ��ȡ�����¼�
    /// </summary>
    private void GetGuardEvent()
    {
        StartCoroutine(GetGuard());
    }

    IEnumerator GetGuard()
    {
        _vampire = null;
        // ����ʹ�������б��а�λ�û�ȡ����
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
