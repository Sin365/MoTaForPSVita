using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : EnvironmentController
{
    private new void OnEnable()
    {
        base.OnDisable();
        GameManager.Instance.PlayerManager.Enable = false;
        // 音频播放
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Magic");
    }

    private new void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.PlayerManager.Enable = true;
    }
    public void ShowMagic(EDirectionType direction)
    {
        GetComponent<Animator>().SetFloat("direction", (int)direction);
        GetComponent<Animator>().SetTrigger("show");
    }
}
