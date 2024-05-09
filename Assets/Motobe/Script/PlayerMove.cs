using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int JumpCount;

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
    //invincibleTime*0.05�b���G����(invincibleTime==8�Ȃ�0.4�b)�@�X�e�[�^�X�ɓ���Ă���������
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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DoubleWall);

        if (!ButtonManager.sceneCheck)
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
                if (JumpCount == 1)
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
        //Ground�ɂӂꂽ�Ƃ�
        if (other.gameObject.CompareTag("Ground"))
        {
            DoubleWall = false;
            //�q�b�v�h���b�v�ŐG�ꂽ��J������h�炷
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
        //�ǂɐG��Ă����
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
        }
        //�n�ʂɐG��Ă����
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
        //�ǂ��痣�ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
            PlayerSkin.Rota = true;
            OnWall = false;
            DoubleWall = false;
        }
        //�n�ʂ��痣�ꂽ�Ƃ�
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
                        //���S���o
                    }
                }
            }
        }
    }
}
