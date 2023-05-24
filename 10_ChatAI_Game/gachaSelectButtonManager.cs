using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gachaSelectButtonManager : MonoBehaviour
{
    /// <summary>
    /// ガチャの種類を選択するボタンを押したときに呼ぶメソッドをまとめたコード
    /// </summary>
    public GameObject gameObjectUp;
    public GameObject gameObjectDown;
    public RectTransform rectTransform;
    public RectTransform rectTransformDown;
    float timer;
    int counter;

    public GameObject twitterObj;
    public GameObject youtubeObj;
    public GameObject fiveChObj;
    public Text gachaName;
    public AudioSource audioSource;
    public AudioClip selectSE;

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(4 * Time.time);
        rectTransform.anchoredPosition = new Vector2(-130, 45 + sin * 5);
        rectTransformDown.anchoredPosition = new Vector2(-130, -130 - sin * 5);
    }

    public void OnEnterUp()
    {
        audioSource.PlayOneShot(selectSE);
        rectTransform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }
    public void OnExitUp()
    {
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void OnEnterDown()
    {
        audioSource.PlayOneShot(selectSE);
        rectTransformDown.localScale = new Vector3(1.5f, -1.5f, 1f);
    }
    public void OnExitDown()
    {
        rectTransformDown.localScale = new Vector3(1f, -1f, 1f);
    }
    public void OnClickUp()
    {
        audioSource.PlayOneShot(selectSE);
        //何らかのSE
        //ガチャの種類を変える
        switch (GameManager.instance.gachaNUM)
        {
            case 2://ツイッターからyoutubeへ
                twitterObj.SetActive(true);
                youtubeObj.SetActive(false);
                GameManager.instance.gachaNUM = 1;
                gachaName.text = "Twitter";
                gameObjectUp.SetActive(false);

                GameManager.instance.drawButtonImage.color = new Color(0.12f, 0.64f, 1, 1);
                GameManager.instance.drawResultImage.color = new Color(0.12f, 0.64f, 1, 1);
                break;
            case 3://ツイッターからyoutubeへ
                fiveChObj.SetActive(false);
                youtubeObj.SetActive(true);
                GameManager.instance.gachaNUM = 2;
                gachaName.text = "Youtube";
                gameObjectDown.SetActive(true);

                GameManager.instance.drawButtonImage.color = new Color(1, 0.12f, 0.12f, 1);
                GameManager.instance.drawResultImage.color = new Color(1, 0.12f, 0.12f, 1);
                break;
            default:
                break;
        }
    }
    public void OnClickDown()
    {
        audioSource.PlayOneShot(selectSE);
        //何らかのSE
        //ガチャの種類を変える
        switch (GameManager.instance.gachaNUM)
        {
            case 1://ツイッターからyoutubeへ
                twitterObj.SetActive(false);
                youtubeObj.SetActive(true);
                GameManager.instance.gachaNUM = 2;
                gachaName.text = "Youtube";
                gameObjectUp.SetActive(true);

                GameManager.instance.drawButtonImage.color = new Color(1, 0.12f, 0.12f, 1);
                GameManager.instance.drawResultImage.color = new Color(1, 0.12f, 0.12f, 1);
                break;
            case 2://ツイッターからyoutubeへ
                fiveChObj.SetActive(true);
                youtubeObj.SetActive(false);
                GameManager.instance.gachaNUM = 3;
                gachaName.text = "5ch";
                gameObjectDown.SetActive(false);

                GameManager.instance.drawButtonImage.color = new Color(1, 0.51f, 0.11f, 1);
                GameManager.instance.drawResultImage.color = new Color(1, 0.51f, 0.11f, 1);
                break;
            default:
                break;
        }
    }
}
