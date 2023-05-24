using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    /// <summary>
    /// スキル獲得画面を動かすスクリプト
    /// </summary>
    public GameObject explanation;
    public Text explanationText1;
    public Text availabilityText1;
    float sin;
    public int ButtonNumber;

    private void OnMouseEnter()
    {
        if (ButtonNumber == 1)
        {
            if (EditManager.skillSwitch1 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch1 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "役が揃いやすくなる。\n5経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "役が揃いやすくなる。\n5経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch1 == true)
            {
                explanationText1.text = "役が揃いやすくなる。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 2)
        {
            if (EditManager.skillSwitch2 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch2 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "報酬が50％アップ。\n5経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "報酬が50％アップ。\n5経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch2 == true)
            {
                explanationText1.text = "報酬が50％アップ。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 3)
        {
            if (EditManager.skillSwitch3 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch3 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "さらに役が揃いやすくなる。\n5経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "さらに役が揃いやすくなる。\n5経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch3 == true)
            {
                explanationText1.text = "さらに役が揃いやすくなる。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 4)
        {
            if (EditManager.skillSwitch4 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch4 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "掛け金の上限を８倍にする。\n5経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "掛け金の上限を８倍にする。\n5経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch4 == true)
            {
                explanationText1.text = "掛け金の上限を８倍にする。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 5)
        {
            if (EditManager.skillSwitch5 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch2 == true && EditManager.skillSwitch5 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "報酬が120％アップ。\n10経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "報酬が120％アップ。\n10経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch5 == true)
            {
                explanationText1.text = "報酬が120％アップ。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 6)
        {
            if (EditManager.skillSwitch6 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if ((EditManager.skillSwitch2 == true || EditManager.skillSwitch3 == true) && EditManager.skillSwitch6 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "ダブルアップに挑戦できる。\n10経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "ダブルアップに挑戦できる。\n10経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch6 == true)
            {
                explanationText1.text = "ダブルアップに挑戦できる。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 7)
        {
            if (EditManager.skillSwitch7 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if ((EditManager.skillSwitch3 == true || EditManager.skillSwitch4 == true) && EditManager.skillSwitch7 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "毎秒１００万円を得る。\n10経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "毎秒１００万円を得る。\n10経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch7 == true)
            {
                explanationText1.text = "毎秒１００万円を得る。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 8)
        {
            if (EditManager.skillSwitch8 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch4 == true && EditManager.skillSwitch8 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "掛け金の上限をさらに８倍にする。\n10経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "掛け金の上限をさらに８倍にする。\n10経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch8 == true)
            {
                explanationText1.text = "掛け金の上限をさらに８倍にする。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 9)
        {
            if (EditManager.skillSwitch9 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch5 == true && EditManager.skillSwitch9 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "報酬が９００％アップ。\n15経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "報酬が９００％アップ。\n15経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch9 == true)
            {
                explanationText1.text = "報酬が９００％アップ。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 10)
        {
            if (EditManager.skillSwitch10 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch6 == true && EditManager.skillSwitch10 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "ダブルアップが90％の確率で成功し、報酬が400％アップ。\n15経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "ダブルアップが90％の確率で成功し、報酬が400％アップ。\n15経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch10 == true)
            {
                explanationText1.text = "ダブルアップが90％の確率で成功し、報酬が400％アップ。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 11)
        {
            if (EditManager.skillSwitch11 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch7 == true && EditManager.skillSwitch11 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "毎秒１００億円を得る。\n15経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "毎秒１００億円を得る。\n15経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch11 == true)
            {
                explanationText1.text = "毎秒１００億円を得る。";
                availabilityText1.text = "取得済";
            }
        }
        else if (ButtonNumber == 12)
        {
            if (EditManager.skillSwitch12 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //スキルが取得可能なら"クリックで取得"を表示する
            if (EditManager.skillSwitch8 == true && EditManager.skillSwitch12 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "掛け金の上限が１兆円になる。\n15経験値必要。";
                availabilityText1.text = "クリックで取得";
            }
            else
            {
                explanationText1.text = "掛け金の上限が１兆円になる。\n15経験値必要。";
                availabilityText1.text = "取得不可";
            }
            if (EditManager.skillSwitch12 == true)
            {
                explanationText1.text = "掛け金の上限が１兆円になる。";
                availabilityText1.text = "取得済";
            }
        }
    }

    private void OnMouseExit()
    {
        if (ButtonNumber == 1)
        {
            if (EditManager.skillSwitch1 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 2)
        {
            if (EditManager.skillSwitch2 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 3)
        {
            if (EditManager.skillSwitch3 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 4)
        {
            if (EditManager.skillSwitch4 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 5)
        {
            if (EditManager.skillSwitch5 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 6)
        {
            if (EditManager.skillSwitch6 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 7)
        {
            if (EditManager.skillSwitch7 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 8)
        {
            if (EditManager.skillSwitch8 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 9)
        {
            if (EditManager.skillSwitch9 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 10)
        {
            if (EditManager.skillSwitch10 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 11)
        {
            if (EditManager.skillSwitch11 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 12)
        {
            if (EditManager.skillSwitch12 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
    }
}
