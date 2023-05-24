using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    /// <summary>
    /// フィールド上のコインを回転させるコード
    /// </summary>

    [Header("プレイヤースクリプト")] public CharacterControll cc;

    private bool interval;//何故か獲得判定が２回起きるので応急処置

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, 0.1f, 0);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (!interval)
        {
            interval = true;
            //画面上のコインを点灯させる
            cc.CoinGot++;
            cc.GetCoin();
            //自身を消滅させる
            Destroy(this.gameObject);
            //コイン取得効果音を鳴らす
        }
    }
}
