using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour
{
    /// <summary>
    /// ダメージテキストの動きを制御するスクリプト
    /// </summary>

    public RectTransform rt;
    public Text txt1;
    public Text txt2;

    void Start()
    {
        StartCoroutine("up");
    }

    IEnumerator up()
    {
        for (int i = 0; i < 21; i++)
        {
            rt.anchoredPosition += new Vector2(0, 1);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }

    public void activate(float damage)
    {
        txt1.text = "" + Mathf.Round(damage);
        txt2.text = "" + Mathf.Round(damage);
    }
}
