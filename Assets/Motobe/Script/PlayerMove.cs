using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
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
    public float DefaultJumpForce;
    public static float PlusJumpForce;
    private float JumpForce;

    //速さ関係
    public float DefaultSpeed;
    public static float PlusSpeed;
    private float Speed;

    /*
    //大きさ関係(ステージの構成的にヒップドロップの範囲強化のほうが良さそうと提案)
    //[SerializeField] public float DefaultSize;
    //[SerializeField] public float PlusSize;
    //private float Size;
    */

    //体力関係
    private int DefaultHp=5;
    //[SerializeField] public int PlusHp;
    private int Hp;

    //空中に居るかの判定
    public static int JumpCount;

    //壁に触れているかの判定
   [SerializeField]private bool OnWall;

    //連続壁ジャンプをしないようにする
    private bool DoubleWall;

    //ヒップドロップをしているかの判定
    public static bool Drop;

    //ダメージを受けているかの確認
    public static bool blink;
    private bool blinkCheck;
    float blinkCount;

    //ダメージを受けた後の無敵時間
    //invincibleTime*0.05秒無敵時間(invincibleTime==8なら0.4秒)
    public int DefaultInvincibleTime;
    public static int PlusInvincibleTime;
    private int InvincibleTime;
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

    //死亡判定
    public static bool PlayerDead;

    //経験値倍率
    public static int EXPUP;

    //天井
    public GameObject Ceiling;

    // Start is called before the first frame update
    void Start()
    {
        if (Ceiling != null)
        {
            Ceiling.SetActive(false);
        }
        EXPUP = 1;
        PlayerDead = false;
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
        PlusSpeed = 0;
        PlusJumpForce = 0;

        
        //Size = DefaultSize + PlusSize;
        Hp = 5;//DefaultHp + PlusHp;

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

        //ステータスを入力
        JumpForce = DefaultJumpForce + PlusJumpForce;
        Speed = DefaultSpeed + PlusSpeed;
        InvincibleTime = DefaultInvincibleTime + PlusInvincibleTime;

        if (EXPUP >= 3)
        {
            EXPUP = 3;
        }

        if (PlayerDead)
        {
            EnemySpawnner.SetActive(false);
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
                if (invincibleTimeCheck >= InvincibleTime)
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

        if (PlayerDead)
        {
            return;
        }
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
                                SEController.jump = true;
                            }
                        }
                        else
                        {
                            rb.velocity = new Vector3(0, JumpForce, 0);
                            SEController.jump = true;
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
                    
                    //壁に触れたまま移動しない
                    if (!OnWall)
                    {
                        //ヒップドロップ中に移動しない
                        if (!Drop)
                        {
                            PlayerSkin.rota = 1;
                            this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
                //右移動
                if (Input.GetKey(KeyCode.D))
                {
                    
                    //壁に触れたまま移動しない
                    if (!OnWall)
                    {
                        //ヒップドロップ中に移動しない
                        if (!Drop)
                        {
                            PlayerSkin.rota = -1;
                            this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
                //ヒップドロップ
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //空中にいるとき
                    if (!Drop)
                    {
                        if (JumpCount == 1||ParyController.parySet)
                        {
                            DropSystem();
                        }
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
            //ヒップドロップの判定
            if (!Drop)
            {
                DropObject.SetActive(false);
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
                if (Ceiling != null)
                {
                    Ceiling.SetActive(true);
                }
                    
                //Time.timeScale = 0;
            }
            else if(SceneManager.GetActiveScene().name=="Menu")
            {
                PlayerSkin.Rota = false;
                startRota = true;
            }
            DoubleWall = false;
            //ヒップドロップで触れたらカメラを揺らす
            if (Drop)
            {
                CameraMove.dropSway = true;
                SEController.drop2 = true;
            }
            EXPUP = 1;
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
            Time.timeScale = 1.0f;
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
            //Time.timeScale = 0.1f;
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
                        if(PlayerUp.setBarrier)
                        {
                            Debug.Log("バリア");
                            blink = true;
                            PlayerUp.setBarrier = false;
                        }
                        else if (Hp > 1)
                        {
                            HpObject[Hp - 1].SetActive(false);
                            Hp -= 1;
                            blink = true;
                            CameraMove.damageSway = true;
                            DamageEffect();
                            SEController.damage = true;
                        }
                        else
                        {
                            CameraMove.damageSway = true;
                            DamageEffect();
                            SEController.dead = true;
                            Dead();
                            blink = true;
                        }
                    }
                }
            }
        }
    }

    public void DropSystem()
    {
        var sequence = DOTween.Sequence();
        rb.velocity = new Vector3(0, JumpForce, 0);
        Drop = true;
        SEController.drop1 = true;
        if (PlayerSkin.rota==0)
        {
            PlayerSkin.rota = 1;
        }
        PlayerSkin.rota *= -2;
        sequence.AppendInterval(0.2f);
        sequence.AppendCallback(() => DropSystem2());
    }
    public void DropSystem2()
    {
        DropObject.SetActive(true);
        PlayerSkin.Rota = false;
        PlayerSkin.rota =0;
        rb.velocity = new Vector3(0, -JumpForce *  2, 0);
    }

    public void StartCount()
    {
        if (SceneManager.GetActiveScene().name!="Main Game")
        {
            StartEnd();
            return;
        }
        else
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
        for (int i=5/*DefaultHp+PlusHp*/;i>Hp;i--)
        {
            sequence.Append(DOTween.ToAlpha(() => img.color, color => img.color = color, 0.8f, 0.1f));
            sequence.Append(DOTween.ToAlpha(() => img.color, color => img.color = color, 0, 0.1f));
        }
        
    }

    public void Dead()
    {
        var sequence = DOTween.Sequence();
        //PlayerSkinObject.SetActive(false);
        HpObject[0].SetActive(false);
        Hp = 0;
        PlayerDead = true;
        EnemySpawnner.SetActive(false);
        Destroy(rb);
        PlayerSkin.Rota = false;
        sequence.AppendInterval(3.0f);
        //ここにシーン転移のやつ
        sequence.AppendCallback(() => SceneChange());
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Menu");
    }
}
