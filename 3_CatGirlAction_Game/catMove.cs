using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMove : MonoBehaviour
{
    /// <summary>
    /// ��l���ɒǏ]����l�R�̓���𐧌䂷��X�N���v�g
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
    [Header("���݂�����̍����̊���(%)")] public float stepOnRate;
    [Header("�W�����v���鎞�ɖ炷SE")] public AudioClip jumpSE; //New!
    [Header("�v���C���[�I�u�W�F�N�g")] public GameObject yojo;
    [Header("����ȏ㗣�ꂽ��c���̏ꏊ�Ƀ��[�v")] public float dis;
    [Header("�c���Ƃ̃\�[�V�����f�B�X�^���X")] public float socialDis;
    #endregion

    #region//��Ƀv���C�x�[�g�ϐ�
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
            //�ڒn����𓾂�
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //�e����W���̑��x�����߂�
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

                //�A�j���[�V������K�p
                SetAnimation();

            //�ړ����x��ݒ�
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
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>Y���̑���</returns>
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
                    jumpPos = transform.position.y; //�W�����v�����ʒu���L�^����
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
    /// X�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>X���̑���</returns>
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
    /// �A�j���[�V������ݒ肷��
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }

    #region//�ڐG����    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == moveFloorTag)
        {
            //���������痣�ꂽ
            moveObj = null;
        }
    }

    IEnumerator JumpRecast()
    {
        yield return new WaitForSeconds(1.5f);
        jumpInterval = false;
    }
}
