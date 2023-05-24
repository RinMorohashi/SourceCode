using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour
{
    /// <summary>
    /// ���j���[��ʑS�̂̓���i�x�e�A���x���A�b�v�A�^�C�g���ɖ߂�j���s���X�N���v�g
    /// </summary>
    public int days;
    [Header("�����\��")] public Text daysDisplay;
    public bool recast;
    public bool Untouchable = false;//���o�̊Ԃ̓I���ɂ��āA�I�t�ɂȂ����瑀��\�ɂȂ�
    public Image blackBG;
    public GameObject cvs;//�L�����o�X
    [Header("�t�B�[���h��̃v���C���[�ʒu")] public RectTransform playerRec;
    public GameObject playerObj;
    public int playerLine = 1;
    public int unitID;
    public bool isGameOver;
    public bool isBattle;
    public bool isField;
    public bool isStrengthening;

    [Header("�L�����o�X�P�w�i�摜")] public RectTransform Backgroundcvs1;
    [Header("�L�����o�X�P�w�i�摜2")] public RectTransform Backgroundcvs2;
    [Header("�L�����o�X�P�w�i�I�u�W�F�N�g")] public GameObject Backgroundobj1;
    [Header("�L�����o�X�P�w�i�I�u�W�F�N�g2")] public GameObject Backgroundobj2;

    public GameObject unitParent;
    public GameObject popUpText;
    public int money;
    public Text moneyText;
    public Text Lv_monster1_Field;
    public Text EXP_monster1_Field;
    public Text Lv_monster2_Field;
    public Text EXP_monster2_Field;
    public Text Lv_monster3_Field;
    public Text EXP_monster3_Field;
    public Text Lv_player_Field;
    public Text EXP_player_Field;
    public GameManager gm;
    public BattleManager bm;
    public Slider HPSlider;
    public Text HPText;
    public Slider HPSlider2;
    public Text HPText2;
    public Slider HPSlider3;
    public Text HPText3;
    public RectTransform AltarBoardRec;
    public GameObject CursorAltar;
    public RectTransform CursorAltarRec;
    public int CursorAltarLocation;
    public int StrengthenNum = 1;//�O�Ȃ�g���[�i�[���A�P�Ȃ烌�b�h�h���S��������ʂ�\������B
    public GameObject[] FaceImageAltar = new GameObject[2];
    public Text NameAltar;
    public Text TextATKAltar;
    public Text TextSPDAltar;
    public Text TextAbilityAltar;
    public Text TextAbilityNameAltar;
    public Text TextAbilityExplanationAltar;
    public Text TextEXPAltar;
    public GameObject EXPShortageImage;
    public GameObject[] ATKLvImage = new GameObject[10];
    public GameObject[] SPDLvImage = new GameObject[10];
    public GameObject[] AbilityLvImage = new GameObject[10];

    public int NumOfParty;//���������X�^�[�̐�
    public bool DoYouHaveDragon;
    public bool DoYouHaveShisa;//�V�[�T�[�������Ă��邩
    public bool DoYouHaveReviathan;//�����@�C�A�T���������Ă��邩
    //�������ɏ��������X�^�[�̏������Ă���
    public int MonsterID1;//���ԃ����X�^�[�̏��
    public int ATK1;//�U����
    public int SPD1;//�f����
    public int ElementID1;//�^�C�v
    public int EXP1;//�����X�^�[�P�̌o���l
    public int ATKLv;//�U���̃��x���i�P�����邲�Ƃ�ATK�̒l��50�グ��j
    public int SPDLv;
    public int AbilityLv;

    public int MonsterID2;//���ԃ����X�^�[�Q�̏��
    public int ATK2;//�U����
    public int SPD2;//�f����
    public int ElementID2;//�^�C�v
    public int EXP2;//�����X�^�[2�̌o���l
    public int ATK2Lv;//�U���̃��x���i�P�����邲�Ƃ�ATK�̒l��50�グ��j
    public int SPD2Lv;
    public int Ability2Lv;

    public int MonsterID3;//���ԃ����X�^�[�R�̏��
    public int ATK3;//�U����
    public int SPD3;//�f����
    public int ElementID3;//�^�C�v
    public int EXP3;//�����X�^�[2�̌o���l
    public int ATK3Lv;//�U���̃��x���i�P�����邲�Ƃ�ATK�̒l��50�グ��j
    public int SPD3Lv;
    public int Ability3Lv;

    public int MaxHP;//�v���C���[��HP
    public int MaxHP2;//�h���S����HP
    public int MaxHP3;//����HP
    public int DEF;//DEF
    public int Element0ID;//�^�C�v
    public int EXP0;//�v���C���[�̌o���l
    public int MaxHPLv;//�U���̃��x���i�P�����邲�Ƃ�ATK�̒l��50�グ��j
    public int DEFLv;
    public int ATK0;//�U����
    public int SPD0;//�f����
    public int ATK0Lv;//�U���̃��x���i�P�����邲�Ƃ�ATK�̒l��50�グ��j
    public int SPD0Lv;
    public int Ability0Lv;

    public GameObject ExplanationTextField;
    public Animator playerAnimField;
    public AudioSource audioSource;
    public AudioClip SE1;//�J�[�\�����������Ƃ��̉�
    public AudioClip SE2;//�������������Ƃ��̉�
    public AudioClip SE3;//���j���[���J�����Ƃ��̉�
    public AudioClip SE4;//����������Ȃ������Ƃ��̉�
    public AudioClip SE5;//�x�e�����Ƃ��̉�
    public AudioClip SE6;//�G�ƃG���J�E���g�����Ƃ��̉�
    public RectTransform BlackImageRect;//��ʂ��Â����鎞�ɕ\������
    public int[,] a;//a = new int[3,4] {{MaxHP,ATKLv},{DEFLv + 10,SPDLv + 10},{Ability0Lv + 20, AbilityLv + 20}};
    public Image[] LvImage;//LvImage[a[]].color = new Color;
    public GameObject StUpText;
    public RectTransform MonsterStrengthenRt;
    public CanvasGroup MonsterStrengthenCanvasGroup;

    public GameObject statusImage2;
    public GameObject statusImage3;//�t�B�[���h�ł̃T�����C�h�b�O�̃X�e�[�^�X�I�u�W�F�N�g
    public GameObject statusImage4;

    public int LvOfForest;
    public int LvOfSea;
    public int LvOfCity;
    public Text LvTextForest;
    public Text LvTextSea;
    public Text LvTextCity;

    public GameObject ItemShortageObj;

    public RectTransform cursorFieldRec;
    public GameObject ImageForest;
    public GameObject ImageSea;
    public GameObject ImageRuin;
    public GameObject ImageRest;
    public int cursorNumField;
    
    public int stageLevel;//�X�e�[�W���N���A������P�グ��B�R�Ȃ�X�e�[�W�R�܂ŁA�T�Ȃ�X�e�[�W�T�܂ŕ\������B
    public int stageLevelCur;//���ݑI������Ă���X�e�[�W�ԍ�
    public GameObject[] stageButton;
    public RectTransform restButtonRect;
    public RectTransform stageButtonParentRect;
    public GameObject fieldParentLeft;
    public GameObject fieldParentRight;
    public GameObject cursorFieldObj;

    public StageManager sm;
    public bool isStrengtheningSub;
    public int cursorNumFieldSub;
    public Image cursorFieldImage;
    public Image cursorStrengthenImage;
    public Text StUpTextImage;

    public RectTransform cursorAltarSelectRect;
    public GameObject[] characterImage;
    public Text ATKCostText;
    public Text SPDCostText;
    public Text AbilityCostText;
    public Text ATKCostText2;
    public Text SPDCostText2;
    public Text AbilityCostText2;


    public GameObject menuCommandExplanationTextBG;//���j���[�J�����Ƃ���true,�����Ƃ���false�ɂ���
    public Text menuCommandExplanationText;
    public int MaxHPBasisPlayer;
    public int MaxHPBasisDragon;
    public int MaxHPBasisDog;

    public bool isSave;
    public bool isBillboard;
    public int billboardNum;
    public GameObject SaveWindow;
    public eventTriggerManager eTM;
    public GameObject[] saveflagWindow;

    public bool[] isBossDefeated;
    public GameObject ImageDoragonStage;
    public GameObject ImageDogStage;
    public GameObject eventTriggerReviathan;

    public villagerManager[] villager;
    public GameObject[] billboardWindow;

    public float entirePlayTime;//���v���C����
    public int entireDefeatNum;//��������
    public int entireCommandUseOne;//�����������g�p��
    public int entireCommandUseTwo;//���܂���g�p��
    public int entireCommandUseThree;//���݂˂����g�p��
    public int entireCommandUseFour;//�����g�p��

    void Start()
    {
        popUpText = (GameObject)Resources.Load("popUpText");
    }
    
    void Update()
    {
        if (!Untouchable && !isGameOver && isField)
        {
            if (!isBattle)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && !recast)
                {
                    if (isStrengthening)//�������Ȃ�
                    {
                        if (CursorAltarLocation <= 2)
                        {
                            CursorAltarLocation++;
                        }
                        CursorAltarLocating();

                        switch (StrengthenNum)
                        {
                            case 0://�_���͎����ł���Ă����
                                ATKLvImageUpdate(MaxHPLv);
                                SPDLvImageUpdate(DEFLv);
                                AbilityLvImageUpdate(Ability0Lv);
                                break;
                            case 1:
                                ATKLvImageUpdate(ATKLv);
                                SPDLvImageUpdate(SPDLv);
                                AbilityLvImageUpdate(AbilityLv);
                                break;
                            case 2:
                                ATKLvImageUpdate(ATK2Lv);
                                SPDLvImageUpdate(SPD2Lv);
                                AbilityLvImageUpdate(Ability2Lv);
                                break;
                            case 3:
                                ATKLvImageUpdate(ATK3Lv);
                                SPDLvImageUpdate(SPD3Lv);
                                AbilityLvImageUpdate(Ability3Lv);
                                break;
                            default:
                                break;
                        }
                        LvImageReset();
                        LvTextReset();
                        audioSource.PlayOneShot(SE1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !recast)
                {
                    if (isStrengthening)
                    {
                        if (CursorAltarLocation >= 2)
                        {
                            CursorAltarLocation--;
                        }
                        CursorAltarLocating();

                        switch (StrengthenNum)
                        {
                            case 0://�_���͎����ł���Ă����
                                ATKLvImageUpdate(MaxHPLv);
                                SPDLvImageUpdate(DEFLv);
                                AbilityLvImageUpdate(Ability0Lv);
                                break;
                            case 1:
                                ATKLvImageUpdate(ATKLv);
                                SPDLvImageUpdate(SPDLv);
                                AbilityLvImageUpdate(AbilityLv);
                                break;
                            case 2:
                                ATKLvImageUpdate(ATK2Lv);
                                SPDLvImageUpdate(SPD2Lv);
                                AbilityLvImageUpdate(Ability2Lv);
                                break;
                            case 3:
                                ATKLvImageUpdate(ATK3Lv);
                                SPDLvImageUpdate(SPD3Lv);
                                AbilityLvImageUpdate(Ability3Lv);
                                break;
                            default:
                                break;
                        }
                        LvImageReset();
                        LvTextReset();
                        audioSource.PlayOneShot(SE1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) && !recast)
                {
                    if (isStrengtheningSub)//�����I�𒆂Ȃ�
                    {
                        if (cursorNumFieldSub >= 2)
                        {
                            cursorNumFieldSub--;
                        }
                        CursorAltarLocatingSub();
                    }
                    else//����ȊO�Ȃ�
                    {
                        if (cursorNumField > 1)
                        {
                            cursorNumField--;
                            cursorRectUpdateField();
                        }
                        if (cursorNumField >= 4)
                        {
                            stageButtonParentRect.anchoredPosition = new Vector2(0, (cursorNumField - 3) * 70);
                        }
                        else
                        {
                            stageButtonParentRect.anchoredPosition = new Vector2(0, 0);
                        }
                    }
                    audioSource.PlayOneShot(SE1);
                    if (!recast)
                    {
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && !recast)
                {
                    
                    if (isStrengtheningSub)
                    {
                        if (cursorNumFieldSub < NumOfParty)
                        {
                            cursorNumFieldSub++;
                        }
                        CursorAltarLocatingSub();
                    }
                    else
                    {
                        if (cursorNumField < 5)//cursorNumField < stageLevel + 1)
                        {
                            cursorNumField++;
                            cursorRectUpdateField();
                        }
                        else if ((stageLevel == 3 || stageLevel == 6) && (cursorNumField < stageLevel + 2))
                        {
                            cursorNumField++;
                            cursorRectUpdateField();
                        }
                        if (cursorNumField >= 4)
                        {
                            stageButtonParentRect.anchoredPosition = new Vector2(0, (cursorNumField - 3) * 70);
                        }
                        else
                        {
                            stageButtonParentRect.anchoredPosition = new Vector2(0, 0);
                        }
                    }
                    audioSource.PlayOneShot(SE1);
                    if (!recast)
                    {
                    }
                }
                if (Input.GetKeyDown(KeyCode.Z) && !recast && isField)
                {
                    if (isStrengthening)
                    {
                        //�������m�肷��R�[�h
                        strengthenEXE();
                        if (!recast)
                        {
                        }
                    }
                    else if (!isStrengthening && isStrengtheningSub)//������ʂɓ���Ƃ�
                    {/*cursorNumFieldSub�ŋ����Ώۂ�I�΂���*/
                        StartCoroutine("StrengthenInIn");

                        if (!recast)
                        {
                        }
                    }
                    if (!isStrengtheningSub)
                    {
                        switch (cursorNumField)
                        {
                            case 1://��
                                StartCoroutine("RestHPRecover");
                                Debug.Log("�񕜂��܂�");
                                break;
                            case 2://����
                                   //�N���������邩�I�ԉ�ʂ��o��
                                cursorFieldObj.transform.SetParent(fieldParentRight.transform);
                                cursorFieldRec.anchoredPosition = new Vector2(-40, 120);
                                cursorFieldRec.sizeDelta = new Vector2(80, 100);
                                cursorNumFieldSub = 1;
                                isStrengtheningSub = true;
                                Debug.Log("�����ΏۑI����ʂ��o���܂�");
                                break;
                            case 3://���x�����Z�b�g
                                //���x���̒l���������f�ނ̐����t�Z���A�v���X����
                                LvOfForest += 5 * ATK0Lv * (ATK0Lv + 1) / 2;
                                LvOfForest += 5 * ATKLv * (ATKLv + 1) / 2;
                                LvOfForest += 5 * ATK2Lv * (ATK2Lv + 1) / 2;
                                LvOfSea += 5 * SPD0Lv * (SPD0Lv + 1) / 2;
                                LvOfSea += 5 * SPDLv * (SPDLv + 1) / 2;
                                LvOfSea += 5 * SPD2Lv * (SPD2Lv + 1) / 2;
                                LvOfCity += 5 * Ability0Lv * (Ability0Lv + 1) / 2;
                                LvOfCity += 5 * AbilityLv * (AbilityLv + 1) / 2;
                                LvOfCity += 5 * Ability2Lv * (Ability2Lv + 1) / 2;
                                EXP0 += 5 * ATK0Lv * (ATK0Lv + 1) / 2;
                                EXP0 += 5 * SPD0Lv * (SPD0Lv + 1) / 2;
                                EXP0 += 5 * Ability0Lv * (Ability0Lv + 1) / 2;
                                EXP1 += 5 * ATKLv * (ATKLv + 1) / 2;
                                EXP1 += 5 * SPDLv * (SPDLv + 1) / 2;
                                EXP1 += 5 * AbilityLv * (AbilityLv + 1) / 2;
                                EXP2 += 5 * ATK2Lv * (ATK2Lv + 1) / 2;
                                EXP2 += 5 * SPD2Lv * (SPD2Lv + 1) / 2;
                                EXP2 += 5 * Ability2Lv * (Ability2Lv + 1) / 2;
                                //���x���̒l��0�ɂ��ǂ�
                                ATK0Lv = 0;
                                ATKLv = 0;
                                ATK2Lv = 0;
                                SPD0Lv = 0;
                                SPDLv = 0;
                                SPD2Lv = 0;
                                Ability0Lv = 0;
                                AbilityLv = 0;
                                Ability2Lv = 0;
                                //���l�̕\�����X�V
                                TextUpdateAfterStrengthen();
                                StatusTextUpdate_Field();
                                Debug.Log("�L�����̋��������Z�b�g���܂�");
                                break;
                            case 4://�A�`�[�u�����g�i�������j
                                /*
                                isSave = true;
                                //SaveWindow.SetActive(true);
                                saveStart();
                                Debug.Log("�Q�[���̐i�s�󋵂��L�^���܂�");
                                */
                                break;
                            case 5://�^�C�g���ɖ߂�
                                isField = false;
                                sm.isStageScene = false;
                                gm.SceneNum = 2;
                                gm.StartCoroutine("fadeOut");
                                Debug.Log("�A�`�[�u�����g��\�����܂�");
                                break;
                            default:
                                break;
                        }
                    }
                    audioSource.PlayOneShot(SE1);
                    if (!recast)
                    {
                        recast = true;
                        StartCoroutine("RecastShort");
                    }
                }
                if (Input.GetKeyDown(KeyCode.X) && !recast && isField)
                {
                    if (isStrengthening)
                    {
                        isStrengthening = false;
                        StartCoroutine("StrengthenOut");
                        audioSource.PlayOneShot(SE3);
                    }
                    else if (isStrengtheningSub)
                    {
                        StartCoroutine("isStrengtheningSubFalsing");
                        cursorFieldObj.transform.SetParent(fieldParentLeft.transform);
                        cursorFieldRec.anchoredPosition = new Vector2(-350, 0);
                        cursorFieldRec.sizeDelta = new Vector2(210, 60);
                        cursorNumField = 2;
                        cursorRectUpdateField();
                    }
                    audioSource.PlayOneShot(SE4);
                    if (!recast)
                    {
                        recast = true;
                        StartCoroutine("RecastShort");
                    }
                }
            }
        }

        entirePlayTime += Time.deltaTime;
    }

    public void cursorRectUpdateField()
    {
        ImageRest.SetActive(false);
        switch (cursorNumField)
        {
            case 1:
                OnEnterDemo1();
                menuCommandExplanationText.text = "���������HP���񕜂��܂��B";
                break;
            case 2:
                OnEnterDemo2();
                menuCommandExplanationText.text = "�A�C�e���������\n�L�������������܂��B";
                break;
            case 3:
                OnEnterDemo3();
                menuCommandExplanationText.text = "�L�����̃��x�������Z�b�g\n���܂��B�����Ɏg����\n�f�ށEEXP�����ɖ߂�܂��B";
                break;
            case 4:
                OnEnterDemo4();
                menuCommandExplanationText.text = "�ߓ������\��I";
                break;
            case 5:
                OnEnterDemo5();
                menuCommandExplanationText.text = "�Z�[�u��Y�ꂸ�ɁI";
                break;
            case 6:
                OnEnterDemo6();
                break;
            case 7:
                OnEnterDemo7();
                break;
            case 8:
                OnEnterDemo8();
                break;
            case 9:
                OnEnterDemo9();
                break;
            default:
                break;
        }
        if (cursorNumField == stageLevel + 1)
        {
            ImageRest.SetActive(true);
        }
        audioSource.PlayOneShot(SE1);

        if (cursorNumField >= 4)
        {
            stageButtonParentRect.anchoredPosition = new Vector2(0, (cursorNumField - 3) * 70);
        }
        else
        {
            stageButtonParentRect.anchoredPosition = new Vector2(0, 0);
        }
    }
    
    void FixedUpdate()
    {
        float clr = 0.7f + 0.3f * Mathf.Sin(3 * Time.time);
        if (isStrengthening)
        {
            cursorAltarSelectRect.anchoredPosition = CursorAltarRec.anchoredPosition + new Vector2(-120, 0);

            cursorStrengthenImage.color = new Color(1, 1, 0, clr - 0.3f);
            StUpTextImage.color = new Color(1, 1, 0, clr);
        }
        if (isField)
        {
            Debug.Log("isStrengtheningSub��" + isStrengtheningSub + "�A�@isStrengthening��" + isStrengthening + "�A�@recast��" + recast);
            Debug.Log("cursorNumField��" + cursorNumField + "�AcursorNumFieldSub��" + cursorNumFieldSub);
            cursorFieldImage.color = new Color(1, 1, 1, clr);

        }
    }

    public void FieldBeggining()
    {
        LvTextForest.text = "�~ " + LvOfForest;
        LvTextSea.text = "�~ " + LvOfSea;
        LvTextCity.text = "�~ " + LvOfCity;
        //�p�[�e�B�̐����Q�Ƃ��ă����X�^�[�̉摜��\������
        switch (NumOfParty)
        {
            case 1:
                statusImage2.SetActive(false);
                statusImage3.SetActive(false);
                statusImage4.SetActive(false);
                break;
            case 2:
                statusImage2.SetActive(true);
                statusImage3.SetActive(false);
                statusImage4.SetActive(false);
                break;
            case 3:
                statusImage2.SetActive(true);
                statusImage3.SetActive(true);
                statusImage4.SetActive(false);
                break;
            case 4:
                statusImage2.SetActive(true);
                statusImage3.SetActive(true);
                statusImage4.SetActive(true);
                break;
            default:
                break;
        }
        //�X�e�[�W���x�����Q�Ƃ��ăX�e�[�W�̑I������\��
        for (int i = 0; i < stageLevel; i++)
        {
            stageButton[i].SetActive(true);
        }
        //��ԉ��ɋx���R�}���h��\��
        restButtonRect.anchoredPosition = new Vector2(-350, stageLevel* -70 + 70);
        if (stageLevel == 3)
        {
            stageButton[3].SetActive(true);
            restButtonRect.anchoredPosition = new Vector2(-350, stageLevel * -70);
        }
        if (stageLevel == 6)
        {
            stageButton[6].SetActive(true);
            restButtonRect.anchoredPosition = new Vector2(-350, stageLevel * -70);
        }
        //���̐��l���X�V
        moneyText.text = "" + money;

        cursorNumField = 1;
        cursorRectUpdateField();
        menuCommandExplanationTextBG.SetActive(true);
    }

    IEnumerator Recast()
    {
        yield return new WaitForSeconds(0.50f);
        recast = false;
    }

    IEnumerator RecastLong()
    {
        yield return new WaitForSeconds(1.5f);
        recast = false;
    }

    IEnumerator RecastShort()
    {
        yield return new WaitForSeconds(0.3f);
        recast = false;
    }

    public void recastNormal()
    {
        StartCoroutine("Recast");
    }

    public void recastLong()
    {
        StartCoroutine("RecastLong");
    }

    IEnumerator BackgroundScroll()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Backgroundcvs1.anchoredPosition += new Vector2(-4, 0);
            Backgroundcvs2.anchoredPosition += new Vector2(-4, 0);

        }
        if (Backgroundcvs1.anchoredPosition.x <= -1270)
        {
            Backgroundcvs1.anchoredPosition = Backgroundcvs2.anchoredPosition + new Vector2(1527, 0);
        }
        if (Backgroundcvs2.anchoredPosition.x <= -1270)
        {
            Backgroundcvs2.anchoredPosition = Backgroundcvs1.anchoredPosition + new Vector2(1527, 0);
        }
    }

    IEnumerator blackOut()
    {
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, i * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
    }
    public void OnClickBackButton()
    {
        StartCoroutine("blackOut2");
    }
    IEnumerator blackOut2()
    {
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, i * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void moneyUpdate()
    {
        moneyText.text = "�������F" + money + " ��";
    }

    IEnumerator StrengthenIn()
    {
        TextUpdateAfterStrengthen();
        ExplanationTextField.SetActive(false);
        for (int i = 1; i < 21; i++)
        {
            AltarBoardRec.anchoredPosition = new Vector3(960 - (i * 48), 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator StrengthenOut()
    {
        StatusTextUpdate_Field();
        for (int i = 1; i < 25; i++)
        {
            AltarBoardRec.anchoredPosition = new Vector3((i * 48), 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        ExplanationTextField.SetActive(true);
        BlackImageRect.anchoredPosition = new Vector2(0, 540);
    }

    IEnumerator EXPShortage()
    {
        EXPShortageImage.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        EXPShortageImage.SetActive(false);
    }

    public void CursorAltarLocating()//������ʂ̃J�[�\���ʒu���X�V����֐�
    {
        switch (CursorAltarLocation)
        {
            case 0:
                break;
            case 1:
                CursorAltarRec.anchoredPosition = new Vector2(-190, -10);
                break;
            case 2:
                CursorAltarRec.anchoredPosition = new Vector2(0, -10);
                break;
                
            case 3:
                CursorAltarRec.anchoredPosition = new Vector2(190, -10);
                break;
            default:
                break;
        }
    }

    public void CursorAltarLocatingSub()//�����ΏۑI����ʂ̃J�[�\���ʒu���X�V����֐�
    {
        switch (cursorNumFieldSub)
        {
            case 0:
                break;
            case 1:
                cursorFieldRec.anchoredPosition = new Vector2(-40, 120);
                cursorFieldRec.sizeDelta = new Vector2(80, 100);
                break;
            case 2:
                cursorFieldRec.anchoredPosition = new Vector2(-40, 30);
                cursorFieldRec.sizeDelta = new Vector2(80, 100);
                break;
            case 3:
                cursorFieldRec.anchoredPosition = new Vector2(-40, -60);
                cursorFieldRec.sizeDelta = new Vector2(80, 100);
                break;
            case 4:
                cursorFieldRec.anchoredPosition = new Vector2(-40, -150);
                cursorFieldRec.sizeDelta = new Vector2(80, 100);
                break;
            default:
                break;
        }
    }

    public void ATKLvImageUpdate(int Lv)
    {
        switch (Lv)
        {
            case 0:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(false); ATKLvImage[2].SetActive(false); ATKLvImage[3].SetActive(false); ATKLvImage[4].SetActive(false); ATKLvImage[5].SetActive(false); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 1:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(false); ATKLvImage[3].SetActive(false); ATKLvImage[4].SetActive(false); ATKLvImage[5].SetActive(false); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 2:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(false); ATKLvImage[4].SetActive(false); ATKLvImage[5].SetActive(false); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 3:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(false); ATKLvImage[5].SetActive(false); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 4:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(false); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 5:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(false); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 6:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(true); ATKLvImage[7].SetActive(false); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 7:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(true); ATKLvImage[7].SetActive(true); ATKLvImage[8].SetActive(false); ATKLvImage[9].SetActive(false);
                break;
            case 8:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(true); ATKLvImage[7].SetActive(true); ATKLvImage[8].SetActive(true); ATKLvImage[9].SetActive(false);
                break;
            case 9:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(true); ATKLvImage[7].SetActive(true); ATKLvImage[8].SetActive(true); ATKLvImage[9].SetActive(true);
                break;
            case 10:
                ATKLvImage[0].SetActive(true); ATKLvImage[1].SetActive(true); ATKLvImage[2].SetActive(true); ATKLvImage[3].SetActive(true); ATKLvImage[4].SetActive(true); ATKLvImage[5].SetActive(true); ATKLvImage[6].SetActive(true); ATKLvImage[7].SetActive(true); ATKLvImage[8].SetActive(true); ATKLvImage[9].SetActive(true);
                break;
            default:
                break;
        }
    }
    public void SPDLvImageUpdate(int Lv)
    {
        switch (Lv)
        {
            case 0:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(false); SPDLvImage[2].SetActive(false); SPDLvImage[3].SetActive(false); SPDLvImage[4].SetActive(false); SPDLvImage[5].SetActive(false); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 1:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(false); SPDLvImage[3].SetActive(false); SPDLvImage[4].SetActive(false); SPDLvImage[5].SetActive(false); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 2:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(false); SPDLvImage[4].SetActive(false); SPDLvImage[5].SetActive(false); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 3:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(false); SPDLvImage[5].SetActive(false); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 4:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(false); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 5:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(false); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 6:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(true); SPDLvImage[7].SetActive(false); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 7:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(true); SPDLvImage[7].SetActive(true); SPDLvImage[8].SetActive(false); SPDLvImage[9].SetActive(false);
                break;
            case 8:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(true); SPDLvImage[7].SetActive(true); SPDLvImage[8].SetActive(true); SPDLvImage[9].SetActive(false);
                break;
            case 9:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(true); SPDLvImage[7].SetActive(true); SPDLvImage[8].SetActive(true); SPDLvImage[9].SetActive(true);
                break;
            case 10:
                SPDLvImage[0].SetActive(true); SPDLvImage[1].SetActive(true); SPDLvImage[2].SetActive(true); SPDLvImage[3].SetActive(true); SPDLvImage[4].SetActive(true); SPDLvImage[5].SetActive(true); SPDLvImage[6].SetActive(true); SPDLvImage[7].SetActive(true); SPDLvImage[8].SetActive(true); SPDLvImage[9].SetActive(true);
                break;
            default:
                break;
        }
    }
    public void AbilityLvImageUpdate(int Lv)
    {
        switch (Lv)
        {
            case 0:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(false); AbilityLvImage[2].SetActive(false); AbilityLvImage[3].SetActive(false); AbilityLvImage[4].SetActive(false); AbilityLvImage[5].SetActive(false); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 1:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(false); AbilityLvImage[3].SetActive(false); AbilityLvImage[4].SetActive(false); AbilityLvImage[5].SetActive(false); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 2:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(false); AbilityLvImage[4].SetActive(false); AbilityLvImage[5].SetActive(false); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 3:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(false); AbilityLvImage[5].SetActive(false); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 4:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(false); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 5:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(false); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 6:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(true); AbilityLvImage[7].SetActive(false); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 7:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(true); AbilityLvImage[7].SetActive(true); AbilityLvImage[8].SetActive(false); AbilityLvImage[9].SetActive(false);
                break;
            case 8:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(true); AbilityLvImage[7].SetActive(true); AbilityLvImage[8].SetActive(true); AbilityLvImage[9].SetActive(false);
                break;
            case 9:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(true); AbilityLvImage[7].SetActive(true); AbilityLvImage[8].SetActive(true); AbilityLvImage[9].SetActive(true);
                break;
            case 10:
                AbilityLvImage[0].SetActive(true); AbilityLvImage[1].SetActive(true); AbilityLvImage[2].SetActive(true); AbilityLvImage[3].SetActive(true); AbilityLvImage[4].SetActive(true); AbilityLvImage[5].SetActive(true); AbilityLvImage[6].SetActive(true); AbilityLvImage[7].SetActive(true); AbilityLvImage[8].SetActive(true); AbilityLvImage[9].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void StatusTextUpdate_Field()
    {
        Lv_player_Field.text = "Lv" + (ATK0Lv + SPD0Lv + Ability0Lv);
        EXP_player_Field.text = "EXP:" + EXP0;
        Lv_monster1_Field.text = "Lv" + (ATKLv + SPDLv + AbilityLv);
        EXP_monster1_Field.text = "EXP:" + EXP1;
        Lv_monster2_Field.text = "Lv" + (ATK2Lv + SPD2Lv + Ability2Lv);
        EXP_monster2_Field.text = "EXP:" + EXP2;
        Lv_monster3_Field.text = "Lv" + (ATK3Lv + SPD3Lv + Ability3Lv);
        EXP_monster3_Field.text = "EXP:" + EXP3;
    }

    public void LvImageReset()//������ʂ̃��x���摜���X�V����֐�
    {

    }
    public void LvTextReset()
    {
        switch (CursorAltarLocation)
        {
            case 1://�U���͂̏オ�蕝
                StUpText.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 0);
                if (StrengthenNum == 0)
                {
                    StUpText.GetComponent<Text>().text = "+30";
                }
                else
                {
                    StUpText.GetComponent<Text>().text = "+30";
                }
                break;
            case 2://�f�����̏オ�蕝
                StUpText.GetComponent<RectTransform>().anchoredPosition = new Vector2(240, 0);
                if (StrengthenNum == 0)
                {
                    StUpText.GetComponent<Text>().text = "+30";
                }
                else
                {
                    StUpText.GetComponent<Text>().text = "+30";
                }
                break;
            case 3://�������x���̏オ�蕝
                StUpText.GetComponent<RectTransform>().anchoredPosition = new Vector2(430, 0);
                StUpText.GetComponent<Text>().text = "+1";
                break;
            default:
                break;
        }
    }
    IEnumerator PowerUpTargetChange()//0.3�b�Ńt�F�[�h�A�E�g����0.3�b�Ńt�F�[�h�C������
    {
        for (int i = 1; i < 12; i++)
        {
            switch (i)
            {
                case 1:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(-25,0);
                    MonsterStrengthenCanvasGroup.alpha = 0.5f;
                    break;
                case 2:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(-35, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.3f;
                    break;
                case 3:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(-40, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.2f;
                    break;
                case 4:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(-43, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.1f;
                    break;
                case 5:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(-45, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.05f;
                    break;
                case 6:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(25, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.5f;
                    TextUpdateAfterStrengthen();
                    break;
                case 7:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(15, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.8f;
                    break;
                case 8:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(10, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.9f;
                    break;
                case 9:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(5, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.95f;
                    break;
                case 10:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(2, 0);
                    MonsterStrengthenCanvasGroup.alpha = 0.98f;
                    break;
                case 11:
                    MonsterStrengthenRt.anchoredPosition = new Vector2(0, 0);
                    MonsterStrengthenCanvasGroup.alpha = 1f;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void TextUpdateAfterStrengthen()
    {
        switch (StrengthenNum)
        {
            case 0:
                FaceImageAltar[3].SetActive(false);
                FaceImageAltar[2].SetActive(false);
                FaceImageAltar[1].SetActive(false);
                FaceImageAltar[0].SetActive(true);
                NameAltar.text = "�v���C���[ Lv" + (ATK0Lv + SPD0Lv + Ability0Lv);
                MaxHP = 1000 + (100 * (ATK0Lv + SPD0Lv + Ability0Lv));
                TextATKAltar.text = "" + (ATK0 + ATK0Lv * 30f);
                TextSPDAltar.text = "" + (SPD0 + SPD0Lv * 30f);
                TextAbilityAltar.text = "Lv" + Ability0Lv;
                TextAbilityExplanationAltar.text = "�v���_���[�W���󂯂��" + (1 + Ability0Lv) + "�x����HP1�őς���B";
                TextEXPAltar.text = "EXP  " + EXP0;
                break;
            case 1:
                FaceImageAltar[3].SetActive(false);
                FaceImageAltar[2].SetActive(false);
                FaceImageAltar[1].SetActive(true);
                FaceImageAltar[0].SetActive(false);
                NameAltar.text = "���b�T�[�h���S�� Lv" + (ATKLv + SPDLv + AbilityLv);
                MaxHP2 = 800 + (80 * (ATKLv + SPDLv + AbilityLv));
                TextATKAltar.text = "" + (ATK1 + ATKLv * 30f);
                TextSPDAltar.text = "" + (SPD1 + SPDLv * 30f);
                TextAbilityAltar.text = "Lv" + AbilityLv;
                //TextAbilityNameAltar.text = "�҉� Lv" + AbilityLv;
                TextAbilityExplanationAltar.text = "�^����_���[�W��" + (10 * AbilityLv) + "%�㏸�B";
                TextEXPAltar.text = "EXP  " + EXP1;
                ATKLvImageUpdate(ATKLv);
                SPDLvImageUpdate(SPDLv);
                AbilityLvImageUpdate(AbilityLv);
                break;
            case 2:
                FaceImageAltar[3].SetActive(false);
                FaceImageAltar[2].SetActive(true);
                FaceImageAltar[1].SetActive(false);
                FaceImageAltar[0].SetActive(false);
                NameAltar.text = "�T�����C�h�b�O Lv" + (ATK2Lv + SPD2Lv + Ability2Lv);
                MaxHP3 = 1200 + (120 * (ATK2Lv + SPD2Lv + Ability2Lv));
                TextATKAltar.text = "" + (ATK2 + ATK2Lv * 30f);
                TextSPDAltar.text = "" + (SPD2 + SPD2Lv * 30f);
                TextAbilityAltar.text = "Lv" + Ability2Lv;
                TextAbilityExplanationAltar.text = "�s�����Ԃ�" + (9 * Ability2Lv) + "%�Z�k����B";
                TextEXPAltar.text = "EXP  " + EXP2;
                ATKLvImageUpdate(ATK2Lv);
                SPDLvImageUpdate(SPD2Lv);
                AbilityLvImageUpdate(Ability2Lv);
                break;
            case 3:
                FaceImageAltar[3].SetActive(true);
                FaceImageAltar[2].SetActive(false);
                FaceImageAltar[1].SetActive(false);
                FaceImageAltar[0].SetActive(false);
                NameAltar.text = "Lv" + (ATK3Lv + SPD3Lv + Ability3Lv);
                TextATKAltar.text = "" + (ATK3 + ATK3Lv * 30f);
                TextSPDAltar.text = "" + (SPD3 + SPD3Lv * 30f);
                TextAbilityAltar.text = "Lv" + Ability3Lv;
                TextAbilityNameAltar.text = "�Đ� Lv" + Ability3Lv;
                TextAbilityExplanationAltar.text = "�O�ɏo�Ă��邠�����AHP��" + (10 * Ability3Lv) + "�Â񕜂���B";
                TextEXPAltar.text = "EXP  " + EXP3;
                ATKLvImageUpdate(ATK3Lv);
                SPDLvImageUpdate(SPD3Lv);
                AbilityLvImageUpdate(Ability3Lv);
                break;
            default:
                break;
        }
        HPText.text = bm.HP + "/" + MaxHP;
        HPSlider.value = bm.HP / MaxHP;
        HPText2.text = bm.HP2 + "/" + MaxHP2;
        HPSlider2.value = bm.HP2 / MaxHP2;
        HPText3.text = bm.HP3 + "/" + MaxHP3;
        HPSlider3.value = bm.HP3 / MaxHP3;

        LvTextForest.text = "�~ " + LvOfForest + "";
        LvTextSea.text = "�~ " + LvOfSea + "";
        LvTextCity.text = "�~ " + LvOfCity + "";

        LvImageReset();
        LvTextReset();

        StrengthenCostTextUpdate();
    }

    public void OnClickStageButton()
    {
        audioSource.PlayOneShot(SE6);
        gm.SceneNum = 3;
        bm.MaxHP = MaxHP;
        bm.MaxHP2 = MaxHP2;
        bm.MaxHP3 = MaxHP3;
        bm.DEF = DEF + DEFLv * 10;
        bm.ATK0 = ATK0 + ATK0Lv * 30;
        bm.SPD0 = SPD0 + SPD0Lv * 30;
        bm.ATK1 = ATK1 + ATKLv * 30;
        bm.SPD1 = SPD1 + SPDLv * 30;
        bm.AbilityLv = AbilityLv;

        bm.ATK2 = ATK2 + ATK2Lv * 30;
        bm.SPD2 = SPD2 + SPD2Lv * 30;
        bm.ATK3 = ATK3 + ATK3Lv * 30;
        bm.SPD3 = SPD3 + SPD3Lv * 30;

        bm.playerSkillRemain = Ability0Lv + 1;
        bm.DragonSkillLevel = AbilityLv + 1;
        bm.DogSkillLevel = Ability2Lv + 1;

        isField = false;
        isBattle = true;

        bm.stageNum = stageLevelCur;

        if (bm.stageNum == 7 || bm.stageNum == 1 || bm.stageNum == 2)
        {
            bm.ImageForest.SetActive(true);
            bm.ImageSea.SetActive(false);
            bm.ImageRuin.SetActive(false);
        }
        else if (bm.stageNum == 4 || bm.stageNum == 5 || bm.stageNum == 6 || bm.stageNum == 6)
        {
            bm.ImageForest.SetActive(false);
            bm.ImageSea.SetActive(true);
            bm.ImageRuin.SetActive(false);
        }
        else if (bm.stageNum == 8 || bm.stageNum == 9)
        {
            bm.ImageForest.SetActive(false);
            bm.ImageSea.SetActive(false);
            bm.ImageRuin.SetActive(true);
        }
        if (stageLevelCur == 11 || stageLevelCur == 7 || stageLevelCur == 1 || stageLevelCur == 2 || stageLevelCur == 13 || stageLevelCur == 14 || stageLevelCur == 15 || stageLevelCur == 16 || stageLevelCur == 19)
        {
            bm.ImageForest.SetActive(true);
            bm.ImageSea.SetActive(false);
            bm.ImageRuin.SetActive(false);
        }
        if (stageLevelCur == 17 || stageLevelCur == 18 || stageLevelCur == 4 || stageLevelCur == 5 || stageLevelCur == 20 || stageLevelCur == 21 || stageLevelCur == 22 || stageLevelCur == 3 || stageLevelCur == 6)
        {
            bm.ImageForest.SetActive(false);
            bm.ImageSea.SetActive(true);
            bm.ImageRuin.SetActive(false);
        }
        if (stageLevelCur == 12 || stageLevelCur == 24 || stageLevelCur == 23 || stageLevelCur == 8 || stageLevelCur == 9 || stageLevelCur == 30)
        {
            bm.ImageForest.SetActive(false);
            bm.ImageSea.SetActive(false);
            bm.ImageRuin.SetActive(true);
        }

        bm.playerHPUpdate();
        bm.enemyEncount();
        gm.StartFadeOut();
    }

    public void OnEnterDemo0()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, 70);
        ImageForest.SetActive(true);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(false);
        stageLevelCur = 11;
    }

    public void OnEnterDemo1()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350,70);
        ImageForest.SetActive(true);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(false);
        stageLevelCur = 1;
    }
    public void OnEnterDemo2()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, 0);
        ImageForest.SetActive(true);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(false);
        stageLevelCur = 2;
    }
    public void OnEnterDemo3()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(true);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(false);
        stageLevelCur = 3;
    }
    public void OnEnterDemo4()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(true);
        ImageRuin.SetActive(false);
        stageLevelCur = 4;
    }
    public void OnEnterDemo5()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(true);
        ImageRuin.SetActive(false);
        stageLevelCur = 5;
    }
    public void OnEnterDemo6()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(true);
        ImageRuin.SetActive(false);
        stageLevelCur = 6;
    }
    public void OnEnterDemo7()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(true);
        stageLevelCur = 7;
    }
    public void OnEnterDemo8()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(true);
        stageLevelCur = 8;
    }
    public void OnEnterDemo9()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(true);
        stageLevelCur = 9;
    }

    public void OnEnterDemo12()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(true);
        stageLevelCur = 12;
    }

    public void OnEnterDemo13()
    {
        stageLevelCur = 13;
    }
    public void OnEnterDemo14()
    {
        stageLevelCur = 14;
    }
    public void OnEnterDemo15()
    {
        stageLevelCur = 15;
    }
    public void OnEnterDemo16()
    {
        stageLevelCur = 16;
    }
    public void OnEnterDemo17()
    {
        stageLevelCur = 17;
    }
    public void OnEnterDemo18()
    {
        stageLevelCur = 18;
    }
    public void OnEnterDemo19()
    {
        stageLevelCur = 19;
    }
    public void OnEnterDemo20()
    {
        stageLevelCur = 20;
    }
    public void OnEnterDemo21()
    {
        stageLevelCur = 21;
    }
    public void OnEnterDemo22()
    {
        stageLevelCur = 22;
    }
    public void OnEnterDemo23()
    {
        stageLevelCur = 23;
    }
    public void OnEnterDemo24()
    {
        stageLevelCur = 24;
    }
    public void OnEnterDemo30()
    {
        stageLevelCur = 30;
    }

    public void OnEnterDemoRest()
    {
        cursorFieldRec.anchoredPosition = new Vector2(-350, -70);
        ImageForest.SetActive(false);
        ImageSea.SetActive(false);
        ImageRuin.SetActive(false);
        ImageRest.SetActive(true);
    }

    IEnumerator ItemShortage()
    {
        ItemShortageObj.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ItemShortageObj.SetActive(false);
    }

    IEnumerator RestHPRecover()
    {
        audioSource.PlayOneShot(SE5);
        while ((bm.HP < MaxHP || bm.HP2 < MaxHP2 || bm.HP3 < MaxHP3) && money > 5)
        {
            money -= 5;
            bm.HP += 5;
            bm.HP2 += 5;
            bm.HP3 += 5;
            if (bm.HP > MaxHP)
            {
                bm.HP = MaxHP;
            }
            if (bm.HP2 > MaxHP2)
            {
                bm.HP2 = MaxHP2;
            }
            if (bm.HP3 > MaxHP3)
            {
                bm.HP3 = MaxHP3;
            }
            HPText.text = bm.HP + "/" + MaxHP;
            HPSlider.value = bm.HP / MaxHP;
            HPText2.text = bm.HP2 + "/" + MaxHP2;
            HPSlider2.value = bm.HP2 / MaxHP2;
            HPText3.text = bm.HP3 + "/" + MaxHP3;
            HPSlider3.value = bm.HP3 / MaxHP3;
            moneyText.text = "" + money;
            yield return new WaitForSeconds(0.01f);
        }
        while ((bm.HP < MaxHP || bm.HP2 < MaxHP2 || bm.HP3 < MaxHP3) && money > 0)
        {
            money -= 1;
            bm.HP += 1;
            bm.HP2 += 1;
            bm.HP3 += 1;
            if (bm.HP > MaxHP)
            {
                bm.HP = MaxHP;
            }
            if (bm.HP2 > MaxHP2)
            {
                bm.HP2 = MaxHP2;
            }
            if (bm.HP3 > MaxHP3)
            {
                bm.HP3 = MaxHP3;
            }
            HPText.text = bm.HP + "/" + MaxHP;
            HPSlider.value = bm.HP / MaxHP;
            HPText2.text = bm.HP2 + "/" + MaxHP2;
            HPSlider2.value = bm.HP2 / MaxHP2;
            HPText3.text = bm.HP3 + "/" + MaxHP3;
            HPSlider3.value = bm.HP3 / MaxHP3;
            moneyText.text = "" + money;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void strengthenEXE()
    {
        switch (CursorAltarLocation)
        {
            case 1://ATK����������
                switch (StrengthenNum)
                {
                    case 0:
                        //�v���[���[��HP����
                        if (EXP0 >= ((ATK0Lv + 1) * 5) && LvOfForest >= ((ATK0Lv + 1) * 5) && ATK0Lv < 10)
                        {
                            EXP0 -= (ATK0Lv + 1) * 5;
                            LvOfForest -= (ATK0Lv + 1) * 5;

                            TextEXPAltar.text = "EXP  " + EXP0;
                            ATK0Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        TextATKAltar.text = "";
                        break;
                    case 1:
                        if (EXP1 >= ((ATKLv + 1) * 5) && LvOfForest >= ((ATKLv + 1) * 5) && ATKLv < 10)
                        {
                            EXP1 -= (ATKLv + 1) * 5;
                            LvOfForest -= (ATKLv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP1;
                            ATKLv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        ATKLvImageUpdate(ATKLv);
                        TextATKAltar.text = "ATK  " + (ATK1 + ATKLv * 50f);
                        break;
                    case 2:
                        if (EXP2 >= ((ATK2Lv + 1) * 5) && LvOfForest >= ((ATK2Lv + 1) * 5) && ATK2Lv < 10)
                        {
                            EXP2 -= (ATK2Lv + 1) * 5;
                            LvOfForest -= (ATK2Lv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP2;
                            ATK2Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        ATKLvImageUpdate(ATK2Lv);
                        TextATKAltar.text = "ATK  " + (ATK2 + ATK2Lv * 50f);
                        break;
                    case 3:
                        if (EXP3 >= 1 && ATK3Lv < 10)
                        {
                            EXP3--;
                            TextEXPAltar.text = "EXP  " + EXP3;
                            ATK3Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        ATKLvImageUpdate(ATK3Lv);
                        TextATKAltar.text = "ATK  " + (ATK3 + ATK3Lv * 50f);
                        break;
                    default:
                        break;
                }
                break;
            case 2://SPD����������
                switch (StrengthenNum)
                {
                    case 0:
                        //�v���[���[��DEF����
                        if (EXP0 >= ((SPD0Lv + 1) * 5) && LvOfSea >= ((SPD0Lv + 1) * 5) && SPD0Lv < 10)
                        {
                            EXP0 -= (SPD0Lv + 1) * 5;
                            LvOfSea -= (SPD0Lv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP0;
                            SPD0Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        SPDLvImageUpdate(DEFLv);
                        TextSPDAltar.text = "DEF  " + DEF;
                        break;
                    case 1:
                        if (EXP1 >= ((SPDLv + 1) * 5) && LvOfSea >= ((SPDLv + 1) * 5) && SPDLv < 10)
                        {
                            EXP1 -= (SPDLv + 1) * 5;
                            LvOfSea -= (SPDLv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP1;
                            SPDLv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        SPDLvImageUpdate(SPDLv);
                        TextSPDAltar.text = "SPD  " + (SPD1 + SPDLv * 30f);
                        break;
                    case 2:
                        if (EXP2 >= ((SPD2Lv + 1) * 5) && LvOfSea >= ((SPD2Lv + 1) * 5) && SPD2Lv < 10)
                        {
                            EXP2 -= (SPD2Lv + 1) * 5;
                            LvOfSea -= (SPD2Lv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP2;
                            SPD2Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        SPDLvImageUpdate(SPD2Lv);
                        TextSPDAltar.text = "SPD  " + (SPD2 + SPD2Lv * 30f);
                        break;
                    case 3:
                        if (EXP3 >= 1 && SPD3Lv < 10)
                        {
                            EXP3--;
                            TextEXPAltar.text = "EXP  " + EXP3;
                            SPD3Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        SPDLvImageUpdate(SPD3Lv);
                        TextSPDAltar.text = "SPD  " + (SPD3 + SPD3Lv * 30f);
                        break;
                    default:
                        break;
                }
                break;
            case 3://��������������
                switch (StrengthenNum)
                {
                    case 0:
                        //�v���[���[�̓�������
                        if (EXP0 >= ((Ability0Lv + 1) * 5) && LvOfCity >= ((Ability0Lv + 1) * 5) && Ability0Lv < 10)
                        {
                            EXP0 -= (Ability0Lv + 1) * 5;
                            LvOfCity -= (Ability0Lv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP0;
                            Ability0Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        AbilityLvImageUpdate(Ability0Lv);
                        TextAbilityAltar.text = "����  Lv" + Ability0Lv;
                        TextAbilityExplanationAltar.text = "�v���_���[�W���󂯂��" + (1 + Ability0Lv) + "�x����HP1�őς���B";
                        break;
                    case 1:
                        if (EXP1 >= ((AbilityLv + 1) * 5) && LvOfCity >= ((AbilityLv + 1) * 5) && AbilityLv < 10)
                        {
                            EXP1 -= (AbilityLv + 1) * 5;
                            LvOfCity -= (AbilityLv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP1;
                            AbilityLv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        AbilityLvImageUpdate(AbilityLv);
                        TextAbilityAltar.text = "����  Lv" + AbilityLv;
                        TextAbilityExplanationAltar.text = "�u���������v�ŗ^����_���[�W��" + (10 * AbilityLv) + "%�㏸�B";
                        break;
                    case 2:
                        if (EXP2 >= ((Ability2Lv + 1) * 5) && LvOfCity >= ((Ability2Lv + 1) * 5) && Ability2Lv < 10)
                        {
                            EXP2 -= (Ability2Lv + 1) * 5;
                            LvOfCity -= (Ability2Lv + 1) * 5;
                            TextEXPAltar.text = "EXP  " + EXP2;
                            Ability2Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        AbilityLvImageUpdate(Ability2Lv);
                        TextAbilityAltar.text = "����  Lv" + Ability2Lv;
                        TextAbilityExplanationAltar.text = "�e�L�X�g�B";
                        break;
                    case 3:
                        if (EXP3 >= 1 && Ability3Lv < 10)
                        {
                            EXP3--;
                            TextEXPAltar.text = "EXP  " + EXP3;
                            Ability3Lv++;
                            audioSource.PlayOneShot(SE2);
                        }
                        else
                        {
                            StartCoroutine("EXPShortage");
                            audioSource.PlayOneShot(SE4);
                        }
                        AbilityLvImageUpdate(Ability3Lv);
                        TextAbilityAltar.text = "����  Lv" + Ability3Lv;
                        TextAbilityExplanationAltar.text = "�e�L�X�g�B";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        TextUpdateAfterStrengthen();
        recast = true;
    }

    IEnumerator isStrengtheningSubFalsing()
    {
        yield return new WaitForSeconds(0.35f);
        isStrengtheningSub = false;
    }

    public void StrengthenCostTextUpdate()
    {
        switch (StrengthenNum)
        {
            case 0:
                ATKCostText.text = "EXP :" + ((ATK0Lv + 1) * 5) + " / " + EXP0;
                SPDCostText.text = "EXP :" + ((SPD0Lv + 1) * 5) + " / " + EXP0;
                AbilityCostText.text = "EXP :" + ((Ability0Lv + 1) * 5) + " / " + EXP0;
                ATKCostText2.text = ":" + ((ATK0Lv + 1) * 5) + " / " + LvOfForest;
                SPDCostText2.text = " :" + ((SPD0Lv + 1) * 5) + " / " + LvOfSea;
                AbilityCostText2.text = " :" + ((Ability0Lv + 1) * 5) + " / " + LvOfCity;
                if (ATK0Lv == 10)
                {
                    ATKCostText.text = "EXP : - / " + EXP0;
                    ATKCostText2.text = " : - / " + LvOfForest;
                }
                if (SPD0Lv == 10)
                {
                    SPDCostText.text = "EXP : - / " + EXP0;
                    SPDCostText2.text = " : - / " + LvOfSea;
                }
                if (Ability0Lv == 10)
                {
                    AbilityCostText.text = "EXP : - / " + EXP0;
                    AbilityCostText2.text = " : - / " + LvOfCity;
                }
                break;
            case 1:
                ATKCostText.text = "EXP :" + ((ATKLv + 1) * 5) + " / " + EXP1;
                SPDCostText.text = "EXP :" + ((SPDLv + 1) * 5) + " / " + EXP1;
                AbilityCostText.text = "EXP :" + ((AbilityLv + 1) * 5) + " / " + EXP1;
                ATKCostText2.text = ":" + ((ATKLv + 1) * 5) + " / " + LvOfForest;
                SPDCostText2.text = " :" + ((SPDLv + 1) * 5) + " / " + LvOfSea;
                AbilityCostText2.text = " :" + ((AbilityLv + 1) * 5) + " / " + LvOfCity;
                if (ATKLv == 10)
                {
                    ATKCostText.text = "EXP : - / " + EXP1;
                    ATKCostText2.text = " : - / " + LvOfForest;
                }
                if (SPDLv == 10)
                {
                    SPDCostText.text = "EXP : - / " + EXP1;
                    SPDCostText2.text = " : - / " + LvOfSea;
                }
                if (AbilityLv == 10)
                {
                    AbilityCostText.text = "EXP : - / " + EXP1;
                    AbilityCostText2.text = " : - / " + LvOfCity;
                }
                break;
            case 2:
                ATKCostText.text = "EXP :" + ((ATK2Lv + 1) * 5) + " / " + EXP2;
                SPDCostText.text = "EXP :" + ((SPD2Lv + 1) * 5) + " / " + EXP2;
                AbilityCostText.text = "EXP :" + ((Ability2Lv + 1) * 5) + " / " + EXP2;
                ATKCostText2.text = ":" + ((ATK2Lv + 1) * 5) + " / " + LvOfForest;
                SPDCostText2.text = " :" + ((SPD2Lv + 1) * 5) + " / " + LvOfSea;
                AbilityCostText2.text = " :" + ((Ability2Lv + 1) * 5) + " / " + LvOfCity;
                if (ATK2Lv == 10)
                {
                    ATKCostText.text = "EXP : - / " + EXP2;
                    ATKCostText2.text = " : - / " + LvOfForest;
                }
                if (SPD2Lv == 10)
                {
                    SPDCostText.text = "EXP : - / " + EXP2;
                    SPDCostText2.text = " : - / " + LvOfSea;
                }
                if (Ability2Lv == 10)
                {
                    AbilityCostText.text = "EXP : - / " + EXP2;
                    AbilityCostText2.text = " : - / " + LvOfCity;
                }
                break;
            default:
                break;
        }
        LvTextForest.text = "�~ " + LvOfForest + "";
        LvTextSea.text = "�~ " + LvOfSea + "";
        LvTextCity.text = "�~ " + LvOfCity + "";
    }

    IEnumerator StrengthenInIn()
    {
        yield return new WaitForSeconds(0.1f);
        switch (cursorNumFieldSub)
        {
            case 1:
                Debug.Log("�v���C���[������ʂ��o��");
                characterImage[0].SetActive(true);
                characterImage[1].SetActive(false);
                characterImage[2].SetActive(false);
                StrengthenNum = 0;
                StartCoroutine("StrengthenIn");
                isStrengthening = true;
                break;
            case 2:
                Debug.Log("�ԗ�������ʂ��o��");
                characterImage[0].SetActive(false);
                characterImage[1].SetActive(true);
                characterImage[2].SetActive(false);
                StrengthenNum = 1;
                StartCoroutine("StrengthenIn");
                isStrengthening = true;
                break;
            case 3:
                Debug.Log("����������ʂ��o��");
                characterImage[0].SetActive(false);
                characterImage[1].SetActive(false);
                characterImage[2].SetActive(true);
                StrengthenNum = 2;
                StartCoroutine("StrengthenIn");
                isStrengthening = true;
                break;
            default:
                break;
        }
    }

    public void saveStart()
    {
        //�Z�[�u����
        PlayerPrefs.SetInt("ATK0Lv", ATK0Lv);
        PlayerPrefs.SetInt("SPD0Lv", SPD0Lv);
        PlayerPrefs.SetInt("Ability0Lv", Ability0Lv);
        PlayerPrefs.SetInt("ATK1Lv", ATKLv);
        PlayerPrefs.SetInt("SPD1Lv", SPDLv);
        PlayerPrefs.SetInt("Ability1Lv", AbilityLv);
        PlayerPrefs.SetInt("ATK2Lv", ATK2Lv);
        PlayerPrefs.SetInt("SPD2Lv", SPD2Lv);
        PlayerPrefs.SetInt("Ability2Lv", Ability2Lv);
        PlayerPrefs.SetInt("EXP0", EXP0);
        PlayerPrefs.SetInt("EXP1", EXP1);
        PlayerPrefs.SetInt("EXP2", EXP2);
        PlayerPrefs.SetInt("Item1", LvOfForest);
        PlayerPrefs.SetInt("Item2", LvOfSea);
        PlayerPrefs.SetInt("Item3", LvOfCity);
        PlayerPrefs.SetInt("NumOfParty", NumOfParty);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("entirePlayTime", (int)entirePlayTime);
        PlayerPrefs.SetInt("entireDefeatNum", entireDefeatNum);
        PlayerPrefs.SetInt("entireCommandUseOne", entireCommandUseOne);
        PlayerPrefs.SetInt("entireCommandUseTwo", entireCommandUseTwo);
        PlayerPrefs.SetInt("entireCommandUseThree", entireCommandUseThree);
        PlayerPrefs.SetInt("entireCommandUseFour", entireCommandUseFour);
        if (sm.isINFJump)
        {
            PlayerPrefs.SetInt("isINFJump", 1);
        }
        //�X�e�[�WID
        PlayerPrefs.SetInt("stageID", sm.stageID);
        //�|�����V���{���G�i�h���A�p���_�A���A�����@�C�A�T���A���ʁj
        if (isBossDefeated[0])
        {
            PlayerPrefs.SetInt("isBossDefeatedZero", 1);
        }
        if (isBossDefeated[1])
        {
            PlayerPrefs.SetInt("isBossDefeatedOne", 1);
        }
        if (isBossDefeated[2])
        {
            PlayerPrefs.SetInt("isBossDefeatedTwo", 1);
        }
        if (isBossDefeated[3])
        {
            PlayerPrefs.SetInt("isBossDefeatedThree", 1);
        }
        if (isBossDefeated[4])
        {
            PlayerPrefs.SetInt("isBossDefeatedFour", 1);
        }
        //eventNum��ۑ�
        PlayerPrefs.SetInt("eventNum", eTM.eventNum);
        PlayerPrefs.SetFloat("playerPosX", playerRec.anchoredPosition.x);
        PlayerPrefs.SetFloat("playerPosY", playerRec.anchoredPosition.y);
        PlayerPrefs.Save();
    }

    public void saveEnding()
    {
        //�Z�[�u����
        PlayerPrefs.SetInt("ATK0Lv", ATK0Lv);
        PlayerPrefs.SetInt("SPD0Lv", SPD0Lv);
        PlayerPrefs.SetInt("Ability0Lv", Ability0Lv);
        PlayerPrefs.SetInt("ATK1Lv", ATKLv);
        PlayerPrefs.SetInt("SPD1Lv", SPDLv);
        PlayerPrefs.SetInt("Ability1Lv", AbilityLv);
        PlayerPrefs.SetInt("ATK2Lv", ATK2Lv);
        PlayerPrefs.SetInt("SPD2Lv", SPD2Lv);
        PlayerPrefs.SetInt("Ability2Lv", Ability2Lv);
        PlayerPrefs.SetInt("EXP0", EXP0);
        PlayerPrefs.SetInt("EXP1", EXP1);
        PlayerPrefs.SetInt("EXP2", EXP2);
        PlayerPrefs.SetInt("Item1", LvOfForest);
        PlayerPrefs.SetInt("Item2", LvOfSea);
        PlayerPrefs.SetInt("Item3", LvOfCity);
        PlayerPrefs.SetInt("NumOfParty", NumOfParty);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("entirePlayTime", (int)entirePlayTime);
        PlayerPrefs.SetInt("entireDefeatNum", entireDefeatNum);
        PlayerPrefs.SetInt("entireCommandUseOne", entireCommandUseOne);
        PlayerPrefs.SetInt("entireCommandUseTwo", entireCommandUseTwo);
        PlayerPrefs.SetInt("entireCommandUseThree", entireCommandUseThree);
        PlayerPrefs.SetInt("entireCommandUseFour", entireCommandUseFour);
        if (sm.isINFJump)
        {
            PlayerPrefs.SetInt("isINFJump", 1);
        }
        //�X�e�[�WID
        PlayerPrefs.SetInt("stageID", sm.stageID);
        //�|�����V���{���G�i�h���A�p���_�A���A�����@�C�A�T���A���ʁj
        if (isBossDefeated[0])
        {
            PlayerPrefs.SetInt("isBossDefeatedZero", 1);
        }
        if (isBossDefeated[1])
        {
            PlayerPrefs.SetInt("isBossDefeatedOne", 1);
        }
        if (isBossDefeated[2])
        {
            PlayerPrefs.SetInt("isBossDefeatedTwo", 1);
        }
        if (isBossDefeated[3])
        {
            PlayerPrefs.SetInt("isBossDefeatedThree", 1);
        }
        if (isBossDefeated[4])
        {
            PlayerPrefs.SetInt("isBossDefeatedFour", 1);
        }
        //eventNum��ۑ�
        PlayerPrefs.SetInt("eventNum", eTM.eventNum);
        PlayerPrefs.Save();
    }

    public void loadStart()
    {
        //���[�h����
        ATK0Lv = PlayerPrefs.GetInt("ATK0Lv", 0);
        SPD0Lv = PlayerPrefs.GetInt("SPD0Lv", 0);
        Ability0Lv = PlayerPrefs.GetInt("Ability0Lv", 0);
        ATKLv = PlayerPrefs.GetInt("ATK1Lv", 0);
        SPDLv = PlayerPrefs.GetInt("SPD1Lv", 0);
        AbilityLv = PlayerPrefs.GetInt("Ability1Lv", 0);
        ATK2Lv = PlayerPrefs.GetInt("ATK2Lv", 0);
        SPD2Lv = PlayerPrefs.GetInt("SPD2Lv", 0);
        Ability2Lv = PlayerPrefs.GetInt("Ability2Lv", 0);
        EXP0 = PlayerPrefs.GetInt("EXP0", 0);
        EXP1 = PlayerPrefs.GetInt("EXP1", 0);
        EXP2 = PlayerPrefs.GetInt("EXP2", 0);
        LvOfForest = PlayerPrefs.GetInt("Item1", 0);
        LvOfSea = PlayerPrefs.GetInt("Item2", 0);
        LvOfCity = PlayerPrefs.GetInt("Item3", 0);
        NumOfParty = PlayerPrefs.GetInt("NumOfParty", 1);
        money = PlayerPrefs.GetInt("money", 0);
        moneyText.text = "" + money;
        entirePlayTime += PlayerPrefs.GetInt("entirePlayTime", 0);
        entireDefeatNum = PlayerPrefs.GetInt("entireDefeatNum", 0);
        entireCommandUseOne = PlayerPrefs.GetInt("entireCommandUseOne", 0);
        entireCommandUseTwo = PlayerPrefs.GetInt("entireCommandUseTwo", 0);
        entireCommandUseThree = PlayerPrefs.GetInt("entireCommandUseThree", 0);
        entireCommandUseFour = PlayerPrefs.GetInt("entireCommandUseFour", 0);
        if (PlayerPrefs.GetInt("isINFJump", 0) == 1)
        {
            sm.isINFJump = true;
        }
        //�X�e�[�WID
        sm.stageID =  PlayerPrefs.GetInt("stageID", 1);
        sm.changeWalkSE();
        //�|�����V���{���G�i�h���A�p���_�A���A�����@�C�A�T���A���ʁj
        if (PlayerPrefs.GetInt("isBossDefeatedZero", 0) == 1)
        {
            isBossDefeated[0] = true;
            //�h��������
            ImageDoragonStage.SetActive(false);
        }
        if (PlayerPrefs.GetInt("isBossDefeatedOne", 0) == 1)
        {
            isBossDefeated[1] = true;
            //�p���_�̔��������
            bm.eventTriggerPanda.SetActive(false);
            //villagerManager��isActive��true�ɂ���
            villager[0].isActive = true;
        }
        if (PlayerPrefs.GetInt("isBossDefeatedTwo", 0) == 1)
        {
            isBossDefeated[2] = true;
            //��������
            ImageDogStage.SetActive(false);
        }
        if (PlayerPrefs.GetInt("isBossDefeatedThree", 0) == 1)
        {
            isBossDefeated[3] = true;
            //�����@�C�A�̔��������
            eventTriggerReviathan.SetActive(false);
            villager[1].isActive = true;
        }
        if (PlayerPrefs.GetInt("isBossDefeatedFour", 0) == 1)
        {
            isBossDefeated[4] = true;
        }
        //eventNum��ۑ�
        eTM.eventNum = PlayerPrefs.GetInt("eventNum", 0);
        playerRec.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("playerPosX", -1134), PlayerPrefs.GetFloat("playerPosY", -220));

        if (NumOfParty == 2)
        {
            DoYouHaveDragon = true;
        }
        else if (NumOfParty == 3)
        {
            DoYouHaveDragon = true;
            DoYouHaveShisa = true;
        }
        MaxHP2 = 800 + (80 * (ATKLv + SPDLv + AbilityLv));
        MaxHP = 1000 + (100 * (ATK0Lv + SPD0Lv + Ability0Lv));
        MaxHP3 = 1200 + (120 * (ATK2Lv + SPD2Lv + Ability2Lv));
        bm.HP = MaxHP;
        bm.HP2 = MaxHP2;
        bm.HP3 = MaxHP3;
        TextUpdateAfterStrengthen();
        StatusTextUpdate_Field();
    }

    public void saveEnter()
    {
        //���{�^���ŃZ�[�u�ł���悤�ɂ���
        isSave = true;
        saveflagWindow[0].SetActive(true);
        saveflagWindow[1].SetActive(true);
        saveflagWindow[2].SetActive(true);
        saveflagWindow[3].SetActive(true);
    }
    public void saveExit()
    {
        isSave = false;
        saveflagWindow[0].SetActive(false);
        saveflagWindow[1].SetActive(false);
        saveflagWindow[2].SetActive(false);
        saveflagWindow[3].SetActive(false);
    }

    public void billboardEnter(int num)
    {
        //���{�^���ŊŔ�ǂ߂�悤�ɂ���
        isBillboard = true;
        switch (num)
        {
            case 0:
                billboardWindow[0].SetActive(true);
                billboardNum = num;
                break;
            case 1:
                billboardWindow[1].SetActive(true);
                billboardNum = num;
                break;
            case 2:
                billboardWindow[2].SetActive(true);
                billboardNum = num;
                break;
            case 3:
                billboardWindow[3].SetActive(true);
                billboardNum = num;
                break;
            case 4:
                billboardWindow[4].SetActive(true);
                billboardNum = num;
                break;
            default:
                break;
        }
    }
    public void billboardExit(int num)
    {
        isBillboard = false;
        billboardWindow[0].SetActive(false);
        billboardWindow[1].SetActive(false);
        billboardWindow[2].SetActive(false);
        billboardWindow[3].SetActive(false);
        billboardWindow[4].SetActive(false);
    }
}
