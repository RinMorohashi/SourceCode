using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    /// <summary>
    /// クリックされたらチュートリアルを表示するボタンにアタッチするスクリプト
    /// </summary>
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject htpb1;
    public GameObject htpb2;
    public GameObject htpb3;
    public int ButtonNumber;

    public void OnClickStartButton()
    {
        if (ButtonNumber == 1)
        {
            image1.SetActive(true);
            htpb1.SetActive(true);
        }
        else if (ButtonNumber == 2)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            htpb2.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (ButtonNumber == 3)
        {
            image2.SetActive(false);
            image3.SetActive(true);
            htpb3.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (ButtonNumber == 4)
        {
            image3.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

}
