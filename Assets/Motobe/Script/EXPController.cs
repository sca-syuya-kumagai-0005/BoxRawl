using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPController : MonoBehaviour
{
    public static float EXP;
    float exp;
    public Image EXPGage;
    public Image EXPGage2;
    public GameObject LevelUpSet;
    int expup;

    // Start is called before the first frame update
    void Start()
    {
        EXP = 0;
        exp = 0;
        expup = 0;
        LevelUpSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (PlayerMove.EXPUP > expup)
        {
            expup = PlayerMove.EXPUP;
        }
        if (EXPGage2.rectTransform.sizeDelta.x >= EXPGage.rectTransform.sizeDelta.x)
        {
            expup = PlayerMove.EXPUP;
        }
        if (PlayerMove.PlayerDead)
        {
            return;
        }
        
        EXPGage.rectTransform.sizeDelta = new Vector2(EXP * 19, 75);
        EXPGage2.rectTransform.sizeDelta = new Vector2(exp * 19, 75);
        if (EXP > exp)
        {
            exp += 10 * expup * Time.deltaTime;
        }
        if (exp >= 100)
        {
            exp -= 100;
            EXP -= 100;
            LevelUpSet.SetActive(true);
            PlayerMove.PlayerDead = true;
        }
    }
}
