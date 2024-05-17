using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    public static int JumpCount;

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
    //invincibleTime*0.05秒無敵時間(invincibleTime==8なら0.4秒)
    public int invincibleTime;
    int invincibleTimeCheck;

    //スタート処理
    bool startRota;
    public Image[] Count;
    public GameObject EnemySpawnner;

    //パリィ処理
    public GameObject ParyObject;
    public static bool paryCheck;

    //ダメージ演出
    public Image damageEffect;

    // Start is called before the first frame update
    void Start()
    {
        
        JumpCount = 0;
        blink = false;
        blinkCheck = false;
        rb = GetComponent<Rigidbody2D>();
        PlayerSkinObject.SetActive(true);
        DropObject.SetActive(false);
        blinkCount = 0;
        invincibleTimeCheck = 0;
        startRota = false;
        EnemySpawnner.SetActive(false);

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
        ParyObject.SetActive(false);
        paryCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(JumpCount);

        if (!ButtonManager.sceneCheck)
        {
            if (startRota)
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

                    if (JumpCount == 1||ParyController.parySet)
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
                if (blinkCount > 0.05f)
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Groundにふれたとき
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!startRota&&SceneManager.GetActiveScene().name!="TmpMenu")
            {
                PlayerSkin.Rota = false;
                StartCount();
                
                //Time.timeScale = 0;
            }
            else if(SceneManager.GetActiveScene().name=="TmpMenu")
            {
                PlayerSkin.Rota = false;
                startRota = true;
            }
            DoubleWall = false;
            //ヒップドロップで触れたらカメラを揺らす
            if (Drop)
            {
                CameraMove.dropSway = true;
            }

        }
        if(other.gameObject.CompareTag("Button"))
        {
            if (Drop)
            {
                CameraMove.dropSway = true;
            }
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //壁に触れている間
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            JumpCount = 0;
            ParyObject.SetActive(false);
        }
        //地面に触れている間
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Button"))
        {
            Drop = false;
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
            ParyObject.SetActive(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //壁から離れたとき
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
            if (ParyObject != null)
                ParyObject.SetActive(true);
            PlayerSkin.Rota = true;
            OnWall = false;
            DoubleWall = false;
        }
        //地面から離れたとき
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Button"))
        {
            JumpCount = 1;
            if(ParyObject!=null)
            ParyObject.SetActive(true);
            PlayerSkin.Rota = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!paryCheck)
            {
                if (!Drop)
                {
                    if (!blink)
                    {
                        if (Hp > 1)
                        {
                            HpObject[Hp - 1].SetActive(false);
                            Hp -= 1;
                            blink = true;
                            CameraMove.damageSway = true;
                            DamageEffect();
                        }
                        else
                        {
                            HpObject[0].SetActive(false);
                            Hp =0;
                            //死亡演出
                        }
                    }
                }
            }
        }
    }

    public void StartCount()
    {
        var sequence = DOTween.Sequence();
        var img3 = Count[3];
        var c3 = img3.color;
        c3.a = 0.0f;
        img3.color = c3;
        var img2 = Count[2];
        var c2 = img2.color;
        c2.a = 0.0f;
        img2.color = c2;
        var img1 = Count[1];
        var c1 = img1.color;
        c1.a = 0.0f;
        img1.color = c1;
        var imgGo = Count[0];
        var cGo = imgGo.color;
        cGo.a = 0.0f;
        imgGo.color = cGo;
        sequence.Append(DOTween.ToAlpha(() => img3.color, color => img3.color = color, 1, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => img3.color, color => img3.color = color, 0, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => img2.color, color => img2.color = color, 1, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => img2.color, color => img2.color = color, 0, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => img1.color, color => img1.color = color, 1, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => img1.color, color => img1.color = color, 0, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => imgGo.color, color => imgGo.color = color, 1, 0.25f));
        sequence.Append(DOTween.ToAlpha(() => imgGo.color, color => imgGo.color = color, 0, 0.25f));
        sequence.AppendCallback(() => StartEnd());
    }

    public void StartEnd()
    {
        startRota = true;
        EnemySpawnner.SetActive(true);
    }

    public void DamageEffect()
    {
        var sequence = DOTween.Sequence();
        var img = damageEffect;
        var color = damageEffect.color;
        color.a = 0;
        sequence.Append(DOTween.ToAlpha(() => img.color, color => img.color = color, 0.8f, 0.1f));
        sequence.Append(DOTween.ToAlpha(() => img.color, color => img.color = color, 0, 0.1f));
    }
}
