using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class odioPlayer : MonoBehaviour
{
    /// <summary>
    /// BGM��ύX����X�N���v�g
    /// </summary>
    private AudioSource audioSource = null;
    [Header("�J�m��")] public AudioClip canonSE;
    [Header("�{����")] public AudioClip boleroSE;
    [Header("�n���K���[���ȑ�T��")] public AudioClip hangarian5SE;
    [Header("G����̃A���A")] public AudioClip GSE;
    [Header("����")] public AudioClip moonSE;
    [Header("�A�C�l�E�N���C�l�E�i�n�g���W�[�N")] public AudioClip musikSE;
    [Header("�E�B���A���E�e������")] public AudioClip WilliamSE;
    [Header("���z������")] public AudioClip fantasySE;
    [Header("�q���̃����c")] public AudioClip minuteWaltzSE;
    [Header("�A���C�W���O�E�O���C�X")] public AudioClip amazingSE;
    public bool isPushed = false;
    public int musicNumber = 0;
    public GameObject bgmText;
    private RectTransform bgmTextPos;
    private Text bgmt;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgmText = GameObject.Find("BGMText");
        if (bgmText == null)
        {
            Debug.Log("bgmText��������Ȃ�");
        }
        bgmTextPos = bgmText.GetComponent<RectTransform>();
        bgmt = bgmText.GetComponent<Text>();
        canon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && !isPushed)
        {
            Debug.Log("BGM��ύX�����I");
            isPushed = true;
            musicNumber++;
            StartCoroutine("recast");
            StartCoroutine("musicBox");
        }
    }

    IEnumerator recast()
    {
        bgmTextPos.anchoredPosition = new Vector3(400f, 34.3f, 0f);
        for (int i = 0; i < 20; i++)
        {
            bgmTextPos.anchoredPosition = new Vector3(400f - i, 34.3f, 0f);
            yield return new WaitForSeconds(0.025f);
        }
        
        isPushed = false;
    }

    IEnumerator musicBox()
    {
        switch (musicNumber)
        {
            case 0:
                canon();
                bgmt.text = "��J�m���i�p�b�w���x���j";
                break;
            case 1:
                bolero();
                bgmt.text = "��{�����i�����F���j";
                break;
            case 2:
                hungarian5();
                bgmt.text = "��n���K���[���ȑ�T�ԁi�u���[���X�j";
                break;
            case 3:
                GAria();
                bgmt.text = "��f����̃A���A�i�o�b�n�j";
                break;
            case 4:
                moon();
                bgmt.text = "�􌎌��i�x�[�g�[���F���j";
                break;
            case 5:
                musik();
                bgmt.text = "��A�C�l�E�N���C�l�E�i�n�g���W�[�N\n�i���[�c�@���g�j";
                break;
            case 6:
                William();
                bgmt.text = "��E�B���A���e�����ȁi���b�V�[�j�j";
                break;
            case 7:
                fantasy();
                bgmt.text = "�􌶑z�����ȁi�V���p���j";
                break;
            case 8:
                minuteWaltz();
                bgmt.text = "��q���̃����c�i�V���p���j";
                break;
            case 9:
                amazing();
                bgmt.text = "��A���C�W���O�O���C�X\n�i�W�����E�j���[�g���j";
                musicNumber = -1;
                break;
            default:
                break;
        }
        yield return null;
    }

    public void canon()
    {
        audioSource.Stop();
        audioSource.clip = canonSE;
        audioSource.time = 88.3f;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
    public void bolero()
    {
        audioSource.Stop();
        audioSource.clip = boleroSE;
        audioSource.time = 10f;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
    public void hungarian5()
    {
        audioSource.Stop();
        audioSource.clip = hangarian5SE;
        audioSource.time = 0f;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
    public void GAria()
    {
        audioSource.Stop();
        audioSource.clip = GSE;
        audioSource.time = 0f;
        audioSource.Play();
    }
    public void moon()
    {
        audioSource.Stop();
        audioSource.clip = moonSE;
        audioSource.time = 0f;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
    public void musik()
    {
        audioSource.Stop();
        audioSource.clip = musikSE;
        audioSource.time = 0f;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
    public void William()
    {
        audioSource.Stop();
        audioSource.clip = WilliamSE;
        audioSource.time = 500.7f;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
    public void fantasy()
    {
        audioSource.Stop();
        audioSource.clip = fantasySE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
    public void minuteWaltz()
    {
        audioSource.Stop();
        audioSource.clip = minuteWaltzSE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
    public void amazing()
    {
        audioSource.Stop();
        audioSource.clip = amazingSE;
        audioSource.time = 0f;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }


    public void reset()
    {
        StartCoroutine("Resetting");
    }

    IEnumerator Resetting()
    {
        yield return new WaitForSeconds(0.5f);
        bgmText = GameObject.Find("BGMText");
        bgmTextPos = bgmText.GetComponent<RectTransform>();
        bgmt = bgmText.GetComponent<Text>();
        switch (musicNumber)
        {
            case 0:
                bgmt.text = "��J�m���i�p�b�w���x���j";
                break;
            case 1:
                bgmt.text = "��{�����i�����F���j";
                break;
            case 2:
                bgmt.text = "��n���K���[���ȑ�T�ԁi�u���[���X�j";
                break;
            case 3:
                bgmt.text = "��f����̃A���A�i�o�b�n�j";
                break;
            case 4:
                bgmt.text = "�􌎌��i�x�[�g�[���F���j";
                break;
            case 5:
                bgmt.text = "��A�C�l�E�N���C�l�E�i�n�g���W�[�N\n�i���[�c�@���g�j";
                break;
            case 6:
                bgmt.text = "��E�B���A���e�����ȁi���b�V�[�j�j";
                break;
            case 7:
                bgmt.text = "�􌶑z�����ȁi�V���p���j";
                break;
            case 8:
                bgmt.text = "��q���̃����c�i�V���p���j";
                break;
            case -1:
                bgmt.text = "��A���C�W���O�O���C�X\n�i�W�����E�j���[�g���j";
                break;
            default:
                break;
        }
    }
}
