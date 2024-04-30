using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家与其它物体交互接口
/// </summary>
public interface IInteraction
{
    /// <summary>
    /// 交互方法
    /// </summary>
    /// <returns>交互返回值 一般用于是否可以继续移动</returns>
    bool Interaction();
}
