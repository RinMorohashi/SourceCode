using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    /// <summary>
    /// 配られたトランプを表示するスクリプト
    /// </summary>
    public int Suit = 1; //1＝♠ 2＝♥ 3＝♦ 4=♣
    public int Number = 1;

    public Text suitText;
    public Text NumberText;

    public GameObject activemark;
    public bool activation = false;

    public int cardPosition;

    public GameObject discard;

    public GameObject Mouse1;
    public GameObject Mouse2;
    public GameObject Mouse3;
    public GameObject Mouse4;
    public GameObject Mouse5;

    void Start()
    {
        discard = GameObject.Find("Discard");

        StartCoroutine("Figuriser");

        Mouse1 = GameObject.Find("ExchangeReminder1");
        Mouse2 = GameObject.Find("ExchangeReminder2");
        Mouse3 = GameObject.Find("ExchangeReminder3");
        Mouse4 = GameObject.Find("ExchangeReminder4");
        Mouse5 = GameObject.Find("ExchangeReminder5");
    }

    IEnumerator Figuriser()
    {
        yield return null;

        switch (Suit)
        {
            case 1:
                suitText.text = "♠";
                break;
            case 2:
                suitText.text = "♥";
                break;
            case 3:
                suitText.text = "♦";
                break;
            case 4:
                suitText.text = "♣";
                break;
            default:
                break;
        }

        NumberText.text = "Number";
        switch (Number)
        {
            case 1:
                NumberText.text = "A";
                break;
            case 2:
                NumberText.text = "2";
                break;
            case 3:
                NumberText.text = "3";
                break;
            case 4:
                NumberText.text = "4";
                break;
            case 5:
                NumberText.text = "5";
                break;
            case 6:
                NumberText.text = "6";
                break;
            case 7:
                NumberText.text = "7";
                break;
            case 8:
                NumberText.text = "8";
                break;
            case 9:
                NumberText.text = "9";
                break;
            case 10:
                NumberText.text = "10";
                    break; ;
            case 11:
                NumberText.text = "J";
                break;
            case 12:
                NumberText.text = "Q";
                break;
            case 13:
                NumberText.text = "K";
                break;
            default:
                break;
        }

        if (Suit == 2 || Suit == 3)
        {
            suitText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            NumberText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void Figuriserer()
    {
        StartCoroutine("Figuriser");
    }

        public void OnClickStartButton()
    {
        if (GameManager.CanExchange == true)
        {
            discard.GetComponent<AudioSource>().Play();

            if (activation == false)
            {
                activemark.SetActive(true);
                activation = true;
            }
            else if (activation == true)
            {
                activemark.SetActive(false);
                activation = false;
            }
        }
    }
}
