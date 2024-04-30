using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallController3 : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += OnLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= OnLoaded;
    }

    private void OnLoaded()
    {
        // ���ﵽ�� 35 �� ����״̬ 1
        if (GameManager.Instance.PlotManager.PlotDictionary[9] == 1)
        {
            // ���� 2 ¥С͵
            GameManager.Instance.ResourceManager.MakeResourceForLevel(2, EResourceType.Actor, 30, new Vector2(4, -5));
            // �ı����״̬
            GameManager.Instance.PlotManager.PlotDictionary[9] = 2;
        }
        else if (GameManager.Instance.PlotManager.PlotDictionary[9] == 2)
        {
            return;
        }
        else
        {
            // ������Դ
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
