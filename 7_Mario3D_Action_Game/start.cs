using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    /// <summary>
    /// タイトル画面を動かすコード
    /// </summary>
    private bool interval = true;

    public Text firstRecord;
    public Text secondRecord;
    public Text thirdRecord;

    public Slider BGMSlider;
    public Slider SESlider;
    public Slider SensiSlider;
    private bool isSetting;
    [SerializeField] RectTransform settingButton;
    [SerializeField] RectTransform BGMButton;
    public AudioSource bgm;
    public RectTransform PromoteText;
    private float Timer;
    public GameObject promoteTextObj;

    public bool isChecked;
    public Image mouseButtonImage;
    public Sprite[] checkSprite;

    void Start()
    {
        StartCoroutine("Interval");

        firstRecord.text = "1st  " + PlayerPrefs.GetFloat("SCORE1", 999.99f).ToString("N2");
        secondRecord.text = "2nd  " + PlayerPrefs.GetFloat("SCORE2", 999.99f).ToString("N2");
        thirdRecord.text = "3rd  " + PlayerPrefs.GetFloat("SCORE3", 999.99f).ToString("N2");

        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        SESlider.value = PlayerPrefs.GetFloat("SEVolume", 0.3f);
        SensiSlider.value = PlayerPrefs.GetFloat("SensiVolume", 0.5f);

        if (PlayerPrefs.GetInt("isChecked", 0) == 1)
        {
            isChecked = true;
            mouseButtonImage.sprite = checkSprite[1];
        }
        else
        {
            isChecked = false;
            mouseButtonImage.sprite = checkSprite[0];
        }

        if (PlayerPrefs.GetInt("isClear", 0) == 1)
        {
            promoteTextObj.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !interval)
        {
            PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
            PlayerPrefs.SetFloat("SEVolume", SESlider.value);
            PlayerPrefs.SetFloat("SensiVolume", SensiSlider.value);
            PlayerPrefs.Save();
            SceneManager.LoadScene("SampleScene");
        }

        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.SetFloat("SEVolume", SESlider.value);
        PlayerPrefs.SetFloat("SensiVolume", SensiSlider.value);
        PlayerPrefs.Save();
        bgm.volume = BGMSlider.value;
        Timer += Time.deltaTime * 2;
        if ((int)Timer % 2 == 0)
        {
            PromoteText.anchoredPosition += new Vector2(0, 0.5f);
        }
        else
        {
            PromoteText.anchoredPosition -= new Vector2(0, 0.5f);
        }
    }

    IEnumerator Interval()
    {
        yield return new WaitForSeconds(1.0f);
        interval = false;
    }

    public void OnClickTweetButton()
    {
        naichilab.UnityRoomTweet.Tweet("mario64", "ゲームを" + PlayerPrefs.GetFloat("SCORE1", 999.99f).ToString("N2") + "秒でクリアした！", "unityroom", "スーパー３Dアクションゲーム");
    }

    public void OnClickSettingButton()
    {
        if (isSetting)
        {
            isSetting = false;
            StartCoroutine("SettingShowDown");
        }
        else
        {
            isSetting = true;
            StartCoroutine("SettingShowUp");
        }
    }
    IEnumerator SettingShowUp()
    {
        for (int i = 1; i < 11; i++)
        {
            settingButton.anchoredPosition = new Vector2(440 - 26f * i, 220);
            BGMButton.anchoredPosition = new Vector2(650 - 26f * i, 240);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator SettingShowDown()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
        PlayerPrefs.SetFloat("SEVolume", SESlider.value);
        PlayerPrefs.SetFloat("SensiVolume", SensiSlider.value);
        PlayerPrefs.Save();

        for (int i = 9; i > -1; i--)
        {
            settingButton.anchoredPosition = new Vector2(440 - 26f * i, 220);
            BGMButton.anchoredPosition = new Vector2(650 - 26f * i, 240);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnClickMouseButton()
    {
        if (isChecked)
        {
            isChecked = false;
            mouseButtonImage.sprite = checkSprite[0];
            PlayerPrefs.SetInt("isChecked", 0);
        }
        else
        {
            isChecked = true;
            mouseButtonImage.sprite = checkSprite[1];
            PlayerPrefs.SetInt("isChecked", 1);
        }
        PlayerPrefs.Save();
    }
}
