using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    /// <summary>
    /// タイトル画面を動かすスクリプト
    /// </summary>
    [Header("背景画像")] public GameObject BGImage;
    [Header("暗転画像")] public Image BGB;
    [Header("暗転画像2")] public RectTransform BGBrt;
    [Header("ゲームモードセレクト")] public GameObject GameModeSelect;
    [Header("マニュアル")] public GameObject Manual;
    public AudioSource tsas;
    [Header("ボタン押したときの効果音")] public AudioClip buttonSE;
    private RectTransform BGrt;
    private float timer;
    private bool isSurvival = true;

    void Start()
    {
        BGrt = BGImage.GetComponent<RectTransform>();
        StartCoroutine("title");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.1f)
        {
            BGrt.anchoredPosition = new Vector3(0, 50 * Mathf.Sin(0.3f * Time.time), 0);
            timer = 0;
        }
    }

    public void OnClickStartButton()
    {
        tsas.PlayOneShot(buttonSE);
        GameModeSelect.SetActive(true);
    }

    public void OnClickSurvivalButton()
    {
        isSurvival = true;
        tsas.PlayOneShot(buttonSE);
        StartCoroutine("startGame");
    }

    public void OnClickSpeedButton()
    {
        isSurvival = false;
        tsas.PlayOneShot(buttonSE);
        StartCoroutine("startGame");
    }

    public void OnClickManualButton()
    {
        tsas.PlayOneShot(buttonSE);
        Manual.SetActive(true);
    }
    public void OnClickCloseButton()
    {
        tsas.PlayOneShot(buttonSE);
        Manual.SetActive(false);
    }

    IEnumerator startGame()
    {
        BGBrt.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < 100; i++)
        {
            BGB.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        if (isSurvival == true)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("GameScene2");
        }
    }

    IEnumerator title()
    {
        BGBrt.anchoredPosition = new Vector2(0, 0);
        BGB.color = new Color(0, 0, 0, 1f);
        for (int i = 0; i < 100; i++)
        {
            BGB.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        BGBrt.anchoredPosition = new Vector2(960, 0);
    }
}
