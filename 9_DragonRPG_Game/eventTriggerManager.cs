using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class eventTriggerManager : MonoBehaviour
{
    /// <summary>
    /// ��b�C�x���g��󔠁A�ŔɐG�ꂽ�Ƃ��̓�����s���X�N���v�g
    /// </summary>
    public GameObject cartain1;
    public GameObject cartain2;
    public int eventNum;
    public bool recast;

    public GameObject TextWindow;
    public GameObject[] faceImageTalk;
    public Text textOfTalk;
    public int talkNum;//��b�C�x���g�̐i�ݓx
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
    public AudioClip SE;//���b�Z�[�W����SE
    public AudioClip SE2;//�A�C�e���l��SE

    public GameObject ItemGetWindow;
    public Text ItemGetText;
    public bool isItemGetScene;
    public AudioClip SE3;//���Ԃ��������Ƃ���SE

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
            //��b��i�߂镶
            talkEvent();
        }
        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && stageManager.isEventScene && !recast && isItemGetScene)
        {
            //�A�C�e���l���E�B���h�E�����
            recast = true;
            StartCoroutine("recastNormal");
            isItemGetScene = false;
            ItemGetWindow.SetActive(false);
            SavedWindow.SetActive(false);
            //�Ŕ�������
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
            case 1://�h���S���Əo��C�x���g
                //��b�E�B���h�E���o��
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
                //�e�L�X�g��ς���
                textOfTalk.text = "�����̐l�ԁI";
                break;
            case 2://�h���S���𒇊Ԃɂ���C�x���g
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
                //�e�L�X�g��ς���
                textOfTalk.text = "�Ђ����I�����Ă����������I";
                break;
            case 3://�p���_�Əo��C�x���g
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
                //�e�L�X�g��ς���
                textOfTalk.text = "�����f�J���̂�����ȁc";
                break;
            case 4://�p���_��|�����C�x���g
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
                textOfTalk.text = "��邶��Ȃ����c���̕������B";
                break;
            case 5://���߂ĊC�ɗ����C�x���g
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
                textOfTalk.text = "�C���Iῂ����I";
                break;
            case 6://���Ɖ�C�x���g
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
                textOfTalk.text = "�َ҂͎��c";
                break;
            case 7://���𒇊Ԃɂ���C�x���g
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
                textOfTalk.text = "���ӂ��B";
                break;
            case 8://���_�Ɖ�C�x���g
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
                textOfTalk.text = "�A�A�h�E���c";
                break;
            case 9://���_��|�����C�x���g
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
                textOfTalk.text = "���m���P�f�X�c";
                break;
            case 10://���߂Ĉ�Ղɗ����C�x���g
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
                textOfTalk.text = "�Ƃ��Ƃ������ˁc";
                break;
            case 11://��Ղ̃{�X�Ɖ�C�x���g
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
                textOfTalk.text = "�������c�B��Ղ̃h���S�����B";
                break;
            case 12://�t�@���g����
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
                textOfTalk.text = "�����Ŏ~�܂�Ȃ����B";
                break;
            case 13://�G���f�B���O�C�x���g
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
                textOfTalk.text = "�ɂ����c";
                break;
            default:
                break;
        }
        //yield return new WaitForSeconds(0.5f);
    }

    public void talkEvent()
    {
        audioSource.PlayOneShot(SE);
        Debug.Log("��b��i�߂܂��B");
        switch (eventNum)
        {
            case 1://�h���S���Əo��C�x���g
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "�����͉��̓꒣�肾�I��������I";
                        break;
                    case 2:
                        faceImageTalk[6].SetActive(true);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "�yZ�z����@�yX�z����Ȃ�";
                        break;
                    case 3:
                        if (firstResponse)
                        {
                            faceImageTalk[6].SetActive(false);
                            faceImageTalk[7].SetActive(true);
                            textOfTalk.text = "�ӂ�I����͒��������ŋ����Ă��I";
                        }
                        else
                        {
                            faceImageTalk[6].SetActive(false);
                            faceImageTalk[7].SetActive(true);
                            textOfTalk.text = "�ǂ����Ă��ʂ肽���Ȃ�c\n�꒣��o�g�����I";
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
                            //�E�B���h�E�����
                            TextWindow.SetActive(false);
                            cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                            cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                            //�o�g�����n�߂�
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
            case 2://�h���S���𒇊Ԃɂ���C�x���g
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "�꒣��͂��Ȃ��̂��̂ł����I";
                        break;
                    case 3:
                        faceImageTalk[6].SetActive(true);
                        faceImageTalk[8].SetActive(false);
                        textOfTalk.text = "�i��Ղ�ڎw���Ă��邱�Ƃ�`�����B�j";
                        break;
                    case 4:
                        faceImageTalk[6].SetActive(false);
                        faceImageTalk[8].SetActive(true);
                        textOfTalk.text = "���H��Ղɍs�����߂ɂ�����ʂ�\n���������Ȃ̂��H";
                        break;
                    case 5:
                        faceImageTalk[8].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "����𑁂�������ȁI��Ղ̃h���S����\n�ɂ͉������f���Ă��񂾁I";
                        break;
                    case 6:
                        textOfTalk.text = "�����s����I���O�Ȃ炠�����\n������Ă��ꂻ�������ȁI";
                        break;
                    case 7:
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "���b�T�[�h���S�������ԂɂȂ����I";
                        audioSource.PlayOneShot(SE3);
                        break;
                    case 8:
                        //�E�B���h�E�����
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
            case 3://�p���_�Əo��C�x���g
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "�����I�����̔����I";
                        break;
                    case 2:
                        textOfTalk.text = "��������Ղɍs�������񂾁I�����ʂ��Ă���I";
                        break;
                    case 3:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "���H�����A���܂Ȃ��ˁB\n�������J�����B";
                        break;
                    case 4:
                        textOfTalk.text = "�c�҂Ă�H���̊p�c���̉H�c\n���O�h���S�����H";
                        break;
                    case 5:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "���������ǁB";
                        break;
                    case 6:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�h���S���͈����I�����œ|���I";
                        break;
                    case 7:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "�҂Ă�A���͈����h���S��\n����Ȃ����āI";
                        break;
                    case 8:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
            case 4://�p���_��|�����C�x���g
                switch (talkNum)
                {
                    case 2:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[8].SetActive(true);
                        textOfTalk.text = "�т����肵����B�}�ɏP����\n���Ă��B";
                        break;
                    case 3:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[8].SetActive(false);
                        textOfTalk.text = "�ǂ����Ă���Ȃ��Ƃ����́H";
                        break;
                    case 4:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�h���S���͐X��R�₷���}������\n�����Ă�����c�B";
                        break;
                    case 5:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "���͂���Ȃ��Ƃ��Ȃ���I";
                        break;
                    case 6:
                        faceImageTalk[1].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�����Ȃ̂��B���������B";
                        break;
                    case 7:
                        textOfTalk.text = "��Ղ͊C��n�����悾�B";
                        break;
                    case 8:
                        faceImageTalk[1].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "�ӂ�A��Ղ̃h���S���̂����ŁA\n���܂Ō����҂���B";
                        break;
                    case 9:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 5://���߂ĊC�ɗ����C�x���g
                //�E�B���h�E�����
                TextWindow.SetActive(false);
                cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                talkNum = 0;
                stageManager.isStageScene = true;
                StartCoroutine("isEventSceneFalse");
                break;
            case 6://���Ɖ�C�x���g
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "���̋���n��҂��P�����ē����\n�����Ă���̂��B";
                        break;
                    case 2:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "�������������ĂȂ����H\n�ʂ��Ă����B";
                        break;
                    case 3:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�ہA�B�������Ă��邩������Ȃ��B";
                        break;
                    case 4:
                        textOfTalk.text = "�P�킹�Ă��炤�I";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "�b���ʂ��˂��I";
                        break;
                    case 6:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
            case 7://���𒇊Ԃɂ���C�x���g
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "�َ҂̕����ł�����c�B";
                        break;
                    case 3:
                        textOfTalk.text = "�����͂������悭�����a��I";
                        break;
                    case 4:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        textOfTalk.text = "��I����Ȃ��Ƃ��Ȃ��Ă������āI";
                        break;
                    case 5:
                        faceImageTalk[0].SetActive(false);
                        faceImageTalk[6].SetActive(true);
                        textOfTalk.text = "�i��Ղ̃h���S����������`����\n����Ȃ�����Ă����B�j";
                        break;
                    case 6:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[6].SetActive(false);
                        textOfTalk.text = "�h���S����ގ��c�H���������\n�ł�����I";
                        break;
                    case 7:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[6].SetActive(false);
                        textOfTalk.text = "�g����q���Ď�Ɏd�������Ă��������I";
                        break;
                    case 8:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        textOfTalk.text = "�ȂɌ����Ă񂾂����B";
                        break;
                    case 9:
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�T�����C�h�b�O�����ԂɂȂ����I";
                        audioSource.PlayOneShot(SE3);
                        break;
                    case 10:
                        faceImageTalk[2].SetActive(false);
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 8://���_�Ɖ�C�x���g
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "�R�R���ʃ��i���A5000�����b�e�N�_�T�C�c";
                        break;
                    case 2:
                        textOfTalk.text = "�\�E�C�E���}���f�X�m�f�c";
                        break;
                    case 3:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[3].SetActive(false);
                        textOfTalk.text = "���[��A�ǂ�����[�H";
                        break;
                    case 4:
                        faceImageTalk[0].SetActive(true);
                        faceImageTalk[3].SetActive(false);
                        textOfTalk.text = "���[��A�ǂ�����[�H\n�yZ�z5000�������@�yX�z�����ʂ�";
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
                                textOfTalk.text = "�X�~�}�Z���l�c���x�C�^�_�L�}�X�c";
                            }
                            else
                            {
                                isMoneyEnough = false;
                                faceImageTalk[0].SetActive(false);
                                faceImageTalk[3].SetActive(true);
                                textOfTalk.text = "�I�q�T���I�������i�C�c";
                            }

                        }
                        else
                        {
                            faceImageTalk[0].SetActive(false);
                            faceImageTalk[3].SetActive(true);
                            textOfTalk.text = "�c��E���W�f�X�J�c�H";
                        }
                        break;
                    case 6:
                        if (reviathanResponse)
                        {
                            if (isMoneyEnough)
                            {
                                textOfTalk.text = "�\���f�n�A�T���E�i���A�T���E�i���c";
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
                            textOfTalk.text = "�W���A�A�����}�X�J�c";
                        }
                        break;
                    case 7:
                        if (reviathanResponse)
                        {
                            faceImageTalk[3].SetActive(false);
                            faceImageTalk[0].SetActive(true);
                            textOfTalk.text = "�s����������c";
                            //�����@�C�A�T����������
                            reviathanRect.transform.DOLocalMove(reviathanRect.anchoredPosition + new Vector2(0, 700), 3.0f);
                        }
                        else
                        {
                            //�E�B���h�E�����
                            TextWindow.SetActive(false);
                            cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                            cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                            //�o�g�����n�߂�
                            talkNum = 0;
                            stageManager.isStageScene = false;
                            fieldManager.OnEnterDemo6();
                            fieldManager.OnClickStageButton();
                            StartCoroutine("isEventSceneFalse");
                        }
                        break;
                    case 8:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 9://���_��|�����C�x���g
                switch (talkNum)
                {
                    case 2:
                        textOfTalk.text = "�s�҃n��l�V�N���L�}�V���E�B\n�\���f�n�A�T���E�i���A�T���E�i���B";
                        break;
                    case 3:
                        faceImageTalk[3].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "�s����������c";
                        reviathanRect.transform.DOLocalMove(reviathanRect.anchoredPosition + new Vector2(0, 640), 1.0f);
                        break;
                    case 4:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
                        talkNum = 0;
                        stageManager.isStageScene = true;
                        StartCoroutine("isEventSceneFalse");
                        break;
                    default:
                        break;
                }
                break;
            case 10://���߂Ĉ�Ղɗ����C�x���g
                switch (talkNum)
                {
                    case 1:
                        faceImageTalk[0].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "���̈�Ղ̉����炷�����͂�������c";
                        break;
                    case 2:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
                        textOfTalk.text = "���O�����������𓭂��Ă���Ɖ\��\n�h���S���ł���ȁc�H";
                        break;
                    case 1:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "��H���q���񂾁I";
                        break;
                    case 3:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "��������I";
                        break;
                    case 4:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "�Ȃ�Ĉ����z�炾�I";
                        break;
                    case 5:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "������܂������I";
                        break;
                    case 6:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "�������Ă����I";
                        break;
                    case 7:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
            case 12://�G���f�B���O�C�x���g
                switch (talkNum)
                {
                    case 1:
                        textOfTalk.text = "�����͗��̍�������n�ł���B";
                        break;
                    case 2:
                        textOfTalk.text = "�s�p�ӂɗ�������ׂ��ł͂Ȃ��B";
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
                        textOfTalk.text = "���̍��c�H����Ă��ƁH";
                        break;
                    case 4:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�Ђ����I�H����I";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(false);
                        faceImageTalk[7].SetActive(true);
                        textOfTalk.text = "�r�r���Ă񂶂�˂��I";
                        break;
                    case 6:
                        faceImageTalk[5].SetActive(true);
                        faceImageTalk[7].SetActive(false);
                        textOfTalk.text = "�������������҂͔r������B";
                        break;
                    case 7:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[5].SetActive(false);
                        textOfTalk.text = "���������[�I";
                        break;
                    case 8:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
            case 13://�G���f�B���O�C�x���g
                switch (talkNum)
                {
                    case 1:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[0].SetActive(true);
                        textOfTalk.text = "�ϔO���ȁI";
                        break;
                    case 2:
                        faceImageTalk[4].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�����`�B���߂�Ȃ����`�B";
                        break;
                    case 3:
                        faceImageTalk[4].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�������Ĉ��������͓�������A\n�X�ɕ��a���K�ꂽ�B";
                        break;
                    case 4:
                        faceImageTalk[5].SetActive(false);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�����Ǝe���ƌ��̖`���͂��ꂩ��������c";
                        break;
                    case 5:
                        faceImageTalk[2].SetActive(true);
                        faceImageTalk[0].SetActive(false);
                        textOfTalk.text = "�ꌏ�����A�ł���ȁI";
                        break;
                    case 6:
                        //�E�B���h�E�����
                        TextWindow.SetActive(false);
                        cartain1.transform.DOLocalMove(new Vector2(0, 320), 1.0f);
                        cartain2.transform.DOLocalMove(new Vector2(0, -320), 1.0f);
                        //�o�g�����n�߂�
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
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n���b�̉�~30";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage1;
                break;
            case 2:
                fieldManager.LvOfSea += 35;
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n�C�b�̗؁~35";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage2;
                break;
            case 3:
                fieldManager.LvOfForest += 40;
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n���b�̉�~40";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage1;
                break;
            case 4:
                fieldManager.LvOfCity += 45;
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n�M�����~45";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage3;
                break;
            case 5:
                fieldManager.LvOfSea += 100;
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n�C�b�̗؁~100";
                ItemIcon.SetActive(true);
                ItemIcon.GetComponent<Image>().sprite = ItemImage2;
                break;
            case 6:
                ItemGetText.text = "�����W�����v����ɓ��ꂽ�I";
                ItemIcon.SetActive(false);
                stageManager.isINFJump = true;
                break;
            case 7:
                fieldManager.LvOfCity += 100;
                ItemGetText.text = "�f�ނ���ɓ��ꂽ�I\n�M�����~100";
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
        endingScoreText.text = "�y���v���C���ԁz�@" + (int)(fieldManager.entirePlayTime / 3600) + "���ԁ@"+ (int)((fieldManager.entirePlayTime % 3600) / 60) + "���@" + (int)(fieldManager.entirePlayTime % 60) + "�b\n\n�y�|�����G�̐��z�@"+ fieldManager.entireDefeatNum + "��\n\n�y�e�R�}���h�̎g�p�񐔁z\n�@���������@"+ fieldManager.entireCommandUseOne + "��\n�@�܂���@�@"+ fieldManager.entireCommandUseTwo + "��\n�@�݂˂����@"+ fieldManager.entireCommandUseThree + "��\n�@���@�@�@"+ fieldManager.entireCommandUseFour + "��\n";
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
        naichilab.UnityRoomTweet.Tweet("rpg3dragon", "�X�̕��������߂����B\n�y�����^�C���z" + (battleManager.defeatTime).ToString("n2") + "s", "unityroom", "DragonHuntRe");
    }

    public void OnEnterTytleButton()
    {
        cursorFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130,-330);
        tweetButtonText.text = "�Z�[�u���ă^�C�g���ɖ߂�܂��B";
        StartCoroutine("cursorFilling");
    }
    public void OnEnterTweetButton()
    {
        cursorFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -330);
        tweetButtonText.text = "#DragonHuntRe �Ń��X�{�X��\n�����^�C�����m�F�ł��܂��B";
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
