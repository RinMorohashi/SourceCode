using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckAligner : MonoBehaviour
{
    /// <summary>
    /// ステージ開始時にデッキを並べるスクリプト
    /// </summary>
    GameObject FirstUnit;
    UnitMove unitMove1;
    GameObject SecondUnit;
    UnitMove unitMove2;
    GameObject ThirdUnit;
    UnitMove unitMove3;
    GameObject FourthUnit;
    UnitMove unitMove4;
    GameObject FifthUnit;
    UnitMove unitMove5;

    void Start()
    {
        FirstUnit = transform.Find("Card").gameObject;
        FirstUnit.transform.localPosition = new Vector3(-180, 0, 0);
        unitMove1 = FirstUnit.GetComponent<UnitMove>();
        unitMove1.deckPosition = -180;

        //デッキナンバーが１のユニットを探してFirstUnitに登録する→FirstUnitの位置を調整する。
        switch (StageCounter.deckNumber2)
        {
            case 2:
                SecondUnit = GameObject.Find("Card2");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card2");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card2");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card2");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;
        }

        switch (StageCounter.deckNumber3)
        {
            case 2:
                SecondUnit = GameObject.Find("Card3");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card3");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card3");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card3");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;

        }

        switch (StageCounter.deckNumber4)
        {
            case 2:
                SecondUnit = GameObject.Find("Card4");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card4");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card4");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card4");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;

        }

        switch (StageCounter.deckNumber5)
        {
            case 2:
                SecondUnit = GameObject.Find("Card5");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card5");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card5");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card5");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;

        }

        switch (StageCounter.deckNumber6)
        {
            case 2:
                SecondUnit = GameObject.Find("Card6");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card6");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card6");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card6");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;

        }

        switch (StageCounter.deckNumber7)
        {
            case 2:
                SecondUnit = GameObject.Find("Card7");
                SecondUnit.transform.localPosition = new Vector3(-90, 0, 0);
                unitMove2 = SecondUnit.GetComponent<UnitMove>();
                unitMove2.deckPosition = -90;
                break;
            case 3:
                ThirdUnit = GameObject.Find("Card7");
                ThirdUnit.transform.localPosition = new Vector3(0, 0, 0);
                unitMove3 = ThirdUnit.GetComponent<UnitMove>();
                unitMove3.deckPosition = 0;
                break;
            case 4:
                FourthUnit = GameObject.Find("Card7");
                FourthUnit.transform.localPosition = new Vector3(90, 0, 0);
                unitMove4 = FourthUnit.GetComponent<UnitMove>();
                unitMove4.deckPosition = 90;
                break;
            case 5:
                FifthUnit = GameObject.Find("Card7");
                FifthUnit.transform.localPosition = new Vector3(180, 0, 0);
                unitMove5 = FifthUnit.GetComponent<UnitMove>();
                unitMove5.deckPosition = 180;
                break;

        }
    }
}
