using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalEvent : MonoBehaviour
{
    /// <summary>
    /// 「主人公とネコは実は死んでいた」イベントを動かすスクリプト
    /// </summary>
    public GameObject ein;
    public GameObject zwei;
    public GameObject drei;
    public GameObject fier;
    public GameObject funf;
    public bool FESwitch;
    [Header("踏切のSE")] public AudioClip humikiriSE;
    [Header("電車のSE")] public AudioClip trainSE;
    [Header("電車のブレーキSE")] public AudioClip trainBSE;
    [Header("電車のクラクションSE")] public AudioClip trainKSE;
    [Header("プレイヤースクリプト")] public player p;
    [Header("猫のスクリプト")] public catMove c;
    [Header("猫")] public GameObject cat;
    [Header("キャンバス")] public GameObject cvs;
    [Header("BGM")]
    private GameObject BGM1;
    private AudioSource BGM1AudioSource;


    private Image einImage;
    private Image zweiImage;
    private Image dreiImage;
    private Image fierImage;
    private bool einSwitch = false;
    private bool zweiSwitch = false;
    private bool dreiSwitch = false;
    private bool fierSwitch = false;
    private bool funfSwitch = false;

    private float dTime;
    private bool eplgSwitch = false;
    private AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        einImage = ein.GetComponent<Image>();
        zweiImage = zwei.GetComponent<Image>();
        dreiImage = drei.GetComponent<Image>();
        fierImage = fier.GetComponent<Image>();
        BGM1 = GameObject.Find("BGM1");
        BGM1AudioSource = BGM1.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FESwitch)
        {
            dTime += 0.01f;
            if (dTime < 1.0f)
            {
                if (BGM1AudioSource.volume > 0)
                {
                    BGM1AudioSource.volume = BGM1AudioSource.volume * 0.95f;
                }
                if (this.gameObject.GetComponent<Image>().color.a < 1.0f)
                {
                    this.gameObject.GetComponent<Image>().color += new Color(0f, 0f, 0f, 0.05f);
                }
                else if (this.gameObject.GetComponent<Image>().color.r > 0f && dTime > 0.5f)
                {
                    this.gameObject.GetComponent<Image>().color -= new Color(0.05f, 0.05f, 0.05f, 0f);
                }
            }
            else if (dTime >= 1.0f && dTime < 3.0f)
            {
                if (einImage.color.a < 1.0f)
                {
                    einImage.color += new Color(0f, 0f, 0f, 0.05f);
                }
                else if (!einSwitch)
                {
                    einSwitch = true;
                    GamaManager.instance.PlaySE(humikiriSE);
                }
            }
            else if (dTime >= 3.0f && dTime < 3.5f)
            {
                if (einImage.color.a > 0f)
                {
                    einImage.color -= new Color(0f, 0f, 0f, 0.05f);
                }
            }
            else if (dTime >= 3.5f && dTime < 4.5f)
            {
                if (zweiImage.color.a < 1.0f)
                {
                    zweiImage.color += new Color(0f, 0f, 0f, 0.05f);
                }
                else if (!zweiSwitch)
                {
                    zweiSwitch = true;
                }
            }
            else if (dTime >= 6.5f && dTime < 7.0f)
            {
                if (zweiImage.color.a > 0f)
                {
                    zweiImage.color -= new Color(0f, 0f, 0f, 0.05f);
                }
            }
            else if (dTime >= 7.0f && dTime < 7.5f)
            {
                if (dreiImage.color.a < 1.0f)
                {
                    dreiImage.color += new Color(0f, 0f, 0f, 0.05f);
                }
                else if (!dreiSwitch)
                {
                    dreiSwitch = true;
                    GamaManager.instance.PlaySE(trainSE);
                }
            }
            else if (dTime >= 9.5f && dTime < 10.0f)
            {
                if (dreiImage.color.a > 0f)
                {
                    dreiImage.color -= new Color(0f, 0f, 0f, 0.05f);
                }
            }
            else if (dTime >= 10.0f && dTime < 10.5f)
            {
                if (fierImage.color.a < 1.0f)
                {
                    fierImage.color += new Color(0f, 0f, 0f, 0.05f);
                }
                else if (!fierSwitch)
                {
                    fierSwitch = true;
                    GamaManager.instance.PlaySE(trainKSE);
                }
                if (!funfSwitch)
                {
                    funfSwitch = true;
                    GamaManager.instance.PlaySE(trainBSE);
                }
            }
            else if (dTime >= 12.5f && dTime < 13.0f)
            {
                if (fierImage.color.a > 0f)
                {
                    fierImage.color -= new Color(0f, 0f, 0f, 0.05f);
                }
            }
            else if (dTime >= 13.0f)
            {
                c.stop = true;
                cat.transform.position = cvs.transform.position;
                cat.transform.position += new Vector3(1.2f, 1.5f, 0);
                p.eplg = true;
                if (!eplgSwitch)
                {
                    cat.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0f);
                    GamaManager.instance.talkEventSwitch = true;
                    p.textNum = 1;
                    eplgSwitch = true;
                    p.startEpilogue();
                }
            }
        }
    }
}
