using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    /// <summary>
    /// �}�O���i�v���C���[�j�𓮂����X�N���v�g
    /// </summary>
    public bool isSurvival;//true�Ȃ�T�o�C�o�����[�h�Afalse�Ȃ�Start��Speed���[�h�ɕύX����
    [Header("�ړ����x")] public float moveVelocity;
    public RectTransform rt;
    public bool UpbuttonFlag = false;
    public bool DownbuttonFlag = false;
    public bool LeftbuttonFlag = false;
    public bool RightbuttonFlag = false;
    public int Velocity;
    public int MaxVelocity;
    [Header("�Q�[���}�l�[�W���\�̉���")] public AudioSource gameManagerAudioSource;
    [Header("���x�̃e�L�X�g")] public Text velocityText;
    [Header("���x�̃e�L�X�g2")] public RectTransform velocityObj;
    [Header("���x�̃e�L�X�g�w�i")] public RectTransform velocityObjBG;
    [Header("�摜�R���|�[�l���g")] public Image playerImage;
    private float timer;
    [Header("�_���[�W���󂯂��Ƃ��̌��ʉ�")]public AudioClip damageSE;
    [Header("�{�^���������Ƃ��̌��ʉ�")] public AudioClip buttonSE;
    public int life = 2;
    [Header("�n�[�g�̉摜�P")] public GameObject heartImage1;
    [Header("�n�[�g�̉摜�Q")] public GameObject heartImage2;
    [Header("�n�[�g�̉摜�R")] public GameObject heartImage3;
    [Header("���B�����̃e�L�X�g")] public Text scoreText;
    [Header("�Q�[���I�[�o�[���b�Z�[�W")] public GameObject gameOverMessage;
    [Header("�Q�[���I�[�o�[���b�Z�[�W2")] public GameObject gameOverMessage2;
    [Header("�����L���O�{�^��")] public GameObject rankingButton;
    public float score;
    public bool isGameOver = false;
    public bool isStop = false;
    [Header("�E�{�^�������Ă邩?")] public bool isRight;
    [Header("�Ó]�摜")] public Image BGB;
    [Header("�Ó]�摜2")] public RectTransform BGBrt;
    [Header("���ߗpUI�P")] public Image UImage1;
    [Header("���ߗpUI�Q")] public Image UImage2;
    [Header("���ߗpUI�R")] public Image UImage3;
    [Header("���ߗpUI�S")] public Image UImage4;
    [Header("���ߗpUI�T")] public Image UImage5;
    [Header("���ߗpUI�U")] public Text UImage6;
    [Header("���ߗpUI�V")] public Text UImage7;
    [Header("���ߗpUI�W")] public Text UImage8;
    [Header("���ߗpUI�X")] public Text UImage9;

    [Header("�w�i����rawImage")] public GameObject bGRI;
    [Header("�w�i����rawImage2")] public GameObject bGRI2;

    void Start()
    {
        if (!isSurvival)
        {
            life = 0;
            heartImage1.SetActive(false);
            heartImage2.SetActive(false);
            heartImage3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -245);
            GameObject.Find("scoreText").GetComponent<RectTransform>().anchoredPosition -= new Vector2(25, 0);
        }
        StartCoroutine("startSwim");
    }

    void Update()
    {
        if (!isGameOver && !isStop)
        {
            timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.UpArrow) || UpbuttonFlag)
            {
                Up();
            }
            if (Input.GetKey(KeyCode.DownArrow) || DownbuttonFlag)
            {
                Down();
            }
            if (Input.GetKey(KeyCode.LeftArrow) || LeftbuttonFlag)
            {
                if (timer >= 0.1f)
                {
                    Velocity--;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || RightbuttonFlag)
            {
                isRight = true;
                if (timer >= 0.1f)
                {
                    Velocity++;
                }
            }
            if (!Input.GetKey(KeyCode.RightArrow))
            {
                isRight = false;
            }
            velocityText.text = Velocity + " km/h";

            if (timer >= 0.1f)
            {
                timer = 0;
            }
            if (Velocity <= 0)
            {
                Velocity = 0;
            }

            if (RightbuttonFlag || isRight)
            {
                GetComponent<AudioSource>().volume = 0.3f;
            }
            else
            {
                GetComponent<AudioSource>().volume = 0.0f;
            }

            score += Velocity * Time.deltaTime / 3.6f;
            if (isSurvival)
            {
                scoreText.text = "���B�����F" + Mathf.Round(score) + "m";
            }
            if (!isSurvival && Velocity >= MaxVelocity)
            {
                scoreText.text = "�ō����x�F" + Mathf.Round(Velocity) + "km/h";
                MaxVelocity = Velocity;
            }
        }
        if (rt.anchoredPosition.y >= 195)
        {
            velocityObj.anchoredPosition = new Vector2(-25, -50);
            velocityObjBG.anchoredPosition = new Vector2(-25, -50);
        }
        else
        {
            velocityObj.anchoredPosition = new Vector2(-25, 50);
            velocityObjBG.anchoredPosition = new Vector2(-25, 50);
        }
        if (rt.anchoredPosition.y <= -65)
        {
            UImage1.color = new Color(0, 0, 0, 0.15f);
            UImage2.color = new Color(1, 1, 1, 0.35f);
            UImage3.color = new Color(1, 1, 1, 0.35f);
            UImage4.color = new Color(1, 1, 1, 0.35f);
            UImage5.color = new Color(1, 1, 1, 0.35f);
            UImage6.color = new Color(1, 1, 1, 0.35f);
            UImage7.color = new Color(1, 1, 1, 0.35f);
            UImage8.color = new Color(1, 1, 1, 0.35f);
            UImage9.color = new Color(1, 1, 1, 0.35f);
        }
        else
        {
            UImage1.color = new Color(0, 0, 0, 0.3f);
            UImage2.color = new Color(1, 1, 1, 0.7f);
            UImage3.color = new Color(1, 1, 1, 0.7f);
            UImage4.color = new Color(1, 1, 1, 0.7f);
            UImage5.color = new Color(1, 1, 1, 0.7f);
            UImage6.color = new Color(1, 1, 1, 0.7f);
            UImage7.color = new Color(1, 1, 1, 0.7f);
            UImage8.color = new Color(1, 1, 1, 0.7f);
            UImage9.color = new Color(1, 1, 1, 0.7f);
        }

        float vScale = 3 * Velocity / 100f;
        bGRI.transform.position -= new Vector3(1 * Time.deltaTime * vScale, 0, 0);
        bGRI2.transform.position -= new Vector3(1 * Time.deltaTime * vScale, 0, 0);
        if (bGRI.GetComponent<RectTransform>().anchoredPosition.x <= -1500)
        {
            bGRI.GetComponent<RectTransform>().anchoredPosition = bGRI2.GetComponent<RectTransform>().anchoredPosition + new Vector2(1919f, 0f);
        }
        if (bGRI2.GetComponent<RectTransform>().anchoredPosition.x <= -1500)
        {
            bGRI2.GetComponent<RectTransform>().anchoredPosition = bGRI.GetComponent<RectTransform>().anchoredPosition + new Vector2(1919f, 0f);
        }
        if (bGRI.GetComponent<RectTransform>().anchoredPosition.x >= 1500)
        {
            bGRI.GetComponent<RectTransform>().anchoredPosition = bGRI2.GetComponent<RectTransform>().anchoredPosition - new Vector2(1919f, 0f);
        }
        if (bGRI2.GetComponent<RectTransform>().anchoredPosition.x >= 1500)
        {
            bGRI2.GetComponent<RectTransform>().anchoredPosition = bGRI.GetComponent<RectTransform>().anchoredPosition - new Vector2(1919f, 0f);
        }

        if (Velocity == 0 && !isStop)
        {
            isStop = true;
            if (Velocity <= 0)
            {
                Velocity = 0;
                velocityText.text = Velocity + " km/h";
            }
            StartCoroutine("stopGameOver");
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }

    void FixedUpdate()
    {
        if (!isGameOver && !isStop)
        {
            Quaternion rot = Quaternion.AngleAxis(8f * Mathf.Sin(2 * Time.time), Vector3.forward);
            // ���݂̎��M�̉�]�̏����擾����B
            Quaternion q = transform.rotation;
            // �������āA���g�ɐݒ�
            transform.rotation = rot;
        }        
    }

    public void Up()
    {
        if (rt.anchoredPosition.y <= 250)
        {
            rt.anchoredPosition += new Vector2(0, moveVelocity * Time.deltaTime);
        }
    }
    public void Down()
    {
        if (rt.anchoredPosition.y >= -250)
        {
            rt.anchoredPosition -= new Vector2(0, moveVelocity * Time.deltaTime);
        }
    }
    public void StayClickUpButton()
    {
        UpbuttonFlag = true;
    }
    public void OutClickUpButton()
    {
        if (!Input.GetKey(KeyCode.UpArrow))
        {
            UpbuttonFlag = false;
        }
    }
    public void StayClickDownButton()
    {
        DownbuttonFlag = true;
    }
    public void OutClickDownButton()
    {
        if (!Input.GetKey(KeyCode.DownArrow))
        {
            DownbuttonFlag = false;
        }
    }

    public void StayClickLeftButton()
    {
        LeftbuttonFlag = true;
    }
    public void OutClickLeftButton()
    {
        if (!Input.GetKey(KeyCode.LeftArrow))
        {
            LeftbuttonFlag = false;
        }
    }
    public void StayClickRightButton()
    {
        RightbuttonFlag = true;
    }
    public void OutClickRightButton()
    {
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            RightbuttonFlag = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "enemy" && !isGameOver && !isStop)
        {
            Debug.Log("�_���[�W���󂯂�");
            Velocity -= 100;
            life--;
            switch (life)
            {
                case 1:
                    heartImage1.SetActive(false);
                    break;
                case 0:
                    heartImage2.SetActive(false);
                    break;
                case -1:
                    heartImage3.SetActive(false);
                    isGameOver = true;
                    StartCoroutine("gameOver");
                    GetComponent<AudioSource>().volume = 0.0f;
                    break;
                default:
                    break;
            }
            gameManagerAudioSource.PlayOneShot(damageSE);
            StartCoroutine("knockback");
        }
    }

    IEnumerator knockback()
    {
        for (int i = 0; i < 3; i++)
        {
            playerImage.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.3f);
            playerImage.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void OnclickRankingButton()
    {
        gameManagerAudioSource.PlayOneShot(buttonSE);
        if (isSurvival)
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Mathf.Round(score), 0);
        }
        else
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(MaxVelocity, 1);
        }

    }

    public void OnClickRetryButton()
    {
        gameManagerAudioSource.PlayOneShot(buttonSE);
        StartCoroutine("retry");
    }

    public void OnClickTitleButton()
    {
        gameManagerAudioSource.PlayOneShot(buttonSE);
        StartCoroutine("title");
    }

    public void OnClickTweetButton()
    {
        gameManagerAudioSource.PlayOneShot(buttonSE);
        if (isSurvival)
        {
            naichilab.UnityRoomTweet.Tweet("tunasimulator", "�}�O����" + Mathf.Round(score) + "m�j�����B", "unityroom", "���悰��g������");
        }
        else
        {
            naichilab.UnityRoomTweet.Tweet("tunasimulator", "�}�O���͎���" + Mathf.Round(MaxVelocity) + "km�ŉj�����B", "unityroom", "���悰��g������");
        }
    }

    IEnumerator startSwim()
    {
        BGBrt.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < 100; i++)
        {
            BGB.color -= new Color(0, 0, 0, 0.01f);
            rt.anchoredPosition = new Vector2(-500 + (i * 2), 0);
            yield return new WaitForSeconds(0.01f);
        }
        BGBrt.anchoredPosition = new Vector2(960, 0);
    }

    IEnumerator gameOver()
    {
        gameOverMessage.SetActive(true);
        Text gomText = gameOverMessage.GetComponent<Text>();
        RectTransform gomrt = gameOverMessage.GetComponent<RectTransform>();

        for (int i = 0; i < 100; i++)
        {
            gomText.color += new Color(0, 0, 0, 0.01f);
            gomrt.anchoredPosition = new Vector3(0, -50 + (i / 2f), 0);
            rt.anchoredPosition -= new Vector2(3, 0);
            Quaternion rot = Quaternion.AngleAxis(10f * i, Vector3.forward);
            transform.rotation = rot;
            yield return new WaitForSeconds(0.01f);
        }
        rankingButton.SetActive(true);
    }

    IEnumerator stopGameOver()
    {
        gameOverMessage2.SetActive(true);
        Text gomText = gameOverMessage2.GetComponent<Text>();
        RectTransform gomrt = gameOverMessage2.GetComponent<RectTransform>();

        for (int i = 0; i < 100; i++)
        {
            gomText.color += new Color(0, 0, 0, 0.01f);
            gomrt.anchoredPosition = new Vector3(0, -50 + (i / 2f), 0);
            transform.localScale = new Vector3(1, -1, 1);
            rt.anchoredPosition -= new Vector2(0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        rankingButton.SetActive(true);
    }

    IEnumerator retry()
    {
        BGBrt.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < 100; i++)
        {
            BGB.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        if (isSurvival)
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
        for (int i = 0; i < 100; i++)
        {
            BGB.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("TitleScene");
    }
}