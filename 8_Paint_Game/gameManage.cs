using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class gameManage : MonoBehaviour
{
    /// <summary>
    /// 筆の色の変更や、塗りつぶしを行うスクリプト
    /// </summary>
    public Color32 CurrentColor;
    public RectTransform zundaLocation;
    public Image hair;
    public Image eye;
    public Image mouse;
    public Image skin;
    public Image ClothMain;
    public Image ClothSub;
    public Image ClothTwo;

    public RectTransform CursorRec;
    public AudioSource audioSource;
    public AudioClip selectSE;
    public AudioClip paintSE1;
    public AudioClip paintSE2;
    public AudioClip paintSE3;
    public AudioClip paintSE4;
    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;

    public GameObject StartButton;
    public GameObject CompleteButton;
    public GameObject TweetButton;
    public GameObject RetryButton;
    public GameObject Color1;
    public GameObject Color2;
    public GameObject Color3;
    public GameObject Color4;
    public GameObject Color5;
    public GameObject Color6;
    public bool isStarted;
    public bool isCompleted;
    public GameObject ZundaBasis;
    public GameObject body;
    public GameObject bodyZwei;
    public GameObject bodyDrei;
    public GameObject bodyFier;
    public GameObject hairObj;
    public GameObject hairObjFier;
    public GameObject eyeObj;
    public GameObject eyeObjFier;
    public GameObject mouseObj;
    public GameObject mouseObjFier;
    public GameObject skinObj;
    public GameObject skinObjZwei;
    public GameObject skinObjDrei;
    public GameObject skinObjFier;
    public GameObject clothMainObj;
    public GameObject clothSubObj;
    public GameObject clothTwoObj;
    public GameObject clothTwoObjZwei;
    public GameObject clothTwoObjDrei;
    public GameObject clothTwoObjFier;
    public float scaleC;

    public GameObject pauseButton1;
    public GameObject pauseButton2;
    public GameObject pauseButton3;
    public GameObject pauseButton4;
    public GameObject TitleImage;

    public bool mouseUp1;
    public bool mouseUp2;
    public bool mouseDown1;
    public bool mouseDown2;

    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    
    void FixedUpdate()
    {
        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        Debug.Log(screenToWorldPointPosition);
        if (screenToWorldPointPosition.x >= -4 && screenToWorldPointPosition.x <= 4 /*Input.mousePosition.x >= 275 && Input.mousePosition.x <= 750*/ && !isCompleted && isStarted)
        {
            if (screenToWorldPointPosition.y <= -4 && zundaLocation.anchoredPosition.y <= 555 /*Input.mousePosition.y <= 50 && zundaLocation.anchoredPosition.y <= 555*/)
            {
                zundaLocation.anchoredPosition += new Vector2(0, 10f);
            }
            else if (screenToWorldPointPosition.y <= -3 && zundaLocation.anchoredPosition.y <= 555 /*Input.mousePosition.y <= 150 && zundaLocation.anchoredPosition.y <= 555*/)
            {
                zundaLocation.anchoredPosition += new Vector2(0, 5f);
            }
            if (screenToWorldPointPosition.y >= 4 && zundaLocation.anchoredPosition.y >= -655 /*Input.mousePosition.y >= 520 && zundaLocation.anchoredPosition.y >= -655*/)
            {
                zundaLocation.anchoredPosition += new Vector2(0, -10f);
            }
            else if (screenToWorldPointPosition.y >= 3 && zundaLocation.anchoredPosition.y >= -655 /*Input.mousePosition.y >= 360 && zundaLocation.anchoredPosition.y >= -655*/)
            {
                zundaLocation.anchoredPosition += new Vector2(0, -5f);
            }
        }
    }

    public void OnEnterUp1()
    {
        mouseUp1 = true;
    }
    public void OnEnterUp2()
    {
        mouseUp2 = true;
    }
    public void OnEnterDown1()
    {
        mouseDown1 = true;
    }
    public void OnEnterDown2()
    {
        mouseDown2 = true;
    }
    public void OnExitUp1()
    {
        mouseUp1 = false;
    }
    public void OnExitUp2()
    {
        mouseUp2 = false;
    }
    public void OnExitDown1()
    {
        mouseDown1 = false;
    }
    public void OnExitDown2()
    {
        mouseDown2 = false;
    }

    public void OnClickStartButton()
    {
        StartButton.transform.DOLocalMove(new Vector3(570, 0, 0), 0.5f);
        CompleteButton.transform.DOLocalMove(new Vector3(370, 0, 0), 0.5f);
        Color1.transform.DOLocalMove(new Vector3(-400, 255, 0), 0.5f);
        Color2.transform.DOLocalMove(new Vector3(-400, 125, 0), 0.5f);
        Color3.transform.DOLocalMove(new Vector3(-400, -5, 0), 0.5f);
        Color4.transform.DOLocalMove(new Vector3(-400, -135, 0), 0.5f);
        Color5.transform.DOLocalMove(new Vector3(-285, 255, 0), 0.5f);
        Color6.transform.DOLocalMove(new Vector3(-285, 125, 0), 0.5f);
        TitleImage.transform.DOLocalMove(new Vector3(0, 555, 0), 0.5f);
        OnClickColorPalette11();
        isStarted = true;
}

    public void OnClickCompleteButton()
    {
        isCompleted = true;
        CompleteButton.transform.DOLocalMove(new Vector3(570, 0, 0), 0.5f);
        TweetButton.transform.DOLocalMove(new Vector3(370, 0, 0), 0.5f);
        RetryButton.transform.DOLocalMove(new Vector3(370, -80, 0), 0.5f);
        pauseButton1.transform.DOLocalMove(new Vector3(-375, 100, 0), 0.5f);
        pauseButton2.transform.DOLocalMove(new Vector3(-375, 30, 0), 0.5f);
        pauseButton3.transform.DOLocalMove(new Vector3(-375, -40, 0), 0.5f);
        pauseButton4.transform.DOLocalMove(new Vector3(-375, -110, 0), 0.5f);
        Color1.transform.DOLocalMove(new Vector3(-550, 255, 0), 0.5f);
        Color2.transform.DOLocalMove(new Vector3(-550, 125, 0), 0.5f);
        Color3.transform.DOLocalMove(new Vector3(-550, -5, 0), 0.5f);
        Color4.transform.DOLocalMove(new Vector3(-550, -135, 0), 0.5f);
        Color5.transform.DOLocalMove(new Vector3(-550, 255, 0), 0.5f);
        Color6.transform.DOLocalMove(new Vector3(-550, 125, 0), 0.5f);
        CursorRec.anchoredPosition = new Vector2(1000,0);

        ZundaBasis.transform.DOScale(new Vector3(scaleC, scaleC, 1), 0.5f);
        ZundaBasis.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);

        audioSource.PlayOneShot(voice1);
    }

    public void OnClickTweetButton()
    {
        StartCoroutine(TweetWithScreenShot.TweetManager.TweetWithScreenShot("ずんだもんぬりえ"));
    }

    public void OnClickRetryButton()
    {
        body.SetActive(true);
        hairObj.SetActive(true);
        eyeObj.SetActive(true);
        mouseObj.SetActive(true);
        skinObj.SetActive(true);
        clothMainObj.SetActive(true);
        clothSubObj.SetActive(true);
        clothTwoObj.SetActive(true);

        bodyZwei.SetActive(false);
        bodyDrei.SetActive(false);
        skinObjZwei.SetActive(false);
        skinObjDrei.SetActive(false);
        clothTwoObjZwei.SetActive(false);
        clothTwoObjDrei.SetActive(false);
        bodyFier.SetActive(false);
        hairObjFier.SetActive(false);
        eyeObjFier.SetActive(false);
        mouseObjFier.SetActive(false);
        skinObjFier.SetActive(false);
        clothTwoObjFier.SetActive(false);

        CompleteButton.transform.DOLocalMove(new Vector3(370, 0, 0), 0.5f);
        TweetButton.transform.DOLocalMove(new Vector3(570, 0, 0), 0.5f);
        RetryButton.transform.DOLocalMove(new Vector3(570, -80, 0), 0.5f);
        pauseButton1.transform.DOLocalMove(new Vector3(-600, 100, 0), 0.5f);
        pauseButton2.transform.DOLocalMove(new Vector3(-600, 30, 0), 0.5f);
        pauseButton3.transform.DOLocalMove(new Vector3(-600, -40, 0), 0.5f);
        pauseButton4.transform.DOLocalMove(new Vector3(-600, -110, 0), 0.5f);
        Color1.transform.DOLocalMove(new Vector3(-400, 255, 0), 0.5f);
        Color2.transform.DOLocalMove(new Vector3(-400, 125, 0), 0.5f);
        Color3.transform.DOLocalMove(new Vector3(-400, -5, 0), 0.5f);
        Color4.transform.DOLocalMove(new Vector3(-400, -135, 0), 0.5f);
        Color5.transform.DOLocalMove(new Vector3(-285, 255, 0), 0.5f);
        Color6.transform.DOLocalMove(new Vector3(-285, 125, 0), 0.5f);

        ZundaBasis.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        audioSource.PlayOneShot(voice1);
        isCompleted = false;
    }

    public void OnClickPause1Button()
    {
        body.SetActive(true);
        hairObj.SetActive(true);
        eyeObj.SetActive(true);
        mouseObj.SetActive(true);
        skinObj.SetActive(true);
        clothMainObj.SetActive(true);
        clothSubObj.SetActive(true);
        clothTwoObj.SetActive(true);
        bodyZwei.SetActive(false) ;
        skinObjZwei.SetActive(false);
        clothTwoObjZwei.SetActive(false);
        bodyDrei.SetActive(false);
        skinObjDrei.SetActive(false);
        clothTwoObjDrei.SetActive(false);
        bodyFier.SetActive(false);
        hairObjFier.SetActive(false);
        eyeObjFier.SetActive(false);
        mouseObjFier.SetActive(false);
        skinObjFier.SetActive(false);
        clothTwoObjFier.SetActive(false);

        audioSource.PlayOneShot(voice1);
    }

    public void OnClickPause2Button()
    {
        bodyZwei.GetComponent<Image>().color = body.GetComponent<Image>().color;
        var c1 = skinObj.GetComponent<Image>().color;
        Debug.Log(c1);
        skinObjZwei.GetComponent<Image>().color = c1;
        Debug.Log(skinObjZwei.GetComponent<Image>().color);
        var c2 = clothTwoObj.GetComponent<Image>().color;
        clothTwoObjZwei.GetComponent<Image>().color = c2;

        body.SetActive(false);
        hairObj.SetActive(true);
        mouseObj.SetActive(true);
        skinObj.SetActive(false);
        clothMainObj.SetActive(true);
        clothSubObj.SetActive(true);
        clothTwoObj.SetActive(false);
        eyeObj.SetActive(false);
        bodyZwei.SetActive(true);
        skinObjZwei.SetActive(true);
        clothTwoObjZwei.SetActive(true);
        bodyDrei.SetActive(false);
        skinObjDrei.SetActive(false);
        clothTwoObjDrei.SetActive(false);
        bodyFier.SetActive(false);
        hairObjFier.SetActive(false);
        eyeObjFier.SetActive(false);
        mouseObjFier.SetActive(false);
        skinObjFier.SetActive(false);
        clothTwoObjFier.SetActive(false);

        audioSource.PlayOneShot(voice2);
    }

    public void OnClickPause3Button()
    {
        bodyDrei.GetComponent<Image>().color = body.GetComponent<Image>().color;
        var c1 = skinObj.GetComponent<Image>().color;
        skinObjDrei.GetComponent<Image>().color = c1;
        var c2 = clothTwoObj.GetComponent<Image>().color;
        clothTwoObjDrei.GetComponent<Image>().color = c2;

        body.SetActive(false);
        hairObj.SetActive(true);
        mouseObj.SetActive(true);
        skinObj.SetActive(false);
        clothMainObj.SetActive(true);
        clothSubObj.SetActive(true);
        clothTwoObj.SetActive(false);
        eyeObj.SetActive(true);
        bodyZwei.SetActive(false);
        skinObjZwei.SetActive(false);
        clothTwoObjZwei.SetActive(false);
        bodyDrei.SetActive(true);
        skinObjDrei.SetActive(true);
        clothTwoObjDrei.SetActive(true);
        bodyFier.SetActive(false);
        hairObjFier.SetActive(false);
        eyeObjFier.SetActive(false);
        mouseObjFier.SetActive(false);
        skinObjFier.SetActive(false);
        clothTwoObjFier.SetActive(false);

        audioSource.PlayOneShot(voice4);
    }

    public void OnClickPause4Button()
    {
        bodyFier.GetComponent<Image>().color = body.GetComponent<Image>().color;
        hairObjFier.GetComponent<Image>().color = hairObj.GetComponent<Image>().color;
        eyeObjFier.GetComponent<Image>().color = eyeObj.GetComponent<Image>().color;
        mouseObjFier.GetComponent<Image>().color = mouseObj.GetComponent<Image>().color;
        var c1 = skinObj.GetComponent<Image>().color;
        skinObjFier.GetComponent<Image>().color = c1;
        var c2 = clothTwoObj.GetComponent<Image>().color;
        clothTwoObjFier.GetComponent<Image>().color = c2;

        body.SetActive(false);
        hairObj.SetActive(false);
        mouseObj.SetActive(false);
        skinObj.SetActive(false);
        clothMainObj.SetActive(true);
        clothSubObj.SetActive(true);
        clothTwoObj.SetActive(false);
        eyeObj.SetActive(false);
        bodyZwei.SetActive(false);
        skinObjZwei.SetActive(false);
        clothTwoObjZwei.SetActive(false);
        bodyDrei.SetActive(false);
        skinObjDrei.SetActive(false);
        clothTwoObjDrei.SetActive(false);
        bodyFier.SetActive(true);
        hairObjFier.SetActive(true);
        eyeObjFier.SetActive(true);
        mouseObjFier.SetActive(true);
        skinObjFier.SetActive(true);
        clothTwoObjFier.SetActive(true);

        audioSource.PlayOneShot(voice3);
    }

    public void PaintingSound()
    {
        int rnd = Random.Range(1, 5);
        switch (rnd)
        {
            case 1:
                audioSource.PlayOneShot(paintSE1);
                break;
            case 2:
                audioSource.PlayOneShot(paintSE2);
                break;
            case 3:
                audioSource.PlayOneShot(paintSE3);
                break;
            case 4:
                audioSource.PlayOneShot(paintSE4);
                break;
            default:
                break;
        }
    }
    
    public void OnClickHair()
    {
        if (!isCompleted && isStarted)
        {
            hair.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickEye()
    {
        if (!isCompleted && isStarted)
        {
            eye.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickMouse()
    {
        if (!isCompleted && isStarted)
        {
            mouse.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickSkin()
    {
        if (!isCompleted && isStarted)
        {
            skin.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickClothMain()
    {
        if (!isCompleted && isStarted)
        {
            ClothMain.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickClothSub()
    {
        if (!isCompleted && isStarted)
        {
            ClothSub.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickClothTwo()
    {
        if (!isCompleted && isStarted)
        {
            ClothTwo.color = CurrentColor;
            PaintingSound();
        }
    }
    public void OnClickColorPalette11()
    {
        CurrentColor = new Color32(240,252,173, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette12()
    {
        CurrentColor = new Color32(192, 202, 138, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette13()
    {
        CurrentColor = new Color32(154, 162, 110, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette21()
    {
        CurrentColor = new Color32(192,252,138, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette22()
    {
        CurrentColor = new Color32(154,202,110, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette23()
    {
        CurrentColor = new Color32(123,162,88, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette31()
    {
        CurrentColor = new Color32(154,252,110, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 155);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette32()
    {
        CurrentColor = new Color32(123,202,88, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 155);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette33()
    {
        CurrentColor = new Color32(98,162,70, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 155);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette41()
    {
        CurrentColor = new Color32(247,230,116, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette42()
    {
        CurrentColor = new Color32(198,184,93, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette43()
    {
        CurrentColor = new Color32(158,147,74, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette51()
    {
        CurrentColor = new Color32(247, 184, 93, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 60);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette52()
    {
        CurrentColor = new Color32(198, 147, 74, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 60);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette53()
    {
        CurrentColor = new Color32(158, 118, 59, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 60);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette61()
    {
        CurrentColor = new Color32(247, 147,74, 255);
        CursorRec.anchoredPosition = new Vector2(-460, 25);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette62()
    {
        CurrentColor = new Color32(198, 118,59, 255);
        CursorRec.anchoredPosition = new Vector2(-425, 25);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette63()
    {
        CurrentColor = new Color32(158, 94,47, 255);
        CursorRec.anchoredPosition = new Vector2(-390, 25);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette71()
    {
        CurrentColor = new Color32(209,126,127, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -35);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette72()
    {
        CurrentColor = new Color32(167,101,102, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -35);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette73()
    {
        CurrentColor = new Color32(134,81,82, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -35);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette81()
    {
        CurrentColor = new Color32(209, 101,102, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -70);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette82()
    {
        CurrentColor = new Color32(167, 81,82, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -70);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette83()
    {
        CurrentColor = new Color32(134, 65,66, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -70);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette91()
    {
        CurrentColor = new Color32(209, 81,82, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -105);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette92()
    {
        CurrentColor = new Color32(167, 65,66, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -105);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette93()
    {
        CurrentColor = new Color32(134, 52,53, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -105);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette101()
    {
        CurrentColor = new Color32(138,192,252, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -165);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette102()
    {
        CurrentColor = new Color32(110,154,202, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -165);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette103()
    {
        CurrentColor = new Color32(88,123,162, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -165);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette111()
    {
        CurrentColor = new Color32(110,154, 252, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -200);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette112()
    {
        CurrentColor = new Color32(88,123, 202, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -200);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette113()
    {
        CurrentColor = new Color32(70,98, 162, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -200);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette121()
    {
        CurrentColor = new Color32(88,23, 252, 255);
        CursorRec.anchoredPosition = new Vector2(-460, -235);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette122()
    {
        CurrentColor = new Color32(70,98, 202, 255);
        CursorRec.anchoredPosition = new Vector2(-425, -235);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette123()
    {
        CurrentColor = new Color32(56,78, 162, 255);
        CursorRec.anchoredPosition = new Vector2(-390, -235);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette131()
    {
        CurrentColor = new Color32(255,255,255, 255);
        CursorRec.anchoredPosition = new Vector2(-345, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette132()
    {
        CurrentColor = new Color32(204,204,204, 255);
        CursorRec.anchoredPosition = new Vector2(-310, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette133()
    {
        CurrentColor = new Color32(163,163,163, 255);
        CursorRec.anchoredPosition = new Vector2(-275, 95);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette141()
    {
        CurrentColor = new Color32(252,251,227, 255);
        CursorRec.anchoredPosition = new Vector2(-345, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette142()
    {
        CurrentColor = new Color32(202,201,182, 255);
        CursorRec.anchoredPosition = new Vector2(-310, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette143()
    {
        CurrentColor = new Color32(162,161,146, 255);
        CursorRec.anchoredPosition = new Vector2(-275, 225);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette151()
    {
        CurrentColor = new Color32(252, 238, 216, 255);
        CursorRec.anchoredPosition = new Vector2(-345, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette152()
    {
        CurrentColor = new Color32(202, 190, 173, 255);
        CursorRec.anchoredPosition = new Vector2(-310, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette153()
    {
        CurrentColor = new Color32(162, 152, 138, 255);
        CursorRec.anchoredPosition = new Vector2(-275, 190);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette161()
    {
        CurrentColor = new Color32(252, 190, 173, 255);
        CursorRec.anchoredPosition = new Vector2(-345, 155);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette162()
    {
        CurrentColor = new Color32(202, 152, 138, 255);
        CursorRec.anchoredPosition = new Vector2(-310, 155);
        audioSource.PlayOneShot(selectSE);
    }

    public void OnClickColorPalette163()
    {
        CurrentColor = new Color32(162, 122, 110, 255);
        CursorRec.anchoredPosition = new Vector2(-275, 155);
        audioSource.PlayOneShot(selectSE);
    }
}
