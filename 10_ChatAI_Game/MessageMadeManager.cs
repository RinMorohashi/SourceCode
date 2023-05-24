using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageMadeManager : MonoBehaviour
{
    /// <summary>
    /// ゲームクリア画面で、作成した文章を見返す機能を制御するスクリプト
    /// </summary>

    //表示する文章のID
    public int messageNum;
    public Text userText;
    public Text AIText;
    public Image userImage;

    public bool playing;
    public string userLetter;
    public string AILetter;

    public string[] WordFirst;
    public string[] WordSecond;
    public string[] WordThird;
    public string[] WordFourth;
    public string[] WordFifth;
    public string WordSixth;

    public float textSpeed;
    public AudioClip speakSE;
    public AudioClip selectSE;
    public AudioClip decideSE;

    public RectTransform rectTransform;
    public RectTransform rectTransformLeft;
    public RectTransform windoeRectTransform;

    public GameObject rightButton;
    public GameObject leftButton;

    public Text pageText;
    public Sprite nothing;

    public GameObject particleObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //左右ボタンの振動
        float sin = Mathf.Sin(4 * Time.time);
        rectTransform.anchoredPosition = new Vector2(350 + sin * 10,0);
        rectTransformLeft.anchoredPosition = new Vector2(-350 - sin * 10,0);
    }

    public void OnEnterRightCursor()
    {
        rightButton.transform.localScale = new Vector3(1.5f,1.5f,1);
        GameManager.instance.audioSource.PlayOneShot(selectSE);
    }
    public void OnEnterLeftCursor()
    {
        leftButton.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        GameManager.instance.audioSource.PlayOneShot(selectSE);
    }
    public void OnExitRightCursor()
    {
        rightButton.transform.localScale = new Vector3(1f, 1f, 1);
    }
    public void OnExitLeftCursor()
    {
        leftButton.transform.localScale = new Vector3(1f, 1f, 1);
    }
    public void OnClickRightCursor()
    {
        GameManager.instance.audioSource.PlayOneShot(decideSE);
        messageNum++;
        if (messageNum == 7)
        {
            messageNum = 1;
        }
        messageChange();
    }
    public void OnClickLeftCursor()
    {
        GameManager.instance.audioSource.PlayOneShot(decideSE);
        messageNum--;
        if (messageNum == 0)
        {
            messageNum = 6;
        }
        messageChange();
    }

    public void messageChange()
    {

        switch (messageNum)
        {
            case 1:
                userImage.sprite = GameManager.instance.doctorSprite;
                userLetter = "自己紹介して！";
                AILetter = "私は" + WordFirst[0] + "という名前の" + WordFirst[1] + "です。会話や質問に答えることができます。";
                break;
            case 2:
                userImage.sprite = GameManager.instance.doctorSprite;
                userLetter = "好きな物を教えて！";
                AILetter = "私は" + WordSecond[0] + "が好きです。なぜなら、" + WordSecond[1] + "に興味があるからです。";
                break;
            case 3:
                userImage.sprite = GameManager.instance.syainSprite;
                userLetter = "休む言い訳を考えて！";
                AILetter = "おはようございます。○○です。今朝から" + WordThird[0] + "が" + WordThird[1] + "なので、出社が難しい状態です。よって本日は" + WordThird[2] + "させていただいてよろしいでしょうか。";
                break;
            case 4:
                userImage.sprite = GameManager.instance.gakuseiSprite;
                userLetter = "告白の文章を考えて！";
                AILetter = "○○さんへ。実は前から○○さんのことが" + WordFourth[0] + "でした。理由は" + WordFourth[1] + "で" + WordFourth[2] + "を" + WordFourth[3] + "している様子に惹かれたからです。もしよければ、今度" + WordFourth[4] + "で" + WordFourth[5] + "しませんか？返事もらえると嬉しいです。■■より";
                break;
            case 5:
                userImage.sprite = GameManager.instance.inuSprite;
                userLetter = "飼い主への感謝の手紙を書いて！";
                AILetter = "ご主人様へ。今まで" + WordFifth[0] + "してくれてありがとう。この間は" + WordFifth[1] + "してしまってごめんなさい。ご主人の" + WordFifth[2] + "な所が大好きです。これからもどうぞよろしく。" + WordFifth[3] + "より";
                break;
                //ハカセへの作文
            case 6:
                userImage.sprite = nothing;
                userLetter = "AIがハカセに伝えたいこと";
                AILetter = WordSixth;
                break;
            default:
                break;
        }
        StopCoroutine("CoDrawText");
        StartCoroutine(CoDrawText(userLetter, AILetter));
    }

    // テキストがヌルヌル出てくるためのコルーチン
    IEnumerator CoDrawText(string text,string textTwo)
    {
        userText.text = "";
        AIText.text = "";
        pageText.text = messageNum + " / 6";

        playing = true;
        float time = 0;
        int lenPrev = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > lenPrev)
            {
                GameManager.instance.audioSource.PlayOneShot(speakSE);
            }
            lenPrev = len;
            if (len > text.Length) break;
            userText.text = text.Substring(0, len);
        }
        userText.text = text;

        yield return new WaitForSeconds(0.5f);

        float timeTwo = 0;
        int lenPrevTwo = 0;
        while (true)
        {
            yield return 0;
            timeTwo += Time.deltaTime;

            // クリックされると一気に表示
            if (IsClicked()) break;

            int lenTwo = Mathf.FloorToInt(timeTwo / textSpeed);
            if (lenTwo > lenPrevTwo)
            {
                GameManager.instance.audioSource.PlayOneShot(speakSE);
            }
            lenPrevTwo = lenTwo;
            if (lenTwo > textTwo.Length) break;
            AIText.text = textTwo.Substring(0, lenTwo);
        }
        AIText.text = textTwo;
        playing = false;
    }
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }
    public void OnClickOpenWindow()
    {
        particleObj.SetActive(false);
       messageNum = 1;
        messageChange();
        windoeRectTransform.DOLocalMove(new Vector3(0, 0, 0), 0.5f)
                    .OnComplete(() => 
                    {

                    });
    }
    public void OnClickCloseWindow()
    {
        particleObj.SetActive(true);
        windoeRectTransform.DOLocalMove(new Vector3(0, -960, 0), 0.5f)
                    .OnComplete(() =>
                    {

                    });
    }
}
