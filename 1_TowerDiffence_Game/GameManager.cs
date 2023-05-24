using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 一定間隔で敵を召喚するスクリプト
    /// </summary>
    public GameObject canvas;

    private void Start()
    {
        if (StageCounter.stageCount == 1)
        {
            //SummonEnemyを3.5秒後に呼び出し、以降は１秒毎に実行
            InvokeRepeating(nameof(DelayMethod), 3.0f, 5.0f);
            //Canvasを拾う
            canvas = GameObject.Find("Canvas");
        }
        else if (StageCounter.stageCount == 2)
        {
            InvokeRepeating(nameof(DelayMethod), 5.0f, 4.5f);
            canvas = GameObject.Find("Canvas");
        }
        else if (StageCounter.stageCount == 3)
        {
            InvokeRepeating(nameof(DelayMethod2), 5.0f, 4.0f);
            canvas = GameObject.Find("Canvas");
        }
        else if (StageCounter.stageCount == 4)
        {
            InvokeRepeating(nameof(DelayMethod), 1.5f, 4.5f);
            InvokeRepeating(nameof(DelayMethod2), 3.0f, 5.0f);
            canvas = GameObject.Find("Canvas");
        }
        else if (StageCounter.stageCount == 5)
        {
            InvokeRepeating(nameof(DelayMethod3), 2.0f, 4.0f);
            InvokeRepeating(nameof(DelayMethod4), 2.0f, 4.1f);
            InvokeRepeating(nameof(DelayMethod5), 2.0f, 4.2f);
            canvas = GameObject.Find("Canvas");
        }
    }

    void DelayMethod()
    {
        Debug.Log("敵を召喚しました。");

        // UnitEnemyプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("UnitEnemy");

        GameObject obj_clone = Instantiate(obj, new Vector3(100, Random.Range(-100f, 100.0f), 0.0f), Quaternion.identity);

        obj_clone.transform.SetParent(this.canvas.transform, false);
    }

    void DelayMethod2()
    {
        Debug.Log("敵を召喚しました。");

        // UnitEnemyプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("UnitEnemy2");

        GameObject obj_clone = Instantiate(obj, new Vector3(100, Random.Range(-100f, 100.0f), 0.0f), Quaternion.identity);

        obj_clone.transform.SetParent(this.canvas.transform, false);
    }

    void DelayMethod3()
    {
        Debug.Log("敵を召喚しました。");

        // UnitEnemyプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("UnitEnemy3");

        GameObject obj_clone = Instantiate(obj, new Vector3(100, Random.Range(-100f, 100.0f), 0.0f), Quaternion.identity);

        obj_clone.transform.SetParent(this.canvas.transform, false);
    }

    void DelayMethod4()
    {
        Debug.Log("敵を召喚しました。");

        // UnitEnemyプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("UnitEnemy4");

        GameObject obj_clone = Instantiate(obj, new Vector3(100, Random.Range(-100f, 100.0f), 0.0f), Quaternion.identity);

        obj_clone.transform.SetParent(this.canvas.transform, false);
    }

    void DelayMethod5()
    {
        Debug.Log("敵を召喚しました。");

        // UnitEnemyプレハブをGameObject型で取得
        GameObject obj = (GameObject)Resources.Load("UnitEnemy5");

        GameObject obj_clone = Instantiate(obj, new Vector3(100, Random.Range(-100f, 100.0f), 0.0f), Quaternion.identity);

        obj_clone.transform.SetParent(this.canvas.transform, false);
    }

    private void OnDestroy()
    {
        // Destroy時に登録したInvokeをすべてキャンセル
        CancelInvoke();
    }
}
