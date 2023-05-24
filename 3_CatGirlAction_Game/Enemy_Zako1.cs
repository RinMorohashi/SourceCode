using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zako1 : MonoBehaviour
{
    /// <summary>
    /// �U�R�G�𓮂����X�N���v�g
    /// </summary>
    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("��ʊO�ł��s������")] public bool nonVisibleAct;
    [Header("�ڐG����")] public enemyCollisionCheck checkCollision;
    [Header("���Z�X�R�A")] public int myScore;
    [Header("���񂾂Ƃ��ɖ炷SE")] public AudioClip downSE;
    [Header("�v���C���[�I�u�W�F�N�g")] public GameObject playerObj;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private bool rightTleftF = false;
    private bool isHit = false;
    private bool isDead = false;
    private string swordTag = "Sword";
    private player p;
    private CircleCollider2D cc = null;
    private bool theWorld = false;
    private float timeStopped;
    #endregion

    void Start()
    {
        playerObj = GameObject.Find("W1");
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
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
            }
            //���񂾂Ƃ�
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
            anim.Play("zako1Down");
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
}
