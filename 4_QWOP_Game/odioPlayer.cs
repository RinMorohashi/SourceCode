using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class odioPlayer : MonoBehaviour
{
    /// <summary>
    /// BGMを変更するスクリプト
    /// </summary>
    private AudioSource audioSource = null;
    [Header("カノン")] public AudioClip canonSE;
    [Header("ボレロ")] public AudioClip boleroSE;
    [Header("ハンガリー舞曲第５番")] public AudioClip hangarian5SE;
    [Header("G線上のアリア")] public AudioClip GSE;
    [Header("月光")] public AudioClip moonSE;
    [Header("アイネ・クライネ・ナハトムジーク")] public AudioClip musikSE;
    [Header("ウィリアム・テル序曲")] public AudioClip WilliamSE;
    [Header("幻想即興曲")] public AudioClip fantasySE;
    [Header("子犬のワルツ")] public AudioClip minuteWaltzSE;
    [Header("アメイジング・グレイス")] public AudioClip amazingSE;
    public bool isPushed = false;
    public int musicNumber = 0;
    public GameObject bgmText;
    private RectTransform bgmTextPos;
    private Text bgmt;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgmText = GameObject.Find("BGMText");
        if (bgmText == null)
        {
            Debug.Log("bgmTextが見つかんない");
        }
        bgmTextPos = bgmText.GetComponent<RectTransform>();
        bgmt = bgmText.GetComponent<Text>();
        canon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && !isPushed)
        {
            Debug.Log("BGMを変更するよ！");
            isPushed = true;
            musicNumber++;
            StartCoroutine("recast");
            StartCoroutine("musicBox");
        }
    }

    IEnumerator recast()
    {
        bgmTextPos.anchoredPosition = new Vector3(400f, 34.3f, 0f);
        for (int i = 0; i < 20; i++)
        {
            bgmTextPos.anchoredPosition = new Vector3(400f - i, 34.3f, 0f);
            yield return new WaitForSeconds(0.025f);
        }
        
        isPushed = false;
    }

    IEnumerator musicBox()
    {
        switch (musicNumber)
        {
            case 0:
                canon();
                bgmt.text = "♪カノン（パッヘルベル）";
                break;
            case 1:
                bolero();
                bgmt.text = "♪ボレロ（ラヴェル）";
                break;
            case 2:
                hungarian5();
                bgmt.text = "♪ハンガリー舞曲第５番（ブラームス）";
                break;
            case 3:
                GAria();
                bgmt.text = "♪Ｇ線上のアリア（バッハ）";
                break;
            case 4:
                moon();
                bgmt.text = "♪月光（ベートーヴェン）";
                break;
            case 5:
                musik();
                bgmt.text = "♪アイネ・クライネ・ナハトムジーク\n（モーツァルト）";
                break;
            case 6:
                William();
                bgmt.text = "♪ウィリアムテル序曲（ロッシーニ）";
                break;
            case 7:
                fantasy();
                bgmt.text = "♪幻想即興曲（ショパン）";
                break;
            case 8:
                minuteWaltz();
                bgmt.text = "♪子犬のワルツ（ショパン）";
                break;
            case 9:
                amazing();
                bgmt.text = "♪アメイジンググレイス\n（ジョン・ニュートン）";
                musicNumber = -1;
                break;
            default:
                break;
        }
        yield return null;
    }

    public void canon()
    {
        audioSource.Stop();
        audioSource.clip = canonSE;
        audioSource.time = 88.3f;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
    public void bolero()
    {
        audioSource.Stop();
        audioSource.clip = boleroSE;
        audioSource.time = 10f;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
    public void hungarian5()
    {
        audioSource.Stop();
        audioSource.clip = hangarian5SE;
        audioSource.time = 0f;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
    public void GAria()
    {
        audioSource.Stop();
        audioSource.clip = GSE;
        audioSource.time = 0f;
        audioSource.Play();
    }
    public void moon()
    {
        audioSource.Stop();
        audioSource.clip = moonSE;
        audioSource.time = 0f;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
    public void musik()
    {
        audioSource.Stop();
        audioSource.clip = musikSE;
        audioSource.time = 0f;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
    public void William()
    {
        audioSource.Stop();
        audioSource.clip = WilliamSE;
        audioSource.time = 500.7f;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
    public void fantasy()
    {
        audioSource.Stop();
        audioSource.clip = fantasySE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
    public void minuteWaltz()
    {
        audioSource.Stop();
        audioSource.clip = minuteWaltzSE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
    public void amazing()
    {
        audioSource.Stop();
        audioSource.clip = amazingSE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }


    public void reset()
    {
        StartCoroutine("Resetting");
    }

    IEnumerator Resetting()
    {
        yield return new WaitForSeconds(0.5f);
        bgmText = GameObject.Find("BGMText");
        bgmTextPos = bgmText.GetComponent<RectTransform>();
        bgmt = bgmText.GetComponent<Text>();
        switch (musicNumber)
        {
            case 0:
                bgmt.text = "♪カノン（パッヘルベル）";
                break;
            case 1:
                bgmt.text = "♪ボレロ（ラヴェル）";
                break;
            case 2:
                bgmt.text = "♪ハンガリー舞曲第５番（ブラームス）";
                break;
            case 3:
                bgmt.text = "♪Ｇ線上のアリア（バッハ）";
                break;
            case 4:
                bgmt.text = "♪月光（ベートーヴェン）";
                break;
            case 5:
                bgmt.text = "♪アイネ・クライネ・ナハトムジーク\n（モーツァルト）";
                break;
            case 6:
                bgmt.text = "♪ウィリアムテル序曲（ロッシーニ）";
                break;
            case 7:
                bgmt.text = "♪幻想即興曲（ショパン）";
                break;
            case 8:
                bgmt.text = "♪子犬のワルツ（ショパン）";
                break;
            case -1:
                bgmt.text = "♪アメイジンググレイス\n（ジョン・ニュートン）";
                break;
            default:
                break;
        }
    }
}
