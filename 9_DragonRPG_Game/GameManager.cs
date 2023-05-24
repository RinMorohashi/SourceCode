using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �V�[���̑J�ځi�^�C�g���A�o�g���A�t�B�[���h�A���j���[�j���s���X�N���v�g
    /// </summary>
    public RectTransform fadeRect;
    public Image fadeOutImage;
    public int SceneNum; //�P�Ȃ�^�C�g���A�Q�Ȃ�v�����[�O�A�R�Ȃ�o�g���V�[����\������B
    [Header("�v�����[�O�V�[���̔w�i")] public RectTransform PrologueScene;
    [Header("�o�g���V�[���̔w�i")]public RectTransform BattleScene;
    [Header("�t�B�[���h�V�[���̔w�i")] public RectTransform FieldScene;
    [Header("�X�e�[�W�V�[���̔w�i")] public RectTransform StageScene;
    public AudioSource audiosourceBGM;
    public AudioSource audiosourceSE;
    public AudioClip BGM1;//�^�C�g�����BGM
    public AudioClip BGM2;//�o�g��BGM
    public AudioClip BGM2Boss;//�o�g��BGM
    public AudioClip BGM3;//�X�e�[�WBGM
    public AudioClip BGM3_2;
    public AudioClip BGM3_3;

    public GameObject playerObjStage;//�X�e�[�W�V�[���̃v���C���[�̈ʒu
    public RectTransform stageParent;
    public FieldManager fieldManager;
    public StageManager stageManager;
    public CursorManager cursorManager;

    public GameObject VolumeButton;
    public bool isVolumeAdjusting;
    public Slider BGMSlider;
    public Slider SESlider;

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isVolumeAdjusting)
        {
            audiosourceBGM.volume = BGMSlider.value;
            audiosourceSE.volume = SESlider.value;
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut()
    {
        fadeRect.anchoredPosition = new Vector2(0, 0);
        fadeRect.localScale = new Vector3(-1, 1, 1);
        for (int i = 1; i < 10; i++)
        {
            switch (i)
            {
                case 1:
                    fadeOutImage.fillAmount = 1 / 20f;
                    break;
                case 2:
                    fadeOutImage.fillAmount = 7.3f / 20f;
                    break;
                case 3:
                    fadeOutImage.fillAmount = 13.7f / 20f;
                    break;
                case 4:
                    fadeOutImage.fillAmount = 16.9f / 20f;
                    break;
                case 5:
                    fadeOutImage.fillAmount = 18.5f / 20f;
                    break;
                case 6:
                    fadeOutImage.fillAmount = 19.3f / 20f;
                    break;
                case 7:
                    fadeOutImage.fillAmount = 19.7f / 20f;
                    break;
                case 8:
                    fadeOutImage.fillAmount = 19.9f / 20f;
                    break;
                case 9:
                    fadeOutImage.fillAmount = 20 / 20f;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        stageManager.changeWalkSE();
        yield return new WaitForSeconds(0.2f);

        switch (SceneNum)
        {
            case 2://�^�C�g����ʂ�\������
                cursorManager.isTytleScene = true;
                PrologueScene.anchoredPosition = new Vector2(0, 0);
                BattleScene.anchoredPosition = new Vector2(2000, 0);
                FieldScene.anchoredPosition = new Vector2(0, -1080);
                StageScene.anchoredPosition = new Vector2(-10000, 0);
                audiosourceBGM.Stop();
                audiosourceBGM.clip = BGM1;
                audiosourceBGM.Play();
                break;
            case 3://�o�g����ʂ�\������
                PrologueScene.anchoredPosition = new Vector2(-960, 0);
                BattleScene.anchoredPosition = new Vector2(0, 0);
                FieldScene.anchoredPosition = new Vector2(0, -1080);
                StageScene.anchoredPosition = new Vector2(-10000, 0);
                audiosourceBGM.Stop();
                if (fieldManager.stageLevelCur == 7 || fieldManager.stageLevelCur == 6 || fieldManager.stageLevelCur == 12 || fieldManager.stageLevelCur == 30)
                {
                    audiosourceBGM.clip = BGM2Boss;
                }
                else
                {
                    audiosourceBGM.clip = BGM2;
                }
                audiosourceBGM.Play();
                break;
            case 4://�t�B�[���h��ʂ�\������

                BattleScene.anchoredPosition = new Vector2(2000, 0);
                FieldScene.anchoredPosition = new Vector2(0, 0);
                StageScene.anchoredPosition = new Vector2(-10000, 0);
                audiosourceBGM.Stop();
                audiosourceBGM.clip = BGM1;
                audiosourceBGM.Play();
                break;
            case 5://�X�e�[�W��ʂ�\������

                BattleScene.anchoredPosition = new Vector2(2000, 0);
                FieldScene.anchoredPosition = new Vector2(0, -1080);
                StageScene.anchoredPosition = new Vector2(0, 0);
                while (playerObjStage.transform.position.x > 1)
                {
                    stageParent.anchoredPosition += new Vector2(-5, 0);
                }
                while (playerObjStage.transform.position.x < -1)
                {
                    stageParent.anchoredPosition -= new Vector2(-5, 0);
                }
                while (playerObjStage.transform.position.y > 1)
                {
                    stageParent.anchoredPosition += new Vector2(0, -5);
                }
                while (playerObjStage.transform.position.y < -1)
                {
                    stageParent.anchoredPosition -= new Vector2(0, -5);
                }
                audiosourceBGM.Stop();
                if (stageManager.stageID == 1)
                {
                    audiosourceBGM.clip = BGM3;
                }
                else if (stageManager.stageID == 2)
                {
                    audiosourceBGM.clip = BGM3_2;
                }
                else if (stageManager.stageID == 3)
                {
                    audiosourceBGM.clip = BGM3_3;
                }
                audiosourceBGM.Play();
                break;
            default:
                break;
        }

        fadeRect.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < 10; i++)
        {
            switch (i)
            {
                case 1:
                    fadeOutImage.fillAmount = 19 / 20f;
                    break;
                case 2:
                    fadeOutImage.fillAmount = 12.7f / 20f;
                    break;
                case 3:
                    fadeOutImage.fillAmount = 6.3f / 20f;
                    break;
                case 4:
                    fadeOutImage.fillAmount = 3.1f / 20f;
                    break;
                case 5:
                    fadeOutImage.fillAmount = 1.5f / 20f;
                    break;
                case 6:
                    fadeOutImage.fillAmount = 0.7f / 20f;
                    break;
                case 7:
                    fadeOutImage.fillAmount = 0.3f / 20f;
                    break;
                case 8:
                    fadeOutImage.fillAmount = 0.1f / 20f;
                    break;
                case 9:
                    fadeOutImage.fillAmount = 0f;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        fadeRect.anchoredPosition = new Vector2(-960, 0);
    }

    public void OnClickVolumeButton()
    {
        //���ʃ{�^�����C�x���g��setparent���ĕ\��
        VolumeButton.SetActive(true);
        isVolumeAdjusting = true;
    }
}
