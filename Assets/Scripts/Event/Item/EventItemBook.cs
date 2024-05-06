using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人属性信息
/// </summary>
public class EnemyPropertyInfo
{
    public string Name;
    public string IconPath;
    public int Health;
    public int Attack;
    public int Denfence;
    public int Gold;
    public string Damage;
}

public class EventItemBook : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowBook(() =>
        {
            List<EnemyPropertyInfo> enemyPropertyInfos = new List<EnemyPropertyInfo>();
            // 获取本层所有活着的怪物
            List<EnemyController> tempEnemyController = new List<EnemyController>();
            foreach (var enemy in GameManager.Instance.PoolManager.UseList)
            {
                if (enemy.GetComponent<EnemyController>())
                {
                    bool has = false;
                    foreach (var tec in tempEnemyController)
                    {
                        if (tec.ID == enemy.GetComponent<EnemyController>().ID)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (!has) tempEnemyController.Add(enemy.GetComponent<EnemyController>());
                }
            }
            // 计算伤害
            foreach (var enemy in tempEnemyController)
            {
                enemyPropertyInfos.Add(new EnemyPropertyInfo
                {
                    Name = enemy.Name,
                    IconPath = enemy.IconPath,
                    Health = enemy.Health,
                    Attack = enemy.Attack,
                    Denfence = enemy.Defence,
                    Gold = enemy.Gold,
                    Damage = CalculateEnemyDamage(enemy),
                });
            }
            // 创建子面板
            foreach (var enemyPropertyInfo in enemyPropertyInfos)
            {
                GameObject obj = Instantiate(Resources.Load("UI/EnemyInfoPanel"), GameManager.Instance.UIManager.BookContent) as GameObject;
                obj.transform.Find("IconImage").GetComponent<Image>().sprite = Instantiate(Resources.Load(enemyPropertyInfo.IconPath) as Sprite);
                obj.transform.Find("NameValueText").GetComponent<Text>().text = enemyPropertyInfo.Name;
                obj.transform.Find("HealthValueText").GetComponent<Text>().text = enemyPropertyInfo.Health.ToString();
                obj.transform.Find("AttackValueText").GetComponent<Text>().text = enemyPropertyInfo.Attack.ToString();
                obj.transform.Find("DefenceValueText").GetComponent<Text>().text = enemyPropertyInfo.Denfence.ToString();
                obj.transform.Find("GoldValueText").GetComponent<Text>().text = enemyPropertyInfo.Gold.ToString();
                obj.transform.Find("DamageValueText").GetComponent<Text>().text = enemyPropertyInfo.Damage;
            }
        });
        return false;
    }

    /// <summary>
    /// 计算敌人伤害
    /// </summary>
    /// <param name="enemy">敌人控制器</param>
    /// <returns>所受伤害内容</returns>
    private string CalculateEnemyDamage(EnemyController enemy)
    {
        // 计算玩家伤害
        int playerDamage = GameManager.Instance.PlayerManager.PlayerInfo.Attack - enemy.Defence;
        playerDamage = playerDamage < 0 ? 0 : playerDamage;
        CalculateJudge(enemy, ref playerDamage);
        // 计算敌人伤害
        int enemyDamage = enemy.Attack - GameManager.Instance.PlayerManager.PlayerInfo.Defence;
        enemyDamage = enemyDamage < 0 ? 0 : enemyDamage;
        // 如果玩家伤害为 0
        if (playerDamage == 0)
        {
            // 如果敌人伤害也为 0
            if (enemyDamage == 0) return "无解";
            // 否则就是肉靶子
            else return "会死";
        }
        // 计算回合数
        int round = enemy.Health % playerDamage > 0 ? enemy.Health / playerDamage : enemy.Health / playerDamage - 1;
        return (round * enemyDamage).ToString();
    }

    /// <summary>
    /// 计算十字架对吸血鬼、兽人的伤害
    /// </summary>
    /// <param name="damage">伤害 引用类型 直接返回</param>
    private void CalculateJudge(EnemyController enemy,ref int damage)
    {
        // 判断对方是否是吸血鬼
        if (enemy.ID == 12 || enemy.ID == 13 || enemy.ID == 16)
        {
            // 判断背包是否有十字架 伤害翻倍
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(19)) damage *= 2;
        }
    }
}
