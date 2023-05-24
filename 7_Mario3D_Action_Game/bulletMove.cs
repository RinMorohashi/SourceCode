using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    /// <summary>
    /// キラー（障害物）を動かすコード
    /// </summary>
    public Transform[] waypoints;
    private int currentWaypointIndex;
    private Quaternion q;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypointIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //地点A→B→C→D→Aへ移動する
        switch (currentWaypointIndex)
        {
            case 1:
                q = Quaternion.Euler(0f, -90f, 0f);
                transform.rotation = q;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, 20 * Time.deltaTime);
                if (Vector3.Distance(transform.position, waypoints[0].position) < 0.1f)
                {
                    currentWaypointIndex = 2;
                }
                break;
            case 2:
                q = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = q;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[1].position, 20 * Time.deltaTime);
                if (Vector3.Distance(transform.position, waypoints[1].position) < 0.1f)
                {
                    currentWaypointIndex = 3;
                }
                break;
            case 3:
                q = Quaternion.Euler(0f, 90f, 0f);
                transform.rotation = q;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[2].position, 20 * Time.deltaTime);
                if (Vector3.Distance(transform.position, waypoints[2].position) < 0.1f)
                {
                    currentWaypointIndex = 4;
                }
                break;
            case 4:
                q = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = q;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[3].position, 20 * Time.deltaTime);
                if (Vector3.Distance(transform.position, waypoints[3].position) < 0.1f)
                {
                    currentWaypointIndex = 1;
                }
                break;
            default:
                break;
        }
    }
}
