using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villagerManager : MonoBehaviour
{
    /// <summary>
    /// ���l�Ƀv���C���[���߂Â�����A�䎌��\��������X�N���v�g
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
            //�v���C���[���߂��ɗ�����䎌��\������
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
            //Debug.Log("�v���C���[�Ƃ̋�����" + dis);
            if (dis < 2.5f && !talkWindow.activeSelf)
            {
                talkWindow.SetActive(true);
            }
        }
    }
}
