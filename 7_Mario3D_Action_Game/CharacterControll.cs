using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControll : MonoBehaviour
{
    /// <summary>
    /// マリオ（プレイヤー）の動き、視点やUI、効果音やBGMの制御をするコード
    /// </summary>
    public float gravity;
    public float mainSPEED;
    public float jumpSpeed;
    public float x_sensi;
    public float y_sensi;
    public new GameObject camera;
    public Vector3 cameraAngle;
    [SerializeField] GameObject head;
    public Rigidbody rb;

    public float sin;
    public float cos;

    private bool jumpSwitch = false;
    private bool jumpRecast = false;

    public GameObject arm;
    public GameObject mario;
    private Animator anim;
    [Header("ジャンプの速さ表現")] public AnimationCurve jumpCurve;
    private float jumpTime = 0;
    [Header("勝利テキスト")] public GameObject winText;
    [Header("ゲームクリアSE")] public AudioClip winSE;
    [Header("失敗SE")] public AudioClip failSE;
    public bool isClear = false;
    public AudioSource odsc;
    public AudioSource odscjump;
    public AudioSource odscBGM;
    [Header("ジャンプするときのSE")] public AudioClip jumpSE;
    [Header("走るときのSE")] public AudioClip runSE;
    public bool isRunning = false;
    [Header("コイン獲得SE")] public AudioClip coinSE;
    public int CoinGot = 0;
    [Header("UI上のコインの画像１（未取得）")] public GameObject Coin1Empty;
    [Header("UI上のコインの画像２（未取得）")] public GameObject Coin2Empty;
    [Header("UI上のコインの画像３（未取得）")] public GameObject Coin3Empty;
    [Header("UI上のコインの画像４（未取得）")] public GameObject Coin4Empty;
    [Header("UI上のコインの画像５（未取得）")] public GameObject Coin5Empty;
    [Header("UI上のコインの画像６（未取得）")] public GameObject Coin6Empty;
    [Header("UI上のコインの画像７（未取得）")] public GameObject Coin7Empty;
    [Header("UI上のコインの画像８（未取得）")] public GameObject Coin8Empty;
    [Header("UI上のコインの画像１（取得済）")] public GameObject Coin1Got;
    [Header("UI上のコインの画像２（取得済）")] public GameObject Coin2Got;
    [Header("UI上のコインの画像３（取得済）")] public GameObject Coin3Got;
    [Header("UI上のコインの画像４（取得済）")] public GameObject Coin4Got;
    [Header("UI上のコインの画像５（取得済）")] public GameObject Coin5Got;
    [Header("UI上のコインの画像６（取得済）")] public GameObject Coin6Got;
    [Header("UI上のコインの画像７（取得済）")] public GameObject Coin7Got;
    [Header("UI上のコインの画像８（取得済）")] public GameObject Coin8Got;

    public float x_Rotation;
    public float y_Rotation;
    public bool xPlusMove;
    public bool xMinusMove;
    public bool yPlusMove;
    public bool yMinusMove;
    public float JumpKey;

    public GameObject mainCameraPos;

    public float cameraHight;//カメラの高さ
    public float cameraDistance;//カメラとマリオの距離
    public float walkDirection;
    public bool isWalkTowardRight;
    public float wx;
    public float wy;

    // 障害物とするレイヤー
    [SerializeField]
    private LayerMask obstacleLayer;
    //　カメラの回転スピード
    [SerializeField]
    private float cameraRotateSpeed;

    [Header("コントローラを使う場合はtrueにしてください")]public bool isUseController;
    public bool isGround;
    public float fallTime;
    public float groundTime;
    public float moveTime;
    public Text paramater1;
    public bool diveRecast;
    public bool diveSwitch;
    public bool isDiving;
    public float diveXSpeed;
    public float diveZSpeed;

    public bool isMovable;
    public AudioClip landingSE;
    public AudioClip landingSE2;
    public AudioClip diveSE;
    public AudioClip screamSE;

    public RectTransform gameOverFadeImage;
    public GameObject gameOverFadeImageComp;
    public bool gameOverSwitch;

    public float flagTime;
    public GameObject[] flag;
    public GameObject flagParent;

    public int respownNum;
    public RectTransform respownPointUpdateText;

    public float clearTime;
    public Text[] ClearText;

    public float[] record;

    public Slider BGMSlider;
    public Slider SESlider;
    public Slider SensiSlider;

    [SerializeField] RectTransform marioOnMap;

    private Vector3 previousCameraPosition;
    private Vector3 previousCameraRotation;
    private float horizontalPushTime;
    private float verticalPushTime;

    private bool isSetting;
    [SerializeField] RectTransform settingButton;
    [SerializeField] RectTransform BGMButton;

    public bool isChecked;
    public Image mouseButtonImage;
    public Sprite[] checkSprite;

    void Awake()
    {
        Application.targetFrameRate = 30; //30FPSに設定
    }

    void Start()
    {
        anim = mario.GetComponent<Animator>();
        odscBGM.Play();

        record[0] = PlayerPrefs.GetFloat("SCORE1", 999.99f);
        record[1] = PlayerPrefs.GetFloat("SCORE2", 999.99f);
        record[2] = PlayerPrefs.GetFloat("SCORE3", 999.99f);
        odscBGM.volume = PlayerPrefs.GetFloat("BGMVolume", 0.03f);
        odsc.volume = PlayerPrefs.GetFloat("SEVolume", 0.5f);
        odscjump.volume = PlayerPrefs.GetFloat("SEVolume", 0.5f);
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        SESlider.value = PlayerPrefs.GetFloat("SEVolume", 0.3f);
        SensiSlider.value = PlayerPrefs.GetFloat("SensiVolume", 0.5f);
        if (PlayerPrefs.GetInt("isChecked", 0) == 1)
        {
            isChecked = true;
            mouseButtonImage.sprite = checkSprite[1];
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isChecked = false;
            mouseButtonImage.sprite = checkSprite[0];
        }
    }

    void Update()
    {
        odscBGM.volume = BGMSlider.value;
        odsc.volume = SESlider.value;
        odscjump.volume = SESlider.value;
        if (transform.position.y <= -10f && !gameOverSwitch)
        {
            gameOverSwitch = true;
            StartCoroutine("miss");
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //マリオのモデルの位置・回転を調整する
        mario.transform.position = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
        // カメラのy軸回転は０→１（１８０度）→０（３６０度）→ー１（５４０度）→０（７２０度）
        Quaternion q = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        float VerticalKey = Input.GetAxis("Vertical");
        float HorizontalKey = Input.GetAxis("Horizontal");
        
        if (HorizontalKey > 0)
        {
            rot = Quaternion.Euler(0, 90, 0);
        }
        if (HorizontalKey < 0)
        {
            rot = Quaternion.Euler(0, 270, 0);
        }
        if (VerticalKey < 0)
        {
            rot = Quaternion.Euler(0, 180, 0);

            if (HorizontalKey > 0)
            {
                rot = Quaternion.Euler(0, 135, 0);
            }
            else if (HorizontalKey < 0)
            {
                rot = Quaternion.Euler(0, 225, 0);
            }
        }
        if (VerticalKey > 0)
        {
            rot = Quaternion.Euler(0, 0, 0);

            if (HorizontalKey > 0)
            {
                rot = Quaternion.Euler(0, 45, 0);
            }
            else if (HorizontalKey < 0)
            {
                rot = Quaternion.Euler(0, 315, 0);
            }
        }

        if ((VerticalKey == 0 && HorizontalKey == 0))
        {

        }
        else if (isMovable)
        {
            mario.transform.rotation = q * rot;
        }
        movecon();
        if (!isGround)
        {
            fallTime += Time.deltaTime;
            anim.SetBool("landing", false);

            groundTime = 0;
        }
        else
        {
            if (isDiving)
            {
                odsc.PlayOneShot(landingSE);
                jumpTime = 0;
            }
            else if (fallTime >= 0.5f)
            {
                odsc.PlayOneShot(landingSE2);
            }
            fallTime = 0;
            anim.SetBool("landing", true);
            isDiving = false;
            diveSwitch = false;
            anim.SetBool("dive", false);

            groundTime += Time.deltaTime;
        }
        if (groundTime >= 0.5f)
        {
            isMovable = true;
        }
        paramater1.text = "fallTime：" + fallTime + "\ngroundTime：" + groundTime + "\njumpTime：" + jumpTime + "\nisDiving：" + isDiving + "\nDiveSwitch：" + diveSwitch + "\nisMovable：" + isMovable;
        
        flagTime += Time.deltaTime;
        if (flagTime < 0.2f)
        {
            flag[0].SetActive(true); flag[1].SetActive(false); flag[2].SetActive(false); flag[3].SetActive(false);
            flag[4].SetActive(true); flag[5].SetActive(false); flag[6].SetActive(false); flag[7].SetActive(false);
            flag[8].SetActive(true); flag[9].SetActive(false); flag[10].SetActive(false); flag[11].SetActive(false);
            flag[12].SetActive(true); flag[13].SetActive(false); flag[14].SetActive(false); flag[15].SetActive(false);
        }
        else if (flagTime >= 0.2f && flagTime < 0.4f)
        {
            flag[0].SetActive(false); flag[1].SetActive(true); flag[2].SetActive(false); flag[3].SetActive(false);
            flag[4].SetActive(false); flag[5].SetActive(true); flag[6].SetActive(false); flag[7].SetActive(false);
            flag[8].SetActive(false); flag[9].SetActive(true); flag[10].SetActive(false); flag[11].SetActive(false);
            flag[12].SetActive(false); flag[13].SetActive(true); flag[14].SetActive(false); flag[15].SetActive(false);
        }
        else if (flagTime >= 0.4f && flagTime < 0.6f)
        {
            flag[0].SetActive(false); flag[1].SetActive(false); flag[2].SetActive(true); flag[3].SetActive(false);
            flag[4].SetActive(false); flag[5].SetActive(false); flag[6].SetActive(true); flag[7].SetActive(false);
            flag[8].SetActive(false); flag[9].SetActive(false); flag[10].SetActive(true); flag[11].SetActive(false);
            flag[12].SetActive(false); flag[13].SetActive(false); flag[14].SetActive(true); flag[15].SetActive(false);
        }
        else if (flagTime >= 0.6f && flagTime < 0.8f)
        {
            flag[0].SetActive(false); flag[1].SetActive(false); flag[2].SetActive(false); flag[3].SetActive(true);
            flag[4].SetActive(false); flag[5].SetActive(false); flag[6].SetActive(false); flag[7].SetActive(true);
            flag[8].SetActive(false); flag[9].SetActive(false); flag[10].SetActive(false); flag[11].SetActive(true);
            flag[12].SetActive(false); flag[13].SetActive(false); flag[14].SetActive(false); flag[15].SetActive(true);
        }
        else if (flagTime >= 0.8f)
        {
            flagTime = 0;
        }
        
        if (!isClear)
        {
            clearTime += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isClear && isGround)
            {
                SceneManager.LoadScene("New Scene");
            }
        }

        if (!diveSwitch)//ジャンプの時間を進める
        {
            if (jumpSwitch)
            {
                jumpTime += Time.deltaTime * 2.5f;
                if (jumpTime >= 1.0f)
                {
                    jumpSwitch = false;
                    jumpTime = 0f;
                }
            }
            else if (jumpRecast)
            {
                jumpTime += Time.deltaTime * 2.5f;
                if (jumpTime >= 1.0f)
                {
                    jumpRecast = false;
                    jumpTime = 0f;
                    anim.SetBool("jump", false);
                }
            }
        }  
        
        if (diveSwitch)
        {
            jumpTime += Time.deltaTime * 7f;
            if (jumpTime >= 1.0f)
            {
                diveSwitch = false;
                jumpTime = 0f;
            }
        }
        else if (isDiving)
        {
            jumpTime += Time.deltaTime * 7f;
            if (jumpTime >= 1.0f)
            {
                diveRecast = false;
                jumpRecast = false;
                jumpTime = 0f;
                anim.SetBool("jump", false);
                anim.SetBool("dive", false);
            }
        }
        marioOnMap.anchoredPosition = new Vector2(transform.position.x * 2.3f, transform.position.z * 2.3f);
        marioOnMap.localEulerAngles = new Vector3(transform.localEulerAngles.x,180 + transform.localEulerAngles.z, transform.localEulerAngles.y);
    }

    void LateUpdate()
    {
        cameracon();
    }
    
    void movecon()
    {
        float VerticalKey = Input.GetAxis("Vertical");
        float HorizontalKey = Input.GetAxis("Horizontal");
            float JumpKey = Input.GetAxis("Jump");
        float XSpeed = 0;
        float ZSpeed;
        float shinXSpeed;
        float shinZSpeed;
        sin = Mathf.Sin((transform.localEulerAngles.y) * (2 * Mathf.PI)/ 360);
        cos = Mathf.Cos((transform.localEulerAngles.y) * (2 * Mathf.PI) / 360);
        if ((yPlusMove || VerticalKey > 0) && isMovable && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))
        {
            ZSpeed = mainSPEED;
        }
        else if ((yMinusMove || VerticalKey < 0) && isMovable && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))
        {
            ZSpeed = -1 * mainSPEED;
        }
        else
        {
            ZSpeed = 0;
        }
        
        if ((xPlusMove || HorizontalKey > 0) && isMovable && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))
        {
            XSpeed = mainSPEED;
        }
        else if ((xMinusMove || HorizontalKey < 0) && isMovable && (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)))
        {
            XSpeed = -1 * mainSPEED;
        }
        else
        {
            XSpeed = 0;
        }
        
        shinXSpeed = (ZSpeed * sin) + (XSpeed * cos);
        shinZSpeed = (XSpeed * sin * -1) + (ZSpeed * cos);
        if (!isDiving)
        {
            float sinM = Mathf.Sin((transform.localEulerAngles.y - 30) * (2 * Mathf.PI) / 360);
            float cosM = Mathf.Cos((transform.localEulerAngles.y - 30) * (2 * Mathf.PI) / 360);
            sinM = Mathf.Sin((transform.localEulerAngles.y + mario.transform.localEulerAngles.y - 40) * (2 * Mathf.PI) / 360);
            cosM = Mathf.Cos((transform.localEulerAngles.y + mario.transform.localEulerAngles.y - 40) * (2 * Mathf.PI) / 360);
            diveXSpeed = (mainSPEED * sinM) + (mainSPEED * cosM);
            diveZSpeed = (mainSPEED * sinM * -1) + (mainSPEED * cosM);
        }
        if (!jumpSwitch && !isDiving)
        {
            rb.velocity = new Vector3(shinXSpeed, -jumpSpeed * jumpCurve.Evaluate(1 - jumpTime), shinZSpeed);
        }
        else if (jumpSwitch)
        {
            rb.velocity = new Vector3(shinXSpeed, jumpSpeed * jumpCurve.Evaluate(jumpTime), shinZSpeed);
        }
        else if (isDiving)
        {
            rb.velocity = new Vector3(diveXSpeed, 0.5f * jumpSpeed * jumpCurve.Evaluate(jumpTime), diveZSpeed);
            if (diveSwitch)
            {
                rb.velocity = new Vector3(diveXSpeed * 1.5f, 0.5f * jumpSpeed * jumpCurve.Evaluate(jumpTime), diveZSpeed * 1.5f);
            }
        }
        if (!jumpRecast && !diveRecast)
        {
            rb.velocity = new Vector3(shinXSpeed, -gravity * fallTime, shinZSpeed);
        }
        if (!jumpRecast && !diveRecast && isDiving)
        {
            rb.velocity = new Vector3(diveXSpeed, -gravity * fallTime, diveZSpeed);
            if (diveSwitch)
            {
                rb.velocity = new Vector3(diveXSpeed * 1.5f, 0.5f * jumpSpeed * jumpCurve.Evaluate(jumpTime), diveZSpeed * 1.5f);
            }
        }
        Debug.Log("jumpKeyは" + JumpKey);
        if (JumpKey > 0)
        {
            if (!jumpRecast && fallTime < 0.0001f)
            {
                StartCoroutine("Jump");
                jumpRecast = true;
                jumpSwitch = true;
            }
            else if (fallTime > 0.3f && !diveRecast && !isDiving)//jumpRecastがtrue,すなわちジャンプ中のとき
            {
                StopCoroutine("Jump");
                StartCoroutine("Dive");
                //jumpRecast = false;
                jumpSwitch = false;
                diveRecast = true;
                diveSwitch = true;
            }
        }
        if ((VerticalKey == 0 && HorizontalKey == 0))
        {
            anim.SetBool("run", false);
            if (isRunning)
            {
                odscjump.Stop();
                isRunning = false;
            }
            moveTime = 0;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            anim.SetBool("run", true);
            if (!isRunning)
            {
                float rnd = Random.Range(0.1f, 4.9f);
                odscjump.time = rnd;
                odscjump.Play();
                isRunning = true;
            }
            moveTime += Time.deltaTime;
        }
        else
        {
            odscjump.Stop();
            anim.SetBool("run", false);
        }
    }

    void cameracon()
    {
        float HorizontalKey = Input.GetAxis("Horizontal");
        if (HorizontalKey > 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (horizontalPushTime < 1)
            {
                x_Rotation = cameraRotateSpeed * SensiSlider.value * Time.deltaTime * horizontalPushTime;
            }
            else
            {
                x_Rotation = cameraRotateSpeed * SensiSlider.value * Time.deltaTime;
            }
            horizontalPushTime += Time.deltaTime * 2;
        }
        else if (HorizontalKey < 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (horizontalPushTime < 1)
            {
                x_Rotation = -1f * cameraRotateSpeed * SensiSlider.value * Time.deltaTime * horizontalPushTime;
            }
            else
            {
                x_Rotation = -1f * cameraRotateSpeed * SensiSlider.value * Time.deltaTime;
            }
            horizontalPushTime += Time.deltaTime * 2;
        }
        else
        {
            x_Rotation *= 0.4f;
            horizontalPushTime = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (verticalPushTime < 1)
            {
                y_Rotation = cameraRotateSpeed * SensiSlider.value * Time.deltaTime * verticalPushTime;
            }
            else
            {
                y_Rotation = cameraRotateSpeed * SensiSlider.value * Time.deltaTime;
            }
            verticalPushTime += Time.deltaTime * 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (verticalPushTime < 1)
            {
                y_Rotation = -1f * cameraRotateSpeed * SensiSlider.value * Time.deltaTime * verticalPushTime;
            }
            else
            {
                y_Rotation = -1f * cameraRotateSpeed * SensiSlider.value * Time.deltaTime;
            }
            verticalPushTime += Time.deltaTime * 2;
        }
        else
        {
            y_Rotation *= 0.5f;
            verticalPushTime = 0;
        }
        if (isChecked)
        {
            x_Rotation = Input.GetAxis("Mouse X");
            y_Rotation = Input.GetAxis("Mouse Y");
            x_Rotation *= 1f * SensiSlider.value * Time.deltaTime * 100;
            y_Rotation *= 1f * SensiSlider.value * Time.deltaTime * 100;
        }

        //プレイヤーを回転→子オブジェクトのカメラも回転？
        this.transform.Rotate(0, x_Rotation, 0);
        //カメラを回転
        camera.transform.Rotate(-y_Rotation, 0, 0);
        cameraAngle = camera.transform.localEulerAngles;
        if (cameraAngle.x < 280 && cameraAngle.x > 180)
        {
            cameraAngle.x = 280;
        }
        if (cameraAngle.x > 45 && cameraAngle.x < 180)
        {
            cameraAngle.x = 45;
        }
        cameraAngle.y = 0;
        cameraAngle.z = 0;

        //カメラを回転？
        camera.transform.localEulerAngles = cameraAngle;

        if (cameraAngle.x < 180)
        {
            cameraHight = cameraAngle.x / 3f;
        }
        else
        {
            cameraHight = 0;
        }
        //カメラを移動
        mainCameraPos.transform.position = new Vector3(this.transform.position.x - 20 * sin, this.transform.position.y + cameraHight + 3, this.transform.position.z - 20 * cos);
        RaycastHit hit;
        //キャラクターとカメラの間に障害物があったら障害物の位置にカメラを移動させる
        if (Physics.Linecast(transform.position, camera.transform.position, out hit, obstacleLayer))
        {
            camera.transform.position = hit.point;
        }
        //　レイを視覚的に確認
        Debug.DrawLine(transform.position, camera.transform.position, Color.red, 0f, false);
        Quaternion a = Quaternion.Euler(previousCameraRotation.x, previousCameraRotation.y, previousCameraRotation.z);
        Quaternion b = Quaternion.Euler(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
        previousCameraPosition = camera.transform.position;
        previousCameraRotation = new Vector3(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
    }

    IEnumerator Jump()
    {
        odsc.PlayOneShot(jumpSE);
        anim.SetBool("jump", true);
        diveRecast = false;
        yield return new WaitForSeconds(0.03f);
    }
    IEnumerator Dive()
    {
        isMovable = false;
        isDiving = true;
        odsc.PlayOneShot(diveSE);
        anim.SetBool("dive", true);
        jumpTime = 0f;
        yield return new WaitForSeconds(0.03f);
    }

    public void GetCoin()
    {
        odsc.PlayOneShot(coinSE);
        switch (CoinGot)
        {
            case 1:
                Coin1Empty.SetActive(false);
                Coin1Got.SetActive(true);
                break;
            case 2:
                Coin2Empty.SetActive(false);
                Coin2Got.SetActive(true);
                break;
            case 3:
                Coin3Empty.SetActive(false);
                Coin3Got.SetActive(true);
                break;
            case 4:
                Coin4Empty.SetActive(false);
                Coin4Got.SetActive(true);
                break;
            case 5:
                Coin5Empty.SetActive(false);
                Coin5Got.SetActive(true);
                break;
            case 6:
                Coin6Empty.SetActive(false);
                Coin6Got.SetActive(true);
                break;
            case 7:
                Coin7Empty.SetActive(false);
                Coin7Got.SetActive(true);
                break;
            case 8:
                Coin8Empty.SetActive(false);
                Coin8Got.SetActive(true);
                winText.SetActive(true);
                odsc.PlayOneShot(winSE);
                isClear = true;
                ClearText[0].text = "クリア時間：" + clearTime.ToString("N2") + "[秒]";
                ClearText[1].text = "クリア時間：" + clearTime.ToString("N2") + "[秒]";

                if (clearTime <= record[0])
                {
                    PlayerPrefs.SetFloat("SCORE1", clearTime);
                    PlayerPrefs.SetFloat("SCORE2", record[0]);
                    PlayerPrefs.SetFloat("SCORE3", record[1]);
                }
                else if (clearTime <= record[1])
                {
                    PlayerPrefs.SetFloat("SCORE2", clearTime);
                    PlayerPrefs.SetFloat("SCORE3", record[1]);
                }
                else if (clearTime <= record[2])
                {
                    PlayerPrefs.SetFloat("SCORE3", clearTime);
                }
                PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
                PlayerPrefs.SetFloat("SEVolume", SESlider.value);
                PlayerPrefs.SetFloat("SensiVolume", SensiSlider.value);
                PlayerPrefs.SetInt("isClear", 1);
                PlayerPrefs.Save();
                break;
            default:
                break;
        }
    }

    IEnumerator RespownPointUpdate()
    {
        odsc.PlayOneShot(coinSE);
        for (int i = 1; i < 11; i++)
        {
            respownPointUpdateText.anchoredPosition = new Vector2(0, -320 + i * 10);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 1; i < 11; i++)
        {
            respownPointUpdateText.anchoredPosition = new Vector2(0, -220 - i * 10);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator miss()
    {
        odsc.PlayOneShot(screamSE);
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i < 21; i++)
        {
            gameOverFadeImage.localScale = new Vector3(1.05f -(i / 20f) ,1.05f - (i / 20f), 1);
            yield return new WaitForSeconds(0.05f);
        }
        gameOverFadeImageComp.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameOverFadeImage.localScale = new Vector3(1, 1, 1);
        gameOverFadeImageComp.SetActive(false);
        odsc.PlayOneShot(failSE);
        if (respownNum == 0)
        {
            transform.position = new Vector3(0, 1.5f, 0);
        }
        else if (respownNum == 1)
        {
            transform.position = new Vector3(0, 35, 5);
        }
        else if (respownNum == 2)
        {
            transform.position = new Vector3(20, 60, 30);
        }
        gameOverSwitch = false;
    }

    public void OnClickSettingButton()
    {
        if (isSetting)
        {
            isSetting = false;
            StartCoroutine("SettingShowDown");
            if (isChecked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.lockState = CursorLockMode.Confined;
            }
        }
        else
        {
            isSetting = true;
            StartCoroutine("SettingShowUp");
        }
    }
    IEnumerator SettingShowUp()
    {
        for (int i = 1; i < 11; i++)
        {
            settingButton.anchoredPosition = new Vector2(440 - 26f * i,220);
            BGMButton.anchoredPosition = new Vector2(650 - 26f * i, 240);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator SettingShowDown()
    {
        for (int i = 9; i > -1; i--)
        {
            settingButton.anchoredPosition = new Vector2(440 - 26f * i, 220);
            BGMButton.anchoredPosition = new Vector2(650 - 26f * i, 240);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnClickMouseButton()
    {
        if (isChecked)
        {
            isChecked = false;
            mouseButtonImage.sprite = checkSprite[0];
            odsc.PlayOneShot(coinSE);
            PlayerPrefs.SetInt("isChecked", 0);
        }
        else
        {
            isChecked = true;
            mouseButtonImage.sprite = checkSprite[1];
            odsc.PlayOneShot(coinSE);
            PlayerPrefs.SetInt("isChecked", 1);
        }
        PlayerPrefs.Save();
    }
    public void OnClickNowhre()
    {
        if (isSetting)
        {
            isSetting = false;
            StartCoroutine("SettingShowDown");
            if (isChecked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
