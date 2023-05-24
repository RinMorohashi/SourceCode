using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// ユニットをドラッグ＆ドロップする動作を行うスクリプト
    /// </summary>
    public Transform cardParent;
    [SerializeField] private GameObject availableMarker;//GameObject型の変数を宣言　好きなゲームオブジェクトをアタッチ

    public void OnBeginDrag(PointerEventData eventData) // ドラッグを始めるときに行う処理
    {
        availableMarker.SetActive(false); //AvailabeMarkを非表示にする.
        cardParent = transform.parent;
        transform.SetParent(cardParent.parent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycastsをオフにする
    }

    public void OnDrag(PointerEventData eventData) // ドラッグした時に起こす処理
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // カードを離したときに行う処理
    {
        transform.SetParent(cardParent, false);
        transform.localPosition += new Vector3(155, 0, 0);
        GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
    }
}
