using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 一定時間ごとに障害物を召喚するスクリプト
    /// </summary>
    public float timerOfEntire = 0;
    private float timerRock;
    private float timerShark;
    private float timerOrca;
    private float timerWhale;
    private float timerAccel;
    private GameObject rock;
    private GameObject shark;
    private GameObject orca;
    private GameObject whale;
    private GameObject accel;
    private GameObject Player;
    private player PlayerScript;
    private bool orcaSwitch;
    void Start()
    {
        rock = (GameObject)Resources.Load("rock");
        shark = (GameObject)Resources.Load("shark");
        orca = (GameObject)Resources.Load("orca");
        whale = (GameObject)Resources.Load("whale");
        accel = (GameObject)Resources.Load("AccelEffect");
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timerOfEntire += Time.deltaTime;
        if (!PlayerScript.isGameOver && !PlayerScript.isStop)
        {
            timerRock += Time.deltaTime;
            timerShark += Time.deltaTime;
            timerOrca += Time.deltaTime;
            timerWhale += Time.deltaTime;
            float tScale = 100f / PlayerScript.Velocity / 2f;
            if (timerRock >= 1.5f && tScale >= 0 && timerOfEntire <= 10f)
            {
                Instantiate(rock, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerRock = 0;
            }
            if (timerRock >= 1.1f && tScale >= 0 && timerOfEntire >= 10f && timerOfEntire <= 20f)
            {
                Instantiate(rock, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerRock = 0;
            }
            if (timerRock >= 0.6f && tScale >= 0 && timerOfEntire >= 20f && timerOfEntire <= 30f)
            {
                Instantiate(rock, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerRock = 0;
            }
            if (timerRock >= 0.6f && tScale >= 0 && timerOfEntire >= 30f && !orcaSwitch)
            {
                Instantiate(rock, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerRock = 0;
            }
            if (timerRock >= 0.75f && tScale >= 0 && orcaSwitch)
            {
                Instantiate(rock, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerRock = 0;
            }
            if (timerShark >= 4.0f && tScale >= 0 && (timerOfEntire >= 30f || PlayerScript.score >= 1000))
            {
                Instantiate(shark, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerShark = 0;
            }
            if (timerOrca >= 6.0f && tScale >= 0 && (timerOfEntire >= 60f || PlayerScript.score >= 2000))
            {
                if (!orcaSwitch)
                {
                    orcaSwitch = true;
                }
                Instantiate(orca, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerOrca = 0;
            }
            if (timerWhale >= 15.0f && tScale >= 0 && (timerOfEntire >= 90f || PlayerScript.score >= 3000))
            {
                Instantiate(whale, new Vector3(20.0f, 2.0f, 0.0f), Quaternion.identity);
                timerWhale = 0;
            }

            if (PlayerScript.RightbuttonFlag || PlayerScript.isRight)
            {
                timerAccel += Time.deltaTime;
                if (timerAccel >= 0.3f)
                {
                    Instantiate(accel, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
                    timerAccel = 0;
                }
            }
        }
    }
}
