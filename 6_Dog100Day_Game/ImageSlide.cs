using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlide : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeftButton()
    {
        StartCoroutine("LeftSlide");
    }

    public void OnClickRightButton()
    {
        StartCoroutine("RightSlide");
    }

    public void OnClickEinZweiButton()
    {
        StartCoroutine("einzweiSlide");
    }

    public void OnClickZweiDreiButton()
    {
        StartCoroutine("zweidreiSlide");
    }

    public void OnClickZweiEinButton()
    {
        StartCoroutine("zweieinSlide");
    }

    IEnumerator LeftSlide()
    {
        for (int i = 1; i < 11; i++)
        {
            Image1.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * -96, -35);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator RightSlide()
    {
        for (int i = 1; i < 11; i++)
        {
            Image1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960 + i * 96, -35);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator einzweiSlide()
    {
        for (int i = 1; i < 11; i++)
        {
            Image1.GetComponent<RectTransform>().anchoredPosition = new Vector2(960 - i * 96, -35);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator zweidreiSlide()
    {
        for (int i = 1; i < 11; i++)
        {
            Image1.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * -96, -35);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator zweieinSlide()
    {
        for (int i = 1; i < 11; i++)
        {
            Image1.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 96, -35);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
