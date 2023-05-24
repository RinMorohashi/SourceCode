using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckManager : MonoBehaviour
{
    /// <summary>
    /// �}���I�i�v���C���[�j���ڒn�������Ƃ� characterControll �ɒm�点��R�[�h�i�}���I�̓����蔻��I�u�W�F�N�g�ɃA�^�b�`����j
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
