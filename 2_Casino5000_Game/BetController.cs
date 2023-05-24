using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    /// <summary>
    /// ベット額によってベットしたコインの画像を更新するスクリプト
    /// </summary>
    public GameObject Manager;
    GameManager gameManager;
    public long betMoney = 100;
    public static int betMoneyRank = 1;
    public Text betFigure;
    public GameObject choose;
    public Text betComma;


    public GameObject coins1;
    public GameObject coins2;
    public GameObject coins3;
    public GameObject coins4;
    public GameObject coins5;
    public GameObject coins6;
    public GameObject coins7;
    public GameObject coins8;
    public GameObject coins9;
    public GameObject coins10;
    public GameObject coins11;
    public GameObject coins12;
    public GameObject coins13;
    public GameObject coins14;
    public GameObject coins15;
    public GameObject coins16;
    public GameObject coins17;
    public GameObject coins18;
    public GameObject coins19;
    public GameObject coins20;
    public GameObject coins21;
    public GameObject coins22;

    public GameObject SE2;
    public GameObject cancel;

    void Start()
    {
         gameManager = Manager.GetComponent<GameManager>();

        switch (betMoneyRank)
        {
            case 1:
                betMoney = 100;
                coins1.SetActive(true);
                coins2.SetActive(false);
                break;
            case 2:
                betMoney = 200;
                coins1.SetActive(false);
                coins2.SetActive(true);
                coins3.SetActive(false);
                break;
            case 3:
                betMoney = 400;
                coins2.SetActive(false);
                coins3.SetActive(true);
                coins4.SetActive(false);
                break;
            case 4:
                betMoney = 800;
                coins3.SetActive(false);
                coins4.SetActive(true);
                coins5.SetActive(false);
                break;
            case 5:
                betMoney = 1600;
                coins4.SetActive(false);
                coins5.SetActive(true);
                coins6.SetActive(false);
                break;
            case 6:
                betMoney = 3200;
                coins5.SetActive(false);
                coins6.SetActive(true);
                coins7.SetActive(false);
                break;
            case 7:
                betMoney = 6400;
                coins6.SetActive(false);
                coins7.SetActive(true);
                coins8.SetActive(false);
                break;
            case 8:
                betMoney = 12800;
                coins7.SetActive(false);
                coins8.SetActive(true);
                coins9.SetActive(false);
                break;
            case 9:
                betMoney = 25600;
                coins8.SetActive(false);
                coins9.SetActive(true);
                coins10.SetActive(false);
                break;
            case 10:
                betMoney = 51200;
                coins9.SetActive(false);
                coins10.SetActive(true);
                coins11.SetActive(false);
                break;
            case 11:
                betMoney = 102400;
                coins10.SetActive(false);
                coins11.SetActive(true);
                coins12.SetActive(false);
                break;
            case 12:
                betMoney = 204800;
                coins11.SetActive(false);
                coins12.SetActive(true);
                coins13.SetActive(false);
                break;
            case 13:
                betMoney = 409600;
                coins12.SetActive(false);
                coins13.SetActive(true);
                coins14.SetActive(false);
                break;
            case 14:
                betMoney = 819200;
                coins13.SetActive(false);
                coins14.SetActive(true);
                coins15.SetActive(false);
                break;
            case 15:
                betMoney = 1638400;
                coins14.SetActive(false);
                coins15.SetActive(true);
                coins16.SetActive(false);
                break;
            case 16:
                betMoney = 3276800;
                coins15.SetActive(false);
                coins16.SetActive(true);
                coins17.SetActive(false);
                break;
            case 17:
                betMoney = 6553600;
                coins16.SetActive(false);
                coins17.SetActive(true);
                coins18.SetActive(false);
                break;
            case 18:
                betMoney = 13107200;
                coins17.SetActive(false);
                coins18.SetActive(true);
                coins19.SetActive(false);
                break;
            case 19:
                betMoney = 26214400;
                coins18.SetActive(false);
                coins19.SetActive(true);
                coins20.SetActive(false);
                break;
            case 20:
                betMoney = 52428800;
                coins19.SetActive(false);
                coins20.SetActive(true);
                coins21.SetActive(false);
                break;
            case 21:
                betMoney = 104857600;
                coins20.SetActive(false);
                coins21.SetActive(true);
                coins22.SetActive(false);
                break;
            case 22:
                betMoney = 1000000000000;
                coins21.SetActive(false);
                coins22.SetActive(true);
                break;
            default:
                break;
        }

        betFigure.text = "" + betMoney;

        if (betMoney >= 1000000000000)
        {
            betComma.text = ",       ,      ,      ,         ";
        }
        else if (betMoney >= 1000000000)
        {
            betComma.text = ",      ,      ,         ";
        }
        else if (betMoney >= 1000000)
        {
            betComma.text = ",      ,         ";
        }
        else if (betMoney >= 1000)
        {
            betComma.text = ",         ";
        }
        else
        {
            betComma.text = "";
        }
    }
    public void OnClickUpButton()
    {
        if (EditManager.skillSwitch12 == true)
        {
            if (betMoneyRank < 22)
            {
                betMoneyRank += 1;
                SE2.GetComponent<AudioSource>().Play();
            }
            else
            {
                cancel.GetComponent<AudioSource>().Play();
            }
        }
        else if (EditManager.skillSwitch8 == true)
        {
            if (betMoneyRank < 21)
            {
                betMoneyRank += 1;
                SE2.GetComponent<AudioSource>().Play();
            }
            else
            {
                cancel.GetComponent<AudioSource>().Play();
            }
        }
        else if (EditManager.skillSwitch4 == true)
        {
            if (betMoneyRank < 18)
            {
                betMoneyRank += 1;
                SE2.GetComponent<AudioSource>().Play();
            }
            else
            {
                cancel.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (betMoneyRank < 15)
            {
                betMoneyRank += 1;
                SE2.GetComponent<AudioSource>().Play();
            }
            else
            {
                cancel.GetComponent<AudioSource>().Play();
            }
        }

            switch (betMoneyRank)
            {
                case 1:
                    betMoney = 100;
                coins1.SetActive(true);
                coins2.SetActive(false);
                break;
                case 2:
                    betMoney = 200;
                coins1.SetActive(false);
                coins2.SetActive(true);
                coins3.SetActive(false);
                break;
                case 3:
                    betMoney = 400;
                coins2.SetActive(false);
                coins3.SetActive(true);
                coins4.SetActive(false);
                break;
                case 4:
                    betMoney = 800;
                coins3.SetActive(false);
                coins4.SetActive(true);
                coins5.SetActive(false);
                break;
                case 5:
                    betMoney = 1600;
                coins4.SetActive(false);
                coins5.SetActive(true);
                coins6.SetActive(false);
                break;
                case 6:
                    betMoney = 3200;
                coins5.SetActive(false);
                coins6.SetActive(true);
                coins7.SetActive(false);
                break;
                case 7:
                    betMoney = 6400;
                coins6.SetActive(false);
                coins7.SetActive(true);
                coins8.SetActive(false);
                break;
                case 8:
                    betMoney = 12800;
                coins7.SetActive(false);
                coins8.SetActive(true);
                coins9.SetActive(false);
                break;
                case 9:
                    betMoney = 25600;
                coins8.SetActive(false);
                coins9.SetActive(true);
                coins10.SetActive(false);
                break;
                case 10:
                    betMoney = 51200;
                coins9.SetActive(false);
                coins10.SetActive(true);
                coins11.SetActive(false);
                break;
                case 11:
                    betMoney = 102400;
                coins10.SetActive(false);
                coins11.SetActive(true);
                coins12.SetActive(false);
                break;
                case 12:
                    betMoney = 204800;
                coins11.SetActive(false);
                coins12.SetActive(true);
                coins13.SetActive(false);
                break;
                case 13:
                    betMoney = 409600;
                coins12.SetActive(false);
                coins13.SetActive(true);
                coins14.SetActive(false);
                break;
                case 14:
                    betMoney = 819200;
                coins13.SetActive(false);
                coins14.SetActive(true);
                coins15.SetActive(false);
                break;
                case 15:
                    betMoney = 1638400;
                coins14.SetActive(false);
                coins15.SetActive(true);
                coins16.SetActive(false);
                break;
            case 16:
                betMoney = 3276800;
                coins15.SetActive(false);
                coins16.SetActive(true);
                coins17.SetActive(false);
                break;
            case 17:
                betMoney = 6553600;
                coins16.SetActive(false);
                coins17.SetActive(true);
                coins18.SetActive(false);
                break;
            case 18:
                betMoney = 13107200;
                coins17.SetActive(false);
                coins18.SetActive(true);
                coins19.SetActive(false);
                break;
            case 19:
                betMoney = 26214400;
                coins18.SetActive(false);
                coins19.SetActive(true);
                coins20.SetActive(false);
                break;
            case 20:
                betMoney = 52428800;
                coins19.SetActive(false);
                coins20.SetActive(true);
                coins21.SetActive(false);
                break;
            case 21:
                betMoney = 104857600;
                coins20.SetActive(false);
                coins21.SetActive(true);
                coins22.SetActive(false);
                break;
            case 22:
                betMoney = 1000000000000;
                coins21.SetActive(false);
                coins22.SetActive(true);
                break;
            default:
                    break;
            }

        betFigure.text = "" + betMoney;

        if (betMoney >= 1000000000000)
        {
            betComma.text = ",       ,      ,      ,         ";
        }
        else if (betMoney >= 1000000000)
        {
            betComma.text = ",      ,      ,         ";
        }
        else if (betMoney >= 1000000)
        {
            betComma.text = ",      ,         ";
        }
        else if (betMoney >= 1000)
        {
            betComma.text = ",         ";
        }
        else
        {
            betComma.text = "";
        }
    }

    public void OnClickDownButton()
    {
        if (betMoneyRank > 1)
        {
            betMoneyRank -= 1;
            SE2.GetComponent<AudioSource>().Play();
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }

        switch (betMoneyRank)
        {
            case 1:
                betMoney = 100;
                coins1.SetActive(true);
                coins2.SetActive(false);
                break;
            case 2:
                betMoney = 200;
                coins1.SetActive(false);
                coins2.SetActive(true);
                coins3.SetActive(false);
                break;
            case 3:
                betMoney = 400;
                coins2.SetActive(false);
                coins3.SetActive(true);
                coins4.SetActive(false);
                break;
            case 4:
                betMoney = 800;
                coins3.SetActive(false);
                coins4.SetActive(true);
                coins5.SetActive(false);
                break;
            case 5:
                betMoney = 1600;
                coins4.SetActive(false);
                coins5.SetActive(true);
                coins6.SetActive(false);
                break;
            case 6:
                betMoney = 3200;
                coins5.SetActive(false);
                coins6.SetActive(true);
                coins7.SetActive(false);
                break;
            case 7:
                betMoney = 6400;
                coins6.SetActive(false);
                coins7.SetActive(true);
                coins8.SetActive(false);
                break;
            case 8:
                betMoney = 12800;
                coins7.SetActive(false);
                coins8.SetActive(true);
                coins9.SetActive(false);
                break;
            case 9:
                betMoney = 25600;
                coins8.SetActive(false);
                coins9.SetActive(true);
                coins10.SetActive(false);
                break;
            case 10:
                betMoney = 51200;
                coins9.SetActive(false);
                coins10.SetActive(true);
                coins11.SetActive(false);
                break;
            case 11:
                betMoney = 102400;
                coins10.SetActive(false);
                coins11.SetActive(true);
                coins12.SetActive(false);
                break;
            case 12:
                betMoney = 204800;
                coins11.SetActive(false);
                coins12.SetActive(true);
                coins13.SetActive(false);
                break;
            case 13:
                betMoney = 409600;
                coins12.SetActive(false);
                coins13.SetActive(true);
                coins14.SetActive(false);
                break;
            case 14:
                betMoney = 819200;
                coins13.SetActive(false);
                coins14.SetActive(true);
                coins15.SetActive(false);
                break;
            case 15:
                betMoney = 1638400;
                coins14.SetActive(false);
                coins15.SetActive(true);
                coins16.SetActive(false);
                break;
            case 16:
                betMoney = 3276800;
                coins15.SetActive(false);
                coins16.SetActive(true);
                coins17.SetActive(false);
                break;
            case 17:
                betMoney = 6553600;
                coins16.SetActive(false);
                coins17.SetActive(true);
                coins18.SetActive(false);
                break;
            case 18:
                betMoney = 13107200;
                coins17.SetActive(false);
                coins18.SetActive(true);
                coins19.SetActive(false);
                break;
            case 19:
                betMoney = 26214400;
                coins18.SetActive(false);
                coins19.SetActive(true);
                coins20.SetActive(false);
                break;
            case 20:
                betMoney = 52428800;
                coins19.SetActive(false);
                coins20.SetActive(true);
                coins21.SetActive(false);
                break;
            case 21:
                betMoney = 104857600;
                coins20.SetActive(false);
                coins21.SetActive(true);
                coins22.SetActive(false);
                break;
            case 22:
                betMoney = 1000000000000;
                coins21.SetActive(false);
                coins22.SetActive(true);
                break;
            default:
                break;
        }

        betFigure.text = "" + betMoney;

        if (betMoney >= 1000000000000)
        {
            betComma.text = ",       ,      ,      ,         ";
        }
        else if (betMoney >= 1000000000)
        {
            betComma.text = ",      ,      ,         ";
        }
        else if (betMoney >= 1000000)
        {
            betComma.text = ",      ,         ";
        }
        else if (betMoney >= 1000)
        {
            betComma.text = ",         ";
        }
        else
        {
            betComma.text = "";
        }
    }
}
