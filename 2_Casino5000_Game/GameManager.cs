using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ポーカーのゲーム部分（カードを配る、交換する、役を確認する）を動かすスクリプト
    /// </summary>
    int suit;
    int number;
    public GameObject playerHand;
    public GameObject DoubleUpBG;
    public GameObject pairpanel;
    public Text pairPanelText;
    public GameObject gameClearPanel;
    public GameObject pushThis;
    public GameObject ExchangeReminder1;
    public GameObject ExchangeReminder2;
    public GameObject ExchangeReminder3;
    public GameObject ExchangeReminder4;
    public GameObject ExchangeReminder5;

    GameObject card1;
    GameObject card2;
    GameObject card3;
    GameObject card4;
    GameObject card5;

    int Num1;
    int Num2;
    int Num3;
    int Num4;
    int Num5;

    int Suit1;
    int Suit2;
    int Suit3;
    int Suit4;
    int Suit5;

    public static long money = 1000;
    public int bet = 100;
    public Text moneyFigure;
    public int strength; // 役の強さ

    public static int EXP = 0;
    public Text EXPFigure;

    public GameObject ExchangeButton;
    public GameObject ExchangeButtonText;
    public GameObject PlayButton;
    public GameObject PlayButtonText;
    public GameObject Explanation;
    public GameObject GettingMoneyEffect;
    public Text GettingMoneyText;

    public long MoneyGot;

    public GameObject betUpButton;
    public GameObject betDownButton;
    BetController betController;
    long moneyBet = 100;

    public GameObject rollEnd;
    public GameObject choose;
    public GameObject cancel;
    public GameObject cardSound;
    public GameObject BGM1;
    public GameObject BGM2;
    public GameObject BGM3;

    public bool BGMSwitch1;
    public bool BGMSwitch2;
    public bool BGMSwitch3;

    private Animator anim = null;
    public bool change1 = false;
    public bool change2 = false;
    public bool change3 = false;
    public bool change4 = false;
    public bool change5 = false;

    int PosNumber;

    public bool DoubleUpSwitch;
    public Text DUMoney;
    public bool HighOrLow;
    public GameObject DUButton;
    public GameObject DUBBG;
    public Text DUExText;
    public GameObject DUExplanation;

    public static bool CanExchange = false;

    public Text moneyComma;
    public Text DUComma;
    public Text timePast;
    public static float startTime;
    public GameObject StartButton;
    public static bool started = false;
    public static bool cleared = false;
    public static float clearTime;
    public GameObject toRankingButton;

    public Text OddsBG;

    public GameObject SceneTransButton;

    public int ExNumber1;
    public int ExNumber2;
    public int ExNumber3;
    public int ExNumber4;
    public int ExNumber5;
    public int ExSuit1;
    public int ExSuit2;
    public int ExSuit3;
    public int ExSuit4;
    public int ExSuit5;

    public GameObject PlayButtonFigure;
    public GameObject StartBG;
    public GameObject loading;
    public GameObject SE3;
    public GameObject million;
    public GameObject trillion;
    public GameObject fanfare;
    public GameObject TimeRecord;
    public bool end;

    public GameObject C0;
    public GameObject C1;
    public GameObject C2;
    public GameObject C3;
    public GameObject C4;
    public GameObject C5;
    public GameObject C6;
    public GameObject C7;
    public GameObject C8;
    public GameObject C9;
    public GameObject D0;
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject D9;
    public GameObject E0;
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;
    public GameObject E5;
    public GameObject E6;
    public GameObject E7;
    public GameObject E8;
    public GameObject E9;
    public GameObject F0;
    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    public GameObject F4;
    public GameObject F5;
    public GameObject F6;
    public GameObject F7;
    public GameObject F8;
    public GameObject F9;
    public GameObject G1;
    public GameObject G2;

    public GameObject pic1;
    public GameObject pic4;
    public GameObject pic1to2;
    public GameObject pic5;
    public GameObject pic2;
    public GameObject picSmile;
    public GameObject picSad;
    public bool smiling;
    public bool sad;
    public int DealerPos = 90;

    void Start()
    {
        ExchangeButton.SetActive(false);
        pairpanel.SetActive(false);
        Explanation.SetActive(false);

        betController = betUpButton.GetComponent<BetController>();
        moneyBet = betController.betMoney;
        pushThis.SetActive(false);
        ExchangeReminder1.SetActive(false);
        ExchangeReminder2.SetActive(false);
        ExchangeReminder3.SetActive(false);
        ExchangeReminder4.SetActive(false);
        ExchangeReminder5.SetActive(false);

        moneyFigure.text = "" + money;
        EXPFigure.text = "" + EXP;
        if (money >= 1000000000000000)
        {
            moneyComma.text = ",      ,      ,       ,      ,       ";
        }
        else if (money >= 1000000000000)
        {
            moneyComma.text = ",      ,       ,      ,       ";
        }
        else if (money >= 1000000000)
        {
            moneyComma.text = ",       ,      ,       ";
        }
        else if (money >= 1000000)
        {
            moneyComma.text = ",      ,       ";
        }
        else if (money >= 1000)
        {
            moneyComma.text = ",       ";
        }
        else
        {
            moneyComma.text = "";
        }

        if (EditManager.skillSwitch7 == true)
        {
            InvokeRepeating("UnearnedIncome1", 0f, 1.0f);
        }

        if (EditManager.skillSwitch11 == true)
        {
            InvokeRepeating("UnearnedIncome2", 0.5f, 1.0f);
        }

        if (EditManager.skillSwitch9 == true)
        {
            OddsBG.text = "5カード ×8448   フラッシュ ×264 \nRSフラッシュ ×4224   ストレート ×165 \nSフラッシュ ×2112             3カード ×3 \n4カード ×1056               2ペア ×66 \nフルハウス ×528               1ペア ×33 ";
        }
        else if (EditManager.skillSwitch5 == true)
        {
            OddsBG.text = "5カード ×844.8  フラッシュ ×26.4\nRSフラッシュ ×422.4ストレート ×16.5\nSフラッシュ ×211.2                3カード ×3\n4カード ×105.6                 2ペア ×6.6/nフルハウス ×52.8                 1ペア ×3.3";
        }
        else if (EditManager.skillSwitch2 == true)
        {
            OddsBG.text = "5カード ×384      フラッシュ ×12  \nRSフラッシュ ×192     ストレート ×7.5  \nSフラッシュ ×96           3カード ×4.5  \n4カード ×48                  2ペア ×3  \nフルハウス ×24               1ペア ×1.5  ";
        }
        else
        {
            OddsBG.text = "5カード ×256      フラッシュ ×8  \nRSフラッシュ ×128      ストレート ×5  \nSフラッシュ ×64            3カード ×3  \n4カード ×32                2ペア ×2  \nフルハウス ×16                1ペア ×1  ";
        }

        if (started == false)
        {
            StartButton.SetActive(true);
            StartBG.SetActive(true);
            //全てのボタンを非表示に
            SceneTransButton.SetActive(false);
            PlayButton.SetActive(false);
            betUpButton.SetActive(false);
            betDownButton.SetActive(false);
            G1.SetActive(false);
            G2.SetActive(false);
        }
        else
        {
            StartButton.SetActive(false);
            InvokeRepeating("timeCounter", 0f, 0.01f);
            InvokeRepeating("betButtonMovement", 1.8f, 1.8f);
        }

        if (money < 1000000)
        {
            BGMSwitch1 = true;
        }
        if (money >= 1000000 && money < 1000000000000)
        {
            BGMSwitch2 = true;
        }
        if (money >= 1000000000000)
        {
            BGMSwitch3 = true;
        }

        if (money >= 1000000000000)
        {
            BGM1.GetComponent<AudioSource>().Stop();
            BGM2.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Play();
        }
        else if (money >= 1000000)
        {
            BGM1.GetComponent<AudioSource>().Stop();
            BGM2.GetComponent<AudioSource>().Play();
            BGM3.GetComponent<AudioSource>().Stop();
        }
        else
        {
            BGM1.GetComponent<AudioSource>().Play();
            BGM2.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Stop();
        }
        nokori();
        InvokeRepeating("DealerAnimation", 1.0f, 3.0f);
    }

    public void OnClickPlayButton()
    {
        moneyBet = betController.betMoney;
        if (moneyBet <= money)
        {
            choose.GetComponent<AudioSource>().Play();
            betUpButton.SetActive(false);
            betDownButton.SetActive(false);

            money -= moneyBet;
            moneyFigure.text = "" + money;
            YouLostMoney(moneyBet);

            if (money >= 1000000000000000)
            {
                moneyComma.text = ",      ,      ,       ,      ,       ";
            }
            else if (money >= 1000000000000)
            {
                moneyComma.text = ",      ,       ,      ,       ";
            }
            else if (money >= 1000000000)
            {
                moneyComma.text = ",       ,      ,       ";
            }
            else if (money >= 1000000)
            {
                moneyComma.text = ",      ,       ";
            }
            else if (money >= 1000)
            {
                moneyComma.text = ",       ";
            }
            else
            {
                moneyComma.text = "";
            }

            Destroy(card1);
            Destroy(card2);
            Destroy(card3);
            Destroy(card4);
            Destroy(card5);

            StartCoroutine("CardProduction");
            pairPanelText.text = "・・・・・・・・・・・";
            pairpanel.SetActive(false);
            ExchangeButton.SetActive(true);
            PlayButton.GetComponent<Image>().color -= new Color(0.0f, 0.0f, 0.0f, 0.5f);
            PlayButtonText.GetComponent<Text>().color -= new Color(0.0f, 0.0f, 0.0f, 0.5f);
            PlayButton.SetActive(false);
            Explanation.SetActive(true);
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
        CanExchange = true;
    }

    IEnumerator CardProduction2()
    {
        for (int i = 1; i < 6; i++)  //普通のカード生成
        {
            yield return null;

            if (i != 1)
            {
                cardSound.GetComponent<AudioSource>().Play();
            }

            //TrampPrefabのSuitとNumberをランダムに決めて画面に表示
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製        

            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            cardScript.cardPosition = i;
            if (EditManager.skillSwitch3 == true)
            {
                switch (i)
                {
                    case 2:
                        GameObject firstCard = GameObject.Find("firstCard");
                        CardScript firstCardScript = firstCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = firstCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 3:
                        GameObject secondCard = GameObject.Find("secondCard");
                        CardScript secondCardScript = secondCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = secondCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 4:
                        GameObject thirdCard = GameObject.Find("thirdCard");
                        CardScript thirdCardScript = thirdCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = thirdCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 5:
                        GameObject fourthCard = GameObject.Find("fourthCard");
                        CardScript fourthCardScript = fourthCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = fourthCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                }
                suit = Random.Range(1, 5);
                cardScript.Suit = suit;
            }
            else if (EditManager.skillSwitch1 == false)
            {
                suit = Random.Range(1, 5);
                Debug.Log("Suitは" + suit);
                number = Random.Range(1, 14);
                Debug.Log("Numberは" + number);
                cardScript.Suit = suit;
                cardScript.Number = number;
            }
            else
            {
                //前のカードの数を探してそれと同じ数にする
                switch (i)
                {
                    case 2:
                        GameObject firstCard = GameObject.Find("firstCard");
                        CardScript firstCardScript = firstCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = firstCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 3:
                        GameObject secondCard = GameObject.Find("secondCard");
                        CardScript secondCardScript = secondCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = secondCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 4:
                        GameObject thirdCard = GameObject.Find("thirdCard");
                        CardScript thirdCardScript = thirdCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = thirdCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 5:
                        GameObject fourthCard = GameObject.Find("fourthCard");
                        CardScript fourthCardScript = fourthCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = fourthCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                }
                suit = Random.Range(1, 5);
                cardScript.Suit = suit;
            }

            obj_clone.transform.SetParent(this.playerHand.transform, false);
            yield return new WaitForSeconds(0.35f);
            obj_clone.transform.localPosition = new Vector3(-270 + i * 90, 0, 0);

            switch (i)
            {
                case 1:
                    obj_clone.name = "firstCard";
                    break;
                case 2:
                    obj_clone.name = "secondCard";
                    break;
                case 3:
                    obj_clone.name = "thirdCard";
                    break;
                case 4:
                    obj_clone.name = "fourthCard";
                    break;
                case 5:
                    obj_clone.name = "fifthCard";
                    break;
                default:
                    break;
            }
        }

        ExchangeReminder1.SetActive(true);
        ExchangeReminder2.SetActive(true);
        ExchangeReminder3.SetActive(true);
        ExchangeReminder4.SetActive(true);
        ExchangeReminder5.SetActive(true);
    }

    IEnumerator CardFlipping()
    {
        yield return null;

        for (int i = 1; i < 6; i++)
        {
            yield return null;

            GameObject obj2 = (GameObject)Resources.Load("ProvideEffect");// Cardsプレハブを取得
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3((-270 + i * 90) * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
        }
    }

    IEnumerator CardProduction()
    {
        for (int i = 1; i < 6; i++)  //普通のカード生成
        {
            yield return null;

            if (i != 1)
            {
                cardSound.GetComponent<AudioSource>().Play();
            }

            //TrampPrefabのSuitとNumberをランダムに決めて画面に表示
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製        

            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            cardScript.cardPosition = i;
            if (EditManager.skillSwitch3 == true)
            {
                switch (i)
                {
                    case 2:
                        GameObject firstCard = GameObject.Find("firstCard");
                        CardScript firstCardScript = firstCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = firstCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 3:
                        GameObject secondCard = GameObject.Find("secondCard");
                        CardScript secondCardScript = secondCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = secondCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 4:
                        GameObject thirdCard = GameObject.Find("thirdCard");
                        CardScript thirdCardScript = thirdCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = thirdCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 5:
                        GameObject fourthCard = GameObject.Find("fourthCard");
                        CardScript fourthCardScript = fourthCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 45)
                        {
                            cardScript.Number = fourthCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                }
                suit = Random.Range(1, 5);
                cardScript.Suit = suit;
            }
            else if (EditManager.skillSwitch1 == false)
            {
                suit = Random.Range(1, 5);
                Debug.Log("Suitは" + suit);
                number = Random.Range(1, 14);
                Debug.Log("Numberは" + number);
                cardScript.Suit = suit;
                cardScript.Number = number;
            }
            else
            {
                //前のカードの数を探してそれと同じ数にする
                switch (i)
                {
                    case 2:
                        GameObject firstCard = GameObject.Find("firstCard");
                        CardScript firstCardScript = firstCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = firstCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 3:
                        GameObject secondCard = GameObject.Find("secondCard");
                        CardScript secondCardScript = secondCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = secondCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 4:
                        GameObject thirdCard = GameObject.Find("thirdCard");
                        CardScript thirdCardScript = thirdCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = thirdCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                    case 5:
                        GameObject fourthCard = GameObject.Find("fourthCard");
                        CardScript fourthCardScript = fourthCard.GetComponent<CardScript>();
                        PosNumber = Random.Range(1, 100);
                        if (PosNumber >= 70)
                        {
                            cardScript.Number = fourthCardScript.Number;//50％の確率で前のカードと同じに
                        }
                        else
                        {
                            number = Random.Range(1, 14);
                            cardScript.Number = number;
                        }
                        break;
                }
                suit = Random.Range(1, 5);
                cardScript.Suit = suit;
            }

            obj_clone.transform.SetParent(this.playerHand.transform, false);

            GameObject obj2 = (GameObject)Resources.Load("ProvideEffect");// Cardsプレハブを取得
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3((-270 + i * 90) * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }

            obj_clone.transform.localPosition = new Vector3(-270 + i * 90, 0, 0);

            switch (i)
            {
                case 1:
                    obj_clone.name = "firstCard";
                    break;
                case 2:
                    obj_clone.name = "secondCard";
                    break;
                case 3:
                    obj_clone.name = "thirdCard";
                    break;
                case 4:
                    obj_clone.name = "fourthCard";
                    break;
                case 5:
                    obj_clone.name = "fifthCard";
                    break;
                default:
                    break;
            }

        }

        ExchangeReminder1.SetActive(true);
        ExchangeReminder2.SetActive(true);
        ExchangeReminder3.SetActive(true);
        ExchangeReminder4.SetActive(true);
        ExchangeReminder5.SetActive(true);
    }

    public void OnClickDoubleUpButton()
    {
        DoubleUpBG.SetActive(true);
        DUExplanation.SetActive(true);
        if (EditManager.skillSwitch10 == false)
        {
            DUExText.text = "的中すれば報酬２倍！";
        }
        else
        {
            DUExText.text = "的中すれば報酬１０倍！";
        }
        DUMoney.text = "" + MoneyGot + " $  ";

        if (MoneyGot >= 1000000000000)
        {
            DUComma.text = ",      ,       ,       ,             ";
        }
        else if (MoneyGot >= 1000000000)
        {
            DUComma.text = ",       ,       ,             ";
        }
        else if (MoneyGot >= 1000000)
        {
            DUComma.text = ",       ,             ";
        }
        else if (MoneyGot >= 1000)
        {
            DUComma.text = ",             ";
        }
        else
        {
            DUComma.text = "";
        }

        money -= MoneyGot;
        moneyFigure.text = "" + money;
        YouLostMoney(MoneyGot);

        DoubleUpSwitch = true;
        StartCoroutine("DoubleUpProduction");
        StartCoroutine("DoubleUpFlipping");
    }

    public void OnClickHighButton()
    {
        HighOrLow = true;
        DoubleUpSwitch = false;
        DUButton.SetActive(false);
        DUBBG.SetActive(false);
        StartCoroutine("DoubleUpProduction");
        StartCoroutine("DoubleUpFlipping");
        StartCoroutine("DoubleUpDetecter");
    }
    public void OnClickLowButton()
    {
        HighOrLow = false;
        DoubleUpSwitch = false;
        DUButton.SetActive(false);
        DUBBG.SetActive(false);
        StartCoroutine("DoubleUpProduction");
        StartCoroutine("DoubleUpFlipping");
        StartCoroutine("DoubleUpDetecter");
    }

    IEnumerator DoubleUpProduction()
    {
        yield return null;

        cardSound.GetComponent<AudioSource>().Play();

        //TrampPrefabのSuitとNumberをランダムに決めて画面に表示
        GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
        GameObject obj_clone = Instantiate(obj, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製        

        CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得

        suit = Random.Range(1, 5);
        number = Random.Range(1, 14);
        cardScript.Suit = suit;
        cardScript.Number = number;
        obj_clone.transform.SetParent(this.DoubleUpBG.transform, false);
        yield return new WaitForSeconds(0.35f);

        if (DoubleUpSwitch == true)
        {
            obj_clone.transform.localPosition = new Vector3(-50, 5, 0);

            obj_clone.name = "DUCard1";
        }
        else
        {
            obj_clone.transform.localPosition = new Vector3(50, 5, 0);

            obj_clone.name = "DUCard2";

            if (EditManager.skillSwitch10 == true)
            {
                GameObject DUCard1 = GameObject.Find("DUCard1");
                CardScript cardScriptEin = DUCard1.GetComponent<CardScript>();
                GameObject DUCard2 = GameObject.Find("DUCard2");
                CardScript cardScriptZwei = DUCard2.GetComponent<CardScript>();

                PosNumber = Random.Range(1, 100);

                if (HighOrLow == true)
                {
                    if (PosNumber > 10)
                    {
                        cardScriptZwei.Number = Random.Range(cardScriptEin.Number + 1, 14);
                        cardScriptZwei.Figuriserer();
                    }

                }
                else
                {
                    if (PosNumber > 10)
                    {
                        cardScriptZwei.Number = Random.Range(1, cardScriptEin.Number + 1);
                        cardScriptZwei.Figuriserer();
                    }
                }
            }
        }
    }

    IEnumerator DoubleUpFlipping()
    {
        yield return null;

        yield return null;

        GameObject obj3 = (GameObject)Resources.Load("DoubleUpEffect");// Cardsプレハブを取得
        GameObject obj3_clone = Instantiate(obj3, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
        anim = obj3_clone.GetComponent<Animator>();
        anim.SetBool("provide1", true);
        obj3_clone.transform.SetParent(this.DoubleUpBG.transform, false);
        if (DoubleUpSwitch == true)
        {
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj3_clone.transform.localPosition = new Vector3(-50 * ((x - 1) / x), 5 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj3_clone);
                }
            }
        }
        else
        {
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj3_clone.transform.localPosition = new Vector3(50 * ((x - 1) / x), 5 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj3_clone);
                }
            }
        }
    }

    IEnumerator DoubleUpDetecter()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject DUCard1 = GameObject.Find("DUCard1");
        CardScript cardScript = DUCard1.GetComponent<CardScript>();
        GameObject DUCard2 = GameObject.Find("DUCard2");
        CardScript cardScript2 = DUCard2.GetComponent<CardScript>();
        if (HighOrLow == true)
        {
            if (cardScript.Number > cardScript2.Number)
            {
                DUExText.text = "失敗！";
                StartCoroutine("Sad");
            }
            else
            {
                if (EditManager.skillSwitch10 == false)
                {
                    //金を２倍にして増やす
                    DUMoney.text = "" + MoneyGot * 2 + " $  ";

                    if (MoneyGot * 2 >= 1000000000000)
                    {
                        DUComma.text = ",      ,       ,       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000000000)
                    {
                        DUComma.text = ",       ,       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000000)
                    {
                        DUComma.text = ",       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000)
                    {
                        DUComma.text = ",             ";
                    }
                    else
                    {
                        DUComma.text = "";
                    }

                    money += MoneyGot * 2;
                    YouGotMoney(MoneyGot * 2);
                }
                else if (EditManager.skillSwitch10 == true)
                {
                    //金を２倍にして増やす
                    DUMoney.text = "" + MoneyGot * 10 + " $  ";

                    if (MoneyGot * 10 >= 1000000000000)
                    {
                        DUComma.text = ",      ,       ,       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000000000)
                    {
                        DUComma.text = ",       ,       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000000)
                    {
                        DUComma.text = ",       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000)
                    {
                        DUComma.text = ",             ";
                    }
                    else
                    {
                        DUComma.text = "";
                    }

                    money += MoneyGot * 10;
                    YouGotMoney(MoneyGot * 10);
                }
                DUExText.text = "成功！";
                SE3.GetComponent<AudioSource>().Play();
                StartCoroutine("Smiling");
            }

        }
        else
        {
            if (cardScript.Number > cardScript2.Number)
            {
                if (EditManager.skillSwitch10 == false)
                {
                    //金を２倍にして増やす
                    DUMoney.text = "" + MoneyGot * 2 + " $  ";

                    if (MoneyGot * 2 >= 1000000000000)
                    {
                        DUComma.text = ",      ,       ,       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000000000)
                    {
                        DUComma.text = ",       ,       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000000)
                    {
                        DUComma.text = ",       ,             ";
                    }
                    else if (MoneyGot * 2 >= 1000)
                    {
                        DUComma.text = ",             ";
                    }
                    else
                    {
                        DUComma.text = "";
                    }

                    money += MoneyGot * 2;
                    YouGotMoney(MoneyGot * 2);
                }
                else if (EditManager.skillSwitch10 == true)
                {
                    //金を２倍にして増やす
                    DUMoney.text = "" + MoneyGot * 10 + " $  ";

                    if (MoneyGot * 10 >= 1000000000000)
                    {
                        DUComma.text = ",      ,       ,       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000000000)
                    {
                        DUComma.text = ",       ,       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000000)
                    {
                        DUComma.text = ",       ,             ";
                    }
                    else if (MoneyGot * 10 >= 1000)
                    {
                        DUComma.text = ",             ";
                    }
                    else
                    {
                        DUComma.text = "";
                    }

                    money += MoneyGot * 10;
                    YouGotMoney(MoneyGot * 10);
                }
                DUExText.text = "成功！";
                SE3.GetComponent<AudioSource>().Play();
                StartCoroutine("Smiling");
            }
            else
            {
                DUExText.text = "失敗！";
                StartCoroutine("Sad");
            }
        }
        moneyFigure.text = "" + money;

        if (money >= 1000000000000000)
        {
            moneyComma.text = ",      ,      ,       ,      ,       ";
        }
        else if (money >= 1000000000000)
        {
            moneyComma.text = ",      ,       ,      ,       ";
        }
        else if (money >= 1000000000)
        {
            moneyComma.text = ",       ,      ,       ";
        }
        else if (money >= 1000000)
        {
            moneyComma.text = ",      ,       ";
        }
        else if (money >= 1000)
        {
            moneyComma.text = ",       ";
        }
        else
        {
            moneyComma.text = "";
        }

        if (money >= 5000000000000000)
        {
            gameclear();
        }

        yield return new WaitForSeconds(1.0f);
        Destroy(DUCard1);
        Destroy(DUCard2);
        DoubleUpBG.SetActive(false);
        DUExplanation.SetActive(false);

        if (money < 1000000)
        {
            BGMSwitch1 = true;
        }
        if (money >= 1000000 && money < 1000000000000)
        {
            BGMSwitch2 = true;
        }
        if (money >= 1000000000000)
        {
            BGMSwitch3 = true;
        }
        if (BGMSwitch1 == true && BGMSwitch2 == true)
        {
            BGMSwitch1 = false;
            BGM1.GetComponent<AudioSource>().Stop();
            BGM2.GetComponent<AudioSource>().Play();
            StartCoroutine("messageMillion");
        }
        if (BGMSwitch2 == true && BGMSwitch3 == true)
        {
            BGMSwitch2 = false;
            BGM2.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Play();
            StartCoroutine("messageTrillion");
        }
        if (BGMSwitch1 == true && BGMSwitch3 == true)
        {
            BGMSwitch1 = false;
            BGM1.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Play();
            StartCoroutine("messageTrillion");
        }
        nokori();
    }

    public void OnClickExchangeButton()
    {
        card1 = GameObject.Find("firstCard");
        card2 = GameObject.Find("secondCard");
        card3 = GameObject.Find("thirdCard");
        card4 = GameObject.Find("fourthCard");
        card5 = GameObject.Find("fifthCard");
        CardScript cardScript1 = card1.GetComponent<CardScript>();
        CardScript cardScript2 = card2.GetComponent<CardScript>();
        CardScript cardScript3 = card3.GetComponent<CardScript>();
        CardScript cardScript4 = card4.GetComponent<CardScript>();
        CardScript cardScript5 = card5.GetComponent<CardScript>();

        ExNumber1 = cardScript1.Number;
        ExNumber2 = cardScript2.Number;
        ExNumber3 = cardScript3.Number;
        ExNumber4 = cardScript4.Number;
        ExNumber5 = cardScript5.Number;
        ExSuit1 = cardScript1.Suit;
        ExSuit2 = cardScript2.Suit;
        ExSuit3 = cardScript3.Suit;
        ExSuit4 = cardScript4.Suit;
        ExSuit5 = cardScript5.Suit;

        cardScript1.activemark.SetActive(false);
        cardScript2.activemark.SetActive(false);
        cardScript3.activemark.SetActive(false);
        cardScript4.activemark.SetActive(false);
        cardScript5.activemark.SetActive(false);


        if (cardScript1.activation == false)
        {
            Destroy(card1);
            change1 = true;
        }
        else
        {
            change1 = false;
        }
        if (cardScript2.activation == false)
        {
            Destroy(card2);
            change2 = true;
        }
        else
        {
            change2 = false;
        }
        if (cardScript3.activation == false)
        {
            Destroy(card3);
            change3 = true;
        }
        else
        {
            change3 = false;
        }
        if (cardScript4.activation == false)
        {
            Destroy(card4);
            change4 = true;
        }
        else
        {
            change4 = false;
        }
        if (cardScript5.activation == false)
        {
            Destroy(card5);
            change5 = true;
        }
        else
        {
            change5 = false;
        }
        StartCoroutine("CardExchange");

        pairpanel.SetActive(true);
        StartCoroutine("HandDetecter");

        ExchangeButton.SetActive(false);

        Explanation.SetActive(false);

        betUpButton.SetActive(true);
        betDownButton.SetActive(true);

        ExchangeReminder1.SetActive(false);
        ExchangeReminder2.SetActive(false);
        ExchangeReminder3.SetActive(false);
        ExchangeReminder4.SetActive(false);
        ExchangeReminder5.SetActive(false);

        CanExchange = false;
    }

    private IEnumerator HandDetecter()
    {
        choose.GetComponent<AudioSource>().Stop();
        choose.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.0f);
        HandDetection();

        if (strength != 1)
        {
            StartCoroutine("Smiling");
        }
        else
        {
            StartCoroutine("Sad");
        }

        moneyBet = betController.betMoney;

        if (EditManager.skillSwitch2 == false && EditManager.skillSwitch5 == false && EditManager.skillSwitch9 == false)
        {
            switch (strength)
            {
                case 1:
                    money -= 0;
                    MoneyGot = 0;
                    EXP += 1;
                    break;
                case 2:
                    money += moneyBet;
                    MoneyGot = moneyBet;
                    EXP += 1;
                    break;
                case 3:
                    money += moneyBet * 2;
                    MoneyGot = moneyBet * 2;
                    EXP += 2;
                    break;
                case 4:
                    money += moneyBet * 3;
                    MoneyGot = moneyBet * 3;
                    EXP += 3;
                    break;
                case 5:
                    money += moneyBet * 5;
                    MoneyGot = moneyBet * 5;
                    EXP += 4;
                    break;
                case 6:
                    money += moneyBet * 8;
                    MoneyGot = moneyBet * 8;
                    EXP += 5;
                    break;
                case 7:
                    money += moneyBet * 16;
                    MoneyGot = moneyBet * 16;
                    EXP += 6;
                    break;
                case 8:
                    money += moneyBet * 32;
                    MoneyGot = moneyBet * 32;
                    EXP += 7;
                    break;
                case 9:
                    money += moneyBet * 64;
                    MoneyGot = moneyBet * 64;
                    EXP += 8;
                    break;
                case 10:
                    money += moneyBet * 128;
                    MoneyGot = moneyBet * 128;
                    EXP += 9;
                    break;
                case 11:
                    money += moneyBet * 256;
                    MoneyGot = moneyBet * 256;
                    EXP += 10;
                    break;
                default:
                    break;
            }
        }
        else if (EditManager.skillSwitch2 == true && EditManager.skillSwitch5 == false && EditManager.skillSwitch9 == false)
        {
            switch (strength)
            {
                case 1:
                    money -= 0;
                    MoneyGot = 0;
                    EXP += 1;
                    break;
                case 2:
                    money += (moneyBet * 1) + (moneyBet / 2);
                    MoneyGot = (moneyBet * 1) + (moneyBet / 2);
                    EXP += 1;
                    break;
                case 3:
                    money += moneyBet * 3;
                    MoneyGot = moneyBet * 3;
                    EXP += 2;
                    break;
                case 4:
                    money += (moneyBet * 4) + (moneyBet / 2);
                    MoneyGot = (moneyBet * 4) + (moneyBet / 2);
                    EXP += 3;
                    break;
                case 5:
                    money += (moneyBet * 7) + (moneyBet / 2);
                    MoneyGot = (moneyBet * 7) + (moneyBet / 2);
                    EXP += 4;
                    break;
                case 6:
                    money += moneyBet * 12;
                    MoneyGot = moneyBet * 12;
                    EXP += 5;
                    break;
                case 7:
                    money += moneyBet * 24;
                    MoneyGot = moneyBet * 24;
                    EXP += 6;
                    break;
                case 8:
                    money += moneyBet * 48;
                    MoneyGot = moneyBet * 48;
                    EXP += 7;
                    break;
                case 9:
                    money += moneyBet * 96;
                    MoneyGot = moneyBet * 96;
                    EXP += 8;
                    break;
                case 10:
                    money += moneyBet * 192;
                    MoneyGot = moneyBet * 192;
                    EXP += 9;
                    break;
                case 11:
                    money += moneyBet * 384;
                    MoneyGot = moneyBet * 384;
                    EXP += 10;
                    break;
                default:
                    break;
            }
        }
        else if (EditManager.skillSwitch5 == true && EditManager.skillSwitch9 == false)
        {
            switch (strength)
            {
                case 1:
                    money -= 0;
                    MoneyGot = 0;
                    EXP += 1;
                    break;
                case 2:
                    money += (moneyBet * 3) + (moneyBet * 3 / 10);
                    MoneyGot = (moneyBet * 3) + (moneyBet * 3 / 10);
                    EXP += 1;
                    break;
                case 3:
                    money += (moneyBet * 6) + (moneyBet * 6 / 10);
                    MoneyGot = (moneyBet * 6) + (moneyBet * 6 / 10);
                    EXP += 2;
                    break;
                case 4:
                    money += (moneyBet * 9) + (moneyBet * 9 / 10);
                    MoneyGot = (moneyBet * 9) + (moneyBet * 9 / 10);
                    EXP += 3;
                    break;
                case 5:
                    money += (moneyBet * 16) + (moneyBet * 5 / 10);
                    MoneyGot = (moneyBet * 16) + (moneyBet * 5 / 10);
                    EXP += 4;
                    break;
                case 6:
                    money += (moneyBet * 26) + (moneyBet * 4 / 10);
                    MoneyGot = (moneyBet * 26) + (moneyBet * 4 / 10);
                    EXP += 5;
                    break;
                case 7:
                    money += (moneyBet * 52) + (moneyBet * 8 / 10);
                    MoneyGot = (moneyBet * 52) + (moneyBet * 8 / 10);
                    EXP += 6;
                    break;
                case 8:
                    money += (moneyBet * 105) + (moneyBet * 6 / 10);
                    MoneyGot = (moneyBet * 105) + (moneyBet * 6 / 10);
                    EXP += 7;
                    break;
                case 9:
                    money += (moneyBet * 211) + (moneyBet * 2 / 10);
                    MoneyGot = (moneyBet * 211) + (moneyBet * 2 / 10);
                    EXP += 8;
                    break;
                case 10:
                    money += (moneyBet * 422) + (moneyBet * 4 / 10);
                    MoneyGot = (moneyBet * 422) + (moneyBet * 4 / 10);
                    EXP += 9;
                    break;
                case 11:
                    money += (moneyBet * 844) + (moneyBet * 8 / 10);
                    MoneyGot = (moneyBet * 844) + (moneyBet * 8 / 10);
                    EXP += 10;
                    break;
                default:
                    break;
            }
        }
        else if (EditManager.skillSwitch9 == true)
        {
            switch (strength)
            {
                case 1:
                    money -= 0;
                    MoneyGot = 0;
                    EXP += 1;
                    break;
                case 2:
                    money += moneyBet * 33;
                    MoneyGot = moneyBet * 33;
                    EXP += 1;
                    break;
                case 3:
                    money += moneyBet * 66;
                    MoneyGot = moneyBet * 66;
                    EXP += 2;
                    break;
                case 4:
                    money += moneyBet * 99;
                    MoneyGot = moneyBet * 99;
                    EXP += 3;
                    break;
                case 5:
                    money += moneyBet * 165;
                    MoneyGot = moneyBet * 165;
                    EXP += 4;
                    break;
                case 6:
                    money += moneyBet * 264;
                    MoneyGot = moneyBet * 264;
                    EXP += 5;
                    break;
                case 7:
                    money += moneyBet * 528;
                    MoneyGot = moneyBet * 528;
                    EXP += 6;
                    break;
                case 8:
                    money += moneyBet * 1056;
                    MoneyGot = moneyBet * 1056;
                    EXP += 7;
                    break;
                case 9:
                    money += moneyBet * 2112;
                    MoneyGot = moneyBet * 2112;
                    EXP += 8;
                    break;
                case 10:
                    money += moneyBet * 4224;
                    MoneyGot = moneyBet * 4224;
                    EXP += 9;
                    break;
                case 11:
                    money += moneyBet * 8448;
                    MoneyGot = moneyBet * 8448;
                    EXP += 10;
                    break;
                default:
                    break;
            }
        }

        moneyFigure.text = "" + money;

        if (money >= 1000000000000000)
        {
            moneyComma.text = ",      ,      ,       ,      ,       ";
        }
        else if (money >= 1000000000000)
        {
            moneyComma.text = ",      ,       ,      ,       ";
        }
        else if (money >= 1000000000)
        {
            moneyComma.text = ",       ,      ,       ";
        }
        else if (money >= 1000000)
        {
            moneyComma.text = ",      ,       ";
        }
        else if (money >= 1000)
        {
            moneyComma.text = ",       ";
        }
        else
        {
            moneyComma.text = "";
        }

        SE3.GetComponent<AudioSource>().Play();
        YouGotMoney(MoneyGot);

        EXPFigure.text = "" + EXP;

        if (money < 1000000)
        {
            BGMSwitch1 = true;
        }
        if (money >= 1000000 && money < 1000000000000)
        {
            BGMSwitch2 = true;
        }
        if (money >= 1000000000000)
        {
            BGMSwitch3 = true;
        }
        if (BGMSwitch1 == true && BGMSwitch2 == true)
        {
            BGMSwitch1 = false;
            BGM1.GetComponent<AudioSource>().Stop();
            BGM2.GetComponent<AudioSource>().Play();
            StartCoroutine("messageMillion");
        }
        if (BGMSwitch2 == true && BGMSwitch3 == true)
        {
            BGMSwitch2 = false;
            BGM2.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Play();
            StartCoroutine("messageTrillion");
        }
        if (BGMSwitch1 == true && BGMSwitch3 == true)
        {
            BGMSwitch1 = false;
            BGM1.GetComponent<AudioSource>().Stop();
            BGM3.GetComponent<AudioSource>().Play();
            StartCoroutine("messageTrillion");
        }

        if (money >= 5000000000000000)
        {
            gameclear();
        }
        PlayButton.SetActive(true);
        PlayButton.GetComponent<Image>().color = new Color(1.0f, 0.28f, 0.0f, 0.5f);
        PlayButtonFigure.GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        if (EditManager.skillSwitch6 == true)
        {
            DUButton.SetActive(true);
            DUBBG.SetActive(true);
        }
        nokori();
    }

    public void YouGotMoney(long moneyGot)
    {
        GettingMoneyEffect.SetActive(true);
        moneyBet = betController.betMoney;
        GettingMoneyText.text = "+" + moneyGot + " $";
        GettingMoneyEffect.transform.localPosition = new Vector3(-50, 0, 0);
        StartCoroutine("Eraser");
    }

    public void YouLostMoney(long moneyGot)
    {
        GettingMoneyEffect.SetActive(true);
        moneyBet = betController.betMoney;
        GettingMoneyText.text = "-" + moneyGot + " $";
        GettingMoneyEffect.transform.localPosition = new Vector3(-50, 0, 0);
        StartCoroutine("Eraser");
    }

    private IEnumerator Eraser()
    {
        yield return new WaitForSeconds(2.0f);
        GettingMoneyEffect.SetActive(false);
    }

    public IEnumerator CardExchange()
    {
        GameObject obj2 = (GameObject)Resources.Load("ProvideEffect");// Cardsプレハブを取得

        if (change1 == true)
        {
            cardSound.GetComponent<AudioSource>().Play();
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3(-180 * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製            
            obj_clone.transform.localPosition = new Vector3(-180, 0, 0);
            obj_clone.transform.SetParent(this.playerHand.transform, false);
            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            obj_clone.name = "firstCard";
            suit = Random.Range(1, 5);
            Debug.Log("Suitは" + suit);
            if (EditManager.skillSwitch3 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 50)
                {
                    cardScript.Number = ExNumber2;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else if (EditManager.skillSwitch1 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 20)
                {
                    cardScript.Number = ExNumber2;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else
            {
                number = Random.Range(1, 14);
                cardScript.Number = number;
            }
        }
        if (change2 == true)
        {
            cardSound.GetComponent<AudioSource>().Play();
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3(-90 * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製            
            obj_clone.transform.localPosition = new Vector3(-90, 0, 0);
            obj_clone.transform.SetParent(this.playerHand.transform, false);
            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            obj_clone.name = "secondCard";
            suit = Random.Range(1, 5);
            Debug.Log("Suitは" + suit);
            cardScript.Suit = suit;
            if (EditManager.skillSwitch3 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 50)
                {
                    cardScript.Number = ExNumber1;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else if (EditManager.skillSwitch1 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 20)
                {
                    cardScript.Number = ExNumber1;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else
            {
                number = Random.Range(1, 14);
                cardScript.Number = number;
            }
        }
        if (change3 == true)
        {
            cardSound.GetComponent<AudioSource>().Play();
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3(0, 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製            
            obj_clone.transform.localPosition = new Vector3(0, 0, 0);
            obj_clone.transform.SetParent(this.playerHand.transform, false);
            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            obj_clone.name = "thirdCard";
            suit = Random.Range(1, 5);
            Debug.Log("Suitは" + suit);
            cardScript.Suit = suit;
            if (EditManager.skillSwitch3 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 50)
                {
                    cardScript.Number = ExNumber2;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else if (EditManager.skillSwitch1 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 20)
                {
                    cardScript.Number = ExNumber2;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else
            {
                number = Random.Range(1, 14);
                cardScript.Number = number;
            }
        }
        if (change4 == true)
        {
            cardSound.GetComponent<AudioSource>().Play();
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3(90 * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製            
            obj_clone.transform.localPosition = new Vector3(90, 0, 0);
            obj_clone.transform.SetParent(this.playerHand.transform, false);
            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            obj_clone.name = "fourthCard";
            suit = Random.Range(1, 5);
            Debug.Log("Suitは" + suit);
            cardScript.Suit = suit;
            if (EditManager.skillSwitch3 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 50)
                {
                    cardScript.Number = ExNumber3;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else if (EditManager.skillSwitch1 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 20)
                {
                    cardScript.Number = ExNumber3;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else
            {
                number = Random.Range(1, 14);
                cardScript.Number = number;
            }
        }
        if (change5 == true)
        {
            cardSound.GetComponent<AudioSource>().Play();
            GameObject obj2_clone = Instantiate(obj2, new Vector3(1000.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製    
            anim = obj2_clone.GetComponent<Animator>();
            anim.SetBool("provide1", true);
            obj2_clone.transform.SetParent(this.playerHand.transform, false);
            for (int m = 0; m < 11; m++)
            {
                yield return new WaitForSeconds(0.03f);
                float x = Mathf.Pow(2, m);
                obj2_clone.transform.localPosition = new Vector3(180 * ((x - 1) / x), 200 - 200 * ((x - 1) / x), -1);
                if (m == 10)
                {
                    Destroy(obj2_clone);
                }
            }
            GameObject obj = (GameObject)Resources.Load("Cards");// Cardsプレハブを取得
            GameObject obj_clone = Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);//Cardsプレハブを複製            
            obj_clone.transform.localPosition = new Vector3(180, 0, 0);
            obj_clone.transform.SetParent(this.playerHand.transform, false);
            CardScript cardScript = obj_clone.GetComponent<CardScript>();//複製したCardsオブジェクトのCardsコンポネントを取得
            obj_clone.name = "fifthCard";
            suit = Random.Range(1, 5);
            Debug.Log("Suitは" + suit);
            cardScript.Suit = suit;
            if (EditManager.skillSwitch3 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 50)
                {
                    cardScript.Number = ExNumber4;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else if (EditManager.skillSwitch1 == true)
            {
                PosNumber = Random.Range(1, 100);
                if (PosNumber >= 20)
                {
                    cardScript.Number = ExNumber4;//50％の確率で前のカードと同じに
                }
                else
                {
                    number = Random.Range(1, 14);
                    cardScript.Number = number;
                }
            }
            else
            {
                number = Random.Range(1, 14);
                cardScript.Number = number;
            }
        }
    }

    public void OnClickSTButton()
    {
        loading.SetActive(true);
        choose.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("EditScene");
    }


    public void HandDetection()
    {
        card1 = GameObject.Find("firstCard");
        card2 = GameObject.Find("secondCard");
        card3 = GameObject.Find("thirdCard");
        card4 = GameObject.Find("fourthCard");
        card5 = GameObject.Find("fifthCard");
        CardScript cardScript1 = card1.GetComponent<CardScript>();
        CardScript cardScript2 = card2.GetComponent<CardScript>();
        CardScript cardScript3 = card3.GetComponent<CardScript>();
        CardScript cardScript4 = card4.GetComponent<CardScript>();
        CardScript cardScript5 = card5.GetComponent<CardScript>();
        Num1 = cardScript1.Number;
        Num2 = cardScript2.Number;
        Num3 = cardScript3.Number;
        Num4 = cardScript4.Number;
        Num5 = cardScript5.Number;
        Suit1 = cardScript1.Suit;
        Suit2 = cardScript2.Suit;
        Suit3 = cardScript3.Suit;
        Suit4 = cardScript4.Suit;
        Suit5 = cardScript5.Suit;

        bool isFlush = false;
        bool isStrait = false;

        if (Num1 - Num2 == 1)
        {
            if (Num2 - Num3 == 1)
            {
                if (Num3 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num4 == 1)
            {
                if (Num4 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num5 == 1)
            {
                if (Num5 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num1 - Num3 == 1)
        {
            if (Num3 - Num2 == 1)
            {
                if (Num2 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num4 == 1)
            {
                if (Num4 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num5 == 1)
            {
                if (Num5 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num1 - Num4 == 1)
        {
            if (Num4 - Num2 == 1)
            {
                if (Num2 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num3 == 1)
            {
                if (Num3 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num5 == 1)
            {
                if (Num5 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num1 - Num5 == 1)
        {
            if (Num5 - Num2 == 1)
            {
                if (Num2 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num3 == 1)
            {
                if (Num3 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num4 == 1)
            {
                if (Num4 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num2 - Num1 == 1)
        {
            if (Num1 - Num3 == 1)
            {
                if (Num3 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num4 == 1)
            {
                if (Num4 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num5 == 1)
            {
                if (Num5 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num2 - Num3 == 1)
        {
            if (Num3 - Num1 == 1)
            {
                if (Num1 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num2 - Num4 == 1)
        {
            if (Num4 - Num1 == 1)
            {
                if (Num1 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num2 - Num5 == 1)
        {
            if (Num5 - Num1 == 1)
            {
                if (Num1 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num3 - Num1 == 1)
        {
            if (Num1 - Num2 == 1)
            {
                if (Num2 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num4 == 1)
            {
                if (Num4 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num5 == 1)
            {
                if (Num5 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num3 - Num2 == 1)
        {
            if (Num2 - Num1 == 1)
            {
                if (Num1 - Num4 == 1 && Num4 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num3 - Num4 == 1)
        {
            if (Num4 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num3 - Num5 == 1)
        {
            if (Num5 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num4 - Num1 == 1)
        {
            if (Num1 - Num2 == 1)
            {
                if (Num2 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num3 == 1)
            {
                if (Num3 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num5 == 1)
            {
                if (Num5 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num4 - Num2 == 1)
        {
            if (Num2 - Num1 == 1)
            {
                if (Num1 - Num3 == 1 && Num3 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num4 - Num3 == 1)
        {
            if (Num3 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num5 == 1 && Num5 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num5 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num5 == 1 && Num5 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num5 == 1)
            {
                if (Num5 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num5 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num4 - Num5 == 1)
        {
            if (Num5 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num5 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num5 - Num1 == 1)
        {
            if (Num1 - Num2 == 1)
            {
                if (Num2 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num3 == 1)
            {
                if (Num3 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num1 - Num4 == 1)
            {
                if (Num4 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num5 - Num2 == 1)
        {
            if (Num2 - Num1 == 1)
            {
                if (Num1 - Num3 == 1 && Num3 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num4 == 1 && Num4 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num2 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num5 - Num3 == 1)
        {
            if (Num3 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num4 == 1 && Num4 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num4 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num4 == 1 && Num4 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num3 - Num4 == 1)
            {
                if (Num4 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num4 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }
        if (Num5 - Num4 == 1)
        {
            if (Num4 - Num1 == 1)
            {
                if (Num1 - Num2 == 1 && Num2 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num1 - Num3 == 1 && Num3 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num2 == 1)
            {
                if (Num2 - Num1 == 1 && Num1 - Num3 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num2 - Num3 == 1 && Num3 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
            else if (Num4 - Num3 == 1)
            {
                if (Num3 - Num1 == 1 && Num1 - Num2 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
                else if (Num3 - Num2 == 1 && Num2 - Num1 == 1)
                {
                    pairPanelText.text = "ストレート";
                    isStrait = true;
                }
            }
        }

        if (isStrait == true)
        {
            strength = 5;
        }

        if (Suit1 == Suit2 && Suit2 == Suit3 && Suit3 == Suit4 && Suit4 == Suit5)
        {
            pairPanelText.text = "フラッシュ";
            isFlush = true;
            strength = 6;
        }

        if (isFlush == true)//ここからロイヤルストレートフラッシュの判定
        {
            switch (Num1)
            {
                case 10:
                    switch (Num2)
                    {
                        case 11:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 12:
                            switch (Num3)
                            {
                                case 11:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 13:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 11:
                    switch (Num2)
                    {
                        case 10:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 12:
                            switch (Num3)
                            {
                                case 10:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 13:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 12:
                    switch (Num2)
                    {
                        case 11:
                            switch (Num3)
                            {
                                case 10:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 10:
                            switch (Num3)
                            {
                                case 11:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 13:
                            switch (Num3)
                            {
                                case 10:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (Num3)
                            {
                                case 10:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 13:
                    switch (Num2)
                    {
                        case 11:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 12:
                            switch (Num3)
                            {
                                case 11:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 10:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 1)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 1:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 1:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 1:
                    switch (Num2)
                    {
                        case 11:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 12:
                            switch (Num3)
                            {
                                case 11:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 13:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 11:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 10)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 10:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 10:
                            switch (Num3)
                            {
                                case 12:
                                    switch (Num4)
                                    {
                                        case 13:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 13:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 11)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 11:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                                case 11:
                                    switch (Num4)
                                    {
                                        case 12:
                                            if (Num5 == 13)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                        case 13:
                                            if (Num5 == 12)
                                            {
                                                pairPanelText.text = "ロイヤルストレートフラッシュ";
                                                strength = 10;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
        if (isStrait == true && isFlush == true && strength != 10)
        {
            pairPanelText.text = "ストレートフラッシュ";
            strength = 9;
        }
        else if ((Num1 == Num2 && Num2 == Num3 && Num3 == Num4) || (Num1 == Num2 && Num2 == Num3 && Num3 == Num5) || (Num1 == Num2 && Num2 == Num4 && Num4 == Num5) || (Num1 == Num3 && Num3 == Num4 && Num4 == Num5) || (Num2 == Num3 && Num3 == Num4 && Num4 == Num5))
        {
            pairPanelText.text = "フォーカード";
            strength = 8;
        }
        else if ((Num1 == Num2 && Num3 == Num4 && Num4 == Num5) || (Num1 == Num3 && Num2 == Num4 && Num4 == Num5) || (Num1 == Num4 && Num2 == Num3 && Num3 == Num5) || (Num1 == Num5 && Num2 == Num3 && Num3 == Num4) || (Num2 == Num3 && Num1 == Num4 && Num4 == Num5) || (Num2 == Num4 && Num1 == Num3 && Num3 == Num5) || (Num2 == Num5 && Num1 == Num3 && Num3 == Num4) || (Num3 == Num4 && Num1 == Num2 && Num2 == Num5) || (Num3 == Num5 && Num1 == Num2 && Num2 == Num4) || (Num4 == Num5 && Num1 == Num2 && Num2 == Num3))
        {
            pairPanelText.text = "フルハウス";
            strength = 7;
        }
        else if (isStrait == true || isFlush == true)
        {

        }
        else if ((Num1 == Num2 && Num2 == Num3) || (Num1 == Num2 && Num2 == Num4) || (Num1 == Num2 && Num2 == Num5) || (Num1 == Num3 && Num3 == Num4) || (Num1 == Num3 && Num3 == Num5) || (Num1 == Num4 && Num4 == Num5) || (Num2 == Num3 && Num3 == Num4) || (Num2 == Num3 && Num3 == Num5) || (Num2 == Num4 && Num4 == Num5) || (Num3 == Num4 && Num4 == Num5))
        {
            pairPanelText.text = "スリーカード";
            strength = 4;
        }
        else if (Num1 == Num2 && Num3 == Num4 || Num1 == Num2 && Num3 == Num5 || Num1 == Num2 && Num4 == Num5 || Num1 == Num3 && Num2 == Num4 || Num1 == Num3 && Num2 == Num5 || Num1 == Num3 && Num4 == Num5 || Num1 == Num4 && Num2 == Num3 || Num1 == Num4 && Num2 == Num5 || Num1 == Num4 && Num3 == Num5 || Num1 == Num5 && Num2 == Num3 || Num1 == Num5 && Num2 == Num4 || Num1 == Num5 && Num3 == Num4 || Num2 == Num3 && Num4 == Num5 || Num2 == Num4 && Num3 == Num5 || Num2 == Num5 && Num3 == Num4)
        {
            pairPanelText.text = "ツーペア";
            strength = 3;
        }
        else if (Num1 == Num2 || Num1 == Num3 || Num1 == Num4 || Num1 == Num5 || Num2 == Num3 || Num2 == Num4 || Num2 == Num5 || Num3 == Num4 || Num3 == Num5 || Num4 == Num5)
        {
            pairPanelText.text = "ワンペア";
            strength = 2;
        }
        else
        {
            pairPanelText.text = "役なし";
            strength = 1;
        }

        if (Num1 == Num2 && Num2 == Num3 && Num3 == Num4 && Num4 == Num5)
        {
            pairPanelText.text = "ファイブカード";
            strength = 11;
        }

        StartCoroutine("HukidasiErase");
    }

    IEnumerator HukidasiErase()
    {
        yield return new WaitForSeconds(1.5f);
        pairpanel.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            card1 = GameObject.Find("firstCard");
            CardScript cardScript1 = card1.GetComponent<CardScript>();
            cardScript1.OnClickStartButton();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            card2 = GameObject.Find("secondCard");
            CardScript cardScript2 = card2.GetComponent<CardScript>();
            cardScript2.OnClickStartButton();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            card3 = GameObject.Find("thirdCard");
            CardScript cardScript3 = card3.GetComponent<CardScript>();
            cardScript3.OnClickStartButton();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            card4 = GameObject.Find("fourthCard");
            CardScript cardScript4 = card4.GetComponent<CardScript>();
            cardScript4.OnClickStartButton();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            card5 = GameObject.Find("fifthCard");
            CardScript cardScript5 = card5.GetComponent<CardScript>();
            cardScript5.OnClickStartButton();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (started == false)
            {
                OnClickStartButton();
            }
            else if (PlayButton.activeSelf == true)
            {
                OnClickPlayButton();
            }
            else
            {
                OnClickExchangeButton();
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            if (started == true)
            {
                OnClickSTButton();
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (PlayButton.activeSelf == true)
            {
                betController.OnClickUpButton();
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (PlayButton.activeSelf == true)
            {
                betController.OnClickDownButton();
            }
        }

        colorChange();
        ExchangeRemind();
    }

    public void colorChange()
    {
        float sin = Mathf.Abs(Mathf.Sin(Time.time));
        pushThis.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
    }

    public void ExchangeRemind()
    {
        float sin = Mathf.Abs(Mathf.Sin(3 * Time.time));
        ExchangeReminder1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
        ExchangeReminder2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
        ExchangeReminder3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
        ExchangeReminder4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
        ExchangeReminder5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f * sin);
    }

    private void UnearnedIncome1()
    {
        money += 1000000;
        moneyFigure.text = "" + money;

        if (money >= 1000000000000000)
        {
            moneyComma.text = ",      ,      ,       ,      ,       ";
        }
        else if (money >= 1000000000000)
        {
            moneyComma.text = ",      ,       ,      ,       ";
        }
        else if (money >= 1000000000)
        {
            moneyComma.text = ",       ,      ,       ";
        }
        else if (money >= 1000000)
        {
            moneyComma.text = ",      ,       ";
        }
        else if (money >= 1000)
        {
            moneyComma.text = ",       ";
        }
        else
        {
            moneyComma.text = "";
        }
    }

    private void UnearnedIncome2()
    {
        money += 10000000000;
        moneyFigure.text = "" + money;

        if (money >= 1000000000000000)
        {
            moneyComma.text = ",      ,      ,       ,      ,       ";
        }
        else if (money >= 1000000000000)
        {
            moneyComma.text = ",      ,       ,      ,       ";
        }
        else if (money >= 1000000000)
        {
            moneyComma.text = ",       ,      ,       ";
        }
        else if (money >= 1000000)
        {
            moneyComma.text = ",      ,       ";
        }
        else if (money >= 1000)
        {
            moneyComma.text = ",       ";
        }
        else
        {
            moneyComma.text = "";
        }
    }

    public void betButtonMovement()
    {
        StartCoroutine("bBM");
    }

    IEnumerator bBM()
    {
        for (int i = 1; i < 7; i++)
        {
            if (i <= 3)
            {
                betUpButton.transform.localPosition += new Vector3(0, 1, 0);
                betDownButton.transform.localPosition -= new Vector3(0, 1, 0);
            }
            else
            {
                betUpButton.transform.localPosition -= new Vector3(0, 1, 0);
                betDownButton.transform.localPosition += new Vector3(0, 1, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void gameclear()
    {
        fanfare.GetComponent<AudioSource>().Play();
        PlayButton.SetActive(false);
        ExchangeButton.SetActive(false);
        DUButton.SetActive(false);
        gameClearPanel.SetActive(true);
        clearTime = (Time.time - startTime);
        clearTime = float.Parse(clearTime.ToString("n2"));
        Debug.Log("クリアタイムは" + clearTime);
        cleared = true;
        CancelInvoke();
        toRankingButton.SetActive(true);
    }

    public void OnClickStartButton()
    {
        if (started == false)
        {
            startTime = Time.time;

            InvokeRepeating("timeCounter", 0f, 0.01f);
        }

        choose.GetComponent<AudioSource>().Play();
        StartButton.SetActive(false);
        StartBG.SetActive(false);
        started = true;
        SceneTransButton.SetActive(true);
        PlayButton.SetActive(true);
        betUpButton.SetActive(true);
        betDownButton.SetActive(true);
        G1.SetActive(true);
        G2.SetActive(true);
        InvokeRepeating("betButtonMovement", 1.8f, 1.8f);
    }

    public void OnClickRankingButton()
    {
        choose.GetComponent<AudioSource>().Play();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(clearTime);
    }

    public void timeCounter()
    {
        timePast.text = "" + (Time.time - startTime).ToString("n2");
    }

    IEnumerator messageMillion()
    {
        for (int m = 0; m < 70; m++)
        {
            yield return new WaitForSeconds(0.03f);
            float x = Mathf.Pow(2, m);
            million.transform.localPosition = new Vector3(-780 + 780 * ((x - 1) / x), 540, -1);
        }
        StartCoroutine("messageMillion2");
    }
    IEnumerator messageMillion2()
    {
        for (int m = 0; m < 70; m++)
        {
            yield return new WaitForSeconds(0.03f);
            float x = Mathf.Pow(2, m);
            million.transform.localPosition = new Vector3(780 * ((x - 1) / x), 540, -1);
        }
    }
    IEnumerator messageTrillion()
    {
        for (int m = 0; m < 70; m++)
        {
            yield return new WaitForSeconds(0.03f);
            float x = Mathf.Pow(2, m);
            trillion.transform.localPosition = new Vector3(-780 + 780 * ((x - 1) / x), 540, -1);
        }
        StartCoroutine("messageTrillion2");
    }
    IEnumerator messageTrillion2()
    {
        for (int m = 0; m < 70; m++)
        {
            yield return new WaitForSeconds(0.03f);
            float x = Mathf.Pow(2, m);
            trillion.transform.localPosition = new Vector3(780 * ((x - 1) / x), 540, -1);
        }
    }

    public long GetPointDigit(long money, long digit)
    {
        return (long)(money / Mathf.Pow(10, digit - 1)) % 10;
    }

    public void nokori()
    {
        C1.SetActive(false);
        C2.SetActive(false);
        C3.SetActive(false);
        C4.SetActive(false);
        C5.SetActive(false);
        C6.SetActive(false);
        C7.SetActive(false);
        C8.SetActive(false);
        C9.SetActive(false);
        C0.SetActive(false);
        D1.SetActive(false);
        D2.SetActive(false);
        D3.SetActive(false);
        D4.SetActive(false);
        D5.SetActive(false);
        D6.SetActive(false);
        D7.SetActive(false);
        D8.SetActive(false);
        D9.SetActive(false);
        D0.SetActive(false);
        E1.SetActive(false);
        E2.SetActive(false);
        E3.SetActive(false);
        E4.SetActive(false);
        E5.SetActive(false);
        E6.SetActive(false);
        E7.SetActive(false);
        E8.SetActive(false);
        E9.SetActive(false);
        E0.SetActive(false);
        F1.SetActive(false);
        F2.SetActive(false);
        F3.SetActive(false);
        F4.SetActive(false);
        F5.SetActive(false);
        F6.SetActive(false);
        F7.SetActive(false);
        F8.SetActive(false);
        F9.SetActive(false);
        F0.SetActive(false);

        long ein = GetPointDigit(money, 13);
        long zwei = GetPointDigit(money, 14);
        long drei = GetPointDigit(money, 15);
        long fier = GetPointDigit(money, 16);
        nokoriIndicater(fier, 4);
        nokoriIndicater(drei, 3);
        nokoriIndicater(zwei, 2);
        nokoriIndicater(ein, 1);        
    }
    public void nokoriIndicater(long i, int keta)
    {
        if (keta == 4)
        {
            long x = 4 - i;

            switch (x)
            {
                case 1 :
                    C1.SetActive(true);
                    break;
                case 2:
                    C2.SetActive(true);
                    break;
                case 3:
                    C3.SetActive(true);
                    break;
                case 4:
                    C4.SetActive(true);
                    break;
                case 5:
                    C5.SetActive(true);
                    break;
                case 6:
                    C6.SetActive(true);
                    break;
                case 7:
                    C7.SetActive(true);
                    break;
                case 8:
                    C8.SetActive(true);
                    break;
                case 9:
                    C9.SetActive(true);
                    break;
                case 0:
                    C0.SetActive(true);
                    break;
                default:
                    end = true;
                    break;
            }
        }
        if (keta == 3)
        {
            long x = 9 - i;

            if (end == false)
            {
                switch (x)
                {
                    case 1:
                        D1.SetActive(true);
                        break;
                    case 2:
                        D2.SetActive(true);
                        break;
                    case 3:
                        D3.SetActive(true);
                        break;
                    case 4:
                        D4.SetActive(true);
                        break;
                    case 5:
                        D5.SetActive(true);
                        break;
                    case 6:
                        D6.SetActive(true);
                        break;
                    case 7:
                        D7.SetActive(true);
                        break;
                    case 8:
                        D8.SetActive(true);
                        break;
                    case 9:
                        D9.SetActive(true);
                        break;
                    case 0:
                        D0.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        if (keta == 2)
        {
            long x = 9 - i;
            if (end == false)
            {
                switch (x)
                {
                    case 1:
                        E1.SetActive(true);
                        break;
                    case 2:
                        E2.SetActive(true);
                        break;
                    case 3:
                        E3.SetActive(true);
                        break;
                    case 4:
                        E4.SetActive(true);
                        break;
                    case 5:
                        E5.SetActive(true);
                        break;
                    case 6:
                        E6.SetActive(true);
                        break;
                    case 7:
                        E7.SetActive(true);
                        break;
                    case 8:
                        E8.SetActive(true);
                        break;
                    case 9:
                        E9.SetActive(true);
                        break;
                    case 0:
                        E0.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        if (keta == 1)
        {
            long x = 9 - i;
            if (end == false)
            {
                switch (x)
                {
                    case 1:
                        F1.SetActive(true);
                        break;
                    case 2:
                        F2.SetActive(true);
                        break;
                    case 3:
                        F3.SetActive(true);
                        break;
                    case 4:
                        F4.SetActive(true);
                        break;
                    case 5:
                        F5.SetActive(true);
                        break;
                    case 6:
                        F6.SetActive(true);
                        break;
                    case 7:
                        F7.SetActive(true);
                        break;
                    case 8:
                        F8.SetActive(true);
                        break;
                    case 9:
                        F9.SetActive(true);
                        break;
                    case 0:
                        F0.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void DealerAnimation()
    {
        int Num = Random.Range(1, 100);
        if (Num > DealerPos)
        {
            DealerPos = 90;
            if (smiling == false && sad == false)
            {
                StartCoroutine("DealerAnim");
            }
        }
        else
        {
            DealerPos -= 5;
        }
    }

    IEnumerator DealerAnim()
    {
        StopCoroutine("Smiling");
        StopCoroutine("Sad");
        pic1.SetActive(false);
        pic4.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic4.SetActive(false);
        pic1to2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic1to2.SetActive(false);
        pic5.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic5.SetActive(false);
        pic2.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        pic2.SetActive(false);
        pic5.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic5.SetActive(false);
        pic1to2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic1to2.SetActive(false);
        pic4.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        pic4.SetActive(false);
        pic1.SetActive(true);
    }
    IEnumerator Smiling()
    {
        StopCoroutine("DealerAnim");
        pic1.SetActive(false);
        pic4.SetActive(false);
        pic1to2.SetActive(false);
        pic5.SetActive(false);
        pic2.SetActive(false);
        picSmile.SetActive(true);
        smiling = true;
        yield return new WaitForSeconds(2.0f);
        picSmile.SetActive(false);
        pic1.SetActive(true);
        smiling = false;
    }
    IEnumerator Sad()
    {
        StopCoroutine("DealerAnim");
        pic1.SetActive(false);
        pic4.SetActive(false);
        pic1to2.SetActive(false);
        pic5.SetActive(false);
        pic2.SetActive(false);
        picSad.SetActive(true);
        sad = true;
        yield return new WaitForSeconds(2.0f);
        picSad.SetActive(false);
        pic1.SetActive(true);
        sad = false;
    }
}