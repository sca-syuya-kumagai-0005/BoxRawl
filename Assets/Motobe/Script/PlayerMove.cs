using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] public float DefaultJumpForce;
    [SerializeField] public float PlusJumpForce;
    private float JumpForce;

    [SerializeField] public float DefaultSpeed;
    [SerializeField] public float PlusSpeed;
    private float Speed;

    [SerializeField] public float DefaultSize;
    [SerializeField] public float PlusSize;
    private float Size;

    private float DefaultHp=2;
    [SerializeField] public float PlusHp;
    private float Hp;

    private int JumpCount;

    private bool OnWall;
    public static bool Drop;
    private float WallJumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpForce = DefaultJumpForce + PlusJumpForce;
        Speed = DefaultSpeed + PlusSpeed;
        Size = DefaultSize + PlusSize;
        Hp = DefaultHp + PlusHp;
        JumpCount = 0;
        OnWall = false;
        Drop = false;
        ButtonManager.sceneCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonManager.sceneCheck==false)
        {
            //ジャンプ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (JumpCount == 0)
                {
                    rb.velocity = new Vector3(0, JumpForce, 0);
                }
            }
            //壁めり込み防止
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                OnWall = false;
            }
            //移動
            if (Input.GetKey(KeyCode.A))
            {
                PlayerSkin.rota = 1;
                if (OnWall == false)
                {
                    if (Drop==false)
                    {
                        this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
                    }
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerSkin.rota = -1;
                if (OnWall == false)
                {
                    if (Drop == false)
                    {
                        this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayerSkin.Rota = false;
                PlayerSkin.rota = 0;
                if (JumpCount >= 1)
                {
                    rb.velocity = new Vector3(0, -JumpForce * 2, 0);
                    Drop = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PlayerSkin.rota = 0;
            JumpCount = 0;
            if (Drop == true)
            {
                CameraMove.sway = true;
            }
            Drop = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            PlayerSkin.rota = 0;
            JumpCount = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            PlayerSkin.Rota = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
            PlayerSkin.Rota = true;
            OnWall = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            JumpCount = 1;
            PlayerSkin.Rota = true;
        }
    }
}
