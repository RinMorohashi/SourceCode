using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour
{
    /// <summary>
    /// �}�b�v�̈ړ����b�C�x���g�A�A�C�e���̎擾��G�Ƃ̃o�g���𐧌䂷��R�[�h
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
    //�ŏ���BGM
    public AudioSource audioSourceBGM1;
    //���C��BGM
    public AudioSource audioSourceBGM2;
    //�o�g��BGM
    public AudioSource audioSourceBGM3;
    //�{�XBGM
    public AudioSource audioSourceBGM4;
    //�N���ABGM
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
    public int enemyID;//�P�F�n�E���h�A�Q�F�X�s���b�g�A�R�F�T���}���_�[�A�S�F�_�[�N�i�C�g
    string enemyName;
    public int enemyHP;
    public int playerHP;
    public int actNum;//�P�Ȃ炽�������A�Q�Ȃ�ɂ���A�퓬�J�n�O�͂O�ɂ��Ă����B
    public Animator slashAnimator;
    public Text playerHPText;
    public int[] playerStatus;//[0]�F���N�A[1]�F�ؓ��A[2]�̗́A[3]�헪�A[4]�b�p�A[5]�m�\�i�ǂ�������l�͂T�j
    public int NumOfAct;
    public Text NumOfActText;
    public bool isOpen;
    public RectTransform statusWindowRect;
    public statusManager stManager;
    public GameObject[] doExamButton;
    int score;
    public RectTransform EquipRect;
    public int equipItem;//�O�F�f��A�P�F�n�V�S�A�Q�F�Q�l���A�R�F�o�ȃJ�[�h�A�S�F�}�X�^�[�\�[�h
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
    bool talked;//�F�l����P�ʂ�������
    bool talkedAtLibralium;//���w���m����P�ʂ�������

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
    //�Ǐ��̑I�������o�Ă���ԁA���̃{�^�����������Ȃ����߂̕ǃI�u�W�F�N�g
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
        NumOfActText.text = NumOfAct + "��";
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-���w��-", 1);
        departSelectUpdate(1);
        departNumber = 1;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 10; playerStatus[5] = 7;

    }

    public void OnClickStartButton()
    {
        isTalking = true;
        //�ŏ��̃C�x���g
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
        //�t�F�[�h�A�E�g���ă{�^���ɂ���ĉ摜��ς���
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
        //�l���A�C�R�����o��
        StartCoroutine(peopleImageAppear(1));
        //talk�C�x���g���o��
        talkEventNum = 7;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson2()//�ǎ��C�x���g
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[1].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //�l���A�C�R�����o��
        StartCoroutine(peopleImageAppear(2));
        //talk�C�x���g���o��
        talkEventNum = 8;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson3()//�J�W�m�C�x���g
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[2].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //�l���A�C�R�����o��
        StartCoroutine(peopleImageAppear(3));
        //talk�C�x���g���o��
        talkEventNum = 9;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        goBackTitleButton.SetActive(false);

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickPerson4()//�o�ȃJ�[�h�C�x���g
    {
        audioSource.PlayOneShot(decideSE);
        peopleImageAtField[3].DOColor(new Color(1, 1, 1, 0), 0.5f);
        //�l���A�C�R�����o��
        StartCoroutine(peopleImageAppear(4));
        //talk�C�x���g���o��
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
        //�n�V�S����ɓ��ꂽ�e�L�X�g
        ItemObjectAtField[0].SetActive(false);
        //talk�C�x���g���o��
        talkEventNum = 100;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        //�A�C�e���X���b�g�Ƀn�V�S��ǉ�����

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickBook()
    {
        audioSource.PlayOneShot(decideSE);
        //�{����ɓ��ꂽ�e�L�X�g
        ItemObjectAtField[1].SetActive(false);
        //talk�C�x���g���o��
            talkEventNum = 101;
            talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
            talkProceedButton.SetActive(true);
            talk();
        //�A�C�e���X���b�g�ɖ{��ǉ�����

        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
    }
    public void OnClickCard()
    {
        audioSource.PlayOneShot(decideSE);
        //�o�ȃJ�[�h����ɓ��ꂽ�e�L�X�g
        ItemObjectAtField[2].SetActive(false);
        //talk�C�x���g���o��
        talkEventNum = 102;
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
        talkProceedButton.SetActive(true);
        talk();
        //�A�C�e���X���b�g�ɖ{��ǉ�����

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
            //�׋��C�x���g���n�߂邩����
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
        //�{�^����傫�����ĉ���炷
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
        //talk�C�x���g���o��
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
        NumOfActText.text = NumOfAct + "��";
        talkEventNum = mapNum;
        doExamButton[0].SetActive(false);
        doExamButton[1].SetActive(false);
        isTextWindowActive = false;
        //�G�̉摜���o��
        for (int i = 0; i < enemyObjAtField.Length; i++)
        {
            enemyObjAtField[i].SetActive(true);
        }
        //dotween��map�摜�̈ʒu�����炷�A������scale���g�傷��
        mapImageRect.DOLocalMove(new Vector2(mapButton[mapNum - 1].anchoredPosition.x * -2, mapButton[mapNum - 1].anchoredPosition.y * -2), 0.5f);
        mapImageRect.DOScale(new Vector3(2,2,1), 0.5f)
            .OnComplete(() => {

                fadeOutRect.anchoredPosition = new Vector2(0, 0);
                fadeOutImage.DOColor(new Color(0,0,0,1),0.2f)
                            .OnComplete(() => {
                                BGImages[mapNum - 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                                fadeOutImage.DOColor(new Color(0, 0, 0, 1), 0.2f)
                                .OnComplete(() => {
                                    //��b�C�x���g
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
                        StartCoroutine(CoDrawText("\n���͓s���̑�w�ɒʂ���w�S�N���B"));
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n���̏�������w���́c"));
                        break;
                    case 3:
                        //�w���I����ʂ��o��
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
                                StartCoroutine(CoDrawText("\n�����A���͕��w���̂S�N�����B"));                              
                                break;
                            case 2:
                                StartCoroutine(CoDrawText("\n�����A���͖@�w���̂S�N�����B"));
                                break;
                            case 3:
                                StartCoroutine(CoDrawText("\n�����A���͌o�ϊw���̂S�N�����B"));
                                break;
                            case 4:
                                StartCoroutine(CoDrawText("\n�����A���͗��w���̂S�N�����B"));
                                break;
                            case 5:
                                StartCoroutine(CoDrawText("\n�����A���͍H�w���̂S�N�����B"));
                                break;
                            case 6:
                                StartCoroutine(CoDrawText("\n�����A���͈�w���̂S�N�����B"));
                                break;
                            default:
                                break;
                        }
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        break;
                    case 5:
                        StartCoroutine(CoDrawText("\n�����āA�Ƃ���v���I�Ȗ�������Ă���B"));
                        break;
                    case 6:
                        StartCoroutine(CoDrawText("\n���͂R���̒����ŁA�����ɑ��Ǝ����s����̂����A"));
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\n���ƂɕK�v�ȒP�ʂ����Ă��Ȃ��̂��B"));
                        break;
                    case 8:
                        StartCoroutine(CoDrawText("\n����āA�Ȃ�Ƃ��Ă��������ɂP�O�P�ʂ��m�ۂ���K�v������B"));
                        break;
                    case 9:
                        StartCoroutine(CoDrawText("\n�P�O�P�ʏW�߂��Ȃ���Η��N���c�I"));
                        break;
                    case 10:
                        isTalking = false;
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Play();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        //���C����ʂ��o��
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
            case 1://�P���قɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�P���ق��B"));
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
            case 2://�U���قɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�U���ق��B"));
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
            case 3://�C���t�H���[�V�����M�������[�ɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�C���t�H���[�V�����M�������[���B"));
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
            case 4://�}���قɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�}���ق��B"));
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
            case 5://�}���قɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�W�E�X���ق��B"));
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
            case 6://�w�����ɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�w�������B"));
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
            case 7://�l�P�ɗ������̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        talkWindowImage.sprite = talkWindowSprite[1];
                        nameText.text = "�y�F�l�z";
                        if (!talked)
                        {
                            StartCoroutine(CoDrawText("\n�������A�v���Ԃ肾�ȁB"));
                        }
                        else
                        {
                            StartCoroutine(CoDrawText("\n�c��̒P�ʂ͎����ŉ��Ƃ����Ă���B"));
                            talkNum = 9;
                        }
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n�����������悢�摲�Ƃ��ȁB"));
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\n����c���P�ʑ���ĂȂ��񂾂�ˁc"));
                        nameText.text = "�y��l���z";
                        break;
                    case 4:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("\n���O�Ȃ炻�������Ǝv���āA"));
                        nameText.text = "�y�F�l�z";
                        break;
                    case 5:
                        StartCoroutine(CoDrawText("\n��������Ɏ���Ƃ������A�Q�P�ʁB"));
                        break;
                    case 6:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\n���c�H"));
                        nameText.text = "�y��l���z";
                        break;
                    case 7:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("\n���O�A���j�P���̃e�X�g�T�{������H��������Ɏ󂯂Ƃ����B"));
                        nameText.text = "�y�F�l�z";
                        break;
                    case 8:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("\n������Ă����̂��c�H�܂��������c"));
                        nameText.text = "�y��l���z";
                        break;
                    case 9:
                        //�P�ʂ��l�����鉉�o
                        StartCoroutine(CoDrawText("�P�ʂ��l�����܂����I"));
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
            case 8://�ǎ��C�x���g
                switch (talkNum)
                {
                    case 1:
                        if (!isTestPassed)
                        {
                            messageAnimation();
                            talkWindowImage.sprite = talkWindowSprite[1];
                            StartCoroutine(CoDrawText("\n�������܁A���{�Ȗڂ̒ǎ����󂯕t���Ă��܂��B"));
                            nameText.text = "�y�����z";
                        }
                        else
                        {
                            messageAnimation();
                            talkWindowImage.sprite = talkWindowSprite[1];
                            StartCoroutine(CoDrawText("\n�N�Ȃ炫���Ƃ��邳�B"));
                            nameText.text = "�y�����z";
                            talkNum = 5;
                        }
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n60�_�ō��i�ł��B�󌱂��܂����H"));                        
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        nameText.text = "�y��l���z";
                        StartCoroutine(CoDrawText("\n�i�m�b��10���炢����΍��i�ł������c�j"));
                        yesButtonText.text = "�󂯂�";
                        noButtonText.text = "�󂯂Ȃ�";
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
                            case 1://�󌱂���
                                NumOfAct--;
                                NumOfActText.text = NumOfAct + "��";
                                //��ʂ��Â�����

                                score = Random.Range(playerStatus[5] * 5,playerStatus[5] * 7 + 1);
                                if (score > 100)
                                {
                                    score = 100;
                                }
                                nameText.text = "";
                                if (score >= 60)
                                {
                                    StartCoroutine(CoDrawText("���ʁc " + score + " �_�B\n���i�ł��B"));
                                    isTestPassed = true;
                                }
                                else
                                {
                                    StartCoroutine(CoDrawText("���ʁc " + score + " �_�B\n�s���i�ł��B"));
                                }
                                break;
                            case 2://�󌱂��Ȃ�
                                talkWindowImage.sprite = talkWindowSprite[1];
                                nameText.text = "�y�����z";
                                StartCoroutine(CoDrawText("\n���ł�����Ă邩��A�܂����ĂˁB"));
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
                            nameText.text = "�y�����z";
                            StartCoroutine(CoDrawText("\n���߂łƂ��B"));
                            StartCoroutine(getCredit());
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "�y�����z";
                            StartCoroutine(CoDrawText("\n���ł�����Ă邩��A�܂����ĂˁB"));
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
            case 9://�J�W�m�C�x���g
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("�悤�����A�ł̓��Z��ցB"));
                        break;
                    case 2:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("�����̊w�����ł���H"));
                        break;
                    case 3:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("�ہA�����̊w�����ł͂Ȃ��B"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("���̒n���ł́A�P�ʂ�q�����ł̃Q�[�����s���Ă���c�I"));
                        break;
                    case 5:
                        talkWindowImage.sprite = talkWindowSprite[0];
                        StartCoroutine(CoDrawText("�P�ʂ�q����c�H����Ȃ��Ƃ��ł���̂��H"));
                        break;
                    case 6:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("�������B�����ł͒P�ʂ�ʉ݂Ƃ��Ĉ����Ă���B�ׂ������Ƃ͕����Ă����Ȃ�H"));
                        break;
                    case 7:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("���Z��łǂ̃����X�^�[�������q����񂾁B"));
                        break;
                    case 8:
                        talkWindowImage.sprite = talkWindowSprite[1];
                        StartCoroutine(CoDrawText("���ĂΑ��ƁA������Η��N�I��邩���H"));
                        yesButtonText.text = "�q����";
                        noButtonText.text = "�q���Ȃ�";
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
                            case 1://�q����
                                //�o�g����ʂ��o���ă����X�^�[���Q�̏o��
                                BattleBeginingCasino();
                                //���������ꂽ�I���������ꂽ�I
                                break;
                            case 2://�󌱂��Ȃ�
                                StartCoroutine(CoDrawText("�҂��Ă邺�B"));
                                break;
                            default:
                                break;
                        }
                        break;
                    case 10:
                        //�ǂ���ɓq����H�u�E�v�u���v
                        StartCoroutine(CoDrawText("�ǂ���ɓq����H"));
                        yesButtonText.text = "��";
                        noButtonText.text = "�E";
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
                                StartCoroutine(CoDrawText("���̃����X�^�[�ɉ��P�ʓq����H"));
                                break;
                            case 2:
                                StartCoroutine(CoDrawText("�E�̃����X�^�[�ɉ��P�ʓq����H"));
                                break;
                            default:
                                break;
                        }
                        yesButtonText.text = "����";
                        doExamButton[1].SetActive(false);
                        break;
                    case 12:
                        //�Q�̂̃����X�^�[���킹��
                        //�����̍U���I
                        //������X�̃_���[�W���󂯂��I
                        //�����̍U���I
                        //������X�̃_���[�W���󂯂��I
                        talkProceedButton.SetActive(true);
                        buttonAssault.SetActive(false);
                        buttonRun.SetActive(false);
                        StartCoroutine(CoDrawText("�����̍U���I"));
                        slashAnimator.SetBool("slash", true);
                        break;
                    case 13:
                        int damage = (int)(Random.Range(playerStatus[1] * 0.9f, playerStatus[1] * 1.1f));
                        StartCoroutine(CoDrawText(enemyName + "�� " + damage + "�̃_���[�W�I"));
                        StartCoroutine("enemyVibration");
                        enemyHP -= damage;
                        EnemyHPImage.DOFillAmount(((float)enemyHP / (float)enemyMaxHP), 0.5f);
                        enemyHPBarText.text = enemyHP + "/" + enemyMaxHP;
                        if (enemyHP <= 0)
                        {
                            talkNum = 99;
                            enemyHPBarText.text = "0/" + enemyMaxHP;
                        }
                        //12�ɖ߂�
                        talkNum = 11;
                        break;
                    case 20:
                        if (score >= 60)
                        {
                            StartCoroutine(CoDrawText("���߂łƂ��B"));
                        }
                        else
                        {
                            StartCoroutine(CoDrawText("���ł�����Ă邩��A�܂����ĂˁB"));
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
            case 10://�o�ȃJ�[�h�C�x���g
                int equip = equipItem;
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        if (!talkedAtLibralium)
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "�y���m�z";
                            StartCoroutine(CoDrawText("\n�˂������Ă�B"));
                        }
                        else
                        {
                            talkWindowImage.sprite = talkWindowSprite[1];
                            nameText.text = "�y���m�z";
                            StartCoroutine(CoDrawText("\n���͐^�ɋ����ׂ��ؖ������������A���̗]���͂���������ɂ͋�������B�c"));
                        }
                        break;
                    case 2:
                        if (!talkedAtLibralium)
                        {
                            StartCoroutine(CoDrawText("\n�킽�����w���̔��m�Ȃ񂾂��ǁA�}�W�ł������藝�𔭌����Ă��܂����́B"));
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
                        StartCoroutine(CoDrawText("\n���̒藝�̏ؖ����@��Y��Ȃ��悤�ɋL�^�������񂾂��ǁA"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("\n�茳�Ɏ��������ċL�^�ł��Ȃ��́I"));
                        break;
                    case 5:
                        if (equipItem == 3)
                        {
                            talkWindowImage.sprite = talkWindowSprite[0];
                            messageAnimation();
                            nameText.text = "";
                            StartCoroutine(CoDrawText("�o�ȃJ�[�h��n�����B"));
                            //�A�C�e�����̏o�ȃJ�[�h������
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
                        StartCoroutine(CoDrawText("\n���H���̎��g���Ă����́H"));
                        nameText.text = "�y���m�z";
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\n���肪�Ƃ��I����ɒP�ʂ�����ˁI"));
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
            case 100://�n�V�S����ɓ��ꂽ�Ƃ��̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        nameText.text = "";
                        StartCoroutine(CoDrawText("�n�V�S����ɓ��ꂽ�I"));
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
            case 101://�{����ɓ��ꂽ�Ƃ��̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�Q�l������ɓ��ꂽ�I"));
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
            case 102://�o�ȃJ�[�h����ɓ��ꂽ�Ƃ��̉�b
                switch (talkNum)
                {
                    case 1:
                        messageAnimation();
                        StartCoroutine(CoDrawText("�o�ȃJ�[�h����ɓ��ꂽ�I"));
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
                        StartCoroutine(CoDrawText("�Q�l����ǂ�ł݂悤���H\n�i�s���񐔂� 1 ����܂��j"));
                        nameText.text = "";
                        //�͂��������{�^�����o��
                        yesButtonText.text = "�͂�";
                        noButtonText.text = "������";
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
                            case 1://�ǂ�
                                //��ʂ��Â�����
                                fadeOutRectUnderTextWindow.anchoredPosition = new Vector2(0, 0);
                                fadeOutImageUnderTextWindow.DOColor(new Color(0,0,0,1),0.5f);
                                StartCoroutine(CoDrawText("�c�c�c�c�c�c�c�c�c�c�B"));
                                NumOfAct--;
                                NumOfActText.text = NumOfAct + "��";
                                break;
                            case 2://�ǂ܂Ȃ�
                                nameText.text = "�y��l���z";
                                StartCoroutine(CoDrawText("\n�ق��ɂ�邱�Ƃ�����͂����B"));
                                talkNum = 3;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        StartCoroutine(CoDrawText("�m�b�� 3 �オ�����I"));
                        playerStatus[5] += 3;
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        break;
                    case 4:
                        //��ʂ𖾂邭����
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
            case 105://����̒P�ʂ��N���b�N�����Ƃ��̃C�x���g
                switch (talkNum)
                {
                    case 1:
                        equipItemNum = equipItem;
                        nameText.text = "";
                        if (equipItemNum != 1)
                        {
                            messageAnimation();
                            StartCoroutine(CoDrawText("�؂̏�ɒP�ʂ��u����Ă���B\n�o��Ύ�ꂻ�����B"));
                        }
                        else
                        {
                            messageAnimation();
                            StartCoroutine(CoDrawText("�؂̏�ɒP�ʂ��u����Ă���B\n�o��Ύ�ꂻ�����B"));
                        }
                        break;
                    case 2:
                        if (equipItemNum != 1)
                        {
                            StartCoroutine(CoDrawText("�i�؂ɓo��ΒP�ʂ���ꂻ�����j"));
                            //�͂��������{�^�����o��
                            yesButtonText.text = "�悶�o��";
                            noButtonText.text = "���߂�";
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
                            StartCoroutine(CoDrawText("�n�V�S���g���Ė؂ɓo�����B"));
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
                                case 1://�悶�o��
                                    StartCoroutine(CoDrawText("�ؓ����g���Ė؂�o�����B"));
                                    break;
                                case 2://���߂�
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
                            //�P�ʊl��
                            StartCoroutine(getCredit());
                            CreditOnTreeRect.anchoredPosition = new Vector2(0, 3000);
                        }
                        break;
                    case 4:
                        if (equipItemNum != 1)
                        {
                            //�P�ʊl��
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
            case 1000://�퓬�̃e�L�X�g
                switch (talkNum)
                {
                    case 1:
                        StartCoroutine(CoDrawText("�ǂ�����H"));
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
                            case 1://�U������
                                audioSource.PlayOneShot(attack1SE);
                                switch (equipItem)
                                {
                                    case 0:
                                        StartCoroutine(CoDrawText("�f��ōU�������I"));
                                        break;
                                    case 1:
                                        StartCoroutine(CoDrawText("�n�V�S�ōU�������I"));
                                        break;
                                    case 2:
                                        StartCoroutine(CoDrawText("�Q�l���ōU�������I"));
                                        break;
                                    case 3:
                                        StartCoroutine(CoDrawText("�o�ȃJ�[�h�ōU�������I"));
                                        break;
                                    default:
                                        break;
                                }
                                slashAnimator.SetBool("slash", true);
                                break;
                            case 2://������
                                StartCoroutine(CoDrawText("�퓬���瓦�����I"));
                                talkNum = 100;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (actNum)
                        {
                            case 1://�U������
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
                                StartCoroutine(CoDrawText(enemyName + "�� " + damage + "�̃_���[�W�I"));
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
                            case 2://������
                                StartCoroutine(CoDrawText("������e�L�X�g�e�X�g�Q"));
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
                        StartCoroutine(CoDrawText("���Ȃ���" + damageReceive + "�̃_���[�W��H������I"));
                        PlayerHPImage.DOFillAmount(((float)playerHP / (float)(5 + playerStatus[0])),0.5f);
                        //�E�B���h�E���Ԃ�Ԃ邳����i�_���[�W��H�炤���o�j
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
                    case 50://�Q�[���I�[�o�[
                        messageAnimation();
                        StartCoroutine(CoDrawText("���ꂽ�c\n�s���񐔂� 1 �������B"));
                        NumOfAct--;
                        NumOfActText.text = NumOfAct + "��";
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
                    case 100://�퓬�ɏ���
                        audioSourceBGM1.Stop();
                        audioSourceBGM2.Stop();
                        audioSourceBGM3.Stop();
                        audioSourceBGM4.Stop();
                        audioSourceBGM5.Stop();
                        audioSource.PlayOneShot(fanfareSE);
                        messageAnimation();
                        StartCoroutine(CoDrawText(enemyName + "��|�����I\n���N +" + enemyID + "�@�ؓ� +" + enemyID + "�@�̗� +" + enemyID + "\n�헪 +" + enemyID + "�@�b�p +" + enemyID + "�@�m�\ +" + enemyID));
                        for (int i = 0; i < 6; i++)
                        {
                            playerStatus[i] += enemyID;
                        }
                        stManager.StatusChartUpdate(playerStatus[0], playerStatus[1], playerStatus[2], playerStatus[3], playerStatus[4], playerStatus[5]);
                        enemyImageAtBattle[enemyID - 1].DOColor(new Color(1,0,0,1),0.3f)
                            .OnComplete(() => {
                                enemyImageAtBattle[enemyID - 1].DOColor(new Color(1, 0, 0, 0), 0.3f);
                            });
                        //�}�b�v����G�̉摜������
                        enemyObjAtField[enemyID - 1].SetActive(false);
                        break;
                    case 101:
                        if (enemyID != 4)
                        {
                            //�퓬��ʂ��I������
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
                                StartCoroutine(CoDrawText("������V�Ƃ��ĒP�ʂ���ɓ��ꂽ�I"));
                                StartCoroutine(getCredit());
                            }
                            else
                            {
                                //�퓬��ʂ��I������
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
                        //�퓬��ʂ��I������
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
                        StartCoroutine(CoDrawText("\n��ꂽ�c�B�����͂����A�낤�B"));
                        nameText.text = "�y��l���z";
                        break;
                    case 2:
                        StartCoroutine(CoDrawText("\n����ς�P���łP�O�P�ʏW�߂�Ȃ�Ė����������񂾁c�B"));
                        break;
                    case 3:
                        nameText.text = "";
                        StartCoroutine(CoDrawText("��l���͗��N���Ă��܂����B"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("���̌�̎�l���̍s���͒N���m��Ȃ��c�B"));
                        break;
                    case 5:
                        //�E�B���h�E�������āA�Q�[���I�[�o�[�摜�ƁA���g���C�{�^�����o���B
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
                        StartCoroutine(CoDrawText("\n�������ɁA�S�Ă̒P�ʂ��W�܂����B"));
                        nameText.text = "�y��l���z";
                        break;
                    case 2:
                        talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -180), 0.5f);
                        break;
                    case 3:
                        StartCoroutine(CoDrawText("\n�v���Β������̂肾�������A���Ƃ����������邱�Ƃ��ł����B"));
                        break;
                    case 4:
                        StartCoroutine(CoDrawText("\n����ł���Ƒ��Ƃł���c"));
                        break;
                    case 5:
                        talkWindowObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -720), 0.5f);
                        talkProceedButton.SetActive(false);
                        //�z���C�g�A�E�g����Congratulation!�Ń����L���O��ʂ��o��
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
                            StartCoroutine(CoDrawText("\n�c���̌�A�����e�X�g�ł̕s�������o���A���͒P�ʎ������ɂȂ����B"));
                        });
                        break;
                    case 7:
                        StartCoroutine(CoDrawText("\n�P�ʂ͂����Ƃ������@�Ŏ�낤�B"));
                        break;
                    case 8:
                        audioSource.PlayOneShot(clapSE);
                        //FIN.�i���N�҂ɑ����j�A�����L���O�{�^���A�^�C�g���ɖ߂�{�^�����o��
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

        //�퓬�ɕ������Ƃ��̏����������ɏ���
        talkProceedButton.SetActive(false);
        talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -720);
        battleBackground.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
        battleBackground.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
        talkNum = 0;

        yield return new WaitForSeconds(0.2f);

        //�s���񐔂�0��������Q�[���I�[�o�[�C�x���g���Ă�
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

    // �e�L�X�g���k���k���o�Ă��邽�߂̃R���[�`��
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        int lenPrev = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
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
                        //���b�Z�[�W���P�������\������R���[�`��
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
        enemyName = "�n�E���h";
    }
    public void EncountSpirit()
    {
        enemyID = 2;
        BattleBeginingEnum();
        enemyName = "�X�s���b�g";
    }
    public void EncountSaramander()
    {
        enemyID = 3;
        BattleBeginingEnum();
        enemyName = "�T���}���_�[";
    }
    public void EncountDarkKnight()
    {
        enemyID = 4;
        BattleBeginingEnum();
        enemyName = "�_�[�N�i�C�g";
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
                //���������ꂽ�I�̃e�L�X�g�\��
                talkEventNum = 1000;
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                messageAnimation();
                StartCoroutine(CoDrawText(enemyName + "�����ꂽ�I"));

                doExamButton[0].SetActive(false);
                doExamButton[1].SetActive(false);
                //��������{�^�����o��
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
                enemyName = "�n�E���h";
                break;
            case 2:
                enemyHP = 15;
                enemyMaxHP = 15;
                enemyHPBarText.text = "15/15";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 30);
                enemyName = "�X�s���b�g";
                break;
            case 3:
                enemyHP = 30;
                enemyMaxHP = 30;
                enemyHPBarText.text = "30/30";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 30);
                enemyName = "�T���}���_�[";
                break;
            case 4:
                enemyHP = 60;
                enemyMaxHP = 60;
                enemyHPBarText.text = "60/60";
                enemyRectAtBattle[enemyIDRight - 1].anchoredPosition = new Vector2(200, 70);
                enemyName = "�_�[�N�i�C�g";
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
                enemyNameLeft = "�n�E���h";
                break;
            case 2:
                enemyHPLeft = 15;
                enemyMaxHPLeft = 15;
                enemyHPBarTextLeft.text = "15/15";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 30);
                enemyNameLeft = "�X�s���b�g";
                break;
            case 3:
                enemyHPLeft = 30;
                enemyMaxHPLeft = 30;
                enemyHPBarTextLeft.text = "30/30";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 30);
                enemyNameLeft = "�T���}���_�[";
                break;
            case 4:
                enemyHPLeft = 60;
                enemyMaxHPLeft = 60;
                enemyHPBarTextLeft.text = "60/60";
                enemyRectAtBattleLeft[enemyIDLeft - 1].anchoredPosition = new Vector2(-200, 70);
                enemyNameLeft = "�_�[�N�i�C�g";
                break;
            default:
                break;
        }
        HPBarLeft.anchoredPosition = new Vector2(-200,180);
        HPBarRight.anchoredPosition = new Vector2(200, 180);
        //�v���C���[HP�E�B���h�E�͂ǂ����ɏ���
        EnemyHPImage.fillAmount = 1;
        EnemyHPImageLeft.fillAmount = 1;
        goBackTitleButton.SetActive(false);
        battleBackground.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        battleBackground.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 0.5f)
            .OnComplete(() => {
                //���������ꂽ�I�̃e�L�X�g�\��
                talkWindowObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -180);
                messageAnimation();
                StartCoroutine(CoDrawText(enemyNameLeft + "�����ꂽ�I\n" + enemyName + "�����ꂽ�I"));
                //��������{�^�����o��
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
        Debug.Log("�X�e�[�^�X�A�C�R�����N���b�N���܂����B");
        if (!isOpen)
        {
            isOpen = true;
            //�X�e�[�^�X�E�B���h�E�������o��
            statusWindowRect.DOLocalMove(new Vector2(-145, 140), 0.3f);
            stWindow.DOLocalMove(new Vector2(-330, 25), 0.3f);
            statusWindowCloser.SetActive(true);
            statusText.text = " �Ƃ��� ";
            statusImage.sprite = crossSprite;
        }
        else
        {
            isOpen = false;
            statusWindowRect.DOLocalMove(new Vector2(-445, 140), 0.3f);
            stWindow.DOLocalMove(new Vector2(-630, 25), 0.3f);
            statusWindowCloser.SetActive(false);
            statusText.text = "�X�e�[�^�X";
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
        CreditText.text = "���Ƃ܂ł���" + (10 - credit) + "�P��";
        getCreditText.text = "2�P�ʊl��";
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

        //10�P�ʏW�߂��Ȃ�A�Q�[���N���A�C�x���g���Ă�
        if (credit >= 10)
        {
            //�S�ẴC�x���g�𒆒f
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
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-���w��-", 1);
        departSelectUpdate(1);
        audioSource.PlayOneShot(decideSE);
        departNumber = 1;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 10; playerStatus[5] = 7;
    }
    public void OnClickDepart2()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-�@�w��-", 2);
        departSelectUpdate(2);
        audioSource.PlayOneShot(decideSE);
        departNumber = 2;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 7; playerStatus[4] = 5; playerStatus[5] = 10;
    }
    public void OnClickDepart3()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-�o�ϊw��-", 3);
        departSelectUpdate(3);
        audioSource.PlayOneShot(decideSE);
        departNumber = 3;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 5; playerStatus[3] = 10; playerStatus[4] = 7; playerStatus[5] = 5;
    }
    public void OnClickDepart4()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-���w��-", 4);
        departSelectUpdate(4);
        audioSource.PlayOneShot(decideSE);
        departNumber = 4;
        playerStatus[0] = 5; playerStatus[1] = 5; playerStatus[2] = 7; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 10;
    }
    public void OnClickDepart5()
    {
        stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-�H�w��-", 5);
        departSelectUpdate(5);
        audioSource.PlayOneShot(decideSE);
        departNumber = 5;
        playerStatus[0] = 7; playerStatus[1] = 10; playerStatus[2] = 5; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 5;
    }
    public void OnClickDepart6()
    {
        stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-��w��-", 6);
        departSelectUpdate(6);
        audioSource.PlayOneShot(decideSE);
        departNumber = 6;
        playerStatus[0] = 10; playerStatus[1] = 5; playerStatus[2] = 7; playerStatus[3] = 5; playerStatus[4] = 5; playerStatus[5] = 5;
    }
    public void departSelectUpdate(int departID)
    {
        //�e�{�^����Outline�R���|�\�Nt���擾���āAeffectColor��ύX����
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
        //�Ó]�����āA�v�����[�O��ʂ������āA�C�x���g�O���N������
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
        stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-���w��-", 1);
        ButtonEnter(departIcon[0]);
    }
    public void OnEnterDepart2()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-�@�w��-", 2);
        ButtonEnter(departIcon[1]);
    }
    public void OnEnterDepart3()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-�o�ϊw��-", 3);
        ButtonEnter(departIcon[2]);
    }
    public void OnEnterDepart4()
    {
        stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-���w��-", 4);
        ButtonEnter(departIcon[3]);
    }
    public void OnEnterDepart5()
    {
        stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-�H�w��-", 5);
        ButtonEnter(departIcon[4]);
    }
    public void OnEnterDepart6()
    {
        stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-��w��-", 6);
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
                stManager.StatusChartUpdatePrologue(5, 5, 5, 5, 10, 7, "-���w��-", 1);
                departSelectUpdate(1);
                break;
            case 2:
                stManager.StatusChartUpdatePrologue(5, 5, 5, 7, 5, 10, "-�@�w��-", 2);
                departSelectUpdate(2);
                break;
            case 3:
                stManager.StatusChartUpdatePrologue(5, 5, 5, 10, 7, 5, "-�o�ϊw��-", 3);
                departSelectUpdate(3);
                break;
            case 4:
                stManager.StatusChartUpdatePrologue(5, 5, 7, 5, 5, 10, "-���w��-", 4);
                departSelectUpdate(4);
                break;
            case 5:
                stManager.StatusChartUpdatePrologue(7, 10, 5, 5, 5, 5, "-�H�w��-", 5);
                departSelectUpdate(5);
                break;
            case 6:
                stManager.StatusChartUpdatePrologue(10, 5, 7, 5, 5, 5, "-��w��-", 6);
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
        //�{���{�n�b�V���^�O���Q�c�C�[�g�i�摜�Ȃ��j
        switch (departNumber)
        {
            case 1:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "���w����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            case 2:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "�@�w����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            case 3:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "�o�ϊw����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            case 4:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "���w����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            case 5:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "�H�w����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            case 6:
                naichilab.UnityRoomTweet.Tweet("escapefrommetrouniv", "��w����" + (20 - NumOfAct) + "�^�[���ŒE�o���܂����B", "�s����w����̒E�o");
                break;
            default:
                break;
        }
    }
    public void aOnClickRankingButton()
    {
        //�����L���O��ʂ��ĂԃR�[�h�������ɏ����i�������j
    }
}
