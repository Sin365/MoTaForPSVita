using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemNotepad : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowNotepad();
        return false;
    }
}
