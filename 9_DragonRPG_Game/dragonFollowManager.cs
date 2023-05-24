using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonFollowManager : MonoBehaviour
{
    /// <summary>
    /// フィールド画面で主人公に付いてくるレッサードラゴンを動かすスクリプト
    /// </summary>
    public GameObject playerObj;
    public RectTransform rt;

    public RectTransform dogRt;
    public GameObject dogFollowerObj;
    public Rigidbody2D dogFollowerRigidBody;
    public float speed;
    public 
    float dis;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerObj.transform.position.x - transform.position.x > 1)
        {
            rt.anchoredPosition += new Vector2(4.5f,0);
            rt.localScale = new Vector3(1,1,1);
        }
        else if (playerObj.transform.position.x - transform.position.x < -1)
        {
            rt.anchoredPosition -= new Vector2(4.5f, 0);
            rt.localScale = new Vector3(-1, 1, 1);
        }
        if (playerObj.transform.position.y - transform.position.y > 1)
        {
            rt.anchoredPosition += new Vector2(0, 4.5f);
        }
        else if (playerObj.transform.position.y - transform.position.y < -1)
        {
            rt.anchoredPosition -= new Vector2(0, 4.5f);
        }

        if (playerObj.transform.position.x - dogFollowerObj.transform.position.x > 3)
        {
            dogFollowerRigidBody.velocity += new Vector2(speed, 0);
            dogRt.localScale = new Vector3(1, 1, 1);
        }
        else if (playerObj.transform.position.x - dogFollowerObj.transform.position.x < -3)
        {
            dogFollowerRigidBody.velocity -= new Vector2(speed, 0);
            dogRt.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            dogFollowerRigidBody.velocity = new Vector2(0, dogFollowerRigidBody.velocity.y);
            
            if (dogFollowerObj.transform.localEulerAngles.z > 10)
            {
                dogFollowerObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, dogFollowerObj.transform.eulerAngles.z +3);
            }
            else if (dogFollowerObj.transform.localEulerAngles.z < -10)
            {
                dogFollowerObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, dogFollowerObj.transform.eulerAngles.z -3);
            }
            
        }
        dis = Vector3.Distance(playerObj.transform.position, dogFollowerObj.transform.position);
        if (dis >= 15)
        {
            dogFollowerObj.transform.position = playerObj.transform.position;
        }
    }
}
