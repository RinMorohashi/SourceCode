using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class TimerController : MonoBehaviour
{
    /// <summary>
    /// クリア時間を計測するスクリプト
    /// </summary>
    public Text timerText;
    public float totalTime;
    float seconds;
    public motor gamemanager;
    private bool timerStopSwitch = false;
    public GameObject gotorankbutton;

    public motor MotorScript;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gamemanager.goalSwitch)
        {
            totalTime += Time.deltaTime;
            seconds = totalTime;
            timerText.text = "TIME : " + seconds.ToString("N2");
        }
        else if (!timerStopSwitch)
        {
            timerStopSwitch = true;
            gotorankbutton.SetActive(true);
        }
    }

    public void OnClickRankingButton()
    {
        MotorScript.isWriting = true;
        switch (startscript.instance.stageNum)
        {
            case 1:
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(seconds, 0);
                break;
            case 2:
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(seconds, 1);
                break;
            case 3:
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(seconds, 2);
                break;
            default:
                Debug.Log("ステージナンバーがおかしい");
                break;
        }       
    }
}
