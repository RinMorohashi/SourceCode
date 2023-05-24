using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour
{
    /// <summary>
    /// マップの移動や会話イベント、アイテムの取得や敵とのバトルを制御するコード
    /// </summary>
    public RectTransform mapImageRect;
    public RectTransform[] mapButton;
    public Image fadeOutImage;
    public RectTransform fadeOutRect;
    public Image fadeOutImageUnderTextWindow;
    public RectTransform fadeOutRectUnderTextWindow;
    public GameObject[] BGImages;
    public GameObject goBackTitleButton;
    public GameObject circleImage;
    public int talkNum;
    public int talkEventNum;
    public bool playing;
    public Text talkText;
    public float textSpeed;
    public AudioSource audioSource;
    //最初のBGM
    public AudioSource audioSourceBGM1;
    //メインBGM
    public AudioSource audioSourceBGM2;
    //バトルBGM
    public AudioSource audioSourceBGM3;
    //ボスBGM
    public AudioSource audioSourceBGM4;
    //クリアBGM
    public AudioSource audioSourceBGM5;
    public AudioClip speakSE;
    public AudioClip enterSE;
    public AudioClip decideSE;
    public AudioClip attack1SE;
    public AudioClip attack2SE;
    public AudioClip attack3SE;
    public AudioClip fanfareSE;
    public AudioClip creditGetSE;
    public AudioClip encountSE;
    public AudioClip clapSE;
    public GameObject talkWindowObj;
    public GameObject talkProceedButton;
    public GameObject[] peopleObjAtField;
    public Image[] peopleImageAtField;
    public Image[] peopleImage;
    public RectTransform[] peopleRectAtField;
    public RectTransform[] peopleRect;
    public GameObject[] ObjEnterIcon;
    [SerializeField] Image talkWindowImage;
    [SerializeField] Sprite[] talkWindowSprite;
    public GameObject[] ItemObjectAtField;
    public RectTransform[] ItemRectAtField;
    public GameObject[] ItemObject;
    public GameObject battleBackground;
    public GameObject buttonAssault;
    public GameObject buttonRun;
    public RectTransform[] enemyRectAtBattle;
    public Image[] enemyImageAtBattle;
    public Sprite[] enemySprite;
    public int enemyID;//１：ハウンド、２：スピリット、３：サラマンダー、４：ダークナイト
    string enemyName;
    public int enemyHP;
    public int playerHP;
    public int actNum;//１ならたたかう、２ならにげる、戦闘開始前は０にしておく。
    public Animator slashAnimator;
    public Text playerHPText;
    public int[] playerStatus;//[0]：健康、[1]：筋肉、[2]体力、[3]戦略、[4]話術、[5]知能（どれも初期値は５）
    public int NumOfAct;
    public Text NumOfActText;
    public bool isOpen;
    public RectTransform statusWindowRect;
    public statusManager stManager;
    public GameObject[] doExamButton;
    int score;
    public RectTransform EquipRect;
    public int equipItem;//０：素手、１：ハシゴ、２：参考書、３：出席カード、４：マスターソード
    public Text yesButtonText;
    public Text noButtonText;
    public bool isMapScene;
    public bool isTextWindowActive;
    public GameObject[] enemyEnterIcon;
    public RectTransform[] enemyRect;
    public GameObject statusWindowCloser;
    public RectTransform stWindow;
    public Text statusText;
    public Image statusImage;
    [SerializeField] Sprite infoSprite;
    [SerializeField] Sprite crossSprite;
    public int enemyIDLeft;
    public int enemyIDRight;
    public Image PlayerHPImage;
    public Image EnemyHPImage;
    public int enemyMaxHP;
    public Text enemyHPBarText;
    public RectTransform[] enemyRectAtBattleLeft;
    public Image[] enemyImageAtBattleLeft;
    public int enemyHPLeft;
    public int enemyMaxHPLeft;
    public Text enemyHPBarTextLeft;
    public Image EnemyHPImageLeft;
    string enemyNameLeft;
    public RectTransform HPBarLeft;
    public RectTransform HPBarRight;
    public int credit;
    public RectTransform getCreditTextRect;
    public Text getCreditText;
    public Text CreditText;
    public Image creditBGImage;

    public GameObject[] enemyObjAtField;
    bool talked;//友人から単位を貰ったか
    bool talkedAtLibralium;//数学博士から単位を貰ったか

    public RectTransform CreditOnTreeRect;
    public GameObject TextCriteria;
    public GameObject shutter;
    public RectTransform[] departIcon;
    public RectTransform prologueBGRect;
    public RectTransform prologueBGRect2;
    public RectTransform decidePrologueButton;
    public Outline[] departButtonOutLine;
    public int departNumber;

    public GameObject[] enemyStatusObj;

    public bool isEpilogue;
    public Image whiteImage;
    public Text celebrateText;

    public GameObject[] finalObj;
    public GameObject titleImage;
    public Text nameText;
    public Slider BGMSlider;
    public Slider SESlider;
    public Slider SpeedSlider;
    public GameObject BGMSliderObj;
    public GameObject newGameButton;
    public RectTransform settingButton;
    public bool isSettingOpened;
    public RectTransform newGameButtonRect;
    public bool isTalking;
    public GameObject GameOverObj;
    public GameObject RetryButtonObj;
    public RectTransform RetryButtonRect;
    public RectTransform RankingButtonRect;
    public RectTransform TitleButtonRect;
    bool isTestPassed;
    public RectTransform kanban1Rect;
    public RectTransform kanban2Rect;
    public RectTransform kanban3Rect;
    public GameObject kanban1Obj;
    public GameObject kanban2Obj;
    public GameObject kanban3Obj;
    bool isDarkKnightDefeated;
    public RectTransform mathObj;
    public Text escapeText;
    public RectTransform tweetObj;
    //読書の選択肢が出ている間、他のボタンを押させないための壁オブジェクト
    public GameObject bookWall;
    int equipItemNum;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceBGM1.Play();
        for (int i = 0; i < 6; i++)
        {
            playerStatus[i] = 5;
        }
        NumOfAct = 20;
        NumOfActText.text = NumOfAct + "回";
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-文学部-", 1);
        departSelectUpdate(1);
        departNumber = 1;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 10; playerStatus[5] = 7;

    }

    public void OnClickStartButton()
    {
        isTalking = true;
        //最初のイベント
        talkEventNum = 0;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
        isTextWindowActive = false;

        BGMSliderObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-440,235),0.3f);
        BGMSliderObj.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f);
        isSettingOpened = false;
        newGameButton.SetActive(false);
    }

    public void OnClickBuildingFirstButton()
    {
        audioSource.PlayOneShot(decideSE);
        //フェードアウトしてボタンによって画像を変える
        StartCoroutine(MapChange(1));
    }
    public void OnClickBuildingSixButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapChange(2));
    }
    public void OnClickInfoGalleryButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapChange(3));
    }
    public void OnClickLiblaryButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapChange(4));
    }
    public void OnClickBuildingEightButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapChange(5));
    }
    public void OnClickDomitoryButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapChange(6));
    }
    public void OnClickPerson1()
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[0].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //人物アイコンを出す
        StartCoroutine(peopleImageAppear(1));
        //talkイベントを出す
        talkEventNum = 7;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson2()//追試イベント
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[1].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //人物アイコンを出す
        StartCoroutine(peopleImageAppear(2));
        //talkイベントを出す
        talkEventNum = 8;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson3()//カジノイベント
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[2].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //人物アイコンを出す
        StartCoroutine(peopleImageAppear(3));
        //talkイベントを出す
        talkEventNum = 9;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson4()//出席カードイベント
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[3].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //人物アイコンを出す
        StartCoroutine(peopleImageAppear(4));
        //talkイベントを出す
        talkEventNum = 10;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }

    public void OnClickGoBackMapButton()
    {
        audioSource.PlayOneShot(decideSE);
        StartCoroutine(MapBack());
    }

    public void OnClickTextProceedButton()
    {
        if (!playing)
        {
            talk();
        }
    }
    public void OnClickAssaultButton()
    {
        actNum = 1;
        if (!playing)
        {
            talk();
        }
    }
    public void OnClickRunButton()
    {
        actNum = 2;
        if (!playing)
        {
            talk();
        }
    }

    public void OnClickHashigo()
    {
        audioSource.PlayOneShot(decideSE);
        //ハシゴを手に入れたテキスト
        ItemObjectAtField[0].SetActive(false);
        //talkイベントを出す
        talkEventNum = 100;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        //アイテムスロットにハシゴを追加する

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickBook()
    {
        audioSource.PlayOneShot(decideSE);
        //本を手に入れたテキスト
        ItemObjectAtField[1].SetActive(false);
        //talkイベントを出す
            talkEventNum = 101;
            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
            talkProceedButton.SetActive(true);
            talk();
        //アイテムスロットに本を追加する

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickCard()
    {
        audioSource.PlayOneShot(decideSE);
        //出席カードを手に入れたテキスト
        ItemObjectAtField[2].SetActive(false);
        //talkイベントを出す
        talkEventNum = 102;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        //アイテムスロットに本を追加する

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnEquipHashigo()
    {
        if (equipItem != 1)
        {
            EquipRect.anchoredPosition = new Vector2(-15, 165);
            equipItem = 1;
        }
        else
        {
            EquipRect.anchoredPosition = new Vector2(-15, 1000);
            equipItem = 0;
        }
    }
    public void OnEquipBook()
    {
        if (equipItem != 2)
        {
            EquipRect.anchoredPosition = new Vector2(-15, 55);
            equipItem = 2;
            //勉強イベントを始めるか聞く
            if (!isTextWindowActive && !isTalking)
            {
                talkEventNum = 104;
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                talkProceedButton.SetActive(true);
                talk();
                isTextWindowActive = true;

                bookWall.SetActive(true);
            }
        }
        else
        {
            EquipRect.anchoredPosition = new Vector2(-15, 1000);
            equipItem = 0;
        }
    }
    public void OnEquipCard()
    {
        if (equipItem != 3)
        {
            EquipRect.anchoredPosition = new Vector2(-15, -55);
            equipItem = 3;
        }
        else
        {
            EquipRect.anchoredPosition = new Vector2(-15, 1000);
            equipItem = 0;
        }
    }
    public void OnEquipSword()
    {
        if (equipItem != 4)
        {
            EquipRect.anchoredPosition = new Vector2(-15, -165);
            equipItem = 4;
        }
        else
        {
            EquipRect.anchoredPosition = new Vector2(-15, 1000);
            equipItem = 0;
        }
    }

    public void OnEnterBuildingFirstButton()
    {
        //ボタンを大きくして音を鳴らす
        ButtonEnter(mapButton[0]);
    }
    public void OnEnterBuildingSixButton()
    {
        ButtonEnter(mapButton[1]);
    }
    public void OnEnterInformationButton()
    {
        ButtonEnter(mapButton[2]);
    }
    public void OnEnterLiblaryButton()
    {
        ButtonEnter(mapButton[3]);
    }
    public void OnEnterBuildingEightButton()
    {
        ButtonEnter(mapButton[4]);
    }
    public void OnEnterDomitoryButton()
    {
        ButtonEnter(mapButton[5]);
    }
    public void OnEnterperson1()
    {
        ButtonEnter(peopleRectAtField[0]);
        ObjEnterIcon[0].SetActive(true);
    }
    public void OnEnterperson2()
    {
        peopleRectAtField[1].localScale = new Vector3(-1.2f, 1.2f, 1);
        audioSource.PlayOneShot(enterSE);
        ObjEnterIcon[1].SetActive(true);
    }
    public void OnEnterperson3()
    {
        peopleRectAtField[2].localScale = new Vector3(1.2f, 1.2f, 1);
        audioSource.PlayOneShot(enterSE);
        ObjEnterIcon[2].SetActive(true);
    }
    public void OnEnterperson4()
    {
        ButtonEnter(peopleRectAtField[3]);
        ObjEnterIcon[3].SetActive(true);
    }
    public void OnEnterEnemy1()
    {
        ButtonEnter(enemyRect[0]);
        enemyEnterIcon[0].SetActive(true);
    }
    public void OnEnterEnemy2()
    {
        ButtonEnter(enemyRect[1]);
        enemyEnterIcon[1].SetActive(true);
    }
    public void OnEnterEnemy3()
    {
        ButtonEnter(enemyRect[2]);
        enemyEnterIcon[2].SetActive(true);
    }
    public void OnEnterEnemy4()
    {
        ButtonEnter(enemyRect[3]);
        enemyEnterIcon[3].SetActive(true);
    }
    public void OnEnterHashigo()
    {
        ButtonEnter(ItemRectAtField[0]);
    }
    public void OnEnterBook()
    {
        ButtonEnter(ItemRectAtField[1]);
    }
    public void OnEnterCard()
    {
        ButtonEnter(ItemRectAtField[2]);
    }
    public void OnEnterSword()
    {
        ButtonEnter(ItemRectAtField[3]);
    }
    public void OnEnterCreditOnTree()
    {
        ButtonEnter(CreditOnTreeRect);
    }
    public void OnExitCreditOnTree()
    {
        ButtonExit(CreditOnTreeRect);
    }
    public void OnClickCreditOnTree()
    {
        audioSource.PlayOneShot(decideSE);
        //talkイベントを出す
        talkEventNum = 105;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }

    public void OnExitBuildingFirstButton()
    {
        ButtonExit(mapButton[0]);
    }
    public void OnExitBuildingSixButton()
    {
        ButtonExit(mapButton[1]);
    }
    public void OnExitInformationButton()
    {
        ButtonExit(mapButton[2]);
    }
    public void OnExitLiblaryButton()
    {
        ButtonExit(mapButton[3]);
    }
    public void OnExitBuildingEightButton()
    {
        ButtonExit(mapButton[4]);
    }
    public void OnExitDomitoryButton()
    {
        ButtonExit(mapButton[5]);
    }
    public void OnExitperson1()
    {
        ButtonExit(peopleRectAtField[0]);
        ObjEnterIcon[0].SetActive(false);
    }
    public void OnExitperson2()
    {
        peopleRectAtField[1].localScale = new Vector3(-1, 1, 1);
        ObjEnterIcon[1].SetActive(false);
    }
    public void OnExitperson3()
    {
        peopleRectAtField[2].localScale = new Vector3(1, 1, 1);
        ObjEnterIcon[2].SetActive(false);
    }
    public void OnExitperson4()
    {
        ButtonExit(peopleRectAtField[3]);
        ObjEnterIcon[3].SetActive(false);
    }
    public void OnExitEnemy1()
    {
        enemyRect[0].localScale = new Vector3(1, 1, 1);
        enemyEnterIcon[0].SetActive(false);
    }
    public void OnExitEnemy2()
    {
        enemyRect[1].localScale = new Vector3(1, 1, 1);
        enemyEnterIcon[1].SetActive(false);
    }
    public void OnExitEnemy3()
    {
        enemyRect[2].localScale = new Vector3(1, 1, 1);
        enemyEnterIcon[2].SetActive(false);
    }
    public void OnExitEnemy4()
    {
        enemyRect[3].localScale = new Vector3(1, 1, 1);
        enemyEnterIcon[3].SetActive(false);
    }
    public void OnExitHashigo()
    {
        ButtonExit(ItemRectAtField[0]);
    }
    public void OnExitBook()
    {
        ButtonExit(ItemRectAtField[1]);
    }
    public void OnExitCard()
    {
        ButtonExit(ItemRectAtField[2]);
    }
    public void OnExitSword()
    {
        ButtonExit(ItemRectAtField[3]);
    }


    public void ButtonEnter(RectTransform ObjRectTransform)
    {
        ObjRectTransform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.2f);
        audioSource.PlayOneShot(enterSE);
    }
    public void ButtonEnterSilent(RectTransform ObjRectTransform)
    {
        ObjRectTransform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.2f);
    }
    public void ButtonExit(RectTransform ObjRectTransform)
    {
        ObjRectTransform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    IEnumerator MapChange(int mapNum)
    {
        bookWall.SetActive(true);

        isMapScene = false;
        NumOfAct--;
        NumOfActText.text = NumOfAct + "回";
        talkEventNum = mapNum;
        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
        isTextWindowActive = false;
        //敵の画像を出す
        for (int i = 0; i < enemyObjAtField.Length; i++)
        {
            enemyObjAtField[i].SetActive(true);
        }
        //dotweenでmap画像の位置をずらす、同時にscaleを拡大する
        mapImageRect.DOLocalMove(new Vector2(mapButton[mapNum - 1].anchoredPosition.x * -2, mapButton[mapNum - 1].anchoredPosition.y * -2), 0.5f);
        mapImageRect.DOScale(new Vector3(2,2,1), 0.5f)
            .OnComplete(() => {

                fadeOutRect.anchoredPosition = new Vector2(0, 0);
                fadeOutImage.DOColor(new Color(0,0,0,1),0.2f)
                            .OnComplete(() => {
                                BGImages[mapNum - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                                fadeOutImage.DOColor(new Color(0, 0, 0, 1), 0.2f)
                                .OnComplete(() => {
                                    //会話イベント
                                    talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                                    talkProceedButton.SetActive(true);
                                    talk();
                                    fadeOutImage.DOColor(new Color(0, 0, 0, 0), 0.2f)
                                    .OnComplete(() => {
                                        fadeOutRect.anchoredPosition = new Vector2(960, 0);

                                        bookWall.SetActive(false);
                                    });
                                });
                            });
            });
        yield return null;
    }

    public void talk()
    {
        isTalking = true;
        talkNum++;
        switch (talkEventNum)
        {
            case 0:
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("\n俺は都内の大学に通う大学４年生。"));
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n俺の所属する学部は…"));
                        break;
                    case 3:
                        //学部選択画面を出す
                        talkProceedButton.SetActive(false);
                        fadeOutRect.anchoredPosition = new Vector2(0, 0);
                        fadeOutImage.DOColor(new Color(0, 0, 0, 1), 0.2f)
                            .OnComplete(() => {
                                prologueBGRect.anchoredPosition = Vector2.zero;

                                fadeOutImage.DOColor(new Color(0, 0, 0, 0), 0.5f)
                                .SetDelay(0.5f)
                                                .OnComplete(() => {
                                                    fadeOutRect.anchoredPosition = new Vector2(0, 540);
                                                });
                            });
                        break;
                    case 4:
                        switch (departNumber)
                        {
                            case 1:
                                StartCoroutine(CoDrawText("\nそう、俺は文学部の４年生だ。"));                              
                                break;
                            case 2:
                                StartCoroutine(CoDrawText("\nそう、俺は法学部の４年生だ。"));
                                break;
                            case 3:
                                StartCoroutine(CoDrawText("\nそう、俺は経済学部の４年生だ。"));
                                break;
                            case 4:
                                StartCoroutine(CoDrawText("\nそう、俺は理学部の４年生だ。"));
                                break;
                            case 5:
                                StartCoroutine(CoDrawText("\nそう、俺は工学部の４年生だ。"));
                                break;
                            case 6:
                                StartCoroutine(CoDrawText("\nそう、俺は医学部の４年生だ。"));
                                break;
                            default:
                                break;
                        }
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        break;
                    case 5:
                        StartCoroutine(CoDrawText("\nそして、とある致命的な問題を抱えている。"));
                        break;
                    case 6:
                        StartCoroutine(CoDrawText("\n今は３月の中頃で、明日に卒業式が行われるのだが、"));
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\n卒業に必要な単位が取れていないのだ。"));
                        break;
                    case 8:
                        StartCoroutine(CoDrawText("\nよって、なんとしても今日中に１０単位を確保する必要がある。"));
                        break;
                    case 9:
                        StartCoroutine(CoDrawText("\n１０単位集められなければ留年だ…！"));
                        break;
                    case 10:
                        isTalking = false;
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Play();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        //メイン画面を出す
                        talkProceedButton.SetActive(false);
                        fadeOutRect.anchoredPosition = new Vector2(0, 0);
                        fadeOutImage.DOColor(new Color(0, 0, 0, 1), 1f)
                            .OnComplete(() => {
                                prologueBGRect2.anchoredPosition = new Vector2(960,0);
                                talkNum = 0;
                                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);

                                titleImage.SetActive(true);
                                titleImage.GetComponent<Image>().DOFade(1,1f);
                                titleImage.GetComponent<RectTransform>().DOScale(new Vector3(1,1,1), 5f)
                                .OnComplete(() => {
                                    titleImage.GetComponent<Image>().DOFade(0, 0.5f);
                                    fadeOutImage.DOColor(new Color(0, 0, 0, 0), 0.5f)
.SetDelay(0.5f)
                .OnComplete(() => {
                    titleImage.SetActive(false);
                    fadeOutRect.anchoredPosition = new Vector2(0, 540);
                });
                                });
                            });
                        break;
                    default:
                        break;
                }
                break;
            case 1://１号館に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("１号館だ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 2://６号館に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("６号館だ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 3://インフォメーションギャラリーに来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("インフォメーションギャラリーだ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 4://図書館に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("図書館だ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 5://図書館に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("８・９号館だ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 6://学生寮に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("学生寮だ。"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 7://人１に来た時の会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        talkWindowImage.sprite = talkWindowSprite[1];
                        nameText.text = "【友人】";
                        if (!talked)
                        {
                            StartCoroutine(CoDrawText("\nおっす、久しぶりだな。"));
                        }
                        else
                        {
                            StartCoroutine(CoDrawText("\n残りの単位は自分で何とかしてくれ。"));
                            talkNum = 9;
                        }
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n俺たちもいよいよ卒業だな。"));
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\nいや…俺単位足りてないんだよね…"));
                        nameText.text = "【主人公】";
                        break;
                    case 4:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("\nお前ならそう言うと思って、"));
                        nameText.text = "【友人】";
                        break;
                    case 5:
                        StartCoroutine(CoDrawText("\n俺が代わりに取っといたぜ、２単位。"));
                        break;
                    case 6:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\nえ…？"));
                        nameText.text = "【主人公】";
                        break;
                    case 7:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("\nお前、月曜１限のテストサボったろ？俺が代わりに受けといた。"));
                        nameText.text = "【友人】";
                        break;
                    case 8:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\nそれっていいのか…？まあいいか…"));
                        nameText.text = "【主人公】";
                        break;
                    case 9:
                        //単位を獲得する演出
                        StartCoroutine(CoDrawText("単位を獲得しました！"));
                        nameText.text = "";
                        StartCoroutine(getCredit());
                        talked = true;
                        break;
                    case 10:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        peopleImageAtField[0].DOColor(new Color(1, 1, 1, 1), 0.5f);
                        peopleImage[0].DOColor(new Color(1,1,1,0),0.5f)
                            .OnComplete(() => {
                                peopleRect[0].anchoredPosition = new Vector2(-1000, 18);
                            });
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 8://追試イベント
                switch (talkNum)
                {
                    case 1:
                        if (!isTestPassed)
                        {
                            messageAnimation();
                            talkWindowImage.sprite = talkWindowSprite[1];
                            StartCoroutine(CoDrawText("\nただいま、教養科目の追試を受け付けています。"));
                            nameText.text = "【教員】";
                        }
                        else
                        {
                            messageAnimation();
                            talkWindowImage.sprite = talkWindowSprite[1];
                            StartCoroutine(CoDrawText("\n君ならきっとやれるさ。"));
                            nameText.text = "【教員】";
                            talkNum = 5;
                        }
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n60点で合格です。受験しますか？"));                        
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        nameText.text = "【主人公】";
                        StartCoroutine(CoDrawText("\n（知恵が10ぐらいあれば合格できそう…）"));
                        yesButtonText.text = "受ける";
                        noButtonText.text = "受けない";
                        doExamButton[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[0].SetActive(true);
                        doExamButton[1].SetActive(true);
                        talkProceedButton.SetActive(false);
                        break;
                    case 4:
                        doExamButton[0].SetActive(false);
                        doExamButton[1].SetActive(false);
                        talkProceedButton.SetActive(true);
                        switch (actNum)
                        {
                            case 1://受験する
                                NumOfAct--;
                                NumOfActText.text = NumOfAct + "回";
                                //画面を暗くする

                                score = Random.Range(playerStatus[5] * 5,playerStatus[5] * 7 + 1);
                                if (score > 100)
                                {
                                    score = 100;
                                }
                                nameText.text = "";
                                if (score >= 60)
                                {
                                    StartCoroutine(CoDrawText("結果… " + score + " 点。\n合格です。"));
                                    isTestPassed = true;
                                }
                                else
                                {
                                    StartCoroutine(CoDrawText("結果… " + score + " 点。\n不合格です。"));
                                }
                                break;
                            case 2://受験しない
                                talkWindowImage.sprite = talkWindowSprite[1];
                                nameText.text = "【教員】";
                                StartCoroutine(CoDrawText("\nいつでもやってるから、またきてね。"));
                                talkNum = 5;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        if (score >= 60)
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "【教員】";
                            StartCoroutine(CoDrawText("\nおめでとう。"));
                            StartCoroutine(getCredit());
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "【教員】";
                            StartCoroutine(CoDrawText("\nいつでもやってるから、またきてね。"));
                        }
                        break;
                    case 6:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        peopleImageAtField[1].DOColor(new Color(1, 1, 1, 1), 0.5f);
                        peopleImage[1].DOColor(new Color(1, 1, 1, 0), 0.5f)
                            .OnComplete(() => {
                                peopleRect[1].anchoredPosition = new Vector2(-1000, 18);
                            });
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 9://カジノイベント
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("ようこそ、闇の闘技場へ。"));
                        break;
                    case 2:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("ただの学生寮でしょ？"));
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("否、ただの学生寮ではない。"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("寮の地下では、単位を賭けた闇のゲームが行われている…！"));
                        break;
                    case 5:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("単位を賭ける…？そんなことができるのか？"));
                        break;
                    case 6:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("そうだ。ここでは単位を通貨として扱っている。細かいことは聞いてくれるなよ？"));
                        break;
                    case 7:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("闘技場でどのモンスターが勝つか賭けるんだ。"));
                        break;
                    case 8:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("勝てば卒業、負ければ留年！やるかい？"));
                        yesButtonText.text = "賭ける";
                        noButtonText.text = "賭けない";
                        doExamButton[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[0].SetActive(true);
                        doExamButton[1].SetActive(true);
                        talkProceedButton.SetActive(false);
                        break;
                    case 9:
                        doExamButton[0].SetActive(false);
                        doExamButton[1].SetActive(false);
                        talkProceedButton.SetActive(true);
                        switch (actNum)
                        {
                            case 1://賭ける
                                //バトル画面を出してモンスターを２体出す
                                BattleBeginingCasino();
                                //○○が現れた！○○が現れた！
                                break;
                            case 2://受験しない
                                StartCoroutine(CoDrawText("待ってるぜ。"));
                                break;
                            default:
                                break;
                        }
                        break;
                    case 10:
                        //どちらに賭ける？「右」「左」
                        StartCoroutine(CoDrawText("どちらに賭ける？"));
                        yesButtonText.text = "左";
                        noButtonText.text = "右";
                        doExamButton[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[0].SetActive(true);
                        doExamButton[1].SetActive(true);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(false);
                        break;
                    case 11:
                        switch (actNum)
                        {
                            case 1:
                                StartCoroutine(CoDrawText("左のモンスターに何単位賭ける？"));
                                break;
                            case 2:
                                StartCoroutine(CoDrawText("右のモンスターに何単位賭ける？"));
                                break;
                            default:
                                break;
                        }
                        yesButtonText.text = "決定";
                        doExamButton[1].SetActive(false);
                        break;
                    case 12:
                        //２体のモンスターを戦わせる
                        //○○の攻撃！
                        //○○はXのダメージを受けた！
                        //△△の攻撃！
                        //△△はXのダメージを受けた！
                        talkProceedButton.SetActive(true);
                        buttonAssault.SetActive(false);
                        buttonRun.SetActive(false);
                        StartCoroutine(CoDrawText("○○の攻撃！"));
                        slashAnimator.SetBool("slash", true);
                        break;
                    case 13:
                        int damage = (int)(Random.Range(playerStatus[1] * 0.9f, playerStatus[1] * 1.1f));
                        StartCoroutine(CoDrawText(enemyName + "に " + damage + "のダメージ！"));
                        StartCoroutine("enemyVibration");
                        enemyHP -= damage;
                        EnemyHPImage.DOFillAmount(((float)enemyHP / (float)enemyMaxHP), 0.5f);
                        enemyHPBarText.text = enemyHP + "/" + enemyMaxHP;
                        if (enemyHP <= 0)
                        {
                            talkNum = 99;
                            enemyHPBarText.text = "0/" + enemyMaxHP;
                        }
                        //12に戻る
                        talkNum = 11;
                        break;
                    case 20:
                        if (score >= 60)
                        {
                            StartCoroutine(CoDrawText("おめでとう。"));
                        }
                        else
                        {
                            StartCoroutine(CoDrawText("いつでもやってるから、またきてね。"));
                        }
                        break;
                    case 21:
                        peopleImage[1].DOColor(new Color(1, 1, 1, 0), 0.5f)
                            .OnComplete(() => {
                                peopleRect[1].anchoredPosition = new Vector2(-1000, 18);
                            });
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 10://出席カードイベント
                int equip = equipItem;
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        if (!talkedAtLibralium)
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "【博士】";
                            StartCoroutine(CoDrawText("\nねえ聞いてよ。"));
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "【博士】";
                            StartCoroutine(CoDrawText("\n私は真に驚くべき証明を見つけたが、この余白はそれを書くには狭すぎる。…"));
                        }
                        break;
                    case 2:
                        if (!talkedAtLibralium)
                        {
                            StartCoroutine(CoDrawText("\nわたし理学部の博士なんだけど、マジですごい定理を発見してしまったの。"));
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[0];
                            peopleImageAtField[3].DOColor(new Color(1, 1, 1, 1), 0.5f);
                            peopleImage[3].DOColor(new Color(1, 1, 1, 0), 0.5f)
                                .OnComplete(() => {
                                    peopleRect[3].anchoredPosition = new Vector2(-1000, 18);
                                });
                            isTalking = false;
                            talkNum = 0;
                            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                            talkProceedButton.SetActive(false);
                            goBackTitleButton.SetActive(true);
                            goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        }
                        break;
                    case 3:
                        StartCoroutine(CoDrawText("\nその定理の証明方法を忘れないように記録したいんだけど、"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("\n手元に紙が無くて記録できないの！"));
                        break;
                    case 5:
                        if (equipItem == 3)
                        {
                            talkWindowImage.sprite = talkWindowSprite[0];
                            messageAnimation();
                            nameText.text = "";
                            StartCoroutine(CoDrawText("出席カードを渡した。"));
                            //アイテム欄の出席カードを消す
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[0];
                            peopleImageAtField[3].DOColor(new Color(1, 1, 1, 1), 0.5f);
                            peopleImage[3].DOColor(new Color(1, 1, 1, 0), 0.5f)
                                .OnComplete(() => {
                                    peopleRect[3].anchoredPosition = new Vector2(-1000, 18);
                                });
                            isTalking = false;
                            talkNum = 0;
                            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                            talkProceedButton.SetActive(false);
                            goBackTitleButton.SetActive(true);
                            goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        }
                        break;
                    case 6:
                        messageAnimation();
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("\nえ？この紙使っていいの？"));
                        nameText.text = "【博士】";
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\nありがとう！お礼に単位あげるね！"));
                        break;
                    case 8:
                        StartCoroutine(getCredit());
                        talkedAtLibralium = true;
                        break;
                    case 9:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        peopleImageAtField[3].DOColor(new Color(1, 1, 1, 1), 0.5f);
                        peopleImage[3].DOColor(new Color(1, 1, 1, 0), 0.5f)
                            .OnComplete(() => {
                                peopleRect[3].anchoredPosition = new Vector2(-1000, 18);
                            });
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 100://ハシゴを手に入れたときの会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        nameText.text = "";
                        StartCoroutine(CoDrawText("ハシゴを手に入れた！"));
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        //goBackTitleButton.SetActive(true);
                        ItemObject[0].SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 101://本を手に入れたときの会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("参考書を手に入れた！"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        ItemObject[1].SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 102://出席カードを手に入れたときの会話
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("出席カードを手に入れた！"));
                        nameText.text = "";
                        break;
                    case 2:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        ItemObject[2].SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 104:
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("参考書を読んでみようか？\n（行動回数が 1 減ります）"));
                        nameText.text = "";
                        //はいいいえボタンを出す
                        yesButtonText.text = "はい";
                        noButtonText.text = "いいえ";
                        doExamButton[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        doExamButton[0].SetActive(true);
                        doExamButton[1].SetActive(true);
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(false);
                        break;
                    case 2:
                        doExamButton[0].SetActive(false);
                        doExamButton[1].SetActive(false);
                        talkProceedButton.SetActive(true);
                        switch (actNum)
                        {
                            case 1://読む
                                //画面を暗くする
                                fadeOutRectUnderTextWindow.anchoredPosition = new Vector2(0, 0);
                                fadeOutImageUnderTextWindow.DOColor(new Color(0,0,0,1),0.5f);
                                StartCoroutine(CoDrawText("…………………………。"));
                                NumOfAct--;
                                NumOfActText.text = NumOfAct + "回";
                                break;
                            case 2://読まない
                                nameText.text = "【主人公】";
                                StartCoroutine(CoDrawText("\nほかにやることがあるはずだ。"));
                                talkNum = 3;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        StartCoroutine(CoDrawText("知恵が 3 上がった！"));
                        playerStatus[5] += 3;
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        break;
                    case 4:
                        //画面を明るくする
                        fadeOutImageUnderTextWindow.DOColor(new Color(0, 0, 0, 0), 0.5f)
                            .OnComplete(() => {
                                fadeOutRectUnderTextWindow.anchoredPosition = new Vector2(0, 540);
                            });
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        if (!isMapScene)
                        {
                            goBackTitleButton.SetActive(true);
                            goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        }
                        isTextWindowActive = false;

                        bookWall.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 105://樹上の単位をクリックしたときのイベント
                switch (talkNum)
                {
                    case 1:
                        equipItemNum = equipItem;
                        nameText.text = "";
                        if (equipItemNum != 1)
                        {
                            messageAnimation();
                            StartCoroutine(CoDrawText("木の上に単位が置かれている。\n登れば取れそうだ。"));
                        }
                        else
                        {
                            messageAnimation();
                            StartCoroutine(CoDrawText("木の上に単位が置かれている。\n登れば取れそうだ。"));
                        }
                        break;
                    case 2:
                        if (equipItemNum != 1)
                        {
                            StartCoroutine(CoDrawText("（木に登れば単位が取れそうだ）"));
                            //はいいいえボタンを出す
                            yesButtonText.text = "よじ登る";
                            noButtonText.text = "諦める";
                            doExamButton[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            doExamButton[1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            doExamButton[0].SetActive(true);
                            doExamButton[1].SetActive(true);
                            talkProceedButton.SetActive(false);
                            goBackTitleButton.SetActive(false);

                            TextCriteria.SetActive(true);
                            if (playerStatus[1] < 15)
                            {
                                shutter.SetActive(true);
                            }

                            bookWall.SetActive(true);
                        }
                        else
                        {
                            StartCoroutine(CoDrawText("ハシゴを使って木に登った。"));
                        }
                        break;
                    case 3:
                        if (equipItemNum != 1)
                        {
                            doExamButton[0].SetActive(false);
                            doExamButton[1].SetActive(false);
                            shutter.SetActive(false);
                            TextCriteria.SetActive(false);
                            talkProceedButton.SetActive(true);
                            bookWall.SetActive(false);
                            switch (actNum)
                            {
                                case 1://よじ登る
                                    StartCoroutine(CoDrawText("筋肉を使って木を登った。"));
                                    break;
                                case 2://諦める
                                    isTalking = false;
                                    talkNum = 0;
                                    talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                                    talkProceedButton.SetActive(false);
                                    if (!isMapScene)
                                    {
                                        goBackTitleButton.SetActive(true);
                                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                    }
                                    isTextWindowActive = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //単位獲得
                            StartCoroutine(getCredit());
                            CreditOnTreeRect.anchoredPosition = new Vector2(0, 3000);
                        }
                        break;
                    case 4:
                        if (equipItemNum != 1)
                        {
                            //単位獲得
                            StartCoroutine(getCredit());
                            CreditOnTreeRect.anchoredPosition = new Vector2(0, 3000);
                        }
                        else
                        {
                            isTalking = false;
                            talkNum = 0;
                            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                            talkProceedButton.SetActive(false);
                            if (!isMapScene)
                            {
                                goBackTitleButton.SetActive(true);
                                goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            }
                            isTextWindowActive = false;
                        }
                        break;
                    case 5:
                        isTalking = false;
                        talkNum = 0;
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                        talkProceedButton.SetActive(false);
                        if (!isMapScene)
                        {
                            goBackTitleButton.SetActive(true);
                            goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        }
                        isTextWindowActive = false;
                        break;
                    default:
                        break;
                }
                break;
            case 1000://戦闘のテキスト
                switch (talkNum)
                {
                    case 1:
                        StartCoroutine(CoDrawText("どうする？"));
                        buttonAssault.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        buttonRun.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        buttonAssault.SetActive(true);
                        buttonRun.SetActive(true);
                        talkProceedButton.SetActive(false);
                        break;
                    case 2:
                        talkProceedButton.SetActive(true);
                        buttonAssault.SetActive(false);
                        buttonRun.SetActive(false);
                        switch (actNum)
                        {
                            case 1://攻撃する
                                audioSource.PlayOneShot(attack1SE);
                                switch (equipItem)
                                {
                                    case 0:
                                        StartCoroutine(CoDrawText("素手で攻撃した！"));
                                        break;
                                    case 1:
                                        StartCoroutine(CoDrawText("ハシゴで攻撃した！"));
                                        break;
                                    case 2:
                                        StartCoroutine(CoDrawText("参考書で攻撃した！"));
                                        break;
                                    case 3:
                                        StartCoroutine(CoDrawText("出席カードで攻撃した！"));
                                        break;
                                    default:
                                        break;
                                }
                                slashAnimator.SetBool("slash", true);
                                break;
                            case 2://逃げる
                                StartCoroutine(CoDrawText("戦闘から逃げた！"));
                                talkNum = 100;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (actNum)
                        {
                            case 1://攻撃する
                                audioSource.PlayOneShot(attack2SE);
                                int damage = 0;
                                switch (equipItem)
                                {
                                    case 0:
                                        damage = (int)(Random.Range(playerStatus[1] * 0.8f, playerStatus[1] * 1.2f));
                                        break;
                                    case 1:
                                        damage = (int)(Random.Range(playerStatus[1] * 1.2f, playerStatus[1] * 1.5f));
                                        break;
                                    case 2:
                                        damage = (int)(Random.Range(playerStatus[1] * 1f, playerStatus[1] * 1.8f));
                                        break;
                                    case 3:
                                        damage = (int)(Random.Range(playerStatus[1] * 0.2f, playerStatus[1] * 0.4f));
                                        break;
                                    default:
                                        break;
                                }
                                StartCoroutine(CoDrawText(enemyName + "に " + damage + "のダメージ！"));
                                StartCoroutine("enemyVibration");
                                enemyHP -= damage;
                                EnemyHPImage.DOFillAmount(((float)enemyHP / (float)enemyMaxHP), 0.5f);
                                enemyHPBarText.text = enemyHP + "/" + enemyMaxHP;
                                if (enemyHP <= 0)
                                {
                                    talkNum = 99;
                                    enemyHPBarText.text = "0/" + enemyMaxHP;
                                }
                                break;
                            case 2://逃げる
                                StartCoroutine(CoDrawText("逃げるテキストテスト２"));
                                talkNum = 0;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        audioSource.PlayOneShot(attack3SE);
                        int damageReceive = 0;
                        switch (enemyID)
                        {
                            case 1:
                                damageReceive = Random.Range(2, 4);
                                break;
                            case 2:
                                damageReceive = Random.Range(3, 5);
                                break;
                            case 3:
                                damageReceive = Random.Range(4, 6);
                                break;
                            case 4:
                                damageReceive = Random.Range(5, 7);
                                break;
                            default:
                                break;
                        }
                        playerHP -= damageReceive;
                        playerHPText.text = "HP " + playerHP;
                        StartCoroutine(CoDrawText("あなたは" + damageReceive + "のダメージを食らった！"));
                        PlayerHPImage.DOFillAmount(((float)playerHP / (float)(5 + playerStatus[0])),0.5f);
                        //ウィンドウをぶるぶるさせる（ダメージを食らう演出）
                        StartCoroutine("playerVibration");
                        if (playerHP > 0)
                        {
                            talkNum = 0;
                        }
                        else
                        {
                            talkNum = 49;
                        }
                        break;
                    case 50://ゲームオーバー
                        messageAnimation();
                        StartCoroutine(CoDrawText("やられた…\n行動回数が 1 減った。"));
                        NumOfAct--;
                        NumOfActText.text = NumOfAct + "回";
                        break;
                    case 51:
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Play();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        isTalking = false;
                        talkNum = 0;
                        StartCoroutine(MapBack());
                        break;
                    case 100://戦闘に勝利
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Stop();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        audioSource.PlayOneShot(fanfareSE);
                        messageAnimation();
                        StartCoroutine(CoDrawText(enemyName + "を倒した！\n健康 +" + enemyID + "　筋肉 +" + enemyID + "　体力 +" + enemyID + "\n戦略 +" + enemyID + "　話術 +" + enemyID + "　知能 +" + enemyID));
                        for (int i = 0; i < 6; i++)
                        {
                            playerStatus[i] += enemyID;
                        }
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        enemyImageAtBattle[enemyID - 1].DOColor(new Color(1,0,0,1),0.3f)
                            .OnComplete(() => {
                                enemyImageAtBattle[enemyID - 1].DOColor(new Color(1, 0, 0, 0), 0.3f);
                            });
                        //マップから敵の画像を消す
                        enemyObjAtField[enemyID - 1].SetActive(false);
                        break;
                    case 101:
                        if (enemyID != 4)
                        {
                            //戦闘画面を終了する
                            audioSourceBGM1.Stop();
                            audioSourceBGM2.Play();
                            audioSourceBGM3.Stop();
                            audioSourceBGM4.Stop();
                            audioSourceBGM5.Stop();
                            talkProceedButton.SetActive(false);
                            goBackTitleButton.SetActive(true);
                            goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -720), 0.5f);
                            battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 1), 0.5f)
                                                            .OnComplete(() => {
                                                                battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
                                                            });
                            talkNum = 0;
                            isTalking = false;
                        }
                        else
                        {
                            if (actNum == 1 && !isDarkKnightDefeated)
                            {
                                isDarkKnightDefeated = true;
                                StartCoroutine(CoDrawText("討伐報酬として単位を手に入れた！"));
                                StartCoroutine(getCredit());
                            }
                            else
                            {
                                //戦闘画面を終了する
                                audioSourceBGM1.Stop();
                                audioSourceBGM2.Play();
                                audioSourceBGM3.Stop();
                                audioSourceBGM4.Stop();
                                audioSourceBGM5.Stop();
                                talkProceedButton.SetActive(false);
                                goBackTitleButton.SetActive(true);
                                goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -720), 0.5f);
                                battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 1), 0.5f)
                                                                .OnComplete(() => {
                                                                    battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
                                                                });
                                talkNum = 0;
                                isTalking = false;
                            }
                        }
                        break;
                    case 102:
                        //戦闘画面を終了する
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Play();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        talkProceedButton.SetActive(false);
                        goBackTitleButton.SetActive(true);
                        goBackTitleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -720), 0.5f);
                        battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 1), 0.5f)
                                                        .OnComplete(() => {
                                                            battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
                                                        });
                        talkNum = 0;
                        isTalking = false;
                        break;
                    default:
                        break;
                }
                break;
            case 5000:
                switch (talkNum)
                {
                    case 1:
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Stop();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        messageAnimation();
                        StartCoroutine(CoDrawText("\n疲れた…。今日はもう帰ろう。"));
                        nameText.text = "【主人公】";
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\nやっぱり１日で１０単位集めるなんて無理だったんだ…。"));
                        break;
                    case 3:
                        nameText.text = "";
                        StartCoroutine(CoDrawText("主人公は留年してしまった。"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("その後の主人公の行方は誰も知らない…。"));
                        break;
                    case 5:
                        //ウィンドウを消して、ゲームオーバー画像と、リトライボタンを出す。
                        GameOverObj.SetActive(true);
                        GameOverObj.GetComponent<Image>().DOFade(1,3f)
                            .OnComplete(() => {
                                RetryButtonObj.SetActive(true);
                            });
                        break;
                    default:
                        break;
                }
                break;
            case 10000:
                switch (talkNum)
                {
                    case 1:
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Stop();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Play();
                        messageAnimation();
                        prologueBGRect2.anchoredPosition = Vector2.zero;
                        StartCoroutine(CoDrawText("\n今ここに、全ての単位が集まった。"));
                        nameText.text = "【主人公】";
                        break;
                    case 2:
                        talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -180), 0.5f);
                        break;
                    case 3:
                        StartCoroutine(CoDrawText("\n思えば長い道のりだったが、何とか成し遂げることができた。"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("\nこれでやっと卒業できる…"));
                        break;
                    case 5:
                        talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -720), 0.5f);
                        talkProceedButton.SetActive(false);
                        //ホワイトアウトしてCongratulation!でランキング画面を出す
                        whiteImage.DOColor(new Color(1, 1, 1, 1), 0.5f)
                            .OnComplete(() =>
                            {
                                escapeText.DOFade(1, 1.5f).SetDelay(3f);
                                celebrateText.DOFade(1, 1.5f).SetDelay(3f).OnComplete(() => {
                                    talkProceedButton.SetActive(true);
                                });
                            });
                            break;
                    case 6:
                        talkProceedButton.SetActive(false);
                        escapeText.DOFade(0, 1.5f);
                        celebrateText.DOFade(0, 1.5f).OnComplete(() => {
                            talkProceedButton.SetActive(true);
                            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                            messageAnimation();
                            StartCoroutine(CoDrawText("\n…その後、期末テストでの不正が発覚し、俺は単位取り消しになった。"));
                        });
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\n単位はちゃんとした方法で取ろう。"));
                        break;
                    case 8:
                        audioSource.PlayOneShot(clapSE);
                        //FIN.（留年編に続く）、ランキングボタン、タイトルに戻るボタンを出す
                        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -720);
                        finalObj[0].SetActive(true);
                        finalObj[2].SetActive(true);
                        finalObj[0].GetComponent<RectTransform>().DOLocalMove(new Vector2(-360,-220), 1f);
                        finalObj[2].GetComponent<RectTransform>().DOLocalMove(new Vector2(330, -210), 1f);
                        tweetObj.DOLocalMove(new Vector2(240, -125), 1f);

                        talkProceedButton.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator MapBack()
    {
        isMapScene = true;
        goBackTitleButton.SetActive(false);
        fadeOutRect.anchoredPosition = new Vector2(0, 0);
        for (int i = 1; i < 11; i++)
        {
            fadeOutImage.color = new Color(0, 0, 0, 0.1f * i);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < BGImages.Length; i++)
        {
            BGImages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(960, 0);
        }
        mapImageRect.anchoredPosition = new Vector2(0,0);
        mapImageRect.localScale = new Vector3(1,1,1);

        //戦闘に負けたときの処理をここに書く
        talkProceedButton.SetActive(false);
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -720);
        battleBackground.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
        battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
        talkNum = 0;

        yield return new WaitForSeconds(0.2f);

        //行動回数が0だったらゲームオーバーイベントを呼ぶ
        if (NumOfAct > 0)
        {
            for (int i = 1; i < 11; i++)
            {
                fadeOutImage.color = new Color(0, 0, 0, 1 - 0.1f * i);
                yield return new WaitForSeconds(0.02f);
            }
            fadeOutRect.anchoredPosition = new Vector2(960, 0);
        }
        else
        {
            fadeOutImageUnderTextWindow.color = new Color(0, 0, 0, 1);
            fadeOutRectUnderTextWindow.anchoredPosition = new Vector2(0, 0);
            fadeOutImage.color = new Color(0, 0, 0, 0);
            fadeOutRect.anchoredPosition = new Vector2(960, 0);

            talkEventNum = 5000;
            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
            talkProceedButton.SetActive(true);
            talk();
            goBackTitleButton.SetActive(false);

            doExamButton[0].SetActive(false);
            doExamButton[1].SetActive(false);
        }
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

    public void messageAnimation()
    {
        talkWindowObj.transform.localScale = new Vector3(0, 0, 1);
        talkWindowObj.transform.DOScale(new Vector3(1, 1, 1), 0.3f)
                    .OnComplete(() => {
                        //メッセージを１文字ずつ表示するコルーチン
                    });
    }

    IEnumerator peopleImageAppear(int personNum)
    {
        switch (personNum)
        {
            case 1:
                peopleRect[0].anchoredPosition = new Vector2(0, 18);
                for (int i = 1; i < 11; i++)
                {
                    peopleImage[0].color = new Color(1, 1, 1, 0.1f * i);
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            case 2:
                peopleRect[1].anchoredPosition = new Vector2(0, -160);
                for (int i = 1; i < 11; i++)
                {
                    peopleImage[1].color = new Color(1, 1, 1, 0.1f * i);
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            case 3:
                peopleRect[2].anchoredPosition = new Vector2(0, -190);
                for (int i = 1; i < 11; i++)
                {
                    peopleImage[2].color = new Color(1, 1, 1, 0.1f * i);
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            case 4:
                peopleRect[3].anchoredPosition = new Vector2(0, -190);
                for (int i = 1; i < 11; i++)
                {
                    peopleImage[3].color = new Color(1, 1, 1, 0.1f * i);
                    yield return new WaitForSeconds(0.02f);
                }
                break;
            default:
                break;
        }
    }

    public void EncountDog()
    {
        enemyID = 1;
        BattleBeginingEnum();
        enemyName = "ハウンド";
    }
    public void EncountSpirit()
    {
        enemyID = 2;
        BattleBeginingEnum();
        enemyName = "スピリット";
    }
    public void EncountSaramander()
    {
        enemyID = 3;
        BattleBeginingEnum();
        enemyName = "サラマンダー";
    }
    public void EncountDarkKnight()
    {
        enemyID = 4;
        BattleBeginingEnum();
        enemyName = "ダークナイト";
    }

    public void BattleBeginingEnum()
    {
        nameText.text = "";
        audioSource.PlayOneShot(encountSE);
        if (playerStatus[3] < 10)
        {
            enemyStatusObj[0].SetActive(false);
            enemyStatusObj[1].SetActive(false);
            enemyStatusObj[2].SetActive(false);
        }
        else if(playerStatus[3] < 15)
        {
            enemyStatusObj[0].SetActive(true);
            enemyStatusObj[1].SetActive(false);
            enemyStatusObj[2].SetActive(false);
        }
        else if (playerStatus[3] < 20)
        {
            enemyStatusObj[0].SetActive(true);
            enemyStatusObj[1].SetActive(true);
            enemyStatusObj[2].SetActive(false);
        }
        else
        {
            enemyStatusObj[0].SetActive(true);
            enemyStatusObj[1].SetActive(true);
            enemyStatusObj[2].SetActive(true);
        }
        for (int i = 0; i < enemyImageAtBattle.Length; i++)
        {
            enemyRectAtBattle[i].anchoredPosition = new Vector2(-10000, 0);
            enemyImageAtBattle[i].color = new Color(1, 1, 1, 1);
        }
        switch (enemyID)
        {
            case 1:
                enemyHP = 15;
                enemyMaxHP = 15;
                enemyHPBarText.text = "15/15";
                enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 0);
                break;
            case 2:
                enemyHP = 20;
                enemyMaxHP = 20;
                enemyHPBarText.text = "20/20";
                enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 30);
                break;
            case 3:
                enemyHP = 50;
                enemyMaxHP = 50;
                enemyHPBarText.text = "50/50";
                enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 30);
                break;
            case 4:
                enemyHP = 100;
                enemyMaxHP = 100;
                enemyHPBarText.text = "100/100";
                enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 70);
                break;
            default:
                break;
        }
        PlayerHPImage.fillAmount = 1;
        EnemyHPImage.fillAmount = 1;
        playerHP = 5 + playerStatus[0];
        playerHPText.text = "HP " + playerHP;
        goBackTitleButton.SetActive(false);
        battleBackground.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(1,1,1),0.5f)
            .OnComplete(() => {
                //○○が現れた！のテキスト表示
                talkEventNum = 1000;
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                messageAnimation();
                StartCoroutine(CoDrawText(enemyName + "が現れた！"));

                doExamButton[0].SetActive(false);
                doExamButton[1].SetActive(false);
                //文字送りボタンを出す
                talkProceedButton.SetActive(true);

                audioSourceBGM1.Stop();
                audioSourceBGM2.Stop();
                audioSourceBGM3.Stop();
                audioSourceBGM4.Stop();
                audioSourceBGM5.Stop();
                if (enemyID != 4)
                {
                    audioSourceBGM3.Play();
                }
                else
                {
                    audioSourceBGM4.Play();
                }
            });
    }
    public void BattleBeginingCasino()
    {
        enemyIDLeft = Random.Range(1,5);
        enemyIDRight = Random.Range(1, 5);
        for (int i = 0; i < enemyImageAtBattle.Length; i++)
        {
            enemyRectAtBattle[i].anchoredPosition = new Vector2(-10000, 0);
            enemyImageAtBattle[i].color = new Color(1, 1, 1, 1);
        }
        for (int i = 0; i < enemyImageAtBattle.Length; i++)
        {
            enemyRectAtBattleLeft[i].anchoredPosition = new Vector2(-10000, 0);
            enemyImageAtBattleLeft[i].color = new Color(1, 1, 1, 1);
        }
        switch (enemyIDRight)
        {
            case 1:
                enemyHP = 10;
                enemyMaxHP = 10;
                enemyHPBarText.text = "10/10";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 0);
                enemyName = "ハウンド";
                break;
            case 2:
                enemyHP = 15;
                enemyMaxHP = 15;
                enemyHPBarText.text = "15/15";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 30);
                enemyName = "スピリット";
                break;
            case 3:
                enemyHP = 30;
                enemyMaxHP = 30;
                enemyHPBarText.text = "30/30";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 30);
                enemyName = "サラマンダー";
                break;
            case 4:
                enemyHP = 60;
                enemyMaxHP = 60;
                enemyHPBarText.text = "60/60";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 70);
                enemyName = "ダークナイト";
                break;
            default:
                break;
        }
        switch (enemyIDLeft)
        {
            case 1:
                enemyHPLeft = 10;
                enemyMaxHPLeft = 10;
                enemyHPBarTextLeft.text = "10/10";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 0);
                enemyNameLeft = "ハウンド";
                break;
            case 2:
                enemyHPLeft = 15;
                enemyMaxHPLeft = 15;
                enemyHPBarTextLeft.text = "15/15";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 30);
                enemyNameLeft = "スピリット";
                break;
            case 3:
                enemyHPLeft = 30;
                enemyMaxHPLeft = 30;
                enemyHPBarTextLeft.text = "30/30";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 30);
                enemyNameLeft = "サラマンダー";
                break;
            case 4:
                enemyHPLeft = 60;
                enemyMaxHPLeft = 60;
                enemyHPBarTextLeft.text = "60/60";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 70);
                enemyNameLeft = "ダークナイト";
                break;
            default:
                break;
        }
        HPBarLeft.anchoredPosition = new Vector2(-200,180);
        HPBarRight.anchoredPosition = new Vector2(200, 180);
        //プレイヤーHPウィンドウはどこかに消す
        EnemyHPImage.fillAmount = 1;
        EnemyHPImageLeft.fillAmount = 1;
        goBackTitleButton.SetActive(false);
        battleBackground.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 0.5f)
            .OnComplete(() => {
                //○○が現れた！のテキスト表示
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                messageAnimation();
                StartCoroutine(CoDrawText(enemyNameLeft + "が現れた！\n" + enemyName + "が現れた！"));
                //文字送りボタンを出す
                talkProceedButton.SetActive(true);
            });
    }
    IEnumerator enemyVibration()
    {
        enemyImageAtBattle[enemyID - 1].color = new Color(1,0,0,1);
        for (int i = 1; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                switch (enemyID)
                {
                    case 1:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(10, 0);
                        break;
                    case 2:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(10,30);
                        break;
                    case 3:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(10, 30);
                        break;
                    case 4:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(10, 70);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (enemyID)
                {
                    case 1:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(-10, 0);
                        break;
                    case 2:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(-10, 30);
                        break;
                    case 3:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(-10, 30);
                        break;
                    case 4:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(-10, 70);
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(0.04f);
            if (i == 5)
            {
                switch (enemyID)
                {
                    case 1:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 0);
                        break;
                    case 2:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 30);
                        break;
                    case 3:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 30);
                        break;
                    case 4:
                        enemyRectAtBattle[enemyID - 1].anchoredPosition = new Vector2(0, 70);
                        break;
                    default:
                        break;
                }
            }
        }
        enemyImageAtBattle[enemyID - 1].color = new Color(1, 1, 1, 1);
    }
    IEnumerator playerVibration()
    {
        for (int i = 1; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(10,0);
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(10, -180);
            }
            else
            {
                battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10, 0);
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10, -180);
            }
            yield return new WaitForSeconds(0.04f);
            if (i == 5)
            {
                battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(00, -180);
            }
        }
    }

    public void OnClickStatusIcon()
    {
        audioSource.PlayOneShot(decideSE);
        Debug.Log("ステータスアイコンをクリックしました。");
        if (!isOpen)
        {
            isOpen = true;
            //ステータスウィンドウを引き出す
            statusWindowRect.DOLocalMove(new Vector2(-145, 140), 0.3f);
            stWindow.DOLocalMove(new Vector2(-330, 25), 0.3f);
            statusWindowCloser.SetActive(true);
            statusText.text = " とじる ";
            statusImage.sprite = crossSprite;
        }
        else
        {
            isOpen = false;
            statusWindowRect.DOLocalMove(new Vector2(-445, 140), 0.3f);
            stWindow.DOLocalMove(new Vector2(-630, 25), 0.3f);
            statusWindowCloser.SetActive(false);
            statusText.text = "ステータス";
            statusImage.sprite = infoSprite;
        }
    }

    public void OnEnterStatus()
    {
        ButtonEnter(statusWindowRect);
    }
    public void OnExitStatus()
    {
        ButtonExit(statusWindowRect);
    }
    public void OnEnterGoBackButton()
    {
        ButtonEnter(goBackTitleButton.GetComponent<RectTransform>());
    }
    public void OnExitGoBackButton()
    {
        ButtonExit(goBackTitleButton.GetComponent<RectTransform>());
    }
    public void OnEnterAssaultButton()
    {
        ButtonEnterSilent(buttonAssault.GetComponent<RectTransform>());
        ButtonEnterSilent(doExamButton[0].GetComponent<RectTransform>());
    }
    public void OnExitAssaultButton()
    {
        ButtonExit(buttonAssault.GetComponent<RectTransform>());
        ButtonExit(doExamButton[0].GetComponent<RectTransform>());
    }
    public void OnEnterRunButton()
    {
        ButtonEnterSilent(buttonRun.GetComponent<RectTransform>());
        ButtonEnterSilent(doExamButton[1].GetComponent<RectTransform>());
    }
    public void OnExitRunButton()
    {
        ButtonExit(buttonRun.GetComponent<RectTransform>());
        ButtonExit(doExamButton[1].GetComponent<RectTransform>());
    }
    IEnumerator getCredit()
    {
        audioSource.PlayOneShot(creditGetSE);
        credit += 2;
        CreditText.text = "卒業まであと" + (10 - credit) + "単位";
        getCreditText.text = "2単位獲得";
        creditBGImage.fillAmount = (float)((float)credit / 10f);
        for (int i = 1; i < 10; i++)
        {
            getCreditTextRect.localScale = new Vector3(11 - i,11 - i,1);
            getCreditText.color = new Color(1, 1, 1, i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 1; i < 10; i++)
        {
            getCreditTextRect.localScale = new Vector3(2 - i * 0.1f, 2 - i * 0.1f, 1);
            getCreditText.color = new Color(1, 1, 1, 0.9f + i * 0.01f);
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 1; i < 10; i++)
        {
            getCreditTextRect.localScale = new Vector3(1 + 0.1f * i, 1 + 0.1f * i, 1);
            getCreditText.color = new Color(1, 1, 1, 1 - i * 0.01f);
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 1; i < 10; i++)
        {
            getCreditTextRect.localScale = new Vector3(1 + i,1 + i, 1);
            getCreditText.color = new Color(1, 1, 1, 1 - i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        getCreditTextRect.localScale = new Vector3(1, 1, 1);
        getCreditText.color = new Color(1, 1, 1, 0);

        //10単位集めたなら、ゲームクリアイベントを呼ぶ
        if (credit >= 10)
        {
            //全てのイベントを中断
            talkEventNum = 10000;
            talkNum = 0;
            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
            talkProceedButton.SetActive(true);
            isEpilogue = true;
            //talk();
            goBackTitleButton.SetActive(false);

            doExamButton[0].SetActive(false);
            doExamButton[1].SetActive(false);
        }
    }

    public void OnClickDepart1()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-文学部-", 1);
        departSelectUpdate(1);
        audioSource.PlayOneShot(decideSE);
        departNumber = 1;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 10; playerStatus[5] = 7;
    }
    public void OnClickDepart2()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-法学部-", 2);
        departSelectUpdate(2);
        audioSource.PlayOneShot(decideSE);
        departNumber = 2;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 7; playerStatus[4] = 5; playerStatus[5] = 10;
    }
    public void OnClickDepart3()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-経済学部-", 3);
        departSelectUpdate(3);
        audioSource.PlayOneShot(decideSE);
        departNumber = 3;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 10; playerStatus[4] = 7; playerStatus[5] = 5;
    }
    public void OnClickDepart4()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-理学部-", 4);
        departSelectUpdate(4);
        audioSource.PlayOneShot(decideSE);
        departNumber = 4;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 7; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 10;
    }
    public void OnClickDepart5()
    {
        stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-工学部-", 5);
        departSelectUpdate(5);
        audioSource.PlayOneShot(decideSE);
        departNumber = 5;
        playerStatus[0] = 7; playerStatus[1] = 10; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 5;
    }
    public void OnClickDepart6()
    {
        stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-医学部-", 6);
        departSelectUpdate(6);
        audioSource.PlayOneShot(decideSE);
        departNumber = 6;
        playerStatus[0] = 10; playerStatus[1] = 5; playerStatus[2] = 7; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 5;
    }
    public void departSelectUpdate(int departID)
    {
        //各ボタンのOutlineコンポ―年tを取得して、effectColorを変更する
        for (int i = 0; i < departButtonOutLine.Length; i++)
        {
            departButtonOutLine[i].effectColor = new Color(1,0,0,0);
        }
        departButtonOutLine[departID - 1].effectColor = new Color(1, 0, 0, 1);
    }
    public void OnClickDecidePrologueButton()
    {
        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
        audioSource.PlayOneShot(decideSE);
        //暗転させて、プロローグ画面を下げて、イベント０を起動する
        fadeOutRect.anchoredPosition = new Vector2(0, 0);
        fadeOutImage.DOColor(new Color(0, 0, 0, 1), 0.2f)
            .OnComplete(() => {
                prologueBGRect.anchoredPosition = new Vector2(-1000, 0);

                fadeOutImage.DOColor(new Color(0, 0, 0, 0), 0.5f)
                .SetDelay(0.5f)
                                .OnComplete(() => {
                                    fadeOutRect.anchoredPosition = new Vector2(0, 540);
                                    talkProceedButton.SetActive(true);
                                });
            });
    }
    public void OnEnterDepart1()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-文学部-", 1);
        ButtonEnter(departIcon[0]);
    }
    public void OnEnterDepart2()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-法学部-", 2);
        ButtonEnter(departIcon[1]);
    }
    public void OnEnterDepart3()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-経済学部-", 3);
        ButtonEnter(departIcon[2]);
    }
    public void OnEnterDepart4()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-理学部-", 4);
        ButtonEnter(departIcon[3]);
    }
    public void OnEnterDepart5()
    {
        stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-工学部-", 5);
        ButtonEnter(departIcon[4]);
    }
    public void OnEnterDepart6()
    {
        stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-医学部-", 6);
        ButtonEnter(departIcon[5]);
    }
    public void OnEnterDecidePrologueButton()
    {
        ButtonEnter(decidePrologueButton);
    }
    public void OnExitDepart1()
    {
        ButtonExit(departIcon[0]);
        ExitDepartUpdate();
    }
    public void OnExitDepart2()
    {
        ButtonExit(departIcon[1]);
        ExitDepartUpdate();
    }
    public void OnExitDepart3()
    {
        ButtonExit(departIcon[2]);
        ExitDepartUpdate();
    }
    public void OnExitDepart4()
    {
        ButtonExit(departIcon[3]);
        ExitDepartUpdate();
    }
    public void OnExitDepart5()
    {
        ButtonExit(departIcon[4]);
        ExitDepartUpdate();
    }
    public void OnExitDepart6()
    {
        ButtonExit(departIcon[5]);
        ExitDepartUpdate();
    }
    public void OnExitDecidePrologueButton()
    {
        ButtonExit(decidePrologueButton);
    }
    public void ExitDepartUpdate()
    {
        switch (departNumber)
        {
            case 1:
                stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-文学部-", 1);
                departSelectUpdate(1);
                break;
            case 2:
                stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-法学部-", 2);
                departSelectUpdate(2);
                break;
            case 3:
                stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-経済学部-", 3);
                departSelectUpdate(3);
                break;
            case 4:
                stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-理学部-", 4);
                departSelectUpdate(4);
                break;
            case 5:
                stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-工学部-", 5);
                departSelectUpdate(5);
                break;
            case 6:
                stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-医学部-", 6);
                departSelectUpdate(6);
                break;
            default:
                break;
        }
    }

    public void BGMvolumeUpdate()
    {
        audioSourceBGM1.volume = BGMSlider.value * 0.5f;
        audioSourceBGM2.volume = BGMSlider.value * 0.5f;
        audioSourceBGM3.volume = BGMSlider.value * 0.5f;
        audioSourceBGM4.volume = BGMSlider.value * 0.5f;
        audioSourceBGM5.volume = BGMSlider.value * 0.5f;
    }
    public void SEvolumeUpdate()
    {
        audioSource.volume = SESlider.value;
    }
    public void aTextSpeedUpdate()
    {
        textSpeed = 0.05f / (SpeedSlider.value + 1f);
    }
    public void aSETest()
    {
        int rnd = Random.Range(1, 7);
        switch (rnd)
        {
            case 1:
                audioSource.PlayOneShot(enterSE);
                break;
            case 2:
                audioSource.PlayOneShot(decideSE);
                break;
            case 3:
                audioSource.PlayOneShot(attack1SE);
                break;
            case 4:
                audioSource.PlayOneShot(fanfareSE);
                break;
            case 5:
                audioSource.PlayOneShot(creditGetSE);
                break;
            case 6:
                audioSource.PlayOneShot(encountSE);
                break;
            default:
                break;
        }
    }
    public void aOnEnterNewGameButton()
    {
        ButtonEnter(newGameButtonRect);
    }
    public void aOnExitNewGameButton()
    {
        ButtonExit(newGameButtonRect);
    }
    public void OnEnterSettingButton()
    {
        ButtonEnter(settingButton);
    }
    public void OnExitSettingButton()
    {
        ButtonExit(settingButton);
    }
    public void OnClickSettingButton()
    {
        audioSource.PlayOneShot(decideSE);
        if (isSettingOpened)
        {
            BGMSliderObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-440, 235), 0.3f);
            BGMSliderObj.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.3f);
            isSettingOpened = false;
        }
        else
        {
            BGMSliderObj.GetComponent<RectTransform>().DOLocalMove(Vector2.zero, 0.3f);
            BGMSliderObj.GetComponent<RectTransform>().DOScale(new Vector3(1,1,1), 0.3f);
            isSettingOpened = true;
        }
    }
    public void OnClickRetryButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnEnterRetryButton()
    {
        ButtonEnter(RetryButtonRect);
    }
    public void OnExitRetryButton()
    {
        ButtonExit(RetryButtonRect);
    }
    public void OnEnterRankingButton()
    {
        ButtonEnter(RankingButtonRect);
    }
    public void OnExitRankingButton()
    {
        ButtonExit(RankingButtonRect);
    }
    public void OnEnterTitleButton()
    {
        ButtonEnter(TitleButtonRect);
    }
    public void OnExitTitleButton()
    {
        ButtonExit(TitleButtonRect);
    }

    public void aOnClickKanban1()
    {
        kanban1Obj.SetActive(true);
        audioSource.PlayOneShot(decideSE);
    }
    public void aOnEnterKanban1()
    {
        ButtonEnter(kanban1Rect);
    }
    public void aOnExitKanban1()
    {
        ButtonExit(kanban1Rect);
    }
    public void aOnClickKanban2()
    {
        kanban2Obj.SetActive(true);
        audioSource.PlayOneShot(decideSE);
    }
    public void aOnEnterKanban2()
    {
        ButtonEnter(kanban2Rect);
    }
    public void aOnExitKanban2()
    {
        ButtonExit(kanban2Rect);
    }
    public void aOnClickKanban3()
    {
        kanban3Obj.SetActive(true);
        audioSource.PlayOneShot(decideSE);
    }
    public void aOnEnterKanban3()
    {
        ButtonEnter(kanban3Rect);
    }
    public void aOnExitKanban3()
    {
        ButtonExit(kanban3Rect);
    }
    public void OnClickkanbanCloser()
    {
        kanban1Obj.SetActive(false);
        kanban2Obj.SetActive(false);
        kanban3Obj.SetActive(false);
        audioSource.PlayOneShot(decideSE);
    }
    public void aOnEnterMath()
    {
        ButtonEnter(mathObj);
    }
    public void aOnExitMath()
    {
        ButtonExit(mathObj);
    }
    public void aOnEnterTweetButton()
    {
        ButtonEnter(tweetObj);
    }
    public void aOnExitTweetButton()
    {
        ButtonExit(tweetObj);
    }
    public void OnClickTweetButton()
    {
        //本文＋ハッシュタグ＊２ツイート（画像なし）
        switch (departNumber)
        {
            case 1:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "文学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            case 2:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "法学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            case 3:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "経済学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            case 4:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "理学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            case 5:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "工学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            case 6:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "医学部で" + (20 - NumOfAct) + "ターンで脱出しました。", "都立大学からの脱出");
                break;
            default:
                break;
        }
    }
    public void aOnClickRankingButton()
    {
        //ランキング画面を呼ぶコードをここに書く（未実装）
    }
}
