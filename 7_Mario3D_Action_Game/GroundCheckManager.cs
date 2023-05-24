using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckManager : MonoBehaviour
{
    /// <summary>
    /// マリオ（プレイヤー）が接地したことを characterControll に知らせるコード（マリオの当たり判定オブジェクトにアタッチする）
    /// </summary>
    public CharacterControll characterControll;

    void OnTriggerEnter(Collider t)
    {
        characterControll.isGround = true;
    }
    void OnTriggerStay(Collider t)
    {
        characterControll.isGround = true;
    }
    void OnTriggerExit(Collider t)
    {
        characterControll.isGround = false;
    }
}
