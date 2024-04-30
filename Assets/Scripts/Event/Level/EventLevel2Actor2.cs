using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor2 : ActorController
{
    public override bool Interaction()
    {
        // С͵˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "����������������Ե��", "лл������ҡ�", "�һ���ħ���Ա߿�һ��������ȥ 35 �����Ұɡ�" }, () =>
        {
            // ���� 35 ¥С͵
            GameManager.Instance.ResourceManager.MakeResourceForLevel(35, EResourceType.Actor, 31, new Vector2(-1, -4));
            // �ı����
            GameManager.Instance.PlotManager.PlotDictionary[9] = 3;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // NPC ����
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
