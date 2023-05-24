using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    /// <summary>
    /// �v���C���[���ڒn�������Ƃ�stagemanager�ɒm�点��X�N���v�g
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
