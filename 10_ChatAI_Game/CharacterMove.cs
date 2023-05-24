using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの待機モーションを制御するコード
    /// </summary>
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject fieldObj;
    [SerializeField] Rigidbody2D fieldObjRB;
    private float timeCounter;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isMoveScene)
        {
            timeCounter += Time.deltaTime;
            playerObj.transform.eulerAngles = new Vector3(0, 0, 15 * Mathf.Sin(3 * Time.time));
        }
        else
        {
            playerObj.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
