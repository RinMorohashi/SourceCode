using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    /// <summary>
    /// �G�����X�^�[�̍s����HP�A�m�b�N�A�E�g�Q�[�W���Ǘ�����X�N���v�g
    /// </summary>
    public float MaxHP;
    public float HP;
    public int ATK;
    public int DEF;
    public int DEFNotKO;//KO���ĂȂ��Ƃ���DEF
    public int DEFKO;//KO���Ă�Ƃ���DEF
    public int SPD;
    public float KnockOut;
    public int KnockOutResistance; //�P�Ȃ�ϐ����A�Q�Ȃ�ϐ����A�R�Ȃ�ϐ���
    public Slider HPSlider;
    public Slider KOSlider;
    public GameObject HPSliderObj;
    public GameObject KOSliderObj;
    public GameObject BackGroundUI;

    public float ChargeTime;
    public BattleManager bm;
    [Header("�G�̈ʒu�f�[�^")] public RectTransform enemyRect;
    [Header("�G�̈ʒu�f�[�^")] public RectTransform enemyRect2;
    [Header("�G�̈ʒu�f�[�^")] public RectTransform enemyRect3;
    [Header("�����̈ʒu�f�[�^")] public RectTransform monsterRect;
    [Header("�����̉摜�ʒu�f�[�^")] public RectTransform monsterImageRect;
    [Header("���g��HP�e�L�X�g")] public Text HPText;
    [Header("���g��KO�e�L�X�g")] public Text KOText;

    public AudioSource audiosource;
    public AudioClip SE1;//���݂��Ƃ��̉�
    public AudioClip SE2;//����Ƃ��̉�
    public AudioClip SE3;//�����Ƃ��̉�
    public AudioClip SE4;//�e���\���鎞�̉�
    public AudioClip SE5;//���A���鎞�̉�
    public AudioClip SE6;//��Ԏ��̉�
    public AudioClip SE7;//�ˌ�����Ƃ��̉�
    public AudioClip SE8;//�p���[�A�b�v����Ƃ��̉�
    public AudioClip SE9;//�łɂȂ�Ƃ��̉�

    public GameObject damageText;
    public GameObject damageTextObj;
    public GameObject battleBackground;

    public int damage;

    public bool isDragon;
    public bool isShisa;
    public bool isReviathan;
    public int enemyID;//�P�Ȃ�I�A�Q�Ȃ�g�}�g�A�R�Ȃ�V�[�T�[�A�S�C�T�C�C�̓G�A�U�C�C�̃{�X�A�V�C�W�C�s�s�̓G

    public bool isKnockOut;
    public float KOTime;
    public GameObject KOImage;
    public RectTransform enemyImageRect;
    public Image enemyImage;
    [Header("��ʒu")] public Vector2 locationWait;
    public Vector3 locationWaitDO;
    public bool isDead;
    public float regeneTime;

    public AudioClip SEKO;
    public AudioClip SESlip;
    public AudioClip SEGuard;//�K�[�h���ꂽ�Ƃ��̉�

    public int enemyEXP;//�|�����Ƃ��̊l���o���l
    public int enemyMoney;
    public int enemyItem;

    private int nextActNum;
    [SerializeField] Sprite image1;
    [SerializeField] Sprite image2;
    [SerializeField] Sprite image3;
    [SerializeField] Sprite image4;
    [SerializeField] Sprite image5;
    [SerializeField] bool isActing;
    private GameObject ActionWindow;
    private Text ActionWindowText;
    private RectTransform ActionWindowRect;
    public bool isHiding;

    private float RecastTimeEnemy5;
    private float RecastTimeEnemy7;
    private int bulletNUM;
    [SerializeField] float actSpeed;
    private bool reviathanFiftySwitch;
    public GameObject waterWall;
    public bool isConvertable;
    private float waterWallTime;
    private GameObject[] poisonIcon;
    public bool[] isPoison;
    private float PoisonTime;

    public GameObject crowEffect;
    public GameObject crowEffectObj;
    public GameObject bulletEffect;
    public GameObject bulletEffectObj;

    public Tweener tweener;

    void Start()
    {
        bm = GameObject.Find("gameManager").GetComponent<BattleManager>();
        enemyRect = GameObject.Find("player").GetComponent<RectTransform>();
        enemyRect2 = GameObject.Find("monster1").GetComponent<RectTransform>();
        enemyRect3 = GameObject.Find("monster2").GetComponent<RectTransform>();
        monsterRect = GetComponent<RectTransform>();
        audiosource = GameObject.Find("JukeBoxSE").GetComponent<AudioSource>();
        damageText = (GameObject)Resources.Load("DamageText");
        battleBackground = GameObject.Find("BattleBackground");
        HPUpdate();
        KOUpdate();
        nextActNum = 1;
        isConvertable = true;
        reviathanFiftySwitch = true;
        isPoison = new bool[3] {false,false,false};
        poisonIcon = new GameObject[3];
        poisonIcon[0] = GameObject.FindWithTag("poisonIcon1");
        poisonIcon[1] = GameObject.FindWithTag("poisonIcon2");
        poisonIcon[2] = GameObject.FindWithTag("poisonIcon3");
        poisonIcon[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(175, -300);
        poisonIcon[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(175, -300);
        poisonIcon[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(175, -300);
        ActionWindow = GameObject.FindWithTag("actionWindow");
        ActionWindowText = GameObject.FindWithTag("actionWindowText").GetComponent<Text>();
        if (ActionWindow != null)
        {
            ActionWindowRect = ActionWindow.GetComponent<RectTransform>();
        }
        ActionWindowRect.anchoredPosition = new Vector2(0, 300);
        actSpeed = 1f;
        if (enemyID == 5)
        {
            RecastTimeEnemy5 = Random.Range(1f, 7.5f);
            actSpeed = 0.7f;
        }
        if (enemyID == 6)
        {
            waterWall = GameObject.FindWithTag("waterWall");
        }
        if (enemyID == 8)
        {
            actSpeed = 0.7f;
        }
        if (enemyID == 10)
        {
            actSpeed = 0.4f;
        }
        crowEffect = (GameObject)Resources.Load("crowEffect");
        bulletEffect = (GameObject)Resources.Load("bulletEffect");
        RecastTimeEnemy7 = 5;
        bulletNUM = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActing)
        {
            ChargeTime += Time.deltaTime;
        }
        switch (enemyID)
        {
            case 3://�r�[�o�[
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver)
                {
                    ChargeTime = 0f;
                    FightAction3();
                }
                break;
            case 4://�T��
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 5f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction4();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 3f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction4();
                }
                break;
            case 5://�q�g�K�^
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= RecastTimeEnemy5 && !isDead && !bm.isGameOver)
                {
                    ChargeTime = 0f;
                    FightAction();
                }
                break;
            case 6://�T��
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 5f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction4();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 3f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction4();
                }
                break;
            case 7://�p���_
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= RecastTimeEnemy7 && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction7();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 1f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction2();
                }
                break;
            case 8://�N���E
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction5();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 3f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction5();
                }
                break;
            case 9://�X���C���O�����f
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 5f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction6();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction6();
                }
                break;
            case 10://����闳�̍�
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= RecastTimeEnemy5 && !isDead && !bm.isGameOver)
                {
                    ChargeTime = 0f;
                    FightAction();
                }
                break;
            case 11://���b�h�h���S��
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver)
                {
                    ChargeTime = 0f;
                    FightAction3();
                }
                break;
            case 12://�u���[�h���S��
                //����
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction5();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 3f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction5();
                }
                break;//�C�G���[�h���S��
            case 13:
                //��
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 5f && !isDead && !bm.isGameOver && nextActNum == 1)
                {
                    ChargeTime = 0f;
                    FightAction();
                }
                else if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 4f && !isDead && !bm.isGameOver && nextActNum == 2)
                {
                    ChargeTime = 0f;
                    FightAction6();
                }
                break;
            default:
                if (bm.BattleSwitch && !isKnockOut && ChargeTime >= 5f && !isDead && !bm.isGameOver)
                {
                    ChargeTime = 0f;
                    FightAction();
                }
                break;
        }

        if (enemyID == 6 && !isDead)//���_�̃��W�F�l�\��
        {
            regeneTime += Time.deltaTime;
            if (bm.BattleSwitch && regeneTime >= 0.8f && !isDead && HP < MaxHP && !bm.isGameOver)
            {
                regeneTime = 0f;
                HP += 10;
                if (HP >= MaxHP)
                {
                    HP = MaxHP;
                }
                HPUpdate();
                damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                damageTextObj.transform.SetParent(battleBackground.transform);
                damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                damageTextObj.GetComponent<RectTransform>().anchoredPosition = monsterRect.anchoredPosition + new Vector2(0, 20);
                damageTextObj.GetComponent<Text>().text = "10";
                damageTextObj.GetComponent<Text>().color = new Color(0.5f, 1f, 0.5f, 1f);
            }
            if (reviathanFiftySwitch && HP < MaxHP * 0.5f && !isKnockOut)
            {
                reviathanFiftySwitch = false;
                audiosource.PlayOneShot(SE8);
                //KO�Ȃ��������
                if (isKnockOut)
                {
                    KnockOut = 0;
                    KOUpdate();
                }
                StartCoroutine("KOZoom");
                //�Ԍ���𔭓�
                ActionWindowRect.anchoredPosition = new Vector2(0, 250);
                if (ActionWindowText != null)
                {
                    ActionWindowText.text = "����";
                }
                waterWall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-240, 30);
                isConvertable = false;//20�b�o�������������
            }
            if (!isConvertable && waterWallTime <= 20)
            {
                waterWallTime += Time.deltaTime;
            }
            else if (!isConvertable)
            {
                waterWall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, 30);
                isConvertable = true;
            }
        }

        if (isKnockOut && !bm.isGameOver)
        {
            KOTime += Time.deltaTime;
            if (KOTime >= 0.1f)
            {
                KOTime = 0;
                KnockOut -= 0.66f;
                KOUpdate();
            }
        }

        //�łȂ�HP���炷
        for (int i = 0; i < 3; i++)
        {
            if (isPoison[i] && !isDead)
            {
                PoisonTime += Time.deltaTime;
                if (bm.BattleSwitch && PoisonTime >= 0.4f && !isDead /*&& HP < MaxHP*/ && !bm.isGameOver)
                {
                    PoisonTime = 0f;
                    if (i == 0)
                    {
                        bm.HP -= 10;
                    }
                    else if (i == 1)
                    {
                        bm.HP2 -= 10;
                    }
                    else
                    {
                        bm.HP3 -= 10;
                    }
                    bm.playerHPUpdate();

                    damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                    damageTextObj.transform.SetParent(battleBackground.transform);
                    damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                    if (bm.fightID == i + 1)
                    {
                        damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                    }
                    else
                    {
                        damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 20);
                    }
                    damageTextObj.GetComponent<Text>().text = "" + 10;
                    damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                }
            }
        }
        
    }

    public void FightAction()//�ʏ�U��
    {
        isActing = true;
        monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), actSpeed).OnComplete(() =>
        {
            switch (bm.fightID)
            {
                case 1:
                    StartCoroutine("enemyVibration");
                    break;
                case 2:
                    StartCoroutine("enemyVibration2");
                    break;
                case 3:
                    StartCoroutine("enemyVibration3");
                    break;
                default:
                    break;
            }
            float rnd = Random.Range(0.9f, 1.1f);
            if (!bm.isGuard)
            {
                audiosource.PlayOneShot(SE1);
                damage = (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                if (damage < 0)
                {
                    damage = 0;
                }
                switch (bm.fightID)
                {
                    case 1:
                        bm.HP -= damage;
                        break;
                    case 2:
                        bm.HP2 -= damage;
                        break;
                    case 3:
                        bm.HP3 -= damage;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                audiosource.PlayOneShot(SEGuard);
            }
            bm.playerHPUpdate();
            //�G�t�F�N�g�o��
                crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                crowEffectObj.transform.SetParent(battleBackground.transform);
                crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
                crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
            crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

            damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
            damageTextObj.transform.SetParent(battleBackground.transform);
            damageTextObj.transform.localScale = new Vector3(1, 1, 1);
            damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
            damageTextObj.GetComponent<Text>().text = "" + damage;
            damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
            if (bm.isGuard)
            {
                damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
            }

            monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
            .SetDelay(0.1f).OnComplete(() => 
            {
                isActing = false;
            });

            //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
            if (bm.HP <= 0 && bm.playerSkillRemain > 0)
            {
                bm.playerSkillRemain--;
                bm.HP = 1;
                bm.playerHPUpdate();
            }

            if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
            {
                bm.isGameOver = true;
                bm.startGameOver();
                if (enemyID == 6)
                {
                    waterWall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, 30);
                }
            }
        });

        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 4:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 6:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 7:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 8:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 9:
                if (rndAct <= 40)
                {
                    nextActNum = 1;
                }
                else
                {
                    nextActNum = 2;
                }
                break;
            default:
                break;
        }

        if (enemyID == 5)
        {
            RecastTimeEnemy5 = Random.Range(1f, 7.5f);
        }
        else if (enemyID == 10)
        {
            RecastTimeEnemy5 = Random.Range(1f, 7.5f);
        }
    }

    public void FightAction2()//�X�}�b�V���u���[
    {
        isActing = true;
        ActionWindowRect.anchoredPosition = new Vector2(0,250);
        if (ActionWindowText != null)
        {
            ActionWindowText.text = "�X�}�b�V���u���[";
        }
        enemyImage.sprite = image2;
        monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), 1.0f).OnComplete(() =>
        {
            //�\���āA1�b��ɍU��
            monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), 1.0f).OnComplete(() =>
            {
                enemyImage.sprite = image3;
                switch (bm.fightID)
                {
                    case 1:
                        StartCoroutine("enemyVibration");
                        break;
                    case 2:
                        StartCoroutine("enemyVibration2");
                        break;
                    case 3:
                        StartCoroutine("enemyVibration3");
                        break;
                    default:
                        break;
                }
                float rnd = Random.Range(0.9f, 1.1f);
                if (!bm.isGuard)
                {
                    audiosource.PlayOneShot(SE1);
                    damage = 3 * (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    switch (bm.fightID)
                    {
                        case 1:
                            bm.HP -= damage;
                            break;
                        case 2:
                            bm.HP2 -= damage;
                            break;
                        case 3:
                            bm.HP3 -= damage;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    audiosource.PlayOneShot(SEGuard);
                }
                bm.playerHPUpdate();

                //�G�t�F�N�g�o��
                crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                crowEffectObj.transform.SetParent(battleBackground.transform);
                crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
                crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
                crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

                damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                damageTextObj.transform.SetParent(battleBackground.transform);
                damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                damageTextObj.GetComponent<Text>().text = "" + damage;
                damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                if (bm.isGuard)
                {
                    damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                    damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                    damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
                }

                monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
                .SetDelay(0.1f).OnComplete(() =>
                {
                    enemyImage.sprite = image1;
                    isActing = false;
                    ActionWindowRect.anchoredPosition = new Vector2(0,300);
                }) ;

                //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
                if (bm.HP <= 0 && bm.playerSkillRemain > 0)
                {
                    bm.playerSkillRemain--;
                    bm.HP = 1;
                    bm.playerHPUpdate();
                }

                if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
                {
                    bm.isGameOver = true;
                    bm.startGameOver();
                }
            });
        });

        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 7:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            default:
                break;
        }
    }

    public void FightAction3()//�r�[�o�[�̍U��
    {
        isActing = true;
        monsterImageRect.transform.DORotate(new Vector3(0, 0, 360), 1.4f).SetRelative();
        monsterRect.transform.DOLocalMove(new Vector3(100f, 150f, 0), 0.7f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), 0.7f).SetEase(Ease.InSine).OnComplete(() =>
            {
                //�����ōU������
                switch (bm.fightID)
                {
                    case 1:
                        StartCoroutine("enemyVibration");
                        break;
                    case 2:
                        StartCoroutine("enemyVibration2");
                        break;
                    case 3:
                        StartCoroutine("enemyVibration3");
                        break;
                    default:
                        break;
                }
                float rnd = Random.Range(0.9f, 1.1f);
                if (!bm.isGuard)
                {
                    audiosource.PlayOneShot(SE1);
                    damage = (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    switch (bm.fightID)
                    {
                        case 1:
                            bm.HP -= damage;
                            break;
                        case 2:
                            bm.HP2 -= damage;
                            break;
                        case 3:
                            bm.HP3 -= damage;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    audiosource.PlayOneShot(SEGuard);
                }
                bm.playerHPUpdate();

                //�G�t�F�N�g�o��
                crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                crowEffectObj.transform.SetParent(battleBackground.transform);
                crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
                crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
                crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

                damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                damageTextObj.transform.SetParent(battleBackground.transform);
                damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                damageTextObj.GetComponent<Text>().text = "" + damage;
                damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                if (bm.isGuard)
                {
                    damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                    damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                    damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
                }

                monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
                    .SetDelay(0.1f).OnComplete(() =>
                    {
                        isActing = false;
                    });

                //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
                if (bm.HP <= 0 && bm.playerSkillRemain > 0)
                {
                    bm.playerSkillRemain--;
                    bm.HP = 1;
                    bm.playerHPUpdate();
                }

                if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
                {
                    bm.isGameOver = true;
                    bm.startGameOver();
                }
            });
        });
        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 7:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            default:
                break;
        }
    }
    public void FightAction4()//���s
    {
        isActing = true;
        if (ActionWindowText != null)
        {
            ActionWindowText.text = "���s";
        }
        StartCoroutine("actionTextAnim");
        audiosource.PlayOneShot(SE2);
        tweener = enemyImage.DOFillAmount(0f, 0.7f).SetEase(Ease.OutSine);
        monsterRect.transform.DOLocalMove(new Vector3(100f, -150f, 0), 0.7f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            isHiding = true;
            monsterRect.transform.DOLocalMove(new Vector3(0f, -150f, 0), 0.7f).SetDelay(1.5f).OnComplete(() =>
            {
                audiosource.PlayOneShot(SE3);
                enemyImage.DOFillAmount(1f, 0.7f).SetEase(Ease.OutSine);
                monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), 0.7f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    isHiding = false;
                    switch (bm.fightID)
                    {
                        case 1:
                            StartCoroutine("enemyVibration");
                            break;
                        case 2:
                            StartCoroutine("enemyVibration2");
                            break;
                        case 3:
                            StartCoroutine("enemyVibration3");
                            break;
                        default:
                            break;
                    }
                    float rnd = Random.Range(0.9f, 1.1f);
                    if (!bm.isGuard)
                    {
                        audiosource.PlayOneShot(SE1);
                        damage = 2 * (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        switch (bm.fightID)
                        {
                            case 1:
                                bm.HP -= damage;
                                break;
                            case 2:
                                bm.HP2 -= damage;
                                break;
                            case 3:
                                bm.HP3 -= damage;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        audiosource.PlayOneShot(SEGuard);
                    }
                    bm.playerHPUpdate();

                    //�G�t�F�N�g�o��
                    crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    crowEffectObj.transform.SetParent(battleBackground.transform);
                    crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
                    crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
                    crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

                    damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                    damageTextObj.transform.SetParent(battleBackground.transform);
                    damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                    damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                    damageTextObj.GetComponent<Text>().text = "" + damage;
                    damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                    if (bm.isGuard)
                    {
                        damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                        damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                        damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
                    }

                    monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
                        .SetDelay(0.1f).OnComplete(() =>
                        {
                            isActing = false;
                        });

                    //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
                    if (bm.HP <= 0 && bm.playerSkillRemain > 0)
                    {
                        bm.playerSkillRemain--;
                        bm.HP = 1;
                        bm.playerHPUpdate();
                    }

                    if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
                    {
                        bm.isGameOver = true;
                        bm.startGameOver();

                        if (enemyID == 6)
                        {
                            waterWall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, 30);
                        }
                    }
                });
            });
        });
        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 4:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 6:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            default:
                break;
        }
    }

    public void FightAction5()//�W�����v
    {
        isActing = true;
        if (ActionWindowText != null)
        {
            ActionWindowText.text = "����";
        }
        StartCoroutine("actionTextAnim");
        audiosource.PlayOneShot(SE6);
        monsterRect.transform.DOLocalMove(new Vector3(100f, 350f, 0), 0.7f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            isHiding = true;
            monsterRect.transform.DOLocalMove(new Vector3(0f, 350f, 0), 0.7f).SetDelay(1.5f).OnComplete(() =>
            {
                audiosource.PlayOneShot(SE7);
                enemyImage.DOFillAmount(1f, 0.7f).SetEase(Ease.OutSine);
                monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), 0.7f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    isHiding = false;
                    //�����ōU������
                    switch (bm.fightID)
                    {
                        case 1:
                            StartCoroutine("enemyVibration");
                            break;
                        case 2:
                            StartCoroutine("enemyVibration2");
                            break;
                        case 3:
                            StartCoroutine("enemyVibration3");
                            break;
                        default:
                            break;
                    }
                    float rnd = Random.Range(0.9f, 1.1f);
                    if (!bm.isGuard)
                    {
                        audiosource.PlayOneShot(SE1);
                        damage = 2 * (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        switch (bm.fightID)
                        {
                            case 1:
                                bm.HP -= damage;
                                break;
                            case 2:
                                bm.HP2 -= damage;
                                break;
                            case 3:
                                bm.HP3 -= damage;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        audiosource.PlayOneShot(SEGuard);
                    }
                    bm.playerHPUpdate();

                    //�G�t�F�N�g�o��
                    crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    crowEffectObj.transform.SetParent(battleBackground.transform);
                    crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
                    crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
                    crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

                    damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                    damageTextObj.transform.SetParent(battleBackground.transform);
                    damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                    damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                    damageTextObj.GetComponent<Text>().text = "" + damage;
                    damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                    if (bm.isGuard)
                    {
                        damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                        damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                        damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
                    }

                    monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
                        .SetDelay(0.1f).OnComplete(() =>
                        {
                            isActing = false;
                        });

                    //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
                    if (bm.HP <= 0 && bm.playerSkillRemain > 0)
                    {
                        bm.playerSkillRemain--;
                        bm.HP = 1;
                        bm.playerHPUpdate();
                    }

                    if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
                    {
                        bm.isGameOver = true;
                        bm.startGameOver();
                    }
                });
            });
        });
        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 4:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 6:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 8:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 12:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            default:
                break;
        }
    }
    public void FightAction6()//�A�V�b�h�u���X
    {
        isActing = true;
        if (ActionWindowText != null)
        {
            ActionWindowText.text = "�A�V�b�h�u���X";
        }
        StartCoroutine("actionTextAnim");
        monsterRect.transform.DOLocalMove(new Vector3(0f, 50f, 0), actSpeed).OnComplete(() =>
        {
            //�����ōU������
            audiosource.PlayOneShot(SE9);
            switch (bm.fightID)
            {
                case 1:
                    StartCoroutine("enemyVibration");
                    //�ŃA�C�R����Active�ɂ���
                    poisonIcon[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(175,60);
                    isPoison[0] = true;
                    break;
                case 2:
                    StartCoroutine("enemyVibration2");
                    poisonIcon[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(175, 60);
                    isPoison[1] = true;
                    break;
                case 3:
                    StartCoroutine("enemyVibration3");
                    poisonIcon[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(175, 60);
                    isPoison[2] = true;
                    break;
                default:
                    break;
            }

            //�G�t�F�N�g�o��
            crowEffectObj = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
            crowEffectObj.transform.SetParent(battleBackground.transform);
            crowEffectObj.transform.localScale = new Vector3(1, 1, 1);
            crowEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
            crowEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

            damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
            damageTextObj.transform.SetParent(battleBackground.transform);
            damageTextObj.transform.localScale = new Vector3(1, 1, 1);
            damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
            damageTextObj.GetComponent<Text>().text = "�ł����I";
            damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 1f, 1f);

            monsterRect.transform.DOLocalMove(locationWaitDO, 0.4f)
            .SetDelay(0.1f).OnComplete(() =>
            {
                isActing = false;
            });

            //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
            if (bm.HP <= 0 && bm.playerSkillRemain > 0)
            {
                bm.playerSkillRemain--;
                bm.HP = 1;
                bm.playerHPUpdate();
            }

            if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
            {
                bm.isGameOver = true;
                bm.startGameOver();
            }
        });

        int rndAct = Random.Range(1, 101);
        switch (enemyID)
        {
            case 4:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 6:
                if (rndAct <= 50)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 8:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 9:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            case 13:
                if (rndAct <= 40)
                {
                    nextActNum = 2;
                }
                else
                {
                    nextActNum = 1;
                }
                break;
            default:
                break;
        }
    }

    public void FightAction7()//�e��
    {
        isActing = true;
        if (bulletNUM != 1 && bulletNUM != 11)
        {
            monsterRect.transform.DOLocalMove(locationWaitDO, 0.3f).OnComplete(() =>
            {
                enemyImage.sprite = image4;
                //�����ōU������
                switch (bm.fightID)
                {
                    case 1:
                        StartCoroutine("enemyVibration");
                        break;
                    case 2:
                        StartCoroutine("enemyVibration2");
                        break;
                    case 3:
                        StartCoroutine("enemyVibration3");
                        break;
                    default:
                        break;
                }
                float rnd = Random.Range(0.9f, 1.1f);
                if (!bm.isGuard)
                {
                    audiosource.PlayOneShot(SE5);
                    damage = (int)Mathf.Round(ATK * rnd - (float)bm.DEF);
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    switch (bm.fightID)
                    {
                        case 1:
                            bm.HP -= damage;
                            break;
                        case 2:
                            bm.HP2 -= damage;
                            break;
                        case 3:
                            bm.HP3 -= damage;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    audiosource.PlayOneShot(SEGuard);
                }
                bm.playerHPUpdate();
                //�G�t�F�N�g�o��
                bulletEffectObj = Instantiate(bulletEffect, new Vector3(1, 0, 0), Quaternion.identity);
                bulletEffectObj.transform.SetParent(battleBackground.transform);
                bulletEffectObj.transform.localScale = new Vector3(1, 1, 1);
                bulletEffectObj.GetComponent<RectTransform>().anchoredPosition = enemyRect.anchoredPosition;// + new Vector2(0, 20);
                bulletEffectObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);

                damageTextObj = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                damageTextObj.transform.SetParent(battleBackground.transform);
                damageTextObj.transform.localScale = new Vector3(1, 1, 1);
                damageTextObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 20);
                damageTextObj.GetComponent<Text>().text = "" + damage;
                damageTextObj.GetComponent<Text>().color = new Color(1f, 0.8f, 0.8f, 1f);
                if (bm.isGuard)
                {
                    damageTextObj.transform.localScale = new Vector3(2, 2, 1);
                    damageTextObj.GetComponent<Text>().text = "�K�[�h�I";
                    damageTextObj.GetComponent<Text>().color = new Color(0.5f, 0.5f, 1f, 1f);
                }

                monsterRect.transform.DOLocalMove(locationWaitDO, 0.1f)
            .SetDelay(0.1f).OnComplete(() =>
            {
                enemyImage.sprite = image5;
                RecastTimeEnemy7 = 0.2f;
                bulletNUM++;
                isActing = false;
            });

                //�����v���C���[HP���O�ŃX�L�������񐔂��c���Ă�����
                if (bm.HP <= 0 && bm.playerSkillRemain > 0)
                {
                    bm.playerSkillRemain--;
                    bm.HP = 1;
                    bm.playerHPUpdate();
                }

                if (bm.HP <= 0 || bm.HP2 <= 0 || bm.HP3 <= 0)
                {
                    bm.isGameOver = true;
                    bm.startGameOver();
                }
            });
        }
        else if (bulletNUM == 1)
        {
            enemyImage.sprite = image5;
            //�e���\���鉹
            audiosource.PlayOneShot(SE4);
            monsterRect.transform.DOLocalMove(locationWaitDO - new Vector3(50, 0,0), 0.3f).OnComplete(() =>
             {
                 monsterRect.transform.DOLocalMove(locationWaitDO + new Vector3(50, 0,0), 0.3f).OnComplete(() =>
                 {
                     monsterRect.transform.DOLocalMove(locationWaitDO, 0.3f);
                     RecastTimeEnemy7 = 1.5f;
                     bulletNUM++;
                     isActing = false;
                 });
             });
        }
        else if (bulletNUM == 11)
        {
            enemyImage.sprite = image1;
            //�e���~�낷��
            audiosource.PlayOneShot(SE4);
            monsterRect.transform.DOLocalMove(locationWaitDO - new Vector3(50, 0,0), 0.3f).OnComplete(() =>
            {
                monsterRect.transform.DOLocalMove(locationWaitDO + new Vector3(50,0, 0), 0.3f).OnComplete(() =>
                {
                    monsterRect.transform.DOLocalMove(locationWaitDO, 0.3f).OnComplete(() =>
                    {
                        bulletNUM = 1;
                        RecastTimeEnemy7 = 5;
                        isActing = false;
                    });
                });
            });
        }

        switch (enemyID)
        {
            case 7:
                if (bulletNUM == 11)
                {

                }
                break;
            default:
                break;
        }
    }

    public void HPUpdate()
    {
        HPSlider.value = HP / MaxHP;
        HPText.text = HP + "/" + MaxHP;
        if (HP <= 0)
        {
            isDead = true;
            transform.DOPause();
            StartCoroutine("enemyFade");
        }
    }
    public void KOUpdate()
    {
        KOSlider.value = KnockOut / 100f;
        if (KnockOut > 100)
        {
            KnockOut = 100;
        }

        if (KnockOut >= 100 && !isKnockOut)
        {
            isKnockOut = true;
            DEF = DEFKO;
            KOImage.SetActive(true);
            StartCoroutine("SlipKO");
            StartCoroutine("KOZoom");
            audiosource.PlayOneShot(SEKO);
            transform.DOPause();
            monsterRect.transform.DOLocalMove(new Vector3(100f, 50f, 0), 1.0f);
        }
        if (KnockOut < 0 && isKnockOut)
        {
            KnockOut = 0;
            isKnockOut = false;
            DEF = DEFNotKO;
            KOImage.SetActive(false);
            ChargeTime = 0f;
            StartCoroutine("StandKO");
        }
        KOText.text = Mathf.Round(KnockOut) + "%";
        if (KnockOut >= 99 && KnockOut < 100)
        {
            KOText.text = "99%";
        }
    }

    IEnumerator enemyVibration()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                enemyRect.anchoredPosition += new Vector2(5, 0);
            }
            else
            {
                enemyRect.anchoredPosition -= new Vector2(5, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator enemyVibration2()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                enemyRect2.anchoredPosition += new Vector2(5, 0);
            }
            else
            {
                enemyRect2.anchoredPosition -= new Vector2(5, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator enemyVibration3()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0)
            {
                enemyRect3.anchoredPosition += new Vector2(5, 0);
            }
            else
            {
                enemyRect3.anchoredPosition -= new Vector2(5, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator enemyFade()
    {
        if (waterWall != null)
        {
            waterWall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, 30);
        }
        BackGroundUI.SetActive(false);

        Image img = GetComponent<Image>();
        for (int i = 1; i < 51; i++)
        {
            enemyImage.color = new Color(1, 1 - (i / 50f), 1 - (i / 50f), 1);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 1; i < 51; i++)
        {
            enemyImage.color = new Color(1, 0, 0, 1 - (i / 50f));
            yield return new WaitForSeconds(0.01f);
        }
        ActionWindow.SetActive(true);
    }

    IEnumerator SlipKO()
    {
        tweener.Kill();
        enemyImage.fillAmount = 1;
        Debug.Log("�����œ|���");
        if (enemyID == 7)
        {
            enemyImage.sprite = image1;
        }
        for (int i = 1; i < 11; i++)
        {
            switch (enemyID)
            {
                case 0:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 1:
                    enemyImageRect.anchoredPosition = new Vector2(8f * i, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 9);
                    break;
                case 2://-50,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -15f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 3://-50,-100
                    enemyImageRect.anchoredPosition = new Vector2(4f * i, -11f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 9);
                    break;
                case 4://40,-50
                    enemyImageRect.anchoredPosition = new Vector2(4f * i, -5f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 9);
                    break;
                case 5://30,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 6://30,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -5f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 7://30,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 8://30,-100
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 9://30,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -15f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                case 10://30,-100
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
                default:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), 18);
                    break;
            }
            yield return new WaitForSeconds(0.04f);
        }
        audiosource.PlayOneShot(SESlip);
    }
    IEnumerator StandKO()
    {
        ActionWindow.SetActive(true);
        Debug.Log("�����ŋN���オ��");
        for (int i = 10; i > 0; i--)
        {
            switch (enemyID)
            {
                case 0:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -9);
                    break;
                case 1:
                    enemyImageRect.anchoredPosition = new Vector2(8f * i, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -9);
                    break;
                case 2://-50,-100
                    enemyImageRect.anchoredPosition = new Vector2(0, -15f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 3://-50,-100
                    enemyImageRect.anchoredPosition = new Vector2(4f * i, -11f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -9);
                    break;
                case 4:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 5:
                    enemyImageRect.anchoredPosition = new Vector2(0, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 6:
                    enemyImageRect.anchoredPosition = new Vector2(0, -5f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 7:
                    enemyImageRect.anchoredPosition = new Vector2(0, -8f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 8:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 9:
                    enemyImageRect.anchoredPosition = new Vector2(0, -15f * i);
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 10:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 11:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                case 12:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
                default:
                    enemyImageRect.Rotate(new Vector3(0, 0, 1), -18);
                    break;
            }
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator KOZoom()//�������ɂ��ēG�ɃY�[������
    {
        RectTransform BBGRect = GameObject.Find("BattleBackground").GetComponent<RectTransform>();
        for (int i = 1; i < 6; i++)
        {
            BBGRect.anchoredPosition = new Vector2(locationWait.x * i / -5, locationWait.y * i / -5);
            BBGRect.localScale = new Vector3(1 + (0.05f * i), 1 + (0.05f * i),1);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 1; i < 6; i++)
        {
            BBGRect.anchoredPosition = new Vector2(locationWait.x * (5 - i) / -5, locationWait.y * (5 - i) / -5);
            BBGRect.localScale = new Vector3(1 + (0.05f * (5 - i)), 1 + (0.05f * (5 - i)), 1);
            yield return new WaitForSeconds(0.05f);
        }
        BBGRect.anchoredPosition = new Vector2(0, 0);
        BBGRect.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator ReviathanAct()//�������ɂ��ēG�ɃY�[������
    {
        RectTransform BBGRect = GameObject.Find("BattleBackground").GetComponent<RectTransform>();
        for (int i = 1; i < 6; i++)
        {
            BBGRect.anchoredPosition = new Vector2(locationWait.x * i / -5, locationWait.y * i / -5);
            BBGRect.localScale = new Vector3(1 + (0.05f * i), 1 + (0.05f * i), 1);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 1; i < 6; i++)
        {
            BBGRect.anchoredPosition = new Vector2(locationWait.x * (5 - i) / -5, locationWait.y * (5 - i) / -5);
            BBGRect.localScale = new Vector3(1 + (0.05f * (5 - i)), 1 + (0.05f * (5 - i)), 1);
            yield return new WaitForSeconds(0.05f);
        }
        BBGRect.anchoredPosition = new Vector2(0, 0);
        BBGRect.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator actionTextAnim()
    {

        Image ActionWindowImage = ActionWindow.GetComponent<Image>();
        ActionWindowRect.anchoredPosition = new Vector2(50, 250);
        ActionWindowImage.color = new Color(1, 1, 1, 1);
        ActionWindowText.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        ActionWindowRect.anchoredPosition = new Vector2(0, 250);
        yield return new WaitForSeconds(4f);
        for (int i = 1; i < 11; i++)
        {
            ActionWindowRect.anchoredPosition = new Vector2(i * -5, 250);
            ActionWindowImage.color = new Color(1, 1, 1, 1 - i * 0.1f);
            ActionWindowText.color = new Color(1, 1, 1, 1 - i * 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
