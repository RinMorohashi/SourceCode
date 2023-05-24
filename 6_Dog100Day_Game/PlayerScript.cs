using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�̈ړ���U���A�X�e�[�^�X�̏㏸�Ȃǂ��s���X�N���v�g
    /// </summary>
    public RectTransform rt;
    [Header("�Q�[���}�l�[�W��")] public Gamemanager gamemanager;
    public GameObject cvs;
    public GameObject cvsUI;
    public GameObject cvsBG;
    [Header("�t�B�[���hBGM")] public AudioSource BGM1;
    [Header("�o�g��BGM")] public AudioSource BGM2;
    [Header("�{�X��BGM")] public AudioSource BGM3;
    [Header("HP�e�L�X�g")] public Text HPtext;
    [Header("ATK�e�L�X�g")] public Text ATKtext;
    [Header("SPD�e�L�X�g")] public Text SPDtext;
    public float HP;
    public float ATK;
    public float SPD;
    public float previousMAXHP;
    public float previousATK;
    public float previousSPD;
    [Header("HP�o�[")] public GameObject HPBar;
    [Header("ATK�o�[")] public GameObject ATKBar;
    [Header("SPD�o�[")] public GameObject SPDBar;
    public int powerUpID;//�P�Ȃ�HP���������ꂽ�A�Q�Ȃ�ATK���������ꂽ
    [Header("�Q�[���}�l�[�W���̃I�[�f�B�I�\�[�X")] public AudioSource odiosourse;
    [Header("�W�����vSE")] public AudioClip jumpSE;
    [Header("�X�e�[�^�X����SE")] public AudioClip powerUpSE;
    [Header("�o�g���ړ�SE")] public AudioClip moveSE;
    [Header("�X�^�~�i��SE")] public AudioClip recoverSE;
    [Header("���̑���͖����ł�SE")] public AudioClip notSE;
    [Header("����SE")] public AudioClip victorySE;
    [Header("�U�����߂�SE")] public AudioClip chargeSE;
    [Header("�U��SE")] public AudioClip attackSE;
    [Header("�퓬�˓�SE")] public AudioClip encountSE;
    [Header("�y�[�W���߂���SE")] public AudioClip pageSE;
    [Header("����ɓ�����SE")] public AudioClip templeSE;
    [Header("�]�E����SE")] public AudioClip jobChangedSE;
    [Header("���b�N����Ă���SE")] public AudioClip lockedSE;
    private GameObject damageText;
    public int currentLine = 1;
    public int previousLine = 1;

    private float a = 0.67f;
    private float b = 0.77f;
    private float c = 0.88f;
    private float d = 0.44f;
    private float e = 0.51f;
    private float f = 0.59f;
    private float g = -120f;

    public int battleCurrentPos = 5;
    public int battlePreviousPos = 5;
    [Header("�퓬��ʂ̔w�i")] public RectTransform battleBGrt;
    [Header("�G�I�u�W�F�N�g")] public GameObject enemyObj;
    [Header("�G�I�u�W�F�N�g�̈ʒu")] public RectTransform enemyRT;
    [Header("�G�X�N���v�g")] public EnemyScript enemyScript;
    private GameObject enemy;//�G�̃v���n�u�������ɓ����
    private GameObject cerbers;
    private GameObject bloodSucker;
    private GameObject amorphous;
    private GameObject dragon;
    public int ratLevel = 1;//�G�̃��x��
    public int cerbersLevel = 1;
    public int bloodSuckerLevel = 1;
    public int amorphousLevel = 1;
    public int dragonLevel = 1;
    [Header("�J�b�g�C���C���[�W")] public GameObject cutInImage;
    [Header("�J�b�g�C���e�L�X�g")] public GameObject cutInText;
    [Header("FIGHT!���N�g��")] public RectTransform fightRt;
    [Header("FIGHT!�C���[�W")] public Image fightImage;
    [Header("GAMEOVER�C���[�W")] public RectTransform gameoverImage;
    [Header("�o�g������HP�e�L�X�g")] public Text playerBattleHPText;
    [Header("�o�g����ʂ̘g")] public Image BattleBGImage;
    [Header("HP�X���C�_�[")] public Slider HPSlider;
    [Header("����HP")] public float currentHP;
    public float Stamina;
    public float currentStamina; //���݃X�^�~�i
    public float previousStamina; //�O�̃X�^�~�i
    public float previousHP;
    [Header("�X�^�~�i�e�L�X�g")] public Text StaminaText;
    [Header("�X�^�~�i�X���C�_�[")] public Slider StaminaSlider;
    [Header("�X�^�~�i������Ȃ��e�L�X�g")] public GameObject staminaShortageText;
    [Header("�X�^�~�i�񕜗�")] public float staminaRecovery;
    public GameObject staminaImage1;
    public GameObject staminaImage2;
    public GameObject staminaImage3;
    public GameObject staminaImage4;
    public GameObject staminaImage5;
    public GameObject staminaImage6;
    [Header("�����e�L�X�g")] public GameObject VictoryText;
    [Header("�������炶�ᓖ����Ȃ��e�L�X�g")] public GameObject outOfRangeText;
    [Header("�^�[���e�L�X�g")] public Text TurnText;
    public int BattleTurn = 1;
    public GameObject attackEffect;

    [Header("���C���J�[�\��")] public GameObject lineCursor11;
    public GameObject lineCursor12;
    public GameObject lineCursor2;
    public GameObject lineCursor3;

    public GameObject field1;
    public GameObject field2;
    public GameObject field3;
    public GameObject field4;
    public GameObject field5;
    public GameObject field6;
    public GameObject field7;
    public GameObject field8;
    public GameObject field9;

    public GameObject powerUpText;
    public GameObject recoverText;
    [Header("�m�b�N�A�E�g�Q�[�W")] public GameObject knockOutObject;
    public float knockOutGauge = 0;
    public float previousknockOutGauge = 0;
    public Slider knockOutSlider;
    public Text knockOutText;
    public bool isKnockOut;
    [Header("KnockOut!���N�g��")] public RectTransform knockOutRt;
    [Header("KnockOut!�C���[�W")] public Image knockOutImage;
    public int countDown;
    public GameObject countDownObj;
    public bool Attacked;
    public bool isClear;
    public GameObject gameClearPanel;
    public int defeatTurn;
    public Text gameClearText;
    private bool isFirstBattle = true;
    //public float enemyIDBegin;
    [SerializeField] RectTransform StBarParentRt;
    [SerializeField] RectTransform daysTextRt;

    public GameObject ObjIconFadeAnim;
    public Image imageIconFadeAnim;
    public Sprite[] iconImage;
    public RectTransform templeBGrt;
    public int JobID;//�P�Ȃ�E�ҁA�Q�Ȃ疂�@�g���A�R�Ȃ�m���A�S�Ȃ�o�[�T�[�J�[�A�T�Ȃ�N���m�X�ɃW���u�`�F���W
    public RectTransform[] templeJobText;//�W���u�̖��O�e�L�X�g
    public Sprite[] jobSprite;//�W���u�̃C���[�W�摜
    public Image jobImage;
    public GameObject jobChangedImage;//�E�҂��疂�@�g���ɓ]�E�����I�̉摜
    public Text[] JobText;//�O�F�K�v���i�A�P�A�Q�F�W���u�R�}���h�A�A�R�`�T�F�]�E�{�[�i�X
    //�������灛���ɓ]�E�����I�̕�
    public Text[] JobChangedText;
    //�O�E�͉���������
    public int previousJobID;
    //�]�E�����𖞂����Ă��邩
    public bool isJobChangeable;
    //�]�E����
    public RectTransform jobChangeTermsTransform;
    public GameObject jobChangeTermsObj;
    [SerializeField] Text battleCommandGuideText;
    public int currentJobID;

    public GameObject magicBullet;
    public Animator bulletAnimator;

    public float testTimer;
    public GameObject mb;
    // �e��p�����[�^�[�̓C���X�y�N�^�[����ݒ肷��
    [SerializeField] Button tweetButton;                        // �c�C�[�g����{�^��
    [SerializeField] string text = "�c�C�[�g�@�\�̃e�X�g��";    // �c�C�[�g�ɑ}������e�L�X�g
    [SerializeField] string linkUrl = "";   // �c�C�[�g�ɑ}������URL
    [SerializeField] string hashtags = "Unity";        // �c�C�[�g�ɑ}������n�b�V���^�O

    void Start()
    {
        rt = GetComponent<RectTransform>();
        damageText = (GameObject)Resources.Load("damageText");
        enemy = (GameObject)Resources.Load("enemy");
        cerbers = (GameObject)Resources.Load("cerbers");
        bloodSucker = (GameObject)Resources.Load("bloodSucker");
        amorphous = (GameObject)Resources.Load("amorphous");
        dragon = (GameObject)Resources.Load("dragon");
        powerUpText = (GameObject)Resources.Load("PowerUpText");
        recoverText = (GameObject)Resources.Load("RecoverText");
        attackEffect = (GameObject)Resources.Load("swordEffect");
        currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("gamemanager��enemyID�� " + gamemanager.enemyID);
        
        testTimer += Time.deltaTime;
        if (testTimer >= 1.0f)
        {
            testTimer = 0;
        }
        if (mb != null && enemyRT != null)
        {
            mb.transform.position = Vector3.MoveTowards(mb.transform.position, enemyRT.position, 100 * Time.deltaTime);
            if (Vector3.Distance(mb.transform.position, enemyRT.transform.position) <= 1)
            {
                bulletAnimator.SetBool("hit", true);
            }
        }
    }

    IEnumerator jump()
    {
        gamemanager.Untouchable = true;
        odiosourse.PlayOneShot(jumpSE);
        lineCursor11.SetActive(false);
        lineCursor12.SetActive(false);
        lineCursor2.SetActive(false);
        lineCursor3.SetActive(false);

        switch (previousLine)
        {
            case 1:
                switch (currentLine)
                {
                    case 1:
                        rt.anchoredPosition = new Vector3(-340, -116, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-335, -86, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-330, -76, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-330, -86, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-330, -111, 0);
                        break;
                    case 2:
                        rt.anchoredPosition = new Vector3(-340, -116, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-325, -11, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-290, 21, 0);
                        rt.localScale = new Vector3(c, c, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-255, 11, 0);
                        rt.localScale = new Vector3(b, b, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-220, -19, 0);
                        rt.localScale = new Vector3(a, a, 1);
                        break;
                    case 3:
                        Debug.Log("��������������");
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (currentLine)
                {
                    case 1:
                        rt.anchoredPosition = new Vector3(-230, -24, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-230, 0, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-260, 39, 0);
                        rt.localScale = new Vector3(b, b, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-290, -36, 0);
                        rt.localScale = new Vector3(c, c, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-330, -111, 0);
                        rt.localScale = new Vector3(1, 1, 1);
                        break;
                    case 2:
                        rt.anchoredPosition = new Vector3(-230, -24, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-225, 11, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-220, 21, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-220, 11, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-220, -19, 0);
                        break;
                    case 3:
                        rt.anchoredPosition = new Vector3(-230, -24, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-225, 45, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-200, 87, 0);
                        rt.localScale = new Vector3(f, f, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-175, 77, 0);
                        rt.localScale = new Vector3(e, e, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-150, 47, 0);
                        rt.localScale = new Vector3(d, d, 1);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (currentLine)
                {
                    case 1:
                        Debug.Log("��������������");
                        break;
                    case 2:
                        rt.anchoredPosition = new Vector3(-160, 42, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-160, 62, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-180, 81, 0);
                        rt.localScale = new Vector3(e, e, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-200, 31, 0);
                        rt.localScale = new Vector3(f, f, 1);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-220, -19, 0);
                        rt.localScale = new Vector3(a, a, 1);
                        break;
                    case 3:
                        rt.anchoredPosition = new Vector3(-160, 42, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-155, 67, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-150, 87, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-150, 77, 0);
                        yield return new WaitForSeconds(0.1f);
                        rt.anchoredPosition = new Vector3(-150, 47, 0);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        previousLine = currentLine;

        switch (currentLine)
        {
            case 1:
                if (gamemanager.days < 9)
                {
                    lineCursor12.SetActive(true);
                }
                else
                {
                    lineCursor11.SetActive(true);
                }
                //�}�X��̃A�C�R�������i�����j
                if (gamemanager.unit != null)
                {
                    gamemanager.unit9Script.iconObjAtField.SetActive(false);
                    gamemanager.unit9Script.enemyObjAtField.SetActive(false);
                    switch (gamemanager.unit9Script.unitID)
                    {
                        case 1:
                            imageIconFadeAnim.sprite = iconImage[0];
                            imageIconFadeAnim.color = new Color(0.75f, 1, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 2:
                            imageIconFadeAnim.sprite = iconImage[1];
                            imageIconFadeAnim.color = new Color(1, 0.75f, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 3:
                            imageIconFadeAnim.sprite = iconImage[2];
                            imageIconFadeAnim.color = new Color(0.75f, 0.75f, 1);
                            StartCoroutine("IconFade");
                            break;
                        default:
                            break;
                    }
                }
                break;
            case 2:
                lineCursor2.SetActive(true);

                if (gamemanager.unit != null)
                {
                    gamemanager.unit9ScriptLine2.iconObjAtField.SetActive(false);
                    gamemanager.unit9ScriptLine2.enemyObjAtField.SetActive(false);
                    switch (gamemanager.unit9ScriptLine2.unitID)
                    {
                        case 1:
                            imageIconFadeAnim.sprite = iconImage[0];
                            imageIconFadeAnim.color = new Color(0.75f, 1, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 2:
                            imageIconFadeAnim.sprite = iconImage[1];
                            imageIconFadeAnim.color = new Color(1, 0.75f, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 3:
                            imageIconFadeAnim.sprite = iconImage[2];
                            imageIconFadeAnim.color = new Color(0.75f, 0.75f, 1);
                            StartCoroutine("IconFade");
                            break;
                        default:
                            break;
                    }
                }
                break;
            case 3:
                lineCursor3.SetActive(true);

                if (gamemanager.unit != null)
                {
                    gamemanager.unit9ScriptLine3.iconObjAtField.SetActive(false);
                    gamemanager.unit9ScriptLine3.enemyObjAtField.SetActive(false);
                    switch (gamemanager.unit9ScriptLine3.unitID)
                    {
                        case 1:
                            imageIconFadeAnim.sprite = iconImage[0];
                            imageIconFadeAnim.color = new Color(0.75f, 1, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 2:
                            imageIconFadeAnim.sprite = iconImage[1];
                            imageIconFadeAnim.color = new Color(1, 0.75f, 0.75f);
                            StartCoroutine("IconFade");
                            break;
                        case 3:
                            imageIconFadeAnim.sprite = iconImage[2];
                            imageIconFadeAnim.color = new Color(0.75f, 0.75f, 1);
                            StartCoroutine("IconFade");
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }

        switch (gamemanager.unitID)
        {
            case 1:
                previousMAXHP = HP;
                HP = HP * 1.02f;
                currentHP += HP * 0.2f;
                if (currentHP >= HP)
                {
                    currentHP = HP;
                }
                powerUpID = 1;
                odiosourse.PlayOneShot(powerUpSE);
                break;
            case 2:
                previousATK = ATK;
                ATK = ATK * 1.02f;
                powerUpID = 2;
                odiosourse.PlayOneShot(powerUpSE);
                break;
            case 3:
                previousSPD = SPD;
                SPD = SPD * 1.02f;
                powerUpID = 3;
                odiosourse.PlayOneShot(powerUpSE);
                break;
            case 4:
                //�퓬���n�߂�
                if (gamemanager.days != 100)
                {
                    StartCoroutine("BattleBeginning");
                    powerUpID = 4;
                }
                break;
            case 5:
                //�]�E���n�߂�
                gamemanager.isJobChangeMode = true;
                StartCoroutine("templeBGFadeIn");
                break;
            default:
                break;
        }
        HPtext.text = "HP  �F" + Mathf.Round(currentHP) + "/" + Mathf.Round(HP);
        ATKtext.text = "ATK�F" + Mathf.Round(ATK);
        SPDtext.text = "SPD�F" + Mathf.Round(SPD);
        HPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (HP/1000), 30);
        HPBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + HP * 0.11f ,200);
        ATKBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (ATK / 1000), 30);
        ATKBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + ATK * 0.11f, 170);
        SPDBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (SPD / 1000), 30);
        SPDBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + SPD * 0.11f, 140);

        GameObject put = Instantiate(powerUpText, new Vector3(1, 0, 0), Quaternion.identity);
        put.transform.SetParent(cvsUI.transform);
        put.transform.localScale = new Vector3(1, 1, 1);
        if (powerUpID == 1)
        {
            GameObject pu2 = Instantiate(recoverText, new Vector3(1, 0, 0), Quaternion.identity);
            pu2.transform.SetParent(cvsUI.transform);
            pu2.transform.localScale = new Vector3(1, 1, 1);
            switch (currentLine)
            {
                case 1:
                    pu2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240, -80);
                    break;
                case 2:
                    pu2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 0);
                    break;
                case 3:
                    pu2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 60);
                    break;
                default:
                    break;
            }
        }
            switch (currentLine)
        {
            case 1:
                put.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240, -80);
                break;
            case 2:
                put.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 0);
                break;
            case 3:
                put.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 60);
                break;
            default:
                break;
        }
        if (gamemanager.days == 100)
        {
            StartCoroutine("BattleBeginning");
        }
        else if(gamemanager.unitID != 4)
        {
            gamemanager.Untouchable = false;
        }
    }

    IEnumerator BattleBeginning()
    {
        BGM1.Stop();
        if (gamemanager.days != 100)
        {
            BGM2.Play();
        }
        else
        {
            BGM3.Play();
        }

        odiosourse.PlayOneShot(encountSE);
        field5.SetActive(true);
        gamemanager.Untouchable = true;
        //currentHP = HP;
        previousHP = currentHP;
        StartCoroutine("playerHPUpdate");
        currentStamina = Mathf.Round(3000 * (SPD / 3000f));
        if (currentStamina >= 6000)
        {
            currentStamina = 6000;
        }
        previousStamina = currentStamina;
        StartCoroutine("staminaUpdate");
        int rnd = Random.Range(1, 5);
        if (gamemanager.days == 100)
        {
            gamemanager.enemyID = 5;
        }
        switch (gamemanager.enemyID)
        {
            case 1:
                enemyObj = Instantiate(enemy, new Vector3(1000, 0, 0), Quaternion.identity);
                Debug.Log("VS ���b�g LV1");
                cutInText.GetComponent<Text>().text = "VS ���b�g LV" + ratLevel;
                break;
            case 2:
                enemyObj = Instantiate(cerbers, new Vector3(1000, 0, 0), Quaternion.identity);
                Debug.Log("VS �P���x���X LV1");
                cutInText.GetComponent<Text>().text = "VS �P���x���X LV" + cerbersLevel;
                break;
            case 3:
                enemyObj = Instantiate(bloodSucker, new Vector3(1000, 0, 0), Quaternion.identity);
                Debug.Log("VS �P���x���X LV1");
                cutInText.GetComponent<Text>().text = "VS �u���b�h�T�b�J�[ LV" + bloodSuckerLevel;
                break;
            case 4:
                enemyObj = Instantiate(amorphous, new Vector3(1000, 0, 0), Quaternion.identity);
                Debug.Log("VS �P���x���X LV1");
                cutInText.GetComponent<Text>().text = "VS �A�����t�@�X LV" + amorphousLevel;
                break;
            case 5:
                enemyObj = Instantiate(dragon, new Vector3(1000, 0, 0), Quaternion.identity);
                Debug.Log("VS �P���x���X LV1");
                cutInText.GetComponent<Text>().text = "VS �h���S��";
                knockOutObject.SetActive(true);
                break;
            default:
                break;
        }
        StartCoroutine("beginningCutIn");
        enemyRT = enemyObj.GetComponent<RectTransform>();
        enemyScript = enemyObj.GetComponent<EnemyScript>();
        enemyObj.transform.SetParent(cvs.transform);
        enemyObj.transform.localScale = new Vector3(1, 1, 1);

        for (int i = 1; i < 21; i++)
        {
            battleBGrt.anchoredPosition = new Vector3(0, 500 - (i * 25), 0);
            //UI���ǂ���
            StBarParentRt.anchoredPosition = new Vector2(0, 7.5f * i);
            daysTextRt.anchoredPosition = new Vector2(0, 220 + 5f * i);
            yield return new WaitForSeconds(0.02f);
        }
        enemyScript.StartCoroutine("enemyHPUpdate");
        gamemanager.isBattle = true;
        for (int i = 1; i < 6; i++)
        {//-330,-111����
            switch (currentLine)
            {
                case 1:
                    rt.anchoredPosition = new Vector3(-330 + (i * 51), -111 + (i * 38.2f) + g, 0);
                    rt.localScale = new Vector3(1 - (i * 0.04f), 1 - (i * 0.04f), 1);
                    break;
                case 2://-220 -19
                    rt.anchoredPosition = new Vector3(-220 + (i * 29), -19 + (i * 19.8f) + g, 0);
                    rt.localScale = new Vector3(0.67f + (i * 0.026f), 0.67f + (i * 0.026f), 1);
                    break;
                case 3://-150 47
                    rt.anchoredPosition = new Vector3(-150 + (i * 15), 47 + (i * 6.6f) + g, 0);
                    rt.localScale = new Vector3(0.44f + (i * 0.072f), 0.44f + (i * 0.072f), 1);
                    break;
                default:
                    break;
            }

            if (gamemanager.enemyID == 5)
            {
                enemyRT.anchoredPosition = new Vector3(115, 280 - (i * 40) + g, 0);
            }
            else if (gamemanager.enemyID == 2)
            {
                enemyRT.anchoredPosition = new Vector3(95, 250 - (i * 40) + g, 0); //51����40�ɕς���
            }
            else if (gamemanager.enemyID == 1 || gamemanager.enemyID == 3 || gamemanager.enemyID == 4)
            {
                enemyRT.anchoredPosition = new Vector3(95, 300 - (i * 51) + g, 0); //51����40�ɕς���
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator BattleEnding()
    {
        gamemanager.Untouchable = true;
        enemyScript.cautionActivate();
        switch (enemyScript.enemyID)
        {
            case 1:
                ratLevel++;
                break;
            case 2:
                cerbersLevel++;
                break;
            case 3:
                bloodSuckerLevel++;
                break;
            case 4:
                amorphousLevel++;
                break;
            case 5:
                dragonLevel++;//��������G���f�B���O�ցH
                BGM3.Stop();
                BGM2.Stop();
                BGM1.Stop();
                isClear = true;
                gameClearPanel.SetActive(true);
                defeatTurn = BattleTurn;
                gameClearText.text = "�����^�[���F" + defeatTurn + "\n\n�N���A���̃X�e�[�^�X\nHP�F" + Mathf.Round(HP) + "\nATK�F" + Mathf.Round(ATK) + "\nSPD�F" + Mathf.Round(SPD);
                StartCoroutine("clearPanelFadein");
                break;
            default:
                break;
        }
        int ID = enemyScript.enemyID;
        int reword = 0;
        switch (ID)
        {
            case 1:
                reword = 2 * ratLevel;            
                break;
            case 2:
                reword = 2 * cerbersLevel;
                break;
            case 3:
                reword = 3 * bloodSuckerLevel;
                break;
            case 4:
                reword = 3 * amorphousLevel;
                break;
            default:
                break;
        }
        Destroy(enemyObj);
        BattleTurn = 1;
        VictoryText.SetActive(true);
        odiosourse.PlayOneShot(victorySE);
        yield return new WaitForSeconds(0.7f);
        for (int i = 1; i < reword; i++)
        {
            int rnd = Random.Range(1, 4);
            switch (rnd)
            {
                case 1:
                    previousMAXHP = HP;
                    HP = HP * 1.02f;
                    powerUpID = 1;
                    odiosourse.PlayOneShot(powerUpSE);
                    break;
                case 2:
                    previousATK = ATK;
                    ATK = ATK * 1.02f;
                    powerUpID = 2;
                    odiosourse.PlayOneShot(powerUpSE);
                    break;
                case 3:
                    previousSPD = SPD;
                    SPD = SPD * 1.02f;
                    powerUpID = 3;
                    odiosourse.PlayOneShot(powerUpSE);
                    break;
                default:
                    break;
            }
            HPtext.text = "HP  �F" + Mathf.Round(currentHP) + "/" + Mathf.Round(HP);
            ATKtext.text = "ATK�F" + Mathf.Round(ATK);
            SPDtext.text = "SPD�F" + Mathf.Round(SPD);
            HPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (HP / 1000), 30);
            HPBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + HP * 0.11f, 200);
            ATKBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (ATK / 1000), 30);
            ATKBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + ATK * 0.11f, 170);
            SPDBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (SPD / 1000), 30);
            SPDBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + SPD * 0.11f, 140);

            GameObject put = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
            put.transform.SetParent(cvsUI.transform);
            put.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
            put.transform.localScale = new Vector3(1, 1, 1);
            BGM2.Stop();
            BGM3.Stop();
            BGM1.Play();
            TurnUpdate();
            yield return new WaitForSeconds(0.5f);
        }

        VictoryText.SetActive(false);
        switch (currentLine)
        {
            case 1:
                rt.anchoredPosition = new Vector3(-330, -111, 0);
                rt.localScale = new Vector3(1, 1, 1);
                break;
            case 2:
                rt.anchoredPosition = new Vector3(-220, -19, 0);
                rt.localScale = new Vector3(0.67f, 0.67f, 1);
                break;
            case 3:
                rt.anchoredPosition = new Vector3(-150, 47, 0);
                rt.localScale = new Vector3(0.44f, 0.44f, 1);
                break;
            default:
                break;
        }
        gamemanager.isBattle = false;
        battleCurrentPos = 5;
        battlePreviousPos = 5;
        fieldUpdate();
        for (int i = 1; i < 21; i++)
        {
            battleBGrt.anchoredPosition = new Vector3(0, i * 25, 0);
            StBarParentRt.anchoredPosition = new Vector2(0, 7.5f * (20 - i));
            daysTextRt.anchoredPosition = new Vector2(0, 220 + 5f * (20 - i));
            yield return new WaitForSeconds(0.02f);
        }
        gamemanager.Untouchable = false;
    }

    IEnumerator BattleMove()
    {
        gamemanager.Untouchable = true;
        currentStamina -= 1000;
        StartCoroutine("staminaUpdate");
        odiosourse.PlayOneShot(moveSE);
        switch (battlePreviousPos)
        {
            case 1:
                switch (battleCurrentPos)
                {
                    case 2:
                        rt.anchoredPosition = new Vector3(-164, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-162, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-98f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-96f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-95, 40 + g, 0);
                        break;
                    case 4:
                        rt.anchoredPosition = new Vector3(-164, 41 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-162, 42 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-122f, 77 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-124f, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-125, 80 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (battleCurrentPos)
                {
                    case 1:
                        rt.anchoredPosition = new Vector3(-96, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-98, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-162f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-164f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-165f, 40 + g, 0);
                        break;
                    case 3:
                        rt.anchoredPosition = new Vector3(-94, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-92, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-28f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-26f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-25f, 40 + g, 0);
                        break;
                    case 5:
                        rt.anchoredPosition = new Vector3(-94, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-92, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-78f, 77 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-76f, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-75f, 80 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (battleCurrentPos)
                {
                    case 1:
                        Debug.Log("��������������");
                        break;
                    case 2:
                        rt.anchoredPosition = new Vector3(-26, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-28, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-92f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-94f, 40 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-95f, 40 + g, 0);
                        break;
                    case 6:
                        rt.anchoredPosition = new Vector3(-25, 41 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-24, 43 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-21f, 77 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 80 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (battleCurrentPos)
                {
                    case 1:
                        rt.anchoredPosition = new Vector3(-126, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-128, 77 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-162f, 43 + g, 0);
                        rt.localScale = new Vector3(1, 1, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-164f, 41 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-165f, 40 + g, 0);
                        break;
                    case 5:
                        rt.anchoredPosition = new Vector3(-124, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-122, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-78f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-76f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-75f, 80 + g, 0);
                        break;
                    case 7:
                        rt.anchoredPosition = new Vector3(-124, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-122, 83 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-113f, 92 + g, 0);
                        rt.localScale = new Vector3(0.64f, 0.64f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-111f, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-110f, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (battleCurrentPos)
                {
                    case 2:
                        rt.anchoredPosition = new Vector3(-76, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-78, 77 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-92f, 43 + g, 0);
                        rt.localScale = new Vector3(1, 1, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-94f, 41 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-95f, 40 + g, 0);
                        break;
                    case 4:
                        rt.anchoredPosition = new Vector3(-76, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-78, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-122f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-124f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-125f, 80 + g, 0);
                        break;
                    case 6:
                        rt.anchoredPosition = new Vector3(-74, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-72, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-23f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-21f, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 80 + g, 0);
                        break;
                    case 8:
                        rt.anchoredPosition = new Vector3(-74, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-72, 83 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-68f, 92 + g, 0);
                        rt.localScale = new Vector3(0.64f, 0.64f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-66f, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-65f, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 6:
                switch (battleCurrentPos)
                {
                    case 3:
                        rt.anchoredPosition = new Vector3(-20, 79 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-21, 77 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-24f, 43 + g, 0);
                        rt.localScale = new Vector3(1, 1, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-25f, 41 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-25f, 40 + g, 0);
                        break;
                    case 5:
                        rt.anchoredPosition = new Vector3(-21, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-23, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-72, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-74, 80 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-75, 80 + g, 0);
                        break;
                    case 9:
                        rt.anchoredPosition = new Vector3(-20, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20, 83 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 92 + g, 0);
                        rt.localScale = new Vector3(0.64f, 0.64f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 7:
                switch (battleCurrentPos)
                {
                    case 4:
                        rt.anchoredPosition = new Vector3(-111, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-113, 92 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-122f, 83 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-124f, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-125f, 80 + g, 0);
                        break;
                    case 8:
                        rt.anchoredPosition = new Vector3(-109, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-107, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-68f, 95 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-66f, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-65f, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 8:
                switch (battleCurrentPos)
                {
                    case 5:
                        rt.anchoredPosition = new Vector3(-66, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-68, 92 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-72, 83 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-74, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-75, 80 + g, 0);
                        break;
                    case 7:
                        rt.anchoredPosition = new Vector3(-66, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-68, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-107, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-109, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-110, 95 + g, 0);
                        break;
                    case 9:
                        rt.anchoredPosition = new Vector3(-64, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-62, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-23, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-21, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            case 9:
                switch (battleCurrentPos)
                {
                    case 6:
                        rt.anchoredPosition = new Vector3(-20, 94 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20, 92 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 83 + g, 0);
                        rt.localScale = new Vector3(0.8f, 0.8f, 1);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 81 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-20f, 80 + g, 0);
                        break;
                    case 8:
                        rt.anchoredPosition = new Vector3(-21, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-23, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-62f, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-64f, 95 + g, 0);
                        yield return new WaitForSeconds(0.03f);
                        rt.anchoredPosition = new Vector3(-65f, 95 + g, 0);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        battlePreviousPos = battleCurrentPos;
        fieldUpdate();
        BattleTurn++;
        TurnUpdate();
        Attacked = false;
        enemyScript.StartCoroutine("enemyAction");
    }

    IEnumerator Attack()
    {
        gamemanager.Untouchable = true;
        currentStamina -= 1000;
        StartCoroutine("staminaUpdate");//�X�^�~�i���炷
        odiosourse.PlayOneShot(chargeSE);
        yield return new WaitForSeconds(0.5f);
        GameObject ae = Instantiate(attackEffect, new Vector3(1, 0, 0), Quaternion.identity);
        ae.transform.SetParent(cvsUI.transform);
        ae.transform.localScale = new Vector3(1, 1, 1);
        ae.GetComponent<RectTransform>().anchoredPosition = enemyObj.GetComponent<RectTransform>().anchoredPosition;//�a���̃G�t�F�N�g�o��
        yield return new WaitForSeconds(0.3f);
        float rnd = Random.Range(0.75f, 1.25f);
        if (enemyScript.enemyID != 5)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd;
        }
        else if (enemyScript.enemyID == 5 && isKnockOut)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd * ((knockOutGauge + 100f) / 100f);
            Debug.Log("�����Ɣ{���|�����Ă�(" + knockOutGauge + "��)");
        }
        else if (enemyScript.enemyID == 5)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd;
            Debug.Log("�m�b�N�A�E�g�ł͂Ȃ�");
        }
        else
        {
            Debug.Log("������������");
        }
        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
        dt.transform.SetParent(cvsUI.transform);
        dt.transform.localScale = new Vector3(1, 1, 1);
        float rnd2 = Random.Range(-20f, 20f);
        float rnd3 = Random.Range(-20f, 20f);
        dt.GetComponent<RectTransform>().anchoredPosition = enemyScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
        dt.GetComponent<DamageTextScript>().activate(ATK * rnd);
        if (enemyScript.enemyID == 5 && isKnockOut)
        {
            dt.GetComponent<DamageTextScript>().activate(ATK * rnd * ((knockOutGauge + 100f) / 100f));
        }
        odiosourse.PlayOneShot(attackSE);
        enemyScript.StartCoroutine("ReceiveDamage");//�G��k��������
        enemyScript.StartCoroutine("enemyHPUpdate");

        if (enemyScript.enemyID == 5)
        {
            float rnd4 = Random.Range(0.95f, 1.25f);
            knockOutGauge += 8.5f * rnd4;
            StartCoroutine("knockOutUpdate");
        }

        BattleTurn++;
        TurnUpdate();
        if (enemyScript.enemyCurrentHP > 0)
        {
            Attacked = true;
            enemyScript.StartCoroutine("enemyAction");
        }
    }

    IEnumerator MagicAttack()
    {
        gamemanager.Untouchable = true;
        currentStamina -= 2000;
        StartCoroutine("staminaUpdate");//�X�^�~�i���炷
        odiosourse.PlayOneShot(chargeSE);
        Debug.Log("���@�U�������܂�");
        mb = Instantiate(magicBullet, new Vector3(1, 0, 0), Quaternion.identity);
        mb.transform.SetParent(cvs.transform);
        mb.transform.localScale = new Vector3(1f, 1f, 1f);
        mb.transform.position = transform.position;//���@�̃G�t�F�N�g�o��
        bulletAnimator = mb.GetComponent<Animator>();
        yield return new WaitForSeconds(0.8f);
        float rnd = Random.Range(0.75f, 1.25f);
        if (enemyScript.enemyID != 5)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd * 1.5f;
        }
        else if (enemyScript.enemyID == 5 && isKnockOut)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd * 1.5f * ((knockOutGauge + 100f) / 100f);
            Debug.Log("�����Ɣ{���|�����Ă�(" + knockOutGauge + "��)");
        }
        else if (enemyScript.enemyID == 5)
        {
            enemyScript.enemyCurrentHP -= ATK * rnd * 1.5f;
            Debug.Log("�m�b�N�A�E�g�ł͂Ȃ�");
        }
        else
        {
            Debug.Log("������������");
        }
        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
        dt.transform.SetParent(cvsUI.transform);
        dt.transform.localScale = new Vector3(1, 1, 1);
        float rnd2 = Random.Range(-20f, 20f);
        float rnd3 = Random.Range(-20f, 20f);
        dt.GetComponent<RectTransform>().anchoredPosition = enemyScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
        dt.GetComponent<DamageTextScript>().activate(ATK * rnd * 1.5f);
        if (enemyScript.enemyID == 5 && isKnockOut)
        {
            dt.GetComponent<DamageTextScript>().activate(ATK * rnd * 1.5f * ((knockOutGauge + 100f) / 100f));
        }
        odiosourse.PlayOneShot(attackSE);
        enemyScript.StartCoroutine("ReceiveDamage");//�G��k��������
        enemyScript.StartCoroutine("enemyHPUpdate");

        if (enemyScript.enemyID == 5)
        {
            float rnd4 = Random.Range(0.95f, 1.25f);
            knockOutGauge += 8.5f * rnd4;
            StartCoroutine("knockOutUpdate");
        }

        BattleTurn++;
        TurnUpdate();
        if (enemyScript.enemyCurrentHP > 0)
        {
            Attacked = true;
            enemyScript.StartCoroutine("enemyAction");
        }
    }

    IEnumerator Rest()
    {
        gamemanager.Untouchable = true;
        odiosourse.PlayOneShot(recoverSE);
        float rnd = Random.Range(0.75f, 1.25f);
        currentStamina += staminaRecovery * rnd * (SPD / 1000);
        if (currentStamina > 6000)
        {
            currentStamina = 6000;
        }
        StartCoroutine("staminaUpdate");
        BattleTurn++;
        TurnUpdate();
        yield return new WaitForSeconds(0.7f);
        Attacked = false;
        enemyScript.StartCoroutine("enemyAction");
    }

    IEnumerator knockOutUpdate()
    {
        knockOutText.text = "KNOCK OUT     " + Mathf.Round(knockOutGauge) + "%";
        float differ = previousknockOutGauge - knockOutGauge; //�}�C�i�X�ɂȂ�
        for (int i = 1; i < 31; i++)
        {
            knockOutSlider.value = (knockOutGauge + differ / i) / 100f;
            yield return new WaitForSeconds(0.01f);
        }
        knockOutSlider.value = knockOutGauge / 100f;
        previousknockOutGauge = knockOutGauge;

        if (knockOutGauge >= 100f && !isKnockOut)
        {
            isKnockOut = true;
            StartCoroutine("KnockOutAnimation");
            countDown = 10;
            enemyScript.cautionReset();
            StartCoroutine("knockOutSE");
            countDownObj.SetActive(true);
            for (int i = 1; i < 11; i++)
            {
                // x�������ɂ��Ė��b2�x�A��]������Quaternion���쐬�i�ϐ���rot�Ƃ���j
                Quaternion rot = Quaternion.AngleAxis(-9, Vector3.forward);
                // ���݂̎��M�̉�]�̏����擾����B
                Quaternion q = enemyObj.transform.rotation;
                // �������āA���g�ɐݒ�
                enemyObj.transform.rotation = q * rot;
                yield return new WaitForSeconds(0.03f);
            }
        }
        if (isKnockOut)
        {
            countDown--;
            countDownObj.GetComponent<Text>().text = "�����オ��܂ł��Ɓ@" + countDown + "�^�[��";
            if (countDown == 0)
            {
                //�m�b�N�A�E�g�I��
                Debug.Log("�m�b�N�A�E�g�I��");
                countDownObj.SetActive(false);
                enemyScript.isAttacking = false;
                isKnockOut = false;
                knockOutGauge = 0;
                StartCoroutine("knockOutUpdate");
                for (int i = 1; i < 11; i++)
                {
                    // x�������ɂ��Ė��b2�x�A��]������Quaternion���쐬�i�ϐ���rot�Ƃ���j
                    Quaternion rot2 = Quaternion.AngleAxis(9, Vector3.forward);
                    // ���݂̎��M�̉�]�̏����擾����B
                    Quaternion q = Quaternion.AngleAxis(9, Vector3.forward);
                    if (enemyObj != null)
                    {
                        q = enemyObj.transform.rotation;
                    }
                    // �������āA���g�ɐݒ�
                    enemyObj.transform.rotation = q * rot2;
                    yield return new WaitForSeconds(0.03f);
                }
            }
        }
    }

    IEnumerator playerHPUpdate()
    {
        playerBattleHPText.text = "HP       " + Mathf.Round(currentHP) + " / " + Mathf.Round(HP);
        float differ = previousHP - currentHP;
        for (int i = 1; i < 31; i++)
        {
            HPSlider.value = (currentHP + differ / i) / HP;
            yield return new WaitForSeconds(0.01f);
        }
        HPSlider.value = currentHP / HP;
        previousHP = currentHP;
        if (currentHP <= 0)
        {
            playerBattleHPText.text = "HP       " + "0" + " / " + Mathf.Round(HP);
            StartCoroutine("GameOver");
        }
        if (currentHP * 3.3f <= HP)
        {
            playerBattleHPText.color = new Color(0.804f, 0, 0, 1);
            BattleBGImage.color = new Color(0.804f, 0, 0, 1);
        }
        else
        {
            playerBattleHPText.color = new Color(0.804f, 0.804f, 0.804f, 1);
            BattleBGImage.color = new Color(0, 0, 0, 1);
        }
    }

    IEnumerator staminaUpdate()
    {
        float differ = previousStamina - currentStamina;
        for (int i = 1; i < 31; i++)
        {
            StaminaSlider.value = (currentStamina + differ / i) / Stamina;
            yield return new WaitForSeconds(0.01f);
        }
        StaminaSlider.value = currentStamina / Stamina;
        previousStamina = currentStamina;
        if (currentStamina < 1000f)
        {
            staminaImage1.SetActive(true);
            staminaImage2.SetActive(true);
            staminaImage3.SetActive(true);
            staminaImage4.SetActive(true);
            staminaImage5.SetActive(true);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "0 / 6";
        }
        else if (currentStamina >= 1000f && currentStamina < 2000f)
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(true);
            staminaImage3.SetActive(true);
            staminaImage4.SetActive(true);
            staminaImage5.SetActive(true);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "1 / 6";
        }
        else if (currentStamina >= 2000f && currentStamina < 3000f)
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(false);
            staminaImage3.SetActive(true);
            staminaImage4.SetActive(true);
            staminaImage5.SetActive(true);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "2 / 6";
        }
        else if (currentStamina >= 3000f && currentStamina < 4000f)
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(false);
            staminaImage3.SetActive(false);
            staminaImage4.SetActive(true);
            staminaImage5.SetActive(true);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "3 / 6";
        }
        else if (currentStamina >= 4000f && currentStamina < 5000f)
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(false);
            staminaImage3.SetActive(false);
            staminaImage4.SetActive(false);
            staminaImage5.SetActive(true);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "4 / 6";
        }
        else if (currentStamina >= 5000f && currentStamina < 6000f)
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(false);
            staminaImage3.SetActive(false);
            staminaImage4.SetActive(false);
            staminaImage5.SetActive(false);
            staminaImage6.SetActive(true);
            StaminaText.text = "STAMINA  " + "5 / 6";
        }
        else
        {
            staminaImage1.SetActive(false);
            staminaImage2.SetActive(false);
            staminaImage3.SetActive(false);
            staminaImage4.SetActive(false);
            staminaImage5.SetActive(false);
            staminaImage6.SetActive(false);
            StaminaText.text = "STAMINA  " + "6 / 6";
        }
    }

    public void TurnUpdate()
    {
        TurnText.text = "" + BattleTurn;
    }

    IEnumerator staminaShortage()
    {
        gamemanager.Untouchable = true;
        staminaShortageText.SetActive(true);
        odiosourse.PlayOneShot(notSE);
        yield return new WaitForSeconds(0.7f);
        gamemanager.Untouchable = false;
        staminaShortageText.SetActive(false);
    }

    IEnumerator OutOfRange()
    {
        gamemanager.Untouchable = true;
        outOfRangeText.SetActive(true);
        odiosourse.PlayOneShot(notSE);
        yield return new WaitForSeconds(0.7f);
        gamemanager.Untouchable = false;
        outOfRangeText.SetActive(false);
    }

    public void fieldUpdate()
    {
        field1.SetActive(false);
        field2.SetActive(false);
        field3.SetActive(false);
        field4.SetActive(false);
        field5.SetActive(false);
        field6.SetActive(false);
        field7.SetActive(false);
        field8.SetActive(false);
        field9.SetActive(false);
        switch (battleCurrentPos)
        {
            case 1:
                field1.SetActive(true);
                break;
            case 2:
                field2.SetActive(true);
                break;
            case 3:
                field3.SetActive(true);
                break;
            case 4:
                field4.SetActive(true);
                break;
            case 5:
                field5.SetActive(true);
                break;
            case 6:
                field6.SetActive(true);
                break;
            case 7:
                field7.SetActive(true);
                break;
            case 8:
                field8.SetActive(true);
                break;
            case 9:
                field9.SetActive(true);
                break;
            default:
                break;
        }
    }

    IEnumerator playerReceiveDamage()
    {
        for (int i = 0; i < 11; i++)
        {
            if (i == 0 || i == 10)
            {
                rt.anchoredPosition += new Vector2(3, 0);
            }
            else if (i % 2 == 1)
            {
                rt.anchoredPosition -= new Vector2(6, 0);
            }
            else
            {
                rt.anchoredPosition += new Vector2(6, 0);
            }
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator beginningCutIn()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 6; i++)
        {
            cutInImage.transform.localScale = new Vector3(1, i / 5f, 0);
            yield return new WaitForSeconds(0.06f);
        }
        for (int i = 1; i < 6; i++)
        {
            cutInText.GetComponent<RectTransform>().anchoredPosition = new Vector2(240 / i / i, 0);
            yield return new WaitForSeconds(0.06f);
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 1; i < 6; i++)
        {
            cutInText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240 / (6 - i) / (6 - i), 0);
            yield return new WaitForSeconds(0.04f);
        }
        cutInText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
        for (int i = 0; i < 6; i++)
        {
            cutInImage.transform.localScale = new Vector3(1, (6 - i) / 10f, 0);
            yield return new WaitForSeconds(0.04f);
        }
        cutInImage.transform.localScale = new Vector3(1, 0, 1);
        for (int i = 1; i < 11; i++)
        {
            fightImage.color = new Color(1, 1, 1, i / 10f);
            fightRt.sizeDelta = new Vector2(320f + (128f / i), 180f + (72 / i));
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 1; i < 11; i++)
        {
            fightImage.color = new Color(1, 1, 1, (11 - i) / 10f);
            fightRt.sizeDelta = new Vector2(320f + (128f / (11 - i)), 180f + (72 / (11 - i)));
            yield return new WaitForSeconds(0.05f);
        }
        fightImage.color = new Color(1, 0.196f, 0.196f, 0);
        gamemanager.Untouchable = false;
    }

    IEnumerator KnockOutAnimation()
    {

        for (int i = 1; i < 51; i++)
        {
            knockOutImage.color = new Color(1, 1, 1, i / 50f);
            knockOutRt.sizeDelta = new Vector2(320f + (640f / i), 180f + (360 / i));
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 1; i < 51; i++)
        {
            knockOutImage.color = new Color(1, 1, 1, (51 - i) / 50f);
            knockOutRt.sizeDelta = new Vector2(320f + (640f / (51 - i)), 180f + (360 / (51 - i)));
            yield return new WaitForSeconds(0.01f);
        }
        knockOutImage.color = new Color(1, 1, 1, 0);
    }

    IEnumerator knockOutSE()
    {
        odiosourse.PlayOneShot(moveSE);
        yield return new WaitForSeconds(0.3f);
        odiosourse.PlayOneShot(moveSE);
        yield return new WaitForSeconds(0.3f);
        odiosourse.PlayOneShot(moveSE);
    }

    IEnumerator GameOver()
    {
        gamemanager.Untouchable = true;
        BGM3.Stop();
        BGM2.Stop();
        BGM1.Stop();
        rt.Rotate(0, 0, 90.0f);
        for (int i = 1; i < 11; i++)
        {
            gameoverImage.anchoredPosition = new Vector2(0, 540f - (i * 54f));
            yield return new WaitForSeconds(0.05f);
        }
        gamemanager.isGameOver = true;
    }

    public void OnClickTurnRankingButton()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(defeatTurn, 0);
    }
    public void OnClickStatusRankingButton()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Mathf.Round(HP + ATK + SPD), 1);
    }

    public void OnClickTweetButton()
    {
        Tweeting();
    }

    private void Tweeting()
    {
        naichilab.UnityRoomTweet.Tweet("100daysdog", "�h���S����" + defeatTurn + "�^�[���œ|�����I\n�X�e�[�^�X�� \nHP�F" + Mathf.Round(HP) + " \nATK�F" + Mathf.Round(ATK) + "\nSPD�F" + Mathf.Round(SPD) + "\n", "unityroom", "unity1week");
    }

    IEnumerator clearPanelFadein()
    {
        for (int i = 1; i < 11; i++)
        {
            gameClearPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540f - i * 54f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator IconFade()
    {
        for (int i = 1; i < 11; i++)
        {
            imageIconFadeAnim.color = new Color(imageIconFadeAnim.color.r, imageIconFadeAnim.color.g, imageIconFadeAnim.color.b, 1 - i * 0.1f);
            ObjIconFadeAnim.transform.localScale = new Vector3(1 + i * 0.1f, 1 + i * 0.1f, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator templeBGFadeIn()
    {
        JobID = 1;
        JobTextUpdate();

        odiosourse.PlayOneShot(templeSE);
        for (int i = 1; i < 21; i++)
        {
            templeBGrt.anchoredPosition = new Vector3(0, -500 + (i * 23), 0);
            yield return new WaitForSeconds(0.02f);
        }
    }


    IEnumerator templePushRight()
    {
        yield return new WaitForSeconds(0.1f);
        switch (JobID)
        {
            case 1://�P����Q��
                odiosourse.PlayOneShot(pageSE);
                templeJobText[0].DOLocalMove(new Vector2(-70, 130), 0.5f);
                templeJobText[1].DOLocalMove(new Vector2(0, 130), 0.5f);
                templeJobText[2].DOLocalMove(new Vector2(70, 130), 0.5f);
                jobImage.sprite = jobSprite[1];
                break;
            case 2:
                odiosourse.PlayOneShot(pageSE);
                templeJobText[0].DOLocalMove(new Vector2(-140, 130), 0.5f);
                templeJobText[1].DOLocalMove(new Vector2(-70, 130), 0.5f);
                templeJobText[2].DOLocalMove(new Vector2(0, 130), 0.5f);
                jobImage.sprite = jobSprite[2];
                break;
            default:
                break;
        }
        if (JobID < 3)
        {
            JobID++;
        }
        JobTextUpdate();
    }
    IEnumerator templePushLeft()
    {
        yield return new WaitForSeconds(0.1f);
        switch (JobID)
        {
            case 2:
                odiosourse.PlayOneShot(pageSE);
                templeJobText[0].DOLocalMove(new Vector2(0, 130), 0.5f);
                templeJobText[1].DOLocalMove(new Vector2(70, 130), 0.5f);
                templeJobText[2].DOLocalMove(new Vector2(140, 130), 0.5f);
                jobImage.sprite = jobSprite[0];
                break;
            case 3:
                odiosourse.PlayOneShot(pageSE);
                templeJobText[0].DOLocalMove(new Vector2(-70, 130), 0.5f);
                templeJobText[1].DOLocalMove(new Vector2(0, 130), 0.5f);
                templeJobText[2].DOLocalMove(new Vector2(70, 130), 0.5f);
                jobImage.sprite = jobSprite[1];
                break;
            default:
                break;
        }
        if (JobID > 0)
        {
            JobID--;
        }
        JobTextUpdate();
    }
    IEnumerator templeJobDecide()
    {
        //�����𖞂����Ă�����
        if (isJobChangeable)
        {
            odiosourse.PlayOneShot(victorySE);
            jobChangedImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            switch (previousJobID)
            {
                case 1:
                    JobChangedText[0].text = "�E��";
                    break;
                case 2:
                    JobChangedText[0].text = "���@�g��";
                    break;
                case 3:
                    JobChangedText[0].text = "�m��";
                    break;
                default:
                    break;
            }
            switch (JobID)
            {
                case 1:
                    JobChangedText[1].text = "�E��";
                    //HP���グ��
                    previousMAXHP = HP;
                    HP = HP * 1.05f;
                    powerUpID = 1;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put.transform.SetParent(cvsUI.transform);
                    put.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //ATK���グ��
                    previousATK = ATK;
                    ATK = ATK * 1.05f;
                    powerUpID = 2;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put2 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put2.transform.SetParent(cvsUI.transform);
                    put2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put2.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //SPD���グ��
                    previousSPD = SPD;
                    SPD = SPD * 1.05f;
                    powerUpID = 3;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put3 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put3.transform.SetParent(cvsUI.transform);
                    put3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put3.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    //�퓬��ʂ̃R�}���h���������A�b�v�f�[�g
                    battleCommandGuideText.text = "�����L�[�F���ǂ�\n   Z�L�[�F��������\n\n   C�L�[�F�₷��";
                    currentJobID = 1;
                    break;
                case 2:
                    JobChangedText[1].text = "���@�g��";
                    //HP���グ��
                    previousMAXHP = HP;
                    HP = HP * 1.00f;
                    powerUpID = 1;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put4 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put4.transform.SetParent(cvsUI.transform);
                    put4.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put4.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //ATK���グ��
                    previousATK = ATK;
                    ATK = ATK * 1.15f;
                    powerUpID = 2;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put5 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put5.transform.SetParent(cvsUI.transform);
                    put5.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put5.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //SPD���グ��
                    previousSPD = SPD;
                    SPD = SPD * 1.00f;
                    powerUpID = 3;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put6 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put6.transform.SetParent(cvsUI.transform);
                    put6.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put6.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    //�퓬��ʂ̃R�}���h���������A�b�v�f�[�g
                    battleCommandGuideText.text = "�����L�[�F���ǂ�\n   Z�L�[�F��������\n   X�L�[�F�܂ق�\n   C�L�[�F�₷��";
                    currentJobID = 2;
                    break;
                case 3:
                    JobChangedText[1].text = "�m��";
                    //HP���グ��
                    previousMAXHP = HP;
                    HP = HP * 1.20f;
                    powerUpID = 1;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put7 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put7.transform.SetParent(cvsUI.transform);
                    put7.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put7.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //ATK���グ��
                    previousATK = ATK;
                    ATK = ATK * 1.00f;
                    powerUpID = 2;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put8 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put8.transform.SetParent(cvsUI.transform);
                    put8.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put8.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    yield return new WaitForSeconds(0.5f);
                    //SPD���グ��
                    previousSPD = SPD;
                    SPD = SPD * 1.10f;
                    powerUpID = 3;
                    odiosourse.PlayOneShot(powerUpSE);
                    GameObject put9 = Instantiate(powerUpText, new Vector3(0, 0, 0), Quaternion.identity);
                    put9.transform.SetParent(cvsUI.transform);
                    put9.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120f);
                    put9.transform.localScale = new Vector3(1, 1, 1);
                    StatusTextUpdate();
                    //�퓬��ʂ̃R�}���h���������A�b�v�f�[�g
                    battleCommandGuideText.text = "�����L�[�F���ǂ�\n   Z�L�[�F��������\n   X�L�[�F�����ӂ�\n   C�L�[�F�₷��";
                    currentJobID = 3;
                    break;
                default:
                    break;
            }
            previousJobID = JobID;

            yield return new WaitForSeconds(1f);
            for (int i = 1; i < 21; i++)
            {
                templeBGrt.anchoredPosition = new Vector3(0, -40 - (i * 23), 0);
                yield return new WaitForSeconds(0.02f);
            }

            jobChangedImage.SetActive(false);
            gamemanager.isJobChangeMode = false;
        }
        else
        {
            odiosourse.PlayOneShot(lockedSE);
            //�����𖞂����Ă��܂���
            jobChangeTermsTransform.DOLocalMove(new Vector2(10, -120), 0.1f).OnComplete(() =>
            {
                jobChangeTermsTransform.DOLocalMove(new Vector2(-10, -120), 0.1f).OnComplete(() =>
                {
                    jobChangeTermsTransform.DOLocalMove(new Vector2(0, -120), 0.05f);
                });
            });
        }
    }
    public void JobTextUpdate()
    {
        switch (JobID)
        {
            case 1:
                jobChangeTermsObj.SetActive(false);
                JobText[0].text = "�Ȃ�";
                JobText[1].text = "�Ȃ�";
                JobText[2].text = "";
                JobText[3].text = "+5%";
                JobText[4].text = "+5%";
                JobText[5].text = "+5%";
                if (true)
                {
                    isJobChangeable = true;
                    JobText[0].color = new Color(1, 1, 1, 1);
                    jobChangeTermsObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                break;
            case 2:
                jobChangeTermsObj.SetActive(true);
                jobChangeTermsObj.GetComponent<Image>().sprite = iconImage[1];
                JobText[0].text = "1500�ȏ�";
                JobText[1].text = "�u�t�@�C�A�v";
                JobText[2].text = "���������������B";
                JobText[3].text = "+0%";
                JobText[4].text = "+15%";
                JobText[5].text = "+0%";
                if (ATK >= 1500)
                {
                    isJobChangeable = true;
                    JobText[0].color = new Color(1, 1, 1, 1);
                    jobChangeTermsObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    isJobChangeable = false;
                    JobText[0].color = new Color(1f, 0.75f, 0.75f, 1f);
                    jobChangeTermsObj.GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                }
                break;
            case 3:
                jobChangeTermsObj.SetActive(true);
                jobChangeTermsObj.GetComponent<Image>().sprite = iconImage[0];
                JobText[0].text = "1500�ȏ�";
                JobText[1].text = "�u�q�[�����O�v";
                JobText[2].text = "HP��S������B";
                JobText[3].text = "+20%";
                JobText[4].text = "+0%";
                JobText[5].text = "+10%";
                if (HP >= 2000)
                {
                    isJobChangeable = true;
                    JobText[0].color = new Color(1, 1, 1, 1);
                    jobChangeTermsObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    isJobChangeable = false;
                    JobText[0].color = new Color(1f, 0.75f, 0.75f, 1f);
                    jobChangeTermsObj.GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                }
                break;
            default:
                break;
        }
    }

    public void StatusTextUpdate()
    {
        HPtext.text = "HP  �F" + Mathf.Round(currentHP) + "/" + Mathf.Round(HP);
        ATKtext.text = "ATK�F" + Mathf.Round(ATK);
        SPDtext.text = "SPD�F" + Mathf.Round(SPD);
        HPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (HP / 1000), 30);
        HPBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + HP * 0.11f, 200);
        ATKBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (ATK / 1000), 30);
        ATKBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + ATK * 0.11f, 170);
        SPDBar.GetComponent<RectTransform>().sizeDelta = new Vector2(220 * (SPD / 1000), 30);
        SPDBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480 + SPD * 0.11f, 140);
    }
}
