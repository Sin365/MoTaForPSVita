using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor3 : ActorController
{
    public override bool Interaction()
    {
        // ��ͷ˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "��л������ң��� 1000 ���������¡�" }, () =>
        {
            // ��ý��
            GameManager.Instance.PlayerManager.PlayerInfo.Gold += 1000;
            // ��ʾ��Ϣ
            GameManager.Instance.UIManager.ShowInfo("��� 1000 ��ҡ�");
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
