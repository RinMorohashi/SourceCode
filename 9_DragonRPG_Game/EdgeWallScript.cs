using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeWallScript : MonoBehaviour
{
    /// <summary>
    /// �v���C���[���Փ˂����Ƃ��A�X�e�[�W��؂�ւ��郁�\�b�h�����s����X�N���v�g�i�X�e�[�W�̒[�����ɔz�u����j
    /// </summary>
    public int wallNum;
    public StageManager stagemanager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (wallNum)
            {
                case 5:
                    stagemanager.stageID = 2;
                    break;
                case 6:
                    stagemanager.stageID = 1;
                    break;
                case 13:
                    stagemanager.stageID = 3;
                    break;
                case 14:
                    stagemanager.stageID = 2;
                    break;
                default:
                    break;
            }
            stagemanager.stageChange(wallNum);
        }
    }
}
