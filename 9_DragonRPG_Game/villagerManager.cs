using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villagerManager : MonoBehaviour
{
    /// <summary>
    /// 村人にプレイヤーが近づいたら、台詞を表示させるスクリプト
    /// </summary>
    public GameObject playerObj;
    public GameObject talkWindow;
    float dis;
    float disPrev;
    public bool isActive;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            //プレイヤーが近くに来たら台詞を表示する
            dis = Vector3.Distance(this.transform.position, playerObj.transform.position);
            if (disPrev > 2.5f && dis <= 2.5f)
            {
                talkWindow.SetActive(true);
            }
            else if (disPrev <= 2.5f && dis > 2.5f)
            {
                talkWindow.SetActive(false);
            }
            disPrev = dis;
            //Debug.Log("プレイヤーとの距離は" + dis);
            if (dis < 2.5f && !talkWindow.activeSelf)
            {
                talkWindow.SetActive(true);
            }
        }
    }
}
