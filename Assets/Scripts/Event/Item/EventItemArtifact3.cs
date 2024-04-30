using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemArtifact3 : MonoBehaviour, IInteraction
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnArtifactUp = OnUpEvent;
        GameManager.Instance.EventManager.OnArtifactDown = OnDownEvent;
    }

    public bool Interaction()
    {
        //GameManager.Instance.UIManager.ShowInfo("站在楼梯口，按 [PageUP] 和 [PageDown] 进行快速移动。");
        GameManager.Instance.UIManager.ShowInfo("站在楼梯口，按 [△] 和 [X] 进行快速移动。");
        return false;
    }

    private void OnUpEvent()
    {
        if (NearTheStair())
        {
            int nextIndex = GameManager.Instance.LevelManager.Level + 1;
            // 44 层跳跃
            nextIndex = nextIndex == 44 ? 45 : nextIndex;
            // 最高楼层不传送
            if (nextIndex > GameManager.Instance.LevelManager.MaxLevel)
            {
                GameManager.Instance.UIManager.ShowInfo("前方的路需要自己探索哦~");
                return;
            }
            // 获取下一层信息
            LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
            // 修改人物下一层位置用于传送
            GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, nextLevelInfo.DownStairPoint);
            // 传送到下一层
            GameManager.Instance.LevelManager.Level = nextIndex;
        }
        else GameManager.Instance.UIManager.ShowInfo("请站在楼梯口使用法老权杖！");
    }
    private void OnDownEvent()
    {
        if (NearTheStair())
        {
            int nextIndex = GameManager.Instance.LevelManager.Level - 1;
            // 44 层跳跃
            nextIndex = nextIndex == 44 ? 43 : nextIndex;
            // 略过 0 层
            if (nextIndex == 0) return;
            // 获取下一层信息
            LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
            // 修改人物下一层位置用于传送
            GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, nextLevelInfo.UpStairPoint);
            // 传送到下一层
            GameManager.Instance.LevelManager.Level = nextIndex;
        }
        else GameManager.Instance.UIManager.ShowInfo("请站在楼梯口使用法老权杖！");
    }

    /// <summary>
    /// 是否在楼梯附近
    /// </summary>
    /// <returns></returns>
    private bool NearTheStair()
    {
        // 从已使用物体列表中按位置获取物体
        foreach (var obj in GameManager.Instance.PoolManager.UseList)
        {
            if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position+Vector2.up))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right))
            {
                return true;
            }
        }
        return false;
    }
}
