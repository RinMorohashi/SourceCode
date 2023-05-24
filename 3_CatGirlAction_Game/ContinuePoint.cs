using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePoint : MonoBehaviour
{
    /// <summary>
    /// セーブポイント、宝箱の動作を制御するスクリプト
    /// </summary>
    [Header("コンティニュー番号")] public int continueNum;
    [Header("音")] public AudioClip se;
    [Header("プレイヤー判定")] public PlayerTriggerCheck trigger;
    [Header("スピード")] public float speed = 3.0f;
    [Header("動く幅")] public float moveDis = 3.0f;
    [Header("中間地点用テキスト")] public GameObject savePoint;
    [Header("宝1ならオンにする")] public bool isTreasure1;
    [Header("宝2ならオンにする")] public bool isTreasure2;

    private bool on = false;
    private float kakudo = 0.0f;
    private Vector3 defaultPos;
    private Text saveText;
    private bool saveSwitch = false;
    private SpriteRenderer sr;
    private Animator anim = null;
    void Start()
    {
        //初期化
        if (trigger == null)
        {
            Debug.Log("インスペクターの設定が足りません");
            Destroy(this);
        }
        defaultPos = transform.position;

        savePoint.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("continueFlag");
        }

        if (isTreasure1 && GamaManager.instance.airialAttackSwitch)
        {
            GameObject go = GameObject.Find("continuePointTreasure");
            go.SetActive(false);
        }
        if (isTreasure2 && GamaManager.instance.zweiJumpSwitch)
        {
            GameObject go = GameObject.Find("continuePointTreasure");
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが範囲内に入った
        if (trigger.isOn && !on)
        {
            if (!isTreasure1 && !isTreasure2)
            {
                GamaManager.instance.continueNum = continueNum;
            }
            else if (isTreasure1 == true)
            {
                GamaManager.instance.airialAttackSwitch = true;
            }
            else if (isTreasure2 == true)
            {
                GamaManager.instance.zweiJumpSwitch = true;
            }
            on = true;
            if (GamaManager.instance.stageNum == 1 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 2 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 2 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 3 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 3 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 4 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 4 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
        }

        if (on)
        {
            if (kakudo < 180.0f)
            {
                //sinカーブで振動させる
                transform.position = defaultPos + Vector3.up * moveDis * Mathf.Sin(kakudo * Mathf.Deg2Rad);

                //途中からちっちゃくなる
                if (kakudo > 90.0f)
                {
                    transform.localScale = Vector3.one * (1 - ((kakudo - 90.0f) / 90.0f));
                }
                kakudo += 180.0f * Time.deltaTime * speed;
            }
            else
            {
                sr.color = new Vector4(1f,1f,1f,0f);
                on = false;
            }
        }
    }

    IEnumerator saved()
    {
        GamaManager.instance.PlaySE(se);
        savePoint.SetActive(true);
        for (int i = 0; i < 60; i++)
        {
            savePoint.transform.position += new Vector3(0f, 0.01f, 0f);
            yield return new WaitForSeconds(0.03f);
        }
        savePoint.SetActive(false);
        savePoint.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -200f, 0);
    }
}
