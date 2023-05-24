using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownFloor : MonoBehaviour
{
    /// <summary>
    /// ���Ɨ����鏰�𓮂����X�N���v�g
    /// </summary>
    [Header("�X�v���C�g������I�u�W�F�N�g")] public GameObject spriteObj;
    [Header("�U����")] public float vibrationWidth = 0.05f;
    [Header("�U�����x")] public float vibrationSpeed = 30.0f;
    [Header("������܂ł̎���")] public float fallTime = 1.0f;
    [Header("�����Ă������x")] public float fallSpeed = 10.0f;
    [Header("�����Ă���߂��Ă��鎞��")] public float returnTime = 5.0f;

    private bool isOn;
    private bool isFall;
    private bool isReturn;
    private Vector3 spriteDefaultPos;
    private Vector3 floorDefaultPos;
    private Vector2 fallVelocity;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private ObjectCollision oc;
    private SpriteRenderer sr;
    private float timer = 0.0f;
    private float fallingTimer = 0.0f;
    private float returnTimer = 0.0f;
    private float blinkTimer = 0.0f;


    private void Start()
    {
        //�����ݒ�
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        oc = GetComponent<ObjectCollision>();
        if (spriteObj != null && oc != null && col != null && rb != null)
        {
            spriteDefaultPos = spriteObj.transform.position;
            fallVelocity = new Vector2(0, -fallSpeed);
            floorDefaultPos = gameObject.transform.position;
            sr = spriteObj.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.Log("fallDownFloor �C���X�y�N�^�[�ɐݒ肵�Y�ꂪ����܂�");
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("fallDownFloor �C���X�y�N�^�[�ɐݒ肵�Y�ꂪ����܂�");
            Destroy(this);
        }
    }

    private void Update()
    {
        //�v���C���[��1��ł��������t���O���I����
        if (oc.playerStepOn)
        {
            isOn = true;
            oc.playerStepOn = false;
        }

        //�v���C���[���̂��Ă��痎����܂ł̊�
        if (isOn && !isFall)
        {
            //�k������
            spriteObj.transform.position = spriteDefaultPos + new Vector3(Mathf.Sin(timer * vibrationSpeed) * vibrationWidth, 0, 0);

            //��莞�Ԃ������痎����
            if (timer > fallTime)
            {
                isFall = true;
            }

            timer += Time.deltaTime;
        }

        //��莞�Ԃ��Ɩ��ł��Ė߂��Ă���
        if (isReturn)
        {
            //���Ł@���Ă��鎞�ɖ߂�
            if (blinkTimer > 0.2f)
            {
                sr.enabled = true;
                blinkTimer = 0.0f;
            }
            //���Ł@�����Ă���Ƃ�
            else if (blinkTimer > 0.1f)
            {
                sr.enabled = false;
            }
            //���Ł@���Ă���Ƃ�
            else
            {
                sr.enabled = true;
            }

            //1�b�������疾�ŏI���
            if (returnTimer > 1.0f)
            {
                isReturn = false;
                blinkTimer = 0f;
                returnTimer = 0f;
                sr.enabled = true;
            }
            else
            {
                blinkTimer += Time.deltaTime;
                returnTimer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        //������
        if (isFall)
        {
            rb.velocity = fallVelocity;

            //��莞�Ԃ��ƌ��̈ʒu�ɖ߂�
            if (fallingTimer > returnTime)
            {
                isReturn = true;
                transform.position = floorDefaultPos;
                rb.velocity = Vector2.zero;
                isFall = false;
                timer = 0.0f;
                fallingTimer = 0.0f;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isOn = false;
            }
        }
    }
}
