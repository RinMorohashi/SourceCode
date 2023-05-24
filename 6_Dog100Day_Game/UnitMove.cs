using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMove : MonoBehaviour
{
    /// <summary>
    /// フィールド画面でのマスを種類や動きを制御するスクリプト
    /// </summary>
    public Gamemanager gamemanager;
    public int locationNum = 1;//画面上の位置を表す
    private RectTransform rt;
    Animator animator;

    public int unitID;// マスの種類を表す（１ならHPマス、２ならATKマス、３ならSPDマス、４なら戦闘マス、５なら宝箱マス）
    public float mainColor = 0.75f;
    public float subColor = 0.25f;

    public int line;

    public Sprite[] enemyImage;
    public Sprite[] iconImage;
    public GameObject enemyObjAtField;
    public GameObject iconObjAtField;
    public Image enemyImageAtField;
    public Image iconImageAtField;
    [SerializeField] PlayerScript playerScript;
    public int enemyID;
    public GameObject templeImageObj;

    [SerializeField] RectTransform iconRTransformAtField;

    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        rt = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        
        if (gamemanager.days <= 2)
        {
            unitID = Random.Range(1, 4);
        }
        else
        {
            unitID = Random.Range(1, 4);
            int rnd = Random.Range(1, 101);
            if (rnd <= 5)
            {
                unitID = 4;
            }
            else if (rnd >= 97)
            {
                //↓転職マス
                //unitID = 5;
            }
            if (gamemanager.days == 22 || gamemanager.days == 52 || gamemanager.days == 82)
            {
                unitID = 4;
            }
        }

        switch (unitID)
        {
            case 1:
                GetComponent<Image>().color = new Color(subColor, mainColor, subColor, 1);
                if (iconObjAtField != null)
                {
                    iconObjAtField.SetActive(true);
                    iconImageAtField.sprite = iconImage[0];
                    iconImageAtField.color = new Color(mainColor, 1, mainColor, 1);
                }
                break;
            case 2:
                GetComponent<Image>().color = new Color(mainColor, subColor, subColor, 1);
                if (iconObjAtField != null)
                {
                    iconObjAtField.SetActive(true);
                    iconImageAtField.sprite = iconImage[1];
                    iconImageAtField.color = new Color(1, mainColor, mainColor, 1);
                }
                break;
            case 3:
                GetComponent<Image>().color = new Color(subColor, subColor, mainColor, 1);
                if (iconObjAtField != null)
                {
                    iconObjAtField.SetActive(true);
                    iconImageAtField.sprite = iconImage[2];
                    iconImageAtField.color = new Color(subColor, subColor, mainColor, 1);
                    iconImageAtField.color = new Color(mainColor, mainColor, 1, 1);
                }
                break;
            case 4:
                if (enemyObjAtField != null)
                {
                    enemyObjAtField.SetActive(true);
                }
                enemyID = Random.Range(1, 5);
                if (gamemanager.days == 100)
                {
                    enemyID = 5;
                }
                switch (enemyID)
                {
                    
                    case 1:
                        enemyImageAtField.sprite = enemyImage[0];
                        break;
                    case 2:
                        enemyImageAtField.sprite = enemyImage[1];
                        break;
                    case 3:
                        enemyImageAtField.sprite = enemyImage[2];
                        break;
                    case 4:
                        enemyImageAtField.sprite = enemyImage[3];
                        break;
                    case 5:
                        enemyImageAtField.sprite = enemyImage[4];
                        break;
         
                    default:
                        break;
                }
                break;
            case 5:
                templeImageObj.SetActive(true);
                break;
            default:
                break;
        }
        
        if (locationNum == 5)
        {
            animator.SetBool("einzwei", true);
        }
        if (locationNum == 6)
        {
            animator.SetBool("dreifier", true);
        }
        if (locationNum == 7)
        {
            animator.SetBool("funfsechs", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (iconRTransformAtField != null)
        {
            switch (line)
            {
                case 1:
                    iconRTransformAtField.anchoredPosition = new Vector2(0, 50 + 20 * Mathf.Sin(gamemanager.timer * 1.5f));
                    break;
                case 2:
                    iconRTransformAtField.anchoredPosition = new Vector2(0, 50 + 12 * Mathf.Sin(gamemanager.timer * 1.5f));
                    break;
                case 3:
                    iconRTransformAtField.anchoredPosition = new Vector2(0, 50 + 7 * Mathf.Sin(gamemanager.timer * 1.5f));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator unitMove()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.02f);
            switch (line)
            {
                case 1:
                    rt.anchoredPosition += new Vector2(-12, 0);
                    break;
                case 2:
                    rt.anchoredPosition += new Vector2(-8, 0);
                    break;
                case 3:
                    rt.anchoredPosition += new Vector2(-16.0f / 3, 0);
                    break;
                default:
                    break;
            }
            if (i == 10)
            {
                locationNum++;
                switch (locationNum)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:
                        animator.SetBool("einzwei", true);
                        break;
                    case 6:
                        animator.SetBool("dreifier", true);
                        break;
                    case 7:
                        switch (line)
                        {
                            case 1:
                                transform.localScale = new Vector3(-1, 1, 1);
                                if (enemyObjAtField != null)
                                {
                                    enemyObjAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                if (iconImageAtField != null)
                                {
                                    iconImageAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                break;
                            case 2:
                                transform.localScale = new Vector3(-0.67f, 0.67f, 0.67f);
                                if (enemyObjAtField != null)
                                {
                                    enemyObjAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                if (iconImageAtField != null)
                                {
                                    iconImageAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                break;
                            case 3:
                                transform.localScale = new Vector3(-0.44f, 0.44f, 0.44f);
                                if (enemyObjAtField != null)
                                {
                                    enemyObjAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                if (iconImageAtField != null)
                                {
                                    iconImageAtField.transform.localScale = new Vector3(-1, 1, 1);
                                }
                                break;
                            default:
                                break;
                        }
                        animator.SetBool("funfsechs", true);
                        break;
                    case 8:
                        animator.SetBool("siebenacht", true);
                        break;
                    default:
                        break;
                }
            }
        }

        if (locationNum == 12)
        {
            Destroy(this.gameObject);
        }
    }
}
