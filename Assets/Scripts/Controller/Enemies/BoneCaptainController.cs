using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCaptainController : EnemyController, IInteraction, IExplosionproof
{
    private void OnEnable()
    {
        OnDeath += () =>
        {
            // �ر����������
            GameManager.Instance.PlayerManager.Enable = false;
            // ���������ƶ�
            GameManager.Instance.PlayerManager.LockEnable = true;
            // ˵��
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "���������ܣ�����", "����ô���䣿", "�����⣬���滹�����ǿ��Ķ��֣���������ɣ�" }, () =>
            {
                // ����
                EnvironmentController ec = null;
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if (obj.GetComponent<MagicDoorController>() != null && (Vector2)obj.transform.position == new Vector2(0, -1)) ec = obj.GetComponent<EnvironmentController>();
                });
                ec.Open(null);
                // ����ʱ������Ʒ
                Vector2 point = new Vector2();
                // ���ɴ�Ѫƿ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-5 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = point;
                }
                // ���ɺ챦ʯ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(-5 + i, 3);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = point;
                }
                // ��������ʯ
                for (int i = 0; i < 3; i++)
                {
                    point.Set(3 + i, 3);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = point;
                }
                // ���ɻ�Կ��
                for (int i = 0; i < 3; i++)
                {
                    point.Set(3 + i, 2);
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = point;
                }
                // ����¥��
                point.Set(0, -5);
                GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 7).transform.position = point;
                // ���ɾ���ذ�
                point.Set(0, -3);
                GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 14).transform.position = point;
                // ���������ƶ�
                GameManager.Instance.PlayerManager.LockEnable = false;
                // �����������
                GameManager.Instance.PlayerManager.Enable = true;
                // ��������
                GameManager.Instance.SoundManager.LockEnable = false;
                // ��Ƶ����
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
                // �������״̬
                GameManager.Instance.PlotManager.PlotDictionary[4] = 5;
            });
        };
    }

    public bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[4])
        {
            // ״̬ 1 �Ի�
            case 3:
                // ����˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "��Ȼ���ӳ���������壿", "���ǲ��������ȥ�ģ�����һ��ս�ɣ�" }, () =>
                {
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[4] = 4;
                });
                break;
            case 4:
                return true;

        }
        return false;
    }
}
