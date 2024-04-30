using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemArtifact2 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInfo("Ү�ձ��ӣ�����~");
        return false;
    }
}
