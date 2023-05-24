using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditManager : MonoBehaviour
{
    /// <summary>
    /// スキル獲得画面を動かすスクリプト２
    /// </summary>
    public GameObject Button1;
    public GameObject Button2_1;
    public GameObject Button2_2;
    public GameObject Button2_3;
    public GameObject Button3_1;
    public GameObject Button3_2;
    public GameObject Button3_3;
    public GameObject Button3_4;
    public GameObject Button4_1;
    public GameObject Button4_2;
    public GameObject Button4_3;
    public GameObject Button4_4;
    public GameObject Image1;
    public GameObject Image2_1;
    public GameObject Image2_2;
    public GameObject Image2_3;
    public GameObject Image3_1;
    public GameObject Image3_2;
    public GameObject Image3_3;
    public GameObject Image3_4;
    public GameObject Image4_1;
    public GameObject Image4_2;
    public GameObject Image4_3;
    public GameObject Image4_4;
    public GameObject explanation1;
    public GameObject explanation2_1;
    public GameObject explanation2_2;
    public GameObject explanation2_3;
    public GameObject explanation3_1;
    public GameObject explanation3_2;
    public GameObject explanation3_3;
    public GameObject explanation3_4;
    public GameObject explanation4_1;
    public GameObject explanation4_2;
    public GameObject explanation4_3;
    public GameObject explanation4_4;
    public GameObject availableMark1;
    public GameObject availableMark2_1;
    public GameObject availableMark2_2;
    public GameObject availableMark2_3;
    public GameObject availableMark3_1;
    public GameObject availableMark3_2;
    public GameObject availableMark3_3;
    public GameObject availableMark3_4;
    public GameObject availableMark4_1;
    public GameObject availableMark4_2;
    public GameObject availableMark4_3;
    public GameObject availableMark4_4;

    public static bool skillSwitch1 = false;
    public static bool skillSwitch2 = false;
    public static bool skillSwitch3 = false;
    public static bool skillSwitch4 = false;
    public static bool skillSwitch5 = false;
    public static bool skillSwitch6 = false;
    public static bool skillSwitch7 = false;
    public static bool skillSwitch8 = false;
    public static bool skillSwitch9 = false;
    public static bool skillSwitch10 = false;
    public static bool skillSwitch11 = false;
    public static bool skillSwitch12 = false;

    public GameObject skillTree1;
    public GameObject skillTree2;
    public GameObject skillTree3;
    public GameObject skillTree4;
    public GameObject skillTree5;
    public GameObject skillTree6;
    public GameObject skillTree7;
    public GameObject skillTree8;

    public GameObject skillExplanation1;
    public GameObject skillExplanation2;
    public GameObject skillExplanation3;
    public GameObject skillExplanation4;
    public GameObject skillExplanation5;
    public GameObject skillExplanation6;
    public GameObject skillExplanation7;
    public GameObject skillExplanation8;
    public GameObject skillExplanation9;
    public GameObject skillExplanation10;
    public GameObject skillExplanation11;
    public GameObject skillExplanation12;

    float sin;
    public GameObject SkillObtained;
    public Text EXPFigure;
    public GameObject SE1;
    public GameObject choose;
    public GameObject loading;
    public GameObject cancel;

    public GameObject BGM4;

    void Start()
    {
        if (skillSwitch1 == false)
        {
            Button1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch2 == false)
        {
            Button2_1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch3 == false)
        {
            Button2_2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch4 == false)
        {
            Button2_3.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch5 == false)
        {
            Button3_1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch6 == false)
        {
            Button3_2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch7 == false)
        {
            Button3_3.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch8 == false)
        {
            Button3_4.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch9 == false)
        {
            Button4_1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch10 == false)
        {
            Button4_2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch11 == false)
        {
            Button4_3.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        if (skillSwitch12 == false)
        {
            Button4_4.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        
        if (skillSwitch1 == false && GameManager.EXP >= 5)
        {
            availableMark1.SetActive(true);            
        }

        if (skillSwitch1 == true && skillSwitch2 == false && GameManager.EXP >= 5)
        {
            availableMark2_1.SetActive(true);
        }

        if (skillSwitch1 == true && skillSwitch3 == false && GameManager.EXP >= 5)
        {
            availableMark2_2.SetActive(true);            
        }
        if (skillSwitch1 == true && skillSwitch4 == false && GameManager.EXP >= 5)
        {
            availableMark2_3.SetActive(true);            
        }
        if (skillSwitch2 == true && skillSwitch5 == false && GameManager.EXP >= 5)
        {
            availableMark3_1.SetActive(true);            
        }
        if ((skillSwitch2 == true || skillSwitch3 == true) && skillSwitch6 == false && GameManager.EXP >= 5)
        {
            availableMark3_2.SetActive(true);            
        }
        if ((skillSwitch3 == true || skillSwitch4 == true) && skillSwitch7 == false && GameManager.EXP >= 5)
        {
            availableMark3_3.SetActive(true);            
        }
        if (skillSwitch4 == true && skillSwitch8 == false && GameManager.EXP >= 5)
        {
            availableMark3_4.SetActive(true);            
        }
        if (skillSwitch5 == true && skillSwitch9 == false && GameManager.EXP >= 5)
        {
            availableMark4_1.SetActive(true);            
        }
        if (skillSwitch6 == true && skillSwitch10 == false && GameManager.EXP >= 5)
        {
            availableMark4_2.SetActive(true);            
        }
        if (skillSwitch7 == true && skillSwitch11 == false && GameManager.EXP >= 5)
        {
            availableMark4_3.SetActive(true);            
        }
        if (skillSwitch8 == true && skillSwitch12 == false && GameManager.EXP >= 5)
        {
            availableMark4_4.SetActive(true);
        }

        if (skillSwitch1 == true)
        {
            skillTree1.SetActive(true);
            skillExplanation1.SetActive(true);
        }
        if (skillSwitch2 == true)
        {
            skillTree2.SetActive(true);
            skillExplanation2.SetActive(true);
        }
        if (skillSwitch3 == true)
        {
            skillTree3.SetActive(true);
            skillExplanation3.SetActive(true);
        }
        if (skillSwitch4 == true)
        {
            skillTree4.SetActive(true);
            skillExplanation4.SetActive(true);
        }
        if (skillSwitch5 == true)
        {
            skillTree5.SetActive(true);
            skillExplanation5.SetActive(true);
        }
        if (skillSwitch6 == true)
        {
            skillTree6.SetActive(true);
            skillExplanation6.SetActive(true);
        }
        if (skillSwitch7 == true)
        {
            skillTree7.SetActive(true);
            skillExplanation7.SetActive(true);
        }
        if (skillSwitch8 == true)
        {
            skillTree8.SetActive(true);
            skillExplanation8.SetActive(true);
        }
        if (skillSwitch9 == true)
        {
            skillExplanation9.SetActive(true);
        }
        if (skillSwitch10 == true)
        {
            skillExplanation10.SetActive(true);
        }
        if (skillSwitch11 == true)
        {
            skillExplanation11.SetActive(true);
        }
        if (skillSwitch12 == true)
        {
            skillExplanation12.SetActive(true);
        }

        EXPFigure.text = "" + GameManager.EXP;
        BGM4.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        sin = Mathf.Abs(Mathf.Sin(Time.time));
        availableMark1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark2_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark2_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark2_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark3_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark3_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark3_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark3_4.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark4_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark4_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark4_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);
        availableMark4_4.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, sin);

        if (Input.GetKeyUp(KeyCode.X))
        {
                OnClickSTButton();
        }
    }

    public void OnClickSTButton()
    {
        loading.SetActive(true);
        choose.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void OnclickButton1()
    {
        if (skillSwitch1 == false && GameManager.EXP >= 5)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 5;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark1.SetActive(false);
            Button1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch1 = true;
            skillExplanation1.SetActive(true);
            skillTree1.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 5)
            {
                if (skillSwitch2 == false)
                {
                    availableMark2_1.SetActive(true);
                }
                if (skillSwitch3 == false)
                {
                    availableMark2_2.SetActive(true);
                }
                if (skillSwitch4 == false)
                {
                    availableMark2_3.SetActive(true);
                }

            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton2_1()
    {
        if (skillSwitch1 == true && skillSwitch2 == false && GameManager.EXP >= 5)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 5;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark2_1.SetActive(false);
            Button2_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch2 = true;
            skillExplanation2.SetActive(true);
            skillTree2.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 10)
            {
                if (skillSwitch5 == false)
                {
                    availableMark3_1.SetActive(true);
                }
                if (skillSwitch6 == false)
                {
                    availableMark3_2.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton2_2()
    {
        if (skillSwitch1 == true && skillSwitch3 == false && GameManager.EXP >= 5)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 5;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark2_2.SetActive(false);
            Button2_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch3 = true;
            skillExplanation3.SetActive(true);
            skillTree3.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 10)
            {
                if (skillSwitch6 == false)
                {
                    availableMark3_2.SetActive(true);
                }
                if (skillSwitch7 == false)
                {
                    availableMark3_3.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton2_3()
    {
        if (skillSwitch1 == true && skillSwitch4 == false && GameManager.EXP >= 5)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 5;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark2_3.SetActive(false);
            Button2_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch4 = true;
            skillExplanation4.SetActive(true);
            skillTree4.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 10)
            {
                if (skillSwitch7 == false)
                {
                    availableMark3_3.SetActive(true);
                }
                if (skillSwitch8 == false)
                {
                    availableMark3_4.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton3_1()
    {
        if (skillSwitch2 == true && skillSwitch5 == false && GameManager.EXP >= 10)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 10;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark3_1.SetActive(false);
            Button3_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch5 = true;
            skillExplanation5.SetActive(true);
            skillTree5.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 15)
            {
                if (skillSwitch9 == false)
                {
                    availableMark4_1.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton3_2()
    {
        if ((skillSwitch2 == true || skillSwitch3 == true) && skillSwitch6 == false && GameManager.EXP >= 10)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 10;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark3_2.SetActive(false);
            Button3_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch6 = true;
            skillExplanation6.SetActive(true);
            skillTree6.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 15)
            {
                if (skillSwitch10 == false)
                {
                    availableMark4_2.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton3_3()
    {
        if ((skillSwitch3 == true || skillSwitch4 == true) && skillSwitch7 == false && GameManager.EXP >= 10)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 10;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark3_3.SetActive(false);
            Button3_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch7 = true;
            skillExplanation7.SetActive(true);
            skillTree7.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 15)
            {
                if (skillSwitch11 == false)
                {
                    availableMark4_3.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton3_4()
    {
        if (skillSwitch4 == true && skillSwitch8 == false && GameManager.EXP >= 10)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 10;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark3_4.SetActive(false);
            Button3_4.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch8 = true;
            skillExplanation8.SetActive(true);
            skillTree8.SetActive(true);
            StartCoroutine("SkillObtainEffect");
            if (GameManager.EXP >= 15)
            {
                if (skillSwitch12 == false)
                {
                    availableMark4_4.SetActive(true);
                }
            }
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton4_1()
    {
        if (skillSwitch5 == true && skillSwitch9 == false && GameManager.EXP >= 15)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 15;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark4_1.SetActive(false);
            Button4_1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch9 = true;
            skillExplanation9.SetActive(true);
            StartCoroutine("SkillObtainEffect");
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton4_2()
    {
        if (skillSwitch6 == true && skillSwitch10 == false && GameManager.EXP >= 15)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 15;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark4_2.SetActive(false);
            Button4_2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch10 = true;
            skillExplanation10.SetActive(true);
            StartCoroutine("SkillObtainEffect");
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton4_3()
    {
        if (skillSwitch7 == true && skillSwitch11 == false && GameManager.EXP >= 15)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 15;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark4_3.SetActive(false);
            Button4_3.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch11 = true;
            skillExplanation11.SetActive(true);
            StartCoroutine("SkillObtainEffect");
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    public void OnclickButton4_4()
    {
        if (skillSwitch8 == true && skillSwitch12 == false && GameManager.EXP >= 15)//スキルが未取得＆スキルを取得する条件が揃っていたら
        {
            GameManager.EXP -= 15;
            EXPFigure.text = "" + GameManager.EXP;
            availableMark4_4.SetActive(false);
            Button4_4.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            skillSwitch12 = true;
            skillExplanation12.SetActive(true);
            StartCoroutine("SkillObtainEffect");
        }
        else
        {
            cancel.GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator SkillObtainEffect()
    {
        SE1.GetComponent<AudioSource>().Play();
        SkillObtained.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SkillObtained.SetActive(false);
    }
}
