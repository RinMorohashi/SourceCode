using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// シーンの遷移（タイトル、バトル、フィールド、メニュー）を行うスクリプト
    /// </summary>
    public RectTransform fadeRect;
    public Image fadeOutImage;
    public int SceneNum; //１ならタイトル、２ならプロローグ、３ならバトルシーンを表示する。
    [Header("プロローグシーンの背景")] public RectTransform PrologueScene;
    [Header("バトルシーンの背景")]public RectTransform BattleScene;
    [Header("フィールドシーンの背景")] public RectTransform FieldScene;
    [Header("ステージシーンの背景")] public RectTransform StageScene;
    public AudioSource audiosourceBGM;
    public AudioSource audiosourceSE;
    public AudioClip BGM1;//タイトル画面BGM
    public AudioClip BGM2;//バトルBGM
    public AudioClip BGM2Boss;//バトルBGM
    public AudioClip BGM3;//ステージBGM
    public AudioClip BGM3_2;
    public AudioClip BGM3_3;

    public GameObject playerObjStage;//ステージシーンのプレイヤーの位置
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
            case 2://タイトル画面を表示する
                cursorManager.isTytleScene = true;
                PrologueScene.anchoredPosition = new Vector2(0, 0);
                BattleScene.anchoredPosition = new Vector2(2000, 0);
                FieldScene.anchoredPosition = new Vector2(0, -1080);
                StageScene.anchoredPosition = new Vector2(-10000, 0);
                audiosourceBGM.Stop();
                audiosourceBGM.clip = BGM1;
                audiosourceBGM.Play();
                break;
            case 3://バトル画面を表示する
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
            case 4://フィールド画面を表示する

                BattleScene.anchoredPosition = new Vector2(2000, 0);
                FieldScene.anchoredPosition = new Vector2(0, 0);
                StageScene.anchoredPosition = new Vector2(-10000, 0);
                audiosourceBGM.Stop();
                audiosourceBGM.clip = BGM1;
                audiosourceBGM.Play();
                break;
            case 5://ステージ画面を表示する

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
        //音量ボタンをイベントにsetparentして表示
        VolumeButton.SetActive(true);
        isVolumeAdjusting = true;
    }
}
