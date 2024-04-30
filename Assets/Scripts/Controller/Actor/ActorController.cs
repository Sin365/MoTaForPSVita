using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : ResourceController, IInteraction
{
    protected Animator _animator;

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual bool Interaction() { return false; }

    public void DisableSelf()
    {
        // 启用人物控制器
        GameManager.Instance.PlayerManager.Enable = true;
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }
}
