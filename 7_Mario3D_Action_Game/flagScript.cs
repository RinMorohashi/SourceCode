using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagScript : MonoBehaviour
{
    /// <summary>
    /// �}���I�i�v���C���[�j�����ɐG�ꂽ�Ƃ��AcharacterControll�̒��Ԓn�_�X�V�C�x���g�����s������R�[�h
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
