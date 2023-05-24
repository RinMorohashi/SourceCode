using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstImpression : MonoBehaviour
{
    /// <summary>
    /// �^�C�g����ʂ𓮂����X�N���v�g
    /// </summary>
    [Header("�^�C�g����ʂ̔w�i�摜")] public GameObject titleBackGround;
    [Header("�^�C�g����ʂ̔w�i�摜2")] public GameObject titleBackGroundZwei;
    [Header("�^�C�g����ʂ̔w�i�摜3")] public GameObject titleBackGroundDrei;
    [Header("�^�C�g�����S")] public GameObject titlelogo;
    [Header("�Q�[���X�^�[�g�{�^��")] public GameObject startButton;
    [Header("�I�v�V�����{�^��")] public GameObject optionButton;
    [Header("�N���W�b�g�{�^��")] public GameObject creditButton;

    private Image titleLogo = null;
    public bool isComplete = false;

    void Start()
    {
        titleLogo = titlelogo.GetComponent<Image>();
        if (GamaManager.instance.trueEnd)
        {
            titleBackGround.SetActive(false);
            titleBackGroundZwei.SetActive(false);
        }
        else if (GamaManager.instance.badEnd)
        {
            titleBackGround.SetActive(false);
            titleBackGroundDrei.SetActive(false);
        }
        else
        {
            titleBackGroundDrei.SetActive(false);
            titleBackGroundZwei.SetActive(false);
        }
            StartCoroutine("scrollTitleBG");
    }

    IEnumerator scrollTitleBG()
    {
        yield return new WaitForSeconds(1.0f);
        if (GamaManager.instance.trueEnd)
        {
            titleBackGround.SetActive(false);
            titleBackGroundZwei.SetActive(false);
            for (int i = 0; i < 210; i++)
            {
                titleBackGroundDrei.transform.localPosition += new Vector3(0f, 2f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 50; i++)
            {
                titleLogo.color += new Color(0f, 0f, 0f, 0.02f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                startButton.transform.localPosition = new Vector3(-345f - 100 / (i + 1), -110f, 0f);
                optionButton.transform.localPosition = new Vector3(-325f - 100 / (i + 1), -165f, 0f);
                creditButton.transform.localPosition = new Vector3(-305f - 100 / (i + 1), -220f, 0f);
                yield return new WaitForSeconds(0.08f);
            }
        }
        else if (GamaManager.instance.badEnd)
        {
            titleBackGround.SetActive(false);
            titleBackGroundDrei.SetActive(false);
            for (int i = 0; i < 210; i++)
            {
                titleBackGroundZwei.transform.localPosition += new Vector3(0f, 2f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 50; i++)
            {
                titleLogo.color += new Color(0f, 0f, 0f, 0.02f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                startButton.transform.localPosition = new Vector3(-345f - 100 / (i + 1), -110f, 0f);
                optionButton.transform.localPosition = new Vector3(-325f - 100 / (i + 1), -165f, 0f);
                creditButton.transform.localPosition = new Vector3(-305f - 100 / (i + 1), -220f, 0f);
                yield return new WaitForSeconds(0.08f);
            }
        }
        else
        {
            titleBackGroundDrei.SetActive(false);
            titleBackGroundZwei.SetActive(false);
            for (int i = 0; i < 210; i++)
            {
                titleBackGround.transform.localPosition += new Vector3(0f, 2f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 50; i++)
            {
                titleLogo.color += new Color(0f, 0f, 0f, 0.02f);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                startButton.transform.localPosition = new Vector3(-345f - 100 / (i + 1), -110f, 0f);
                optionButton.transform.localPosition = new Vector3(-325f - 100 / (i + 1), -165f, 0f);
                creditButton.transform.localPosition = new Vector3(-305f - 100 / (i + 1), -220f, 0f);
                yield return new WaitForSeconds(0.08f);

            }
        }
        isComplete = true;
    }

    public void OnClickBGImageButton()
    {
        Debug.Log("�����ꂽ��");
        StopAllCoroutines();
        titleBackGround.transform.localPosition = new Vector3(0f, 210f, 0f);
        titleBackGroundZwei.transform.localPosition = new Vector3(0f, 210f, 0f);
        titleBackGroundDrei.transform.localPosition = new Vector3(0f, 210f, 0f);
        titleLogo.color = new Color(1f, 1f, 1f, 1f);
        startButton.transform.localPosition = new Vector3(-350f, -110f, 0f);
        optionButton.transform.localPosition = new Vector3(-330f, -165f, 0f);
        creditButton.transform.localPosition = new Vector3(-310f, -220f, 0f);

        isComplete = true;
    }
}
