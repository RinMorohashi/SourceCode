using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    /// <summary>
    /// �t�B�[���h��̃R�C������]������R�[�h
    /// </summary>

    [Header("�v���C���[�X�N���v�g")] public CharacterControll cc;

    private bool interval;//���̂��l�����肪�Q��N����̂ŉ��}���u

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
            //��ʏ�̃R�C����_��������
            cc.CoinGot++;
            cc.GetCoin();
            //���g�����ł�����
            Destroy(this.gameObject);
            //�R�C���擾���ʉ���炷
        }
    }
}
