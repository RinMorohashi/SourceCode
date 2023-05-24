using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startscript : MonoBehaviour
{
    /// <summary>
    /// 現在のステージ番号によってステージ背景や床オブジェクトを変更するスクリプト
    /// </summary>
    public GameObject startbutton;
    public static startscript instance = null;
    public bool startSwitch = false;
    private GameObject manual;
    private GameObject manual2;

    public int stageNum;//1なら草原、2なら砂漠、3なら宇宙を表示
    public bool isCoolTime;
    public GameObject stage1Obj;
    public GameObject stage2Obj;
    public GameObject stage3Obj;
    public Text manualText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (!startSwitch)
        {
            manual = GameObject.Find("Manual");
            manual2 = GameObject.Find("BGMText");
            manual.SetActive(false);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        stage1Obj = GameObject.Find("Background");
        stage2Obj = GameObject.Find("Background2");
        stage3Obj = GameObject.Find("Background3");
        if (manual != null)
        {
            manualText = manual.GetComponent<Text>();
        }
        else
        {
            Debug.Log("manualが見つからない");
        }

        switch (stageNum)
        {
            case 1:
                stage1Obj.SetActive(true);
                stage2Obj.SetActive(false);
                stage3Obj.SetActive(false);
                break;
            case 2:
                stage1Obj.SetActive(false);
                stage2Obj.SetActive(true);
                stage3Obj.SetActive(false);
                break;
            case 3:
                stage1Obj.SetActive(false);
                stage2Obj.SetActive(false);
                stage3Obj.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("StartBButton") > 0 || Input.GetAxisRaw("Button2") > 0 || Input.GetAxisRaw("Button3") > 0 || Input.GetAxisRaw("Button0") > 0) && !startSwitch)
        {
            Debug.Log("動いてる？");
            OnClickStartButton();
        }
    }

    public void OnClickStartButton()
    {
        Time.timeScale = 1;
        startSwitch = true;
        startbutton = GameObject.Find("StartButton");
        startbutton.SetActive(false);
        manual.SetActive(true);
        manual2.SetActive(true);
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log("シーンが変わったよ！");
        GameObject aa = GameObject.Find("StartButton");
        if (aa != null)
        {
            Debug.Log("スターとボタン捕まえたよ！");
        }
        if (startSwitch)
        {
            aa.SetActive(false);
        }
    }

    public void startRecast()
    {
        StartCoroutine("recast");
    }
    IEnumerator recast()
    {
        yield return new WaitForSeconds(0.5f);
        isCoolTime = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            Debug.Log("呼ばれたよ！");
            stage1Obj = GameObject.Find("Background");
            stage2Obj = GameObject.Find("Background2");
            stage3Obj = GameObject.Find("Background3");
            //ステージ３ならテキストを白くする
            manual = GameObject.Find("Manual");
            manual2 = GameObject.Find("BGMText");
            switch (stageNum)
            {
                case 1:
                    stage1Obj.SetActive(true);
                    stage2Obj.SetActive(false);
                    stage3Obj.SetActive(false);
                    manual.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1);
                    manual2.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1);
                    break;
                case 2:
                    stage1Obj.SetActive(false);
                    stage2Obj.SetActive(true);
                    stage3Obj.SetActive(false);
                    break;
                case 3:
                    stage1Obj.SetActive(false);
                    stage2Obj.SetActive(false);
                    stage3Obj.SetActive(true);
                    manual.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0.8f, 1);
                    manual2.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0.8f, 1);
                    break;
                default:
                    break;
            }
        }
        
    }
}
