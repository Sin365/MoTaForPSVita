using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : Singleton<PlotManager>
{
    private Dictionary<int, int> _plotDictionary = new Dictionary<int, int>();

    public Dictionary<int, int> PlotDictionary { get => _plotDictionary; set => _plotDictionary = value; }

    /// <summary>
    /// 初始化剧情
    /// </summary>
    public void Init()
    {
        _plotDictionary = new Dictionary<int, int>
        {
            // 2 层小偷剧情
            {1,1 },
            // 6 层商人剧情
            {2,1 },
            // 7 层商人剧情
            {3,1 },
            // 10 层骷髅队长剧情
            {4,1 },
            // 12 层商人剧情
            {5,1 },
            // 15 层商人剧情
            {6,1 },
            // 23 层墙壁剧情
            {7,0 },
            // 31 层墙壁剧情
            {8,1 },
            // 35 层巨龙拦路剧情
            {9,1 },
            // 38 层商人剧情
            {10,1 },
            // 39 层商人剧情
            {11,1 },
            // 45 层商人剧情
            {12,1 },
            // 47 层商人剧情
            {13,1 },
            // 49 层魔王剧情
            {14,1 },
            // 20 层吸血鬼剧情
            {15,1 },
            // 40 层骑士队长剧情
            {16,1 },
            // 25 层大法师剧情
            {17,1 },
            // 26 层公主剧情
            {18,1 },
        };
    }
}
