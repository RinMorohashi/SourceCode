using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    /// <summary>
    /// 敵モンスターのステータスと行動を制御するスクリプト（ラットは４ターン周期、ケルベロスは６ターン周期で同じ行動を繰り返す）
    /// </summary>

    public int enemyID;//１：ラット　２：ケルベロス　３：ブラッドサッカー　４：アモルファス
    public float enemyHP;
    public float enemyCurrentHP;
    public float enemyATK;
    public float enemySPD;

    public RectTransform rt;
    [Header("バトル中のHPテキスト")] public Text enemyBattleHPText;
    [Header("HPスライダー")] public Slider HPSlider;
    public float previousHP;
    public PlayerScript playerScript;
    [Header("ゲームマネージャ")] public Gamemanager gamemanager;
    [Header("ゲームマネージャのオーディオソース")] public AudioSource odiosourse;
    [Header("攻撃予報SE")] public AudioClip cautionSE;
    [Header("攻撃SE")] public AudioClip attackSE;
    private GameObject damageText;
    public bool isAttacking = false;
    [Header("攻撃予報の画像")] public GameObject cautionFields;
    private GameObject crowEffect;
    public GameObject cvs;//キャンバス

    private GameObject cf11;//1のマスに1ターン後に攻撃することを知らせるオブジェクト
    private GameObject cf12;//1のマスに2ターン後に攻撃することを知らせるオブジェクト
    private GameObject cf13;//1のマスに3ターン後に攻撃することを知らせるオブジェクト

    private GameObject cf21;//2のマスに1ターン後に攻撃することを知らせるオブジェクト
    private GameObject cf22;//2のマスに2ターン後に攻撃することを知らせるオブジェクト
    private GameObject cf23;//2のマスに3ターン後に攻撃することを知らせるオブジェクト

    private GameObject cf31;
    private GameObject cf32;
    private GameObject cf33;
    private GameObject cf41;
    private GameObject cf42;
    private GameObject cf43;
    private GameObject cf51;
    private GameObject cf52;
    private GameObject cf53;
    private GameObject cf61;
    private GameObject cf62;
    private GameObject cf63;
    private GameObject cf71;
    private GameObject cf72;
    private GameObject cf73;
    private GameObject cf81;
    private GameObject cf82;
    private GameObject cf83;
    private GameObject cf91;
    private GameObject cf92;
    private GameObject cf93;

    private int randomAttack;
    private float HPratio = 1.5f;
    private float ATKratio = 1.5f;
    private float aa = 120f;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        odiosourse = GameObject.Find("GameManager").GetComponent<AudioSource>();
        enemyBattleHPText = GameObject.Find("enemyHPText").GetComponent<Text>();
        HPSlider = GameObject.Find("EnemyHPGauge").GetComponent<Slider>();
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();

        switch (enemyID)
        {
            case 1:
                for (int i = 1; i < playerScript.ratLevel; i++)
                {
                    enemyHP = enemyHP * HPratio;
                    enemyCurrentHP = enemyCurrentHP * HPratio;
                    enemyATK = enemyATK * ATKratio;
                    enemySPD = enemySPD * ATKratio;
                }
                break;
            case 2:
                transform.localScale = new Vector3(2, 2, 1);
                for (int i = 1; i < playerScript.cerbersLevel; i++)
                {
                    enemyHP = enemyHP * HPratio;
                    enemyCurrentHP = enemyCurrentHP * HPratio;
                    enemyATK = enemyATK * ATKratio;
                    enemySPD = enemySPD * ATKratio;
                }
                break;
            case 3:
                transform.localScale = new Vector3(2, 2, 1);
                for (int i = 1; i < playerScript.bloodSuckerLevel; i++)
                {
                    enemyHP = enemyHP * HPratio;
                    enemyCurrentHP = enemyCurrentHP * HPratio;
                    enemyATK = enemyATK * ATKratio;
                    enemySPD = enemySPD * ATKratio;
                }
                break;
            case 4:
                transform.localScale = new Vector3(2, 2, 1);
                for (int i = 1; i < playerScript.amorphousLevel; i++)
                {
                    enemyHP = enemyHP * HPratio;
                    enemyCurrentHP = enemyCurrentHP * HPratio;
                    enemyATK = enemyATK * ATKratio;
                    enemySPD = enemySPD * ATKratio;
                }
                break;
            case 5:
                transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                for (int i = 1; i < playerScript.dragonLevel; i++)
                {
                    enemyHP = enemyHP * HPratio;
                    enemyCurrentHP = enemyCurrentHP * HPratio;
                    enemyATK = enemyATK * ATKratio;
                    enemySPD = enemySPD * ATKratio;
                }
                break;
            default:
                break;
        }

        previousHP = enemyCurrentHP;
        cautionFields = GameObject.Find("cautionFields");
        cvs = GameObject.Find("Canvas2");
        damageText = (GameObject)Resources.Load("damageText");
        crowEffect = (GameObject)Resources.Load("crowEffect");
        cf11 = GameObject.Find("caution (1)");
        cf12 = GameObject.Find("caution (2)");
        cf13 = GameObject.Find("caution (3)");
        cf21 = GameObject.Find("caution (4)");
        cf22 = GameObject.Find("caution (5)");
        cf23 = GameObject.Find("caution (6)");
        cf31 = GameObject.Find("caution (7)");
        cf32 = GameObject.Find("caution (8)");
        cf33 = GameObject.Find("caution (9)");
        cf41 = GameObject.Find("caution (10)");
        cf42 = GameObject.Find("caution (11)");
        cf43 = GameObject.Find("caution (12)");
        cf51 = GameObject.Find("caution (13)");
        cf52 = GameObject.Find("caution (14)");
        cf53 = GameObject.Find("caution (15)");
        cf61 = GameObject.Find("caution (16)");
        cf62 = GameObject.Find("caution (17)");
        cf63 = GameObject.Find("caution (18)");
        cf71 = GameObject.Find("caution (19)");
        cf72 = GameObject.Find("caution (20)");
        cf73 = GameObject.Find("caution (21)");
        cf81 = GameObject.Find("caution (22)");
        cf82 = GameObject.Find("caution (23)");
        cf83 = GameObject.Find("caution (24)");
        cf91 = GameObject.Find("caution (25)");
        cf92 = GameObject.Find("caution (26)");
        cf93 = GameObject.Find("caution (27)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemyHPUpdate()
    {
        enemyBattleHPText.text = "ENEMY HP      " + Mathf.Round(enemyCurrentHP) + " / " + enemyHP;
        if (enemyCurrentHP <= 0)
        {
            enemyCurrentHP = 0;
            enemyBattleHPText.text = "ENEMY HP      " + "0" + " / " + enemyHP;
        }
        float differ = previousHP - enemyCurrentHP;
        for (int i = 1; i < 31; i++)
        {
            HPSlider.value = (enemyCurrentHP + differ / i) / enemyHP;
            yield return new WaitForSeconds(0.01f);
        }
        HPSlider.value = enemyCurrentHP / enemyHP;
        previousHP = enemyCurrentHP;
        if (enemyCurrentHP <= 0)
        {
            playerScript.StartCoroutine("BattleEnding");
        }
    }

    IEnumerator enemyAction()
    {
        yield return null;

        switch (enemyID)
        {
            case 1:
                if (playerScript.BattleTurn % 4 == 0 && !isAttacking)
                {
                    cautionReset();
                    cf33.SetActive(true);
                    cf63.SetActive(true);
                    cf93.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, - aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 4 == 1 && isAttacking)
                {
                    cautionReset();
                    cf32.SetActive(true);
                    cf62.SetActive(true);
                    cf92.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 4 == 2 && isAttacking)
                {
                    cautionReset();
                    cf31.SetActive(true);
                    cf61.SetActive(true);
                    cf91.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 4 == 3 && isAttacking)
                {
                    cautionReset();
                    //ここで攻撃する
                    if (!(playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9))
                    {
                        odiosourse.PlayOneShot(attackSE);
                    }
                    GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce1.transform.SetParent(cvs.transform);
                    ce1.transform.localScale = new Vector3(1, 1, 1);
                    ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, 10 - aa);
                    GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce2.transform.SetParent(cvs.transform);
                    ce2.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, 70 - aa);
                    GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce3.transform.SetParent(cvs.transform);
                    ce3.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                    ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 115 - aa);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    if (playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9)
                    {
                        yield return new WaitForSeconds(0.3f);
                        float rnd = Random.Range(0.75f, 1.25f);
                        playerScript.currentHP -= enemyATK * rnd;
                        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                        dt.transform.SetParent(cvs.transform);
                        dt.transform.localScale = new Vector3(1, 1, 1);
                        float rnd2 = Random.Range(-20f, 20f);
                        float rnd3 = Random.Range(-20f, 20f);
                        dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                        dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                        odiosourse.PlayOneShot(attackSE);
                        playerScript.StartCoroutine("playerReceiveDamage");
                        playerScript.StartCoroutine("playerHPUpdate");
                    }
                    isAttacking = false;
                }
                break;
            case 3:
                if (playerScript.BattleTurn % 13 == 4 && !isAttacking)
                {
                    cautionReset();
                    cf23.SetActive(true);
                    cf43.SetActive(true);
                    cf53.SetActive(true);
                    cf63.SetActive(true);
                    cf83.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 5 && isAttacking)
                {
                    cautionReset();
                    cf22.SetActive(true);
                    cf42.SetActive(true);
                    cf52.SetActive(true);
                    cf62.SetActive(true);
                    cf82.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 6 && isAttacking)
                {
                    cautionReset();
                    cf21.SetActive(true);
                    cf41.SetActive(true);
                    cf51.SetActive(true);
                    cf61.SetActive(true);
                    cf81.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 7 && isAttacking)
                {
                    cautionReset();
                    //ここで攻撃する
                    if (!(playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8))
                    {
                        odiosourse.PlayOneShot(attackSE);
                    }
                    GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce1.transform.SetParent(cvs.transform);
                    ce1.transform.localScale = new Vector3(1, 1, 1);
                    ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                    GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce2.transform.SetParent(cvs.transform);
                    ce2.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 80 - aa);
                    GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce3.transform.SetParent(cvs.transform);
                    ce3.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                    GameObject ce4 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce4.transform.SetParent(cvs.transform);
                    ce4.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                    GameObject ce5 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce5.transform.SetParent(cvs.transform);
                    ce5.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                    ce5.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    if (playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8)
                    {
                        yield return new WaitForSeconds(0.3f);
                        float rnd = Random.Range(0.75f, 1.25f);
                        playerScript.currentHP -= enemyATK * rnd;
                        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                        dt.transform.SetParent(cvs.transform);
                        dt.transform.localScale = new Vector3(1, 1, 1);
                        float rnd2 = Random.Range(-20f, 20f);
                        float rnd3 = Random.Range(-20f, 20f);
                        dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                        dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                        odiosourse.PlayOneShot(attackSE);
                        playerScript.StartCoroutine("playerReceiveDamage");
                        playerScript.StartCoroutine("playerHPUpdate");
                    }
                    isAttacking = false;
                }
                if (playerScript.BattleTurn % 13 == 9 && !isAttacking)
                {
                    cautionReset();
                    cf23.SetActive(true);
                    cf33.SetActive(true);
                    cf63.SetActive(true);
                    cf83.SetActive(true);
                    cf93.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 10 && isAttacking)
                {
                    cautionReset();
                    cf22.SetActive(true);
                    cf32.SetActive(true);
                    cf62.SetActive(true);
                    cf82.SetActive(true);
                    cf92.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 11 && isAttacking)
                {
                    cautionReset();
                    cf21.SetActive(true);
                    cf31.SetActive(true);
                    cf61.SetActive(true);
                    cf81.SetActive(true);
                    cf91.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 13 == 12 && isAttacking)
                {
                    cautionReset();
                    //ここで攻撃する
                    if (!(playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9))
                    {
                        odiosourse.PlayOneShot(attackSE);
                    }
                    GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce1.transform.SetParent(cvs.transform);
                    ce1.transform.localScale = new Vector3(1, 1, 1);
                    ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                    GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce2.transform.SetParent(cvs.transform);
                    ce2.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                    GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce3.transform.SetParent(cvs.transform);
                    ce3.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                    GameObject ce4 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce4.transform.SetParent(cvs.transform);
                    ce4.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                    GameObject ce5 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce5.transform.SetParent(cvs.transform);
                    ce5.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                    ce5.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    if (playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9)
                    {
                        yield return new WaitForSeconds(0.3f);
                        float rnd = Random.Range(0.75f, 1.25f);
                        playerScript.currentHP -= enemyATK * rnd;
                        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                        dt.transform.SetParent(cvs.transform);
                        dt.transform.localScale = new Vector3(1, 1, 1);
                        float rnd2 = Random.Range(-20f, 20f);
                        float rnd3 = Random.Range(-20f, 20f);
                        dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                        dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                        odiosourse.PlayOneShot(attackSE);
                        playerScript.StartCoroutine("playerReceiveDamage");
                        playerScript.StartCoroutine("playerHPUpdate");
                    }
                    isAttacking = false;
                }
                break;
            case 2:
                if (playerScript.BattleTurn % 6 == 2)
                {
                    if (isAttacking)
                    {
                        cautionReset();
                        if (!(playerScript.battleCurrentPos == 7 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9))
                        {
                            odiosourse.PlayOneShot(attackSE);
                        }
                        GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce1.transform.SetParent(cvs.transform);
                        ce1.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                        ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110, 95 - aa);
                        GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce2.transform.SetParent(cvs.transform);
                        ce2.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                        ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                        GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce3.transform.SetParent(cvs.transform);
                        ce3.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                        ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        if (playerScript.battleCurrentPos == 7 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9)
                        {
                            yield return new WaitForSeconds(0.3f);
                            float rnd = Random.Range(0.75f, 1.25f);
                            playerScript.currentHP -= enemyATK * rnd;
                            GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                            dt.transform.SetParent(cvs.transform);
                            dt.transform.localScale = new Vector3(1, 1, 1);
                            float rnd2 = Random.Range(-20f, 20f);
                            float rnd3 = Random.Range(-20f, 20f);
                            dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                            dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                            odiosourse.PlayOneShot(attackSE);
                            playerScript.StartCoroutine("playerReceiveDamage");
                            playerScript.StartCoroutine("playerHPUpdate");
                        }
                    }

                    cautionReset();
                    cf12.SetActive(true);
                    cf22.SetActive(true);
                    cf32.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 6 == 3 && isAttacking)
                {
                    cautionReset();
                    cf11.SetActive(true);
                    cf21.SetActive(true);
                    cf31.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 6 == 4 && isAttacking)
                {
                    cautionReset();
                    if (!(playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3))
                    {
                        odiosourse.PlayOneShot(attackSE);
                    }
                    GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce1.transform.SetParent(cvs.transform);
                    ce1.transform.localScale = new Vector3(1, 1, 1);
                    ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 40 - aa);
                    GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce2.transform.SetParent(cvs.transform);
                    ce2.transform.localScale = new Vector3(1, 1, 1);
                    ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                    GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce3.transform.SetParent(cvs.transform);
                    ce3.transform.localScale = new Vector3(1, 1, 1);
                    ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    if (playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3)
                    {
                        yield return new WaitForSeconds(0.3f);
                        float rnd = Random.Range(0.75f, 1.25f);
                        playerScript.currentHP -= enemyATK * rnd;
                        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                        dt.transform.SetParent(cvs.transform);
                        dt.transform.localScale = new Vector3(1, 1, 1);
                        float rnd2 = Random.Range(-20f, 20f);
                        float rnd3 = Random.Range(-20f, 20f);
                        dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                        dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                        odiosourse.PlayOneShot(attackSE);
                        playerScript.StartCoroutine("playerReceiveDamage");
                        playerScript.StartCoroutine("playerHPUpdate");
                    }
                    cf42.SetActive(true);
                    cf52.SetActive(true);
                    cf62.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 6 == 5 && isAttacking)
                {
                    cautionReset();
                    cf41.SetActive(true);
                    cf51.SetActive(true);
                    cf61.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 6 == 0 && isAttacking)
                {
                    cautionReset();
                    if (!(playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6))
                    {
                        odiosourse.PlayOneShot(attackSE);
                    }
                    GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce1.transform.SetParent(cvs.transform);
                    ce1.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 80 - aa);
                    GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce2.transform.SetParent(cvs.transform);
                    ce2.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                    GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                    ce3.transform.SetParent(cvs.transform);
                    ce3.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    if (playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6)
                    {
                        yield return new WaitForSeconds(0.3f);
                        float rnd = Random.Range(0.75f, 1.25f);
                        playerScript.currentHP -= enemyATK * rnd;
                        GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                        dt.transform.SetParent(cvs.transform);
                        dt.transform.localScale = new Vector3(1, 1, 1);
                        float rnd2 = Random.Range(-20f, 20f);
                        float rnd3 = Random.Range(-20f, 20f);
                        dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                        dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                        odiosourse.PlayOneShot(attackSE);
                        playerScript.StartCoroutine("playerReceiveDamage");
                        playerScript.StartCoroutine("playerHPUpdate");
                    }
                    cf72.SetActive(true);
                    cf82.SetActive(true);
                    cf92.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 6 == 1 && isAttacking)
                {
                    cautionReset();
                    cf71.SetActive(true);
                    cf81.SetActive(true);
                    cf91.SetActive(true);
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    odiosourse.PlayOneShot(cautionSE);
                }
                break;
            case 4:
                if (playerScript.BattleTurn % 3 == 2 && !isAttacking)
                {
                    randomAttack = Random.Range(1, 7);
                    cautionReset();
                    switch (randomAttack)
                    {
                        case 1:
                            cf12.SetActive(true);
                            cf22.SetActive(true);
                            cf32.SetActive(true);
                            cf52.SetActive(true);
                            break;
                        case 2:
                            cf22.SetActive(true);
                            cf32.SetActive(true);
                            cf62.SetActive(true);
                            break;
                        case 3:
                            cf22.SetActive(true);
                            cf42.SetActive(true);
                            cf52.SetActive(true);
                            cf62.SetActive(true);
                            cf82.SetActive(true);
                            break;
                        case 4:
                            cf32.SetActive(true);
                            cf52.SetActive(true);
                            cf62.SetActive(true);
                            cf92.SetActive(true);
                            break;
                        case 5:
                            cf52.SetActive(true);
                            cf72.SetActive(true);
                            cf82.SetActive(true);
                            cf92.SetActive(true);
                            break;
                        case 6:
                            cf62.SetActive(true);
                            cf82.SetActive(true);
                            cf92.SetActive(true);
                            break;
                        default:
                            break;
                    }
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 3 == 0 && isAttacking)
                {
                    cautionReset();
                    switch (randomAttack)
                    {
                        case 1:
                            cf11.SetActive(true);
                            cf21.SetActive(true);
                            cf31.SetActive(true);
                            cf51.SetActive(true);
                            break;
                        case 2:
                            cf21.SetActive(true);
                            cf31.SetActive(true);
                            cf61.SetActive(true);
                            break;
                        case 3:
                            cf21.SetActive(true);
                            cf41.SetActive(true);
                            cf51.SetActive(true);
                            cf61.SetActive(true);
                            cf81.SetActive(true);
                            break;
                        case 4:
                            cf31.SetActive(true);
                            cf51.SetActive(true);
                            cf61.SetActive(true);
                            cf91.SetActive(true);
                            break;
                        case 5:
                            cf51.SetActive(true);
                            cf71.SetActive(true);
                            cf81.SetActive(true);
                            cf91.SetActive(true);
                            break;
                        case 6:
                            cf61.SetActive(true);
                            cf81.SetActive(true);
                            cf91.SetActive(true);
                            break;
                        default:
                            break;
                    }
                    cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                    isAttacking = true;
                    odiosourse.PlayOneShot(cautionSE);
                }
                else if (playerScript.BattleTurn % 3 == 1 && isAttacking)
                {
                    cautionReset();
                    //ここで攻撃する
                    cautionReset();
                    switch (randomAttack)
                    {
                        case 1:
                            if (!(playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 5))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce1 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce1.transform.SetParent(cvs.transform);
                            ce1.transform.localScale = new Vector3(1, 1, 1);
                            ce1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 40 - aa);
                            GameObject ce2 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce2.transform.SetParent(cvs.transform);
                            ce2.transform.localScale = new Vector3(1, 1, 1);
                            ce2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                            GameObject ce3 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce3.transform.SetParent(cvs.transform);
                            ce3.transform.localScale = new Vector3(1, 1, 1);
                            ce3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            GameObject ce4 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce4.transform.SetParent(cvs.transform);
                            ce4.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 5)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        case 2:
                            if (!(playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3|| playerScript.battleCurrentPos == 6))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce12 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce12.transform.SetParent(cvs.transform);
                            ce12.transform.localScale = new Vector3(1, 1, 1);
                            ce12.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                            GameObject ce22 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce22.transform.SetParent(cvs.transform);
                            ce22.transform.localScale = new Vector3(1, 1, 1);
                            ce22.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                            GameObject ce32 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce32.transform.SetParent(cvs.transform);
                            ce32.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce32.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        case 3:
                            if (!(playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce13 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce13.transform.SetParent(cvs.transform);
                            ce13.transform.localScale = new Vector3(1, 1, 1);
                            ce13.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                            GameObject ce23 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce23.transform.SetParent(cvs.transform);
                            ce23.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce23.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 80 - aa);
                            GameObject ce33 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce33.transform.SetParent(cvs.transform);
                            ce33.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce33.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                            GameObject ce43 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce43.transform.SetParent(cvs.transform);
                            ce43.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce43.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                            GameObject ce53 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce53.transform.SetParent(cvs.transform);
                            ce53.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce53.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        case 4:
                            if (!(playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce14 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce14.transform.SetParent(cvs.transform);
                            ce14.transform.localScale = new Vector3(1, 1, 1);
                            ce14.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                            GameObject ce24 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce24.transform.SetParent(cvs.transform);
                            ce24.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce24.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                            GameObject ce34 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce34.transform.SetParent(cvs.transform);
                            ce34.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce34.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            GameObject ce44 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce44.transform.SetParent(cvs.transform);
                            ce44.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce44.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        case 5:
                            if(!(playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 7 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce15 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce15.transform.SetParent(cvs.transform);
                            ce15.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce15.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                            GameObject ce25 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce25.transform.SetParent(cvs.transform);
                            ce25.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce25.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110, 95 - aa);
                            GameObject ce35 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce35.transform.SetParent(cvs.transform);
                            ce35.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce35.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            GameObject ce45 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce45.transform.SetParent(cvs.transform);
                            ce45.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce45.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 7 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        case 6:
                            if (!(playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9))
                            {
                                odiosourse.PlayOneShot(attackSE);
                            }
                            GameObject ce16 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce16.transform.SetParent(cvs.transform);
                            ce16.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce16.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                            GameObject ce26 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce26.transform.SetParent(cvs.transform);
                            ce26.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce26.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                            GameObject ce36 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce36.transform.SetParent(cvs.transform);
                            ce36.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce36.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 8 || playerScript.battleCurrentPos == 9)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                            break;
                        default:
                            break;
                    }
                    isAttacking = false;
                }
                break;
            case 5://ドラゴン
                if (!playerScript.isKnockOut)
                {
                    if (playerScript.BattleTurn % 3 == 2)
                    {
                        cautionReset();
                        if (isAttacking)
                        {
                            if (!(playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 8))
                            {
                                odiosourse.PlayOneShot(attackSE);
                                float rnd4 = Random.Range(0.95f, 1.25f);
                                playerScript.knockOutGauge += 3.5f * rnd4;
                                if (playerScript.knockOutGauge >= 100f && !playerScript.isKnockOut && !playerScript.Attacked)
                                {
                                    playerScript.knockOutGauge = 99f;
                                }
                                playerScript.StartCoroutine("knockOutUpdate");
                            }
                            GameObject ce16 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce16.transform.SetParent(cvs.transform);
                            ce16.transform.localScale = new Vector3(1, 1, 1);
                            ce16.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95, 40 - aa);
                            GameObject ce26 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce26.transform.SetParent(cvs.transform);
                            ce26.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce26.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75, 80 - aa);
                            GameObject ce36 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce36.transform.SetParent(cvs.transform);
                            ce36.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce36.GetComponent<RectTransform>().anchoredPosition = new Vector2(-65, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 2 || playerScript.battleCurrentPos == 5 || playerScript.battleCurrentPos == 8)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }

                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            odiosourse.PlayOneShot(cautionSE);

                            cf11.SetActive(true);
                            cf41.SetActive(true);
                            cf71.SetActive(true);
                        }

                        cf32.SetActive(true);
                        cf62.SetActive(true);
                        cf92.SetActive(true);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        odiosourse.PlayOneShot(cautionSE);

                    }
                    else if (playerScript.BattleTurn % 3 == 0)
                    {
                        cautionReset();
                        if (isAttacking)
                        {
                            if (!(playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 7))
                            {
                                odiosourse.PlayOneShot(attackSE);
                                float rnd4 = Random.Range(0.95f, 1.25f);
                                playerScript.knockOutGauge += 3.5f * rnd4;
                                if (playerScript.knockOutGauge >= 100f && !playerScript.isKnockOut && !playerScript.Attacked)
                                {
                                    playerScript.knockOutGauge = 99f;
                                }
                                playerScript.StartCoroutine("knockOutUpdate");
                            }
                            GameObject ce16 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce16.transform.SetParent(cvs.transform);
                            ce16.transform.localScale = new Vector3(1, 1, 1);
                            ce16.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 40 - aa);
                            GameObject ce26 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce26.transform.SetParent(cvs.transform);
                            ce26.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                            ce26.GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 80 - aa);
                            GameObject ce36 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                            ce36.transform.SetParent(cvs.transform);
                            ce36.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                            ce36.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110, 95 - aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                            if (playerScript.battleCurrentPos == 1 || playerScript.battleCurrentPos == 4 || playerScript.battleCurrentPos == 7)
                            {
                                yield return new WaitForSeconds(0.3f);
                                float rnd = Random.Range(0.75f, 1.25f);
                                playerScript.currentHP -= enemyATK * rnd;
                                GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                                dt.transform.SetParent(cvs.transform);
                                dt.transform.localScale = new Vector3(1, 1, 1);
                                float rnd2 = Random.Range(-20f, 20f);
                                float rnd3 = Random.Range(-20f, 20f);
                                dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                                dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                                odiosourse.PlayOneShot(attackSE);
                                playerScript.StartCoroutine("playerReceiveDamage");
                                playerScript.StartCoroutine("playerHPUpdate");
                            }
                        }

                        cf31.SetActive(true);
                        cf61.SetActive(true);
                        cf91.SetActive(true);
                        cf22.SetActive(true);
                        cf52.SetActive(true);
                        cf82.SetActive(true);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        odiosourse.PlayOneShot(cautionSE);
                        isAttacking = true;
                    }
                    else if (playerScript.BattleTurn % 3 == 1 && isAttacking)
                    {
                        cautionReset();
                        if (!(playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9))
                        {
                            odiosourse.PlayOneShot(attackSE);
                            float rnd4 = Random.Range(0.95f, 1.25f);
                            playerScript.knockOutGauge += 3.5f * rnd4;
                            if (playerScript.knockOutGauge >= 100f && !playerScript.isKnockOut && !playerScript.Attacked)
                            {
                                playerScript.knockOutGauge = 99f;
                            }
                            playerScript.StartCoroutine("knockOutUpdate");
                        }
                        GameObject ce16 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce16.transform.SetParent(cvs.transform);
                        ce16.transform.localScale = new Vector3(1, 1, 1);
                        ce16.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 40 - aa);
                        GameObject ce26 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce26.transform.SetParent(cvs.transform);
                        ce26.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        ce26.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 80 - aa);
                        GameObject ce36 = Instantiate(crowEffect, new Vector3(1, 0, 0), Quaternion.identity);
                        ce36.transform.SetParent(cvs.transform);
                        ce36.transform.localScale = new Vector3(0.64f, 0.64f, 1);
                        ce36.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 95 - aa);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        if (playerScript.battleCurrentPos == 3 || playerScript.battleCurrentPos == 6 || playerScript.battleCurrentPos == 9)
                        {
                            yield return new WaitForSeconds(0.3f);
                            float rnd = Random.Range(0.75f, 1.25f);
                            playerScript.currentHP -= enemyATK * rnd;
                            GameObject dt = Instantiate(damageText, new Vector3(1, 0, 0), Quaternion.identity);
                            dt.transform.SetParent(cvs.transform);
                            dt.transform.localScale = new Vector3(1, 1, 1);
                            float rnd2 = Random.Range(-20f, 20f);
                            float rnd3 = Random.Range(-20f, 20f);
                            dt.GetComponent<RectTransform>().anchoredPosition = playerScript.rt.anchoredPosition + new Vector2(rnd2, rnd3);
                            dt.GetComponent<DamageTextScript>().activate(enemyATK * rnd);
                            odiosourse.PlayOneShot(attackSE);
                            playerScript.StartCoroutine("playerReceiveDamage");
                            playerScript.StartCoroutine("playerHPUpdate");
                        }

                        cf21.SetActive(true);
                        cf51.SetActive(true);
                        cf81.SetActive(true);
                        cf12.SetActive(true);
                        cf42.SetActive(true);
                        cf72.SetActive(true);
                        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -aa);
                        odiosourse.PlayOneShot(cautionSE);
                    }
                }
                
                break;
            default:
                break;
        }
        
        gamemanager.Untouchable = false;
    }

    public void cautionReset()
    {
        cf11.SetActive(false);
        cf12.SetActive(false);
        cf13.SetActive(false);
        cf21.SetActive(false);
        cf22.SetActive(false);
        cf23.SetActive(false);
        cf31.SetActive(false);
        cf32.SetActive(false);
        cf33.SetActive(false);
        cf41.SetActive(false);
        cf42.SetActive(false);
        cf43.SetActive(false);
        cf51.SetActive(false);
        cf52.SetActive(false);
        cf53.SetActive(false);
        cf61.SetActive(false);
        cf62.SetActive(false);
        cf63.SetActive(false);
        cf71.SetActive(false);
        cf72.SetActive(false);
        cf73.SetActive(false);
        cf81.SetActive(false);
        cf82.SetActive(false);
        cf83.SetActive(false);
        cf91.SetActive(false);
        cf92.SetActive(false);
        cf93.SetActive(false);
    }

    public void cautionActivate()
    {
        cautionFields.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 500);
        cf11.SetActive(true);
        cf12.SetActive(true);
        cf13.SetActive(true);
        cf21.SetActive(true);
        cf22.SetActive(true);
        cf23.SetActive(true);
        cf31.SetActive(true);
        cf32.SetActive(true);
        cf33.SetActive(true);
        cf41.SetActive(true);
        cf42.SetActive(true);
        cf43.SetActive(true);
        cf51.SetActive(true);
        cf52.SetActive(true);
        cf53.SetActive(true);
        cf61.SetActive(true);
        cf62.SetActive(true);
        cf63.SetActive(true);
        cf71.SetActive(true);
        cf72.SetActive(true);
        cf73.SetActive(true);
        cf81.SetActive(true);
        cf82.SetActive(true);
        cf83.SetActive(true);
        cf91.SetActive(true);
        cf92.SetActive(true);
        cf93.SetActive(true);
    }

    IEnumerator ReceiveDamage()
    {
        for (int i = 0; i < 11; i++)
        {
            if (i == 0 || i == 10)
            {
                rt.anchoredPosition += new Vector2(3, 0);
            }
            else if (i % 2 == 1)
            {
                rt.anchoredPosition -= new Vector2(6, 0);
            }
            else
            {
                rt.anchoredPosition += new Vector2(6, 0);
            }
            yield return new WaitForSeconds(0.04f);
        }
    }
}
