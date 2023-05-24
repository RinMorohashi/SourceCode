using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Image�R���|�[�l���g��K�v�Ƃ���
[RequireComponent(typeof(Image))]

public class GrabObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>
    /// AI���앶����Ƃ��ɁA���t�u���b�N�̃h���b�O���h���b�v�̓������s���X�N���v�g
    /// </summary>
    
    // �h���b�O�O�̈ʒu
    private Vector3 prevPos;

    //��_�i�}�E�X�̊�͍��������A�I�u�W�F�N�g�̊�͉�ʒ����ɂȂ�̂ŕ␳����B�j
    private Vector2 rootPos;

    public string StringWordBlock;
    [Header("�P��u���b�N�̎��ʔԍ�")] public int wordID;
    [SerializeField] Text textComponent;
    RectTransform OwnRectTransform;
    public float maxTextWidth = 80f; // �ő�̉���
    public float minScale = 0.5f; // �ŏ��̃X�P�[��
    public GameObject parentWordBlock;
    public GameObject parentDummy;
    public AudioSource audiosource;
        public AudioClip grabSE;
    public AudioClip setSE;
    public int blankIDPrev;
    public bool isGrab;
    public bool isSet;

    private Vector2 mousePos;
    private Vector3 objPos;

    public wordID wordid;
    public wordID wordidPrev;
    void Start()
    {
        rootPos = new Vector3(240f * Screen.width / 960f, 270f * Screen.height / 540f, 0f); //��ʂ̔����i400, 300�j
        OwnRectTransform = transform.GetComponent<RectTransform>();
        blankIDPrev = -1;
        Debug.Log("�X�N���[���̉�����" + Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrab)
        {
            float sin = Mathf.Sin(Time.time);
            OwnRectTransform.eulerAngles = new Vector3(0, 0, 5 * sin);
        }
    }


    //�h���b�O���h���b�v�֌W

    public void OnClick()
    {
        //������ƌX����i���t�u���b�N��͂�ł��邱�Ƃ��v���C���[�ɒm�点�邽�߁j
        transform.localEulerAngles = new Vector3(0, 0, -20);
        isGrab = true;

        OwnRectTransform.localScale = new Vector3(1.2f, 1.2f, 1);
        transform.SetParent(parentDummy.transform);
        transform.SetParent(parentWordBlock.transform);
        audiosource.PlayOneShot(grabSE);
    }
    public void OnClickEnd()
    {
        //������ƌX����
        transform.localEulerAngles = new Vector3(0, 0, 0);
        isGrab = false;
        audiosource.PlayOneShot(setSE);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�O�̈ʒu���L�����Ă���
        prevPos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O���͈ʒu���X�V����
        mousePos = Input.mousePosition;
        objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f/*10*/));
        this.transform.position = objPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �h���b�O�O�̈ʒu�ɖ߂�
        transform.localPosition = prevPos;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        wordidPrev = wordid;
        foreach (var hit in raycastResults)
        {
            //�h���b�v�ʒu���͈͊O�Ȃ�A�h���b�O�O�̈ʒu�ɖ߂�
            if (hit.gameObject.CompareTag("wordrobe"))
            {
                mousePos = Input.mousePosition;
                objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                this.transform.position = objPos;


                //�P��u���b�N���O�����Ƃ��A�󗓂ɖ߂�
                if (blankIDPrev != -1)
                {
                    GameManager.instance.wordSetStateString[blankIDPrev - 1] = "";
                    GameManager.instance.wordSetStateID[blankIDPrev - 1] = -1;
                    blankIDPrev = -1;
                    if (wordid != false)
                    {
                        wordid.isBlockSet = false;
                    }
                }
                isSet = false;
            }
        }
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("blank"))
            {
                wordidPrev = wordid;
                wordid = hit.gameObject.GetComponent<wordID>();
                if (!wordid.isBlockSet)//�u�����ꏊ�������u����Ă��Ȃ��󗓂�������
                {
                    //�P��u���b�N�̈ʒu���󗓂ɍ��킹��
                    transform.position = hit.gameObject.transform.position;
                    //�P��u���b�N�̉������󗓂Ɏ��܂�悤�ɍ��킹��
                    float textWidth = textComponent.preferredWidth;
                    Debug.Log("preferredWidth��" + textWidth);
                    if (textWidth > maxTextWidth)
                    {
                        float newScale = maxTextWidth / textWidth;
                        OwnRectTransform.localScale = new Vector3(newScale, 1f, 1f);
                    }
                    else
                    {
                        OwnRectTransform.localScale = Vector3.one;
                    }

                    //�h���b�O�O���ʂ̋󗓂ɒu����Ă�����
                    if (blankIDPrev != -1)
                    {
                        //���̋󗓂���ɂ���
                        GameManager.instance.wordSetStateString[blankIDPrev - 1] = "";
                        GameManager.instance.wordSetStateID[blankIDPrev - 1] = -1;
                        blankIDPrev = -1;
                        if (wordidPrev != null)
                        {
                            wordidPrev.isBlockSet = false;
                        }
                    }
                    //�󗓂𖄂߂�
                    if (wordid != null)
                    {
                        GameManager.instance.wordSetStateString[wordid.blankID - 1] = StringWordBlock;
                        GameManager.instance.wordSetStateID[wordid.blankID - 1] = wordID;
                        wordid.isBlockSet = true;

                        blankIDPrev = wordid.blankID;
                    }
                    isSet = true;
                }
                else
                {
                    wordid = wordidPrev;
                }
            }
        }

        //�X���𒼂�
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //�g���Ă܂���B
        /*var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        foreach (var hit in raycastResults)
        {
            // ���� DroppableField �̏�Ȃ�A���̈ʒu�ɌŒ肷��
            if (hit.gameObject.CompareTag("DroppableField"))
            {
                transform.position = hit.gameObject.transform.position;
                this.enabled = false;
            }
        }*/
    }

    public void OnEnterPointer()
    {
        if (!isSet)
        {
            OwnRectTransform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
    }
    public void OnExitPointer()
    {
        if (!isSet)
        {
            OwnRectTransform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}