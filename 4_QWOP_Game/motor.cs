using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class motor : MonoBehaviour
{
    /// <summary>
    /// 犬の動きを制御するスクリプト
    /// </summary>
    [Header("前足")] public GameObject maeasi;
    [Header("後ろ足")] public GameObject ushiroasi;
    [Header("胴体")] public GameObject doutai;
    private HingeJoint2D maeHJ;
    private HingeJoint2D ushiroHJ;
    private RectTransform doutaiPos;
    public float a;
    public bool goalSwitch = false;
    public GameObject goalText;
    public Text goaltext;
    [Header("スタートボタン")] public GameObject startB;

    public bool isWriting = false;

    [Header("地球の中心座標")] public Vector2 earthPos;

    void Start()
    {
        GameObject st = GameObject.Find("dontdstryOnRoad");
        startscript ss = st.GetComponent<startscript>();
        if (!ss.startSwitch)
        {
            Time.timeScale = 0;
        }
        else
        {
            startB.SetActive(false);
        }
        maeHJ = maeasi.GetComponent<HingeJoint2D>();
        ushiroHJ = ushiroasi.GetComponent<HingeJoint2D>();
        doutaiPos = doutai.GetComponent<RectTransform>();
        goaltext = goalText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var jointmotor = maeHJ.motor;
        var jointmotor2 = ushiroHJ.motor;
        var jointlimits = maeHJ.limits;
        if (Input.GetKey(KeyCode.O) || Input.GetAxis("Horizontal") < 0)
        {
            jointmotor.motorSpeed = a;
        }
        else if (Input.GetKey(KeyCode.P) || Input.GetAxis("Horizontal") > 0)
        {
            jointmotor.motorSpeed = a * -1;
        }
        else
        {
            jointmotor.motorSpeed = 0;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetAxis("Horizontal2") < 0)
        {
            jointmotor2.motorSpeed = a;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetAxis("Horizontal2") > 0)
        {
            jointmotor2.motorSpeed = a * -1;
        }
        else
        {
            jointmotor2.motorSpeed = 0;
        }

        if ((Input.GetKey(KeyCode.R) || Input.GetAxis("Button2") > 0) && !isWriting)
        {
            GameObject st = GameObject.Find("dontdstryOnRoad");
            odioPlayer op = st.GetComponent<odioPlayer>();
            op.reset();
            SceneManager.LoadScene("SampleScene");
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetAxis("Button3") > 0) && !isWriting && startscript.instance.startSwitch && !startscript.instance.isCoolTime)
        {
            switch (startscript.instance.stageNum)
            {
                case 1:
                    startscript.instance.stageNum = 2;
                    break;
                case 2:
                    startscript.instance.stageNum = 3;
                    break;
                case 3:
                    startscript.instance.stageNum = 1;
                    break;
                default:
                    break;
            }
            Debug.Log("現在のステージは" + startscript.instance.stageNum);
            startscript.instance.isCoolTime = true;
            startscript.instance.startRecast();

            GameObject st = GameObject.Find("dontdstryOnRoad");
            odioPlayer op = st.GetComponent<odioPlayer>();
            op.reset();
            SceneManager.LoadScene("SampleScene");
        }

        maeHJ.motor = jointmotor;
        ushiroHJ.motor = jointmotor2;

        if (doutaiPos.anchoredPosition.x >= 310f && !goalSwitch)
        {
            goalSwitch = true;
            goalText.SetActive(true);
        }
        if (goalSwitch)
        {
            float sin = Mathf.Sin(3 * Time.time);
            float sin2 = Mathf.Sin(4 * Time.time);
            float sin3 = Mathf.Sin(5 * Time.time);
            goaltext.color = new Color(sin * 0.5f + 0.5f, sin2 * 0.5f + 0.5f, sin3 * 0.5f + 0.5f);
        }

        if (startscript.instance.stageNum == 3)
        {
            float Xdelta = doutaiPos.anchoredPosition.x - earthPos.x;
            float Ydelta = doutaiPos.anchoredPosition.y - earthPos.y;
            float angle = Mathf.Atan2(Ydelta,Xdelta);
            Physics2D.gravity = new Vector3(-1f * Mathf.Cos(angle), -1f * Mathf.Sin(angle), 0);
            //Debug.Log("重力操作中");
        }
        else
        {
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
        }
    }
    
}
