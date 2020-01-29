using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDASystem : MonoBehaviour
{
    private int currentLevelFactor;
    private int currentPlayerFactor;
    protected Player player;

    protected int enemyQuantity;

    protected int specialEnemyQuantity;
    protected int specialSkillFactor;

    private int playerMaxHp;
    private int playerSkillPoint;
    private int playerQi;
    private int playerATK;

    public int CurrentPlayerFactor { get => currentPlayerFactor; set => currentPlayerFactor = value; }
    public int CurrentLevelFactor { get => currentLevelFactor; set => currentLevelFactor = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (player == null)
            player = Player.MyInstance;
        //CalculateCPF();

    }

    // Update is called once per frame
    private void Update()
    {
        if(player == null)
            player = Player.MyInstance;

    }
    private void GetPlayerStat()
    {
        playerMaxHp = player.maxHp;
        playerSkillPoint = player.skillStars;
        playerQi = player.qi;
        playerATK = player.baseATK;
    }

    public void CalculateCPF()
    {
        GetPlayerStat();
        CurrentPlayerFactor = playerMaxHp / 100 + playerSkillPoint + playerQi + playerATK;
        Debug.Log("cpf:" + CurrentPlayerFactor);
    }

    public void CalculateCLF(List<GameObject> enemyList)
    {
        CurrentLevelFactor = 0;
        foreach(GameObject g in enemyList)
        {
            CurrentLevelFactor += g.GetComponent<Enemy>().StrengthValue;
        }
        Debug.Log("clf:" + currentLevelFactor);

    }

    /// <summary>
    /// 计算LF和PF的差值，LF-PF 
    /// 关卡强度高于玩家强度的数值
    /// </summary>
    /// <param name="enemylist"></param>
    /// <returns></returns>
    public int FactorCompare(List<GameObject> enemylist)
    {
        int f = 0;
        CalculateCLF(enemylist);
        CalculateCPF();
        f = CurrentLevelFactor - CurrentPlayerFactor;
        return f;
    }
}
