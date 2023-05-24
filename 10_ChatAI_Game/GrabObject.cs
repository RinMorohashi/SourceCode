using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Imageコンポーネントを必要とする
[RequireComponent(typeof(Image))]

public class GrabObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>
    /// AIが作文するときに、言葉ブロックのドラッグ＆ドロップの動きを行うスクリプト
    /// </summary>
    
    // ドラッグ前の位置
    private Vector3 prevPos;

    //基準点（マウスの基準は左下だが、オブジェクトの基準は画面中央になるので補正する。）
    private Vector2 rootPos;

    public string StringWordBlock;
    [Header("単語ブロックの識別番号")] public int wordID;
    [SerializeField] Text textComponent;
    RectTransform OwnRectTransform;
    public float maxTextWidth = 80f; // 最大の横幅
    public float minScale = 0.5f; // 最小のスケール
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
        rootPos = new Vector3(240f * Screen.width / 960f, 270f * Screen.height / 540f, 0f); //画面の半分（400, 300）
        OwnRectTransform = transform.GetComponent<RectTransform>();
        blankIDPrev = -1;
        Debug.Log("スクリーンの横幅は" + Screen.width);
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


    //ドラッグ＆ドロップ関係

    public void OnClick()
    {
        //ちょっと傾ける（言葉ブロックを掴んでいることをプレイヤーに知らせるため）
        transform.localEulerAngles = new Vector3(0, 0, -20);
        isGrab = true;

        OwnRectTransform.localScale = new Vector3(1.2f, 1.2f, 1);
        transform.SetParent(parentDummy.transform);
        transform.SetParent(parentWordBlock.transform);
        audiosource.PlayOneShot(grabSE);
    }
    public void OnClickEnd()
    {
        //ちょっと傾ける
        transform.localEulerAngles = new Vector3(0, 0, 0);
        isGrab = false;
        audiosource.PlayOneShot(setSE);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        prevPos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中は位置を更新する
        mousePos = Input.mousePosition;
        objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f/*10*/));
        this.transform.position = objPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置に戻す
        transform.localPosition = prevPos;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        wordidPrev = wordid;
        foreach (var hit in raycastResults)
        {
            //ドロップ位置が範囲外なら、ドラッグ前の位置に戻す
            if (hit.gameObject.CompareTag("wordrobe"))
            {
                mousePos = Input.mousePosition;
                objPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                this.transform.position = objPos;


                //単語ブロックを外したとき、空欄に戻す
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
                if (!wordid.isBlockSet)//置いた場所が何も置かれていない空欄だったら
                {
                    //単語ブロックの位置を空欄に合わせる
                    transform.position = hit.gameObject.transform.position;
                    //単語ブロックの横幅を空欄に収まるように合わせる
                    float textWidth = textComponent.preferredWidth;
                    Debug.Log("preferredWidthは" + textWidth);
                    if (textWidth > maxTextWidth)
                    {
                        float newScale = maxTextWidth / textWidth;
                        OwnRectTransform.localScale = new Vector3(newScale, 1f, 1f);
                    }
                    else
                    {
                        OwnRectTransform.localScale = Vector3.one;
                    }

                    //ドラッグ前も別の空欄に置かれていたら
                    if (blankIDPrev != -1)
                    {
                        //その空欄を空にする
                        GameManager.instance.wordSetStateString[blankIDPrev - 1] = "";
                        GameManager.instance.wordSetStateID[blankIDPrev - 1] = -1;
                        blankIDPrev = -1;
                        if (wordidPrev != null)
                        {
                            wordidPrev.isBlockSet = false;
                        }
                    }
                    //空欄を埋める
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

        //傾きを直す
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //使ってません。
        /*var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        foreach (var hit in raycastResults)
        {
            // もし DroppableField の上なら、その位置に固定する
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