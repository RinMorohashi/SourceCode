using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagScript : MonoBehaviour
{
    /// <summary>
    /// マリオ（プレイヤー）が旗に触れたとき、characterControllの中間地点更新イベントを実行させるコード
    /// </summary>
    
    public CharacterControll characterControll;
    public int flagNum;

    void OnTriggerEnter(Collider t)
    {
        if (characterControll.respownNum != flagNum)
        {
            characterControll.respownNum = flagNum;
            characterControll.StartCoroutine("RespownPointUpdate");
        }
    }
}
