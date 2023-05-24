using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageMadeManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���N���A��ʂŁA�쐬�������͂����Ԃ��@�\�𐧌䂷��X�N���v�g
    /// </summary>

    //�\�����镶�͂�ID
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
        //���E�{�^���̐U��
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
                userLetter = "���ȏЉ�āI";
                AILetter = "����" + WordFirst[0] + "�Ƃ������O��" + WordFirst[1] + "�ł��B��b�⎿��ɓ����邱�Ƃ��ł��܂��B";
                break;
            case 2:
                userImage.sprite = GameManager.instance.doctorSprite;
                userLetter = "�D���ȕ��������āI";
                AILetter = "����" + WordSecond[0] + "���D���ł��B�Ȃ��Ȃ�A" + WordSecond[1] + "�ɋ��������邩��ł��B";
                break;
            case 3:
                userImage.sprite = GameManager.instance.syainSprite;
                userLetter = "�x�ތ�������l���āI";
                AILetter = "���͂悤�������܂��B�����ł��B��������" + WordThird[0] + "��" + WordThird[1] + "�Ȃ̂ŁA�o�Ђ������Ԃł��B����Ė{����" + WordThird[2] + "�����Ă��������Ă�낵���ł��傤���B";
                break;
            case 4:
                userImage.sprite = GameManager.instance.gakuseiSprite;
                userLetter = "�����̕��͂��l���āI";
                AILetter = "��������ցB���͑O���灛������̂��Ƃ�" + WordFourth[0] + "�ł����B���R��" + WordFourth[1] + "��" + WordFourth[2] + "��" + WordFourth[3] + "���Ă���l�q�Ɏ䂩�ꂽ����ł��B�����悯��΁A���x" + WordFourth[4] + "��" + WordFourth[5] + "���܂��񂩁H�Ԏ����炦��Ɗ������ł��B�������";
                break;
            case 5:
                userImage.sprite = GameManager.instance.inuSprite;
                userLetter = "������ւ̊��ӂ̎莆�������āI";
                AILetter = "����l�l�ցB���܂�" + WordFifth[0] + "���Ă���Ă��肪�Ƃ��B���̊Ԃ�" + WordFifth[1] + "���Ă��܂��Ă��߂�Ȃ����B����l��" + WordFifth[2] + "�ȏ�����D���ł��B���ꂩ����ǂ�����낵���B" + WordFifth[3] + "���";
                break;
                //�n�J�Z�ւ̍앶
            case 6:
                userImage.sprite = nothing;
                userLetter = "AI���n�J�Z�ɓ`����������";
                AILetter = WordSixth;
                break;
            default:
                break;
        }
        StopCoroutine("CoDrawText");
        StartCoroutine(CoDrawText(userLetter, AILetter));
    }

    // �e�L�X�g���k���k���o�Ă��邽�߂̃R���[�`��
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

            // �N���b�N�����ƈ�C�ɕ\��
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

            // �N���b�N�����ƈ�C�ɕ\��
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
