using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZako2 : MonoBehaviour
{
    /// <summary>
    /// ザコ敵２を動かすスクリプト
    /// </summary>
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public enemyCollisionCheck checkCollision;
    [Header("加算スコア")] public int myScore;
    [Header("死んだときに鳴らすSE")] public AudioClip downSE;
    [Header("プレイヤーオブジェクト")] public GameObject playerObj;
    [Header("ジャンプの速さ表現")] public AnimationCurve jumpCurve;
    public bool isJump;
    public bool alreadyJumped;
    public bool isFalling;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private bool rightTleftF = false;
    private bool isHit = false;
    private bool isDead = false;
    private string swordTag = "Sword";
    private player p;
    private CircleCollider2D cc = null;
    private float ySpeed;
    private bool sky = false;
    private bool theWorld = false;
    private float timeStopped;
    #endregion

    void Start()
    {
        ySpeed = 10f * speed;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CircleCollider2D>();
        p = playerObj.GetComponent<player>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (sr.isVisible || nonVisibleAct)
            {
                isJump = IsWalkAnimEnd();
                alreadyJumped = IsJumpUpAnimEnd();
                isFalling = IsJumpDownAnimEnd();
                if (isJump)
                {
                    StartCoroutine("jumpUp");
                    anim.Play("zako2JumpUp");
                    sky = true;
                }
                if (alreadyJumped)
                {
                    StartCoroutine("jumpDown");
                    anim.Play("zako2JumpDown");
                    sky = true;
                }
                if (isFalling)
                {
                    anim.Play("zako2Stand");
                    sky = false;
                }

                if (checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }

                int xVector = -1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-2.5f, 2.5f, 1);
                }
                else
                {
                    transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }

                if (sky == true)
                {
                    rb.velocity = new Vector2(0.3f * xVector * speed, ySpeed);
                }
                else
                {
                    rb.velocity = new Vector2(1.5f * xVector * speed, ySpeed);
                }
            }
            else
            {
                rb.Sleep();
            }
            //死んだとき
            if (isHit)
            {
                if (GamaManager.instance != null)
                {

                    GamaManager.instance.score += myScore;
                }
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
            }
        }
        else
        {
            cc.isTrigger = true;
            StartCoroutine("Knockback");
        }
    }

    void Update()
    {
        if (theWorld)
        {
            timeStopped += Time.unscaledDeltaTime;
            if (timeStopped >= 0.0f && timeStopped < 0.06f)
            {
                transform.localPosition += new Vector3(-0.02f, 0f, 0f);
            }
            else if (timeStopped >= 0.06f && timeStopped < 0.12f)
            {
                transform.localPosition -= new Vector3(-0.02f, 0f, 0f);
            }
            else if (timeStopped >= 0.12f && timeStopped < 0.18f)
            {
                transform.localPosition += new Vector3(-0.02f, 0f, 0f);
            }
            else if (timeStopped >= 0.18f && timeStopped < 0.24f)
            {
                transform.localPosition -= new Vector3(-0.02f, 0f, 0f);
            }
            else if (timeStopped >= 0.24f && timeStopped < 0.32f)
            {
                transform.localPosition += new Vector3(-0.02f, 0f, 0f);
            }
            if (timeStopped >= 0.32f)
            {
                timeStopped = 0;
                theWorld = false;
                Time.timeScale = 1.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == swordTag)
        {
            theWorld = true;
            GamaManager.instance.PlaySE(downSE);
            anim.Play("zako2Down");
            Time.timeScale = 0;
            isHit = true;
        }
    }

    IEnumerator Knockback()
    {
        bool isLeft = p.isLeft;

        for (int i = 0; i < 10; i++)
        {
            float sin = Mathf.Sin((i / 3.2f) + 1.57f);
            if (isLeft)
            {
                transform.localPosition += new Vector3(-0.01f, 0.03f * sin, 0f);
            }
            else
            {
                transform.localPosition += new Vector3(0.01f, 0.03f * sin, 0f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject, 0.5f);
    }

    //歩きアニメーションが完了しているかどうか
    private bool IsWalkAnimEnd()
    {
        if (!isDead && anim != null)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("zako2Stand"))
            {
                if (currentState.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsJumpUpAnimEnd()
    {
        if (!isDead && anim != null)
        {
            AnimatorStateInfo currentState2 = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState2.IsName("zako2JumpUp"))
            {
                if (currentState2.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsJumpDownAnimEnd()
    {
        if (!isDead && anim != null)
        {
            AnimatorStateInfo currentState3 = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState3.IsName("zako2JumpDown"))
            {
                if (currentState3.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator jumpUp()
    {
        for (float i = 0; i < 10; i++)
        {
            ySpeed = 2 * speed * jumpCurve.Evaluate(i/10);
            yield return new WaitForSeconds(0.03f);
        }
    }
    IEnumerator jumpDown()
    {
        for (float i = 0; i < 10; i++)
        {
            ySpeed = -2.2f * speed * jumpCurve.Evaluate((10-i)/ 10);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
