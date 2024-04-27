using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̃X�N���v�g�̓}�E�X�ɑΉ�����ꍇ�Ɏg�p����X�N���v�g�ł�
public class ButtonSize : MonoBehaviour
{
    private int myNumber;//���g�����Ԗڂ̃{�^�����𔻒f����ϐ�
    private GameObject buttonManager;
    private bool onFlag;
    // Start is called before the first frame update
    void Start()
    {
        buttonManager = GameObject.Find("ButtonManager").gameObject;
        myNumber=myNumberSet();
    }

    // Update is called once per frame
    void Update()
    {
        PointerOn();
    }

    int myNumberSet()
    {
        for(int i=0;i<buttonManager.transform.childCount;i++)
        {
            if(buttonManager.transform.GetChild(i).gameObject==this.gameObject)
            {
                return i;
            }
        }
        return 0;
    }

    public void PointerEnter() {onFlag = true;}
    public void PointerExit() {  onFlag = false; }
    public void PointerOn()
    {
        if(onFlag)
        {
            ButtonManager.selectButtonNumber = myNumber;
        }
    }
}
