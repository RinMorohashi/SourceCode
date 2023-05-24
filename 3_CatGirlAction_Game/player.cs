using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class player : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�̃W�����v�A�U���Ȃǂ̓���Ɖ�b�C�x���g�𐧌䂷��X�N���v�g
    /// </summary>
    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("�W�����v���x")] public float jumpSpeed;
    [Header("�W�����v���鍂��")] public float jumpHeight;
    [Header("�W�����v��������")] public float jumpLimitTime;
    [Header("�ڒn����")] public GC ground;
    [Header("�����Ԃ�������")] public GC head;
    [Header("�_�b�V���̑����\��")] public AnimationCurve dashCurve;
    [Header("�W�����v�̑����\��")] public AnimationCurve jumpCurve;
    [Header("�󒆍U���̏㏸�\��")] public AnimationCurve airialAttackCurve;
    [Header("���݂�����̍����̊���(%)")] public float stepOnRate;
    [Header("�U���̓����蔻��")] public GameObject sworddetection;
    [Header("�W�����v���鎞�ɖ炷SE")] public AudioClip jumpSE; //New!
    [Header("�U������Ƃ��ɖ炷SE")] public AudioClip attackSE; //New
    [Header("���񂾂Ƃ��ɖ炷SE")] public AudioClip tooBadSE;
    [Header("���̎a���G�t�F�N�g")] public GameObject SSE;
    [Header("�󒆂̎a���G�t�F�N�g")] public GameObject SSE2;
    [Header("�L�̐����o��")] public GameObject catWindow;
    [Header("�L�̐����o���̕���")] public GameObject catWindowText;
    [Header("�c���̐����o��")] public GameObject yojoWindow;
    [Header("�c���̐����o���̑I�����P")] public GameObject yojoWindowText1;
    [Header("�c���̐����o���̑I�����Q")] public GameObject yojoWindowText2;
    [Header("�c���̑I�����Ă���I����")] public GameObject yojoWindowText;
    [Header("���̐����o��")] public GameObject dogWindow;
    [Header("���̐����o���̕���")] public GameObject dogWindowText;
    [Header("�R�C���l��SE")] public AudioClip coinSE;
    [Header("���b�Z�[�W����SE")] public AudioClip talkingSE;
    [Header("�X�e�[�W���������̃X�e�[�W���O�\��")] public stageNumberSlide sns;
    public GameObject vcam;
    public CinemachineVirtualCamera cvc;
    private GameObject playerDummy;
    private bool endSwitch = false;
    public bool endrollSwitch = false;
    private Text catText;
    private Text yojoText;
    private Text yojoText2;
    private Text dogText;
    private bool trueEventSwitchNull = false;
    private bool trueEventSwitchEins = false;
    private GameObject finalEvent;
    private FinalEvent FE;
    public bool eplg = false;
    private bool catZatudan = false;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private AudioSource oodio;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    public bool isGround = false;
    public bool isJump = false;
    public float jumpPos = 0.0f;    
    public bool isHead = false;
    public bool isRun = false;
    public bool isDown = false;
    public bool isAttack = false;
    private float dashTime, jumpTime;
    private float beforeKey;
    private string enemyTag = "enemy";
    private bool isContinue = false;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private SpriteRenderer sr = null;
    private string deadAreaTag = "DeadArea";
    private string hitAreaTag = "HitArea"; 
        private bool nonDownAnim = false;
    private string moveFloorTag = "MoveFloor";
    private MoveObject moveObj = null;
    private bool jumpInterval;
    private Animator animZwei = null;
    private Vector3 posYojo;
    public bool isLeft = false;
    private bool isAirialAttack;
    private bool canDoubleJump = false;
    private bool doubleJumpDirection = false;
    private bool isAutoRun = false;
    public int textNum = 0;
    public bool talking = false;
    public bool sentakusi = false;
    public float sentakusiTime = 0;
    public bool isHidari = true;
    private GameObject boat;
    public bool boatSwitch = false;
    private bool tt = false;
    private bool resetSwitch;
    public bool stagedreiswitch = false;
    private bool silencer = false;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        animZwei = SSE.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        oodio = GetComponent<AudioSource>();
        catText = catWindowText.GetComponent<Text>();
        yojoText = yojoWindowText1.GetComponent<Text>();
        yojoText2 = yojoWindowText2.GetComponent<Text>();
        dogText = dogWindowText.GetComponent<Text>();
        cvc = vcam.GetComponent<CinemachineVirtualCamera>();
        if (GamaManager.instance.stageNum == 5)
        {
            boat = GameObject.Find("boat");
            playerDummy = GameObject.Find("W1dummy");
        }
        if (GamaManager.instance.stageNum == 1)
        {
            finalEvent = GameObject.Find("WhiteBackground");
            FE = finalEvent.GetComponent<FinalEvent>();
            finalEvent.SetActive(false);
        }
        if (GamaManager.instance.goBackSwitch)
        {
            sns.snsActivater();
        }
    }

    void FixedUpdate()
    {
        if (!endSwitch)
        {
            posYojo = transform.position;

            if (isDown == false && isAttack == false)
            {
                //�ڒn����𓾂�
                isGround = ground.IsGround();

                if (boatSwitch)
                {
                    isGround = true;
                }

                isHead = head.IsGround();
                isHead = false;

                float xSpeed = 0;
                float ySpeed = 0;
                //�e����W���̑��x�����߂�
                if (!GamaManager.instance.talkEventSwitch && !GamaManager.instance.eventSwitch1to2 && !GamaManager.instance.eventSwitch2to3 && !GamaManager.instance.eventSwitch3to4 && !GamaManager.instance.eventSwitch4to5 && !GamaManager.instance.eventSwitch2to1 && !GamaManager.instance.eventSwitch3to2 && !GamaManager.instance.eventSwitch4to3 && !GamaManager.instance.eventSwitch5to4)
                {
                    xSpeed = GetXSpeed();
                    ySpeed = GetYSpeed();
                }

                //�A�j���[�V������K�p
                SetAnimation();

                //�ړ����x��ݒ�
                Vector2 addVelocity = Vector2.zero;
                if (moveObj != null)
                {
                    addVelocity = moveObj.GetVelocity();
                }

                if (isAttack == false)
                {
                    rb.velocity = new Vector2(xSpeed, ySpeed) + addVelocity;
                }
                else
                {
                    rb.velocity = addVelocity;
                }

                //�U���{�^���������Ƃ�
                attackAction();
            }
            else if (isAttack == false)
            {
                rb.velocity = new Vector2(0, -gravity);
            }

            if (isAutoRun)
            {
                autoRun();
            }

            if (Input.GetKey(KeyCode.R) && !resetSwitch && !talking && !boatSwitch && !sentakusi && !isAutoRun && !eplg && !trueEventSwitchNull && !trueEventSwitchEins && !endrollSwitch && !endSwitch && !GamaManager.instance.talkEventSwitch && !GamaManager.instance.vanguard)
            {
                resetSwitch = true;
                StartCoroutine("suicideRecast");
                GamaManager.instance.PlaySE(tooBadSE);
                anim.Play("playerDown");
                isDown = true;
                isHead = false;
            }

            if (isContinue)
            {
                //���Ł@���Ă��鎞�ɖ߂�
                if (blinkTime > 0.2f)
                {
                    sr.enabled = true;
                    blinkTime = 0.0f;
                }
                //���Ł@�����Ă���Ƃ�
                else if (blinkTime > 0.1f)
                {
                    sr.enabled = false;
                }
                //���Ł@���Ă���Ƃ�
                else
                {
                    sr.enabled = true;
                }

                //1�b�������疾�ŏI���
                if (continueTime > 1.0f)
                {
                    isContinue = false;
                    blinkTime = 0f;
                    continueTime = 0f;
                    sr.enabled = true;
                }
                else
                {
                    blinkTime += Time.deltaTime;
                    continueTime += Time.deltaTime;
                }
            }

            if (sentakusi == true)
            {
                Sentakumati();
            }
            if (GamaManager.instance.stageNum == 5 && transform.position.x >= -5.5f)
            {
                if (!isAutoRun && !boatSwitch)
                {
                    rb.velocity = new Vector2(0, -gravity);
                    isGround = true;
                    isJump = false;
                    anim.Play("playerStand");
                }
                if (GamaManager.instance.stage5EventSwitch)
                {
                    GamaManager.instance.stage5EventSwitch = false;
                    GamaManager.instance.talkEventSwitch = true;
                    StartCoroutine("talkingToTheCat");
                }
            }
            if (boatSwitch)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }

        if (GamaManager.instance.stageNum == 1)
        {
            if (talking)
            {
                rb.velocity = new Vector2(0, -gravity);
            }
            if (transform.position.x <= -30f)
            {
                GamaManager.instance.trueEndSwitch = true;
            }
            else
            {
                GamaManager.instance.trueEndSwitch = false;
            }
            if (transform.position.x <= -40f && transform.position.x >= -45f)
            {
                catTalksTrueOn();
            }
            else if (GamaManager.instance.trueEndSwitch && !trueEventSwitchNull)
            {
                catTextOff();
            }
            if (transform.position.x <= -12f && transform.position.x >= -20f && !GamaManager.instance.talkEventSwitch && sns.displayComplete)
            {
                catTalksTrueOn();
            }
            else if (catZatudan)
            {
                catTextOff();
                catZatudan = false;
            }
            if (transform.position.x <= -62.5f && !trueEventSwitchNull)
            {
                Debug.Log("��������g�D���[�G���h");
                trueEventSwitchNull = true;
                rb.velocity = new Vector2(0, -gravity);
                isJump = false;
                isRun = false;
                GamaManager.instance.talkEventSwitch = true;
                textNum = 1;
                StartCoroutine("talkingToTheCat");
            }

        }
        else if (GamaManager.instance.stageNum == 3)
        {
            if (stagedreiswitch)
            {
                if (transform.position.x <= 30f && transform.position.x >= 20f)
                {
                    catTalksTrueOn();
                }
                else
                {
                    catWindow.SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>Y���̑���</returns>
    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;

        if (isGround)
        {
            doubleJumpDirection = false;
            isJump = false;
            if (verticalKey > 0)
                {
                    if (jumpInterval != true)
                    {
                        if (!isJump)
                        {
                            oodio.Play();
                        anim.Play("playerJumpUp");
                    }
                        ySpeed = jumpSpeed;
                        jumpPos = transform.position.y; //�W�����v�����ʒu���L�^����
                        isJump = true;
                        jumpInterval = true;
                        StartCoroutine("JumpRecast");
                        jumpTime = 0.0f;
                    }
                }
                else
                {
                    isJump = false;
                }
        }
        else if (isJump)
        {
            //������L�[�������Ă��邩
            bool pushUpKey = verticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���ĂȂ���
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else if (jumpInterval == false && GamaManager.instance.zweiJumpSwitch)
            {
                isJump = false;
                if (doubleJumpDirection == false)
                {
                    doubleJumpDirection = true;
                    canDoubleJump = true;
                    jumpTime = 0.0f;
                }
            }
        }
        else if (canDoubleJump)
        {
            if (!isJump && verticalKey > 0)
            {
                canDoubleJump = false;
                oodio.Play();
                anim.Play("playerJumpUp");
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //�W�����v�����ʒu���L�^����
                isJump = true;
                jumpInterval = true;
                StartCoroutine("JumpRecast");
                jumpTime = 0.0f;
            }
        }

        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
            return ySpeed;
    }

    /// <summary>
    /// X�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>X���̑���</returns>
    private float GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;
            isLeft = false;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
            isLeft = true;
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        //�O��̓��͂���_�b�V���̔��]�𔻒f���đ��x��ς���
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }

        beforeKey = horizontalKey;
        xSpeed *= dashCurve.Evaluate(dashTime);
        beforeKey = horizontalKey;

            return xSpeed;
    }

    /// <summary>
    /// �A�j���[�V������ݒ肷��
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }

    #region//�ڐG���� New!      
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
        if (collision.collider.tag == enemyTag)
        {
            GamaManager.instance.PlaySE(tooBadSE);
            anim.Play("playerDown");
            isDown = true;
            isHead = false;
        }
        //������
        else if (collision.collider.tag == moveFloorTag)
        {
            //���݂�����ɂȂ鍂��
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
            //���݂�����̃��[���h���W
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
            foreach (ContactPoint2D p in collision.contacts)
            {
                //�������ɏ���Ă���
                if (p.point.y < judgePos)
                {
                    moveObj = collision.gameObject.GetComponent<MoveObject>();
                }           
            }
        }
        //�����鏰
        else if (collision.collider.tag == "FallFloor")
        {
            o.playerStepOn = true;
        }
    }
    #endregion

    /// <summary>
    /// �R���e�B�j���[�ҋ@��Ԃ�
    /// </summary>
    /// <returns></returns>
    public bool IsContinueWaiting()
    {
        if (GamaManager.instance.isGameOver)  // New!
        {
            return false;
        }
        else
        {
            return IsDownAnimEnd() || nonDownAnim;  // New!
        }
    }

    //�_�E���A�j���[�V�������������Ă��邩�ǂ���
    private bool IsDownAnimEnd()
    {
        if (isDown && anim != null)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("playerDown"))
            {
                if (currentState.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// �R���e�B�j���[����
    /// </summary>
    public void ContinuePlayer()
    {
        isDown = false;
        isHead = false;
        anim.Play("playerStand");
        isJump = false;
        isRun = false;
        isContinue = true;
        nonDownAnim = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == deadAreaTag)
        {
                if (GamaManager.instance.stageNum == 3 && !stagedreiswitch)
                {
                    stagedreiswitch = true;
                }
            GamaManager.instance.PlaySE(tooBadSE);
            ReceiveDamage(false);
        }
        else if (collision.tag == hitAreaTag)
        {
            ReceiveDamage(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == moveFloorTag)
        {
            //���������痣�ꂽ
            moveObj = null;
        }
    }

    //���ꂽ���̏���
    private void ReceiveDamage(bool downAnim)
    {
        if (isDown)
        {
            return;
        }
        else
        {
            if (downAnim)
            {
                GamaManager.instance.PlaySE(tooBadSE);
                anim.Play("playerDown");
            }
            else
            {
                nonDownAnim = true;
            }
            isDown = true;
            isHead = false;
        }
    }

    private void attackAction()
    {
        float fire1Key = Input.GetAxis("Fire1");
        if (fire1Key > 0 && GamaManager.instance.talkEventSwitch)
        {
            if (!talking)
            {
                StartCoroutine("talkingToTheCat");
            }
        }
        else if (fire1Key > 0 && GamaManager.instance.stageNum == 5 && transform.position.x >= -5.5f)
        {
            if (!talking)
            {
                StartCoroutine("talkingToTheCat");
            }
        }
        else if (GamaManager.instance.stageNum == 5 || GamaManager.instance.trueEndSwitch)
        {

        }
        else if (fire1Key > 0 && isGround == true)
        {
            anim.Play("playerAttack");
            isAttack = true;
            StartCoroutine("playerAttackMotion");
        }
        else if (fire1Key > 0 && isGround == false && isAirialAttack == false�@&& GamaManager.instance.airialAttackSwitch)
        {
            anim.Play("playerAirialAttack");
            isAttack = true;
            isAirialAttack = true;
            StartCoroutine("playerAirialAttackMotion");
        }

    }

    IEnumerator playerAttackMotion()
    {
        rb.velocity = new Vector2(0, -gravity);
        GamaManager.instance.PlaySE(attackSE);
        yield return new WaitForSeconds(0.25f);
        sworddetection.SetActive(true);
        SSE.SetActive(true);
        animZwei.Play("swordAttack");
        yield return new WaitForSeconds(0.10f);
        sworddetection.SetActive(false);
        yield return new WaitForSeconds(0.35f);
        SSE.SetActive(false);
        isAttack = false;
    }

    IEnumerator playerAirialAttackMotion()
    {
        GamaManager.instance.PlaySE(attackSE);

        for (int i = 1; i < 10; i++)
        {
            float xSpeed = GetXSpeed();
            rb.velocity = new Vector2(xSpeed * 0.5f, airialAttackCurve.Evaluate(i / 10f));
            yield return new WaitForSeconds(0.05f);
            if (i == 4)
            {
                SSE2.SetActive(true);
            }
            else if (i == 6)
            {
                SSE2.SetActive(false);
            }
        }
        isAttack = false;
        yield return new WaitForSeconds(0.5f);
        isAirialAttack = false;
    }

        IEnumerator JumpRecast()
    {
        yield return new WaitForSeconds(0.3f);
        jumpInterval = false;
    }

    public void a()
    {
        StartCoroutine("playerFadeIn");
    }

    IEnumerator playerFadeIn()
    {
        if (GamaManager.instance.talkEventSwitch && GamaManager.instance.stageNum == 1)
        {
            layAnimation();
            yield return new WaitForSeconds(2.0f);
            awakeAnimation();
            yield return new WaitForSeconds(1.0f);
            standAnimation();
            yield return new WaitForSeconds(1.0f);
            catWindow.SetActive(true);
            textNum++;
        }
        if (GamaManager.instance.eventSwitch1to2 && GamaManager.instance.stageNum == 2)
        {
            Debug.Log("�X�e�[�W�Q�̃t�F�[�h�C���������܂���");
            transform.position = new Vector3(-25f, -4.2f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
        }
        else if (GamaManager.instance.eventSwitch2to3 && GamaManager.instance.stageNum == 3)
        {
            transform.position = new Vector3(-26f, -4.9f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            anim.Play("playerRun");
        }
        else if (GamaManager.instance.eventSwitch3to4 && GamaManager.instance.stageNum == 4)
        {
            transform.position = new Vector3(-27f, -5f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            anim.Play("playerRun");
        }
        else if (GamaManager.instance.eventSwitch4to5 && GamaManager.instance.stageNum == 5)
        {
            transform.position = new Vector3(-26f, -4f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            anim.Play("playerRun");
        }
        if (GamaManager.instance.eventSwitch2to1 && GamaManager.instance.stageNum == 1)
        {
            transform.position = new Vector3(132.9f, 15f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
        }
        else if (GamaManager.instance.eventSwitch3to2 && GamaManager.instance.stageNum == 2)
        {
            transform.position = new Vector3(135.5f, 14.3f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
        }
        else if (GamaManager.instance.eventSwitch4to3 && GamaManager.instance.stageNum == 3)
        {
            transform.position = new Vector3(132.3f, 5.1f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
        }
        else if (GamaManager.instance.eventSwitch5to4 && GamaManager.instance.stageNum == 4)
        {
            transform.position = new Vector3(187f, -22.5f, 0f);
            isAutoRun = true;
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
        }
        yield return new WaitForSeconds(1.0f);
        isAutoRun = false;
        isRun = false;

        if (GamaManager.instance.eventSwitch1to2 && GamaManager.instance.stageNum == 2)
        {
            if (GamaManager.instance.firstTimeReachedStage2zwei)
            {
                GamaManager.instance.firstTimeReachedStage2zwei = false;
                catWindow.SetActive(true);
                textNum++;
            }
            else
            {
                GamaManager.instance.talkEventSwitch = false;
            }
        }
        if (GamaManager.instance.eventSwitch2to3 && GamaManager.instance.stageNum == 3)
        {
            if (GamaManager.instance.firstTimeReachedStage3zwei)
            {
                GamaManager.instance.firstTimeReachedStage3zwei = false;
                catWindow.SetActive(true);
                textNum++;
            }
            else
            {
                GamaManager.instance.talkEventSwitch = false;
            }
        }
        if (GamaManager.instance.eventSwitch3to4 && GamaManager.instance.stageNum == 4)
        {
            if (GamaManager.instance.firstTimeReachedStage4zwei)
            {
                GamaManager.instance.firstTimeReachedStage4zwei = false;
                catWindow.SetActive(true);
                textNum++;
            }
            else
            {
                GamaManager.instance.talkEventSwitch = false;
            }
        }
        if (GamaManager.instance.eventSwitch4to5 && GamaManager.instance.stageNum == 5)
        {
            if (GamaManager.instance.firstTimeReachedStage5zwei)
            {
                GamaManager.instance.firstTimeReachedStage5zwei = false;
                catWindow.SetActive(true);
                textNum++;
            }
            else
            {
                GamaManager.instance.talkEventSwitch = false;
            }
        }

        GamaManager.instance.eventSwitch1to2 = false;
        GamaManager.instance.eventSwitch2to3 = false;
        GamaManager.instance.eventSwitch3to4 = false;
        GamaManager.instance.eventSwitch4to5 = false;
        GamaManager.instance.eventSwitch2to1 = false;
        GamaManager.instance.eventSwitch3to2 = false;
        GamaManager.instance.eventSwitch4to3 = false;
        GamaManager.instance.eventSwitch5to4 = false;
    }

    public void autoRun()
    {
        if (!GamaManager.instance.eventSwitch2to1 && !GamaManager.instance.eventSwitch3to2 && !GamaManager.instance.eventSwitch4to3 && !GamaManager.instance.eventSwitch5to4)
        {
            rb.velocity = new Vector2(speed, -gravity);
        }
        else
        {
            rb.velocity = new Vector2(-speed, -gravity);
        }
    }

    public void layAnimation()
    {
        anim.Play("playerLay");
    }
    public void awakeAnimation()
    {
        anim.Play("playerAwake");
    }
    public void standAnimation()
    {
        anim.Play("playerStand");
    }

    public void startEpilogue()
    {
        StartCoroutine("talkingToTheCat");
    }

    IEnumerator talkingToTheCat()
    {
        if (!talking && !endrollSwitch && !silencer)
        {
            silencer = true;
            GamaManager.instance.PlaySE(talkingSE);
        }
        talking = true;
        if (eplg)
        {
            switch (textNum)
            {
                case 1:
                    catWindow.SetActive(true);
                    catText.text = "�c�܂��������Ă邩�H";
                    break;
                case 2:
                    catText.text = "���̂Ƃ��́A���������Ă��ꂽ�񂾂�ȁB\n���肪�Ƃ�";
                    break;
                case 3:
                    catText.text = "���ǂǂ�����瀂��ꂿ�܂������ǁB";
                    break;
                case 4:
                    catText.text = "�͂��߂́A���O��V���܂�\n��������肾�������ǁc";
                    break;
                case 5:
                    catText.text = "���������G���f�B���O�����肾��ȁB";
                    break;
                case 6:
                    catText.text = "���O�����ǂ���΁c\n�������ł��܂��ꏏ�ɖ`�����悤���B";
                    break;
                case 7:
                    catText.text = "������c�܂�����B�񑩂��B";
                    break;
                case 8:
                    catWindow.SetActive(false);
                    yield return new WaitForSeconds(1.0f);
                    endrollSwitch = true;
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.trueEndSwitch && trueEventSwitchEins)
        {
            switch (textNum)
            {
                case 1:
                    catWindow.SetActive(true);
                    catText.text = "���̐��E�ɖ߂肽�����H";
                    break;
                case 2:
                    yojoText.text = "����";
                    yojoText2.text = "����";
                    yojoWindowText.SetActive(true);
                    yojoWindowText.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoWindow.SetActive(true);
                    yojoWindowText1.SetActive(true);
                    yojoWindowText1.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoWindowText2.SetActive(true);
                    yojoWindowText2.transform.localPosition = new Vector3(72f, 10f, 0f);
                    sentakusi = true;
                    break;
                case 3:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "�m���Ă邩�H";
                        }
                        else
                        {
                            catText.text = "�������B";
                        }
                    }
                    break;
                case 4:
                    if (isHidari)
                    {
                        catText.text = "�L�ɂ͂X�̖�������񂾁B���M����Ȃ����B";
                    }
                    else
                    {
                        transform.position += new Vector3(0.1f, 0, 0);
                        trueEventSwitchNull = false;
                        catWindow.SetActive(false);
                        talking = false;
                        trueEventSwitchEins = true;
                        GamaManager.instance.talkEventSwitch = false;
                        textNum = 0;
                    }
                    break;
                case 5:
                    catText.text = "�����牴�͌��̐��E�ɖ߂��B";
                    break;
                case 6:
                    catText.text = "�����Œ�Ă����A�����ЂƂ��O�ɂ���B";
                    break;
                case 7:
                    catText.text = "�ł��邩������Ȃ����ǁA����Ă݂�B";
                    break;
                case 8:
                    catText.text = "�܂����������񂾂�H���ɕt���Ă����B";
                    break;
                case 9:
                    //�G���f�B���O��
                    catWindow.SetActive(false);
                    talking = false;
                    GamaManager.instance.eventSwitch2to1 = true;
                    GamaManager.instance.vanguard = true;
                    yield return new WaitForSeconds(0.5f);
                    isAutoRun = true;
                    isRun = true;
                    yield return new WaitForSeconds(2.0f);
                    isAutoRun = false;
                    isRun = false;
                    finalEvent.SetActive(true);
                    FE.FESwitch = true;
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.trueEndSwitch)
        {
            Debug.Log("���������Ă����H");
            catWindow.SetActive(true);
            switch (textNum)
            {
                case 0:
                    break;
                case 1:
                    catText.text = "�����ƁA���������͐i�߂Ȃ����B";
                    break;
                case 2:
                    catText.text = "���̐�͂��O�����������E�ɑ����Ă�B�����A";
                    break;
                case 3:
                    catText.text = "�A���̂͐����Ă�z�������B";
                    break;
                case 4:
                    catText.text = "���O�͎���ł邩��\n�߂�Ȃ��B";
                    break;
                case 5:
                    catText.text = "�����A�����͂��̐��Ƃ��̐��̋��E�Ȃ񂾁B";
                    break;
                case 6:
                    Debug.Log("�����Ŗ߂�");
                    if (!tt)
                    {
                        tt = true;
                        transform.position += new Vector3(0.5f, 0, 0);
                    }
                    trueEventSwitchNull = false;
                    catWindow.SetActive(false);
                    talking = false;
                    trueEventSwitchEins = true;
                    GamaManager.instance.talkEventSwitch = false;
                    textNum = 0;
                    tt = false;
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.stageNum == 1)
        {
            switch (textNum)
            {
                case 1:
                    transform.localScale = new Vector3(-1, 1, 1);
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "�悤�₭�ڂ��o�߂��ȁB\n���̐��E�͏��߂Ă��H";
                    break;
                case 2:
                    catText.text = "���̕Ӊ������Ƃ��o�邩��A�C��t�����B";
                    break;
                case 3:
                    yojoWindow.SetActive(true);
                    sentakusi = true;
                    break;
                case 4:
                    if (sentakusi != true)
                    {
                        Debug.Log("�����ŗc���̐����o����������");
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "�����͐��E�̋��E��"/*"���Ȃ��炾�ȁB"*/;
                        }
                        else
                        {
                            catText.text = "�����̔L���B���O�Ƃ��͖����B";
                        }
                    }
                    break;
                case 5:
                    if (isHidari)
                    {
                        catText.text = "��̐��E�Ɖ��̐��E�������āA\n���̋��E���������Ă킯���ȁB"/*"�ǂ����痈���񂾁H\n�����͎q��������悤�ȏ�����Ȃ��񂾁B"*/;
                    }
                    else
                    {
                        catText.text = "���̐��E�ɂ͉��񂩗������Ƃ���񂾂��B"/*"���O�����N���H"*/;
                    }
                    break;
                case 6:
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "���̕ӂ��ē����Ă���B\n��l�����Ȃ�����ȁB"/*���P���肩�B*/;
                    break;
                case 7:
                    transform.localScale = new Vector3(-1, 1, 1);
                    catText.text = "�����ɗ����l�Ԃ݂͂�ȉE�֐i�ނ񂾁B\n�s���Ă݂悤���B"/*"�悵�A�������ē����Ă���B"*/;
                    break;
                case 8:
                    catText.text = " ";
                    yield return new WaitForSeconds(0.5f);
                    catText.text = "���������o�������Ă���B"/*"���O�ɂ͍s���ׂ��ꏊ������񂾂�H\n���Ԃ�E�̕��p���B"*/;
                    break;
                case 9:
                    catWindow.SetActive(false);
                    talking = false;
                    yield return new WaitForSeconds(0.5f);
                    GamaManager.instance.talkEventSwitch = false;
                    textNum = 0;
                    sns.snsActivater();
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.stageNum == 2)
        {
            switch (textNum)
            {
                case 1:
                    transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "�l�Ԃɂ͑S�����O������񂾂�H";
                    break;
                case 2:
                    yojoWindow.SetActive(true);
                    yojoWindowText2.SetActive(false);
                    yojoWindowText.SetActive(false);
                    yojoWindowText1.transform.localPosition = new Vector3(0f, 10f, 0f);
                    yojoText.text = "(���̖��O�c\n�v���o���Ȃ��c)";
                    break;
                case 3:
                    yojoWindowText2.SetActive(true);
                    yojoWindowText.SetActive(true);
                    yojoWindowText1.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoText.text = "������Ȃ��B";
                    sentakusi = true;
                    break;
                case 4:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "�������ꂽ�̂��B"/*"�L���������̂��B"*/;
                        }
                        else
                        {
                            catText.text = "�u�W�o�j�����v���B�����Ȃ����O���B";
                        }
                    }
                    break;
                case 5:
                    if (sentakusi != true)
                    {
                        if (isHidari)
                        {
                            catText.text = "���̕����C�y�ŗǂ��B";
                        }
                        else
                        {
                            catText.text = "�u�j�����v�̏��Ƃ��L���ۂ��Ă����ȁB";
                        }
                    }
                    break;
                case 6:
                    catWindow.SetActive(false);
                    talking = false;
                    yield return new WaitForSeconds(0.5f);
                    GamaManager.instance.talkEventSwitch = false;
                    textNum = 0;
                    sns.snsActivater();
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.stageNum == 3)
        {
            switch (textNum)
            {
                case 1:
                    transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "���͂���ȋ��������񂾁A���B";
                    break;
                case 2:
                    catText.text = "�l���|��Ă�����S�z��\n�t���Ă��Ă邾���Ȃ񂾁B";
                    break;
                case 3:
                    catText.text = "����A�S�z�Ƃ�����Ȃ���\n���O�ɋ��������邾���Ȃ̂����B";
                    break;
                case 4:
                    catText.text = "�����v���o����Ƃ����ȁB�L���B";
                    break;
                case 5:
                    catWindow.SetActive(false);
                    talking = false;
                    yield return new WaitForSeconds(0.5f);
                    GamaManager.instance.talkEventSwitch = false;
                    textNum = 0;
                    sns.snsActivater();
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.stageNum == 4)
        {
            switch (textNum)
            {
                case 1:
                    transform.localScale = new Vector3(1, 1, 1);
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "���������œ��������B";
                    break;
                case 2:
                    yojoWindow.SetActive(true);
                    sentakusi = true;
                    break;
                case 3:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "�͌����B���ꂵ��������Ȃ��B";
                        }
                        else
                        {
                            catText.text = "���������ݒ肾����ȁB";
                        }
                    }
                    break;
                case 4:
                    if (isHidari)
                    {
                        catText.text = "�s�������ƂȂ�����A�悭�m��Ȃ��񂾁B\n���܂˂��B";
                    }
                    else
                    {
                        catText.text = "���̐��E���ᕁ�ʂ̂��Ƃ����B";
                    }
                    break;
                case 5:
                    catWindow.SetActive(false);
                    talking = false;
                    yield return new WaitForSeconds(0.5f);
                    GamaManager.instance.talkEventSwitch = false;
                    textNum = 0;
                    sns.snsActivater();
                    break;
                default:
                    break;
            }
        }
        else if (transform.position.x >= -5.5f)
        {
            switch (textNum)
            {
                case 0:
                    isAutoRun = false;
                    isRun = false;
                    dogWindow.SetActive(true);
                    break;
                case 1:
                    isAutoRun = false;
                    isRun = false;
                    dogWindow.SetActive(true);
                    break;
                case 2:
                    dogText.text = "���n��Ȃ炱�̏M�ɏ��ł������B";
                    break;
                case 3:
                    dogText.text = "�^���͋��݂U���ł������B";
                    break;
                case 4:
                    yojoWindowText.SetActive(true);
                    yojoWindowText.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    //isHidari = true;
                    yojoWindow.SetActive(true);
                    yojoWindowText1.SetActive(true);
                    yojoWindowText1.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoWindowText2.SetActive(true);
                    yojoWindowText2.transform.localPosition = new Vector3(72f, 10f, 0f);
                    sentakusi = true;
                    break;
                case 5:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            GamaManager.instance.score -= 6;
                            GamaManager.instance.PlaySE(coinSE);
                            dogText.text = "����ł͂悢�����`�B";
                        }
                        else
                        {
                            dogText.text = "��߂郏���H";
                        }
                    }
                    break;
                case 6:
                    if (isHidari)
                    {
                        dogWindow.SetActive(false);
                        isAutoRun = true;
                        isRun = true;
                        anim.Play("playerRun");
                        yield return new WaitForSeconds(0.3f);
                        isAutoRun = false;
                        isRun = false;
                        yield return new WaitForSeconds(1.0f);
                        yojoOnboat();
                        boatSwitch = true;
                        yield return new WaitForSeconds(1.5f);
                        catWindow.SetActive(true);
                        catText.text = "���̈ē��͂����܂ł��B";
                    }
                    else
                    {
                        if (!isHidari)
                        {
                            transform.position -= new Vector3(0.3f, 0f, 0f);
                            GamaManager.instance.stage5EventSwitch = true;

                            dogWindow.SetActive(false);
                            catWindow.SetActive(false);
                            talking = false;
                            yield return new WaitForSeconds(0.5f);
                            GamaManager.instance.talkEventSwitch = false;
                            GamaManager.instance.stage5EventSwitch = true;
                            dogText.text = "����ɂ������B";
                        }

                        textNum = 0;
                    }
                    break;
                case 7:
                    if (isHidari)
                    {
                        catText.text = "���ʂꂾ�ȁB";
                    }
                    break;
                case 8:
                    yojoWindowText.SetActive(true);
                    yojoWindowText.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    //isHidari = true;
                    yojoWindow.SetActive(true);
                    yojoWindowText1.SetActive(true);
                    yojoWindowText1.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoWindowText2.SetActive(true);
                    yojoWindowText2.transform.localPosition = new Vector3(72f, 10f, 0f);
                    yojoText.text = "�ʂ������";
                    yojoText2.text = "�ꏏ�ɍs�����B";
                    sentakusi = true;
                    break;
                case 9:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "�����B�y�����U�����������B";
                        }
                        else
                        {
                            catText.text = "�������ꏏ�ɂ͍s���Ȃ��B";
                        }
                    }
                    break;
                case 10:
                    if (isHidari)
                    {
                        catText.text = "���O�݂����Ȑl�Ԃ͏��߂Ă�����ȁB";
                    }
                    else
                    {
                        catText.text = "���������͐l�Ԃ������s����񂾁B";
                    }
                    break;
                case 11:
                    if (isHidari)
                    {
                        catText.text = "�������ł����C�łȁB";
                    }
                    else
                    {
                        catText.text = "���Ⴀ�ȁB";
                    }
                    break;
                case 12:
                    if (isHidari)
                    {
                        GamaManager.instance.stage5EventSwitch = true;

                        dogWindow.SetActive(false);
                        catWindow.SetActive(false);
                        talking = false;
                        yield return new WaitForSeconds(0.5f);
                        GamaManager.instance.talkEventSwitch = false;
                        if (!endSwitch)
                        {
                            endSwitch = true;
                            StartCoroutine("ending");
                        }
                    }
                    else
                    {
                        catText.text = "�������ł����C�łȁB";
                    }
                    break;
                case 13:
                    if (!isHidari)
                    {
                        isHidari = true;
                        GamaManager.instance.stage5EventSwitch = true;

                        dogWindow.SetActive(false);
                        catWindow.SetActive(false);
                        talking = false;
                        yield return new WaitForSeconds(0.5f);
                        GamaManager.instance.talkEventSwitch = false;
                        if (!endSwitch)
                        {
                            endSwitch = true;
                            StartCoroutine("ending");
                        }
                    }

                    textNum = 0;
                    break;
                default:
                    break;
            }
        }
        else if (GamaManager.instance.stageNum == 5)
        {
            switch (textNum)
            {
                case 1:
                    catWindow.SetActive(false);
                    talking = false;
                    GamaManager.instance.talkEventSwitch = false;
                    yield return new WaitForSeconds(0.5f);
                    textNum = 0;
                    sns.snsActivater();
                    break;
                default:
                    break;
            }
        }

        yield return new WaitForSeconds(0.3f);
        if (sentakusi != true)
        {
            Debug.Log("��b��i�߂܂�");
            textNum++;
        }
        talking = false;
        silencer = false;
    }

    public void Sentakumati()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float fire1Key = Input.GetAxis("Fire1");

        if (sentakusiTime < 0.1f)
        {
            isHidari = true;
        }
        if (fire1Key > 0 && sentakusiTime > 0.5f)
        {
            sentakusi = false;
            yojoWindowText.SetActive(false);
            if (isHidari)
            {
                yojoWindowText2.SetActive(false);
                yojoWindowText1.transform.localPosition = new Vector3(0f, 10f, 0f);
            }
            else
            {
                yojoWindowText1.SetActive(false);
                yojoWindowText2.transform.localPosition = new Vector3(0f, 10f, 0f);
                if (GamaManager.instance.stageNum == 2)
                {
                    yojoText2.text = "�W�o�j�������Ă����́B";
                }
            }
        }
        if (horizontalKey > 0)
        {
            yojoWindowText.transform.localPosition = new Vector3(72f, 10f, 0f);
            isHidari = false;
        }
        if (horizontalKey < 0)
        {
            yojoWindowText.transform.localPosition = new Vector3(-72f, 10f, 0f);
            isHidari = true;
        }
        sentakusiTime += Time.deltaTime;
    }

    public void  yojoOnboat()
    {
        boat.transform.SetParent(transform);
        transform.position = new Vector3(-1.12f, -4.79f, 0f);
        boat.transform.localPosition = new Vector3(-0.62f, 0.49f, 0f);
    }

    IEnumerator ending()
    {
        //�{�[�g���E�ɐi��
        //�J�����͓������Ȃ�
        cvc.Follow = playerDummy.transform;
        while (transform.position.x <= 9.0f)
        {
            float sin = Mathf.Sin(1.5f * Time.time);
            transform.position += new Vector3(0.02f, 0f, 0f);
            Quaternion q = Quaternion.AngleAxis(sin * 6, Vector3.forward);
            transform.rotation = q;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2.0f);
        endrollSwitch = true;
        //�L�����փt�F�[�h�A�E�g
        //�Ó]���ăX�^�b�t���[��
        //�X�R�A�\��
        //�Q�i�W�����v������΂��̕ǂ��z����ꂽ�����c
    }

    public void catTalksTrueOn()
    {
        catWindow.SetActive(true);
        switch (GamaManager.instance.stageNum)
        {
            case 1:
                catText.text = "�������ɍs���̂��H\n�܂���������";
                if (transform.position.x <= -12f && transform.position.x >= -20f)
                {
                    catText.text = "�����F�ړ��@���F�W�����v�@z�F�U��";
                    catZatudan = true;
                }
                break;
            case 2:
                break;
            case 3:
                catText.text = "�󒆍U���ŉ����܂Ŕ�ׂ邺�B";
                break;
            case 4:
                break;
            default:
                break;
        }
    }
    public void catTextOff()
    {
        catWindow.SetActive(false);
    }

    IEnumerator suicideRecast()
    {
        yield return new WaitForSeconds(1.0f);
        resetSwitch = false;
    }
}
