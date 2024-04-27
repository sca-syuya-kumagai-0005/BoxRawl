using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトはマウスに対応する場合に使用するスクリプトです
public class ButtonSize : MonoBehaviour
{
    private int myNumber;//自身が何番目のボタン化を判断する変数
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
