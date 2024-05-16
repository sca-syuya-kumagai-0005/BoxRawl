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

    //�v���C���[�̌����ڂ̃I�u�W�F�N�g
    public GameObject PlayerSkinObject;

    //�W�����v�ł��邩�m�F���邽�߂̃I�u�W�F�N�g
    public GameObject JumpChecker;

    //�q�b�v�h���b�v�œG��|������̃I�u�W�F�N�g
    public GameObject DropObject;

    //�̗͕\���p�̃I�u�W�F�N�g
    public GameObject[] HpObject;

    //�W�����v�̍����֌W
    [SerializeField] public float DefaultJumpForce;
    [SerializeField] public float PlusJumpForce;
    private float JumpForce;

    //�����֌W
    [SerializeField] public float DefaultSpeed;
    [SerializeField] public float PlusSpeed;
    private float Speed;

    //�傫���֌W(�X�e�[�W�̍\���I�Ƀq�b�v�h���b�v�͈̔͋����̂ق����ǂ������ƒ��)
    [SerializeField] public float DefaultSize;
    [SerializeField] public float PlusSize;
    private float Size;

    //�̗͊֌W
    private int DefaultHp=2;
    [SerializeField] public int PlusHp;
    private int Hp;

    //�󒆂ɋ��邩�̔���
    public static int JumpCount;

    //�ǂɐG��Ă��邩�̔���
    private bool OnWall;

    //�A���ǃW�����v�����Ȃ��悤�ɂ���
    private bool DoubleWall;

    //�q�b�v�h���b�v�����Ă��邩�̔���
    public static bool Drop;

    //�_���[�W���󂯂Ă��邩�̊m�F
    private bool blink;
    private bool blinkCheck;
    float blinkCount;

    //�_���[�W���󂯂���̖��G����
    //invincibleTime*0.05�b���G����(invincibleTime==8�Ȃ�0.4�b)
    public int invincibleTime;
    int invincibleTimeCheck;

    //�X�^�[�g����
    bool startRota;
    public Image[] Count;
    public GameObject EnemySpawnner;

    //�p���B����
    public GameObject ParyObject;
    public static bool paryCheck;

    //�_���[�W���o
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

        //�X�e�[�^�X�����
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
                //�W�����v
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (JumpCount == 0)
                    {
                        //�ǂł̘A���W�����v�h�~
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
                //�ǂ߂荞�ݖh�~
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    OnWall = false;
                }
                //���ړ�
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerSkin.rota = 1;
                    //�ǂɐG�ꂽ�܂܈ړ����Ȃ�
                    if (!OnWall)
                    {
                        //�q�b�v�h���b�v���Ɉړ����Ȃ�
                        if (!Drop)
                        {
                            this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
                //�E�ړ�
                if (Input.GetKey(KeyCode.D))
                {
                    PlayerSkin.rota = -1;
                    //�ǂɐG�ꂽ�܂܈ړ����Ȃ�
                    if (!OnWall)
                    {
                        //�q�b�v�h���b�v���Ɉړ����Ȃ�
                        if (!Drop)
                        {
                            this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
                //�q�b�v�h���b�v
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //�󒆂ɂ���Ƃ�

                    if (JumpCount == 1||ParyController.parySet)
                    {
                        PlayerSkin.Rota = false;
                        PlayerSkin.rota = 0;
                        rb.velocity = new Vector3(0, -JumpForce * 2, 0);
                        Drop = true;
                    }
                }
                //�W�����v�\���m�F�p�I�u�W�F�N�g�̕\����\��
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
            //�q�b�v�h���b�v���̔���
            if (Drop)
            {
                DropObject.SetActive(true);
            }
            else
            {
                DropObject.SetActive(false);
            }

            //�_���[�W���󂯂����̓_��
            if (blink)
            {
                //�_��
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
        //Ground�ɂӂꂽ�Ƃ�
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
            //�q�b�v�h���b�v�ŐG�ꂽ��J������h�炷
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
        //�ǂɐG��Ă����
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            JumpCount = 0;
            ParyObject.SetActive(false);
        }
        //�n�ʂɐG��Ă����
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
        //�ǂ��痣�ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
            if (ParyObject != null)
                ParyObject.SetActive(true);
            PlayerSkin.Rota = true;
            OnWall = false;
            DoubleWall = false;
        }
        //�n�ʂ��痣�ꂽ�Ƃ�
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
                            //���S���o
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
