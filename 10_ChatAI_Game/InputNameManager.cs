using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameManager : MonoBehaviour
{
    /// <summary>
    /// ���O���͉�ʂ𓮂������߂̃X�N���v�g
    /// </summary>

    //�I�u�W�F�N�g�ƌ��т���
    public WebGLNativeInputField inputField;
    public Text text;
    public GameObject ObjDecideButton;

    public GrabObject nameScript;
    public Text nameText;

    public void InputText()
    {
        //�e�L�X�g��inputField�̓��e�𔽉f
        text.text = inputField.text;

    }

    public void OnClickDecideNameButton()
    {
        if (inputField.text != "")
        {
            InputText();
            GameManager.instance.playerName = "" + text.text;
            GameManager.instance.PushedTalkButton();
            nameScript.StringWordBlock = text.text;
            nameText.text = text.text;
            GameManager.instance.OnLearnEnter();
        }
    }
    public void OnEnterDecideNameButton()
    {
        ObjDecideButton.transform.localScale = new Vector3(1.2f,1.2f,1);
        GameManager.instance.OnLearnEnter();
    }
    public void OnExitDecideNameButton()
    {
        ObjDecideButton.transform.localScale = new Vector3(1f, 1f, 1);
    }
}
