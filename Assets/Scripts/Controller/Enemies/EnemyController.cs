using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ResourceController
{
    public int Health;
    public int MaxHealth;
    public int Attack;
    public int Defence;
    public int Gold;

    public Action OnDeath;

    protected void OnDisable()
    {
        OnDeath = null;
    }

    public void SetHealth(int value)
    {
        Health = value <= 0 ? 0 : value;
    }
}
