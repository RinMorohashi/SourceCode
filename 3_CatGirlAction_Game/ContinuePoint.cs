using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePoint : MonoBehaviour
{
    /// <summary>
    /// �Z�[�u�|�C���g�A�󔠂̓���𐧌䂷��X�N���v�g
    /// </summary>
    [Header("�R���e�B�j���[�ԍ�")] public int continueNum;
    [Header("��")] public AudioClip se;
    [Header("�v���C���[����")] public PlayerTriggerCheck trigger;
    [Header("�X�s�[�h")] public float speed = 3.0f;
    [Header("������")] public float moveDis = 3.0f;
    [Header("���Ԓn�_�p�e�L�X�g")] public GameObject savePoint;
    [Header("��1�Ȃ�I���ɂ���")] public bool isTreasure1;
    [Header("��2�Ȃ�I���ɂ���")] public bool isTreasure2;

    private bool on = false;
    private float kakudo = 0.0f;
    private Vector3 defaultPos;
    private Text saveText;
    private bool saveSwitch = false;
    private SpriteRenderer sr;
    private Animator anim = null;
    void Start()
    {
        //������
        if (trigger == null)
        {
            Debug.Log("�C���X�y�N�^�[�̐ݒ肪����܂���");
            Destroy(this);
        }
        defaultPos = transform.position;

        savePoint.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("continueFlag");
        }

        if (isTreasure1 && GamaManager.instance.airialAttackSwitch)
        {
            GameObject go = GameObject.Find("continuePointTreasure");
            go.SetActive(false);
        }
        if (isTreasure2 && GamaManager.instance.zweiJumpSwitch)
        {
            GameObject go = GameObject.Find("continuePointTreasure");
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[���͈͓��ɓ�����
        if (trigger.isOn && !on)
        {
            if (!isTreasure1 && !isTreasure2)
            {
                GamaManager.instance.continueNum = continueNum;
            }
            else if (isTreasure1 == true)
            {
                GamaManager.instance.airialAttackSwitch = true;
            }
            else if (isTreasure2 == true)
            {
                GamaManager.instance.zweiJumpSwitch = true;
            }
            on = true;
            if (GamaManager.instance.stageNum == 1 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 2 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 2 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 3 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 3 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 4 && (continueNum == 1) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
            else if (GamaManager.instance.stageNum == 4 && (continueNum == 2) && (saveSwitch == false))
            {
                saveSwitch = true;
                StartCoroutine("saved");
            }
        }

        if (on)
        {
            if (kakudo < 180.0f)
            {
                //sin�J�[�u�ŐU��������
                transform.position = defaultPos + Vector3.up * moveDis * Mathf.Sin(kakudo * Mathf.Deg2Rad);

                //�r�����炿�����Ⴍ�Ȃ�
                if (kakudo > 90.0f)
                {
                    transform.localScale = Vector3.one * (1 - ((kakudo - 90.0f) / 90.0f));
                }
                kakudo += 180.0f * Time.deltaTime * speed;
            }
            else
            {
                sr.color = new Vector4(1f,1f,1f,0f);
                on = false;
            }
        }
    }

    IEnumerator saved()
    {
        GamaManager.instance.PlaySE(se);
        savePoint.SetActive(true);
        for (int i = 0; i < 60; i++)
        {
            savePoint.transform.position += new Vector3(0f, 0.01f, 0f);
            yield return new WaitForSeconds(0.03f);
        }
        savePoint.SetActive(false);
        savePoint.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -200f, 0);
    }
}
