using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    /// <summary>
    /// BGM、効果音のボリュームを調整するスクリプト
    /// </summary>
    public float bgmVolume;
    public float SEVolume;
    public static soundManager instance;
    public Slider BGMSlider;
    public Slider SESlider;
    public AudioSource astitle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BGMSlider.value = bgmVolume; SESlider.value = SEVolume;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (BGMSlider != null)
        {
            bgmVolume = BGMSlider.value;
        }
        if (SESlider != null)
        {
            SEVolume = SESlider.value;
        }
        if (astitle != null)
        {
            astitle.volume = bgmVolume;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("シーンがロードされたよ！");
        GameObject bgm1 = GameObject.Find("BGM");
        GameObject bgm2 = GameObject.Find("BGM2");
        GameObject bgm3 = GameObject.Find("BGM3");
        GameObject gamemanager = GameObject.Find("GameManager");
        if (bgm1 != null)
        {
            AudioSource as1 = bgm1.GetComponent<AudioSource>();
            as1.volume = bgmVolume;
        }
        if (bgm2 != null)
        {
            AudioSource as2 = bgm2.GetComponent<AudioSource>();
            as2.volume = bgmVolume;
        }
        if (bgm3 != null)
        {
            AudioSource as3 = bgm3.GetComponent<AudioSource>();
            as3.volume = bgmVolume;
        }
        if (gamemanager != null)
        {
            AudioSource as4 = gamemanager.GetComponent<AudioSource>();
            as4.volume = SEVolume;
        }
        GameObject gameManagerTitle = GameObject.Find("gameManagerTitle");
        if (gameManagerTitle != null)
        {
            astitle = gameManagerTitle.GetComponent<AudioSource>();
        }
    }
}
