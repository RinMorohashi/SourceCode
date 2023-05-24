using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// ���j�b�g���h���b�O���h���b�v���铮����s���X�N���v�g
    /// </summary>
    public Transform cardParent;
    [SerializeField] private GameObject availableMarker;//GameObject�^�̕ϐ���錾�@�D���ȃQ�[���I�u�W�F�N�g���A�^�b�`

    public void OnBeginDrag(PointerEventData eventData) // �h���b�O���n�߂�Ƃ��ɍs������
    {
        availableMarker.SetActive(false); //AvailabeMark���\���ɂ���.
        cardParent = transform.parent;
        transform.SetParent(cardParent.parent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycasts���I�t�ɂ���
    }

    public void OnDrag(PointerEventData eventData) // �h���b�O�������ɋN��������
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // �J�[�h�𗣂����Ƃ��ɍs������
    {
        transform.SetParent(cardParent, false);
        transform.localPosition += new Vector3(155, 0, 0);
        GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycasts���I���ɂ���
    }
}
