using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    /// <summary>
    /// ƒXƒe[ƒWƒNƒŠƒA‚É•ñV‚ğ•\¦‚³‚¹‚éƒXƒNƒŠƒvƒg
    /// </summary>
    public GameObject gameManager;

    int rewardSelecter1;
    int rewardSelecter2;
    int rewardSelecter3;

    public int reward1UnitID;
    public int reward2UnitID;
    public int reward3UnitID;

    public Text text11;
    public Text text12;
    public Text text21;
    public Text text22;
    public Text text31;
    public Text text32;

    public static int[] notPossess = { 2, 3, 4, 5, 6, 7 }; //‚±‚±‚©‚çŠùŠ‚Ì”Ô†‚ğŠO‚µ‚½‚¢

    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;

    public GameObject reward1;
    public GameObject reward2;
    public GameObject reward3;

    // Start is called before the first frame update
    void Start()
    {        
        RewardManaging();
    }

    public void RewardManaging ()
    {
        bool Unit2Showcase = false;
        bool Unit3Showcase = false;
        bool Unit4Showcase = false;
        bool Unit5Showcase = false;
        bool Unit6Showcase = false;
        bool Unit7Showcase = false;

        GameObject obj1 = (GameObject)Resources.Load("Card");
        GameObject obj2 = (GameObject)Resources.Load("Card2");
        GameObject obj3 = (GameObject)Resources.Load("Card3");
        GameObject obj4 = (GameObject)Resources.Load("Card4");
        GameObject obj5 = (GameObject)Resources.Load("Card5");
        GameObject obj6 = (GameObject)Resources.Load("Card6");
        GameObject obj7 = (GameObject)Resources.Load("Card7");

        image2.transform.SetParent(gameManager.transform, false);
        image2.transform.localPosition = new Vector3(0, 0, 0);
        image3.transform.SetParent(gameManager.transform, false);
        image3.transform.localPosition = new Vector3(0, 0, 0);
        image4.transform.SetParent(gameManager.transform, false);
        image4.transform.localPosition = new Vector3(0, 0, 0);
        image5.transform.SetParent(gameManager.transform, false);
        image5.transform.localPosition = new Vector3(0, 0, 0);
        image6.transform.SetParent(gameManager.transform, false);
        image6.transform.localPosition = new Vector3(0, 0, 0);
        image7.transform.SetParent(gameManager.transform, false);
        image7.transform.localPosition = new Vector3(0, 0, 0);

        if (StageCounter.possession2 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(2);
            notPossess = PList.ToArray();
        }

        if (StageCounter.possession3 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(3);
            notPossess = PList.ToArray();
        }

        if (StageCounter.possession4 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(4);
            notPossess = PList.ToArray();
        }

        if (StageCounter.possession5 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(5);
            notPossess = PList.ToArray();
        }

        if (StageCounter.possession6 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(6);
            notPossess = PList.ToArray();
        }

        if (StageCounter.possession7 == true)
        {
            List<int> PList = new List<int>();
            PList.AddRange(notPossess);
            PList.Remove(7);
            notPossess = PList.ToArray();
        }
        
        rewardSelecter1 = Random.Range(2, 8);
        Debug.Log("rewardSelecter1‚Í" + rewardSelecter1);
        rewardSelecter2 = Random.Range(2, 8);
        rewardSelecter3 = Random.Range(2, 8);

    while (rewardSelecter2 == rewardSelecter1)
        {
            rewardSelecter2 = Random.Range(2, 8);
        }
        while (rewardSelecter3 == rewardSelecter1 || rewardSelecter3 == rewardSelecter2)
        {
            rewardSelecter3 = Random.Range(2, 8);
        }

        switch (rewardSelecter1)
        {
            case 2:
                Unit2Showcase = true;
                image2.transform.SetParent(reward1.transform, false);
                image2.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 2;
                text11.text = "Bat";
                text12.text = "HP:¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚Q‚ğ•ñV‚P‚Éİ’è");
                break;
            case 3:
                Unit3Showcase = true;
                image3.transform.SetParent(reward1.transform, false);
                image3.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 3;
                text11.text = "Archer";
                text12.text = "HP:¡¡\nAttack:¡¡\n–î‚ÅUŒ‚";
                Debug.Log("ƒ†ƒjƒbƒg‚R‚ğ•ñV‚P‚Éİ’è");
                break;
            case 4:
                Unit4Showcase = true;
                image4.transform.SetParent(reward1.transform, false);
                image4.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 4;
                text11.text = "Guardian";
                text12.text = "HP:¡¡¡¡¡\nAttack:¡¡\né‚ğç‚é";
                Debug.Log("ƒ†ƒjƒbƒg‚S‚ğ•ñV‚P‚Éİ’è");
                break;
            case 5:
                Unit5Showcase = true;
                image5.transform.SetParent(reward1.transform, false);
                image5.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 5;
                text11.text = "Titan";
                text12.text = "HP:¡¡¡¡\nAttack:¡¡¡\nSpeed:¡";
                Debug.Log("ƒ†ƒjƒbƒg‚T‚ğ•ñV‚P‚Éİ’è");
                break;
            case 6:
                Unit6Showcase = true;
                image6.transform.SetParent(reward1.transform, false);
                image6.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 6;
                text11.text = "Dragoon";
                text12.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚U‚ğ•ñV‚P‚Éİ’è");
                break;
            case 7:
                Unit7Showcase = true;
                image7.transform.SetParent(reward1.transform, false);
                image7.transform.localPosition = new Vector3(0, 80, 0);
                reward1UnitID = 7;
                text11.text = "Pikachu";
                text12.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚V‚ğ•ñV‚P‚Éİ’è");
                break;
        }

        switch (rewardSelecter2)
        {
            case 2:
                Unit2Showcase = true;
                image2.transform.SetParent(reward2.transform, false);
                image2.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 2;
                text21.text = "Bat";
                text22.text = "HP:¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚Q‚ğ•ñV‚Q‚Éİ’è");
                break;
            case 3:
                Unit3Showcase = true;
                image3.transform.SetParent(reward2.transform, false);
                image3.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 3;
                text21.text = "Archer";
                text22.text = "HP:¡¡\nAttack:¡¡\n–î‚ÅUŒ‚";
                Debug.Log("ƒ†ƒjƒbƒg‚R‚ğ•ñV‚Q‚Éİ’è");
                break;
            case 4:
                Unit4Showcase = true;
                image4.transform.SetParent(reward2.transform, false);
                image4.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 4;
                text21.text = "Guardian";
                text22.text = "HP:¡¡¡¡¡\nAttack:¡¡\né‚ğç‚é";
                Debug.Log("ƒ†ƒjƒbƒg‚S‚ğ•ñV‚Q‚Éİ’è");
                break;
            case 5:
                Unit5Showcase = true;
                image5.transform.SetParent(reward2.transform, false);
                image5.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 5;
                text21.text = "Titan";
                text22.text = "HP:¡¡¡¡\nAttack:¡¡¡\nSpeed:¡";
                Debug.Log("ƒ†ƒjƒbƒg‚T‚ğ•ñV‚Q‚Éİ’è");
                break;
            case 6:
                Unit6Showcase = true;
                image6.transform.SetParent(reward2.transform, false);
                image6.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 6;
                text21.text = "Dragoon";
                text22.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚U‚ğ•ñV‚Q‚Éİ’è");
                break;
            case 7:
                Unit7Showcase = true;
                image7.transform.SetParent(reward2.transform, false);
                image7.transform.localPosition = new Vector3(0, 80, 0);
                reward2UnitID = 7;
                text21.text = "Pikachu";
                text22.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚V‚ğ•ñV‚Q‚Éİ’è");
                break;
        }

        switch (rewardSelecter3)
        {
            case 2:
                Unit2Showcase = true;
                image2.transform.SetParent(reward3.transform, false);
                image2.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 2;
                text31.text = "Bat";
                text32.text = "HP:¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚Q‚ğ•ñV‚R‚Éİ’è");
                break;
            case 3:
                Unit3Showcase = true;
                image3.transform.SetParent(reward3.transform, false);
                image3.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 3;
                text31.text = "Archer";
                text32.text = "HP:¡¡\nAttack:¡¡\n–î‚ÅUŒ‚";
                Debug.Log("ƒ†ƒjƒbƒg‚R‚ğ•ñV‚R‚Éİ’è");
                break;
            case 4:
                Unit4Showcase = true;
                image4.transform.SetParent(reward3.transform, false);
                image4.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 4;
                text31.text = "Guardian";
                text32.text = "HP:¡¡¡¡¡\nAttack:¡¡\né‚ğç‚é";
                Debug.Log("ƒ†ƒjƒbƒg‚S‚ğ•ñV‚R‚Éİ’è");
                break;
            case 5:
                Unit5Showcase = true;
                image5.transform.SetParent(reward3.transform, false);
                image5.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 5;
                text31.text = "Titan";
                text32.text = "HP:¡¡¡¡\nAttack:¡¡¡\nSpeed:¡";
                Debug.Log("ƒ†ƒjƒbƒg‚T‚ğ•ñV‚R‚Éİ’è");
                break;
            case 6:
                Unit6Showcase = true;
                image6.transform.SetParent(reward3.transform, false);
                image6.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 6;
                text31.text = "Dragoon";
                text32.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚U‚ğ•ñV‚R‚Éİ’è");
                break;
            case 7:
                Unit7Showcase = true;
                image7.transform.SetParent(reward3.transform, false);
                image7.transform.localPosition = new Vector3(0, 80, 0);
                reward3UnitID = 7;
                text31.text = "Pikachu";
                text32.text = "HP:¡¡\nAttack:¡¡\nSpeed:¡¡¡";
                Debug.Log("ƒ†ƒjƒbƒg‚V‚ğ•ñV‚R‚Éİ’è");
                break;
        }

        if (StageCounter.possession2 == true && Unit2Showcase == true)
        {
            RewardHelp();
        }
        else if (StageCounter.possession3 == true && Unit3Showcase == true)
        {
            RewardHelp();
        }
        else if (StageCounter.possession4 == true && Unit4Showcase == true)
        {
            RewardHelp();
        }
        else if (StageCounter.possession5 == true && Unit5Showcase == true)
        {
            RewardHelp();
        }
        else if (StageCounter.possession6 == true && Unit6Showcase == true)
        {
            RewardHelp();
        }
        else if (StageCounter.possession7 == true && Unit7Showcase == true)
        {
            RewardHelp();
        }
    }

    public void RewardHelp()
    {
        RewardManaging();
    }

    void Update()
    {
        
    }
}
