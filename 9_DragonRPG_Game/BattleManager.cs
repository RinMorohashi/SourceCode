using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// バトルシーン全体のキャラの動きやアニメーション、ステータスやドロップアイテムなどのパラメータを制御するスクリプト
    /// </summary>
    public bool BattleSwitch; //バトルを始めるときオンにする
    public Slider MPSlider;
    public Slider MPSlider2;
    public Slider MPSlider3;
    public GameObject[] MPImage;
    public GameObject[] MPImage2;
    public GameObject[] MPImage3;
    public bool[] PreMPImageActiveSelf;
    public bool[] CurMPImageActiveSelf;
    public bool isMPCharged;

    public float ChargeTime;
    public bool isActing;
    public bool isGuard;

    [Header("戦うか逃げるかの選択肢")] public GameObject choiceBattleObj;
    public GameObject[] choiceBattleEnemyImage;
    public GameObject enemy0;
    public GameObject enemy1;//敵のリソース
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy12;
    public GameObject enemy13;
    public GameObject enemy14;
    public GameObject enemy15;
    [Header("敵の位置データ")] public RectTransform enemyRect;
    public GameObject enemyObj;//生成した敵オブジェクト
    public GameObject enemyParent;
    [Header("味方1の位置データ")] public RectTransform monsterRect;
    [Header("味方1のImage")] public Image monsterImage;
    [Header("味方2の位置データ")] public RectTransform monster2Rect;
    [Header("味方2のImage")] public Image monster2Image;
    [Header("プレイヤーの位置データ")] public RectTransform playerRect;
    [Header("プレイヤーのオブジェクト")] public GameObject playerObj;
    public float MaxHP;//プレイヤーのHP関連
    public float HP;
    public Slider HPSlider;
    public float MaxHP2;//ドラゴンのHP関連
    public float HP2;
    public Slider HP2Slider;
    public float MaxHP3;//犬のHP関連
    public float HP3;
    public Slider HP3Slider;
    public int DEF;
    public int monsterATK; //味方の攻撃力
    public EnemyScript enemyScript;
    public RectTransform resultBoardRec;

    public Animator animMonster;
    public Animator animPlayer;

    public GameObject faceImage1;
    public GameObject faceImage2;
    public GameObject faceImage3;
    public Text MonsterName;
    public Text MonsterName2;
    public Text MonsterName3;


    public int MonsterID1;
    public int ATK0;
    public int SPD0;
    public int ATK1;
    public int SPD1;
    public int AbilityLv;
    public int AbilityID;
    public int ElementID1;

    public int ATK2;
    public int SPD2;

    public int ATK3;
    public int SPD3;

    public GameManager gm;
    public FieldManager fm;
    public Text HPTextField;
    public Slider HPSliderField;
    public Text HP2TextField;
    public Slider HP2SliderField;
    public Text HP3TextField;
    public Slider HP3SliderField;

    public AudioSource audioSource;
    public AudioSource audioSourceBGM;
    public AudioClip SEpunch;//パンチしたときの音
    public AudioClip SEpunch2;//剣で斬ったときの音
    public AudioClip SEFatalAttack;//KOしてる敵を殴ったときの音
    public AudioClip SEresult;//リザルト画面が降りてくるときの音
    public AudioClip SEGetEXP;//経験値を得たときの音
    public AudioClip SEMPCharged;//MPがたまったときの音
    public AudioClip SEChange;//味方を交代させたときの音
    public AudioClip SEShield;//シールド張ったときの音
    public AudioClip SEMiss;//外したときの音
    public AudioClip SEDrawSword;//ナイフを取り出したときの音
    public AudioClip SEAttackHard;//強めのナイフ攻撃の音
    public GameObject damageText;
    public GameObject damageTextObj;
    public GameObject battleBackground;
    public RectTransform resultEXPImageRT;
    public CanvasGroup resultEXPImageCanvasGroup;
    public RectTransform resultEXPImageRT2;
    public CanvasGroup resultEXPImageCanvasGroup2;
    public RectTransform resultEXPImageRT3;
    public CanvasGroup resultEXPImageCanvasGroup3;
    public RectTransform resultEXPImageRT4;
    public CanvasGroup resultEXPImageCanvasGroup4;
    public RectTransform resultTextRec;

    public GameObject Deck1Obj;
    public GameObject Deck2Obj;
    public GameObject Deck3Obj;
    public GameObject commandBackgroundObj1;
    public GameObject commandBackgroundObj2;
    public GameObject commandBackgroundObj3;
    public RectTransform Deck1Rect;
    public RectTransform Deck2Rect;
    public RectTransform Deck3Rect;
    public RectTransform commandBackgroundRect1;
    public RectTransform commandBackgroundRect2;
    public RectTransform commandBackgroundRect3;
    public Text MPText;
    public Text MPText2;
    public Text MPText3;
    public RectTransform commandRect;
    public int fightID;//どのモンスターが場に出ているか
    public bool recast;

    public Text[] resultRewardText;
    public RectTransform textTraceGainRect;
    public CanvasGroup textTraceGainGroup;
    public RectTransform textMoneyGainRect;
    public CanvasGroup textMoneyGainGroup;
    public Text textTraceGainImage;
    public Text textMoneyGain;
    public bool isSurpriseAttack;

    [Header("森画像")] public GameObject ImageForest;
    [Header("海画像")] public GameObject ImageSea;
    [Header("遺跡画像")] public GameObject ImageRuin;
    public int stageNum;//フィールド画面で選択されたステージの番号
    [Header("ドラゴンの定位置")]public Vector2 positionDragon;
    public Vector3 positionDragonDO;
    [Header("サムライドッグの定位置")] public Vector2 positionShisa;
    public Vector3 positionShisaDO;
    [Header("控えの定位置")] public Vector2 positionBack;
    public Vector3 positionBackDO;
    public GameObject[] guardImage;
    public Image[] GuardActiveImage;
    public bool isTusk;
    public bool isScale;
    public Text textPlayerHP;
    public Text textPlayerHP2;
    public Text textPlayerHP3;

    public RectTransform CommandBGDeck1;
    public RectTransform CommandBGDeck2;
    public RectTransform CommandBGDeck3;
    public int CommandIDDeck1;
    public int CommandIDDeck2;
    public int CommandIDDeck3;
    public bool isResultComplete;

    public GameObject ActionTextObj;
    public Text ActionText;
    RectTransform ActionTextRTransform;

    public bool isGameOver;//プレイヤーのHPが0になったときにtrueにする
    public GameObject gameOverText;
    public RectTransform gameOverTextRect;
    public Text gameOverTextText;
    public Image gameOverBackGround;

    public Image[] deck1CommandColor;//ドラゴンのコマンドの色
    public Image[] deck2CommandColor;
    public Image[] deck3CommandColor;
    public Color[] CommandColor;//コマンドの色　１にたたかう、２にふいうち、３にまもる、４に回復（予定）

    public Text explainTextBattle;//バトルシーンの下にある説明テキスト
    public GameObject[] darkImage;//UIを暗くする

    public GameObject[] enemyAtStageObj;
    public StageManager sm;

    public eventTriggerManager eTM;
    public int eventNumBattle;

    public GameObject eventTriggerPanda;
    public GameObject eventTriggerReviathan;

    public GameObject APShortageObj;
    public AudioClip SEAPShortage;

    public GameObject[] commandExplanationObj;
    public GameObject[] commandExplanationText;

    public bool isFinalBattle;
    public bool isFinalBattle2;
    public bool isFinalBattle3;

    public int playerSkillRemain;//特性使用可能回数
    public int DragonSkillLevel;
    public int DogSkillLevel;

    public int attackLevel;
    public GameObject swordEffect;
    public GameObject swordEffectObj;
    public GameObject fistEffect;
    public GameObject fistEffectObj;
    public GameObject katanaEffect;
    public GameObject katanaEffectObj;
    public AudioClip SEPlayerAttack;//主人公が攻撃したときの音

    public GameObject ConvertFailedText;

    [SerializeField] Image ItemImage;
    [SerializeField] Sprite tuskImage;
    [SerializeField] Sprite seaImage;
    [SerializeField] Sprite AbilityImage;

    public float defeatTime;

    public Sprite Z1; public Sprite Z2; public Sprite X1; public Sprite X2;
    public Sprite Up1; public Sprite Up2; public Sprite Down1; public Sprite Down2;
    public Image zButtonImage1; public Image zButtonImage2; public Image zButtonImage3;
    public Image xButtonImage1; public Image xButtonImage2; public Image xButtonImage3;
    public Image UpButtonImage1; public Image UpButtonImage2; public Image UpButtonImage3;
    public Image DownButtonImage1; public Image DownButtonImage2; public Image DownButtonImage3;

    void Start()
    {
        enemy0 = (GameObject)Resources.Load("enemy0");
        enemy1 = (GameObject)Resources.Load("enemy1");
        enemy2 = (GameObject)Resources.Load("enemy2");
        enemy3 = (GameObject)Resources.Load("enemy3");
        enemy4 = (GameObject)Resources.Load("enemy4");
        enemy5 = (GameObject)Resources.Load("enemy5");
        enemy6 = (GameObject)Resources.Load("enemy6");
        enemy7 = (GameObject)Resources.Load("enemy7");
        enemy8 = (GameObject)Resources.Load("enemy8");
        enemy9 = (GameObject)Resources.Load("enemy9");
        enemy12 = (GameObject)Resources.Load("enemy10");
        enemy13 = (GameObject)Resources.Load("enemy11");
        enemy14 = (GameObject)Resources.Load("enemy12");
        enemy15 = (GameObject)Resources.Load("enemy13");
        damageText = (GameObject)Resources.Load("DamageText");
        swordEffect = (GameObject)Resources.Load("swordEffect");
        fistEffect = (GameObject)Resources.Load("fistEffect");
        katanaEffect = (GameObject)Resources.Load("katanaEffect");

        ActionTextRTransform = ActionTextObj.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChargeTime += Time.deltaTime;
        if (BattleSwitch /*&& !isGuard*/ && ChargeTime >= 0.1f && !isGameOver)
        {
            ChargeTime = 0f;
            MPSlider.value += 0.003f * (SPD0 / 100f);
            if (MPSlider.value == 1f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(true); MPImage[7].SetActive(true);
                MPText.text = "AP 8/8";
            }
            else if (MPSlider.value >= 0.875f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(true); MPImage[7].SetActive(false);
                MPText.text = "AP 7/8";
            }
            else if (MPSlider.value >= 0.75f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 6/8";
            }
            else if (MPSlider.value >= 0.625f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 5/8";
            }
            else if (MPSlider.value >= 0.5f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 4/8";
            }
            else if (MPSlider.value >= 0.375f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 3/8";
            }
            else if (MPSlider.value >= 0.25f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 2/8";
            }
            else if (MPSlider.value >= 0.125f)
            {
                MPImage[0].SetActive(true); MPImage[1].SetActive(false); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 1/8";
            }
            else
            {
                MPImage[0].SetActive(false); MPImage[1].SetActive(false); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
                MPText.text = "AP 0/8";
            }

            for (int i = 0; i < 8; i++)
            {
                CurMPImageActiveSelf[i] = MPImage[i].activeSelf;
                if (CurMPImageActiveSelf[i] == true && PreMPImageActiveSelf[i] == false)
                {
                    isMPCharged = true;
                }
                PreMPImageActiveSelf[i] = CurMPImageActiveSelf[i];
            }

            MPSlider2.value += 0.003f * (SPD1 / 100f);
            if (MPSlider2.value == 1f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(true); MPImage2[7].SetActive(true);
                MPText2.text = "AP 8/8";
            }
            else if (MPSlider2.value >= 0.875f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(true); MPImage2[7].SetActive(false);
                MPText2.text = "AP 7/8";
            }
            else if (MPSlider2.value >= 0.75f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 6/8";
            }
            else if (MPSlider2.value >= 0.625f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 5/8";
            }
            else if (MPSlider2.value >= 0.5f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 4/8";
            }
            else if (MPSlider2.value >= 0.375f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 3/8";
            }
            else if (MPSlider2.value >= 0.25f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 2/8";
            }
            else if (MPSlider2.value >= 0.125f)
            {
                MPImage2[0].SetActive(true); MPImage2[1].SetActive(false); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 1/8";
            }
            else
            {
                MPImage2[0].SetActive(false); MPImage2[1].SetActive(false); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
                MPText2.text = "AP 0/8";
            }

            MPSlider3.value += 0.003f * (SPD2 / 100f);
            if (MPSlider3.value == 1f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(true); MPImage3[7].SetActive(true);
                MPText3.text = "AP 8/8";
            }
            else if (MPSlider3.value >= 0.875f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(true); MPImage3[7].SetActive(false);
                MPText3.text = "AP 7/8";
            }
            else if (MPSlider3.value >= 0.75f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 6/8";
            }
            else if (MPSlider3.value >= 0.625f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 5/8";
            }
            else if (MPSlider3.value >= 0.5f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 4/8";
            }
            else if (MPSlider3.value >= 0.375f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 3/8";
            }
            else if (MPSlider3.value >= 0.25f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 2/8";
            }
            else if (MPSlider3.value >= 0.125f)
            {
                MPImage3[0].SetActive(true); MPImage3[1].SetActive(false); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 1/8";
            }
            else
            {
                MPImage3[0].SetActive(false); MPImage3[1].SetActive(false); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
                MPText3.text = "AP 0/8";
            }

            if (isMPCharged)
            {
                isMPCharged = false;
                audioSource.PlayOneShot(SEMPCharged);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !recast && BattleSwitch && fm.isBattle && fm.NumOfParty >= 2 && enemyScript.HP > 0 && !isGameOver)
        {
            //モンスターを切り替える
            if (enemyScript.isConvertable)
            {
                StartCoroutine("MonsterChangeUp");
            }
            else
            {
                //交代できない！
                StopCoroutine("ConvertFailed");
                StartCoroutine("ConvertFailed");
            }

            recast = true;
            StartCoroutine("Recast");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !recast && fm.isBattle && fm.NumOfParty >= 2 && enemyScript.HP > 0 && !isGameOver && !isActing)
        {
            //モンスターを切り替える
            if (enemyScript.isConvertable)
            {
                StartCoroutine("MonsterChangeDown");
            }
            else
            {
                //交代できない！
                StopCoroutine("ConvertFailed");
                StartCoroutine("ConvertFailed");
            }

            recast = true;
            StartCoroutine("Recast");
        }
        if (Input.GetKeyDown(KeyCode.Z) && !recast && fm.isBattle && !isGameOver)
        {
            Debug.Log("戦闘中にZキーが押されたよ！");
            if (enemyScript.HP > 0)
            {
                switch (fightID)
                {
                    case 1:
                        OnFightButtonClick();
                        break;
                    case 2:
                        OnFight2ButtonClick();
                        break;
                    case 3:
                        OnFight3ButtonClick();
                        break;
                    default:
                        break;
                }
            }
            else if (isResultComplete)
            {
                //リザルト画面の次へボタン
                isResultComplete = false;
                OnResultNextButtonClick();
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && !recast && fm.isBattle && !isGameOver)
        {
            if (enemyScript.HP > 0)
            {
                switch (fightID)
                {
                    case 1:
                        OnGuardButtonClick();
                        break;
                    case 2:
                        OnGuard2ButtonClick();
                        break;
                    case 3:
                        OnGuard3ButtonClick();
                        break;
                    default:
                        break;
                }
            }
            else if (isResultComplete)
            {
                //リザルト画面の次へボタン
                isResultComplete = false;
                OnResultNextButtonClick();
            }

            recast = true;
            StartCoroutine("RecastShort");
        }

        if (isFinalBattle || isFinalBattle2 || isFinalBattle3)
        {
            defeatTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine("buttonPushZ");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine("buttonPushX");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine("buttonPushUp");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine("buttonPushDown");
        }
    }

    public void enemyEncount()
    {
        ActionTextRTransform.anchoredPosition = new Vector2(-50, 300);
        recast = true;
        recastBattleBegin();
        //ここで敵をinstantiateしてenemyScriptに代入する
        switch (stageNum)
        {
            case 11://森の敵を出す//迷える竜の魂を出す
                enemyObj = Instantiate(enemy0, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 1://森の敵を出す//スライムを出す
                enemyObj = Instantiate(enemy1, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 2://森の敵を出す//ビーバーを出す
                enemyObj = Instantiate(enemy3, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 3://狛犬を出す
                enemyObj = Instantiate(enemy2, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case 4://サメを出す
                enemyObj = Instantiate(enemy4, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 5://ヒトガタを出す
                enemyObj = Instantiate(enemy5, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 6://水神を出す
                enemyObj = Instantiate(enemy6, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 7://パンダを出す
                enemyObj = Instantiate(enemy7, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 8://古都の敵②を出す
                enemyObj = Instantiate(enemy8, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 9://ラスボスを出す
                enemyObj = Instantiate(enemy9, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 12://眠れる竜の魂を出す
                enemyObj = Instantiate(enemy12, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 13://スライムを出す
                enemyObj = Instantiate(enemy1, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 14://ビーバーを出す
                enemyObj = Instantiate(enemy3, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 15://スライムを出す
                enemyObj = Instantiate(enemy1, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 16://ビーバーを出す
                enemyObj = Instantiate(enemy3, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 17://サメを出す
                enemyObj = Instantiate(enemy4, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 18://サメを出す
                enemyObj = Instantiate(enemy4, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 19://スライムを出す
                enemyObj = Instantiate(enemy1, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 30://レッドドラゴンを出す
                enemyObj = Instantiate(enemy13, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                defeatTime = 0;
                isFinalBattle = true;
                break;
            case 20://ヒトガタを出す
                enemyObj = Instantiate(enemy5, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 21://サメを出す
                enemyObj = Instantiate(enemy4, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 22://ヒトガタを出す
                enemyObj = Instantiate(enemy5, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 23://古都の敵②を出す
                enemyObj = Instantiate(enemy8, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 24://ラスボスを出す
                enemyObj = Instantiate(enemy9, new Vector3(1, 0, 0), Quaternion.identity);
                enemyObj.transform.SetParent(enemyParent.transform);
                enemyObj.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                break;
        }
        enemyRect = enemyObj.GetComponent<RectTransform>();
        enemyScript = enemyObj.GetComponent<EnemyScript>();
        enemyRect.anchoredPosition = enemyScript.locationWait;

        resultBoardRec.anchoredPosition = new Vector2(0, 540);

        //UIをどける
        switch (fm.NumOfParty)
        {
            case 1:
                Deck1Rect.anchoredPosition = new Vector2(-250, -380);
                commandBackgroundRect1.anchoredPosition = new Vector2(120, -380);
                break;
            case 2:
                Deck1Rect.anchoredPosition = new Vector2(0, -380);
                commandBackgroundRect1.anchoredPosition = new Vector2(335, -380);
                break;
            case 3:
                Deck1Rect.anchoredPosition = new Vector2(0, -380);
                commandBackgroundRect1.anchoredPosition = new Vector2(335, -380);
                break;
            default:
                break;
        }
        Deck2Rect.anchoredPosition = new Vector2(-330, -380);
        Deck3Rect.anchoredPosition = new Vector2(-330, -460);
        commandBackgroundRect2.anchoredPosition = new Vector2(335, -380);
        commandBackgroundRect2.anchoredPosition = new Vector2(335, -460);
        if (!fm.DoYouHaveDragon)
        {
            monsterImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            monsterImage.color = new Color(1, 1, 1, 1);
        }
        if (!fm.DoYouHaveShisa)
        {
            monster2Image.color = new Color(1, 1, 1, 0);
        }
        else
        {
            monster2Image.color = new Color(1, 1, 1, 1);
        }
        playerRect.anchoredPosition = new Vector2(-600, 50);
        monsterRect.anchoredPosition = new Vector2(-600, 50);
        monster2Rect.anchoredPosition = new Vector2(-600, 50);
        MPSlider.value = 0.5f;
        MPSlider2.value = 0.5f;
        MPSlider3.value = 0.5f;
        MPTextUpdate();
        if (MPSlider.value == 1f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(true); MPImage[7].SetActive(true);
            MPText.text = "AP 8/8";
        }
        else if (MPSlider.value >= 0.875f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(true); MPImage[7].SetActive(false);
            MPText.text = "AP 7/8";
        }
        else if (MPSlider.value >= 0.75f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(true); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 6/8";
        }
        else if (MPSlider.value >= 0.625f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(true); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 5/8";
        }
        else if (MPSlider.value >= 0.5f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(true); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 4/8";
        }
        else if (MPSlider.value >= 0.375f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(true); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 3/8";
        }
        else if (MPSlider.value >= 0.25f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(true); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 2/8";
        }
        else if (MPSlider.value >= 0.125f)
        {
            MPImage[0].SetActive(true); MPImage[1].SetActive(false); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 1/8";
        }
        else
        {
            MPImage[0].SetActive(false); MPImage[1].SetActive(false); MPImage[2].SetActive(false); MPImage[3].SetActive(false); MPImage[4].SetActive(false); MPImage[5].SetActive(false); MPImage[6].SetActive(false); MPImage[7].SetActive(false);
            MPText.text = "AP 0/8";
        }

        if (MPSlider2.value == 1f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(true); MPImage2[7].SetActive(true);
            MPText2.text = "AP 8/8";
        }
        else if (MPSlider2.value >= 0.875f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(true); MPImage2[7].SetActive(false);
            MPText2.text = "AP 7/8";
        }
        else if (MPSlider2.value >= 0.75f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(true); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 6/8";
        }
        else if (MPSlider2.value >= 0.625f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(true); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 5/8";
        }
        else if (MPSlider2.value >= 0.5f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(true); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 4/8";
        }
        else if (MPSlider2.value >= 0.375f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(true); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 3/8";
        }
        else if (MPSlider2.value >= 0.25f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(true); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 2/8";
        }
        else if (MPSlider2.value >= 0.125f)
        {
            MPImage2[0].SetActive(true); MPImage2[1].SetActive(false); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 1/8";
        }
        else
        {
            MPImage2[0].SetActive(false); MPImage2[1].SetActive(false); MPImage2[2].SetActive(false); MPImage2[3].SetActive(false); MPImage2[4].SetActive(false); MPImage2[5].SetActive(false); MPImage2[6].SetActive(false); MPImage2[7].SetActive(false);
            MPText2.text = "AP 0/8";
        }
        if (MPSlider3.value == 1f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(true); MPImage3[7].SetActive(true);
            MPText3.text = "AP 8/8";
        }
        else if (MPSlider3.value >= 0.875f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(true); MPImage3[7].SetActive(false);
            MPText3.text = "AP 7/8";
        }
        else if (MPSlider3.value >= 0.75f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(true); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 6/8";
        }
        else if (MPSlider3.value >= 0.625f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(true); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 5/8";
        }
        else if (MPSlider3.value >= 0.5f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(true); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 4/8";
        }
        else if (MPSlider3.value >= 0.375f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(true); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 3/8";
        }
        else if (MPSlider3.value >= 0.25f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(true); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 2/8";
        }
        else if (MPSlider3.value >= 0.125f)
        {
            MPImage3[0].SetActive(true); MPImage3[1].SetActive(false); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 1/8";
        }
        else
        {
            MPImage3[0].SetActive(false); MPImage3[1].SetActive(false); MPImage3[2].SetActive(false); MPImage3[3].SetActive(false); MPImage3[4].SetActive(false); MPImage3[5].SetActive(false); MPImage3[6].SetActive(false); MPImage3[7].SetActive(false);
            MPText3.text = "AP 0/8";
        }
        fightID = 1;
        darkImage[0].SetActive(false); darkImage[1].SetActive(true); darkImage[2].SetActive(true);
        Deck1Rect.localScale = new Vector3(1, 1, 1);
        Deck2Rect.localScale = new Vector3(0.7f, 0.7f, 1);
        Deck3Rect.localScale = new Vector3(0.7f,0.7f, 1);
        //プレイヤーが走ってくるアニメ―ション
        StartCoroutine("BattlePlayerFadeIn");
    }

    IEnumerator BattlePlayerFadeIn()
    {
        animPlayer.SetBool("run", true);

        //説明テキストを更新する
        if (fm.NumOfParty >= 2)
        {
            explainTextBattle.text = "【↑↓】交代　【←→】コマンド選択　【Z】コマンド実行";
        }
        else
        {
            explainTextBattle.text = "【←→】コマンド選択　【Z】コマンド実行";
        }

        yield return new WaitForSeconds(1.0f);
        monsterRect.transform.DOLocalMove(new Vector3(-350, 50, 0f), 1.0f);
        monster2Rect.transform.DOLocalMove(new Vector3(-350, 50, 0f), 1.0f);
        playerObj.transform.DOLocalMove(new Vector3(-150, 50, 0f), 1.0f)
        .OnComplete(() => {
            animPlayer.SetBool("run", false);
            BattleBeginning();
         });
    }

    IEnumerator abcde()
    {
        choiceBattleObj.SetActive(true);
        for (int i = 1; i < 11; i++)
        {
            choiceBattleObj.GetComponent<RectTransform>().localScale = new Vector3(i * 0.1f, i * 0.1f, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator UIShowUp()//UIを表示する
    {
        switch (fm.NumOfParty)
        {
            case 1:
                Deck1Obj.transform.DOLocalMove(new Vector3(-250, -180, 0f), 1.0f).OnComplete(() =>
                {
                    BattleSwitch = true;
                });
                commandBackgroundObj1.transform.DOLocalMove(new Vector3(120, -180, 0f), 1.0f);
                break;
            case 2:
                Deck1Obj.transform.DOLocalMove(new Vector3(0, -180, 0f), 1.0f).OnComplete(() =>
                {
                    BattleSwitch = true;
                });
                Deck2Obj.transform.DOLocalMove(new Vector3(-330, -180, 0f), 1.0f);
                commandBackgroundObj1.transform.DOLocalMove(new Vector3(335, -180, 0f), 1.0f);
                break;
            case 3:
                Deck1Obj.transform.DOLocalMove(new Vector3(0, -180, 0f), 1.0f).OnComplete(() =>
                {
                    BattleSwitch = true;
                });
                Deck2Obj.transform.DOLocalMove(new Vector3(-330, -140, 0f), 1.0f);
                Deck3Obj.transform.DOLocalMove(new Vector3(-330, -220, 0f), 1.0f);
                commandBackgroundObj1.transform.DOLocalMove(new Vector3(335, -180, 0f), 1.0f);
                break;
            default:
                break;
        }
        yield return null;
    }

    public void BattleBeginning()
    {
        //選択肢を消す
        choiceBattleObj.SetActive(false);
        //UIを出す//モンスターを出す
        StartCoroutine("UIShowUp");
        MonsterName.text = "プレイヤー Lv" + (fm.ATK0Lv + fm.SPD0Lv + fm.Ability0Lv);
        ATK0 = (int)(fm.ATK0 + fm.ATK0Lv * 30f);
        SPD0 = (int)(fm.SPD0 + fm.SPD0Lv * 30f);
        faceImage1.SetActive(true);
        faceImage2.SetActive(false);
        faceImage3.SetActive(false);
        animMonster.SetBool("w1", true);

        MonsterName2.text = "レッサードラゴン Lv" + (fm.ATKLv + fm.SPDLv + fm.AbilityLv);
        ATK1 = (int)(fm.ATK1 + fm.ATKLv * 30f);
        SPD1 = (int)(fm.SPD1 + fm.SPDLv * 30f);

        MonsterName3.text = "サムライドッグ Lv" + (fm.ATK2Lv + fm.SPD2Lv + fm.Ability2Lv);
        ATK2 = (int)(fm.ATK2 + fm.ATK2Lv * 30f);
        SPD2 = (int)(fm.SPD2 + fm.SPD2Lv * 30f);

        ActionTextRTransform.anchoredPosition = new Vector2(-50, 300);
    }

    public void OnFightButtonClick()
    {
        if (!isActing && MPSlider.value >= 0.125f)
        {
            MPSlider.value -= 0.125f;
            MPTextUpdate();
            isSurpriseAttack = false;
            StopCoroutine("FightActionPlayer");
            StartCoroutine("FightActionPlayer");

            recast = true;
            StartCoroutine("RecastShort");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void OnFight2ButtonClick()
    {
        if (!isActing && MPSlider2.value >= 0.125f)
        {
            MPSlider2.value -= 0.125f;
            MPTextUpdate();
            isSurpriseAttack = false;
            StartCoroutine("FightAction");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void OnFight3ButtonClick()
    {
        if (!isActing && MPSlider3.value >= 0.125f)
        {
            MPSlider3.value -= 0.125f;
            MPTextUpdate();
            isSurpriseAttack = true;
            StartCoroutine("FightAction");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void OnGuardButtonClick()
    {
        if (!isActing && MPSlider.value >= 0.125f)
        {
            MPSlider.value -= 0.125f;
            MPTextUpdate();
            StartCoroutine("GuardAction");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void OnGuard2ButtonClick()
    {
        if (!isActing && MPSlider2.value >= 0.125f)
        {
            MPSlider2.value -= 0.125f;
            MPTextUpdate();
            StartCoroutine("GuardAction");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void OnGuard3ButtonClick()
    {
        if (!isActing && MPSlider2.value >= 0.125f)
        {
            MPSlider3.value -= 0.125f;
            MPTextUpdate();
            StartCoroutine("GuardAction");
        }
        else if (!isActing && MPSlider.value < 0.125f)
        {
            StartCoroutine("APShortage");
        }
    }

    public void MPTextUpdate()
    {
        if (MPSlider.value == 1)
        {
            MPText.text = "AP 8/8";
        }
        else if (MPSlider.value >= 0.875f)
        {
            MPText.text = "AP 7/8";
        }
        else if (MPSlider.value >= 0.75f)
        {
            MPText.text = "AP 6/8";
        }
        else if (MPSlider.value >= 0.625f)
        {
            MPText.text = "AP 5/8";
        }
        else if (MPSlider.value >= 0.5f)
        {
            MPText.text = "AP 4/8";
        }
        else if (MPSlider.value >= 0.375f)
        {
            MPText.text = "AP 3/8";
        }
        else if (MPSlider.value >= 0.25f)
        {
            MPText.text = "AP 2/8";
        }
        else if (MPSlider.value >= 0.125f)
        {
            MPText.text = "AP 1/8";
        }
        else
        {
            MPText.text = "AP 0/8";
        }
    }

    IEnumerator FightAction()
    {
        isActing = true;
        if (isGuard)
        {
            isGuard = false;
            guardImage[0].SetActive(false);
            guardImage[1].SetActive(false);
            guardImage[2].SetActive(false);
            StopCoroutine("GuardAnimation1");
            StopCoroutine("GuardAnimation2");
            StopCoroutine("GuardAnimation3");
        }
        StopCoroutine("actionTextAnim");
        StartCoroutine("actionTextAnim");
        ActionText.text = "たたかう";
        if (isSurpriseAttack)
        {
            ActionText.text = "みねうち";
            fm.entireCommandUseThree++;
        }
        else
        {
            fm.entireCommandUseOne++;
        }
        switch (fightID)
        {
            case 1://プレイヤーの攻撃
                playerRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x - 100, 50, 0), 0.3f).OnComplete(() =>//0.32
                {
                    playerRect.transform.DOLocalMove(positionShisaDO, 0.2f)//0.32
                    .SetDelay(0.3f);//0.56
                });
                break;
            case 2://ドラゴンの攻撃
                monsterRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x - 100, 50, 0), 0.3f).OnComplete(() =>//0.32
                {
                    monsterRect.transform.DOLocalMove(positionShisaDO, 0.2f)//0.32
                    .SetDelay(0.3f);//0.56
                });
                break;
            case 3://サムライドッグの攻撃
                monster2Rect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x - 100, 50, 0), (0.3f * (1 - (0.09f * DogSkillLevel)))).OnComplete(() =>//0.32
                {
                    monster2Rect.transform.DOLocalMove(positionShisaDO, (0.2f * (1 - (0.09f * DogSkillLevel))))//0.32
                    .SetDelay((0.3f * (1 - (0.09f * DogSkillLevel))));//0.56
                });
                break;
            default:
                break;
        }
        if (fightID == 3)
        {
            yield return new WaitForSeconds((0.16f * (1 - (0.09f * DogSkillLevel))));//0.16
        }
        else
        {
            yield return new WaitForSeconds(0.16f);//0.16
        }
        if (!enemyScript.isHiding)
        {
            if (isSurpriseAttack)
            {
                audioSource.PlayOneShot(SEpunch2);
            }
            else
            {
                audioSource.PlayOneShot(SEpunch);
            }
        }
        else
        {
            audioSource.PlayOneShot(SEMiss);
        }
        //エフェクト出す
        if (fightID == 2)
        {
            fistEffectObj = Instantiate(fistEffect, new Vector3(1, 0, 0), Quaternion.identity);
            fistEffectObj.transform.SetParent(battleBackground.transform);
            fistEffectObj.transform.localScale = new Vector3(1, 1, 1);
            fistEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
        }
        else
        {
            katanaEffectObj = Instantiate(katanaEffect, new Vector3(1, 0, 0), Quaternion.identity);
            katanaEffectObj.transform.SetParent(battleBackground.transform);
            katanaEffectObj.transform.localScale = new Vector3(1, 1, 1);
            katanaEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
        }
        if (fightID == 3)
        {
            yield return new WaitForSeconds((0.32f * (1 - (0.09f * DogSkillLevel))));//0.16
        }
        else
        {
            yield return new WaitForSeconds(0.32f);//0.16
        }
        if (!enemyScript.isHiding)
        {
            StartCoroutine("enemyVibration");
        }
        float rnd = Random.Range(0.9f, 1.1f);
        int damage =0;
        switch (fightID)
        {
            case 1:
                damage = (int)Mathf.Round((ATK0 - (float)enemyScript.DEF) * rnd);
                break;
            case 2:
                damage = (int)Mathf.Round((ATK1 - (float)enemyScript.DEF) * rnd);
                damage += (int)((float)damage * (0.1f + (DragonSkillLevel / 50f)));
                break;
            case 3:
                damage = (int)Mathf.Round((ATK2 - (float)enemyScript.DEF) * rnd);
                break;
            default:
                break;
        }
        if (enemyScript.isHiding)
        {
            damage = 0;
        }
        if (isSurpriseAttack)
        {
            damage = (int)Mathf.Round(damage * 0.6f);
        }
        if (damage < 0)
        {
            damage = 0;
        }

        if (enemyScript.isKnockOut)
        {
            damage = damage * 2;
        }

        enemyScript.HP -= damage;
        enemyScript.HPUpdate();
        if (!isSurpriseAttack && !enemyScript.isHiding)
        {
            if (!enemyScript.isKnockOut)
            {
                switch (enemyScript.KnockOutResistance)
                {
                    case 0:
                        enemyScript.KnockOut += 13f * rnd;
                        break;
                    case 1:
                        enemyScript.KnockOut += 7f * rnd;
                        break;
                    case 2:
                        enemyScript.KnockOut += 5f * rnd;
                        break;
                    case 3:
                        enemyScript.KnockOut += 3f * rnd;
                        break;
                    default:
                        Debug.Log("ノックアウト耐性を設定していない");
                        break;
                }
            }
        }
        else if (!enemyScript.isHiding)
        {
            if (!enemyScript.isKnockOut)
            {
                switch (enemyScript.KnockOutResistance)
                {
                    case 0:
                        enemyScript.KnockOut += 27.5f * rnd;
                        break;
                    case 1:
                        enemyScript.KnockOut += 17f * rnd;
                        break;
                    case 2:
                        enemyScript.KnockOut += 12f * rnd;
                        break;
                    case 3:
                        enemyScript.KnockOut += 8f * rnd;
                        break;
                    default:
                        Debug.Log("ノックアウト耐性を設定していない");
                        break;
                }
            }
        }
        
        enemyScript.KOUpdate();
        if (enemyScript.HP <= 0)
        {
            if (isFinalBattle)
            {
                isFinalBattle = false;
                isFinalBattle2 = true;
                //敵を下げて、新しい敵を出して戻す
                enemyRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x + 500f, enemyRect.anchoredPosition.y, 0), 1.0f)
                    .OnComplete(() => {
                        enemyObj = Instantiate(enemy14, new Vector3(1, 0, 0), Quaternion.identity);
                        enemyObj.transform.SetParent(enemyParent.transform);
                        enemyObj.transform.localScale = new Vector3(1, 1, 1);
                        enemyRect = enemyObj.GetComponent<RectTransform>();
                        enemyScript = enemyObj.GetComponent<EnemyScript>();
                        enemyRect.anchoredPosition = enemyScript.locationWait + new Vector2(500,0);
                        enemyRect.transform.DOLocalMove(new Vector3(enemyScript.locationWait.x, enemyScript.locationWait.y, 0), 1.0f);
                        Destroy(GameObject.Find("enemy11(Clone)"));
                        BattleSwitch = true;
                    });
            }
            else if (isFinalBattle2)
            {
                isFinalBattle2 = false;
                isFinalBattle3 = true;
                //敵を下げて、新しい敵を出して戻す
                enemyRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x + 500f, enemyRect.anchoredPosition.y, 0), 1.0f)
                    .OnComplete(() => {
                        enemyObj = Instantiate(enemy15, new Vector3(1, 0, 0), Quaternion.identity);
                        enemyObj.transform.SetParent(enemyParent.transform);
                        enemyObj.transform.localScale = new Vector3(1, 1, 1);
                        enemyRect = enemyObj.GetComponent<RectTransform>();
                        enemyScript = enemyObj.GetComponent<EnemyScript>();
                        enemyRect.anchoredPosition = enemyScript.locationWait + new Vector2(500, 0);
                        enemyRect.transform.DOLocalMove(new Vector3(enemyScript.locationWait.x, enemyScript.locationWait.y, 0), 1.0f);
                        Destroy(GameObject.Find("enemy12(Clone)"));
                        BattleSwitch = true;
                    });
            }
            else if (isFinalBattle3)
            {
                isFinalBattle3 = false;
                BattleSwitch = false;

                StartCoroutine("BattleResult");
            }
            else
            {
                BattleSwitch = false;

                StartCoroutine("BattleResult");
            }
        }

        damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
        damageTextObj.transform.SetParent(battleBackground.transform);
        damageTextObj.transform.localScale = new Vector3(1, 1, 1);
        damageTextObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
        damageTextObj.GetComponent<Text>().text = "" + damage;
        if (enemyScript.isKnockOut)
        {
            damageTextObj.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            damageTextObj.GetComponent<Text>().color= new Color(1, 1, 0.4f, 1);
        }
        if (enemyScript.isHiding)
        {
            damageTextObj.GetComponent<Text>().text = "MISS!";
        }
        damageTextObj.GetComponent<DamageTextScript>().isHiding = enemyScript.isHiding;
        if (fightID == 3)
        {
            yield return new WaitForSeconds((0.3f * (1 - (0.09f * DogSkillLevel))));//0.16
        }
        else
        {
            yield return new WaitForSeconds(0.3f);//0.16
        }
        isActing = false;
    }
    IEnumerator FightActionPlayer()
    {
        fm.entireCommandUseOne++;
        isActing = true;
        if (isGuard)
        {
            isGuard = false;
            guardImage[0].SetActive(false);
            guardImage[1].SetActive(false);
            guardImage[2].SetActive(false);
            StopCoroutine("GuardAnimation1");
            StopCoroutine("GuardAnimation2");
            StopCoroutine("GuardAnimation3");
        }
        StopCoroutine("actionTextAnim");
        StartCoroutine("actionTextAnim");
        ActionText.text = "たたかう";
        if (isSurpriseAttack)
        {
            ActionText.text = "みねうち";
        }
        if (attackLevel == 0)
        {
            animPlayer.SetBool("attackOne", true);
            animPlayer.SetBool("attackThree", false);
            attackLevel = 1;
            yield return new WaitForSeconds(0.2f);
        }
        else if (attackLevel == 1)
        {
            animPlayer.SetBool("attackTwo", true);
            animPlayer.SetBool("attackOne", false);
            attackLevel = 2;
            yield return new WaitForSeconds(0.1f);
        }
        else if (attackLevel == 2)
        {
            animPlayer.SetBool("attackThree", true);
            animPlayer.SetBool("attackTwo", false);
            attackLevel = 3;
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            attackLevel = 0;
        }
        //音鳴らす
        if (!enemyScript.isHiding)
        {
            if (isSurpriseAttack)
            {
                audioSource.PlayOneShot(SEpunch2);
            }
            else if (attackLevel != 3)
            {
                audioSource.PlayOneShot(SEPlayerAttack);
            }
            else
            {
                audioSource.PlayOneShot(SEAttackHard);
            }
        }
        else
        {
            audioSource.PlayOneShot(SEMiss);
        }

        //エフェクト出す
        swordEffectObj = Instantiate(swordEffect, new Vector3(1, 0, 0), Quaternion.identity);
        swordEffectObj.transform.SetParent(battleBackground.transform);
        swordEffectObj.transform.localScale = new Vector3(1, 1, 1);
        swordEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
        yield return new WaitForSeconds(0.16f);
        //敵を振動させる
        if (!enemyScript.isHiding)
        {
            StartCoroutine("enemyVibration");
        }
        float rnd = Random.Range(0.9f, 1.1f);
        int damage = 0;
        damage = (int)Mathf.Round((ATK0 - (float)enemyScript.DEF) * rnd);
        if (attackLevel == 3)
        {
            damage = (int)(damage * 2f);
        }
        if (enemyScript.isHiding)
        {
            damage = 0;
        }
        if (isSurpriseAttack)
        {
            damage = (int)Mathf.Round(damage * 0.6f);
        }
        if (damage < 0)
        {
            damage = 0;
        }
        enemyScript.HP -= damage;
        enemyScript.HPUpdate();
        if (!isSurpriseAttack && !enemyScript.isHiding)
        {
            if (!enemyScript.isKnockOut)
            {
                switch (enemyScript.KnockOutResistance)
                {
                    case 0:
                        enemyScript.KnockOut += 13f * rnd;
                        break;
                    case 1:
                        enemyScript.KnockOut += 7f * rnd;
                        break;
                    case 2:
                        enemyScript.KnockOut += 5f * rnd;
                        break;
                    case 3:
                        enemyScript.KnockOut += 3f * rnd;
                        break;
                    default:
                        Debug.Log("ノックアウト耐性を設定していない");
                        break;
                }
            }
        }
        else if (!enemyScript.isHiding)
        {
            if (!enemyScript.isKnockOut)
            {
                switch (enemyScript.KnockOutResistance)
                {
                    case 0:
                        enemyScript.KnockOut += 27.5f * rnd;
                        break;
                    case 1:
                        enemyScript.KnockOut += 17f * rnd;
                        break;
                    case 2:
                        enemyScript.KnockOut += 12f * rnd;
                        break;
                    case 3:
                        enemyScript.KnockOut += 8f * rnd;
                        break;
                    default:
                        Debug.Log("ノックアウト耐性を設定していない");
                        break;
                }
            }
        }

        enemyScript.KOUpdate();
        if (enemyScript.HP <= 0)
        {
            if (isFinalBattle)
            {
                isFinalBattle = false;
                isFinalBattle2 = true;
                //敵を下げて、新しい敵を出して戻す
                enemyRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x + 500f, enemyRect.anchoredPosition.y, 0), 1.0f)
                    .OnComplete(() => {
                        enemyObj = Instantiate(enemy14, new Vector3(1, 0, 0), Quaternion.identity);
                        enemyObj.transform.SetParent(enemyParent.transform);
                        enemyObj.transform.localScale = new Vector3(1, 1, 1);
                        enemyRect = enemyObj.GetComponent<RectTransform>();
                        enemyScript = enemyObj.GetComponent<EnemyScript>();
                        enemyRect.anchoredPosition = enemyScript.locationWait + new Vector2(500, 0);
                        enemyRect.transform.DOLocalMove(new Vector3(enemyScript.locationWait.x, enemyScript.locationWait.y, 0), 1.0f);
                        Destroy(GameObject.Find("enemy11(Clone)"));
                        BattleSwitch = true;
                    });
            }
            else if (isFinalBattle2)
            {
                isFinalBattle2 = false;
                isFinalBattle3 = true;
                //敵を下げて、新しい敵を出して戻す
                enemyRect.transform.DOLocalMove(new Vector3(enemyRect.anchoredPosition.x + 500f, enemyRect.anchoredPosition.y, 0), 1.0f)
                    .OnComplete(() => {
                        enemyObj = Instantiate(enemy15, new Vector3(1, 0, 0), Quaternion.identity);
                        enemyObj.transform.SetParent(enemyParent.transform);
                        enemyObj.transform.localScale = new Vector3(1, 1, 1);
                        enemyRect = enemyObj.GetComponent<RectTransform>();
                        enemyScript = enemyObj.GetComponent<EnemyScript>();
                        enemyRect.anchoredPosition = enemyScript.locationWait + new Vector2(500, 0);
                        enemyRect.transform.DOLocalMove(new Vector3(enemyScript.locationWait.x, enemyScript.locationWait.y, 0), 1.0f);
                        Destroy(GameObject.Find("enemy12(Clone)"));
                        BattleSwitch = true;
                    });
            }
            else if (isFinalBattle3)
            {
                isFinalBattle3 = false;
                BattleSwitch = false;

                StartCoroutine("BattleResult");
            }
            else
            {
                BattleSwitch = false;

                StartCoroutine("BattleResult");
            }
        }

        damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
        damageTextObj.transform.SetParent(battleBackground.transform);
        damageTextObj.transform.localScale = new Vector3(1, 1, 1);
        damageTextObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
        damageTextObj.GetComponent<Text>().text = "" + damage;
        if (enemyScript.isKnockOut)
        {
            damageTextObj.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            damageTextObj.GetComponent<Text>().color = new Color(1, 1, 0.4f, 1);
        }
        if (enemyScript.isHiding)
        {
            damageTextObj.GetComponent<Text>().text = "MISS!";
        }
        damageTextObj.GetComponent<DamageTextScript>().isHiding = enemyScript.isHiding;
        if (attackLevel != 3)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.83f);
        }
        isActing = false;
        
        if (attackLevel != 3)
        {
            yield return new WaitForSeconds(0.4f);//0.16
        }
        
        animPlayer.SetBool("attackOne", false);
        animPlayer.SetBool("attackTwo", false);
        animPlayer.SetBool("attackThree", false);
        attackLevel = 0;
    }
    IEnumerator GuardAction()
    {
        if (!isGuard)
        {
            fm.entireCommandUseTwo++;
            isActing = true;
            StopCoroutine("actionTextAnim");
            StartCoroutine("actionTextAnim");
            ActionText.text = "まもる";
            isGuard = true;
            switch (fightID)
            {
                case 1:
                    guardImage[0].SetActive(true);
                    StartCoroutine("GuardAnimation1");
                    break;
                case 2:
                    guardImage[1].SetActive(true);
                    StartCoroutine("GuardAnimation2");
                    break;
                case 3:
                    guardImage[2].SetActive(true);
                    StartCoroutine("GuardAnimation3");
                    break;
                default:
                    break;
            }
            audioSource.PlayOneShot(SEShield);
            yield return new WaitForSeconds(0.5f);
            isActing = false;
        }
        else
        {
            isGuard = false;
            guardImage[0].SetActive(false);
            guardImage[1].SetActive(false);
            guardImage[2].SetActive(false);

            StopCoroutine("GuardAnimation1");
            StopCoroutine("GuardAnimation2");
            StopCoroutine("GuardAnimation3");

            isActing = false;
        }
    }

    IEnumerator enemyVibration()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                enemyRect.anchoredPosition += new Vector2(5,0);
            }
            else
            {
                enemyRect.anchoredPosition -= new Vector2(5, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MonsterChangeUp()
    {
        fm.entireCommandUseFour++;
        if (isGuard)
        {
            isGuard = false;
            guardImage[0].SetActive(false);
            guardImage[1].SetActive(false);
            guardImage[2].SetActive(false);
        }
        switch (fightID)//場に出ているモンスターを変える
        {
            case 1:
                switch (fm.NumOfParty)
                {
                    case 2:
                        fightID = 2;
                        break;
                    case 3:
                        fightID = 2;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (fm.NumOfParty)
                {
                    case 2:
                        fightID = 1;
                        break;
                    case 3:
                        fightID = 3;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                fightID = 1;
                break;
            default:
                break;
        }
        StartCoroutine("monsterGoBack");
        switch (fm.NumOfParty)
        {
            case 2:
                if (fightID == 2)//戦うポケモンを１から２へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(false); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck1Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck2Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//２体、上入力、状態２へ
                        {
                            case 1:
                                Deck1Rect.anchoredPosition = new Vector2(-8, -211);
                                Deck2Rect.anchoredPosition = new Vector2(-322, -149);
                                commandBackgroundRect1.anchoredPosition = new Vector2(342,-211);
                                commandBackgroundRect2.anchoredPosition = new Vector2(627, -149);
                                break;
                            case 2:
                                Deck1Rect.anchoredPosition = new Vector2(-32, -239);
                                Deck2Rect.anchoredPosition = new Vector2(-298, -121);
                                commandBackgroundRect1.anchoredPosition = new Vector2(363, -239);
                                commandBackgroundRect2.anchoredPosition = new Vector2(606, -121);
                                break;
                            case 3:
                                Deck1Rect.anchoredPosition = new Vector2(-68, -261);
                                Deck2Rect.anchoredPosition = new Vector2(-262, -99);
                                commandBackgroundRect1.anchoredPosition = new Vector2(396, -261);
                                commandBackgroundRect2.anchoredPosition = new Vector2(573, -99);
                                break;
                            case 4:
                                Deck1Rect.anchoredPosition = new Vector2(-114, -275);
                                Deck2Rect.anchoredPosition = new Vector2(-216, -85);
                                commandBackgroundRect1.anchoredPosition = new Vector2(438, -275);
                                commandBackgroundRect2.anchoredPosition = new Vector2(531, -85);
                                break;
                            case 5:
                                Deck1Rect.anchoredPosition = new Vector2(-165, -280);
                                Deck2Rect.anchoredPosition = new Vector2(-165, -80);
                                commandBackgroundRect1.anchoredPosition = new Vector2(485, -280);
                                commandBackgroundRect2.anchoredPosition = new Vector2(485, -80);
                                break;
                            case 6:
                                Deck1Rect.anchoredPosition = new Vector2(-216, -275);
                                Deck2Rect.anchoredPosition = new Vector2(-114, -85);
                                commandBackgroundRect1.anchoredPosition = new Vector2(531, -275);
                                commandBackgroundRect2.anchoredPosition = new Vector2(438, -85);
                                break;
                            case 7:
                                Deck1Rect.anchoredPosition = new Vector2(-262, -261);
                                Deck2Rect.anchoredPosition = new Vector2(-68, -99);
                                commandBackgroundRect1.anchoredPosition = new Vector2(573, -261);
                                commandBackgroundRect2.anchoredPosition = new Vector2(396, -99);
                                break;
                            case 8:
                                Deck1Rect.anchoredPosition = new Vector2(-298, -239);
                                Deck2Rect.anchoredPosition = new Vector2(-32, -121);
                                commandBackgroundRect1.anchoredPosition = new Vector2(606, -239);
                                commandBackgroundRect2.anchoredPosition = new Vector2(363, -121);
                                break;
                            case 9:
                                Deck1Rect.anchoredPosition = new Vector2(-322, -211);
                                Deck2Rect.anchoredPosition = new Vector2(-8, -149);
                                commandBackgroundRect1.anchoredPosition = new Vector2(627, -211);
                                commandBackgroundRect2.anchoredPosition = new Vector2(342, -149);
                                break;
                            case 10:
                                Deck1Rect.anchoredPosition = new Vector2(-330, -180);
                                Deck2Rect.anchoredPosition = new Vector2(-0, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(335, -180);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else
                {
                    darkImage[0].SetActive(false); darkImage[1].SetActive(true); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck2Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck1Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//２体、上入力、状態１へ
                        {
                            case 1:
                                Deck2Rect.anchoredPosition = new Vector2(-8, -211);
                                Deck1Rect.anchoredPosition = new Vector2(-322, -149);
                                commandBackgroundRect2.anchoredPosition = new Vector2(342, -211);
                                commandBackgroundRect1.anchoredPosition = new Vector2(627, -149);
                                break;
                            case 2:
                                Deck2Rect.anchoredPosition = new Vector2(-32, -239);
                                Deck1Rect.anchoredPosition = new Vector2(-298, -121);
                                commandBackgroundRect2.anchoredPosition = new Vector2(363, -239);
                                commandBackgroundRect1.anchoredPosition = new Vector2(606, -121);
                                break;
                            case 3:
                                Deck2Rect.anchoredPosition = new Vector2(-68, -261);
                                Deck1Rect.anchoredPosition = new Vector2(-262, -99);
                                commandBackgroundRect2.anchoredPosition = new Vector2(396, -261);
                                commandBackgroundRect1.anchoredPosition = new Vector2(573, -99);
                                break;
                            case 4:
                                Deck2Rect.anchoredPosition = new Vector2(-114, -275);
                                Deck1Rect.anchoredPosition = new Vector2(-216, -85);
                                commandBackgroundRect2.anchoredPosition = new Vector2(438, -275);
                                commandBackgroundRect1.anchoredPosition = new Vector2(531, -85);
                                break;
                            case 5:
                                Deck2Rect.anchoredPosition = new Vector2(-165, -280);
                                Deck1Rect.anchoredPosition = new Vector2(-165, -80);
                                commandBackgroundRect2.anchoredPosition = new Vector2(485, -280);
                                commandBackgroundRect1.anchoredPosition = new Vector2(485, -80);
                                break;
                            case 6:
                                Deck2Rect.anchoredPosition = new Vector2(-216, -275);
                                Deck1Rect.anchoredPosition = new Vector2(-114, -85);
                                commandBackgroundRect2.anchoredPosition = new Vector2(531, -275);
                                commandBackgroundRect1.anchoredPosition = new Vector2(438, -85);
                                break;
                            case 7:
                                Deck2Rect.anchoredPosition = new Vector2(-262, -261);
                                Deck1Rect.anchoredPosition = new Vector2(-68, -99);
                                commandBackgroundRect2.anchoredPosition = new Vector2(573, -261);
                                commandBackgroundRect1.anchoredPosition = new Vector2(396, -99);
                                break;
                            case 8:
                                Deck2Rect.anchoredPosition = new Vector2(-298, -239);
                                Deck1Rect.anchoredPosition = new Vector2(-32, -121);
                                commandBackgroundRect2.anchoredPosition = new Vector2(606, -239);
                                commandBackgroundRect1.anchoredPosition = new Vector2(363, -121);
                                break;
                            case 9:
                                Deck2Rect.anchoredPosition = new Vector2(-322, -211);
                                Deck1Rect.anchoredPosition = new Vector2(-8, -149);
                                commandBackgroundRect2.anchoredPosition = new Vector2(627, -211);
                                commandBackgroundRect1.anchoredPosition = new Vector2(342, -149);
                                break;
                            case 10:
                                Deck2Rect.anchoredPosition = new Vector2(-330, -180);
                                Deck1Rect.anchoredPosition = new Vector2(-0, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(335, -180);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                break;
            case 3:
                if (fightID == 2)//戦うポケモンを１から２へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(false); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck1Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck2Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、上入力、状態２へ
                        {
                            case 1:
                                Deck1Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck2Rect.anchoredPosition = new Vector2(-288, -136);
                                Deck3Rect.anchoredPosition = new Vector2(-367, -214);
                                commandBackgroundRect1.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect2.anchoredPosition = new Vector2(597, -136);
                                commandBackgroundRect3.anchoredPosition = new Vector2(669, -214);
                                break;
                            case 2:
                                Deck1Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck2Rect.anchoredPosition = new Vector2(-243, -134);
                                Deck3Rect.anchoredPosition = new Vector2(-398, -207);
                                commandBackgroundRect1.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect2.anchoredPosition = new Vector2(556, -134);
                                commandBackgroundRect3.anchoredPosition = new Vector2(697, -207);
                                break;
                            case 3:
                                Deck1Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck2Rect.anchoredPosition = new Vector2(-197, -134);
                                Deck3Rect.anchoredPosition = new Vector2(-421, -199);
                                commandBackgroundRect1.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect2.anchoredPosition = new Vector2(514, -134);
                                commandBackgroundRect3.anchoredPosition = new Vector2(718, -199);
                                break;
                            case 4:
                                Deck1Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck2Rect.anchoredPosition = new Vector2(-152, -136);
                                Deck3Rect.anchoredPosition = new Vector2(-435, -190);
                                commandBackgroundRect1.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect2.anchoredPosition = new Vector2(473, -136);
                                commandBackgroundRect3.anchoredPosition = new Vector2(731, -190);
                                break;
                            case 5:
                                Deck1Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck2Rect.anchoredPosition = new Vector2(-110, -140);
                                Deck3Rect.anchoredPosition = new Vector2(-440, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect2.anchoredPosition = new Vector2(435, -140);
                                commandBackgroundRect3.anchoredPosition = new Vector2(735, -180);
                                break;
                            case 6:
                                Deck1Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck2Rect.anchoredPosition = new Vector2(-73, -146);
                                Deck3Rect.anchoredPosition = new Vector2(-435, -170);
                                commandBackgroundRect1.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect2.anchoredPosition = new Vector2(401, -146);
                                commandBackgroundRect3.anchoredPosition = new Vector2(731, -170);
                                break;
                            case 7:
                                Deck1Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck2Rect.anchoredPosition = new Vector2(-42, -153);
                                Deck3Rect.anchoredPosition = new Vector2(-421, -161);
                                commandBackgroundRect1.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect2.anchoredPosition = new Vector2(373, -153);
                                commandBackgroundRect3.anchoredPosition = new Vector2(718, -161);
                                break;
                            case 8:
                                Deck1Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck2Rect.anchoredPosition = new Vector2(-19, -161);
                                Deck3Rect.anchoredPosition = new Vector2(-398, -153);
                                commandBackgroundRect1.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect2.anchoredPosition = new Vector2(352, -161);
                                commandBackgroundRect3.anchoredPosition = new Vector2(697, -153);
                                break;
                            case 9:
                                Deck1Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck2Rect.anchoredPosition = new Vector2(-5, -170);
                                Deck3Rect.anchoredPosition = new Vector2(-367, -146);
                                commandBackgroundRect1.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect2.anchoredPosition = new Vector2(339, -170);
                                commandBackgroundRect3.anchoredPosition = new Vector2(669, -146);
                                break;
                            case 10:
                                Deck1Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck2Rect.anchoredPosition = new Vector2(0, -180);
                                Deck3Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect2.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect3.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else if(fightID == 3)//戦うポケモンを２から３へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(true); darkImage[2].SetActive(false);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck2Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck3Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、上入力、状態２へ
                        {
                            case 1:
                                Deck2Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck3Rect.anchoredPosition = new Vector2(-288, -136);
                                Deck1Rect.anchoredPosition = new Vector2(-367, -214);
                                commandBackgroundRect2.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect3.anchoredPosition = new Vector2(597, -136);
                                commandBackgroundRect1.anchoredPosition = new Vector2(669, -214);
                                break;
                            case 2:
                                Deck2Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck3Rect.anchoredPosition = new Vector2(-243, -134);
                                Deck1Rect.anchoredPosition = new Vector2(-398, -207);
                                commandBackgroundRect2.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect3.anchoredPosition = new Vector2(556, -134);
                                commandBackgroundRect1.anchoredPosition = new Vector2(697, -207);
                                break;
                            case 3:
                                Deck2Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck3Rect.anchoredPosition = new Vector2(-197, -134);
                                Deck1Rect.anchoredPosition = new Vector2(-421, -199);
                                commandBackgroundRect2.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect3.anchoredPosition = new Vector2(514, -134);
                                commandBackgroundRect1.anchoredPosition = new Vector2(718, -199);
                                break;
                            case 4:
                                Deck2Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck3Rect.anchoredPosition = new Vector2(-152, -136);
                                Deck1Rect.anchoredPosition = new Vector2(-435, -190);
                                commandBackgroundRect2.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect3.anchoredPosition = new Vector2(473, -136);
                                commandBackgroundRect1.anchoredPosition = new Vector2(731, -190);
                                break;
                            case 5:
                                Deck2Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck3Rect.anchoredPosition = new Vector2(-110, -140);
                                Deck1Rect.anchoredPosition = new Vector2(-440, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect3.anchoredPosition = new Vector2(435, -140);
                                commandBackgroundRect1.anchoredPosition = new Vector2(735, -180);
                                break;
                            case 6:
                                Deck2Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck3Rect.anchoredPosition = new Vector2(-73, -146);
                                Deck1Rect.anchoredPosition = new Vector2(-435, -170);
                                commandBackgroundRect2.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect3.anchoredPosition = new Vector2(401, -146);
                                commandBackgroundRect1.anchoredPosition = new Vector2(731, -170);
                                break;
                            case 7:
                                Deck2Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck3Rect.anchoredPosition = new Vector2(-42, -153);
                                Deck1Rect.anchoredPosition = new Vector2(-421, -161);
                                commandBackgroundRect2.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect3.anchoredPosition = new Vector2(373, -153);
                                commandBackgroundRect1.anchoredPosition = new Vector2(718, -161);
                                break;
                            case 8:
                                Deck2Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck3Rect.anchoredPosition = new Vector2(-19, -161);
                                Deck1Rect.anchoredPosition = new Vector2(-398, -153);
                                commandBackgroundRect2.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect3.anchoredPosition = new Vector2(352, -161);
                                commandBackgroundRect1.anchoredPosition = new Vector2(697, -153);
                                break;
                            case 9:
                                Deck2Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck3Rect.anchoredPosition = new Vector2(-5, -170);
                                Deck1Rect.anchoredPosition = new Vector2(-367, -146);
                                commandBackgroundRect2.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect3.anchoredPosition = new Vector2(339, -170);
                                commandBackgroundRect1.anchoredPosition = new Vector2(669, -146);
                                break;
                            case 10:
                                Deck2Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck3Rect.anchoredPosition = new Vector2(0, -180);
                                Deck1Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect3.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else if (fightID == 1)//戦うポケモンを３から１へ変える
                {
                    darkImage[0].SetActive(false); darkImage[1].SetActive(true); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck3Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck1Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、上入力、状態２へ
                        {
                            case 1:
                                Deck3Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck1Rect.anchoredPosition = new Vector2(-288, -136);
                                Deck2Rect.anchoredPosition = new Vector2(-367, -214);
                                commandBackgroundRect3.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect1.anchoredPosition = new Vector2(597, -136);
                                commandBackgroundRect2.anchoredPosition = new Vector2(669, -214);
                                break;
                            case 2:
                                Deck3Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck1Rect.anchoredPosition = new Vector2(-243, -134);
                                Deck2Rect.anchoredPosition = new Vector2(-398, -207);
                                commandBackgroundRect3.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect1.anchoredPosition = new Vector2(556, -134);
                                commandBackgroundRect2.anchoredPosition = new Vector2(697, -207);
                                break;
                            case 3:
                                Deck3Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck1Rect.anchoredPosition = new Vector2(-197, -134);
                                Deck2Rect.anchoredPosition = new Vector2(-421, -199);
                                commandBackgroundRect3.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect1.anchoredPosition = new Vector2(514, -134);
                                commandBackgroundRect2.anchoredPosition = new Vector2(718, -199);
                                break;
                            case 4:
                                Deck3Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck1Rect.anchoredPosition = new Vector2(-152, -136);
                                Deck2Rect.anchoredPosition = new Vector2(-435, -190);
                                commandBackgroundRect3.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect1.anchoredPosition = new Vector2(473, -136);
                                commandBackgroundRect2.anchoredPosition = new Vector2(731, -190);
                                break;
                            case 5:
                                Deck3Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck1Rect.anchoredPosition = new Vector2(-110, -140);
                                Deck2Rect.anchoredPosition = new Vector2(-440, -180);
                                commandBackgroundRect3.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect1.anchoredPosition = new Vector2(435, -140);
                                commandBackgroundRect2.anchoredPosition = new Vector2(735, -180);
                                break;
                            case 6:
                                Deck3Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck1Rect.anchoredPosition = new Vector2(-73, -146);
                                Deck2Rect.anchoredPosition = new Vector2(-435, -170);
                                commandBackgroundRect3.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect1.anchoredPosition = new Vector2(401, -146);
                                commandBackgroundRect2.anchoredPosition = new Vector2(731, -170);
                                break;
                            case 7:
                                Deck3Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck1Rect.anchoredPosition = new Vector2(-42, -153);
                                Deck2Rect.anchoredPosition = new Vector2(-421, -161);
                                commandBackgroundRect3.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect1.anchoredPosition = new Vector2(373, -153);
                                commandBackgroundRect2.anchoredPosition = new Vector2(718, -161);
                                break;
                            case 8:
                                Deck3Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck1Rect.anchoredPosition = new Vector2(-19, -161);
                                Deck2Rect.anchoredPosition = new Vector2(-398, -153);
                                commandBackgroundRect3.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect1.anchoredPosition = new Vector2(352, -161);
                                commandBackgroundRect2.anchoredPosition = new Vector2(697, -153);
                                break;
                            case 9:
                                Deck3Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck1Rect.anchoredPosition = new Vector2(-5, -170);
                                Deck2Rect.anchoredPosition = new Vector2(-367, -146);
                                commandBackgroundRect3.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect1.anchoredPosition = new Vector2(339, -170);
                                commandBackgroundRect2.anchoredPosition = new Vector2(669, -146);
                                break;
                            case 10:
                                Deck3Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck1Rect.anchoredPosition = new Vector2(0, -180);
                                Deck2Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect3.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect1.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                break;
            default:
                break;
        }
        
    }

    IEnumerator MonsterChangeDown()
    {
        fm.entireCommandUseFour++;
        if (isGuard)
        {
            isGuard = false;
            guardImage[0].SetActive(false);
            guardImage[1].SetActive(false);
            guardImage[2].SetActive(false);
        }
        switch (fightID)//場に出ているモンスターを変える
        {
            case 1:
                switch (fm.NumOfParty)
                {
                    case 2://パーティが２体だったら
                        fightID = 2;
                        break;
                    case 3://パーティが３体だったら
                        fightID = 3;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (fm.NumOfParty)
                {
                    case 2:
                        fightID = 1;
                        break;
                    case 3:
                        fightID = 1;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                fightID = 2;
                break;
            default:
                break;
        }
        StartCoroutine("monsterGoBack");
        switch (fm.NumOfParty)
        {
            case 2:
                if (fightID == 2)//戦うポケモンを１から２へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(false); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck1Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck2Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//２体、上入力、状態２へ
                        {
                            case 1:
                                Deck1Rect.anchoredPosition = new Vector2(-8, -149);
                                Deck2Rect.anchoredPosition = new Vector2(-322, -211);
                                commandBackgroundRect1.anchoredPosition = new Vector2(342, -149);
                                commandBackgroundRect2.anchoredPosition = new Vector2(627, -211);
                                break;
                            case 2:
                                Deck1Rect.anchoredPosition = new Vector2(-32, -121);
                                Deck2Rect.anchoredPosition = new Vector2(-298, -239);
                                commandBackgroundRect1.anchoredPosition = new Vector2(363, -121);
                                commandBackgroundRect2.anchoredPosition = new Vector2(606, -239);
                                break;
                            case 3:
                                Deck1Rect.anchoredPosition = new Vector2(-68, -99);
                                Deck2Rect.anchoredPosition = new Vector2(-262, -261);
                                commandBackgroundRect1.anchoredPosition = new Vector2(396, -99);
                                commandBackgroundRect2.anchoredPosition = new Vector2(573, -261);
                                break;
                            case 4:
                                Deck1Rect.anchoredPosition = new Vector2(-114, -85);
                                Deck2Rect.anchoredPosition = new Vector2(-216, -275);
                                commandBackgroundRect1.anchoredPosition = new Vector2(438, -85);
                                commandBackgroundRect2.anchoredPosition = new Vector2(531, -275);
                                break;
                            case 5:
                                Deck1Rect.anchoredPosition = new Vector2(-165, -80);
                                Deck2Rect.anchoredPosition = new Vector2(-165, -280);
                                commandBackgroundRect1.anchoredPosition = new Vector2(485, -80);
                                commandBackgroundRect2.anchoredPosition = new Vector2(485, -280);
                                break;
                            case 6:
                                Deck1Rect.anchoredPosition = new Vector2(-216, -85);
                                Deck2Rect.anchoredPosition = new Vector2(-114, -275);
                                commandBackgroundRect1.anchoredPosition = new Vector2(531, -85);
                                commandBackgroundRect2.anchoredPosition = new Vector2(438, -275);
                                break;
                            case 7:
                                Deck1Rect.anchoredPosition = new Vector2(-262, -99);
                                Deck2Rect.anchoredPosition = new Vector2(-68, -261);
                                commandBackgroundRect1.anchoredPosition = new Vector2(573, -99);
                                commandBackgroundRect2.anchoredPosition = new Vector2(396, -261);
                                break;
                            case 8:
                                Deck1Rect.anchoredPosition = new Vector2(-298, -121);
                                Deck2Rect.anchoredPosition = new Vector2(-32, -239);
                                commandBackgroundRect1.anchoredPosition = new Vector2(606, -121);
                                commandBackgroundRect2.anchoredPosition = new Vector2(363, -239);
                                break;
                            case 9:
                                Deck1Rect.anchoredPosition = new Vector2(-322, -149);
                                Deck2Rect.anchoredPosition = new Vector2(-8, -211);
                                commandBackgroundRect1.anchoredPosition = new Vector2(627, -149);
                                commandBackgroundRect2.anchoredPosition = new Vector2(342, -211);
                                break;
                            case 10:
                                Deck1Rect.anchoredPosition = new Vector2(-330, -180);
                                Deck2Rect.anchoredPosition = new Vector2(-0, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(335, -180);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else
                {
                    darkImage[0].SetActive(false); darkImage[1].SetActive(true); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck2Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck1Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//２体、上入力、状態１へ
                        {
                            case 1:
                                Deck2Rect.anchoredPosition = new Vector2(-8, -149);
                                Deck1Rect.anchoredPosition = new Vector2(-322, -211);
                                commandBackgroundRect2.anchoredPosition = new Vector2(342, -149);
                                commandBackgroundRect1.anchoredPosition = new Vector2(627, -211);
                                break;
                            case 2:
                                Deck2Rect.anchoredPosition = new Vector2(-32, -121);
                                Deck1Rect.anchoredPosition = new Vector2(-298, -239);
                                commandBackgroundRect2.anchoredPosition = new Vector2(363, -121);
                                commandBackgroundRect1.anchoredPosition = new Vector2(606, -239);
                                break;
                            case 3:
                                Deck2Rect.anchoredPosition = new Vector2(-68, -99);
                                Deck1Rect.anchoredPosition = new Vector2(-262, -261);
                                commandBackgroundRect2.anchoredPosition = new Vector2(396, -99);
                                commandBackgroundRect1.anchoredPosition = new Vector2(573, -261);
                                break;
                            case 4:
                                Deck2Rect.anchoredPosition = new Vector2(-114, -85);
                                Deck1Rect.anchoredPosition = new Vector2(-216, -275);
                                commandBackgroundRect2.anchoredPosition = new Vector2(438, -85);
                                commandBackgroundRect1.anchoredPosition = new Vector2(531, -275);
                                break;
                            case 5:
                                Deck2Rect.anchoredPosition = new Vector2(-165, -80);
                                Deck1Rect.anchoredPosition = new Vector2(-165, -280);
                                commandBackgroundRect2.anchoredPosition = new Vector2(485, -80);
                                commandBackgroundRect1.anchoredPosition = new Vector2(485, -280);
                                break;
                            case 6:
                                Deck2Rect.anchoredPosition = new Vector2(-216, -85);
                                Deck1Rect.anchoredPosition = new Vector2(-114, -275);
                                commandBackgroundRect2.anchoredPosition = new Vector2(531, -85);
                                commandBackgroundRect1.anchoredPosition = new Vector2(438, -275);
                                break;
                            case 7:
                                Deck2Rect.anchoredPosition = new Vector2(-262, -99);
                                Deck1Rect.anchoredPosition = new Vector2(-68, -261);
                                commandBackgroundRect2.anchoredPosition = new Vector2(573, -99);
                                commandBackgroundRect1.anchoredPosition = new Vector2(396, -261);
                                break;
                            case 8:
                                Deck2Rect.anchoredPosition = new Vector2(-298, -121);
                                Deck1Rect.anchoredPosition = new Vector2(-32, -239);
                                commandBackgroundRect2.anchoredPosition = new Vector2(606, -121);
                                commandBackgroundRect1.anchoredPosition = new Vector2(363, -239);
                                break;
                            case 9:
                                Deck2Rect.anchoredPosition = new Vector2(-322, -149);
                                Deck1Rect.anchoredPosition = new Vector2(-8, -211);
                                commandBackgroundRect2.anchoredPosition = new Vector2(627, -149);
                                commandBackgroundRect1.anchoredPosition = new Vector2(342, -211);
                                break;
                            case 10:
                                Deck2Rect.anchoredPosition = new Vector2(-330, -180);
                                Deck1Rect.anchoredPosition = new Vector2(-0, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(335, -180);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                break;
            case 3:
                if (fightID == 2)//戦うポケモンを１から２へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(false); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck3Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck2Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、下入力、状態２へ
                        {
                            case 1:
                                Deck1Rect.anchoredPosition = new Vector2(-367, -146);
                                Deck2Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck3Rect.anchoredPosition = new Vector2(-5, -170);
                                commandBackgroundRect1.anchoredPosition = new Vector2(669, -146);
                                commandBackgroundRect2.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect3.anchoredPosition = new Vector2(339, -170);
                                break;
                            case 2:
                                Deck1Rect.anchoredPosition = new Vector2(-398, -153);
                                Deck2Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck3Rect.anchoredPosition = new Vector2(-19, -161);
                                commandBackgroundRect1.anchoredPosition = new Vector2(697, -153);
                                commandBackgroundRect2.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect3.anchoredPosition = new Vector2(352, -161);
                                break;
                            case 3:
                                Deck1Rect.anchoredPosition = new Vector2(-421, -161);
                                Deck2Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck3Rect.anchoredPosition = new Vector2(-42, -153);
                                commandBackgroundRect1.anchoredPosition = new Vector2(718, -161);
                                commandBackgroundRect2.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect3.anchoredPosition = new Vector2(373, -153);
                                break;
                            case 4:
                                Deck1Rect.anchoredPosition = new Vector2(-435, -170);
                                Deck2Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck3Rect.anchoredPosition = new Vector2(-73, -146);
                                commandBackgroundRect1.anchoredPosition = new Vector2(731, -170);
                                commandBackgroundRect2.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect3.anchoredPosition = new Vector2(401, -146);
                                break;
                            case 5:
                                Deck1Rect.anchoredPosition = new Vector2(-440, -180);
                                Deck2Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck3Rect.anchoredPosition = new Vector2(-110, -140);
                                commandBackgroundRect1.anchoredPosition = new Vector2(735, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect3.anchoredPosition = new Vector2(435, -140);
                                break;
                            case 6:
                                Deck1Rect.anchoredPosition = new Vector2(-435, -190);
                                Deck2Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck3Rect.anchoredPosition = new Vector2(-152, -136);
                                commandBackgroundRect1.anchoredPosition = new Vector2(731, -190);
                                commandBackgroundRect2.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect3.anchoredPosition = new Vector2(473, -136);
                                break;
                            case 7:
                                Deck1Rect.anchoredPosition = new Vector2(-421, -199);
                                Deck2Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck3Rect.anchoredPosition = new Vector2(-197, -134);
                                commandBackgroundRect1.anchoredPosition = new Vector2(718, -199);
                                commandBackgroundRect2.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect3.anchoredPosition = new Vector2(514, -134);
                                break;
                            case 8:
                                Deck1Rect.anchoredPosition = new Vector2(-398, -207);
                                Deck2Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck3Rect.anchoredPosition = new Vector2(-243, -134);
                                commandBackgroundRect1.anchoredPosition = new Vector2(697, -207);
                                commandBackgroundRect2.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect3.anchoredPosition = new Vector2(556, -134);
                                break;
                            case 9:
                                Deck1Rect.anchoredPosition = new Vector2(-367, -214);
                                Deck2Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck3Rect.anchoredPosition = new Vector2(-288, -136);
                                commandBackgroundRect1.anchoredPosition = new Vector2(669, -214);
                                commandBackgroundRect2.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect3.anchoredPosition = new Vector2(597, -136);
                                break;
                            case 10:
                                Deck1Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck2Rect.anchoredPosition = new Vector2(0, -180);
                                Deck3Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect2.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect3.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else if (fightID == 3)//戦うポケモンを１から３へ変える
                {
                    darkImage[0].SetActive(true); darkImage[1].SetActive(true); darkImage[2].SetActive(false);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck1Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck3Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、上入力、状態２へ
                        {
                            case 1:
                                Deck2Rect.anchoredPosition = new Vector2(-367, -146);
                                Deck3Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck1Rect.anchoredPosition = new Vector2(-5, -170);
                                commandBackgroundRect2.anchoredPosition = new Vector2(669, -146);
                                commandBackgroundRect3.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect1.anchoredPosition = new Vector2(339, -170);
                                break;
                            case 2:
                                Deck2Rect.anchoredPosition = new Vector2(-398, -153);
                                Deck3Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck1Rect.anchoredPosition = new Vector2(-19, -161);
                                commandBackgroundRect2.anchoredPosition = new Vector2(697, -153);
                                commandBackgroundRect3.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect1.anchoredPosition = new Vector2(352, -161);
                                break;
                            case 3:
                                Deck2Rect.anchoredPosition = new Vector2(-421, -161);
                                Deck3Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck1Rect.anchoredPosition = new Vector2(-42, -153);
                                commandBackgroundRect2.anchoredPosition = new Vector2(718, -161);
                                commandBackgroundRect3.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect1.anchoredPosition = new Vector2(373, -153);
                                break;
                            case 4:
                                Deck2Rect.anchoredPosition = new Vector2(-435, -170);
                                Deck3Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck1Rect.anchoredPosition = new Vector2(-73, -146);
                                commandBackgroundRect2.anchoredPosition = new Vector2(731, -170);
                                commandBackgroundRect3.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect1.anchoredPosition = new Vector2(401, -146);
                                break;
                            case 5:
                                Deck2Rect.anchoredPosition = new Vector2(-440, -180);
                                Deck3Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck1Rect.anchoredPosition = new Vector2(-110, -140);
                                commandBackgroundRect2.anchoredPosition = new Vector2(735, -180);
                                commandBackgroundRect3.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect1.anchoredPosition = new Vector2(435, -140);
                                break;
                            case 6:
                                Deck2Rect.anchoredPosition = new Vector2(-435, -190);
                                Deck3Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck1Rect.anchoredPosition = new Vector2(-152, -136);
                                commandBackgroundRect2.anchoredPosition = new Vector2(731, -190);
                                commandBackgroundRect3.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect1.anchoredPosition = new Vector2(473, -136);
                                break;
                            case 7:
                                Deck2Rect.anchoredPosition = new Vector2(-421, -199);
                                Deck3Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck1Rect.anchoredPosition = new Vector2(-197, -134);
                                commandBackgroundRect2.anchoredPosition = new Vector2(718, -199);
                                commandBackgroundRect3.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect1.anchoredPosition = new Vector2(514, -134);
                                break;
                            case 8:
                                Deck2Rect.anchoredPosition = new Vector2(-398, -207);
                                Deck3Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck1Rect.anchoredPosition = new Vector2(-243, -134);
                                commandBackgroundRect2.anchoredPosition = new Vector2(697, -207);
                                commandBackgroundRect3.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect1.anchoredPosition = new Vector2(556, -134);
                                break;
                            case 9:
                                Deck2Rect.anchoredPosition = new Vector2(-367, -214);
                                Deck3Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck1Rect.anchoredPosition = new Vector2(-288, -136);
                                commandBackgroundRect2.anchoredPosition = new Vector2(669, -214);
                                commandBackgroundRect3.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect1.anchoredPosition = new Vector2(597, -136);
                                break;
                            case 10:
                                Deck2Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck3Rect.anchoredPosition = new Vector2(0, -180);
                                Deck1Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect3.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                else if (fightID == 1)//戦うポケモンを３から１へ変える
                {
                    darkImage[0].SetActive(false); darkImage[1].SetActive(true); darkImage[2].SetActive(true);
                    for (int i = 1; i < 11; i++)
                    {
                        Deck2Rect.localScale = new Vector3(1f - 0.03f * i, 1f - 0.03f * i, 1);
                        Deck1Rect.localScale = new Vector3(0.7f + 0.03f * i, 0.7f + 0.03f * i, 1);
                        switch (i)//３体、上入力、状態２へ
                        {
                            case 1:
                                Deck3Rect.anchoredPosition = new Vector2(-367, -146);
                                Deck1Rect.anchoredPosition = new Vector2(-288, -224);
                                Deck2Rect.anchoredPosition = new Vector2(-5, -170);
                                commandBackgroundRect3.anchoredPosition = new Vector2(669, -146);
                                commandBackgroundRect1.anchoredPosition = new Vector2(597, -224);
                                commandBackgroundRect2.anchoredPosition = new Vector2(339, -170);
                                break;
                            case 2:
                                Deck3Rect.anchoredPosition = new Vector2(-398, -153);
                                Deck1Rect.anchoredPosition = new Vector2(-243, -226);
                                Deck2Rect.anchoredPosition = new Vector2(-19, -161);
                                commandBackgroundRect3.anchoredPosition = new Vector2(697, -153);
                                commandBackgroundRect1.anchoredPosition = new Vector2(556, -226);
                                commandBackgroundRect2.anchoredPosition = new Vector2(352, -161);
                                break;
                            case 3:
                                Deck3Rect.anchoredPosition = new Vector2(-421, -161);
                                Deck1Rect.anchoredPosition = new Vector2(-197, -226);
                                Deck2Rect.anchoredPosition = new Vector2(-42, -153);
                                commandBackgroundRect3.anchoredPosition = new Vector2(718, -161);
                                commandBackgroundRect1.anchoredPosition = new Vector2(514, -226);
                                commandBackgroundRect2.anchoredPosition = new Vector2(373, -153);
                                break;
                            case 4:
                                Deck3Rect.anchoredPosition = new Vector2(-435, -170);
                                Deck1Rect.anchoredPosition = new Vector2(-152, -224);
                                Deck2Rect.anchoredPosition = new Vector2(-73, -146);
                                commandBackgroundRect3.anchoredPosition = new Vector2(731, -170);
                                commandBackgroundRect1.anchoredPosition = new Vector2(473, -224);
                                commandBackgroundRect2.anchoredPosition = new Vector2(401, -146);
                                break;
                            case 5:
                                Deck3Rect.anchoredPosition = new Vector2(-440, -180);
                                Deck1Rect.anchoredPosition = new Vector2(-110, -220);
                                Deck2Rect.anchoredPosition = new Vector2(-110, -140);
                                commandBackgroundRect3.anchoredPosition = new Vector2(735, -180);
                                commandBackgroundRect1.anchoredPosition = new Vector2(435, -220);
                                commandBackgroundRect2.anchoredPosition = new Vector2(435, -140);
                                break;
                            case 6:
                                Deck3Rect.anchoredPosition = new Vector2(-435, -190);
                                Deck1Rect.anchoredPosition = new Vector2(-73, -214);
                                Deck2Rect.anchoredPosition = new Vector2(-152, -136);
                                commandBackgroundRect3.anchoredPosition = new Vector2(731, -190);
                                commandBackgroundRect1.anchoredPosition = new Vector2(401, -214);
                                commandBackgroundRect2.anchoredPosition = new Vector2(473, -136);
                                break;
                            case 7:
                                Deck3Rect.anchoredPosition = new Vector2(-421, -199);
                                Deck1Rect.anchoredPosition = new Vector2(-42, -207);
                                Deck2Rect.anchoredPosition = new Vector2(-197, -134);
                                commandBackgroundRect3.anchoredPosition = new Vector2(718, -199);
                                commandBackgroundRect1.anchoredPosition = new Vector2(373, -207);
                                commandBackgroundRect2.anchoredPosition = new Vector2(514, -134);
                                break;
                            case 8:
                                Deck3Rect.anchoredPosition = new Vector2(-398, -207);
                                Deck1Rect.anchoredPosition = new Vector2(-19, -199);
                                Deck2Rect.anchoredPosition = new Vector2(-243, -134);
                                commandBackgroundRect3.anchoredPosition = new Vector2(697, -207);
                                commandBackgroundRect1.anchoredPosition = new Vector2(352, -199);
                                commandBackgroundRect2.anchoredPosition = new Vector2(556, -134);
                                break;
                            case 9:
                                Deck3Rect.anchoredPosition = new Vector2(-367, -214);
                                Deck1Rect.anchoredPosition = new Vector2(-5, -190);
                                Deck2Rect.anchoredPosition = new Vector2(-288, -136);
                                commandBackgroundRect3.anchoredPosition = new Vector2(669, -214);
                                commandBackgroundRect1.anchoredPosition = new Vector2(339, -190);
                                commandBackgroundRect2.anchoredPosition = new Vector2(597, -136);
                                break;
                            case 10:
                                Deck3Rect.anchoredPosition = new Vector2(-330, -220);
                                Deck1Rect.anchoredPosition = new Vector2(0, -180);
                                Deck2Rect.anchoredPosition = new Vector2(-330, -140);
                                commandBackgroundRect3.anchoredPosition = new Vector2(635, -220);
                                commandBackgroundRect1.anchoredPosition = new Vector2(335, -180);
                                commandBackgroundRect2.anchoredPosition = new Vector2(635, -140);
                                break;
                            default:
                                break;
                        }
                        yield return new WaitForSeconds(0.03f);
                    }
                }
                break;
            default:
                break;
        }

    }

    IEnumerator monsterGoBack()
    {
        switch (fightID)
        {
            case 1:
                audioSource.PlayOneShot(SEChange);
                //プレイヤーを前に出す
                playerObj.transform.DOLocalMove(new Vector3(-150, 50, 0f), 0.2f);
                monsterRect.transform.DOLocalMove(positionBackDO, 0.2f);
                monster2Rect.transform.DOLocalMove(positionBackDO, 0.2f);
                break;
            case 2:
                audioSource.PlayOneShot(SEChange);
                monsterRect.transform.DOLocalMove(positionShisaDO, 0.2f);
                //プレイヤーを下げる
                playerObj.transform.DOLocalMove(positionBackDO, 0.2f);
                monster2Rect.transform.DOLocalMove(positionBackDO, 0.2f);
                break;
            case 3:
                playerObj.transform.DOLocalMove(positionBackDO, 0.2f);
                monsterRect.transform.DOLocalMove(positionBackDO, 0.2f);
                monster2Rect.transform.DOLocalMove(positionShisaDO, 0.2f);
                audioSource.PlayOneShot(SEChange);
                break;
            default:
                break;
        }
        yield return null;
    }

    public void playerHPUpdate()
    {
        HPSlider.value = HP / MaxHP;
        textPlayerHP.text = "HP " + HP + "/" + MaxHP;
        HP2Slider.value = HP2 / MaxHP2;
        textPlayerHP2.text = "HP " + HP2 + "/" + MaxHP2;
        HP3Slider.value = HP3 / MaxHP3;
        textPlayerHP3.text = "HP " + HP3 + "/" + MaxHP3;
    }

    IEnumerator BattleResult()
    {
        //説明テキストを変更する
        explainTextBattle.text = "【Z】次へ";
        //+EXPのテキストを変更する
        float randomRange1 = Random.Range(0.9f, 1.3f);
        float randomRange2 = Random.Range(0.9f, 1.3f);
        float randomRange3 = Random.Range(0.9f, 1.3f);
        int EXP = enemyScript.enemyEXP;
        int item = enemyScript.enemyItem;
        int MONEY = enemyScript.enemyMoney;
        resultRewardText[0].text = "EXP +" + (int)(EXP * randomRange1);
        resultRewardText[1].text = "EXP +" + (int)(EXP * randomRange2);
        resultRewardText[2].text = "EXP +" + (int)(EXP * randomRange3);

        if (enemyScript.isDragon)
        {
            if (!fm.DoYouHaveDragon)
            {
                resultRewardText[1].text = "仲間になった";
                fm.NumOfParty++;
                fm.MonsterID2 = 4;
                fm.ATK2 = 100;
                fm.SPD2 = 100;
            }
            fm.DoYouHaveDragon = true;
        }
        if (enemyScript.isShisa)
        {
            if (!fm.DoYouHaveShisa)
            {
                resultRewardText[2].text = "仲間になった";
                fm.NumOfParty++;
                fm.MonsterID2 = 4;
                fm.ATK2 = 100;
                fm.SPD2 = 100;
            }
            fm.DoYouHaveShisa = true;
        }
        if (enemyScript.isReviathan)
        {
            if (!fm.DoYouHaveReviathan)
            {
                resultRewardText[3].text = "仲間になった";
                fm.NumOfParty++;
                fm.MonsterID3 = 6;
                fm.ATK3 = 100;
                fm.SPD3 = 100;
            }
            fm.DoYouHaveReviathan = true;
        }
        yield return new WaitForSeconds(0.5f);
        fm.EXP0 += (int)(EXP * randomRange1);
        fm.EXP1 += (int)(EXP * randomRange2);
        fm.EXP2 += (int)(EXP * randomRange3);
        if (fm.NumOfParty == 4)
        {
            fm.EXP2 += (int)(EXP * randomRange3);
            fm.EXP3 += (int)(EXP * randomRange3);
        }
        float rnd = Random.Range(1f,2.0f);
        switch (enemyScript.enemyID)
        {
            case 1://スライム
                fm.LvOfForest += (int)Mathf.Round(item * rnd);
                textTraceGainImage.text = "魔獣の牙 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = tuskImage;
                break;
            case 2://ビーバー
                fm.LvOfForest += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "魔獣の牙 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = tuskImage;
                break;
            case 3://侍ドッグ
                fm.LvOfForest += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "魔獣の牙 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = tuskImage;
                break;
            case 4://サメ
                fm.LvOfSea += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "海獣の鱗 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = seaImage;
                break;
            case 5://ヒトガタ
                fm.LvOfSea += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "海獣の鱗 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = seaImage;
                break;
            case 6://リヴァイアサン
                fm.LvOfSea += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "海獣の鱗 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = seaImage;
                break;
            case 7://パンダ
                fm.LvOfForest += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "魔獣の牙 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = tuskImage;
                break;
            case 8://クロウ
                fm.LvOfCity += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "貴金属 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = AbilityImage;
                break;
            case 9://スライムグランデ
                fm.LvOfCity += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "貴金属 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = AbilityImage;
                break;
            case 10://眠れる竜の魂
                fm.LvOfCity += (int)Mathf.Round(20 * rnd);
                textTraceGainImage.text = "貴金属 ×" + (int)Mathf.Round(item * rnd);
                ItemImage.sprite = AbilityImage;
                break;
            default:
                break;
        }
        //金を増やす
        fm.money += (int)(MONEY * rnd);
        textMoneyGain.text = "＋ " + (int)(MONEY * rnd);
        audioSourceBGM.Stop();
        audioSource.PlayOneShot(SEresult);
        for (int i = 1; i < 51; i++)
        {
            resultBoardRec.anchoredPosition = new Vector2(0, 540 - (10.8f * i));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("ResultRewardFadeIn");
    }

    IEnumerator ResultRewardFadeIn()//0.3秒でフェードアウトして0.3秒でフェードインする
    {
        fm.entireDefeatNum++;

        switch (fm.NumOfParty)
        {
            case 1:
                resultTextRec.anchoredPosition = new Vector2(0, 120);
                for (int i = 1; i < 25; i++)
                {
                    switch (i)
                    {
                        case 1:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT.anchoredPosition = new Vector2(25, -70);
                            resultEXPImageCanvasGroup.alpha = 0.5f;
                            break;
                        case 2:
                            resultEXPImageRT.anchoredPosition = new Vector2(15, -70);
                            resultEXPImageCanvasGroup.alpha = 0.8f;
                            break;
                        case 3:
                            resultEXPImageRT.anchoredPosition = new Vector2(10, -70);
                            resultEXPImageCanvasGroup.alpha = 0.9f;
                            break;
                        case 4:
                            resultEXPImageRT.anchoredPosition = new Vector2(5, -70);
                            resultEXPImageCanvasGroup.alpha = 0.95f;
                            break;
                        case 5:
                            resultEXPImageRT.anchoredPosition = new Vector2(2, -70);
                            resultEXPImageCanvasGroup.alpha = 0.98f;
                            break;
                        case 6:
                            resultEXPImageRT.anchoredPosition = new Vector2(0, -70);
                            resultEXPImageCanvasGroup.alpha = 1f;
                            break;
                        case 7:
                            audioSource.PlayOneShot(SEGetEXP);
                            textMoneyGainRect.anchoredPosition = new Vector2(25, -150);
                            textMoneyGainGroup.alpha = 0.5f;
                            break;
                        case 8:
                            textMoneyGainRect.anchoredPosition = new Vector2(15, -150);
                            textMoneyGainGroup.alpha = 0.8f;
                            break;
                        case 9:
                            textMoneyGainRect.anchoredPosition = new Vector2(10, -150);
                            textMoneyGainGroup.alpha = 0.9f;
                            break;
                        case 10:
                            textMoneyGainRect.anchoredPosition = new Vector2(5, -150);
                            textMoneyGainGroup.alpha = 0.95f;
                            break;
                        case 11:
                            textMoneyGainRect.anchoredPosition = new Vector2(2, -150);
                            textMoneyGainGroup.alpha = 0.98f;
                            break;
                        case 12:
                            textMoneyGainRect.anchoredPosition = new Vector2(0, -150);
                            textMoneyGainGroup.alpha = 1f;
                            break;
                        case 13:
                            audioSource.PlayOneShot(SEGetEXP);
                            textTraceGainRect.anchoredPosition = new Vector2(25, -230);
                            textTraceGainGroup.alpha = 0.5f;
                            break;
                        case 14:
                            textTraceGainRect.anchoredPosition = new Vector2(15, -230);
                            textTraceGainGroup.alpha = 0.8f;
                            break;
                        case 15:
                            textTraceGainRect.anchoredPosition = new Vector2(10, -230);
                            textTraceGainGroup.alpha = 0.9f;
                            break;
                        case 16:
                            textTraceGainRect.anchoredPosition = new Vector2(5, -230);
                            textTraceGainGroup.alpha = 0.95f;
                            break;
                        case 17:
                            textTraceGainRect.anchoredPosition = new Vector2(2, -230);
                            textTraceGainGroup.alpha = 0.98f;
                            break;
                        case 18:
                            textTraceGainRect.anchoredPosition = new Vector2(0, -230);
                            textTraceGainGroup.alpha = 1f;
                            break;
                        default:
                            break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            case 2:
                resultTextRec.anchoredPosition = new Vector2(0, 120);
                for (int i = 1; i < 25; i++)
                {
                    switch (i)
                    {
                        case 1:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT.anchoredPosition = new Vector2(25, -70);
                            resultEXPImageCanvasGroup.alpha = 0.5f;
                            break;
                        case 2:
                            resultEXPImageRT.anchoredPosition = new Vector2(15, -70);
                            resultEXPImageCanvasGroup.alpha = 0.8f;
                            break;
                        case 3:
                            resultEXPImageRT.anchoredPosition = new Vector2(10, -70);
                            resultEXPImageCanvasGroup.alpha = 0.9f;
                            break;
                        case 4:
                            resultEXPImageRT.anchoredPosition = new Vector2(5, -70);
                            resultEXPImageCanvasGroup.alpha = 0.95f;
                            break;
                        case 5:
                            resultEXPImageRT.anchoredPosition = new Vector2(2, -70);
                            resultEXPImageCanvasGroup.alpha = 0.98f;
                            break;
                        case 6:
                            resultEXPImageRT.anchoredPosition = new Vector2(0, -70);
                            resultEXPImageCanvasGroup.alpha = 1f;
                            break;
                        case 7:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT2.anchoredPosition = new Vector2(25, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.5f;
                            break;
                        case 8:
                            resultEXPImageRT2.anchoredPosition = new Vector2(15, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.8f;
                            break;
                        case 9:
                            resultEXPImageRT2.anchoredPosition = new Vector2(10, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.9f;
                            break;
                        case 10:
                            resultEXPImageRT2.anchoredPosition = new Vector2(5, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.95f;
                            break;
                        case 11:
                            resultEXPImageRT2.anchoredPosition = new Vector2(2, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.98f;
                            break;
                        case 12:
                            resultEXPImageRT2.anchoredPosition = new Vector2(0, -150);
                            resultEXPImageCanvasGroup2.alpha = 1f;
                            break;
                        case 13:
                            audioSource.PlayOneShot(SEGetEXP);
                            textMoneyGainRect.anchoredPosition = new Vector2(25, -230);
                            textMoneyGainGroup.alpha = 0.5f;
                            break;
                        case 14:
                            textMoneyGainRect.anchoredPosition = new Vector2(15, -230);
                            textMoneyGainGroup.alpha = 0.8f;
                            break;
                        case 15:
                            textMoneyGainRect.anchoredPosition = new Vector2(10, -230);
                            textMoneyGainGroup.alpha = 0.9f;
                            break;
                        case 16:
                            textMoneyGainRect.anchoredPosition = new Vector2(5, -230);
                            textMoneyGainGroup.alpha = 0.95f;
                            break;
                        case 17:
                            textMoneyGainRect.anchoredPosition = new Vector2(2, -230);
                            textMoneyGainGroup.alpha = 0.98f;
                            break;
                        case 18:
                            textMoneyGainRect.anchoredPosition = new Vector2(0, -230);
                            textMoneyGainGroup.alpha = 1f;
                            break;
                        case 19:
                            audioSource.PlayOneShot(SEGetEXP);
                            textTraceGainRect.anchoredPosition = new Vector2(25, -290);
                            textTraceGainGroup.alpha = 0.5f;
                            break;
                        case 20:
                            textTraceGainRect.anchoredPosition = new Vector2(15, -290);
                            textTraceGainGroup.alpha = 0.8f;
                            break;
                        case 21:
                            textTraceGainRect.anchoredPosition = new Vector2(10, -290);
                            textTraceGainGroup.alpha = 0.9f;
                            break;
                        case 22:
                            textTraceGainRect.anchoredPosition = new Vector2(5, -290);
                            textTraceGainGroup.alpha = 0.95f;
                            break;
                        case 23:
                            textTraceGainRect.anchoredPosition = new Vector2(2, -290);
                            textTraceGainGroup.alpha = 0.98f;
                            break;
                        case 24:
                            textTraceGainRect.anchoredPosition = new Vector2(0, -290);
                            textTraceGainGroup.alpha = 1f;
                            break;
                        default:
                            break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            case 3:
                resultTextRec.anchoredPosition = new Vector2(0, 120);
                for (int i = 1; i < 31; i++)
                {
                    switch (i)
                    {
                        case 1:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT.anchoredPosition = new Vector2(25, -70);
                            resultEXPImageCanvasGroup.alpha = 0.5f;
                            break;
                        case 2:
                            resultEXPImageRT.anchoredPosition = new Vector2(15, -70);
                            resultEXPImageCanvasGroup.alpha = 0.8f;
                            break;
                        case 3:
                            resultEXPImageRT.anchoredPosition = new Vector2(10, -70);
                            resultEXPImageCanvasGroup.alpha = 0.9f;
                            break;
                        case 4:
                            resultEXPImageRT.anchoredPosition = new Vector2(5, -70);
                            resultEXPImageCanvasGroup.alpha = 0.95f;
                            break;
                        case 5:
                            resultEXPImageRT.anchoredPosition = new Vector2(2, -70);
                            resultEXPImageCanvasGroup.alpha = 0.98f;
                            break;
                        case 6:
                            resultEXPImageRT.anchoredPosition = new Vector2(0, -70);
                            resultEXPImageCanvasGroup.alpha = 1f;
                            break;
                        case 7:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT2.anchoredPosition = new Vector2(25, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.5f;
                            break;
                        case 8:
                            resultEXPImageRT2.anchoredPosition = new Vector2(15, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.8f;
                            break;
                        case 9:
                            resultEXPImageRT2.anchoredPosition = new Vector2(10, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.9f;
                            break;
                        case 10:
                            resultEXPImageRT2.anchoredPosition = new Vector2(5, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.95f;
                            break;
                        case 11:
                            resultEXPImageRT2.anchoredPosition = new Vector2(2, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.98f;
                            break;
                        case 12:
                            resultEXPImageRT2.anchoredPosition = new Vector2(0, -150);
                            resultEXPImageCanvasGroup2.alpha = 1f;
                            break;
                        case 13:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT3.anchoredPosition = new Vector2(25, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.5f;
                            break;
                        case 14:
                            resultEXPImageRT3.anchoredPosition = new Vector2(15, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.8f;
                            break;
                        case 15:
                            resultEXPImageRT3.anchoredPosition = new Vector2(10, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.9f;
                            break;
                        case 16:
                            resultEXPImageRT3.anchoredPosition = new Vector2(5, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.95f;
                            break;
                        case 17:
                            resultEXPImageRT3.anchoredPosition = new Vector2(2, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.98f;
                            break;
                        case 18:
                            resultEXPImageRT3.anchoredPosition = new Vector2(0, -230);
                            resultEXPImageCanvasGroup3.alpha = 1f;
                            break;
                        case 19:
                            audioSource.PlayOneShot(SEGetEXP);
                            textMoneyGainRect.anchoredPosition = new Vector2(25, -310);
                            textMoneyGainGroup.alpha = 0.5f;
                            break;
                        case 20:
                            textMoneyGainRect.anchoredPosition = new Vector2(15, -310);
                            textMoneyGainGroup.alpha = 0.8f;
                            break;
                        case 21:
                            textMoneyGainRect.anchoredPosition = new Vector2(10, -310);
                            textMoneyGainGroup.alpha = 0.9f;
                            break;
                        case 22:
                            textMoneyGainRect.anchoredPosition = new Vector2(5, -310);
                            textMoneyGainGroup.alpha = 0.95f;
                            break;
                        case 23:
                            textMoneyGainRect.anchoredPosition = new Vector2(2, -310);
                            textMoneyGainGroup.alpha = 0.98f;
                            break;
                        case 24:
                            textMoneyGainRect.anchoredPosition = new Vector2(0, -310);
                            textMoneyGainGroup.alpha = 1f;
                            break;
                        case 25:
                            resultTextRec.anchoredPosition = new Vector2(0, 200);
                            audioSource.PlayOneShot(SEGetEXP);
                            textTraceGainRect.anchoredPosition = new Vector2(25, -370);
                            textTraceGainGroup.alpha = 0.5f;
                            break;
                        case 26:
                            textTraceGainRect.anchoredPosition = new Vector2(15, -370);
                            textTraceGainGroup.alpha = 0.8f;
                            break;
                        case 27:
                            textTraceGainRect.anchoredPosition = new Vector2(10, -370);
                            textTraceGainGroup.alpha = 0.9f;
                            break;
                        case 28:
                            textTraceGainRect.anchoredPosition = new Vector2(5, -370);
                            textTraceGainGroup.alpha = 0.95f;
                            break;
                        case 29:
                            textTraceGainRect.anchoredPosition = new Vector2(2, -370);
                            textTraceGainGroup.alpha = 0.98f;
                            break;
                        case 30:
                            textTraceGainRect.anchoredPosition = new Vector2(0, -370);
                            textTraceGainGroup.alpha = 1f;
                            break;
                        default:
                            break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            case 4:
                resultTextRec.anchoredPosition = new Vector2(0, 120);
                for (int i = 1; i < 37; i++)
                {
                    switch (i)
                    {
                        case 1:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT.anchoredPosition = new Vector2(25, -70);
                            resultEXPImageCanvasGroup.alpha = 0.5f;
                            break;
                        case 2:
                            resultEXPImageRT.anchoredPosition = new Vector2(15, -70);
                            resultEXPImageCanvasGroup.alpha = 0.8f;
                            break;
                        case 3:
                            resultEXPImageRT.anchoredPosition = new Vector2(10, -70);
                            resultEXPImageCanvasGroup.alpha = 0.9f;
                            break;
                        case 4:
                            resultEXPImageRT.anchoredPosition = new Vector2(5, -70);
                            resultEXPImageCanvasGroup.alpha = 0.95f;
                            break;
                        case 5:
                            resultEXPImageRT.anchoredPosition = new Vector2(2, -70);
                            resultEXPImageCanvasGroup.alpha = 0.98f;
                            break;
                        case 6:
                            resultEXPImageRT.anchoredPosition = new Vector2(0, -70);
                            resultEXPImageCanvasGroup.alpha = 1f;
                            break;
                        case 7:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT2.anchoredPosition = new Vector2(25, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.5f;
                            break;
                        case 8:
                            resultEXPImageRT2.anchoredPosition = new Vector2(15, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.8f;
                            break;
                        case 9:
                            resultEXPImageRT2.anchoredPosition = new Vector2(10, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.9f;
                            break;
                        case 10:
                            resultEXPImageRT2.anchoredPosition = new Vector2(5, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.95f;
                            break;
                        case 11:
                            resultEXPImageRT2.anchoredPosition = new Vector2(2, -150);
                            resultEXPImageCanvasGroup2.alpha = 0.98f;
                            break;
                        case 12:
                            resultEXPImageRT2.anchoredPosition = new Vector2(0, -150);
                            resultEXPImageCanvasGroup2.alpha = 1f;
                            break;
                        case 13:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT3.anchoredPosition = new Vector2(25, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.5f;
                            break;
                        case 14:
                            resultEXPImageRT3.anchoredPosition = new Vector2(15, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.8f;
                            break;
                        case 15:
                            resultEXPImageRT3.anchoredPosition = new Vector2(10, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.9f;
                            break;
                        case 16:
                            resultEXPImageRT3.anchoredPosition = new Vector2(5, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.95f;
                            break;
                        case 17:
                            resultEXPImageRT3.anchoredPosition = new Vector2(2, -230);
                            resultEXPImageCanvasGroup3.alpha = 0.98f;
                            break;
                        case 18:
                            resultEXPImageRT3.anchoredPosition = new Vector2(0, -230);
                            resultEXPImageCanvasGroup3.alpha = 1f;
                            break;
                        case 19:
                            audioSource.PlayOneShot(SEGetEXP);
                            resultEXPImageRT4.anchoredPosition = new Vector2(25, -310);
                            resultEXPImageCanvasGroup4.alpha = 0.5f;
                            break;
                        case 20:
                            resultEXPImageRT4.anchoredPosition = new Vector2(15, -310);
                            resultEXPImageCanvasGroup4.alpha = 0.8f;
                            break;
                        case 21:
                            resultEXPImageRT4.anchoredPosition = new Vector2(10, -310);
                            resultEXPImageCanvasGroup4.alpha = 0.9f;
                            break;
                        case 22:
                            resultEXPImageRT4.anchoredPosition = new Vector2(5, -310);
                            resultEXPImageCanvasGroup4.alpha = 0.95f;
                            break;
                        case 23:
                            resultEXPImageRT4.anchoredPosition = new Vector2(2, -310);
                            resultEXPImageCanvasGroup4.alpha = 0.98f;
                            break;
                        case 24:
                            resultEXPImageRT4.anchoredPosition = new Vector2(0, -310);
                            resultEXPImageCanvasGroup4.alpha = 1f;
                            break;
                        case 25:
                            resultTextRec.anchoredPosition = new Vector2(0, 200);
                            audioSource.PlayOneShot(SEGetEXP);
                            textMoneyGainRect.anchoredPosition = new Vector2(25, -390);
                            textMoneyGainGroup.alpha = 0.5f;
                            break;
                        case 26:
                            textMoneyGainRect.anchoredPosition = new Vector2(15, -390);
                            textMoneyGainGroup.alpha = 0.8f;
                            break;
                        case 27:
                            textMoneyGainRect.anchoredPosition = new Vector2(10, -390);
                            textMoneyGainGroup.alpha = 0.9f;
                            break;
                        case 28:
                            textMoneyGainRect.anchoredPosition = new Vector2(5, -390);
                            textMoneyGainGroup.alpha = 0.95f;
                            break;
                        case 29:
                            textMoneyGainRect.anchoredPosition = new Vector2(2, -390);
                            textMoneyGainGroup.alpha = 0.98f;
                            break;
                        case 30:
                            textMoneyGainRect.anchoredPosition = new Vector2(0, -390);
                            textMoneyGainGroup.alpha = 1f;
                            break;
                        case 31:
                            resultTextRec.anchoredPosition = new Vector2(0, 280);
                            audioSource.PlayOneShot(SEGetEXP);
                            textTraceGainRect.anchoredPosition = new Vector2(25, -450);
                            textTraceGainGroup.alpha = 0.5f;
                            break;
                        case 32:
                            textTraceGainRect.anchoredPosition = new Vector2(15, -450);
                            textTraceGainGroup.alpha = 0.8f;
                            break;
                        case 33:
                            textTraceGainRect.anchoredPosition = new Vector2(10, -450);
                            textTraceGainGroup.alpha = 0.9f;
                            break;
                        case 34:
                            textTraceGainRect.anchoredPosition = new Vector2(5, -450);
                            textTraceGainGroup.alpha = 0.95f;
                            break;
                        case 35:
                            textTraceGainRect.anchoredPosition = new Vector2(2, -450);
                            textTraceGainGroup.alpha = 0.98f;
                            break;
                        case 36:
                            textTraceGainRect.anchoredPosition = new Vector2(0, -450);
                            textTraceGainGroup.alpha = 1f;
                            break;
                        default:
                            break;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;
            default:
                break;
        }
        isResultComplete = true;
    }

    public void OnResultNextButtonClick()
    {
        fm.isBattle = false;
        sm.isStageScene = true;
        fm.StatusTextUpdate_Field();

        if (enemyScript.enemyID == 0)
        {
            eventNumBattle = 2;//2番のイベントを始める
            eTM.eventNum = eventNumBattle;
            eTM.StartCoroutine("eventBegin");
            fm.isBossDefeated[0] = true;
        }
        else if (enemyScript.enemyID == 7)
        {
            eventNumBattle = 4;
            eTM.eventNum = eventNumBattle;
            eTM.StartCoroutine("eventBegin");
            fm.isBossDefeated[1] = true;
        }
        else if (enemyScript.enemyID == 2)
        {
            eventNumBattle = 7;
            eTM.eventNum = eventNumBattle;
            eTM.StartCoroutine("eventBegin");
            fm.isBossDefeated[2] = true;
        }
        else if (enemyScript.enemyID == 6)
        {
            eventNumBattle = 9;
            eTM.eventNum = eventNumBattle;
            eTM.StartCoroutine("eventBegin");
            fm.isBossDefeated[3] = true;
        }
        else if (enemyScript.enemyID == 13)
        {
            eventNumBattle = 13;
            eTM.eventNum = eventNumBattle;
            eTM.StartCoroutine("eventBegin");
            fm.isBossDefeated[4] = true;
        }
        else
        {
            eventNumBattle = 0;//何もしない
        }

        sm.recast = true;
        sm.recastLong();
        if (stageNum != 7 || stageNum != 6)
        {
            enemyAtStageObj[stageNum - 1].SetActive(false);
        }

        Destroy(GameObject.Find("enemy1(Clone)"));
        Destroy(GameObject.Find("enemy2(Clone)"));
        Destroy(GameObject.Find("enemy3(Clone)"));
        Destroy(GameObject.Find("enemy4(Clone)"));
        Destroy(GameObject.Find("enemy5(Clone)"));
        Destroy(GameObject.Find("enemy6(Clone)"));
        Destroy(GameObject.Find("enemy7(Clone)"));
        Destroy(GameObject.Find("enemy8(Clone)"));
        Destroy(GameObject.Find("enemy9(Clone)"));
        Destroy(GameObject.Find("enemy10(Clone)"));
        HPTextField.text = HP + "/" + MaxHP;
        HPSliderField.value = HP / MaxHP;
        HP2TextField.text = HP2 + "/" + MaxHP2;
        HP2SliderField.value = HP2 / MaxHP2;
        HP3TextField.text = HP3 + "/" + MaxHP3;
        HP3SliderField.value = HP3 / MaxHP3;

        gm.SceneNum = 5;
        gm.StartFadeOut();
        StartCoroutine("ResultEXPImageReset");

        choiceBattleObj.SetActive(false);

    }

    IEnumerator ResultEXPImageReset()
    {
        yield return new WaitForSeconds(1.0f);
        resultEXPImageRT.anchoredPosition = new Vector2(700, -70);
        resultEXPImageRT2.anchoredPosition = new Vector2(700, -150);
        resultEXPImageRT3.anchoredPosition = new Vector2(700, -230);
        resultEXPImageRT4.anchoredPosition = new Vector2(700, -310);
        textMoneyGainRect.anchoredPosition = new Vector2(700, -390);
        textTraceGainRect.anchoredPosition = new Vector2(700, -450);
    }

    IEnumerator CommandSelectUpdate()
    {
        yield return null;
        fm.audioSource.PlayOneShot(fm.SE1);
        switch (CommandIDDeck1)
        {
            case 1:
                CommandBGDeck1.anchoredPosition = new Vector2(-145, 0);
                break;
            case 2:
                CommandBGDeck1.anchoredPosition = new Vector2(-15, 0);
                break;
            default:
                Debug.Log("おかしい");
                break;
        }
        switch (CommandIDDeck2)
        {
            case 1:
                CommandBGDeck2.anchoredPosition = new Vector2(-145, 0);
                break;
            case 2:
                CommandBGDeck2.anchoredPosition = new Vector2(-15, 0);
                break;
            default:
                Debug.Log("おかしい");
                break;
        }
        switch (CommandIDDeck3)
        {
            case 1:
                CommandBGDeck3.anchoredPosition = new Vector2(-145, 0);
                break;
            case 2:
                CommandBGDeck3.anchoredPosition = new Vector2(-15, 0);
                break;
            default:
                Debug.Log("おかしい");
                break;
        }
    }

    IEnumerator Recast()
    {
        yield return new WaitForSeconds(0.50f);
        recast = false;
    }
    IEnumerator RecastBattleBegin()
    {
        yield return new WaitForSeconds(2.0f);
        recast = false;
    }
    IEnumerator RecastShort()
    {
        yield return new WaitForSeconds(0.3f);
        recast = false;
    }
    public void recastBattleBegin()
    {
        StartCoroutine("RecastBattleBegin");
    }

    public void startGameOver()
    {
        StartCoroutine("GameOverStaging");
    }
    IEnumerator GameOverStaging()//ゲームオーバー演出
    {
        gameOverText.SetActive(true);
        for (int ii = 1; ii < 11; ii++)
        {
            gameOverBackGround.color = new Color(1, 0, 0, ii * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 1; i < 11; i++)
        {
            gameOverTextRect.anchoredPosition = new Vector2(50 - i * 5,0);
            gameOverTextText.color = new Color(1,1,1,i * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.0f);

        isFinalBattle = false;
        isFinalBattle2 = false;
        isFinalBattle3 = false;

        //ステージの初めに戻る
        sm.recast = true;
        sm.recastLong();
        fm.isBattle = false;
        sm.isStageScene = true;
        fm.StatusTextUpdate_Field();

        Destroy(GameObject.Find("enemy0(Clone)"));
        Destroy(GameObject.Find("enemy1(Clone)"));
        Destroy(GameObject.Find("enemy2(Clone)"));
        Destroy(GameObject.Find("enemy3(Clone)"));
        Destroy(GameObject.Find("enemy4(Clone)"));
        Destroy(GameObject.Find("enemy5(Clone)"));
        Destroy(GameObject.Find("enemy6(Clone)"));
        Destroy(GameObject.Find("enemy7(Clone)"));
        Destroy(GameObject.Find("enemy8(Clone)"));
        Destroy(GameObject.Find("enemy9(Clone)"));
        Destroy(GameObject.Find("enemy10(Clone)"));
        Destroy(GameObject.Find("enemy10(Clone)"));
        Destroy(GameObject.Find("enemy11(Clone)"));
        Destroy(GameObject.Find("enemy12(Clone)"));
        Destroy(GameObject.Find("enemy13(Clone)"));
        HP = fm.MaxHP;
        HP2 = fm.MaxHP2;
        HP3 = fm.MaxHP3;
        HPTextField.text = HP + "/" + MaxHP;
        HPSliderField.value = HP / MaxHP;
        HP2TextField.text = HP2 + "/" + MaxHP2;
        HP2SliderField.value = HP2 / MaxHP2;
        HP3TextField.text = HP3 + "/" + MaxHP3;
        HP3SliderField.value = HP3 / MaxHP3;

        gm.SceneNum = 5;
        gm.StartFadeOut();
        yield return new WaitForSeconds(0.5f);
        isGameOver = false;
        gameOverBackGround.color = new Color(1, 0, 0, 0);
        gameOverTextRect.anchoredPosition = new Vector2(-50f, 0);
        gameOverTextText.color = new Color(1, 1, 1, 0);
        gameOverText.SetActive(false);
        StartCoroutine("ResultEXPImageReset");

        fm.playerRec.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("playerPosX", -1134), PlayerPrefs.GetFloat("playerPosY", -220));
    }

    IEnumerator APShortage()
    {
        audioSource.PlayOneShot(SEGetEXP);
        APShortageObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        APShortageObj.SetActive(false);
    }
    IEnumerator ConvertFailed()
    {
        audioSource.PlayOneShot(SEGetEXP);
        ConvertFailedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        ConvertFailedText.SetActive(false);
    }

    IEnumerator GuardAnimation1()
    {
        for (int i = 30; i > 0; i--)
        {
            GuardActiveImage[0].fillAmount = (i * 0.0333f);
            if (fightID != 1)
            {
                isGuard = false;
                guardImage[0].SetActive(false);
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (isGuard)
        {
            isGuard = false;
            guardImage[0].SetActive(false);
        }
    }
    IEnumerator GuardAnimation2()
    {
        for (int i = 30; i > 0; i--)
        {
            GuardActiveImage[1].fillAmount = (i * 0.0333f);
            if (fightID != 2)
            {
                isGuard = false;
                guardImage[1].SetActive(false);
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (isGuard)
        {
            isGuard = false;
            guardImage[1].SetActive(false);
        }
    }
    IEnumerator GuardAnimation3()
    {
        for (int i = 30; i > 0; i--)
        {
            GuardActiveImage[2].fillAmount = (i * 0.0333f);
            if (fightID != 3)
            {
                isGuard = false;
                guardImage[2].SetActive(false);
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (isGuard)
        {
            isGuard = false;
            guardImage[2].SetActive(false);
        }
    }

    IEnumerator actionTextAnim()
    {
        Image ActionTextImage = ActionTextObj.GetComponent<Image>();
        ActionTextRTransform.anchoredPosition = new Vector2(-50,250);
        ActionTextImage.color = new Color(1, 1, 1, 1);
        ActionText.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        ActionTextRTransform.anchoredPosition = new Vector2(0, 250);
        yield return new WaitForSeconds(0.3f);
        for (int i = 1; i < 11; i++)
        {
            ActionTextRTransform.anchoredPosition = new Vector2(i * 5, 250);
            ActionTextImage.color = new Color(1, 1, 1, 1 - i * 0.1f);
            ActionText.color = new Color(1, 1, 1, 1 - i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator buttonPushZ()
    {
        zButtonImage1.sprite = Z2; zButtonImage2.sprite = Z2; zButtonImage3.sprite = Z2;
        yield return new WaitForSeconds(0.1f);
        zButtonImage1.sprite = Z1; zButtonImage2.sprite = Z1; zButtonImage3.sprite = Z1;
    }
    IEnumerator buttonPushX()
    {
        xButtonImage1.sprite = X2; xButtonImage2.sprite = X2; xButtonImage3.sprite = X2;
        yield return new WaitForSeconds(0.1f);
        xButtonImage1.sprite = X1; xButtonImage2.sprite = X1; xButtonImage3.sprite = X1;
    }
    IEnumerator buttonPushUp()
    {
        UpButtonImage1.sprite = Up2; UpButtonImage2.sprite = Up2; UpButtonImage3.sprite = Up2;
        yield return new WaitForSeconds(0.1f);
        UpButtonImage1.sprite = Up1; UpButtonImage2.sprite = Up1; UpButtonImage3.sprite = Up1;
    }
    IEnumerator buttonPushDown()
    {
        DownButtonImage1.sprite = Down2; DownButtonImage2.sprite = Down2; DownButtonImage3.sprite = Down2;
        yield return new WaitForSeconds(0.1f);
        DownButtonImage1.sprite = Down1; DownButtonImage2.sprite = Down1; DownButtonImage3.sprite = Down1;
    }
}
