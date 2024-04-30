using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireController : EnemyController
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnVampireShow?.Invoke();
           OnDeath += () =>
        {
            // ˵��
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�ޣ��ϵۣ�������Ҳû�뵽�Լ������һ�����ࡣ", "��Ȼ��������ʱ��ʤ���������ڴ�ʦ��˵�㻹��̫���ˡ�" }, () =>
            {
                // ����ʱ������Ʒ
                Vector2 point = new Vector2();
                // ���ɴ�Ѫƿ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-1 + i, -2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = point;
                }
                // ���ɺ챦ʯ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-2, 1 - i);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = point;
                }
                // ��������ʯ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(2, 1 - i);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = point;
                }
                // ���ɻ�Կ��
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-1 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = point;
                }
                // �ı����״̬
                GameManager.Instance.PlotManager.PlotDictionary[15] = 2;
                // �����������
                GameManager.Instance.PlayerManager.Enable = true;
                // ������Ƶ����
                GameManager.Instance.SoundManager.LockEnable = false;
                // ��Ƶ����
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
            });
        };
    }
}
