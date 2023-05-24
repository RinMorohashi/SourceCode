using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordID : MonoBehaviour
{
    /// <summary>
    /// 文章の空欄オブジェクトにアタッチして、どの空欄（blankID）に言葉ブロックがセットされているか(isBlockSet)をGameManagerが取得できるようにする
    /// </summary>

    public int blankID;
    public bool isBlockSet;
}
