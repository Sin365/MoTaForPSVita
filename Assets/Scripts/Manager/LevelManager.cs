using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 关卡传送信息
/// </summary>
public class LevelTransferInfo
{
    /// <summary>
    /// 上楼梯旁边的传送点
    /// </summary>
    public Vector2 UpStairPoint;
    /// <summary>
    /// 下楼梯旁边的传送点
    /// </summary>
    public Vector2 DownStairPoint;
}

public class LevelManager : Singleton<LevelManager>
{
    private int _level;
    private int _maxLevel;
    private Dictionary<int, LevelTransferInfo> _levelTransferInfo;

    public int Level
    {
        get => _level;
        set
        {
            GameManager.Instance.EventManager.OnLevelChanged?.Invoke(_level, value);
            _level = value;
            if (_level > _maxLevel)
            {
                _maxLevel = _level;
            }
            // 音频播放
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "UpOrDownStairs");
        }
    }

    public Dictionary<int, LevelTransferInfo> LevelTransferInfo { get => _levelTransferInfo; }
    public int MaxLevel { get => _maxLevel; set => _maxLevel = value; }

    public LevelManager()
    {
        // 关卡从 -1 开始 跳过初始化 0 层
        _level = -1;
        // 初始化传送点
        _levelTransferInfo = new Dictionary<int, LevelTransferInfo>
        {
            {1,new LevelTransferInfo{
                UpStairPoint=new Vector3(-4,5),
                DownStairPoint=new Vector2(0,-5),
            } },
            {2,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,-4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {3,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {4,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,-4),
                DownStairPoint=new Vector2(5,-4),
            } },
            {5,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,4),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {6,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,-4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {7,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,4),
                DownStairPoint=new Vector2(5,-4),
            } },
            {8,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {9,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,-4),
                DownStairPoint=new Vector2(0,4),
            } },
            {10,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(-5,-4),
            } },
            {11,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,-4),
                DownStairPoint=new Vector2(0,-4),
            } },
            {12,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,-5),
                DownStairPoint=new Vector2(4,-5),
            } },
            {13,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {14,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(5,-4),
            } },
            {15,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(0,-4),
            } },
            {16,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(0,4),
            } },
            {17,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(-1,-5),
            } },
            {18,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,4),
                DownStairPoint=new Vector2(0,4),
            } },
            {19,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {20,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(0,-4),
            } },
            {21,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(0,4),
            } },
            {22,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-1),
                DownStairPoint=new Vector2(0,-4),
            } },
            {23,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {24,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {25,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {26,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {27,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,-5),
                DownStairPoint=new Vector2(-4,-5),
            } },
            {28,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,-5),
                DownStairPoint=new Vector2(4,-5),
            } },
            {29,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(-5,-4),
            } },
            {30,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(0,-4),
            } },
            {31,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(0,4),
            } },
            {32,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,4),
                DownStairPoint=new Vector2(0,-5),
            } },
            {33,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,5),
                DownStairPoint=new Vector2(4,5),
            } },
            {34,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(-4,5),
            } },
            {35,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,4),
                DownStairPoint=new Vector2(0,-4),
            } },
            {36,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,-4),
                DownStairPoint=new Vector2(5,4),
            } },
            {37,new LevelTransferInfo{
                UpStairPoint=new Vector2(-4,5),
                DownStairPoint=new Vector2(5,-4),
            } },
            {38,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,5),
                DownStairPoint=new Vector2(-4,5),
            } },
            {39,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,-5),
                DownStairPoint=new Vector2(5,4),
            } },
            {40,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,4),
                DownStairPoint=new Vector2(4,-5),
            } },
            {41,new LevelTransferInfo{
                UpStairPoint=new Vector2(0,-4),
                DownStairPoint=new Vector2(0,4),
            } },
            {42,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,4),
                DownStairPoint=new Vector2(-1,-5),
            } },
            {43,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,-4),
                DownStairPoint=new Vector2(-5,4),
            } },
            {45,new LevelTransferInfo{
                UpStairPoint=new Vector2(4,5),
                DownStairPoint=new Vector2(-4,5),
            } },
            {46,new LevelTransferInfo{
                UpStairPoint=new Vector2(5,-4),
                DownStairPoint=new Vector2(5,4),
            } },
            {47,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,4),
                DownStairPoint=new Vector2(4,-5),
            } },
            {48,new LevelTransferInfo{
                UpStairPoint=new Vector2(-5,-4),
                DownStairPoint=new Vector2(5,-4),
            } },
            {49,new LevelTransferInfo{
                UpStairPoint=Vector2.zero,
                DownStairPoint=new Vector2(-4,-5),
            } },
        };
    }
}
