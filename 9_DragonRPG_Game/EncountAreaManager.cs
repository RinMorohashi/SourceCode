using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncountAreaManager : MonoBehaviour
{
    /// <summary>
    /// �G�ƃG���J�E���g���Ȃ��G���A�����X�N���v�g�i�G���A�I�u�W�F�N�g�ɃA�^�b�`���Ďg���j
    /// </summary>
    public StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stageManager.encountSwitch = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stageManager.encountSwitch = true;
        }
    }
}
