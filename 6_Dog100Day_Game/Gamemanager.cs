using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    /// <summary>
    /// フィールド画面でのマスの生成や背景のスライドを行うスクリプト
    /// </summary>
    public float timer;
    public int days;
    [Header("日数表示")] public Text daysDisplay;
    private bool recast;
    public bool Untouchable = false;//演出の間はオンにして、オフになったら操作可能になる
    public Image blackBG;

    public GameObject unit1;//一番右のマス
    public GameObject unit2;
    public GameObject unit3;
    public GameObject unit4;
    public GameObject unit5;
    public GameObject unit6;
    public GameObject unit7;//一番左のマス
    public GameObject unit8;
    public GameObject unit9;
    public GameObject unit10;//一番左のマス
    public GameObject unit11;
    public GameObject unit12;
    public UnitMove unit1Script;
    public UnitMove unit2Script;
    public UnitMove unit3Script;
    public UnitMove unit4Script;
    public UnitMove unit5Script;
    public UnitMove unit6Script;
    public UnitMove unit7Script;
    public UnitMove unit8Script;
    public UnitMove unit9Script;
    public UnitMove unit10Script;
    public UnitMove unit11Script;
    public UnitMove unit12Script;
    public GameObject unit1Line2;//一番右のマス
    public GameObject unit2Line2;
    public GameObject unit3Line2;
    public GameObject unit4Line2;
    public GameObject unit5Line2;
    public GameObject unit6Line2;
    public GameObject unit7Line2;//一番左のマス
    public GameObject unit8Line2;
    public GameObject unit9Line2;
    public GameObject unit10Line2;//一番左のマス
    public GameObject unit11Line2;
    public GameObject unit12Line2;
    public UnitMove unit1ScriptLine2;
    public UnitMove unit2ScriptLine2;
    public UnitMove unit3ScriptLine2;
    public UnitMove unit4ScriptLine2;
    public UnitMove unit5ScriptLine2;
    public UnitMove unit6ScriptLine2;
    public UnitMove unit7ScriptLine2;
    public UnitMove unit8ScriptLine2;
    public UnitMove unit9ScriptLine2;
    public UnitMove unit10ScriptLine2;
    public UnitMove unit11ScriptLine2;
    public UnitMove unit12ScriptLine2;
    public GameObject unit1Line3;//一番右のマス
    public GameObject unit2Line3;
    public GameObject unit3Line3;
    public GameObject unit4Line3;
    public GameObject unit5Line3;
    public GameObject unit6Line3;
    public GameObject unit7Line3;//一番左のマス
    public GameObject unit8Line3;
    public GameObject unit9Line3;
    public GameObject unit10Line3;//一番左のマス
    public GameObject unit11Line3;
    public GameObject unit12Line3;
    public UnitMove unit1ScriptLine3;
    public UnitMove unit2ScriptLine3;
    public UnitMove unit3ScriptLine3;
    public UnitMove unit4ScriptLine3;
    public UnitMove unit5ScriptLine3;
    public UnitMove unit6ScriptLine3;
    public UnitMove unit7ScriptLine3;
    public UnitMove unit8ScriptLine3;
    public UnitMove unit9ScriptLine3;
    public UnitMove unit10ScriptLine3;
    public UnitMove unit11ScriptLine3;
    public UnitMove unit12ScriptLine3;

    public GameObject unit;//マスのプレハブをここに入れる
    public GameObject unitLine2;
    public GameObject unitLine3;
    public GameObject cvs;//キャンバス
    [Header("プレイヤースクリプト")] public PlayerScript playerScript;
    public int unitID;
    public int enemyID;
    public bool isBattle;
    public bool isGameOver;

    [Header("キャンバス１背景画像")] public RectTransform Backgroundcvs1;
    [Header("キャンバス１背景画像2")] public RectTransform Backgroundcvs2;
    [Header("キャンバス１背景オブジェクト")] public GameObject Backgroundobj1;
    [Header("キャンバス１背景オブジェクト2")] public GameObject Backgroundobj2;

    [SerializeField] float mainColor = 0.75f;
    [SerializeField] float subColor = 0.25f;

    public GameObject unitLine1Parent;
    public GameObject unitLine2Parent;
    public GameObject unitLine3Parent;
    public bool isJobChangeMode;

    void Start()
    {
        unit = (GameObject)Resources.Load("unit");
        unitLine2 = (GameObject)Resources.Load("unitLine2");
        unitLine3 = (GameObject)Resources.Load("unitLine3");
        StartCoroutine("blackOutOut");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!Untouchable && !isGameOver && !playerScript.isClear)
        {
            if (!isBattle)
            {
                if (Input.GetKey(KeyCode.RightArrow) && !recast && !isJobChangeMode)
                {
                    recast = true;
                        days++;
                        daysDisplay.text = days + "日目";

                        playerScript.StartCoroutine("jump");

                        massProduction();

                    StartCoroutine("Recast");
                }
                if (Input.GetKey(KeyCode.UpArrow) && !recast && days >= 9 && !isJobChangeMode)
                {
                        switch (playerScript.currentLine)
                        {
                            case 1:
                                playerScript.currentLine = 2;
                                recast = true;
                                days++;
                                daysDisplay.text = days + "日目";

                                playerScript.StartCoroutine("jump");

                                massProduction();

                                StartCoroutine("Recast");
                                break;
                            case 2:
                                playerScript.currentLine = 3;
                                recast = true;
                                days++;
                                daysDisplay.text = days + "日目";

                                playerScript.StartCoroutine("jump");

                                massProduction();

                                StartCoroutine("Recast");
                                break;
                            case 3:
                                break;
                            default:
                                break;
                        }
                }
                if (Input.GetKey(KeyCode.DownArrow) && !recast)
                {
                    if (!isJobChangeMode)
                    {
                        switch (playerScript.currentLine)
                        {
                            case 1:

                                break;
                            case 2:
                                playerScript.currentLine = 1;
                                recast = true;
                                days++;
                                daysDisplay.text = days + "日目";

                                playerScript.StartCoroutine("jump");

                                massProduction();

                                StartCoroutine("Recast");
                                break;
                            case 3:
                                playerScript.currentLine = 2;
                                recast = true;
                                days++;
                                daysDisplay.text = days + "日目";

                                playerScript.StartCoroutine("jump");

                                massProduction();

                                StartCoroutine("Recast");
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && !recast && isJobChangeMode)
                {
                    //ジョブ選択
                    playerScript.StartCoroutine("templePushRight");
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !recast && isJobChangeMode)
                {
                    //ジョブ選択
                    playerScript.StartCoroutine("templePushLeft");
                }
                if (Input.GetKeyDown(KeyCode.Z) && !recast && isJobChangeMode)
                {
                    //ジョブ決定
                    playerScript.StartCoroutine("templeJobDecide");
                }
            }
            else //バトル中の入力
            {
                if (Input.GetKey(KeyCode.RightArrow) && !recast)
                {
                    recast = true;
                    if (playerScript.currentStamina >= 1000)
                    {
                        switch (playerScript.battleCurrentPos)
                        {
                            case 1:
                                playerScript.battleCurrentPos = 2;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 2:
                                playerScript.battleCurrentPos = 3;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 4:
                                playerScript.battleCurrentPos = 5;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 5:
                                playerScript.battleCurrentPos = 6;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 7:
                                playerScript.battleCurrentPos = 8;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 8:
                                playerScript.battleCurrentPos = 9;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        playerScript.StartCoroutine("staminaShortage");
                    }
                    StartCoroutine("Recast");
                }
                if (Input.GetKey(KeyCode.UpArrow) && !recast)
                {
                    recast = true;
                    if (playerScript.currentStamina >= 1000)
                    {
                        switch (playerScript.battleCurrentPos)
                        {
                            case 1:
                                playerScript.battleCurrentPos = 4;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 2:
                                playerScript.battleCurrentPos = 5;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 3:
                                playerScript.battleCurrentPos = 6;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 4:
                                playerScript.battleCurrentPos = 7;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 5:
                                playerScript.battleCurrentPos = 8;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 6:
                                playerScript.battleCurrentPos = 9;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        playerScript.StartCoroutine("staminaShortage");
                    }
                    StartCoroutine("Recast");
                }
                if (Input.GetKey(KeyCode.DownArrow) && !recast)
                {
                    recast = true;

                    if (playerScript.currentStamina >= 1000)
                    {
                        switch (playerScript.battleCurrentPos)
                        {
                            case 4:
                                playerScript.battleCurrentPos = 1;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 5:
                                playerScript.battleCurrentPos = 2;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 6:
                                playerScript.battleCurrentPos = 3;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 7:
                                playerScript.battleCurrentPos = 4;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 8:
                                playerScript.battleCurrentPos = 5;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 9:
                                playerScript.battleCurrentPos = 6;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        playerScript.StartCoroutine("staminaShortage");
                    }
                    StartCoroutine("Recast");
                }
                if (Input.GetKey(KeyCode.LeftArrow) && !recast)
                {
                    recast = true;
                    if (playerScript.currentStamina >= 1000)
                    {
                        switch (playerScript.battleCurrentPos)
                        {
                            case 2:
                                playerScript.battleCurrentPos = 1;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 3:
                                playerScript.battleCurrentPos = 2;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 5:
                                playerScript.battleCurrentPos = 4;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 6:
                                playerScript.battleCurrentPos = 5;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 8:
                                playerScript.battleCurrentPos = 7;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            case 9:
                                playerScript.battleCurrentPos = 8;
                                playerScript.StartCoroutine("BattleMove");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        playerScript.StartCoroutine("staminaShortage");
                    }
                    StartCoroutine("Recast");
                }

                if (Input.GetKey(KeyCode.Z) && !recast) //攻撃
                {
                    if (playerScript.currentStamina >= 1000 && (playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9))
                    {
                        recast = true;
                        playerScript.StartCoroutine("Attack");
                        StartCoroutine("Recast");
                    }
                    else if (!(playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9))
                    {
                        playerScript.StartCoroutine("OutOfRange");
                    }
                    else
                    {
                        playerScript.StartCoroutine("staminaShortage");
                    }
                }
                if (Input.GetKey(KeyCode.X) && !recast) //ジョブコマンド
                {
                    switch (playerScript.currentJobID)
                    {
                        case 2://魔法使い
                            if (playerScript.currentStamina >= 2000)
                            {
                                recast = true;
                                playerScript.StartCoroutine("MagicAttack");
                                StartCoroutine("Recast");
                            }
                            else
                            {
                                playerScript.StartCoroutine("staminaShortage");
                            }
                            break;
                        case 3://僧侶
                            break;
                        default:
                            break;
                    }
                }
                if (Input.GetKey(KeyCode.C) && !recast) //待機
                {
                    recast = true;
                    playerScript.StartCoroutine("Rest");
                    StartCoroutine("Recast");
                }
            }
        }
        if (isGameOver)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                StartCoroutine("blackOut");
            }
            if (Input.GetKey(KeyCode.X))
            {
                StartCoroutine("blackOut2");
            }
        }
    }

    public void massProduction()
    {
        StartCoroutine("BackgroundScroll");
        unit1Script.StartCoroutine("unitMove");
        unit2Script.StartCoroutine("unitMove");
        unit3Script.StartCoroutine("unitMove");
        unit4Script.StartCoroutine("unitMove");
        unit5Script.StartCoroutine("unitMove");
        unit6Script.StartCoroutine("unitMove");
        unit7Script.StartCoroutine("unitMove");
        unit8Script.StartCoroutine("unitMove");
        unit9Script.StartCoroutine("unitMove");
        unit10Script.StartCoroutine("unitMove");
        unit11Script.StartCoroutine("unitMove");
        unit12Script.StartCoroutine("unitMove");

        unit12 = unit11;
        unit12Script = unit11Script;
        unit11 = unit10;
        unit11Script = unit10Script;
        unit10 = unit9;
        unit10Script = unit9Script;
        unit9 = unit8;
        unit9Script = unit8Script;
        if (playerScript.currentLine == 1)
        {
            unitID = unit9Script.unitID;
            if (unitID == 4)
            {
                enemyID = unit9Script.enemyID;
                Debug.Log("enemyIDは" + enemyID);
            }
        }
        unit8 = unit7;
        unit8Script = unit7Script;
        unit7 = unit6;
        unit7Script = unit6Script;
        unit6 = unit5;
        unit6Script = unit5Script;
        unit5 = unit4;
        unit5Script = unit4Script;
        unit4 = unit3;
        unit4Script = unit3Script;
        unit3 = unit2;
        unit3Script = unit2Script;

        unit2 = unit1;
        unit2Script = unit1Script;
        unit1 = Instantiate(unit, new Vector3(1, 0, 0), Quaternion.identity);
        unit1.transform.SetParent(cvs.transform);
        unit1.transform.localScale = new Vector3(1, 1, 1);
        unit1.GetComponent<RectTransform>().anchoredPosition = new Vector2(1080, -180);
        unit1Script = unit1.GetComponent<UnitMove>();
        unit1.transform.SetParent(unitLine1Parent.transform);

        if (days >= 2)
        {
            if (unit1ScriptLine2 != null)
            {
                unit1ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit2ScriptLine2 != null)
            {
                unit2ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit3ScriptLine2 != null)
            {
                unit3ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit4ScriptLine2 != null)
            {
                unit4ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit5ScriptLine2 != null)
            {
                unit5ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit6ScriptLine2 != null)
            {
                unit6ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit7ScriptLine2 != null)
            {
                unit7ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit8ScriptLine2 != null)
            {
                unit8ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit9ScriptLine2 != null)
            {
                unit9ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit10ScriptLine2 != null)
            {
                unit10ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit11ScriptLine2 != null)
            {
                unit11ScriptLine2.StartCoroutine("unitMove");
            }
            if (unit12ScriptLine2 != null)
            {
                unit12ScriptLine2.StartCoroutine("unitMove");
            }

            if (unit11Line2 != null && unit11ScriptLine2 != null)
            {
                unit12Line2 = unit11Line2;
                unit12ScriptLine2 = unit11ScriptLine2;
            }
            if (unit10Line2 != null && unit10ScriptLine2 != null)
            {
                unit11Line2 = unit10Line2;
                unit11ScriptLine2 = unit10ScriptLine2;
            }
            if (unit9Line2 != null && unit9ScriptLine2 != null)
            {
                unit10Line2 = unit9Line2;
                unit10ScriptLine2 = unit9ScriptLine2;
            }
            if (unit8Line2 != null && unit8ScriptLine2 != null)
            {
                unit9Line2 = unit8Line2;
                unit9ScriptLine2 = unit8ScriptLine2;
                if (playerScript.currentLine == 2)
                {
                    unitID = unit9ScriptLine2.unitID;
                    if (unitID == 4)
                    {
                        enemyID = unit9ScriptLine2.enemyID;
                        Debug.Log("enemyIDは" + enemyID);
                    }
                }
            }
            if (unit7Line2 != null && unit7ScriptLine2 != null)
            {
                unit8Line2 = unit7Line2;
                unit8ScriptLine2 = unit7ScriptLine2;
            }
            if (unit6Line2 != null && unit6ScriptLine2 != null)
            {
                unit7Line2 = unit6Line2;
                unit7ScriptLine2 = unit6ScriptLine2;
            }
            if (unit5Line2 != null && unit5ScriptLine2 != null)
            {
                unit6Line2 = unit5Line2;
                unit6ScriptLine2 = unit5ScriptLine2;
            }
            if (unit4Line2 != null && unit4ScriptLine2 != null)
            {
                unit5Line2 = unit4Line2;
                unit5ScriptLine2 = unit4ScriptLine2;
            }
            if (unit3Line2 != null && unit3ScriptLine2 != null)
            {
                unit4Line2 = unit3Line2;
                unit4ScriptLine2 = unit3ScriptLine2;
            }
            if (unit2Line2 != null && unit2ScriptLine2 != null)
            {
                unit3Line2 = unit2Line2;
                unit3ScriptLine2 = unit2ScriptLine2;
            }
            if (unit1Line2 != null && unit1ScriptLine2 != null)
            {
                unit2Line2 = unit1Line2;
                unit2ScriptLine2 = unit1ScriptLine2;
            }



            unit1Line2 = Instantiate(unitLine2, new Vector3(1, 0, 0), Quaternion.identity);
            unit1Line2.transform.SetParent(cvs.transform);
            unit1Line2.transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
            unit1Line2.GetComponent<RectTransform>().anchoredPosition = new Vector2(720, -60);
            unit1ScriptLine2 = unit1Line2.GetComponent<UnitMove>();
            unit1Line2.transform.SetParent(unitLine2Parent.transform);

            if (unit1ScriptLine3 != null)
            {
                unit1ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit2ScriptLine3 != null)
            {
                unit2ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit3ScriptLine3 != null)
            {
                unit3ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit4ScriptLine3 != null)
            {
                unit4ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit5ScriptLine3 != null)
            {
                unit5ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit6ScriptLine3 != null)
            {
                unit6ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit7ScriptLine3 != null)
            {
                unit7ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit8ScriptLine3 != null)
            {
                unit8ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit9ScriptLine3 != null)
            {
                unit9ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit10ScriptLine3 != null)
            {
                unit10ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit11ScriptLine3 != null)
            {
                unit11ScriptLine3.StartCoroutine("unitMove");
            }
            if (unit12ScriptLine3 != null)
            {
                unit12ScriptLine3.StartCoroutine("unitMove");
            }

            if (unit11Line3 != null && unit11ScriptLine3 != null)
            {
                unit12Line3 = unit11Line3;
                unit12ScriptLine3 = unit11ScriptLine3;
            }
            if (unit10Line3 != null && unit10ScriptLine3 != null)
            {
                unit11Line3 = unit10Line3;
                unit11ScriptLine3 = unit10ScriptLine3;
            }
            if (unit9Line3 != null && unit9ScriptLine3 != null)
            {
                unit10Line3 = unit9Line3;
                unit10ScriptLine3 = unit9ScriptLine3;
            }
            if (unit8Line3 != null && unit8ScriptLine3 != null)
            {
                unit9Line3 = unit8Line3;
                unit9ScriptLine3 = unit8ScriptLine3;
                if (playerScript.currentLine == 3)
                {
                    unitID = unit9ScriptLine3.unitID;
                    if (unitID == 4)
                    {
                        enemyID = unit9ScriptLine3.enemyID;
                        Debug.Log("enemyIDは" + enemyID);
                    }
                }
            }
            if (unit7Line3 != null && unit7ScriptLine3 != null)
            {
                unit8Line3 = unit7Line3;
                unit8ScriptLine3 = unit7ScriptLine3;
            }
            if (unit6Line3 != null && unit6ScriptLine3 != null)
            {
                unit7Line3 = unit6Line3;
                unit7ScriptLine3 = unit6ScriptLine3;
            }
            if (unit5Line3 != null && unit5ScriptLine3 != null)
            {
                unit6Line3 = unit5Line3;
                unit6ScriptLine3 = unit5ScriptLine3;
            }
            if (unit4Line3 != null && unit4ScriptLine3 != null)
            {
                unit5Line3 = unit4Line3;
                unit5ScriptLine3 = unit4ScriptLine3;
            }
            if (unit3Line3 != null && unit3ScriptLine3 != null)
            {
                unit4Line3 = unit3Line3;
                unit4ScriptLine3 = unit3ScriptLine3;
            }
            if (unit2Line3 != null && unit2ScriptLine3 != null)
            {
                unit3Line3 = unit2Line3;
                unit3ScriptLine3 = unit2ScriptLine3;
            }
            if (unit1Line3 != null && unit1ScriptLine3 != null)
            {
                unit2Line3 = unit1Line3;
                unit2ScriptLine3 = unit1ScriptLine3;
            }



            unit1Line3 = Instantiate(unitLine3, new Vector3(1, 0, 0), Quaternion.identity);
            unit1Line3.transform.SetParent(cvs.transform);
            unit1Line3.transform.localScale = new Vector3(0.44f, 0.44f, 0.44f);
            unit1Line3.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, 20);
            unit1ScriptLine3 = unit1Line3.GetComponent<UnitMove>();
            unit1Line3.transform.SetParent(unitLine3Parent.transform);
        }

    }

    IEnumerator Recast()
    {
        yield return new WaitForSeconds(0.5f);
        recast = false;
    }

    IEnumerator BackgroundScroll()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.02f);
            Backgroundcvs1.anchoredPosition += new Vector2(-8, 0);
            Backgroundcvs2.anchoredPosition += new Vector2(-8, 0);

        }
        if (Backgroundcvs1.anchoredPosition.x <= -1270)
        {
            Backgroundcvs1.anchoredPosition = Backgroundcvs2.anchoredPosition + new Vector2(1527, 0);
        }
        if (Backgroundcvs2.anchoredPosition.x <= -1270)
        {
            Backgroundcvs2.anchoredPosition = Backgroundcvs1.anchoredPosition + new Vector2(1527, 0);
        }
    }

    IEnumerator blackOut()
    {
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, i * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickBackButton()
    {
        StartCoroutine("blackOut2");
    }
    IEnumerator blackOut2()
    {
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, i * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
        SceneManager.LoadScene("TitleScene");
    }
    IEnumerator blackOutOut()
    {
        for (int i = 1; i < 31; i++)
        {
            blackBG.color = new Color(0, 0, 0, (30 - i) * 0.04f);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void massParameterDecide(UnitMove unitMove, GameObject unitObj)
    {
        if (days <= 2)
        {
            unitMove.unitID = Random.Range(1, 4);
        }
        else
        {
            unitMove.unitID = Random.Range(1, 4);
            int rnd = Random.Range(1, 101);
            if (rnd <= 5)
            {
                unitMove.unitID = 4;
            }
            if (days == 22 || days == 52 || days == 82)
            {
                unitMove.unitID = 4;
            }
        }

        switch (unitMove.unitID)
        {
            case 1:
                unitObj.GetComponent<Image>().color = new Color(subColor, mainColor, subColor, 1);
                break;
            case 2:
                unitObj.GetComponent<Image>().color = new Color(mainColor, subColor, subColor, 1);
                break;
            case 3:
                unitObj.GetComponent<Image>().color = new Color(subColor, subColor, mainColor, 1);
                break;
            case 4:
                unitMove.enemyObjAtField.SetActive(true);
                int enemyID = Random.Range(1, 5);
                if (days == 100)
                {
                    enemyID = 5;
                }
                switch (enemyID)
                {
                    case 1:
                        unitMove.enemyImageAtField.sprite = unitMove.enemyImage[0];
                        break;
                    case 2:
                        unitMove.enemyImageAtField.sprite = unitMove.enemyImage[1];
                        break;
                    case 3:
                        unitMove.enemyImageAtField.sprite = unitMove.enemyImage[2];
                        break;
                    case 4:
                        unitMove.enemyImageAtField.sprite = unitMove.enemyImage[3];
                        break;
                    case 5:
                        unitMove.enemyImageAtField.sprite = unitMove.enemyImage[4];
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
