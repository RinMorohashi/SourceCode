using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTextScript : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのステータスが上がったときに生成されるテキストを動かすスクリプト
    /// </summary>
    public RectTransform rt;
    public Text txt1;
    public Text txt2;
    public PlayerScript playerScript;

    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();
        StartCoroutine("up");
        switch (GameObject.Find("player").GetComponent<PlayerScript>().powerUpID)
        {
            case 1:
                txt1.text = "HP +" + Mathf.Round(playerScript.HP - playerScript.previousMAXHP);
                txt2.text = "HP +" + Mathf.Round(playerScript.HP - playerScript.previousMAXHP);
                txt2.color = new Color(0, 1, 0, 1);
                break;
            case 2:
                txt1.text = "ATK +" + Mathf.Round(playerScript.ATK - playerScript.previousATK);
                txt2.text = "ATK +" + Mathf.Round(playerScript.ATK - playerScript.previousATK);
                txt2.color = new Color(1, 0, 0, 1);
                break;
            case 3:
                txt1.text = "SPD +" + Mathf.Round(playerScript.SPD - playerScript.previousSPD);
                txt2.text = "SPD +" + Mathf.Round(playerScript.SPD - playerScript.previousSPD);
                txt2.color = new Color(0, 0, 1, 1);
                break;
            case 4:
                txt1.text = "戦闘開始！";
                txt2.text = "戦闘開始！";
                txt2.color = new Color(1, 1, 1, 1);
                break;
            default:
                break;
        }
    }

    IEnumerator up()
    {
        for (int i = 0; i < 21; i++)
        {
            rt.anchoredPosition += new Vector2(0, 5);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }
}
