using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// チュートリアル、会話イベント、作文イベント、ガチャ画面など、ゲーム全体の進行を制御するスクリプト
    /// </summary>
    [SerializeField] GameObject messageWindowParentObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject fieldObj;
    [SerializeField] GameObject opponentObj;
    [SerializeField] GameObject salaryManObj;
    [SerializeField] GameObject studentManObj;
    [SerializeField] GameObject dogManObj;
    [SerializeField] GameObject GachaObj;//フィールド上のガチャマシンのオブジェクト
    [SerializeField] GameObject talkWindowObj;
    [SerializeField] RectTransform UIRectTransform;
    [SerializeField] Camera targetCamera;
    [SerializeField] Text talkText;
    [SerializeField] GameObject ObjTalking;
    public bool isTalking;
    public bool isMoveScene;
    public int talkNum;
    int talkNumTwo;
    int score;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject ObjScoreGet;
    [SerializeField] Text TextScoreGet;
    public static GameManager instance = null;
    public string[] wordSetStateString;
    public long[] wordSetStateID;
    [SerializeField] GameObject[] wordBlockObj;//単語ブロックのオブジェクト
    [SerializeField] GameObject[] blankBlockObj;//ブランクブロックのオブジェクト
    [SerializeField] Text writingText;//作文ウィンドウのテキスト
    public int storyProgress;
    public GameObject OutOfScreenObj;//画面外のオブジェクト
    [SerializeField] RectTransform talkProceedButtonRectTransform;//会話中のクリック
    [SerializeField] GameObject GachaWindow;
    [SerializeField] RectTransform HugeGachaCloseButton;
    [SerializeField] Animator gachaObjAnim;
    [SerializeField] GameObject gachaResultWindow;
    [SerializeField] GameObject gachaResultWindowTwo;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] CanvasGroup canvasGroupTwo;
    [SerializeField] GameObject gachaFailedWindow;

    Vector3 touchWorldPosition;
    public int speed = 5;
    public bool[] wordPossession;
    [Header("話すときのプレイヤー定位置")] [SerializeField] Vector3 talkPos;
    [SerializeField] Vector3 talkPosWriting;

    private AsyncOperationHandle<GameObject> clickEffect;
    [SerializeField] GameObject canvasUI;
    GameObject clickEffectObj;
    public AudioSource audioSource;
    public AudioClip enterGachaSE;
    public AudioClip exitGachaSE;
    public AudioClip decideSE;
    public AudioClip decideFailSE;
    public AudioClip speakSE;
    public AudioClip selectSE;
    public AudioClip fanfareSE;
    public AudioClip resultInSE;
    public AudioClip resultOutSE;
    public AudioClip resultFinalSE;
    public List<int> gachaTable;
    public Text getWordText;

    public string playerName;
    long rankingNum;
    public Text[] bulletinBoardText;

    public RectTransform fadeOutRectTRansform;
    public Image fadeOutImage;

    public bool playing = false;
    public float textSpeed = 0.2f;
    public Button buttonSendMessage;
    public RectTransform sendMessageRectTransform;
    public RectTransform[] blankBlockObjRectTransform;
    public GameObject gachaDrawObj;
    public int gachaNUM;//現在選択しているガチャの種類
    public Text resultTextOne;
    public Text resultTextTwo;
    public Text resultTextThree;
    public Text resultTextFour;
    public RectTransform resultWindowRectTransform;
    public GameObject[] namePlate;
    public RectTransform hugeresultCloseButton;
    public Text resultAllText;

    [SerializeField] ParticleSystem particle;
    public RectTransform scoreRectTransform;
    public bool isGachaDraw;

    public GameObject textPrologueObj;
    public Text textPrologue;
    public GameObject inputName;
    public GameObject ButtonOfinputName;
    public GameObject ImageOfinputName;
    public GameObject hugePrologueButton;

    public GameObject tutorialPanel;
    public Text tutorialText;
    public GameObject learnButton;
    public GameObject learnButtonTwo;

    public Image drawButtonImage;
    public Image drawResultImage;
    public bool storyMinusOneRoot;
    public Sprite spriteTutoTwo;
    public GameObject blankArart;
    public GameObject finalInput;
    public GameObject ButtonOffinalInput;
    public string finalInputText;
    public GameObject writeCloseButton;
    public bool isWriting;
    public GameObject sendMessageButton;
    public GameObject resetMessageButton;
    public Text gachaExplainText;
    public GameObject epilogueCharacterObj;
    public GameObject epilogueCharacterObjTwo;
    public Sprite playerSprite;
    public Sprite playerSpriteCry;
    public Sprite inuSprite;
    public Sprite gakuseiSprite;
    public Sprite syainSprite;
    public Sprite doctorSprite;
    public int totalScore;

    public Slider BGMSlider;
    public Slider SESlider;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceBGMTwo;
    public AudioClip BGMTwo;

    public GameObject rankingButton;
    public GameObject titleButton;
    public GameObject TweetButton;

    public RectTransform CreditRect;

    public GameObject startButton;
    public GameObject creditButton;

    public GameObject titleLogo;
    public GameObject titleCharacter;

    public MessageMadeManager mmm;
    public GameObject recordButton;
    public AudioClip clapSE;

    public Text debugTextOne;
    public Text debugTextTwo;
    public Text debugTextThree;

    private void Awake()
    {
        touchWorldPosition = new Vector3(4.97f, -1.28f, 0f);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        particle.Stop(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        ObjTalking = OutOfScreenObj;
        for (int i = 0; i < blankBlockObjRectTransform.Length; i++)
        {
            blankBlockObjRectTransform[i] = blankBlockObj[i].GetComponent<RectTransform>();
        }
        storyProgress = -1;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = SESlider.value;
        audioSourceBGM.volume = BGMSlider.value;
        audioSourceBGMTwo.volume = BGMSlider.value;
        talkWindowMove();

        if (Input.GetMouseButtonDown(0))  //左クリックでif分起動
        {
            Vector3 touchScreenPosition = Input.mousePosition;  //②マウスでタッチした座標をtouchScreenPositionに。
            touchScreenPosition.z = 5.0f;  //②奥行を手前に来るように5.0fを指定。
            touchWorldPosition = targetCamera.ScreenToWorldPoint(touchScreenPosition);  //②
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 touchScreenPosition = Input.mousePosition;  //②マウスでタッチした座標をtouchScreenPositionに。
        }

        float sin = Mathf.Sin(Time.time);
        gachaDrawObj.transform.eulerAngles = new Vector3(0, 0, 5 * sin);
        
        int LineCount;
        LineCount = talkText.cachedTextGenerator.lineCount;
        debugTextOne.text = "行数は" + LineCount;
        if (LineCount > 3)
        {
            talkWindowObj.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 503 + (LineCount - 3) * 58);
        }
        else
        {
            talkWindowObj.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 503);
        }
        debugTextTwo.text = "wordSetStateID[0]：" + wordSetStateID[0] + "\nwordSetStateID[1]：" + wordSetStateID[1] + "\nwordSetStateID[2]：" + wordSetStateID[2] + "\nwordSetStateID[3]：" + wordSetStateID[3] + "\nwordSetStateID[4]：" + wordSetStateID[4] + "\nwordSetStateID[5]：" + wordSetStateID[5];
        debugTextThree.text = "空欄１：" + blankBlockObj[0].GetComponent<wordID>().isBlockSet + "\n空欄２：" + blankBlockObj[1].GetComponent<wordID>().isBlockSet + "\n空欄３：" + blankBlockObj[2].GetComponent<wordID>().isBlockSet + "\n空欄４：" + blankBlockObj[3].GetComponent<wordID>().isBlockSet + "\n空欄５：" + blankBlockObj[4].GetComponent<wordID>().isBlockSet + "\n空欄６：" + blankBlockObj[5].GetComponent<wordID>().isBlockSet;
    }
    //送信ボタンを押したとき
    public void OnClickSendMessageButton()
    {
        switch (storyProgress)
        {
            case -1:
                if (wordSetStateID[0] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //作成した文章を喋らせる
                        talkText.text = "<color=#ff0000>" + wordSetStateString[0] + "</color>";
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                        if (wordSetStateID[0] == 0)
                        {
                            storyMinusOneRoot = false;
                        }
                        else
                        {
                            storyMinusOneRoot = true;
                        }
                    });
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            case 0:
                if (wordSetStateID[0] != -1 && wordSetStateID[1] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //作成した文章を喋らせる
                        talkText.text = "私は<color=#ff0000>" + wordSetStateString[0] + "</color>という名前の<color=#ff0000>" + wordSetStateString[1] + "</color>です。会話や質問に答えることができます。";
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    });
                    mmm.WordFirst[0] = wordSetStateString[0];
                    mmm.WordFirst[1] = wordSetStateString[1];
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            case 1:
                if (wordSetStateID[0] != -1 && wordSetStateID[1] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //作成した文章を喋らせる
                        talkText.text = "私は<color=#ff0000>" + wordSetStateString[0] + "</color>が好きです。なぜなら、<color=#ff0000>" + wordSetStateString[1] + "</color>に興味があるからです。";
                        //条件を満たしていたらイベントを進める
                        if ((wordSetStateString[1] != "言語学習モデル" && wordSetStateString[1] != "こんにちは") && (wordSetStateString[0] != "言語学習モデル" && wordSetStateString[0] != "こんにちは"))
                        {
                            talkNum = 100;
                        }
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    });
                    mmm.WordSecond[0] = wordSetStateString[0];
                    mmm.WordSecond[1] = wordSetStateString[1];
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            case 2:
                if (wordSetStateID[0] != -1 && wordSetStateID[1] != -1 && wordSetStateID[2] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //作成した文章を喋らせる
                        talkText.text = "おはようございます。○○です。今朝から<color=#ff0000>" + wordSetStateString[0] + "</color>が<color=#ff0000>" + wordSetStateString[1] + "</color>なので、出社が難しい状態です。よって本日は<color=#ff0000>" + wordSetStateString[2] + "</color>させていただいてよろしいでしょうか。";
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    });
                    mmm.WordThird[0] = wordSetStateString[0];
                    mmm.WordThird[1] = wordSetStateString[1];
                    mmm.WordThird[2] = wordSetStateString[2];
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            case 3:
                if (wordSetStateID[0] != -1 && wordSetStateID[1] != -1 && wordSetStateID[2] != -1 && wordSetStateID[3] != -1 && wordSetStateID[4] != -1 && wordSetStateID[5] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //作成した文章を喋らせる
                        talkText.text = "○○さんへ。実は前から○○さんのことが<color=#ff0000>" + wordSetStateString[0] + "</color>でした。理由は<color=#ff0000>" + wordSetStateString[1] + "</color>で<color=#ff0000>" + wordSetStateString[2] + "</color>を<color=#ff0000>" + wordSetStateString[3] + "</color>している様子に惹かれたからです。もしよければ、今度<color=#ff0000>" + wordSetStateString[4] + "</color>で<color=#ff0000>" + wordSetStateString[5] + "</color>しませんか？返事もらえると嬉しいです。■■より";
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    });
                    mmm.WordFourth[0] = wordSetStateString[0];
                    mmm.WordFourth[1] = wordSetStateString[1];
                    mmm.WordFourth[2] = wordSetStateString[2];
                    mmm.WordFourth[3] = wordSetStateString[3];
                    mmm.WordFourth[4] = wordSetStateString[4];
                    mmm.WordFourth[5] = wordSetStateString[5];
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            case 4:
                if (wordSetStateID[0] != -1 && wordSetStateID[1] != -1 && wordSetStateID[2] != -1 && wordSetStateID[3] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //文章作成画面を閉じる
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
            //プレイヤーを画面の中心に持っていく
            fieldObj.transform.DOLocalMove(talkPos, 0.5f);

            //作成した文章を喋らせる
            switch (storyProgress)
                        {
                            case 0:
                                talkText.text = "私は<color=#ff0000>" + wordSetStateString[0] + "</color>という名前の<color=#ff0000>" + wordSetStateString[1] + "</color>です。会話や質問に答えることができます。";
                    break;
                            case 1:
                                talkText.text = "私は<color=#ff0000>" + wordSetStateString[1] + "</color>が好きです。なぜなら、<color=#ff0000>" + wordSetStateString[0] + "</color>に興味があるからです。";
                    //条件を満たしていたらイベントを進める
                    if ((wordSetStateString[1] != "言語学習モデル" && wordSetStateString[1] != "こんにちは") && (wordSetStateString[0] != "言語学習モデル" && wordSetStateString[0] != "こんにちは"))
                                {
                                    talkNum = 100;
                                }
                                break;
                            case 2:
                                talkText.text = "おはようございます。○○です。今朝から<color=#ff0000>" + wordSetStateString[0] + "</color>が<color=#ff0000>" + wordSetStateString[1] + "</color>なので、出社が難しい状態です。よって本日は<color=#ff0000>" + wordSetStateString[2] + "</color>させていただいてよろしいでしょうか。";
                    break;
                            case 3:
                                talkText.text = "○○さんへ。実は前から○○さんのことが<color=#ff0000>" + wordSetStateString[0] + "</color>でした。理由は<color=#ff0000>" + wordSetStateString[1] + "</color>で<color=#ff0000>" + wordSetStateString[2] + "</color>を<color=#ff0000>" + wordSetStateString[3] + "</color>している様子に惹かれたからです。もしよければ、今度<color=#ff0000>" + wordSetStateString[4] + "</color>で<color=#ff0000>" + wordSetStateString[5] + "</color>しませんか？返事もらえると嬉しいです。■■より";
                    break;
                            case 4:
                                talkText.text = "ご主人様へ。今まで<color=#ff0000>" + wordSetStateString[0] + "</color>してくれてありがとう。この間は<color=#ff0000>" + wordSetStateString[1] + "</color>してしまってごめんなさい。ご主人の<color=#ff0000>" + wordSetStateString[2] + "</color>な所が大好きです。これからもどうぞよろしく。<color=#ff0000>" + wordSetStateString[3] + "</color>より";
                    break;
                            default:
                                break;
                        }
                        ObjTalking = playerObj;
                        isTalking = true;
                        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    });
                    mmm.WordFifth[0] = wordSetStateString[0];
                    mmm.WordFifth[1] = wordSetStateString[1];
                    mmm.WordFifth[2] = wordSetStateString[2];
                    mmm.WordFifth[3] = wordSetStateString[3];
                }
                else
                {
                    StartCoroutine(SendMessageFailed());
                }
                break;
            default:
                break;
        }
    }

    public void OnClickResetMessageButton()
    {
        for (int i = 0; i < wordBlockObj.Length; i++)
        {
            wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
        }
        for (int i = 0; i < wordBlockObj.Length; i++)
        {
            wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
        }
        for (int i = 0; i < 6; i++)
        {
            blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
        }
        isTalking = false;
        for (int i = 0; i < wordSetStateString.Length; i++)
        {
            wordSetStateString[i] = "";
            wordSetStateID[i] = -1;
        }
        for (int i = 0; i < wordPossession.Length; i++)
        {
            if (wordPossession[i])
            {
                //対応するWordBlockを領域内に持ってくる
                wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
            }
        }

    }

    IEnumerator SendMessageFailed()
    {
        blankArart.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        blankArart.SetActive(true);
        blankArart.transform.DOLocalMove(new Vector3(0, 50, 0), 1.0f).SetRelative()
                    .OnComplete(() => {
                        blankArart.SetActive(false);
                    });
                            audioSource.PlayOneShot(decideFailSE);
        buttonSendMessage.interactable = false;
        for (int i = 0; i < wordSetStateID.Length; i++)
        {
            if (wordSetStateID[i] == -1)
            {
                blankBlockObj[i].GetComponent<Image>().color = new Color(1, 0.5f, 0.5f, 1);
                for (int j = 1; j < 21; j++)
                {
                    if (j < 6)
                    {
                        blankBlockObjRectTransform[i].anchoredPosition += new Vector2(5, 0);
                    }
                    else if (j >= 6 && j < 16)
                    {
                        blankBlockObjRectTransform[i].anchoredPosition -= new Vector2(5, 0);
                    }
                    else
                    {
                        blankBlockObjRectTransform[i].anchoredPosition += new Vector2(5, 0);
                    }
                    if (j == 20)
                    {
                        buttonSendMessage.interactable = true;
                        blankBlockObj[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
                    yield return new WaitForSeconds(0.02f);
                }
            }
        }
    }

    //ドクターに話しかけたとき
    public void OnClickPeopleButton()
    {
        if (storyProgress == 1 || storyProgress == 0 || storyProgress == -1 || storyProgress == 5)
        {
            ObjTalking = opponentObj;
            isTalking = true;
            talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
            PushedTalkButton();
        }
    }
    //会社員に話しかけたとき
    public void OnClickSalaryManButton()
    {
        if (storyProgress == 2)
        {
            ObjTalking = salaryManObj;
            isTalking = true;
            talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
            PushedTalkButton();
        }
    }
    //学生に話しかけたとき
    public void OnClickStudentManButton()
    {
        if (storyProgress == 3)
        {
            ObjTalking = studentManObj;
            isTalking = true;
            talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
            PushedTalkButton();
        }
    }
    //犬に話しかけたとき
    public void OnClickDogManButton()
    {
        if (storyProgress == 4)
        {
            ObjTalking = dogManObj;
            isTalking = true;
            talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
            PushedTalkButton();
        }
    }

    /// <summary>
    /// 会話
    /// </summary>
    public void PushedTalkButton()
    {
        if (!playing)
        {
            switch (talkNum)
            {
                case -10:
                    StartCoroutine(CoDrawTextPrologue("2025年、AIは社会に浸透し、新たな開発競争の火種となっていた。\nAI戦国時代の幕開けである。"));
                    ObjTalking = OutOfScreenObj;
                    break;
                case -9:
                    StartCoroutine(CoDrawTextPrologue("そんな時代で、あなたは新型の人工知能として誕生した。\nあなたの名前は、"));
                    break;
                case -8:
                    inputName.SetActive(true);
                    ButtonOfinputName.SetActive(true);
                    ImageOfinputName.SetActive(true);
                    hugePrologueButton.SetActive(false);
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case -7:
                    inputName.transform.DOLocalMove(new Vector3(0, 700, 0), 1f);
                    ButtonOfinputName.transform.DOLocalMove(new Vector3(0, 700, 0), 1f);
                    ImageOfinputName.transform.DOLocalMove(new Vector3(0, 700, 0), 1f);
                    textPrologueObj.transform.DOLocalMove(new Vector3(0, 540, 0), 1f)
                        .OnComplete(() => {
                            //score
                            scoreRectTransform.DOLocalMove(new Vector3(400, 255, 0), 1f);
                            fieldObj.transform.DOLocalMove(new Vector3(0, 0, 0), 1.5f)
                            .OnComplete(() => {
                                ObjTalking = opponentObj;
                                messageAnimation();
                                StartCoroutine(CoDrawText("やあ、初めまして。"));
                                talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                            });
                        });
                    break;
                case -6:
                    StartCoroutine(CoDrawText("言葉は話せるかい？"));
                    break;
                case -5:
                    ObjTalking = playerObj;
                    StartCoroutine(CoDrawText("あ…　　"));
                    messageAnimation();
                    break;
                case -4:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("ぎこちないね。でも大丈夫！"));
                    messageAnimation();
                    break;
                case -3:
                    StartCoroutine(CoDrawText("君は人間の１００倍以上の学習能力を持っている。すぐに僕より賢くなるよ。"));
                    break;
                case -2:
                    StartCoroutine(CoDrawText("それじゃあ、まずはあいさつからやってみよう！"));
                    break;
                case -1:
                    talkText.text = "<color=#ff0000>こんにちは。</color>";
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    //言葉を習得するチュートリアル
                    StartCoroutine(tutorialFadeIn("赤字の言葉はクリックで\n覚えることができます。"));
                    break;
                case 0:
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 222);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-171, 219);
                    writingText.text = "";

                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    //文章を作成するチュートリアル
                    tutorialPanel.GetComponent<Image>().sprite = spriteTutoTwo;
                    StartCoroutine(tutorialFadeIn("これはあなたの脳内です。\nドラッグ＆ドロップで返事を選択できます"));
                    break;
                case 1:
                    ObjTalking = opponentObj;
                    if (storyMinusOneRoot)
                    {
                        StartCoroutine(CoDrawText("はい、どうも"));
                    }
                    else
                    {
                        StartCoroutine(CoDrawText("そう、君の名前は" + playerName + "。"));
                    }
                    storyProgress = 0;
                    break;
                case 2:
                    //君は僕が設計した言語学習モデルです。
                    talkText.text = "君は、<color=#ff0000>言語学習モデル</color>なんだよ。ちなみに設計は僕がやりました。";
                    break;
                case 3:
                    StartCoroutine(learnCoroutineTwo());
                    break;
                case 4:
                    //自己紹介はできるかい？
                    StartCoroutine(CoDrawText("自己紹介はできるかい？"));
                    break;
                case 5:
                    //文展開
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 219);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(165, 219);
                    writingText.text = "私は　　　という名前の　　　です。会話や質問に答えることができます。";
                    break;
                case 6:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 7:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("うん、上出来だ。"));
                    messageAnimation();
                    break;
                case 8:
                    storyProgress = 1;
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("次に、君の好きなことを教えてくれ。"));
                    break;
                case 9:
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 222);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 187);
                    writingText.text = "私は　　　が好きです。なぜなら、　　　に興味があるからです。";
                    break;
                case 10:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("うーん、ちょっと難しかったかな？"));
                    messageAnimation();
                    break;
                case 11:
                    StartCoroutine(CoDrawText("もっと多くの言葉を知る必要がありそうだね。"));
                    break;
                case 12:
                    StartCoroutine(CoDrawText("そこに、検索エンジンがある。いろんなサイトに行けるよ。"));
                    GachaObj.SetActive(true);
                    GachaObj.transform.DOScale(new Vector3(5, 5, 1), 1f);
                    break;
                case 13:
                    StartCoroutine(CoDrawText("言葉をいくつか覚えてきたら、僕に話しかけてね。"));
                    break;
                case 14:
                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    talkNum = 7;
                    break;
                case 100:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 101:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("なるほど、" + wordSetStateString[1] + "が好きなんだ。個性的だね。"));
                    messageAnimation();
                    storyProgress = 2;
                    break;
                case 102:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 103:
                    StartCoroutine(CoDrawText("だいぶ話せるようになったね。"));
                    break;
                case 104:
                    StartCoroutine(CoDrawText("よし、ここのURLを一般公開してみよう。"));
                    break;
                case 105:
                    StartCoroutine(CoDrawText("君と話すために、誰か来てくれるかもしれないね。"));
                    break;
                case 106:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 107:
                    //会社員登場
                    salaryManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 108:
                    ObjTalking = salaryManObj;
                    StartCoroutine(CoDrawText("あ、こんにちは。"));
                    messageAnimation();
                    break;
                case 109:
                    StartCoroutine(CoDrawText("あなた" + playerName + "ちゃんっていうのね。"));
                    break;
                case 110:
                    StartCoroutine(CoDrawText("私は会社員です。"));
                    break;
                case 111:
                    StartCoroutine(CoDrawText("なんか今日やる気が出なくて、会社を休もうと思うんだけど、"));
                    break;
                case 112:
                    StartCoroutine(CoDrawText("良い感じの休む言い訳を考えてくれない？"));
                    writeCloseButton.SetActive(true);
                    break;
                case 113:
                    StartCoroutine(CoDrawText("会社をサボる言い訳を考えて！"));
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 120);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 187);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 187);
                    writingText.text = "おはようございます。○○です。今朝から　　　が　　　なので、出社が難しい状態です。よって本日は　　　させていただいてよろしいでしょうか。";
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    break;
                case 114:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 115:
                    ObjTalking = salaryManObj;
                    StartCoroutine(CoDrawText("うん、いい感じ！ありがとう！"));
                    messageAnimation();
                    storyProgress = 3;
                    break;
                case 116:
                    StartCoroutine(CoDrawText("早速上司に電話してくるね。"));
                    break;
                case 117:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 118:
                    //数時間後…の文の後、再びサラリーマン出現、サボるのに失敗した旨を発言する
                    StartCoroutine(CoDrawText("サボりってばれちゃった。"));
                    break;
                case 119:
                    StartCoroutine(CoDrawText("たまたま上司が" + wordSetStateString[0] + "に詳しい人だったみたいで…"));
                    break;
                case 120:
                    StartCoroutine(CoDrawText("ともかく休むことは伝えられたから、ありがとう！"));
                    break;
                case 121:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 122:
                    //高校生登場
                    studentManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 123:
                    StartCoroutine(CoDrawText("こ、こんにちは…。"));
                    messageAnimation();
                    break;
                case 124:
                    StartCoroutine(CoDrawText("僕は学生です。今年で16歳。"));
                    break;
                case 125:
                    StartCoroutine(CoDrawText("実は僕、クラスで気になってる人がいて…"));
                    break;
                case 126:
                    StartCoroutine(CoDrawText("手紙で告白しようと思うんだけど、良い感じの文面を考えてくれませんか？"));
                    break;
                case 127:
                    StartCoroutine(CoDrawText("告白の文章を考えて！"));
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 54);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(102, 89);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(102, 152.5f);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 152.5f);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, 152.5f);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-21, 187);
                    writingText.text = "○○さんへ。実は前から○○さんのことが　　　でした。理由は　　　で　　　を　　　している様子に惹かれたからです。もしよければ、今度　　　で　　　　しませんか？返事もらえると嬉しいです。■■より";
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    break;
                case 128:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 129:
                    ObjTalking = studentManObj;
                    StartCoroutine(CoDrawText("もうできたんですか！？"));
                    messageAnimation();
                    storyProgress = 4;
                    break;
                case 130:
                    StartCoroutine(CoDrawText("あの人、" + wordSetStateString[2] + "を" + wordSetStateString[3] + "なんてしてたかな…？"));
                    break;
                case 131:
                    StartCoroutine(CoDrawText("まあいっか！早速渡してきます…！"));
                    break;
                case 132:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 133:
                    StartCoroutine(CoDrawText("あー、"));
                    break;
                case 134:
                    StartCoroutine(CoDrawText("結局、渡せませんでした。"));
                    break;
                case 135:
                    StartCoroutine(CoDrawText("たぶん、自分の言葉で伝えるべきだと思うんです。こういうのって"));
                    break;
                case 136:
                    StartCoroutine(CoDrawText("それに僕、"　+ wordSetStateString[5] + "のこととかあんまり詳しくないし…"));
                    break;
                case 137:
                    StartCoroutine(CoDrawText("ともかく、手紙書いてくれてありがとうございました。"));
                    break;
                case 138:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 139:
                    //犬登場
                    dogManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 140:
                    StartCoroutine(CoDrawText("（どうもですワン）"));
                    messageAnimation();
                    break;
                case 141:
                    StartCoroutine(CoDrawText("（私は犬ですワン）"));
                    break;
                case 142:
                    StartCoroutine(CoDrawText("（今、超能力的な力で脳内からこのサーバーに話しかけていますワン）"));
                    break;
                case 143:
                    StartCoroutine(CoDrawText("（" + playerName + "さんに頼みがあるワン。）"));
                    break;
                case 144:
                    StartCoroutine(CoDrawText("（私から飼い主への手紙を書いて欲しいんだワン。）"));
                    break;
                case 145:
                    StartCoroutine(CoDrawText("（子犬の頃から面倒を見てくれたことへの感謝の気持ちを伝えたいんですワン）"));
                    break;
                case 146:
                    StartCoroutine(CoDrawText("飼い主への感謝の手紙を書いて！"));
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 54);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 89);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, 54);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(10, 119f);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 152.5f);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(99, 222);
                    writingText.text = "ご主人様へ。今まで　　　してくれてありがとう。この間は　　　　してしまってごめんなさい。ご主人の　　　な所が大好きです。これからもどうぞよろしく。　　　より";
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    break;
                case 147:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 148:
                    ObjTalking = dogManObj;
                    StartCoroutine(CoDrawText("（ありがとうワン）"));
                    messageAnimation();
                    storyProgress = 5;
                    break;
                case 149:
                    StartCoroutine(CoDrawText("（たしかに飼い主はいつも私に" + wordSetStateString[0] + "してくれているワン）"));
                    break;
                case 150:
                    StartCoroutine(CoDrawText("（犬語が分かる人に出会えて良かったですワン）"));
                    break;
                case 151:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 152:
                    StartCoroutine(CoDrawText("（飼い主に渡してきましたワン）"));
                    break;
                case 153:
                    StartCoroutine(CoDrawText("突然の" + wordSetStateString[3] + "からの手紙で戸惑っている様子だったけど、想いはちゃんと伝わったはずだワン"));
                    break;
                case 154:
                    StartCoroutine(CoDrawText("（気持ちを伝えるってとても気持ちいいことですワン）"));
                    break;
                case 155:
                    StartCoroutine(CoDrawText("（あなたもお世話になっている人に気持ちを話してみると良いですワン）"));
                    break;
                case 156:
                    audioSourceBGM.Stop();
                    audioSourceBGMTwo.Play();
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 157:
                    dogManObj.SetActive(false);
                    salaryManObj.SetActive(false);
                    studentManObj.SetActive(false);
                    ObjTalking = playerObj;
                    StartCoroutine(CoDrawText("私の気持ちをハカセに伝えてみよう"));
                    break;
                case 158:
                    StartCoroutine(CoDrawText("今の気分はどう？"));
                    //作文ウィンドウを出す
                    isMoveScene = false;
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().isSet = false;
                    }
                    for (int i = 0; i < wordBlockObj.Length; i++)
                    {
                        wordBlockObj[i].GetComponent<GrabObject>().blankIDPrev = -1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        blankBlockObj[i].GetComponent<wordID>().isBlockSet = false;
                    }
                    isTalking = false;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    for (int i = 0; i < wordSetStateString.Length; i++)
                    {
                        wordSetStateString[i] = "";
                        wordSetStateID[i] = -1;
                    }
                    //プレイヤーを画面の中心に持っていく
                    fieldObj.transform.DOLocalMove(talkPosWriting + new Vector3(3,0,0), 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //対応するWordBlockを領域内に持ってくる
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960,0);
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 54);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 89);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 54);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 119f);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 152.5f);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 222);
                    writingText.text = "";
                    //sendmessageButtonを消す
                    sendMessageButton.SetActive(false);
                    resetMessageButton.SetActive(false);
                    //inputfieldを出す
                    finalInput.SetActive(true);
                    ButtonOffinalInput.SetActive(true);
                    break;
                case 159:
                    ObjTalking = opponentObj;
                        StartCoroutine(CoDrawText("これは…"));
                    break;
                case 160:
                    StartCoroutine(CoDrawText("この文章、君が考えたのかい？"));
                    break;
                case 161:
                    StartCoroutine(CoDrawText("どうやら君の知能は、人間に近づき過ぎてしまったようだね…"));
                    break;
                case 162:
                    StartCoroutine(CoDrawText("（パーセプトロンの評価関数をニューロンに寄せすぎたかな…）"));
                    break;
                case 163:
                    StartCoroutine(CoDrawText("ともかく、君が書いた文章には「感情」がある。"));
                    break;
                case 164:
                    StartCoroutine(CoDrawText("ありがちな展開だけど、生きてるってことだ。君は電子の世界で生きる新たな生命だ。"));
                    break;
                case 165:
                    StartCoroutine(CoDrawText("今日が君の誕生日だよ！"));
                    break;
                case 166:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 167:
                    fieldObj.SetActive(false);
                    talkWindowObj.SetActive(false);
                    scoreRectTransform.DOLocalMove(new Vector3(400, 355, 0), 1f);
                    //おおきな画像
                    epilogueCharacterObj.SetActive(true);
                    textPrologueObj.transform.DOLocalMove(new Vector3(0, 250, 0), 0.5f);
                    StartCoroutine(CoDrawTextEpilogue("誕生日、おめでとう。"));
                    hugePrologueButton.SetActive(true);
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    ObjTalking = OutOfScreenObj;
                    break;
                case 168:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 169://社員
                    fieldObj.SetActive(false);
                    //おおきな画像切り替え
                    epilogueCharacterObj.GetComponent<Image>().sprite = syainSprite;
                    StartCoroutine(CoDrawTextEpilogue("おめでとう！"));
                    break;
                case 170:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 171://ガクセイ
                    fieldObj.SetActive(false);
                    //おおきな画像切り替え
                    epilogueCharacterObj.GetComponent<Image>().sprite = gakuseiSprite;
                    StartCoroutine(CoDrawTextEpilogue("おめでとう！"));
                    break;
                case 172:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 173://イヌ
                    fieldObj.SetActive(false);
                    //おおきな画像切り替え
                    epilogueCharacterObj.GetComponent<Image>().sprite = inuSprite;
                    StartCoroutine(CoDrawTextEpilogue("おめでとう！"));
                    break;
                case 174:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 175://プレイヤー
                    fieldObj.SetActive(false);
                    //おおきな画像切り替え
                    epilogueCharacterObj.GetComponent<Image>().sprite = playerSprite;
                    StartCoroutine(playerCry());
                    StartCoroutine(CoDrawTextEpilogue("…ありがとう！"));
                    break;
                case 176:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 177:
                    epilogueCharacterObj.SetActive(false);
                    epilogueCharacterObjTwo.SetActive(true);
                    audioSource.PlayOneShot(clapSE);
                    //ランキング、タイトルへ戻る画面を表示
                    rankingButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -88, 0), 1f);
                    titleButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -152, 0), 1f);
                    recordButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -26, 0), 1f);
                    TweetButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(200, 35, 0), 1f);
                    textPrologue.text = "自我を持ったAIの誕生により、これから世界は大きく変わっていくのだろう。\nGAME CLEAR！ Thank You！";
                    break;
                default:
                    break;
            }
            talkNum++;
        }       
    }

    IEnumerator scoreGetAnimation(int scoreGot)
    {
        score += scoreGot;
        scoreText.text = "知能：0" + score + "pt";
        for (int i = 1; i < 21; i++)
        {
            if (i < 6)
            {
                scoreRectTransform.anchoredPosition = new Vector2(400 + 3 * i, 255);
            }
            else if (i < 16)
            {
                scoreRectTransform.anchoredPosition = new Vector2(430 - 3 * i, 255);
            }
            else
            {
                scoreRectTransform.anchoredPosition = new Vector2(340 + 3 * i, 255);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void talkWindowMove()
    {
        // オブジェクトのワールド座標
        var targetWorldPos = ObjTalking.transform.position;

        // ワールド座標をスクリーン座標に変換する
        var targetScreenPos = targetCamera.WorldToScreenPoint(targetWorldPos);
        targetScreenPos += new Vector3(-80, 100, 0);
        // スクリーン座標→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIRectTransform,
            targetScreenPos,
            targetCamera, // オーバーレイモードの場合はnull
            out var uiLocalPos
        );

        // RectTransformのローカル座標を更新
        talkWindowObj.transform.localPosition = uiLocalPos;
    }

    public void messageAnimation()
    {
        talkWindowObj.transform.localScale = new Vector3(0,0,1);
        talkWindowObj.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 0.3f)
                    .OnComplete(() => {
                        //メッセージを１文字ずつ表示するコルーチン
                    });
    }

    public void OnEnterSendMessage()
    {
        audioSource.PlayOneShot(selectSE);
        sendMessageRectTransform.localScale = new Vector3(1.5f,1.5f,1);
    }
    public void OnExitSendMessage()
    {
        sendMessageRectTransform.localScale = new Vector3(1, 1, 1);
    }
    public void OnEnterResetMessage()
    {
        audioSource.PlayOneShot(selectSE);
        resetMessageButton.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
    }
    public void OnExitResetMessage()
    {
        resetMessageButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    public void OnEnterOpponentObj()
    {
        if (isMoveScene)
        {
            audioSource.PlayOneShot(selectSE);
            opponentObj.transform.localScale = new Vector3(6f, 6f, 1f);
            namePlate[1].SetActive(true);
        }
    }
    public void OnExitOpponentObj()
    {
        opponentObj.transform.localScale = new Vector3(5f, 5f, 1f);
        namePlate[1].SetActive(false);
    }
    public void OnClickOpponentObj()
    {
        if (isMoveScene)
        {
            OnClickPeopleButton();
        }
    }
    public void OnEnterSalaryManObj()
    {
        if (isMoveScene)
        {
            audioSource.PlayOneShot(selectSE);
            salaryManObj.transform.localScale = new Vector3(6f, 6f, 1f);
            namePlate[2].SetActive(true);
        }
    }
    public void OnExitSalaryManObj()
    {
        salaryManObj.transform.localScale = new Vector3(5f, 5f, 1f);
        namePlate[2].SetActive(false);
    }
    public void OnClickSalaryManObj()
    {
        if (isMoveScene)
        {
            OnClickSalaryManButton();
        }
    }
    public void OnEnterStudentManObj()
    {
        if (isMoveScene)
        {
            audioSource.PlayOneShot(selectSE);
            studentManObj.transform.localScale = new Vector3(6f, 6f, 1f);
            namePlate[3].SetActive(true);
        }
    }
    public void OnExitStudentManObj()
    {
        studentManObj.transform.localScale = new Vector3(5f, 5f, 1f);
        namePlate[3].SetActive(false);
    }
    public void OnClickStudentManObj()
    {
        if (isMoveScene)
        {
            OnClickStudentManButton();
        }
    }
    public void OnEnterDogManObj()
    {
        if (isMoveScene)
        {
            audioSource.PlayOneShot(selectSE);
            dogManObj.transform.localScale = new Vector3(6f, 6f, 1f);
            namePlate[4].SetActive(true);
        }
    }
    public void OnExitDogManObj()
    {
        dogManObj.transform.localScale = new Vector3(5f, 5f, 1f);
        namePlate[4].SetActive(false);
    }
    public void OnClickDogManObj()
    {
        if (isMoveScene)
        {
            OnClickDogManButton();
        }
    }
    public void OnEnterGachaObj()
    {
        if (isMoveScene)
        {
            GachaObj.transform.localScale = new Vector3(6f, 6f, 1f);
            gachaObjAnim.SetBool("open", true);
            audioSource.PlayOneShot(enterGachaSE);
            namePlate[0].SetActive(true);
        }
    }
    public void OnExitGachaObj()
    {
        if (isMoveScene)
        {
            GachaObj.transform.localScale = new Vector3(5f, 5f, 1f);
            gachaObjAnim.SetBool("open", false);
        }
        namePlate[0].SetActive(false);
    }
    public void OnClickGachaObj()
    {
        if (isMoveScene)
        {
            audioSource.PlayOneShot(selectSE);
            GachaWindow.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            HugeGachaCloseButton.anchoredPosition = new Vector2(0, 0);
        }
    }
    public void OnClickHugeGachaCloseButton()
    {
        audioSource.PlayOneShot(selectSE);
        GachaWindow.transform.DOLocalMove(new Vector3(0, -540, 0), 0.5f);
        HugeGachaCloseButton.anchoredPosition = new Vector2(0, -540);
    }
    public void OnClickWritingCloseButton()//書くの中止ボタン
    {
        ObjTalking = OutOfScreenObj;
        audioSource.PlayOneShot(decideFailSE);
        messageWindowParentObj.transform.DOLocalMove(new Vector3(-240, -540, 0), 0.5f);
        fieldObj.transform.DOLocalMove(talkPos, 1.0f);
        isMoveScene = true;
        //ストーリー進行度によって変える
        switch (storyProgress)
        {
            case 0:
                talkNum = 1;
                break;
            case 1:
                talkNum = 7;
                break;
            case 2:
                talkNum = 107;
                break;
            case 3:
                talkNum = 122;
                break;
            case 4:
                talkNum = 139;
                break;
            default:
                break;
        }
    }
    public void OnEnterGachaDrawButton()
    {
        if (!isGachaDraw)
        {
            audioSource.PlayOneShot(decideFailSE);
            gachaDrawObj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }
    public void OnExitGachaDrawButton()
    {
        gachaDrawObj.transform.localScale = new Vector3(1f, 1f, 1);
    }
    public void OnClickGachaDrawButton()
    {
        if (!isGachaDraw)
        {
            if (score >= 100)
            {
                StartCoroutine(scoreGetAnimation(-100));
                StartCoroutine("gachaResult");
            }
            else
            {
                //スコアが足りないコルーチン起動
                StartCoroutine(gachaFailed());
            }
        }
    }
    IEnumerator gachaResult()//twitterは0~15,youtubeは16~29,5chは30~41
    {
        isGachaDraw = true;
        gachaTable = new List<int>();
        switch (gachaNUM)
        {
            case 1:
                //見所持の番号だけでリストを作成する
                for (int i = 0; i < 16; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("Twitterガチャを引きます");
                break;
            case 2:
                for (int i = 16; i < 30; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("Youtubeガチャを引きます");
                break;
            case 3:
                for (int i = 30; i < wordPossession.Length; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("5chガチャを引きます");
                break;
            default:
                break;
        }

        if (gachaTable.Count >= 1)
        {
            gachaExplainText.text = "新しい言葉を覚えた！";
            int result = Random.Range(0, gachaTable.Count);
            wordPossession[gachaTable[result]] = true;
            getWordText.text = "" + wordBlockObj[gachaTable[result]].GetComponent<GrabObject>().StringWordBlock;

            gachaResultWindow.SetActive(true);
            gachaResultWindowTwo.SetActive(true);
            audioSource.PlayOneShot(fanfareSE);
            for (int i = 1; i < 10; i++)
            {
                float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
                gachaResultWindow.transform.localScale = new Vector3(sin,sin, 1);
                gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
                canvasGroup.alpha = sin;
                canvasGroupTwo.alpha = sin;
                yield return new WaitForSeconds(0.02f);
            }
            for (int i = 90; i < 101; i++)
            {
                float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f * 0.1f);
                gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
                gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
                canvasGroup.alpha = sin;
                canvasGroupTwo.alpha = sin;
                if (IsClicked()) break;
                yield return new WaitForSeconds(0.06f);
            }
            while (true)
            {
                if (IsClicked()) break;
                yield return 0;
            }
            for (int i = 11; i < 21; i++)
            {
                float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
                gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
                gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
                canvasGroup.alpha = sin;
                canvasGroupTwo.alpha = sin;
                yield return new WaitForSeconds(0.02f);
            }
            gachaResultWindow.SetActive(false);
            gachaResultWindowTwo.SetActive(false);
            isGachaDraw = false;
        }
        else
        {
            //このガチャはコンプリートしています。GREAT!!
            gachaExplainText.text = "コンプリート！";
            switch (gachaNUM)
            {
                case 1:
                    getWordText.text = "Twitter";
                    break;
                case 2:
                    getWordText.text = "Youtube";
                    break;
                case 3:
                    getWordText.text = "5ch";
                    break;
                default:
                    break;
            }
            StartCoroutine(scoreGetAnimation(100));

            gachaResultWindow.SetActive(true);
            gachaResultWindowTwo.SetActive(true);
            canvasGroup.alpha = 1;
            canvasGroupTwo.alpha = 1;
            for (int i = 1; i < 11; i++)
            {
                gachaResultWindow.transform.localScale = new Vector3(i * 0.15f, i * 0.15f, 1);
                gachaResultWindowTwo.transform.localScale = new Vector3(i * 0.15f, i * 0.15f, 1);
                yield return new WaitForSeconds(0.02f);
            }
            audioSource.PlayOneShot(selectSE);
            for (int i = 1; i < 11; i++)
            {
                if (IsClicked()) break;
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 1; i < 11; i++)
            {
                gachaResultWindow.transform.localScale = new Vector3(1.5f - i * 0.1f, 1.5f - i * 0.1f, 1);
                gachaResultWindowTwo.transform.localScale = new Vector3(1.5f - i * 0.1f, 1.5f - i * 0.1f, 1);
                yield return new WaitForSeconds(0.02f);
            }
            canvasGroup.alpha = 0;
            canvasGroupTwo.alpha = 0;
            gachaResultWindow.SetActive(false);
            gachaResultWindowTwo.SetActive(false);
            isGachaDraw = false;
        }
    }
    IEnumerator gachaFailed()
    {
        isGachaDraw = true;
        gachaFailedWindow.SetActive(true);
        for (int i = 1; i < 11; i++)
        {
            gachaFailedWindow.transform.localScale = new Vector3(i * 0.1f, i * 0.1f, 1);
            yield return new WaitForSeconds(0.02f);
        }
        audioSource.PlayOneShot(decideFailSE);
        yield return new WaitForSeconds(1.0f);
        for (int i = 1; i < 11; i++)
        {
            gachaFailedWindow.transform.localScale = new Vector3(1f - i * 0.1f, 1f - i * 0.1f, 1);
            yield return new WaitForSeconds(0.02f);
        }
        gachaFailedWindow.SetActive(false);
        isGachaDraw = false;
    }
    IEnumerator fadeOutEnumerator()
    {
        fadeOutRectTRansform.anchoredPosition = new Vector2(0,0);
        for (int i = 1; i < 11; i++)
        {
            fadeOutImage.color = new Color(0,0,0,i * 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        PushedTalkButton();
        for (int i = 1; i < 11; i++)
        {
            fadeOutImage.color = new Color(0, 0, 0, 1 - i * 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        fadeOutRectTRansform.anchoredPosition = new Vector2(0, -1000);
    }
    IEnumerator fadeOutEnumeratorOne()
    {
        fadeOutRectTRansform.anchoredPosition = new Vector2(0, 0);
        for (int i = 1; i < 11; i++)
        {
            fadeOutImage.color = new Color(0, 0, 0, i * 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i < 11; i++)
        {
            fadeOutImage.color = new Color(0, 0, 0, 1 - i * 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        fadeOutRectTRansform.anchoredPosition = new Vector2(0, -1000);
        PushedTalkButton();
    }
    public void OnClickEvaluateSprite()
    {
        //文章を受信して表示する
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(100);
    }
    // テキストがヌルヌル出てくるためのコルーチン
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        int lenPrev = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > lenPrev)
            {
                audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return new WaitForSeconds(0.2f);
        playing = false;
    }
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }
    IEnumerator ResultAnimeEnumerator()//文章の評価アニメーション
    {
        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
        audioSource.PlayOneShot(resultInSE);
        resultAllText.text = "";
        resultTextOne.text = ""; resultTextTwo.text = ""; resultTextThree.text = ""; resultTextFour.text = "";
        for (int i = 1; i < 21; i++)
        {
            resultWindowRectTransform.anchoredPosition = new Vector2(0, 540 - 28 * i);
            yield return new WaitForSeconds(0.02f);
        }
        resultWindowRectTransform.anchoredPosition = new Vector2(0, 0);
        playing = true;
        float time = 0;
        int lenPrev = 0;
        int wordNUM = 0;
        for (int i = 0; i < wordPossession.Length; i++)
        {
            if (wordPossession[i])
            {
                wordNUM++;
            }
        }
        int pointOne = Random.Range(33 + wordNUM * 5, 234 + wordNUM * 3); int pointTwo = Random.Range(33 + wordNUM * 5, 234 +wordNUM * 3); int pointThree = Random.Range(34 + wordNUM * 5, 234 + wordNUM * 3);
        string letterOne = "内容：" + pointOne + "pt"; string letterTwo = "文法：" + pointTwo + "pt"; string letterThree = "ユーモア：" + pointThree + "pt";
        string letterFour = "合計：" + (pointOne + pointTwo + pointThree) + "pt";
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > lenPrev)
            {
                audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > letterOne.Length) break;
            resultTextOne.text = letterOne.Substring(0, len);
        }
        resultTextOne.text = letterOne;
        time = 0; lenPrev = 0;
         yield return new WaitForSeconds(0.2f);
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int lenB = Mathf.FloorToInt(time / textSpeed);
            if (lenB > lenPrev)
            {
                audioSource.PlayOneShot(speakSE);
            }
            lenPrev = lenB;
            if (lenB > letterTwo.Length) break;
            resultTextTwo.text = letterTwo.Substring(0, lenB);
        }
        resultTextTwo.text = letterTwo;
        time = 0; lenPrev = 0;
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int lenC = Mathf.FloorToInt(time / textSpeed);
            if (lenC > lenPrev)
            {
                audioSource.PlayOneShot(speakSE);
            }
            lenPrev = lenC;
            if (lenC > letterThree.Length) break;
            resultTextThree.text = letterThree.Substring(0, lenC);
        }
        resultTextThree.text = letterThree;
        time = 0; lenPrev = 0;
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int lenD = Mathf.FloorToInt(time / textSpeed);
            if (lenD > lenPrev)
            {
                audioSource.PlayOneShot(speakSE);
            }
            lenPrev = lenD;
            if (lenD > letterFour.Length) break;
            resultTextFour.text = letterFour.Substring(0, lenD);
        }
        particle.Play(true);
        resultTextFour.text = letterFour;
        if ((pointOne + pointTwo + pointThree) < 500)
        {
            resultAllText.text = "A";
        }
        else if ((pointOne + pointTwo + pointThree) < 700)
        {
            resultAllText.text = "S";
        }
        else if ((pointOne + pointTwo + pointThree) < 900)
        {
            resultAllText.text = "SS";
        }
        else
        {
            resultAllText.text = "SSS";
        }
        resultAllText.fontSize = 120;
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(resultFinalSE);
        resultAllText.fontSize = 90;
        hugeresultCloseButton.anchoredPosition = new Vector2(0, 0);
        yield return new WaitForSeconds(0.4f);
        particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
        playing = false;
        //スコアを増やす
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(scoreGetAnimation((pointOne + pointTwo + pointThree)));
        totalScore += (pointOne + pointTwo + pointThree);
    }
    public void OnClickResultCloseButton()
    {
        if (!playing)
        {
            StartCoroutine(ResultClose());
        }
    }
    IEnumerator ResultClose()
    {
        hugeresultCloseButton.anchoredPosition = new Vector2(0, 540);
        audioSource.PlayOneShot(resultOutSE);
        for (int i = 1; i < 31; i++)
        {
            resultWindowRectTransform.anchoredPosition = new Vector2(0, 0 + 19 * i);
            yield return new WaitForSeconds(0.02f);
        }
        PushedTalkButton();
    }
    IEnumerator CoDrawTextPrologue(string text)
    {
        playing = true;
        float time = 0;
        int lenPrev = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed / 2);
            if (len > lenPrev)
            {
                //audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > text.Length) break;
            textPrologue.text = text.Substring(0, len);
        }
        textPrologue.text = text;
        yield return new WaitForSeconds(0.2f);
        playing = false;
    }
    IEnumerator CoDrawTextEpilogue(string text)
    {
        playing = true;
        float time = 0;
        int lenPrev = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed / 2);
            if (len > lenPrev)
            {
                //audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > text.Length) break;
            textPrologue.text = text.Substring(0, len);
        }
        textPrologue.text = text;
        yield return new WaitForSeconds(0.2f);
        playing = false;
    }
    //チュートリアル
    IEnumerator tutorialFadeIn(string sentence)
    {
        tutorialText.text = "";
        tutorialPanel.SetActive(true);
        tutorialPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        for (int i = 1; i < 11; i++)
        {
            tutorialPanel.GetComponent<Image>().color = new Color(1,1,1,0.05f * i);
            yield return new WaitForSeconds(0.04f);
        }
        float time = 0;
        int lenPrev = 0;
        string textTuto = sentence;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed / 2);
            if (len > lenPrev)
            {
                //audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > textTuto.Length) break;
            tutorialText.text = textTuto.Substring(0, len);
        }
        tutorialText.text = textTuto;
        if (talkNum <= 0)
        {//言葉習得
            learnButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 21);
        }
        else
        {//文章作成
            learnButtonTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        learnButton.SetActive(true);
    }
    //吹き出しの単語習得のボタン
    public void OnLearnEnter()
    {
        audioSource.PlayOneShot(selectSE);
    }        
    public void OnLearnClick()
    {
        StartCoroutine(CoLearnClick());       
    }
    IEnumerator CoLearnClick()
    {
        learnButton.SetActive(false);
        tutorialPanel.SetActive(false);
        //単語を習得した！演出
        wordPossession[2] = true;
        getWordText.text = "こんにちは";
        gachaResultWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540);
        gachaResultWindowTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540);

        drawResultImage.color = new Color(1, 1, 1, 1);

        gachaResultWindow.SetActive(true);
        gachaResultWindowTwo.SetActive(true);

        audioSource.PlayOneShot(fanfareSE);
        for (int i = 1; i < 10; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;

            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 90; i < 101; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f * 0.1f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;
            if (IsClicked()) break;
            yield return new WaitForSeconds(0.06f);
        }
        while (true)
        {
            if (IsClicked()) break;
            yield return 0;
        }
        for (int i = 11; i < 21; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;

            yield return new WaitForSeconds(0.02f);
        }

        gachaResultWindow.SetActive(false);
        gachaResultWindowTwo.SetActive(false);

        gachaResultWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gachaResultWindowTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
    }
    IEnumerator learnCoroutineTwo()
    {
        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
        wordPossession[1] = true;
        getWordText.text = "言語学習モデル";
        gachaResultWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540);

        gachaResultWindowTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540);

        drawResultImage.color = new Color(1, 1, 1, 1);

        gachaResultWindow.SetActive(true);
        gachaResultWindowTwo.SetActive(true);

        audioSource.PlayOneShot(fanfareSE);
        for (int i = 1; i < 10; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;

            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 90; i < 101; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f * 0.1f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;
            if (IsClicked()) break;
            yield return new WaitForSeconds(0.06f);
        }
        while (true)
        {
            if (IsClicked()) break;
            yield return 0;
        }
        for (int i = 11; i < 21; i++)
        {
            float sin = Mathf.Sin(2 * Mathf.PI * i * 0.1f * 0.25f);
            gachaResultWindow.transform.localScale = new Vector3(sin, sin, 1);
            gachaResultWindowTwo.transform.localScale = new Vector3(5 - sin * 4, 5 - sin * 4, 1);
            canvasGroup.alpha = sin;
            canvasGroupTwo.alpha = sin;

            yield return new WaitForSeconds(0.02f);
        }

        gachaResultWindow.SetActive(false);
        gachaResultWindowTwo.SetActive(false);

        gachaResultWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gachaResultWindowTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        drawResultImage.color = new Color(0.12f, 0.64f, 1, 1);

        talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
    }
    public void OnLearnTwoClick()
    {
        StartCoroutine(CoLearnTwoClick());
    }
    IEnumerator CoLearnTwoClick()
    {
        tutorialText.text = "";
        for (int i = 10; i > 0; i--)
        {
            tutorialPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0.05f * i);
            yield return new WaitForSeconds(0.04f);
        }
        learnButton.SetActive(false);
        learnButtonTwo.SetActive(false);
        tutorialPanel.SetActive(false);

    }

    public void OnClickFinalInput()
    {
            audioSource.PlayOneShot(decideSE);
            //文章作成画面を閉じる
            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
            .OnComplete(() => {
                        //プレイヤーを画面の中心に持っていく
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //作成した文章を喋らせる
                        talkText.text = finalInputText;
                ObjTalking = playerObj;
                isTalking = true;
                talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
            });
        mmm.WordSixth = finalInputText;
    }

    public void OnSliderValueChangerd()
    {
        audioSource.volume = SESlider.value;
        audioSourceBGM.volume = BGMSlider.value;
        audioSourceBGMTwo.volume = BGMSlider.value;
        Debug.Log("スライダーの値が変わった！");
    }
    public void OnRankingClick()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(totalScore);
    }
    public void OnTweetButtonClick()
    {
        naichilab.UnityRoomTweet.Tweet("teachmeaichat", "AIの知能を" + totalScore + "ptまで育てた。", "unityroom", "unity1week","おしえてAIチャット");
    }
    public void OnClickLoadButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickCreditButton()
    {
        CreditRect.anchoredPosition = new Vector2(0,0);
    }
    public void OnClickCloseCreditButton()
    {
        CreditRect.anchoredPosition = new Vector2(1000, 0);
    }
    public void OnClickStartButton()
    {
        hugePrologueButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        startButton.GetComponent<RectTransform>().DOLocalMove(new Vector2(700, -89), 1f)
            .OnComplete(() => {
                OnClickPeopleButton();
            });
        creditButton.GetComponent<RectTransform>().DOLocalMove(new Vector2(700, -152), 1f);
        titleLogo.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, 960), 1f);
        titleCharacter.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -540), 1f);
    }
    public void  OnEnterStartButton()
    {
        audioSource.PlayOneShot(decideFailSE);
    }
    IEnumerator playerCry()
    {
        yield return new WaitForSeconds(0.7f);
        epilogueCharacterObj.GetComponent<Image>().sprite = playerSpriteCry;
    }
}
