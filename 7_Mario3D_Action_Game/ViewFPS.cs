using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewFPS : MonoBehaviour
{
    /// <summary>
    /// FPSを表示するコード
    /// </summary>
    [SerializeField]
    private float Interval = 0.1f;

    public Text _tex;

    private float _time_cnt;
    private int _frames;
    private float _time_mn;
    private float _fps;

    // FPSの表示と計算
    private void Update()
    {
        _time_mn -= Time.deltaTime;
        _time_cnt += Time.timeScale / Time.deltaTime;
        _frames++;

        if (0 < _time_mn) return;

        _fps = _time_cnt / _frames;
        _time_mn = Interval;
        _time_cnt = 0;
        _frames = 0;

        _tex.text = "FPS: " + _fps.ToString("f2");
    }
}
