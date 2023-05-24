using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour
{
    /// <summary>
    /// スキルの取得状況や音量、BGMの管理を行うスクリプト
    /// </summary>
    public static GamaManager instance = null;

    public int score;
    public int stageNum;
    public int continueNum;
    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public bool isStageClear = false;
    [Header("オンなら空中攻撃ができる")] public bool airialAttackSwitch;
    [Header("オンなら２段ジャンプができる")] public bool zweiJumpSwitch;

    private AudioSource audioSource = null;
    public bool goBackSwitch;
    public bool talkEventSwitch = false;
    public bool eventSwitch1to2 = false;
    public bool eventSwitch2to3 = false;
    public bool eventSwitch3to4 = false;
    public bool eventSwitch4to5 = false;
    public bool eventSwitch2to1 = false;
    public bool eventSwitch3to2 = false;
    public bool eventSwitch4to3 = false;
    public bool eventSwitch5to4 = false;
    public bool stage5EventSwitch = false;
    public bool firstTimeReachedStage1 = true;
    public bool firstTimeReachedStage2 = true;
    public bool firstTimeReachedStage3 = true;
    public bool firstTimeReachedStage4 = true;
    public bool firstTimeReachedStage5 = true;
    public bool firstTimeReachedStage1zwei = true;
    public bool firstTimeReachedStage2zwei = true;
    public bool firstTimeReachedStage3zwei = true;
    public bool firstTimeReachedStage4zwei = true;
    public bool firstTimeReachedStage5zwei = true;
    public bool trueEndSwitch = false;
    public bool vanguard = false;
    public bool badEnd = false;
    public bool trueEnd = false;
    public float BGMVolume = 0.5f;
    public float SEVolume = 0.5f;
    [Header("Stage1のBGM")] public AudioSource BGM1;
    [Header("Stage2のBGM")] public AudioSource BGM2;
    [Header("Stage3のBGM")] public AudioSource BGM3;
    [Header("Stage4のBGM")] public AudioSource BGM4;
    [Header("Stage5のBGM")] public AudioSource BGM5;

    public int windowSizeNum = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    /// <summary>
    /// 最初から始める時の処理
    /// </summary>
    public void RetryGame()
    {
        isGameOver = false;
        score = 0;
        stageNum = 1;
        continueNum = 0;
        goBackSwitch = false;
        airialAttackSwitch = false;
        zweiJumpSwitch = false;
        talkEventSwitch = true;
        eventSwitch1to2 = false;
        eventSwitch2to3 = false;
        eventSwitch3to4 = false;
        eventSwitch4to5 = false;
        eventSwitch5to4 = false;
        eventSwitch4to3 = false;
        eventSwitch3to2 = false;
        eventSwitch2to1 = false;
        stage5EventSwitch = false;
        firstTimeReachedStage1 = true;
        firstTimeReachedStage2 = true;
        firstTimeReachedStage3 = true;
        firstTimeReachedStage4 = true;
        firstTimeReachedStage5 = true;
        firstTimeReachedStage1zwei = true;
        firstTimeReachedStage2zwei = true;
        firstTimeReachedStage3zwei = true;
        firstTimeReachedStage4zwei = true;
        firstTimeReachedStage5zwei = true;
        trueEndSwitch = false;
        vanguard = false;
        stageNum = 0;
    }

    public void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        switch (stageNum)
        {
            case 0:
                BGM1.Stop();
                BGM2.Stop();
                BGM3.Stop();
                BGM4.Stop();
                BGM5.Stop();
                break;
            case 1:
                BGM1.Play();
                BGM2.Stop();
                BGM3.Stop();
                BGM4.Stop();
                BGM5.Stop();
                break;
            case 2:
                BGM1.Stop();
                BGM2.Play();
                BGM3.Stop();
                BGM4.Stop();
                BGM5.Stop();
                break;
            case 3:
                BGM1.Stop();
                BGM2.Stop();
                BGM3.Play();
                BGM4.Stop();
                BGM5.Stop();
                break;
            case 4:
                BGM1.Stop();
                BGM2.Stop();
                BGM3.Stop();
                BGM4.Play();
                BGM5.Stop();
                break;
            case 5:
                BGM1.Stop();
                BGM2.Stop();
                BGM3.Stop();
                BGM4.Stop();
                BGM5.Play();
                break;
            default:
                break;
        }
        GameObject p = GameObject.Find("W1");
        AudioSource pas = p.GetComponent<AudioSource>();
        pas.volume = SEVolume;
        GameObject pcat = GameObject.Find("CatBody");
        AudioSource pascat = pcat.GetComponent<AudioSource>();
        pascat.volume = SEVolume;
    }
}
