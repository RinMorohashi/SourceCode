using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのジャンプ、攻撃などの動作と会話イベントを制御するスクリプト
    /// </summary>
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプする高さ")] public float jumpHeight;
    [Header("ジャンプ制限時間")] public float jumpLimitTime;
    [Header("接地判定")] public GC ground;
    [Header("頭をぶつけた判定")] public GC head;
    [Header("ダッシュの速さ表現")] public AnimationCurve dashCurve;
    [Header("ジャンプの速さ表現")] public AnimationCurve jumpCurve;
    [Header("空中攻撃の上昇表現")] public AnimationCurve airialAttackCurve;
    [Header("踏みつけ判定の高さの割合(%)")] public float stepOnRate;
    [Header("攻撃の当たり判定")] public GameObject sworddetection;
    [Header("ジャンプする時に鳴らすSE")] public AudioClip jumpSE; //New!
    [Header("攻撃するときに鳴らすSE")] public AudioClip attackSE; //New
    [Header("死んだときに鳴らすSE")] public AudioClip tooBadSE;
    [Header("剣の斬撃エフェクト")] public GameObject SSE;
    [Header("空中の斬撃エフェクト")] public GameObject SSE2;
    [Header("猫の吹き出し")] public GameObject catWindow;
    [Header("猫の吹き出しの文字")] public GameObject catWindowText;
    [Header("幼女の吹き出し")] public GameObject yojoWindow;
    [Header("幼女の吹き出しの選択肢１")] public GameObject yojoWindowText1;
    [Header("幼女の吹き出しの選択肢２")] public GameObject yojoWindowText2;
    [Header("幼女の選択している選択肢")] public GameObject yojoWindowText;
    [Header("犬の吹き出し")] public GameObject dogWindow;
    [Header("犬の吹き出しの文字")] public GameObject dogWindowText;
    [Header("コイン獲得SE")] public AudioClip coinSE;
    [Header("メッセージ送りSE")] public AudioClip talkingSE;
    [Header("ステージ入った時のステージ名前表示")] public stageNumberSlide sns;
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

    #region//プライベート変数
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
                //接地判定を得る
                isGround = ground.IsGround();

                if (boatSwitch)
                {
                    isGround = true;
                }

                isHead = head.IsGround();
                isHead = false;

                float xSpeed = 0;
                float ySpeed = 0;
                //各種座標軸の速度を求める
                if (!GamaManager.instance.talkEventSwitch && !GamaManager.instance.eventSwitch1to2 && !GamaManager.instance.eventSwitch2to3 && !GamaManager.instance.eventSwitch3to4 && !GamaManager.instance.eventSwitch4to5 && !GamaManager.instance.eventSwitch2to1 && !GamaManager.instance.eventSwitch3to2 && !GamaManager.instance.eventSwitch4to3 && !GamaManager.instance.eventSwitch5to4)
                {
                    xSpeed = GetXSpeed();
                    ySpeed = GetYSpeed();
                }

                //アニメーションを適用
                SetAnimation();

                //移動速度を設定
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

                //攻撃ボタン押したとき
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
                //明滅　ついている時に戻る
                if (blinkTime > 0.2f)
                {
                    sr.enabled = true;
                    blinkTime = 0.0f;
                }
                //明滅　消えているとき
                else if (blinkTime > 0.1f)
                {
                    sr.enabled = false;
                }
                //明滅　ついているとき
                else
                {
                    sr.enabled = true;
                }

                //1秒たったら明滅終わり
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
                Debug.Log("ここからトゥルーエンド");
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
    /// Y成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>Y軸の速さ</returns>
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
                        jumpPos = transform.position.y; //ジャンプした位置を記録する
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
            //上方向キーを押しているか
            bool pushUpKey = verticalKey > 0;
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
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
                jumpPos = transform.position.y; //ジャンプした位置を記録する
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
    /// X成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>X軸の速さ</returns>
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

        //前回の入力からダッシュの反転を判断して速度を変える
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
    /// アニメーションを設定する
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }

    #region//接触判定 New!      
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
        //動く床
        else if (collision.collider.tag == moveFloorTag)
        {
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
            foreach (ContactPoint2D p in collision.contacts)
            {
                //動く床に乗っている
                if (p.point.y < judgePos)
                {
                    moveObj = collision.gameObject.GetComponent<MoveObject>();
                }           
            }
        }
        //落ちる床
        else if (collision.collider.tag == "FallFloor")
        {
            o.playerStepOn = true;
        }
    }
    #endregion

    /// <summary>
    /// コンティニュー待機状態か
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

    //ダウンアニメーションが完了しているかどうか
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
    /// コンティニューする
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
            //動く床から離れた
            moveObj = null;
        }
    }

    //やられた時の処理
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
        else if (fire1Key > 0 && isGround == false && isAirialAttack == false　&& GamaManager.instance.airialAttackSwitch)
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
            Debug.Log("ステージ２のフェードインが動きました");
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
                    catText.text = "…まだ聞こえてるか？";
                    break;
                case 2:
                    catText.text = "あのときは、俺を助けてくれたんだよな。\nありがとう";
                    break;
                case 3:
                    catText.text = "結局どっちも轢かれちまったけど。";
                    break;
                case 4:
                    catText.text = "はじめは、お前を天国まで\n見送るつもりだったけど…";
                    break;
                case 5:
                    catText.text = "こういうエンディングもありだよな。";
                    break;
                case 6:
                    catText.text = "お前さえ良ければ…\nあっちでもまた一緒に冒険しようぜ。";
                    break;
                case 7:
                    catText.text = "だから…また会おう。約束だ。";
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
                    catText.text = "元の世界に戻りたいか？";
                    break;
                case 2:
                    yojoText.text = "うん";
                    yojoText2.text = "いや";
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
                            catText.text = "知ってるか？";
                        }
                        else
                        {
                            catText.text = "そうか。";
                        }
                    }
                    break;
                case 4:
                    if (isHidari)
                    {
                        catText.text = "猫には９つの命があるんだ。迷信じゃないぜ。";
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
                    catText.text = "だから俺は元の世界に戻れる。";
                    break;
                case 6:
                    catText.text = "そこで提案だが、命をひとつお前にやるよ。";
                    break;
                case 7:
                    catText.text = "できるか分からないけど、やってみる。";
                    break;
                case 8:
                    catText.text = "まだ生きたいんだろ？俺に付いてこい。";
                    break;
                case 9:
                    //エンディングへ
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
            Debug.Log("もしかしてここ？");
            catWindow.SetActive(true);
            switch (textNum)
            {
                case 0:
                    break;
                case 1:
                    catText.text = "おっと、ここから先は進めないぜ。";
                    break;
                case 2:
                    catText.text = "この先はお前が元いた世界に続いてる。だが、";
                    break;
                case 3:
                    catText.text = "帰れるのは生きてる奴だけだ。";
                    break;
                case 4:
                    catText.text = "お前は死んでるから\n戻れない。";
                    break;
                case 5:
                    catText.text = "そう、ここはあの世とこの世の境界なんだ。";
                    break;
                case 6:
                    Debug.Log("ここで戻る");
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
                    catText.text = "ようやく目が覚めたな。\nこの世界は初めてか？";
                    break;
                case 2:
                    catText.text = "この辺化け物とか出るから、気を付けろよ。";
                    break;
                case 3:
                    yojoWindow.SetActive(true);
                    sentakusi = true;
                    break;
                case 4:
                    if (sentakusi != true)
                    {
                        Debug.Log("ここで幼女の吹き出しが消える");
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "ここは世界の境界だ"/*"見ない顔だな。"*/;
                        }
                        else
                        {
                            catText.text = "ただの猫だ。名前とかは無い。";
                        }
                    }
                    break;
                case 5:
                    if (isHidari)
                    {
                        catText.text = "上の世界と下の世界があって、\nその境界がここってわけだな。"/*"どこから来たんだ？\n此処は子供が来るような所じゃないんだ。"*/;
                    }
                    else
                    {
                        catText.text = "この世界には何回か来たことあるんだぜ。"/*"お前こそ誰だ？"*/;
                    }
                    break;
                case 6:
                    yield return new WaitForSeconds(1.0f);
                    catText.text = "この辺を案内してやるよ。\n一人じゃ危ないからな。"/*ワケありか。*/;
                    break;
                case 7:
                    transform.localScale = new Vector3(-1, 1, 1);
                    catText.text = "ここに来た人間はみんな右へ進むんだ。\n行ってみようぜ。"/*"よし、ここを案内してやるよ。"*/;
                    break;
                case 8:
                    catText.text = " ";
                    yield return new WaitForSeconds(0.5f);
                    catText.text = "化け物が出たら守ってやるよ。"/*"お前には行くべき場所があるんだろ？\nたぶん右の方角だ。"*/;
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
                    catText.text = "人間には全員名前があるんだろ？";
                    break;
                case 2:
                    yojoWindow.SetActive(true);
                    yojoWindowText2.SetActive(false);
                    yojoWindowText.SetActive(false);
                    yojoWindowText1.transform.localPosition = new Vector3(0f, 10f, 0f);
                    yojoText.text = "(私の名前…\n思い出せない…)";
                    break;
                case 3:
                    yojoWindowText2.SetActive(true);
                    yojoWindowText.SetActive(true);
                    yojoWindowText1.transform.localPosition = new Vector3(-72f, 10f, 0f);
                    yojoText.text = "分からない。";
                    sentakusi = true;
                    break;
                case 4:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "頭をやられたのか。"/*"記憶が無いのか。"*/;
                        }
                        else
                        {
                            catText.text = "「ジバニャン」か。悪くない名前だ。";
                        }
                    }
                    break;
                case 5:
                    if (sentakusi != true)
                    {
                        if (isHidari)
                        {
                            catText.text = "その方が気楽で良い。";
                        }
                        else
                        {
                            catText.text = "「ニャン」の所とか猫っぽくていいな。";
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
                    catText.text = "実はそんな強く無いんだ、俺。";
                    break;
                case 2:
                    catText.text = "人が倒れてたから心配で\n付いてきてるだけなんだ。";
                    break;
                case 3:
                    catText.text = "いや、心配とかじゃなくて\nお前に興味があるだけなのかも。";
                    break;
                case 4:
                    catText.text = "いつか思い出せるといいな。記憶。";
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
                    catText.text = "もうすぐで到着だぜ。";
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
                            catText.text = "河原だ。それしか分からない。";
                        }
                        else
                        {
                            catText.text = "そういう設定だからな。";
                        }
                    }
                    break;
                case 4:
                    if (isHidari)
                    {
                        catText.text = "行ったことないから、よく知らないんだ。\nすまねえ。";
                    }
                    else
                    {
                        catText.text = "この世界じゃ普通のことだぜ。";
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
                    dogText.text = "川を渡るならこの舟に乗るですワン。";
                    break;
                case 3:
                    dogText.text = "運賃は金貨６枚ですワン。";
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
                            dogText.text = "それではよい旅を～。";
                        }
                        else
                        {
                            dogText.text = "やめるワン？";
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
                        catText.text = "俺の案内はここまでだ。";
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
                            dogText.text = "こんにちワン。";
                        }

                        textNum = 0;
                    }
                    break;
                case 7:
                    if (isHidari)
                    {
                        catText.text = "お別れだな。";
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
                    yojoText.text = "別れを言う";
                    yojoText2.text = "一緒に行こう。";
                    sentakusi = true;
                    break;
                case 9:
                    if (sentakusi != true)
                    {
                        sentakusiTime = 0;
                        yojoWindow.SetActive(false);
                        if (isHidari)
                        {
                            catText.text = "ああ。楽しい散歩だったぜ。";
                        }
                        else
                        {
                            catText.text = "悪いが一緒には行けない。";
                        }
                    }
                    break;
                case 10:
                    if (isHidari)
                    {
                        catText.text = "お前みたいな人間は初めてだからな。";
                    }
                    else
                    {
                        catText.text = "ここから先は人間だけが行けるんだ。";
                    }
                    break;
                case 11:
                    if (isHidari)
                    {
                        catText.text = "あっちでも元気でな。";
                    }
                    else
                    {
                        catText.text = "じゃあな。";
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
                        catText.text = "あっちでも元気でな。";
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
            Debug.Log("会話を進めます");
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
                    yojoText2.text = "ジバニャンっていうの。";
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
        //ボートが右に進む
        //カメラは動かさない
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
        //猫が左へフェードアウト
        //暗転してスタッフロール
        //スコア表示
        //２段ジャンプがあればあの壁を越えられたかも…
    }

    public void catTalksTrueOn()
    {
        catWindow.SetActive(true);
        switch (GamaManager.instance.stageNum)
        {
            case 1:
                catText.text = "こっちに行くのか？\nまあいいけど";
                if (transform.position.x <= -12f && transform.position.x >= -20f)
                {
                    catText.text = "←→：移動　↑：ジャンプ　z：攻撃";
                    catZatudan = true;
                }
                break;
            case 2:
                break;
            case 3:
                catText.text = "空中攻撃で遠くまで飛べるぜ。";
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
