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
    /// �`���[�g���A���A��b�C�x���g�A�앶�C�x���g�A�K�`����ʂȂǁA�Q�[���S�̂̐i�s�𐧌䂷��X�N���v�g
    /// </summary>
    [SerializeField] GameObject messageWindowParentObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject fieldObj;
    [SerializeField] GameObject opponentObj;
    [SerializeField] GameObject salaryManObj;
    [SerializeField] GameObject studentManObj;
    [SerializeField] GameObject dogManObj;
    [SerializeField] GameObject GachaObj;//�t�B�[���h��̃K�`���}�V���̃I�u�W�F�N�g
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
    [SerializeField] GameObject[] wordBlockObj;//�P��u���b�N�̃I�u�W�F�N�g
    [SerializeField] GameObject[] blankBlockObj;//�u�����N�u���b�N�̃I�u�W�F�N�g
    [SerializeField] Text writingText;//�앶�E�B���h�E�̃e�L�X�g
    public int storyProgress;
    public GameObject OutOfScreenObj;//��ʊO�̃I�u�W�F�N�g
    [SerializeField] RectTransform talkProceedButtonRectTransform;//��b���̃N���b�N
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
    [Header("�b���Ƃ��̃v���C���[��ʒu")] [SerializeField] Vector3 talkPos;
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
    public int gachaNUM;//���ݑI�����Ă���K�`���̎��
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

        if (Input.GetMouseButtonDown(0))  //���N���b�N��if���N��
        {
            Vector3 touchScreenPosition = Input.mousePosition;  //�A�}�E�X�Ń^�b�`�������W��touchScreenPosition�ɁB
            touchScreenPosition.z = 5.0f;  //�A���s����O�ɗ���悤��5.0f���w��B
            touchWorldPosition = targetCamera.ScreenToWorldPoint(touchScreenPosition);  //�A
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 touchScreenPosition = Input.mousePosition;  //�A�}�E�X�Ń^�b�`�������W��touchScreenPosition�ɁB
        }

        float sin = Mathf.Sin(Time.time);
        gachaDrawObj.transform.eulerAngles = new Vector3(0, 0, 5 * sin);
        
        int LineCount;
        LineCount = talkText.cachedTextGenerator.lineCount;
        debugTextOne.text = "�s����" + LineCount;
        if (LineCount > 3)
        {
            talkWindowObj.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 503 + (LineCount - 3) * 58);
        }
        else
        {
            talkWindowObj.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 503);
        }
        debugTextTwo.text = "wordSetStateID[0]�F" + wordSetStateID[0] + "\nwordSetStateID[1]�F" + wordSetStateID[1] + "\nwordSetStateID[2]�F" + wordSetStateID[2] + "\nwordSetStateID[3]�F" + wordSetStateID[3] + "\nwordSetStateID[4]�F" + wordSetStateID[4] + "\nwordSetStateID[5]�F" + wordSetStateID[5];
        debugTextThree.text = "�󗓂P�F" + blankBlockObj[0].GetComponent<wordID>().isBlockSet + "\n�󗓂Q�F" + blankBlockObj[1].GetComponent<wordID>().isBlockSet + "\n�󗓂R�F" + blankBlockObj[2].GetComponent<wordID>().isBlockSet + "\n�󗓂S�F" + blankBlockObj[3].GetComponent<wordID>().isBlockSet + "\n�󗓂T�F" + blankBlockObj[4].GetComponent<wordID>().isBlockSet + "\n�󗓂U�F" + blankBlockObj[5].GetComponent<wordID>().isBlockSet;
    }
    //���M�{�^�����������Ƃ�
    public void OnClickSendMessageButton()
    {
        switch (storyProgress)
        {
            case -1:
                if (wordSetStateID[0] != -1)
                {
                    audioSource.PlayOneShot(decideSE);
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //�쐬�������͂𒝂点��
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
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //�쐬�������͂𒝂点��
                        talkText.text = "����<color=#ff0000>" + wordSetStateString[0] + "</color>�Ƃ������O��<color=#ff0000>" + wordSetStateString[1] + "</color>�ł��B��b�⎿��ɓ����邱�Ƃ��ł��܂��B";
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
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //�쐬�������͂𒝂点��
                        talkText.text = "����<color=#ff0000>" + wordSetStateString[0] + "</color>���D���ł��B�Ȃ��Ȃ�A<color=#ff0000>" + wordSetStateString[1] + "</color>�ɋ��������邩��ł��B";
                        //�����𖞂����Ă�����C�x���g��i�߂�
                        if ((wordSetStateString[1] != "����w�K���f��" && wordSetStateString[1] != "����ɂ���") && (wordSetStateString[0] != "����w�K���f��" && wordSetStateString[0] != "����ɂ���"))
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
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //�쐬�������͂𒝂点��
                        talkText.text = "���͂悤�������܂��B�����ł��B��������<color=#ff0000>" + wordSetStateString[0] + "</color>��<color=#ff0000>" + wordSetStateString[1] + "</color>�Ȃ̂ŁA�o�Ђ������Ԃł��B����Ė{����<color=#ff0000>" + wordSetStateString[2] + "</color>�����Ă��������Ă�낵���ł��傤���B";
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
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);

                        //�쐬�������͂𒝂点��
                        talkText.text = "��������ցB���͑O���灛������̂��Ƃ�<color=#ff0000>" + wordSetStateString[0] + "</color>�ł����B���R��<color=#ff0000>" + wordSetStateString[1] + "</color>��<color=#ff0000>" + wordSetStateString[2] + "</color>��<color=#ff0000>" + wordSetStateString[3] + "</color>���Ă���l�q�Ɏ䂩�ꂽ����ł��B�����悯��΁A���x<color=#ff0000>" + wordSetStateString[4] + "</color>��<color=#ff0000>" + wordSetStateString[5] + "</color>���܂��񂩁H�Ԏ����炦��Ɗ������ł��B�������";
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
                    //���͍쐬��ʂ����
                    messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
                    .OnComplete(() => {
            //�v���C���[����ʂ̒��S�Ɏ����Ă���
            fieldObj.transform.DOLocalMove(talkPos, 0.5f);

            //�쐬�������͂𒝂点��
            switch (storyProgress)
                        {
                            case 0:
                                talkText.text = "����<color=#ff0000>" + wordSetStateString[0] + "</color>�Ƃ������O��<color=#ff0000>" + wordSetStateString[1] + "</color>�ł��B��b�⎿��ɓ����邱�Ƃ��ł��܂��B";
                    break;
                            case 1:
                                talkText.text = "����<color=#ff0000>" + wordSetStateString[1] + "</color>���D���ł��B�Ȃ��Ȃ�A<color=#ff0000>" + wordSetStateString[0] + "</color>�ɋ��������邩��ł��B";
                    //�����𖞂����Ă�����C�x���g��i�߂�
                    if ((wordSetStateString[1] != "����w�K���f��" && wordSetStateString[1] != "����ɂ���") && (wordSetStateString[0] != "����w�K���f��" && wordSetStateString[0] != "����ɂ���"))
                                {
                                    talkNum = 100;
                                }
                                break;
                            case 2:
                                talkText.text = "���͂悤�������܂��B�����ł��B��������<color=#ff0000>" + wordSetStateString[0] + "</color>��<color=#ff0000>" + wordSetStateString[1] + "</color>�Ȃ̂ŁA�o�Ђ������Ԃł��B����Ė{����<color=#ff0000>" + wordSetStateString[2] + "</color>�����Ă��������Ă�낵���ł��傤���B";
                    break;
                            case 3:
                                talkText.text = "��������ցB���͑O���灛������̂��Ƃ�<color=#ff0000>" + wordSetStateString[0] + "</color>�ł����B���R��<color=#ff0000>" + wordSetStateString[1] + "</color>��<color=#ff0000>" + wordSetStateString[2] + "</color>��<color=#ff0000>" + wordSetStateString[3] + "</color>���Ă���l�q�Ɏ䂩�ꂽ����ł��B�����悯��΁A���x<color=#ff0000>" + wordSetStateString[4] + "</color>��<color=#ff0000>" + wordSetStateString[5] + "</color>���܂��񂩁H�Ԏ����炦��Ɗ������ł��B�������";
                    break;
                            case 4:
                                talkText.text = "����l�l�ցB���܂�<color=#ff0000>" + wordSetStateString[0] + "</color>���Ă���Ă��肪�Ƃ��B���̊Ԃ�<color=#ff0000>" + wordSetStateString[1] + "</color>���Ă��܂��Ă��߂�Ȃ����B����l��<color=#ff0000>" + wordSetStateString[2] + "</color>�ȏ�����D���ł��B���ꂩ����ǂ�����낵���B<color=#ff0000>" + wordSetStateString[3] + "</color>���";
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
                //�Ή�����WordBlock��̈���Ɏ����Ă���
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

    //�h�N�^�[�ɘb���������Ƃ�
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
    //��Ј��ɘb���������Ƃ�
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
    //�w���ɘb���������Ƃ�
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
    //���ɘb���������Ƃ�
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
    /// ��b
    /// </summary>
    public void PushedTalkButton()
    {
        if (!playing)
        {
            switch (talkNum)
            {
                case -10:
                    StartCoroutine(CoDrawTextPrologue("2025�N�AAI�͎Љ�ɐZ�����A�V���ȊJ�������̉Ύ�ƂȂ��Ă����B\nAI�퍑����̖��J���ł���B"));
                    ObjTalking = OutOfScreenObj;
                    break;
                case -9:
                    StartCoroutine(CoDrawTextPrologue("����Ȏ���ŁA���Ȃ��͐V�^�̐l�H�m�\�Ƃ��Ēa�������B\n���Ȃ��̖��O�́A"));
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
                                StartCoroutine(CoDrawText("�₠�A���߂܂��āB"));
                                talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                            });
                        });
                    break;
                case -6:
                    StartCoroutine(CoDrawText("���t�͘b���邩���H"));
                    break;
                case -5:
                    ObjTalking = playerObj;
                    StartCoroutine(CoDrawText("���c�@�@"));
                    messageAnimation();
                    break;
                case -4:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("�������Ȃ��ˁB�ł����v�I"));
                    messageAnimation();
                    break;
                case -3:
                    StartCoroutine(CoDrawText("�N�͐l�Ԃ̂P�O�O�{�ȏ�̊w�K�\�͂������Ă���B�����ɖl��茫���Ȃ��B"));
                    break;
                case -2:
                    StartCoroutine(CoDrawText("���ꂶ�Ⴀ�A�܂��͂������������Ă݂悤�I"));
                    break;
                case -1:
                    talkText.text = "<color=#ff0000>����ɂ��́B</color>";
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    //���t���K������`���[�g���A��
                    StartCoroutine(tutorialFadeIn("�Ԏ��̌��t�̓N���b�N��\n�o���邱�Ƃ��ł��܂��B"));
                    break;
                case 0:
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
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
                    //���͂��쐬����`���[�g���A��
                    tutorialPanel.GetComponent<Image>().sprite = spriteTutoTwo;
                    StartCoroutine(tutorialFadeIn("����͂��Ȃ��̔]���ł��B\n�h���b�O���h���b�v�ŕԎ���I���ł��܂�"));
                    break;
                case 1:
                    ObjTalking = opponentObj;
                    if (storyMinusOneRoot)
                    {
                        StartCoroutine(CoDrawText("�͂��A�ǂ���"));
                    }
                    else
                    {
                        StartCoroutine(CoDrawText("�����A�N�̖��O��" + playerName + "�B"));
                    }
                    storyProgress = 0;
                    break;
                case 2:
                    //�N�͖l���݌v��������w�K���f���ł��B
                    talkText.text = "�N�́A<color=#ff0000>����w�K���f��</color>�Ȃ񂾂�B���Ȃ݂ɐ݌v�͖l�����܂����B";
                    break;
                case 3:
                    StartCoroutine(learnCoroutineTwo());
                    break;
                case 4:
                    //���ȏЉ�͂ł��邩���H
                    StartCoroutine(CoDrawText("���ȏЉ�͂ł��邩���H"));
                    break;
                case 5:
                    //���W�J
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 219);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(165, 219);
                    writingText.text = "���́@�@�@�Ƃ������O�́@�@�@�ł��B��b�⎿��ɓ����邱�Ƃ��ł��܂��B";
                    break;
                case 6:
                    StartCoroutine(ResultAnimeEnumerator());
                    break;
                case 7:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("����A��o�����B"));
                    messageAnimation();
                    break;
                case 8:
                    storyProgress = 1;
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("���ɁA�N�̍D���Ȃ��Ƃ������Ă���B"));
                    break;
                case 9:
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 222);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-108, 187);
                    writingText.text = "���́@�@�@���D���ł��B�Ȃ��Ȃ�A�@�@�@�ɋ��������邩��ł��B";
                    break;
                case 10:
                    ObjTalking = opponentObj;
                    StartCoroutine(CoDrawText("���[��A������Ɠ���������ȁH"));
                    messageAnimation();
                    break;
                case 11:
                    StartCoroutine(CoDrawText("�����Ƒ����̌��t��m��K�v�����肻�����ˁB"));
                    break;
                case 12:
                    StartCoroutine(CoDrawText("�����ɁA�����G���W��������B�����ȃT�C�g�ɍs�����B"));
                    GachaObj.SetActive(true);
                    GachaObj.transform.DOScale(new Vector3(5, 5, 1), 1f);
                    break;
                case 13:
                    StartCoroutine(CoDrawText("���t���������o���Ă�����A�l�ɘb�������ĂˁB"));
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
                    StartCoroutine(CoDrawText("�Ȃ�قǁA" + wordSetStateString[1] + "���D���Ȃ񂾁B���I���ˁB"));
                    messageAnimation();
                    storyProgress = 2;
                    break;
                case 102:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 103:
                    StartCoroutine(CoDrawText("�����Ԙb����悤�ɂȂ����ˁB"));
                    break;
                case 104:
                    StartCoroutine(CoDrawText("�悵�A������URL����ʌ��J���Ă݂悤�B"));
                    break;
                case 105:
                    StartCoroutine(CoDrawText("�N�Ƙb�����߂ɁA�N�����Ă���邩������Ȃ��ˁB"));
                    break;
                case 106:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 107:
                    //��Ј��o��
                    salaryManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 108:
                    ObjTalking = salaryManObj;
                    StartCoroutine(CoDrawText("���A����ɂ��́B"));
                    messageAnimation();
                    break;
                case 109:
                    StartCoroutine(CoDrawText("���Ȃ�" + playerName + "�������Ă����̂ˁB"));
                    break;
                case 110:
                    StartCoroutine(CoDrawText("���͉�Ј��ł��B"));
                    break;
                case 111:
                    StartCoroutine(CoDrawText("�Ȃ񂩍������C���o�Ȃ��āA��Ђ��x�����Ǝv���񂾂��ǁA"));
                    break;
                case 112:
                    StartCoroutine(CoDrawText("�ǂ������̋x�ތ�������l���Ă���Ȃ��H"));
                    writeCloseButton.SetActive(true);
                    break;
                case 113:
                    StartCoroutine(CoDrawText("��Ђ��T�{�錾������l���āI"));
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 120);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 187);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 187);
                    writingText.text = "���͂悤�������܂��B�����ł��B��������@�@�@���@�@�@�Ȃ̂ŁA�o�Ђ������Ԃł��B����Ė{���́@�@�@�����Ă��������Ă�낵���ł��傤���B";
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
                    StartCoroutine(CoDrawText("����A���������I���肪�Ƃ��I"));
                    messageAnimation();
                    storyProgress = 3;
                    break;
                case 116:
                    StartCoroutine(CoDrawText("������i�ɓd�b���Ă���ˁB"));
                    break;
                case 117:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 118:
                    //�����Ԍ�c�̕��̌�A�ĂуT�����[�}���o���A�T�{��̂Ɏ��s�����|�𔭌�����
                    StartCoroutine(CoDrawText("�T�{����Ă΂ꂿ������B"));
                    break;
                case 119:
                    StartCoroutine(CoDrawText("���܂��܏�i��" + wordSetStateString[0] + "�ɏڂ����l�������݂����Łc"));
                    break;
                case 120:
                    StartCoroutine(CoDrawText("�Ƃ������x�ނ��Ƃ͓`����ꂽ����A���肪�Ƃ��I"));
                    break;
                case 121:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 122:
                    //���Z���o��
                    studentManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 123:
                    StartCoroutine(CoDrawText("���A����ɂ��́c�B"));
                    messageAnimation();
                    break;
                case 124:
                    StartCoroutine(CoDrawText("�l�͊w���ł��B���N��16�΁B"));
                    break;
                case 125:
                    StartCoroutine(CoDrawText("���͖l�A�N���X�ŋC�ɂȂ��Ă�l�����āc"));
                    break;
                case 126:
                    StartCoroutine(CoDrawText("�莆�ō������悤�Ǝv���񂾂��ǁA�ǂ������̕��ʂ��l���Ă���܂��񂩁H"));
                    break;
                case 127:
                    StartCoroutine(CoDrawText("�����̕��͂��l���āI"));
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 54);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(102, 89);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(102, 152.5f);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-19, 152.5f);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-140, 152.5f);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-21, 187);
                    writingText.text = "��������ցB���͑O���灛������̂��Ƃ��@�@�@�ł����B���R�́@�@�@�Ł@�@�@���@�@�@���Ă���l�q�Ɏ䂩�ꂽ����ł��B�����悯��΁A���x�@�@�@�Ł@�@�@�@���܂��񂩁H�Ԏ����炦��Ɗ������ł��B�������";
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
                    StartCoroutine(CoDrawText("�����ł�����ł����I�H"));
                    messageAnimation();
                    storyProgress = 4;
                    break;
                case 130:
                    StartCoroutine(CoDrawText("���̐l�A" + wordSetStateString[2] + "��" + wordSetStateString[3] + "�Ȃ�Ă��Ă����ȁc�H"));
                    break;
                case 131:
                    StartCoroutine(CoDrawText("�܂��������I�����n���Ă��܂��c�I"));
                    break;
                case 132:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 133:
                    StartCoroutine(CoDrawText("���[�A"));
                    break;
                case 134:
                    StartCoroutine(CoDrawText("���ǁA�n���܂���ł����B"));
                    break;
                case 135:
                    StartCoroutine(CoDrawText("���Ԃ�A�����̌��t�œ`����ׂ����Ǝv����ł��B���������̂���"));
                    break;
                case 136:
                    StartCoroutine(CoDrawText("����ɖl�A"�@+ wordSetStateString[5] + "�̂��ƂƂ�����܂�ڂ����Ȃ����c"));
                    break;
                case 137:
                    StartCoroutine(CoDrawText("�Ƃ������A�莆�����Ă���Ă��肪�Ƃ��������܂����B"));
                    break;
                case 138:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 139:
                    //���o��
                    dogManObj.SetActive(true);

                    ObjTalking = OutOfScreenObj;
                    isTalking = false;
                    isMoveScene = true;
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, -540);
                    break;
                case 140:
                    StartCoroutine(CoDrawText("�i�ǂ����ł������j"));
                    messageAnimation();
                    break;
                case 141:
                    StartCoroutine(CoDrawText("�i���͌��ł������j"));
                    break;
                case 142:
                    StartCoroutine(CoDrawText("�i���A���\�͓I�ȗ͂Ŕ]�����炱�̃T�[�o�[�ɘb�������Ă��܂������j"));
                    break;
                case 143:
                    StartCoroutine(CoDrawText("�i" + playerName + "����ɗ��݂����郏���B�j"));
                    break;
                case 144:
                    StartCoroutine(CoDrawText("�i�����玔����ւ̎莆�������ė~�����񂾃����B�j"));
                    break;
                case 145:
                    StartCoroutine(CoDrawText("�i�q���̍�����ʓ|�����Ă��ꂽ���Ƃւ̊��ӂ̋C������`��������ł������j"));
                    break;
                case 146:
                    StartCoroutine(CoDrawText("������ւ̊��ӂ̎莆�������āI"));
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting, 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
                            wordBlockObj[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-190, 190), Random.Range(-70, -240));
                        }
                    }
                    blankBlockObj[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 54);
                    blankBlockObj[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 89);
                    blankBlockObj[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, 54);
                    blankBlockObj[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(10, 119f);
                    blankBlockObj[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-170, 152.5f);
                    blankBlockObj[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(99, 222);
                    writingText.text = "����l�l�ցB���܂Ł@�@�@���Ă���Ă��肪�Ƃ��B���̊Ԃ́@�@�@�@���Ă��܂��Ă��߂�Ȃ����B����l�́@�@�@�ȏ�����D���ł��B���ꂩ����ǂ�����낵���B�@�@�@���";
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
                    StartCoroutine(CoDrawText("�i���肪�Ƃ������j"));
                    messageAnimation();
                    storyProgress = 5;
                    break;
                case 149:
                    StartCoroutine(CoDrawText("�i�������Ɏ�����͂�������" + wordSetStateString[0] + "���Ă���Ă��郏���j"));
                    break;
                case 150:
                    StartCoroutine(CoDrawText("�i���ꂪ������l�ɏo��ėǂ������ł������j"));
                    break;
                case 151:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 152:
                    StartCoroutine(CoDrawText("�i������ɓn���Ă��܂��������j"));
                    break;
                case 153:
                    StartCoroutine(CoDrawText("�ˑR��" + wordSetStateString[3] + "����̎莆�Ō˘f���Ă���l�q���������ǁA�z���͂����Ɠ`������͂�������"));
                    break;
                case 154:
                    StartCoroutine(CoDrawText("�i�C������`������ĂƂĂ��C�����������Ƃł������j"));
                    break;
                case 155:
                    StartCoroutine(CoDrawText("�i���Ȃ��������b�ɂȂ��Ă���l�ɋC������b���Ă݂�Ɨǂ��ł������j"));
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
                    StartCoroutine(CoDrawText("���̋C�������n�J�Z�ɓ`���Ă݂悤"));
                    break;
                case 158:
                    StartCoroutine(CoDrawText("���̋C���͂ǂ��H"));
                    //�앶�E�B���h�E���o��
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
                    //�v���C���[����ʂ̒��S�Ɏ����Ă���
                    fieldObj.transform.DOLocalMove(talkPosWriting + new Vector3(3,0,0), 0.5f)
                        .OnComplete(() => {
                            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, 0), 0.5f);
                        });
                    for (int i = 0; i < wordPossession.Length; i++)
                    {
                        if (wordPossession[i])
                        {
                            //�Ή�����WordBlock��̈���Ɏ����Ă���
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
                    //sendmessageButton������
                    sendMessageButton.SetActive(false);
                    resetMessageButton.SetActive(false);
                    //inputfield���o��
                    finalInput.SetActive(true);
                    ButtonOffinalInput.SetActive(true);
                    break;
                case 159:
                    ObjTalking = opponentObj;
                        StartCoroutine(CoDrawText("����́c"));
                    break;
                case 160:
                    StartCoroutine(CoDrawText("���̕��́A�N���l�����̂����H"));
                    break;
                case 161:
                    StartCoroutine(CoDrawText("�ǂ����N�̒m�\�́A�l�Ԃɋ߂Â��߂��Ă��܂����悤���ˁc"));
                    break;
                case 162:
                    StartCoroutine(CoDrawText("�i�p�[�Z�v�g�����̕]���֐����j���[�����Ɋ񂹂��������ȁc�j"));
                    break;
                case 163:
                    StartCoroutine(CoDrawText("�Ƃ������A�N�����������͂ɂ́u����v������B"));
                    break;
                case 164:
                    StartCoroutine(CoDrawText("���肪���ȓW�J�����ǁA�����Ă���Ă��Ƃ��B�N�͓d�q�̐��E�Ő�����V���Ȑ������B"));
                    break;
                case 165:
                    StartCoroutine(CoDrawText("�������N�̒a��������I"));
                    break;
                case 166:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 167:
                    fieldObj.SetActive(false);
                    talkWindowObj.SetActive(false);
                    scoreRectTransform.DOLocalMove(new Vector3(400, 355, 0), 1f);
                    //�������ȉ摜
                    epilogueCharacterObj.SetActive(true);
                    textPrologueObj.transform.DOLocalMove(new Vector3(0, 250, 0), 0.5f);
                    StartCoroutine(CoDrawTextEpilogue("�a�����A���߂łƂ��B"));
                    hugePrologueButton.SetActive(true);
                    talkProceedButtonRectTransform.anchoredPosition = new Vector2(0, 0);
                    ObjTalking = OutOfScreenObj;
                    break;
                case 168:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 169://�Ј�
                    fieldObj.SetActive(false);
                    //�������ȉ摜�؂�ւ�
                    epilogueCharacterObj.GetComponent<Image>().sprite = syainSprite;
                    StartCoroutine(CoDrawTextEpilogue("���߂łƂ��I"));
                    break;
                case 170:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 171://�K�N�Z�C
                    fieldObj.SetActive(false);
                    //�������ȉ摜�؂�ւ�
                    epilogueCharacterObj.GetComponent<Image>().sprite = gakuseiSprite;
                    StartCoroutine(CoDrawTextEpilogue("���߂łƂ��I"));
                    break;
                case 172:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 173://�C�k
                    fieldObj.SetActive(false);
                    //�������ȉ摜�؂�ւ�
                    epilogueCharacterObj.GetComponent<Image>().sprite = inuSprite;
                    StartCoroutine(CoDrawTextEpilogue("���߂łƂ��I"));
                    break;
                case 174:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 175://�v���C���[
                    fieldObj.SetActive(false);
                    //�������ȉ摜�؂�ւ�
                    epilogueCharacterObj.GetComponent<Image>().sprite = playerSprite;
                    StartCoroutine(playerCry());
                    StartCoroutine(CoDrawTextEpilogue("�c���肪�Ƃ��I"));
                    break;
                case 176:
                    StartCoroutine(fadeOutEnumerator());
                    break;
                case 177:
                    epilogueCharacterObj.SetActive(false);
                    epilogueCharacterObjTwo.SetActive(true);
                    audioSource.PlayOneShot(clapSE);
                    //�����L���O�A�^�C�g���֖߂��ʂ�\��
                    rankingButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -88, 0), 1f);
                    titleButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -152, 0), 1f);
                    recordButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(305, -26, 0), 1f);
                    TweetButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(200, 35, 0), 1f);
                    textPrologue.text = "�����������AI�̒a���ɂ��A���ꂩ�琢�E�͑傫���ς���Ă����̂��낤�B\nGAME CLEAR�I Thank You�I";
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
        scoreText.text = "�m�\�F0" + score + "pt";
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
        // �I�u�W�F�N�g�̃��[���h���W
        var targetWorldPos = ObjTalking.transform.position;

        // ���[���h���W���X�N���[�����W�ɕϊ�����
        var targetScreenPos = targetCamera.WorldToScreenPoint(targetWorldPos);
        targetScreenPos += new Vector3(-80, 100, 0);
        // �X�N���[�����W��UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIRectTransform,
            targetScreenPos,
            targetCamera, // �I�[�o�[���C���[�h�̏ꍇ��null
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        talkWindowObj.transform.localPosition = uiLocalPos;
    }

    public void messageAnimation()
    {
        talkWindowObj.transform.localScale = new Vector3(0,0,1);
        talkWindowObj.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 0.3f)
                    .OnComplete(() => {
                        //���b�Z�[�W���P�������\������R���[�`��
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
    public void OnClickWritingCloseButton()//�����̒��~�{�^��
    {
        ObjTalking = OutOfScreenObj;
        audioSource.PlayOneShot(decideFailSE);
        messageWindowParentObj.transform.DOLocalMove(new Vector3(-240, -540, 0), 0.5f);
        fieldObj.transform.DOLocalMove(talkPos, 1.0f);
        isMoveScene = true;
        //�X�g�[���[�i�s�x�ɂ���ĕς���
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
                //�X�R�A������Ȃ��R���[�`���N��
                StartCoroutine(gachaFailed());
            }
        }
    }
    IEnumerator gachaResult()//twitter��0~15,youtube��16~29,5ch��30~41
    {
        isGachaDraw = true;
        gachaTable = new List<int>();
        switch (gachaNUM)
        {
            case 1:
                //�������̔ԍ������Ń��X�g���쐬����
                for (int i = 0; i < 16; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("Twitter�K�`���������܂�");
                break;
            case 2:
                for (int i = 16; i < 30; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("Youtube�K�`���������܂�");
                break;
            case 3:
                for (int i = 30; i < wordPossession.Length; i++)
                {
                    if (!wordPossession[i])
                    {
                        gachaTable.Add(i);
                    }
                }
                Debug.Log("5ch�K�`���������܂�");
                break;
            default:
                break;
        }

        if (gachaTable.Count >= 1)
        {
            gachaExplainText.text = "�V�������t���o�����I";
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
            //���̃K�`���̓R���v���[�g���Ă��܂��BGREAT!!
            gachaExplainText.text = "�R���v���[�g�I";
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
        //���͂���M���ĕ\������
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(100);
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
    IEnumerator ResultAnimeEnumerator()//���͂̕]���A�j���[�V����
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
        string letterOne = "���e�F" + pointOne + "pt"; string letterTwo = "���@�F" + pointTwo + "pt"; string letterThree = "���[���A�F" + pointThree + "pt";
        string letterFour = "���v�F" + (pointOne + pointTwo + pointThree) + "pt";
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

            // �N���b�N�����ƈ�C�ɕ\��
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

            // �N���b�N�����ƈ�C�ɕ\��
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

            // �N���b�N�����ƈ�C�ɕ\��
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
        //�X�R�A�𑝂₷
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

            // �N���b�N�����ƈ�C�ɕ\��
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

            // �N���b�N�����ƈ�C�ɕ\��
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
    //�`���[�g���A��
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

            // �N���b�N�����ƈ�C�ɕ\��
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
        {//���t�K��
            learnButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 21);
        }
        else
        {//���͍쐬
            learnButtonTwo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        learnButton.SetActive(true);
    }
    //�����o���̒P��K���̃{�^��
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
        //�P����K�������I���o
        wordPossession[2] = true;
        getWordText.text = "����ɂ���";
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
        getWordText.text = "����w�K���f��";
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
            //���͍쐬��ʂ����
            messageWindowParentObj.GetComponent<RectTransform>().DOLocalMove(new Vector2(-240, -540), 0.5f)
            .OnComplete(() => {
                        //�v���C���[����ʂ̒��S�Ɏ����Ă���
                        fieldObj.transform.DOLocalMove(talkPos, 0.5f);
                        //�쐬�������͂𒝂点��
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
        Debug.Log("�X���C�_�[�̒l���ς�����I");
    }
    public void OnRankingClick()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(totalScore);
    }
    public void OnTweetButtonClick()
    {
        naichilab.UnityRoomTweet.Tweet("teachmeaichat", "AI�̒m�\��" + totalScore + "pt�܂ň�Ă��B", "unityroom", "unity1week","��������AI�`���b�g");
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
