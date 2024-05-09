using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Rigidbody
    private Rigidbody2D rb;

    //プレイヤーの見た目のオブジェクト
    public GameObject PlayerSkinObject;

    //ジャンプできるか確認するためのオブジェクト
    public GameObject JumpChecker;

    //ヒップドロップで敵を倒す判定のオブジェクト
    public GameObject DropObject;

    //体力表示用のオブジェクト
    public GameObject[] HpObject;

    //ジャンプの高さ関係
    [SerializeField] public float DefaultJumpForce;
    [SerializeField] public float PlusJumpForce;
    private float JumpForce;

    //速さ関係
    [SerializeField] public float DefaultSpeed;
    [SerializeField] public float PlusSpeed;
    private float Speed;

    //大きさ関係(ステージの構成的にヒップドロップの範囲強化のほうが良さそうと提案)
    [SerializeField] public float DefaultSize;
    [SerializeField] public float PlusSize;
    private float Size;

    //体力関係
    private int DefaultHp=2;
    [SerializeField] public int PlusHp;
    private int Hp;

    //空中に居るかの判定
    private int JumpCount;

    //壁に触れているかの判定
    private bool OnWall;

    //連続壁ジャンプをしないようにする
    private bool DoubleWall;

    //ヒップドロップをしているかの判定
    public static bool Drop;

    //ダメージを受けているかの確認
    private bool blink;
    private bool blinkCheck;
    float blinkCount;

    //ダメージを受けた後の無敵時間
    //invincibleTime*0.05秒無敵時間(invincibleTime==8なら0.4秒)　ステータスに入れてもいいかも
    public int invincibleTime;
    int invincibleTimeCheck;

    // Start is called before the first frame update
    void Start()
    {
        blink = false;
        blinkCheck = false;
        rb = GetComponent<Rigidbody2D>();
        PlayerSkinObject.SetActive(true);
        DropObject.SetActive(false);
        blinkCount = 0;
        invincibleTimeCheck = 0;


        //ステータスを入力
        JumpForce = DefaultJumpForce + PlusJumpForce;
        Speed = DefaultSpeed + PlusSpeed;
        Size = DefaultSize + PlusSize;
        Hp = DefaultHp + PlusHp;

        for(int i = 0; i < 5; i++)
        {
            HpObject[i].SetActive(false);
        }
        for (int i = 0; i < Hp; i++)
        {
            HpObject[i].SetActive(true);
        }

        ButtonManager.sceneCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DoubleWall);

        if (!ButtonManager.sceneCheck)
        {
            //ジャンプ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (JumpCount == 0)
                {
                    //壁での連続ジャンプ防止
                    if (OnWall)
                    {
                        if (!DoubleWall)
                        {
                            rb.velocity = new Vector3(0, JumpForce, 0);
                            DoubleWall = true;
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector3(0, JumpForce, 0);
                    }
                }
                
            }
            //壁めり込み防止
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                OnWall = false;
            }
            //左移動
            if (Input.GetKey(KeyCode.A))
            {
                PlayerSkin.rota = 1;
                //壁に触れたまま移動しない
                if (!OnWall)
                {
                    //ヒップドロップ中に移動しない
                    if (!Drop)
                    {
                        this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
                    }
                }
            }
            //右移動
            if (Input.GetKey(KeyCode.D))
            {
                PlayerSkin.rota = -1;
                //壁に触れたまま移動しない
                if (!OnWall)
                {
                    //ヒップドロップ中に移動しない
                    if (!Drop)
                    {
                        this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
                    }
                }
            }
            //ヒップドロップ
            if (Input.GetKeyDown(KeyCode.S))
            {
                //空中にいるとき
                if (JumpCount == 1)
                {
                    PlayerSkin.Rota = false;
                    PlayerSkin.rota = 0;
                    rb.velocity = new Vector3(0, -JumpForce * 2, 0);
                    Drop = true;
                }
            }
            //ジャンプ可能か確認用オブジェクトの表示非表示
            if (JumpCount == 0)
            {
                if (!DoubleWall)
                {
                    JumpChecker.SetActive(true);
                }
                else
                {
                    JumpChecker.SetActive(false);
                }
            }
            else
            {
                JumpChecker.SetActive(false);
            }
        }
        //ヒップドロップ中の判定
        if (Drop)
        {
            DropObject.SetActive(true);
        }
        else
        {
            DropObject.SetActive(false);
        }

        //ダメージを受けた時の点滅
        if (blink)
        {
            //点滅
            if (blinkCount>0.05f)
            {
                if (blinkCheck)
                {
                    PlayerSkinObject.SetActive(true);
                    blinkCheck = false;
                    invincibleTimeCheck++;
                }
                else
                {
                    PlayerSkinObject.SetActive(false);
                    blinkCheck = true;
                    invincibleTimeCheck++;
                }
                if (invincibleTimeCheck >= invincibleTime)
                {
                    blink = false;
                    invincibleTimeCheck = 0;
                }
                blinkCount = 0;
            }
            else
            {
                blinkCount += Time.deltaTime;
            }
        }
        if (!blink)
        {
            PlayerSkinObject.SetActive(true);
            blinkCount = 0;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Groundにふれたとき
        if (other.gameObject.CompareTag("Ground"))
        {
            DoubleWall = false;
            //ヒップドロップで触れたらカメラを揺らす
            if (Drop)
            {
                CameraMove.sway = true;
            }
        }
        if(other.gameObject.CompareTag("Button"))
        {
            if (Drop)
            {
                CameraMove.sway = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //壁に触れている間
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
        }
        //地面に触れている間
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Button"))
        {
            Drop = false;
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //壁から離れたとき
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
            PlayerSkin.Rota = true;
            OnWall = false;
            DoubleWall = false;
        }
        //地面から離れたとき
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Button"))
        {
            JumpCount = 1;
            PlayerSkin.Rota = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!Drop)
            {
                if (!blink)
                {
                    if (Hp > 0)
                    {
                        HpObject[Hp - 1].SetActive(false);
                        Hp -= 1;
                        blink = true;
                    }
                    else
                    {
                        //死亡演出
                    }
                }
            }
        }
    }
}
