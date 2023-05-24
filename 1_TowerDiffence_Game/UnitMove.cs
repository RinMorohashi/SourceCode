using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitMove : MonoBehaviour, IEndDragHandler
{
    /// <summary>
    /// ユニットのステータスを記録し、攻撃や死亡などのアクションを行うスクリプト
    /// </summary>
    
    //インスペクターで設定する
    public bool isEnemy = false;

    GameObject unitField; //UnitFieldそのものが入る変数
    public DropPlace dropPlace;
    public bool isActive = false;
    public bool possession; //所持しているか

    public float speed;
    public int MaxHP;
    public int HP;
    public int attack;
    public float IDnumber; //ユニットの種類
    public int deckNumber; //ユニットの番号
    public int deckPosition; //デッキでのアイコンの位置
    private Rigidbody2D rb;

    private bool firstDrag = true;
    public GameObject hpBar;
    public GameObject canvas;
    public GameObject cardDeck;
    public GameObject archer; //Archer要素
    public GameObject guardian; // Guardian要素

    Vector2 tmp2;

    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        rb = GetComponent<Rigidbody2D>();

        unitField = GameObject.Find("UnitField"); //UnitFieldをオブジェクトの名前から取得して変数に格納する
        dropPlace = unitField.GetComponent<DropPlace>(); //UnitFieldの中にあるDropPlaceを取得して変数に格納する

        //矢を発射する文SummonArrowを3.5秒後に呼び出し、以降は１秒毎に実行
        if (IDnumber == 3)
        {
            InvokeRepeating(nameof(SummonArrow), 0.1f, 1.0f);
        }
        else if (IDnumber == 4)
        {
            InvokeRepeating(nameof(SummonSword), 1.0f, 2.0f);
        }

        //Canvasを拾う
        canvas = GameObject.Find("Canvas");
        //CardDeckを拾う
        cardDeck = GameObject.Find("CardDeck");
    }

    public void OnEndDrag(PointerEventData eventData) // カードを離したときに行う処理
    {
        Debug.Log("ドロップされました");

        if(firstDrag == true)
        {
            if (IDnumber == 1)
            {
                GameObject obj = (GameObject)Resources.Load("Card");

                GameObject obj_clone = Instantiate(obj, new Vector3(-180f, 0.0f, 0.0f), Quaternion.identity);

                obj_clone.transform.SetParent(this.cardDeck.transform, false);
            }
            else if (IDnumber == 2)
            {
                switch (StageCounter.deckNumber2)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card2");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card2");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card2");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card2");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }
            else if (IDnumber == 3)
            {
                switch (StageCounter.deckNumber3)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card3");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card3");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card3");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card3");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }
            else if (IDnumber == 4)
            {
                tmp2 = this.transform.position;

                switch (StageCounter.deckNumber4)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card4");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card4");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card4");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card4");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }
            else if (IDnumber == 5)
            {
                switch (StageCounter.deckNumber5)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card5");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card5");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card5");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card5");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }
            else if (IDnumber == 6)
            {
                switch (StageCounter.deckNumber6)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card6");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card6");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card6");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card6");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }
            else if (IDnumber == 7)
            {
                switch (StageCounter.deckNumber7)
                {
                    case 2:
                        GameObject obj = (GameObject)Resources.Load("Card7");
                        GameObject obj_clone = Instantiate(obj, new Vector3(-90f, 0.0f, 0.0f), Quaternion.identity);
                        obj_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 3:
                        GameObject obj2 = (GameObject)Resources.Load("Card7");
                        GameObject obj2_clone = Instantiate(obj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                        obj2_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 4:
                        GameObject obj3 = (GameObject)Resources.Load("Card7");
                        GameObject obj3_clone = Instantiate(obj3, new Vector3(90f, 0.0f, 0.0f), Quaternion.identity);
                        obj3_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                    case 5:
                        GameObject obj4 = (GameObject)Resources.Load("Card7");
                        GameObject obj4_clone = Instantiate(obj4, new Vector3(180f, 0.0f, 0.0f), Quaternion.identity);
                        obj4_clone.transform.SetParent(this.cardDeck.transform, false);
                        break;
                }
            }

            firstDrag = false;
        }

        // コルーチンの起動
        StartCoroutine(DelayCoroutine());
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 1フレーム待つ
        for (var i = 0; i < 1; i++)
        {
            yield return null;
        }

        isActive = dropPlace.activation;
        Debug.Log("activationを参照しました。"+isActive+"でした。");//ここでfalseならこれを破壊する
        if (isActive == false)
        {
            Destroy(this.gameObject); 
        }
        else
        {
            hpBar.SetActive(true);
        }
    }

    void SummonArrow()
    {
        if (isActive == true)
        {
            Debug.Log("矢を召喚しました。");
            //矢を生成するコード
            GameObject arrow = (GameObject)Resources.Load("Arrow");
            GameObject arrow_clone = Instantiate(arrow, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            arrow_clone.transform.SetParent(archer.transform, false);
        }
    }

    void SummonSword()
    {
        if (isActive == true)
        {
            Debug.Log("剣を召喚しました。");
            //矢を生成するコード
            GameObject arrow = (GameObject)Resources.Load("Sword");
            GameObject arrow_clone = Instantiate(arrow, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            arrow_clone.transform.SetParent(guardian.transform, false);
        }
    }

    private void OnDestroy()
    {
        // Destroy時に登録したInvokeをすべてキャンセル
        CancelInvoke();
    }

    void FixedUpdate()
    {
        if (isActive == true)
        {

            if (isEnemy == false)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (IDnumber == 4)
            {
                rb.velocity = new Vector2(0, 0);
                float sin2 = Mathf.Sin(tmp2.y + Time.time);
                this.transform.localPosition = new Vector2(-300, sin2 * 100);
            }
        }

            //破壊
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "とぶつかりました。");

        if (collision.gameObject.CompareTag("EnemyUnit"))
        {
            //自身のHPを減らす処理
            HP -= collision.gameObject.GetComponent<UnitMoveEnemy>().enemyAttack;
        }
        if (collision.gameObject.CompareTag("EnemyGuardian"))
        {
            //自身のHPを減らす処理
            HP -= collision.gameObject.GetComponent<EnemyGuardianMove>().enemyAttack;
        }

        if (isEnemy == false)
        {
            if (collision.gameObject.CompareTag("FrontLine"))
            {

            }
            else if (collision.gameObject.CompareTag("Unit"))
            {

            }
            else if (IDnumber == 3)
            {

            }
            else if (IDnumber == 4)
            {

            }
            else
            {
                transform.position += new Vector3(-50, 0, 0);
            }
        }
        else
        {
            transform.position += new Vector3(50, 0, 0);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "と離れました。");
    }
}
