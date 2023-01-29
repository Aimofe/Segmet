using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image live;
    public Image dash;
    public Image jump;
    public Image attack;
    public Text exp;
    public GameObject boss;
    public Text bossName;
    public Image bossLive;
    public Sejmet Sejmet;
    //Hacer un evento y llamar cuando alguien le haga daño
    void NormalizeLive()
    {
        live.fillAmount = Sejmet.vida / 100.0f;
    }
    void NormalizeDash()
    {
        dash.fillAmount = 1 - ((Sejmet.nextSpecialDash - Time.realtimeSinceStartup ) / Sejmet.dashAmount);
    }
    void NormalizeJump()
    {
        jump.fillAmount = 1 - ((Sejmet.nextSpecialJump - Time.realtimeSinceStartup ) / Sejmet.jumpAmount);
    }
    void NormalizeAttack()
    {
        if(Sejmet.waterAtackLevel >= 2)
        {
            attack.fillAmount = Sejmet.waterCannonDuration / Sejmet.attackAmount;
        }
        else if(Sejmet.waterAtackLevel > Sejmet.windAtackLevel)
        {
            attack.fillAmount = Sejmet.GetComponent<SejmetWater>().waterAtackLvl1List.Count / Sejmet.attackAmount;
        }else
        {
            attack.fillAmount = 1 - ((Sejmet.nextSpecialAtack - Time.realtimeSinceStartup) / Sejmet.attackAmount);
        }
        
    }
    void ExperiencePoints()
    {
        exp.text = Level_Manager.Instance.exp.ToString() ;
    }

    public void SetBoosName(string name)
    {
        bossName.text = name;
    }

    public void BossLive()
    {
       bossLive.fillAmount = Level_Manager.Instance.GetBossLive() / 100.0f;
    }
    // Update is called once per frame
    void Update()
    {
        NormalizeLive();
        NormalizeDash();
        NormalizeAttack();
        NormalizeJump();
        ExperiencePoints();
        if (boss.activeInHierarchy)
            BossLive();
    }
}
