using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// ÉtÉBÅ[ÉãÉhâÊñ Ç≈ÇÃÉvÉåÉCÉÑÅ[ÇÃìÆÇ´Çêßå‰Ç∑ÇÈÉXÉNÉäÉvÉg
    /// </summary>
    public bool isStageScene;
    public bool recast;

    public GameManager gameManager;
    public FieldManager fieldManager;
    public RectTransform stageParent;
    public RectTransform stageForestRect;
    public GameObject playerObjStage;
    public RectTransform playerRect;
    public Rigidbody2D rigidbodyPlayer;
    public float speed;//ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆë¨ìx
    public Animator PlayerAnim;
    public int WALLNUM;

    public bool[] isMove;//0ÅFâEÇâüÇµÇƒÇ¢ÇÈÇ© 1ÅFç∂ÇâüÇµÇƒÇ¢ÇÈÇ©
    public int walkScrollSpeed;

    public RectTransform menuBackground;
    public RectTransform menuLeft;
    public RectTransform menuRight;
    //public bool isMenuOpened;
    public AudioClip SEWalk1;//êXÇï‡Ç≠Ç∆Ç´ÇÃâπ
    public AudioClip SEWalk2;//äCï”Çï‡Ç≠Ç∆Ç´ÇÃâπ
    public AudioClip SEWalk3;//à‚ê’Çï‡Ç≠Ç∆Ç´ÇÃâπ
    public AudioClip SEJump;//ÉWÉÉÉìÉvÇµÇΩÇ∆Ç´ÇÃâπ
    public AudioClip SEChange;
    public AudioSource jukeBoxWalkSE;
    public AudioSource jukeBoxSE;
    public int stageID;//ë´âπÇïœÇ¶ÇÈÇΩÇﬂÇÃÉXÉeÅ[ÉWéØï ID

    public GameObject[] respawnPoint;

    public eventTriggerManager eTM;
    public bool isEventScene;
    public GameObject stageExplanationText;

    public bool isFirstOcean;
    public bool isFirstRuin;

    public GameObject dragonFollower;
    public GameObject dogFollower;
    public int previousWallNum;
    public Slider SESlider;

    public bool isGround;
    public bool isEncountActive;
    public Image EncountButton;
    public Sprite CheckedImage;
    public Sprite UnCheckedImage;

    private Vector3 m_velocity;
    private Vector3 targetPosition;
    public GameObject stageParentObj;

    private float encountRecast;
    public bool isINFJump;
    public GameObject BGTwo;
    public GameObject BGThree;

    public bool encountSwitch;
    public RectTransform ImageBackgroundStage1;
    public RectTransform ImageBackgroundStage2;
    public RectTransform ImageBackgroundStage3;

    void Start()
    {
        isGround = true;
        targetPosition = new Vector3(0,1.5f,0);
        isEncountActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isStageScene && !recast && !isEventScene)
        {
            if (fieldManager.isSave)
            {
                Debug.Log("ÉZÅ[ÉuÇ∑ÇÈÇÊÅI");
                fieldManager.saveStart();
                eTM.touchedSaveFlag();
            }
            else if (fieldManager.isBillboard)
            {
                //ä≈î¬Çï\é¶
                eTM.touchedBillboard();
            }
            else
            {
                //ÉÅÉjÉÖÅ[âÊñ ÇèoÇ∑
                if (!fieldManager.isField)
                {
                    fieldManager.isField = true;
                    fieldManager.TextUpdateAfterStrengthen();
                    fieldManager.StatusTextUpdate_Field();
                    fieldManager.cursorRectUpdateField();
                    fieldManager.FieldBeggining();
                    StartCoroutine("menuShowUp");
                }
            }

            recast = true;
            StartCoroutine("Recast");
            fieldManager.recast = true;
            fieldManager.recastNormal();
        }
        if (Input.GetKeyDown(KeyCode.X) && isStageScene && fieldManager.isField && !recast && !fieldManager.isStrengthening && !fieldManager.isStrengtheningSub)
        {
            fieldManager.isField = false;
            StartCoroutine("menuShowDown");
            recast = true;
            StartCoroutine("Recast");
            fieldManager.recast = true;
            fieldManager.recastNormal();
        }
        if (isStageScene && !recast && !fieldManager.isField)
        {
            playerMoveStage();
            m_velocity += (targetPosition - playerObjStage.transform.position) * walkScrollSpeed;
            m_velocity *= 0.5f;
            stageParentObj.transform.position += m_velocity *= Time.deltaTime;
            encountRecast += Time.deltaTime;
        }
        else
        {
            jukeBoxWalkSE.volume = 0;
            PlayerAnim.SetBool("isRun", false);
            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
        }
        if (!isStageScene)
        {
            jukeBoxWalkSE.volume = 0;
                jukeBoxWalkSE.volume = 0;
                PlayerAnim.SetBool("isRun", false);
                rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
            stageExplanationText.SetActive(false);
        }
        else
        {
            stageExplanationText.SetActive(true);
            //îwåiÇÃÉXÉNÉçÅ[Éã
            switch (stageID)
            {
                case 1:
                    ImageBackgroundStage1.anchoredPosition = new Vector2(480 + (playerRect.anchoredPosition.x + 1400) * -0.154f, -270);//-1400~4800Å®480~-480
                    break;
                case 2:
                    ImageBackgroundStage2.anchoredPosition = new Vector2(480 + (playerRect.anchoredPosition.x + 2600) * -0.12f, 270 + (playerRect.anchoredPosition.y + 3300) * -0.6f);//-2600~3400Å®480~-480,  -2400~-3300Å®-270,270
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStageScene && !recast  && !fieldManager.isField)
        {
            playerEncountStage();
        }
        if (!isGround)
        {
            jukeBoxWalkSE.volume = 0;
        }
    }

    public void enemyEncountAtStageScene(int enemyID)
    {
        switch (enemyID)
        {
            case 11:
                fieldManager.OnEnterDemo0();
                break;
            case 1:
                fieldManager.OnEnterDemo1();
                break;
            case 2:
                fieldManager.OnEnterDemo2();
                break;
            case 3:
                fieldManager.OnEnterDemo3();
                break;
            case 4:
                fieldManager.OnEnterDemo4();
                break;
            case 5:
                fieldManager.OnEnterDemo5();
                break;
            case 6:
                fieldManager.OnEnterDemo6();
                break;
            case 7:
                fieldManager.OnEnterDemo7();
                break;
            case 8:
                fieldManager.OnEnterDemo8();
                break;
            case 9:
                fieldManager.OnEnterDemo9();
                break;
            case 13:
                fieldManager.OnEnterDemo13();
                break;
            case 14:
                fieldManager.OnEnterDemo14();
                break;
            case 15:
                fieldManager.OnEnterDemo15();
                break;
            case 16:
                fieldManager.OnEnterDemo16();
                break;
            case 17:
                fieldManager.OnEnterDemo17();
                break;
            case 18:
                fieldManager.OnEnterDemo18();
                break;
            case 19:
                fieldManager.OnEnterDemo19();
                break;
            case 20:
                fieldManager.OnEnterDemo20();
                break;
            case 21:
                fieldManager.OnEnterDemo21();
                break;
            case 22:
                fieldManager.OnEnterDemo22();
                break;
            case 23:
                fieldManager.OnEnterDemo23();
                break;
            case 24:
                fieldManager.OnEnterDemo24();
                break;
            default:
                break;
        }
        fieldManager.OnClickStageButton();
    }

    public void playerMoveStage()
    {
        if (Input.GetKey(KeyCode.UpArrow) && targetPosition.y >= -2)
        {
            targetPosition -= new Vector3(0, 10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && targetPosition.y <= 4f)
        {
            targetPosition += new Vector3(0, 10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)/*|| Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)*/)
        {
            if (isGround)
            {
                jukeBoxWalkSE.volume = SESlider.value;
            }
            PlayerAnim.SetBool("isRun", true);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isMove[0] = true;
            }
            else
            {
                isMove[0] = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                isMove[1] = true;
            }
            else
            {
                isMove[1] = false;
            }
            if (isMove[0])
            {
                if (isMove[1])
                {
                    if (isMove[2])
                    {
                        if (isMove[3])
                        {
                            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                        }
                        else
                        {
                            rigidbodyPlayer.velocity = new Vector3(0, speed, 0);
                            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                        }
                    }
                    else
                    {
                        if (isMove[3])
                        {
                            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                        }
                        else
                        {
                            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                        }
                    }
                }
                else
                {
                    if (isMove[2])
                    {
                        if (isMove[3])
                        {
                            rigidbodyPlayer.velocity = new Vector3(speed, rigidbodyPlayer.velocity.y, 0);
                            playerRect.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            rigidbodyPlayer.velocity = new Vector3(speed, rigidbodyPlayer.velocity.y, 0);
                            playerRect.localScale = new Vector3(1, 1, 1);
                        }
                    }
                    else
                    {
                        if (isMove[3])
                        {
                            rigidbodyPlayer.velocity = new Vector3(speed, rigidbodyPlayer.velocity.y, 0);
                            playerRect.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            rigidbodyPlayer.velocity = new Vector3(speed, rigidbodyPlayer.velocity.y, 0);
                            playerRect.localScale = new Vector3(1, 1, 1);
                        }
                    }
                }
            }
            else if (isMove[1])
            {
                if (isMove[2])
                {
                    if (isMove[3])
                    {
                        rigidbodyPlayer.velocity = new Vector3(-1 * speed, rigidbodyPlayer.velocity.y, 0);
                        playerRect.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        rigidbodyPlayer.velocity = new Vector3(-1 * speed, rigidbodyPlayer.velocity.y, 0);
                        playerRect.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else
                {
                    if (isMove[3])
                    {
                        rigidbodyPlayer.velocity = new Vector3(-1 * speed, rigidbodyPlayer.velocity.y, 0);
                        playerRect.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        rigidbodyPlayer.velocity = new Vector3(-1 * speed, rigidbodyPlayer.velocity.y, 0);
                        playerRect.localScale = new Vector3(-1, 1, 1);
                    }
                }
            }
            else if (isMove[2])
            {
                if (isMove[3])
                {
                    rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                }
                else
                {
                    rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
                }
            }
            else
            {
                rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
            }

        }
        else
        {
            jukeBoxWalkSE.volume = 0;
            PlayerAnim.SetBool("isRun", false);
            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
        }
        if (playerRect.localScale.x >= 0)
        {
            targetPosition.x = -3f;
        }
        else
        {
            targetPosition.x = 3f;
        }
        if (!isStageScene)
        {
            jukeBoxWalkSE.volume = 0;
            PlayerAnim.SetBool("isRun", false);
            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
        }
        if (isEventScene)
        {
            jukeBoxWalkSE.volume = 0;
            PlayerAnim.SetBool("isRun", false);
            rigidbodyPlayer.velocity = new Vector3(0, rigidbodyPlayer.velocity.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGround || isINFJump) && isStageScene && !isEventScene)
        {
            jukeBoxSE.PlayOneShot(SEJump);
            jukeBoxWalkSE.volume = 0;
            if (rigidbodyPlayer.velocity.y < 0)
            {
                rigidbodyPlayer.velocity -= new Vector2(0, rigidbodyPlayer.velocity.y);
            }
            rigidbodyPlayer.velocity += new Vector2(0, 12);
        }
        if (rigidbodyPlayer.velocity.y > 12)
        {
            rigidbodyPlayer.velocity -= new Vector2(0, rigidbodyPlayer.velocity.y - 12);
        }
        else if (rigidbodyPlayer.velocity.y < -9)
        {
            rigidbodyPlayer.velocity -= new Vector2(0, rigidbodyPlayer.velocity.y + 9);
        }
    }

    public void playerEncountStage()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)/* || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)*/)&& encountSwitch && isEncountActive && encountRecast >= 5.0f && isGround && !isEventScene)
        {
            //ìGÇ∆ÇÃÉGÉìÉJÉEÉìÉg
            int rndNum = Random.Range(1, 301);
            Debug.Log("ÉGÉìÉJÉEÉìÉgÇîªíf");
            if (rndNum == 300 && isStageScene)
            {
                encountRecast = 0;
                isStageScene = false;
                int rndEncount = Random.Range(1, 101);
                //êXÉXÉeÅ[ÉWÇ»ÇÁ
                if (stageID == 1)
                {
                    if (rndEncount <= 50)
                    {
                        fieldManager.OnEnterDemo1();
                    }
                    else
                    {
                        fieldManager.OnEnterDemo2();
                    }
                }
                else if (stageID == 2)
                {
                    if (rndEncount <= 50)
                    {
                        fieldManager.OnEnterDemo4();
                    }
                    else
                    {
                        fieldManager.OnEnterDemo5();
                    }
                }
                else if (stageID == 3)
                {
                    if (rndEncount <= 50)
                    {
                        fieldManager.OnEnterDemo8();
                    }
                    else
                    {
                        fieldManager.OnEnterDemo9();
                    }
                }
                fieldManager.OnClickStageButton();

            }
        }
    }

    public void recastLong()
    {
        StartCoroutine("RecastLong");
    }

    IEnumerator RecastLong()
    {
        yield return new WaitForSeconds(1.5f);
        recast = false;
    }

    IEnumerator Recast()
    {
        yield return new WaitForSeconds(0.5f);
        recast = false;
    }

    IEnumerator menuShowUp()
    {
        menuBackground.anchoredPosition = new Vector2(0,0);
        for (int i = 1; i < 26; i++)
        {
            menuLeft.anchoredPosition = new Vector2(-300 + 14 * i, 0);
            menuRight.anchoredPosition = new Vector2(605 - 8 * i, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator menuShowDown()
    {
        fieldManager.menuCommandExplanationTextBG.SetActive(false);
        for (int i = 25; i >= 0; i--)
        {
            menuLeft.anchoredPosition = new Vector2(-300 + 14 * i, 0);
            menuRight.anchoredPosition = new Vector2(605 - 8 * i, 0);
            yield return new WaitForSeconds(0.01f);
        }
        menuBackground.anchoredPosition = new Vector2(0, -1000);
    }

    public void stageChange(int wallNum)
    {
        recast = true;
        WALLNUM = wallNum;
        StartCoroutine("stageChangeCoroutine");
        StartCoroutine("RecastLong");
    }

    IEnumerator stageChangeCoroutine()
    {
        jukeBoxSE.PlayOneShot(SEChange);
        yield return new WaitForSeconds(1.0f);
        //éüÇÃÉXÉeÅ[ÉWÇï\é¶Ç∑ÇÈ
        switch (WALLNUM)
        {
            case 1://êXÇPÇ©ÇÁêXÇQÇ÷
                playerObjStage.transform.position = respawnPoint[1].transform.position;
                break;
            case 2://êXÇQÇ©ÇÁêXÇPÇ÷
                playerObjStage.transform.position = respawnPoint[0].transform.position;
                break;
            case 3://êXÇQÇ©ÇÁêXÇRÇ÷
                playerObjStage.transform.position = respawnPoint[3].transform.position;
                break;
            case 4://êXÇRÇ©ÇÁêXÇQÇ÷
                playerObjStage.transform.position = respawnPoint[2].transform.position;
                break;
            case 5://êXÇRÇ©ÇÁäCÇPÇ÷
                playerObjStage.transform.position = respawnPoint[5].transform.position;
                if (isFirstOcean)
                {
                    isFirstOcean = false;
                    //ÉCÉxÉìÉg
                    eTM.eventNum = 5;
                    eTM.StartCoroutine("eventBegin");
                }
                break;
            case 6://äCÇPÇ©ÇÁêXÇRÇ÷
                playerObjStage.transform.position = respawnPoint[4].transform.position;
                break;
            case 7://äCÇPÇ©ÇÁäCÇQÇ÷
                playerObjStage.transform.position = respawnPoint[6].transform.position;
                break;
            case 8://äCÇQÇ©ÇÁäCÇPÇ÷
                playerObjStage.transform.position = respawnPoint[7].transform.position;
                break;
            case 9://äCÇQÇ©ÇÁäCÇRÇ÷
                playerObjStage.transform.position = respawnPoint[8].transform.position;
                break;
            case 10://äCÇRÇ©ÇÁäCÇQÇ÷
                playerObjStage.transform.position = respawnPoint[9].transform.position;
                break;
            case 11://äCÇRÇ©ÇÁäCÇSÇ÷
                playerObjStage.transform.position = respawnPoint[10].transform.position;
                break;
            case 12://äCÇSÇ©ÇÁäCÇRÇ÷
                playerObjStage.transform.position = respawnPoint[11].transform.position;
                break;
            case 13://äCÇSÇ©ÇÁà‚ê’ÇPÇ÷
                playerObjStage.transform.position = respawnPoint[12].transform.position;
                if (isFirstRuin)
                {
                    isFirstRuin = false;
                    //ÉCÉxÉìÉg
                    eTM.eventNum = 10;
                    eTM.StartCoroutine("eventBegin");
                }
                break;
            case 14://à‚ê’ÇPÇ©ÇÁäCÇSÇ÷
                playerObjStage.transform.position = respawnPoint[13].transform.position;
                break;
            case 15://à‚ê’ÇPÇ©ÇÁà‚ê’ÇQÇ÷
                playerObjStage.transform.position = respawnPoint[14].transform.position;
                break;
            case 16://à‚ê’ÇQÇ©ÇÁà‚ê’ÇPÇ÷
                playerObjStage.transform.position = respawnPoint[15].transform.position;
                break;
            default:
                break;
        }
        previousWallNum = WALLNUM;
        dragonFollower.transform.position = playerObjStage.transform.position;
        dogFollower.transform.position = playerObjStage.transform.position;
        gameManager.StartFadeOut();
    }
    public void changeWalkSE()
    {
        switch (stageID)
        {
            case 1:
                jukeBoxWalkSE.clip = SEWalk1;
                BGTwo.SetActive(false);
                BGThree.SetActive(false);
                break;
            case 2:
                jukeBoxWalkSE.clip = SEWalk2;
                BGTwo.SetActive(true);
                BGThree.SetActive(false);
                break;
            case 3:
                jukeBoxWalkSE.clip = SEWalk3;
                BGTwo.SetActive(true);
                BGThree.SetActive(true);
                break;
            default:
                break;
        }
        jukeBoxWalkSE.Play();
    }

    public void OnClickencountSwitch()
    {
        if (isEncountActive)
        {
            isEncountActive = false;
            EncountButton.sprite = UnCheckedImage;
        }
        else
        {
            isEncountActive = true;
            EncountButton.sprite = CheckedImage;
        }
    }
}
