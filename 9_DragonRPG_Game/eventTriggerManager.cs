using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class eventTriggerManager : MonoBehaviour
{
    /// <summary>
    /// 会話イベントや宝箱、看板に触れたときの動作を行うスクリプト
    /// </summary>
    public GameObject cartain1;
    public GameObject cartain2;
    public int eventNum;
    public bool recast;

    public GameObject TextWindow;
    public GameObject[] faceImageTalk;
    public Text textOfTalk;
    public int talkNum;//会話イベントの進み度
    public FieldManager fieldManager;
    public StageManager stageManager;
    public bool dogResponse;
    public bool reviathanResponse;
    public bool dragonResponse;
    public bool firstResponse;
    public GameObject playerObjAtStage;
    public RectTransform reviathanRect;
    public RectTransform PandaRect;
    public bool isMoneyEnough;

    public GameObject dragonFollower;
    public GameObject dogFollower;

    public RectTransform endingBackgroundRec;
    public Image endingBackgroundImage;
    public Text ending1Text;
    public Text ending2Text;
    public Text endingText;

    public AudioSource audioSource;
    public AudioClip SE;//メッセージ送りSE
    public AudioClip SE2;//アイテム獲得SE

    public GameObject ItemGetWindow;
    public Text ItemGetText;
    public bool isItemGetScene;
    public AudioClip SE3;//仲間が増えたときのSE

    public Image endingBackgroundCharacterImage;

    public GameObject SavedWindow;
    public GameObject shadowOfDragon;

    public GameObject ItemIcon;
    public Sprite ItemImage1;
    public Sprite ItemImage2;
    public Sprite ItemImage3;

    public GameObject[] billboardInfo;

    public Text endingScoreText;
    public Text endingCharaLevelTextOne;
    public Text endingCharaLevelTextTwo;
    public Text endingCharaLevelTextThree;
    public RectTransform endingScoreRTransform;

    public GameObject goBackTytleButton;
    public GameObject tweetButton;
    public GameManager gameManager;
    public GameObject cursorFill;

    public CanvasGroup canvasGroup;
    public Text tweetButtonText;
    Image cursorFillImage;
    public BattleManager battleManager;

    void Start()
    {
        if (fieldManager.NumOfParty == 2)
        {
            dragonFollower.SetActive(true);
            dragonFollower.GetComponent<RectTransform>().anchoredPosition += new Vector2(200, 0);
            shadowOfDragon.SetActive(true);
        }
        else if (fieldManager.NumOfParty >= 3)
        {
            dragonFollower.SetActive(true);
            dragonFollower.GetComponent<RectTransform>().anchoredPosition += new Vector2(200, 0);
            shadowOfDragon.SetActive(true);
            dogFollower.SetActive(true);
            dogFollower.GetComponent<RectTransform>().anchoredPosition += new Vector2(200, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stageManager.isEventScene && !recast && !isItemGetScene)
        {
            if (talkNum == 4 && eventNum == 6)
            {
                dogResponse = true;
            }
            if (talkNum == 5 && eventNum == 8)
            {
                reviathanResponse = true;
            }
            if (talkNum == 17 && eventNum == 11)
            {
                dragonResponse = true;
            }
            //会話を進める文
            talkEvent();
        }
        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && stageManager.isEventScene && !recast && isItemGetScene)
        {
            //アイテム獲得ウィンドウを閉じる
            recast = true;
            StartCoroutine("recastNormal");
            isItemGetScene = false;
            ItemGetWindow.SetActive(false);
            SavedWindow.SetActive(false);
            //看板も下げる
            billboardInfo[0].SetActive(false); billboardInfo[1].SetActive(false); billboardInfo[2].SetActive(false); billboardInfo[3].SetActive(false); billboardInfo[4].SetActive(false);
            cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
            cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
            stageManager.isStageScene = true;
            StartCoroutine("isEventSceneFalse");
        }
        if (Input.GetKeyDown(KeyCode.X) && stageManager.isEventScene && !recast && talkNum == 3 && eventNum == 1)
        {
            firstResponse = false;
            talkEvent();
            recast = true;
            StartCoroutine("recastEvent");
        }
        if (Input.GetKeyDown(KeyCode.X) && stageManager.isEventScene && !recast && talkNum == 4 && eventNum == 6)
        {
            dogResponse = false;
            talkEvent();
            recast = true;
            StartCoroutine("recastEvent");
        }
        if (Input.GetKeyDown(KeyCode.X) && stageManager.isEventScene && !recast && talkNum == 5 && eventNum == 8)
        {
            reviathanResponse = false;
            talkEvent();
            recast = true;
            StartCoroutine("recastEvent");
        }
        if (Input.GetKeyDown(KeyCode.X) && stageManager.isEventScene && !recast && talkNum == 17 && eventNum == 11)
        {
            dragonResponse = false;
            talkEvent();
            recast = true;
            StartCoroutine("recastEvent");
        }
    }

    IEnumerator recastEvent()
    {
        yield return new WaitForSeconds(0.5f);
        recast = false;
    }
    IEnumerator recastLong()
    {
        yield return new WaitForSeconds(1.5f);
        recast = false;
    }
    IEnumerator recastNormal()
    {
        yield return new WaitForSeconds(0.5f);
        recast = false;
    }

    IEnumerator eventBegin()
    {
        recast = true;
        StartCoroutine("recastEvent");
        if (eventNum == 2)
        {
            dragonFollower.SetActive(true);
            dragonFollower.GetComponent<RectTransform>().anchoredPosition += new Vector2(200,0);
        }
        else if (eventNum == 7)
        {
            dogFollower.SetActive(true);
            dogFollower.GetComponent<RectTransform>().anchoredPosition += new Vector2(200, 0);
        }
        stageManager.isEventScene = true;
        cartain1.transform.DOLocalMove(new Vector2(0, 270), 1.0f);
        cartain2.transform.DOLocalMove(new Vector2(0, -270), 1.0f);
        yield return new WaitForSeconds(1f);
        switch (eventNum)
        {
            case 1://ドラゴンと出会うイベント
                //会話ウィンドウを出す
                TextWindow.SetActive(true);
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(true);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                //テキストを変える
                textOfTalk.text = "そこの人間！";
                break;
            case 2://ドラゴンを仲間にするイベント
                TextWindow.SetActive(true);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[8].SetActive(true);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                //テキストを変える
                textOfTalk.text = "ひいい！許してくださいい！";
                break;
            case 3://パンダと出会うイベント
                TextWindow.SetActive(true);
                faceImageTalk[0].SetActive(true);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[8].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                //テキストを変える
                textOfTalk.text = "何やらデカいのがいるな…";
                break;
            case 4://パンダを倒したイベント
                faceImageTalk[1].SetActive(true);
                faceImageTalk[0].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[8].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "やるじゃないか…俺の負けだ。";
                break;
            case 5://初めて海に来たイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(true);
                TextWindow.SetActive(true);
                textOfTalk.text = "海だ！眩しい！";
                break;
            case 6://犬と会うイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(true);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "拙者は侍…";
                break;
            case 7://犬を仲間にするイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(true);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "ぐふっ。";
                break;
            case 8://水神と会うイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(true);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "ア、ドウモ…";
                break;
            case 9://水神を倒したイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(true);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "私ノ負ケデス…";
                break;
            case 10://初めて遺跡に来たイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(true);
                TextWindow.SetActive(true);
                textOfTalk.text = "とうとう来たね…";
                break;
            case 11://遺跡のボスと会うイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(true);
                TextWindow.SetActive(true);
                textOfTalk.text = "いたぞ…。遺跡のドラゴンだ。";
                break;
            case 12://ファントム戦
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(true);
                faceImageTalk[4].SetActive(false);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "そこで止まりなさい。";
                break;
            case 13://エンディングイベント
                faceImageTalk[8].SetActive(false);
                faceImageTalk[7].SetActive(false);
                faceImageTalk[6].SetActive(false);
                faceImageTalk[5].SetActive(false);
                faceImageTalk[4].SetActive(true);
                faceImageTalk[3].SetActive(false);
                faceImageTalk[2].SetActive(false);
                faceImageTalk[1].SetActive(false);
                faceImageTalk[0].SetActive(false);
                TextWindow.SetActive(true);
                textOfTalk.text = "痛たた…";
                break;
            default:
                break;
        }
        //yield return new WaitForSeconds(0.5f);
    }

    public void talkEvent()
    {
        audioSource.PlayOneShot(SE);
        Debug.Log("会話を進めます。");
        switch (eventNum)
        {
            case 1://ドラゴンと出会うイベント
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "ここは俺の縄張りだ！立ち去れ！";
                        break;
                    case 2:
                        faceImageTalk[6].SetActive(true);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "【Z】去る　【X】去らない";
                        break;
                    case 3:
                        if (firstResponse)
                        {
                            faceImageTalk[6].SetActive(false);
                            faceImageTalk[7].SetActive(true);
                            textOfTalk.text = "ふん！今回は忠告だけで許してやる！";
                        }
                        else
                        {
                            faceImageTalk[6].SetActive(false);
                            faceImageTalk[7].SetActive(true);
                            textOfTalk.text = "どうしても通りたいなら…\n縄張りバトルだ！";
                        }
                        break;
                    case 4:
                        if (firstResponse)
                        {
                            playerObjAtStage.transform.DOMove(playerObjAtStage.transform.position - new Vector3(1f, 0, 0), 1.0f).OnComplete(() => {
                                TextWindow.SetActive(false);
                                cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                                cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                                talkNum = 0;
                                stageManager.isStageScene = true;
                                StartCoroutine("isEventSceneFalse");
                            });
                        }
                        else
                        {
                            //ウィンドウを閉じる
                            TextWindow.SetActive(false);
                            cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                            cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                            //バトルを始める
                            talkNum = 0;
                            stageManager.isStageScene = false;
                            fieldManager.OnEnterDemo0();
                            fieldManager.OnClickStageButton();
                            StartCoroutine("isEventSceneFalse");
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 2://ドラゴンを仲間にするイベント
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "縄張りはあなたのものですぅ！";
                        break;
                    case 3:
                        faceImageTalk[6].SetActive(true);
                        faceImageTalk[8].SetActive(false);
                        textOfTalk.text = "（遺跡を目指していることを伝えた。）";
                        break;
                    case 4:
                        faceImageTalk[6].SetActive(false);
                        faceImageTalk[8].SetActive(true);
                        textOfTalk.text = "え？遺跡に行くためにここを通り\nたいだけなのか？";
                        break;
                    case 5:
                        faceImageTalk[8].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "それを早く言えよな！遺跡のドラゴン共\nには俺も迷惑してたんだ！";
                        break;
                    case 6:
                        textOfTalk.text = "俺も行くよ！お前ならあいつらを\nやっつけてくれそうだしな！";
                        break;
                    case 7:
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "レッサードラゴンが仲間になった！";
                        audioSource.PlayOneShot(SE3);
                        break;
                    case 8:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 3://パンダと出会うイベント
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "おい！そこの白黒！";
                        break;
                    case 2:
                        textOfTalk.text = "俺たち遺跡に行きたいんだ！そこ通してくれ！";
                        break;
                    case 3:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "んん？ああ、すまないね。\n今道を開けるよ。";
                        break;
                    case 4:
                        textOfTalk.text = "…待てよ？その角…その羽…\nお前ドラゴンか？";
                        break;
                    case 5:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "そうだけど。";
                        break;
                    case 6:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "ドラゴンは悪だ！ここで倒す！";
                        break;
                    case 7:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "待てよ、俺は悪いドラゴン\nじゃないって！";
                        break;
                    case 8:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = false;
                        fieldManager.OnEnterDemo7();
                        fieldManager.OnClickStageButton();
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 4://パンダを倒したイベント
                switch (talkNum)
                {
                    case 2:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[8].SetActive(true);
                        textOfTalk.text = "びっくりしたよ。急に襲って\nきてさ。";
                        break;
                    case 3:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[8].SetActive(false);
                        textOfTalk.text = "どうしてこんなことしたの？";
                        break;
                    case 4:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "ドラゴンは森を燃やす悪党だって\n聞いてたから…。";
                        break;
                    case 5:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "俺はそんなことしないよ！";
                        break;
                    case 6:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "そうなのか。悪かった。";
                        break;
                    case 7:
                        textOfTalk.text = "遺跡は海を渡った先だ。";
                        break;
                    case 8:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "ふん、遺跡のドラゴンのせいで、\n俺まで嫌われ者だよ。";
                        break;
                    case 9:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 5://初めて海に来たイベント
                //ウィンドウを閉じる
                TextWindow.SetActive(false);
                cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                talkNum = 0;
                stageManager.isStageScene = true;
                StartCoroutine("isEventSceneFalse");
                break;
            case 6://犬と会うイベント
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "この橋を渡る者を襲撃して刀狩り\nをしているのだ。";
                        break;
                    case 2:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "俺たち刀持ってないぜ？\n通してくれよ。";
                        break;
                    case 3:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "否、隠し持っているかもしれない。";
                        break;
                    case 4:
                        textOfTalk.text = "襲わせてもらう！";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "話が通じねえ！";
                        break;
                    case 6:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = false;
                        fieldManager.OnEnterDemo3();
                        fieldManager.OnClickStageButton();
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 7://犬を仲間にするイベント
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "拙者の負けでござる…。";
                        break;
                    case 3:
                        textOfTalk.text = "ここはいさぎよく腹を斬る！";
                        break;
                    case 4:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        textOfTalk.text = "わ！そんなことしなくていいって！";
                        break;
                    case 5:
                        faceImageTalk[0].SetActive(false);
                        faceImageTalk[6].SetActive(true);
                        textOfTalk.text = "（遺跡のドラゴン討伐を手伝って\nくれないか提案した。）";
                        break;
                    case 6:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[6].SetActive(false);
                        textOfTalk.text = "ドラゴンを退治…？もちろんやる\nでござる！";
                        break;
                    case 7:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[6].SetActive(false);
                        textOfTalk.text = "身命を賭して主に仕えさせていただく！";
                        break;
                    case 8:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        textOfTalk.text = "なに言ってんだこいつ。";
                        break;
                    case 9:
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "サムライドッグが仲間になった！";
                        audioSource.PlayOneShot(SE3);
                        break;
                    case 10:
                        faceImageTalk[2].SetActive(false);
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 8://水神と会うイベント
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "ココヲ通ルナラ、5000＄払ッテクダサイ…";
                        break;
                    case 2:
                        textOfTalk.text = "ソウイウ決マリデスノデ…";
                        break;
                    case 3:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[3].SetActive(false);
                        textOfTalk.text = "うーん、どうするー？";
                        break;
                    case 4:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[3].SetActive(false);
                        textOfTalk.text = "うーん、どうするー？\n【Z】5000＄払う　【X】押し通る";
                        break;
                    case 5:
                        if (reviathanResponse)
                        {
                            if (fieldManager.money >= 5000)
                            {
                                fieldManager.money -= 5000;
                                isMoneyEnough = true;
                                faceImageTalk[0].SetActive(false);
                                faceImageTalk[3].SetActive(true);
                                textOfTalk.text = "スミマセンネ…丁度イタダキマス…";
                            }
                            else
                            {
                                isMoneyEnough = false;
                                faceImageTalk[0].SetActive(false);
                                faceImageTalk[3].SetActive(true);
                                textOfTalk.text = "オ客サンオ金足リナイ…";
                            }

                        }
                        else
                        {
                            faceImageTalk[0].SetActive(false);
                            faceImageTalk[3].SetActive(true);
                            textOfTalk.text = "…戦ウ感ジデスカ…？";
                        }
                        break;
                    case 6:
                        if (reviathanResponse)
                        {
                            if (isMoneyEnough)
                            {
                                textOfTalk.text = "ソレデハ、サヨウナラ、サヨウナラ…";
                            }
                            else
                            {
                                playerObjAtStage.transform.DOMove(playerObjAtStage.transform.position - new Vector3(0.5f, 0, 0), 1.0f).OnComplete(() => {
                                    TextWindow.SetActive(false);
                                    cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                                    cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                                    talkNum = 0;
                                    stageManager.isStageScene = true;
                                    StartCoroutine("isEventSceneFalse");
                                });
                            }
                        }
                        else
                        {
                            textOfTalk.text = "ジャア、ヤリマスカ…";
                        }
                        break;
                    case 7:
                        if (reviathanResponse)
                        {
                            faceImageTalk[3].SetActive(false);
                            faceImageTalk[0].SetActive(true);
                            textOfTalk.text = "行っちゃった…";
                            //リヴァイアサンを下げる
                            reviathanRect.transform.DOLocalMove(reviathanRect.anchoredPosition + new Vector2(0, 700), 3.0f);
                        }
                        else
                        {
                            //ウィンドウを閉じる
                            TextWindow.SetActive(false);
                            cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                            cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                            //バトルを始める
                            talkNum = 0;
                            stageManager.isStageScene = false;
                            fieldManager.OnEnterDemo6();
                            fieldManager.OnClickStageButton();
                            StartCoroutine("isEventSceneFalse");
                        }
                        break;
                    case 8:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 9://水神を倒したイベント
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "敗者ハ大人シク引キマショウ。\nソレデハ、サヨウナラ、サヨウナラ。";
                        break;
                    case 3:
                        faceImageTalk[3].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "行っちゃった…";
                        reviathanRect.transform.DOLocalMove(reviathanRect.anchoredPosition + new Vector2(0, 640), 1.0f);
                        break;
                    case 4:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 10://初めて遺跡に来たイベント
                switch (talkNum)
                {
                    case 1:
                        faceImageTalk[0].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "この遺跡の奥からすごい力を感じる…";
                        break;
                    case 2:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 11:
                switch (talkNum)
                {
                    case 2:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "お前たちが悪事を働いていると噂の\nドラゴンであるな…？";
                        break;
                    case 1:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "ん？お客さんだ！";
                        break;
                    case 3:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "そうだよ！";
                        break;
                    case 4:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "なんて悪い奴らだ！";
                        break;
                    case 5:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "やっちまおうぜ！";
                        break;
                    case 6:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "かかってこい！";
                        break;
                    case 7:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = false;
                        fieldManager.OnEnterDemo30();
                        fieldManager.OnClickStageButton();
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 12://エンディングイベント
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "ここは竜の魂が眠る地である。";
                        break;
                    case 2:
                        textOfTalk.text = "不用意に立ち入るべきではない。";
                        break;
                    case 3:
                        faceImageTalk[8].SetActive(false);
                        faceImageTalk[7].SetActive(false);
                        faceImageTalk[6].SetActive(false);
                        faceImageTalk[5].SetActive(false);
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[3].SetActive(false);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "竜の魂…幽霊ってこと？";
                        break;
                    case 4:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "ひいぃ！幽霊っ！";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "ビビってんじゃねえ！";
                        break;
                    case 6:
                        faceImageTalk[5].SetActive(true);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "安息を脅かす者は排除する。";
                        break;
                    case 7:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[5].SetActive(false);
                        textOfTalk.text = "お助けをー！";
                        break;
                    case 8:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        talkNum = 0;
                        stageManager.isStageScene = false;
                        fieldManager.OnEnterDemo12();
                        fieldManager.OnClickStageButton();
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 13://エンディングイベント
                switch (talkNum)
                {
                    case 1:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "観念しな！";
                        break;
                    case 2:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "うぅ～。ごめんなさい～。";
                        break;
                    case 3:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "こうして悪しき竜は討伐され、\n森に平和が訪れた。";
                        break;
                    case 4:
                        faceImageTalk[5].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "少女と仔竜と犬の冒険はこれからも続く…";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "一件落着、であるな！";
                        break;
                    case 6:
                        //ウィンドウを閉じる
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //バトルを始める
                        StartCoroutine("Ending1");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        talkNum++;
    }

    IEnumerator isEventSceneFalse()
    {
        yield return new WaitForSeconds(0.5f);
        stageManager.isEventScene = false;
        endingBackgroundRec.anchoredPosition = new Vector3(0, -540, 0);
    }

    public void ItemGet(int TreasureNum)
    {
        recast = true;
        StartCoroutine("recastNormal");
        stageManager.isEventScene = true;
        isItemGetScene = true;
        ItemGetWindow.SetActive(true);
        audioSource.PlayOneShot(SE2);
        switch (TreasureNum)
        {
            case 1:
                fieldManager.LvOfForest += 30;
                ItemGetText.text = "素材を手に入れた！\n魔獣の牙×30";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage1;
                break;
            case 2:
                fieldManager.LvOfSea += 35;
                ItemGetText.text = "素材を手に入れた！\n海獣の鱗×35";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage2;
                break;
            case 3:
                fieldManager.LvOfForest += 40;
                ItemGetText.text = "素材を手に入れた！\n魔獣の牙×40";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage1;
                break;
            case 4:
                fieldManager.LvOfCity += 45;
                ItemGetText.text = "素材を手に入れた！\n貴金属×45";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage3;
                break;
            case 5:
                fieldManager.LvOfSea += 100;
                ItemGetText.text = "素材を手に入れた！\n海獣の鱗×100";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage2;
                break;
            case 6:
                ItemGetText.text = "無限ジャンプを手に入れた！";
                ItemIcon.SetActive(false);
                stageManager.isINFJump = true;
                break;
            case 7:
                fieldManager.LvOfCity += 100;
                ItemGetText.text = "素材を手に入れた！\n貴金属×100";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage3;
                break;
            default:
                break;
        }
    }

    public void touchedSaveFlag()
    {
        recast = true;
        StartCoroutine("recastNormal");
        stageManager.isEventScene = true;
        isItemGetScene = true;
        SavedWindow.SetActive(true);
        audioSource.PlayOneShot(SE2);
    }

    public void touchedBillboard()
    {
        recast = true;
        StartCoroutine("recastNormal");
        stageManager.isEventScene = true;
        isItemGetScene = true;
        billboardInfo[fieldManager.billboardNum].SetActive(true);
        audioSource.PlayOneShot(SE);
    }

    IEnumerator Ending1()
    {
        endingScoreText.text = "【総プレイ時間】　" + (int)(fieldManager.entirePlayTime / 3600) + "時間　"+ (int)((fieldManager.entirePlayTime % 3600) / 60) + "分　" + (int)(fieldManager.entirePlayTime % 60) + "秒\n\n【倒した敵の数】　"+ fieldManager.entireDefeatNum + "体\n\n【各コマンドの使用回数】\n　たたかう　"+ fieldManager.entireCommandUseOne + "回\n　まもる　　"+ fieldManager.entireCommandUseTwo + "回\n　みねうち　"+ fieldManager.entireCommandUseThree + "回\n　交代　　　"+ fieldManager.entireCommandUseFour + "回\n";
        endingCharaLevelTextOne.text = "Lv" + (fieldManager.ATK0Lv + fieldManager.SPD0Lv + fieldManager.Ability0Lv);
        endingCharaLevelTextTwo.text = "Lv" + (fieldManager.ATKLv + fieldManager.SPDLv + fieldManager.AbilityLv);
        endingCharaLevelTextThree.text = "Lv" + (fieldManager.ATK2Lv + fieldManager.SPD2Lv + fieldManager.Ability2Lv);
        endingBackgroundRec.anchoredPosition = new Vector3(0, 0, 0);
        for (int i = 1; i < 11; i++)
        {
            endingBackgroundImage.color = new Color(0, 0, 0, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 1; i < 11; i++)
        {
            canvasGroup.alpha = i * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 11; i++)
        {
            endingText.color = new Color(1, 1, 1, 0.1f * i);
            ending1Text.color = new Color(1, 1, 1, 0.1f * i);
            endingBackgroundCharacterImage.color = new Color(1, 1, 1, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
        tweetButton.SetActive(true);
        goBackTytleButton.SetActive(true);
    }

    public void OnClickGoBackTytleButton()
    {
        cursorFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
        fieldManager.saveEnding();
        tweetButton.SetActive(false);
        goBackTytleButton.SetActive(false);
        cursorFillImage.fillAmount = 0;
        talkNum = 0;
        stageManager.isStageScene = true;
        StartCoroutine("isEventSceneFalse");
        gameManager.SceneNum = 2;
        gameManager.StartCoroutine("fadeOut");
    }

    public void OnClickTweetButton()
    {
        naichilab.UnityRoomTweet.Tweet("rpg3dragon", "森の平穏を取り戻した。\n【討伐タイム】" + (battleManager.defeatTime).ToString("n2") + "s", "unityroom", "DragonHuntRe");
    }

    public void OnEnterTytleButton()
    {
        cursorFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130,-330);
        tweetButtonText.text = "セーブしてタイトルに戻ります。";
        StartCoroutine("cursorFilling");
    }
    public void OnEnterTweetButton()
    {
        cursorFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -330);
        tweetButtonText.text = "#DragonHuntRe でラスボスの\n討伐タイムを確認できます。";
        StartCoroutine("cursorFilling");
    }
    IEnumerator cursorFilling()
    {
        cursorFillImage = cursorFill.GetComponent<Image>();
        for (int i = 1; i < 11; i++)
        {
            cursorFillImage.fillAmount = i * 0.1f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
