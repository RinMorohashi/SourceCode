using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMove : MonoBehaviour
{
    /// <summary>
    /// 主人公に追従するネコの動作を制御するスクリプト
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
    [Header("踏みつけ判定の高さの割合(%)")] public float stepOnRate;
    [Header("ジャンプする時に鳴らすSE")] public AudioClip jumpSE; //New!
    [Header("プレイヤーオブジェクト")] public GameObject yojo;
    [Header("これ以上離れたら幼女の場所にワープ")] public float dis;
    [Header("幼女とのソーシャルディスタンス")] public float socialDis;
    #endregion

    #region//主にプライベート変数
    private AudioSource oodio;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    public bool isGround = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private bool isHead = false;
    private bool isRun = false;
    private bool isDown = false;
    public bool isAttack = false;
    public float dashTime, jumpTime;
    private float beforeKey;
    private string enemyTag = "enemy";
    private SpriteRenderer sr = null;
    private string moveFloorTag = "MoveFloor";
    private MoveObject moveObj = null;
    public bool jumpInterval;
    private Vector3 posYojo;
    private Vector3 posCat;
    public float ySpeed;
    private bool yCoroutineSwitch = false;
    private player ps;
    private bool stopSwitch;
    private float stopLatency = 0f;
    public bool stop = false;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        oodio = GetComponent<AudioSource>();
        ps = yojo.GetComponent<player>();
        ySpeed = -gravity;
    }

    void FixedUpdate()
    {
        posYojo = yojo.transform.position;
        posCat = transform.position;
        float distance = Vector3.Distance(posYojo, posCat);

        if (distance >= dis && !ps.boatSwitch)
        {
            transform.position = new Vector3(-1 + posYojo[0], posYojo[1],posYojo[2]);
        }

        if (isDown == false && isAttack == false)
        {
            //接地判定を得る
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //各種座標軸の速度を求める
            float xSpeed = 0;
            if (!stop)
            {
                xSpeed = GetXSpeed();
                float verticalKey = Input.GetAxis("Vertical");
                if ((verticalKey > 0) && (isGround) && (jumpInterval == false) && (yCoroutineSwitch == false))
                {
                    StartCoroutine("YSpeedCalculater");
                    yCoroutineSwitch = true;
                }
            }

                //アニメーションを適用
                SetAnimation();

            //移動速度を設定
            Vector2 addVelocity = Vector2.zero;
            if (moveObj != null)
            {
                addVelocity = moveObj.GetVelocity();
            }
            rb.velocity = new Vector2(xSpeed, ySpeed) + addVelocity;

        }
        else
        {
            rb.velocity = new Vector2(0, -gravity);
        }
        if (ps.boatSwitch)
        {
            stopLatency += Time.deltaTime;
            if (stopLatency >= 1.0f)
            {
                stopSwitch = true;
            }
        }
    }

    /// <summary>
    /// Y成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>Y軸の速さ</returns>
    IEnumerator YSpeedCalculater()
    {
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeedD = -gravity;
        yield return new WaitForSeconds(0.4f);

        if (!stopSwitch)
        {
            if (isGround)
            {
                if (jumpInterval != true)
                {
                    oodio.Play();

                    ySpeedD = jumpSpeed;
                    jumpPos = transform.position.y; //ジャンプした位置を記録する
                    jumpInterval = true;
                    StartCoroutine("JumpRecast");
                    jumpTime = 0.0f;
                }

            }

            for (int i = 0; i < 100; i++)
            {
                bool canHeight = jumpPos + jumpHeight > transform.position.y;
                bool canTime = jumpLimitTime > jumpTime;
                if (canHeight && canTime && !isHead)
                {
                    ySpeedD = jumpSpeed;
                    jumpTime += Time.deltaTime;
                }
                else
                {
                    jumpTime = 0.0f;
                    ySpeed = -gravity;
                    yCoroutineSwitch = false;
                    yield break;
                }
                ySpeedD *= jumpCurve.Evaluate(jumpTime);
                ySpeed = ySpeedD;
                yield return new WaitForFixedUpdate();
            }
        }
        ySpeed = -gravity;
    }

    /// <summary>
    /// X成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>X軸の速さ</returns>
    private float GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;

        if (posYojo[0] - socialDis >= posCat[0] /*horizontalKey > 0*/)
        {
            if (!GamaManager.instance.vanguard)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (posYojo[0] + socialDis <= posCat[0] /*horizontalKey < 0*/)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
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

        if (stopSwitch)
        {
            xSpeed = 0f;
            isRun = false;
        }
        if (GamaManager.instance.vanguard)
        {
            xSpeed = -speed;
        }

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

    #region//接触判定    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == moveFloorTag)
        {
            //動く床から離れた
            moveObj = null;
        }
    }

    IEnumerator JumpRecast()
    {
        yield return new WaitForSeconds(1.5f);
        jumpInterval = false;
    }
}
