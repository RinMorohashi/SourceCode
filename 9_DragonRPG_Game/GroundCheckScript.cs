using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが接地したことをstagemanagerに知らせるスクリプト
    /// </summary>
    public StageManager stageManager;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Untagged"))
        {
            stageManager.isGround = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Untagged"))
        {
            stageManager.isGround = false;
        }
    }
}
