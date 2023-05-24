using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    /// <summary>
    /// タイトル画面のボタンや操作説明ウィンドウを動かすスクリプト
    /// </summary>
    public Image blackBG;
    public GameObject soundAdjustButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("blackOutOut");
    }

    public void OnClickStartButton()
    {
        StartCoroutine("blackOut");
    }

    public void OnClickSoundButton()
    {
        soundAdjustButton.SetActive(true);
    }

    public void OnCloseSoundButton()
    {
        soundAdjustButton.SetActive(false);
    }

    IEnumerator blackOut()
    {
        GameObject.Find("black").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, i * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
        SceneManager.LoadScene("SampleScene");
    }
        IEnumerator blackOutOut()
        {
            for (int i = 1; i < 31; i++)
            {
                blackBG.color = new Color(0, 0, 0, (30 - i) * 0.04f);
                yield return new WaitForSeconds(0.03f);
            }
        GameObject.Find("black").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 540);
    }
}
