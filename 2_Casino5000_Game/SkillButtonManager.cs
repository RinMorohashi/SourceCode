using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    /// <summary>
    /// �X�L���l����ʂ𓮂����X�N���v�g
    /// </summary>
    public GameObject explanation;
    public Text explanationText1;
    public Text availabilityText1;
    float sin;
    public int ButtonNumber;

    private void OnMouseEnter()
    {
        if (ButtonNumber == 1)
        {
            if (EditManager.skillSwitch1 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch1 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "���������₷���Ȃ�B\n5�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "���������₷���Ȃ�B\n5�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch1 == true)
            {
                explanationText1.text = "���������₷���Ȃ�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 2)
        {
            if (EditManager.skillSwitch2 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch2 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "��V��50���A�b�v�B\n5�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "��V��50���A�b�v�B\n5�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch2 == true)
            {
                explanationText1.text = "��V��50���A�b�v�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 3)
        {
            if (EditManager.skillSwitch3 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch3 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "����ɖ��������₷���Ȃ�B\n5�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "����ɖ��������₷���Ȃ�B\n5�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch3 == true)
            {
                explanationText1.text = "����ɖ��������₷���Ȃ�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 4)
        {
            if (EditManager.skillSwitch4 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch1 == true && EditManager.skillSwitch4 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "�|�����̏�����W�{�ɂ���B\n5�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "�|�����̏�����W�{�ɂ���B\n5�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch4 == true)
            {
                explanationText1.text = "�|�����̏�����W�{�ɂ���B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 5)
        {
            if (EditManager.skillSwitch5 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch2 == true && EditManager.skillSwitch5 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "��V��120���A�b�v�B\n10�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "��V��120���A�b�v�B\n10�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch5 == true)
            {
                explanationText1.text = "��V��120���A�b�v�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 6)
        {
            if (EditManager.skillSwitch6 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if ((EditManager.skillSwitch2 == true || EditManager.skillSwitch3 == true) && EditManager.skillSwitch6 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "�_�u���A�b�v�ɒ���ł���B\n10�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "�_�u���A�b�v�ɒ���ł���B\n10�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch6 == true)
            {
                explanationText1.text = "�_�u���A�b�v�ɒ���ł���B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 7)
        {
            if (EditManager.skillSwitch7 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if ((EditManager.skillSwitch3 == true || EditManager.skillSwitch4 == true) && EditManager.skillSwitch7 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B\n10�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B\n10�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch7 == true)
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 8)
        {
            if (EditManager.skillSwitch8 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch4 == true && EditManager.skillSwitch8 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "�|�����̏��������ɂW�{�ɂ���B\n10�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "�|�����̏��������ɂW�{�ɂ���B\n10�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch8 == true)
            {
                explanationText1.text = "�|�����̏��������ɂW�{�ɂ���B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 9)
        {
            if (EditManager.skillSwitch9 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch5 == true && EditManager.skillSwitch9 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "��V���X�O�O���A�b�v�B\n15�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "��V���X�O�O���A�b�v�B\n15�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch9 == true)
            {
                explanationText1.text = "��V���X�O�O���A�b�v�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 10)
        {
            if (EditManager.skillSwitch10 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch6 == true && EditManager.skillSwitch10 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "�_�u���A�b�v��90���̊m���Ő������A��V��400���A�b�v�B\n15�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "�_�u���A�b�v��90���̊m���Ő������A��V��400���A�b�v�B\n15�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch10 == true)
            {
                explanationText1.text = "�_�u���A�b�v��90���̊m���Ő������A��V��400���A�b�v�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 11)
        {
            if (EditManager.skillSwitch11 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch7 == true && EditManager.skillSwitch11 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B\n15�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B\n15�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch11 == true)
            {
                explanationText1.text = "���b�P�O�O���~�𓾂�B";
                availabilityText1.text = "�擾��";
            }
        }
        else if (ButtonNumber == 12)
        {
            if (EditManager.skillSwitch12 == false)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            explanation.SetActive(true);
            //�X�L�����擾�\�Ȃ�"�N���b�N�Ŏ擾"��\������
            if (EditManager.skillSwitch8 == true && EditManager.skillSwitch12 == false && GameManager.EXP >= 5)
            {
                explanationText1.text = "�|�����̏�����P���~�ɂȂ�B\n15�o���l�K�v�B";
                availabilityText1.text = "�N���b�N�Ŏ擾";
            }
            else
            {
                explanationText1.text = "�|�����̏�����P���~�ɂȂ�B\n15�o���l�K�v�B";
                availabilityText1.text = "�擾�s��";
            }
            if (EditManager.skillSwitch12 == true)
            {
                explanationText1.text = "�|�����̏�����P���~�ɂȂ�B";
                availabilityText1.text = "�擾��";
            }
        }
    }

    private void OnMouseExit()
    {
        if (ButtonNumber == 1)
        {
            if (EditManager.skillSwitch1 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 2)
        {
            if (EditManager.skillSwitch2 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 3)
        {
            if (EditManager.skillSwitch3 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 4)
        {
            if (EditManager.skillSwitch4 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 5)
        {
            if (EditManager.skillSwitch5 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 6)
        {
            if (EditManager.skillSwitch6 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 7)
        {
            if (EditManager.skillSwitch7 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 8)
        {
            if (EditManager.skillSwitch8 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 9)
        {
            if (EditManager.skillSwitch9 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 10)
        {
            if (EditManager.skillSwitch10 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 11)
        {
            if (EditManager.skillSwitch11 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
        else if (ButtonNumber == 12)
        {
            if (EditManager.skillSwitch12 == false)
            {
                this.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            explanation.SetActive(false);
        }
    }
}
