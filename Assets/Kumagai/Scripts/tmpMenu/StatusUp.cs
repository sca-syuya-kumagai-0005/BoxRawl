using UnityEngine;

public class StatusUp : MonoBehaviour
{
    [SerializeField]private string nowPlayerType;
    [SerializeField]private string selectPlayerType;
    private int nowTypeNumber;
    [SerializeField] private int tmpTypeNumber;
    [SerializeField]private int selectTypeNumber;
    public static bool selectType;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject[] type;

    void Start()
    {
        nowPlayerType = "Default";
        nowTypeNumber = 0;
        tmpTypeNumber = 0;
        selectType = true;
    }

    // Update is called once per frame
    void Update()
    {
        window.SetActive(selectType);
        if (selectType)
        {
            PlayerTypeSelection();
        }
    }

    private void PlayerTypeSet()
    {
        switch (selectTypeNumber)
        {
            case 0:
                {
                    selectPlayerType = "Default";
                }
                break;
            case 1:
                {
                    selectPlayerType = "Speed";
                }
                break;
            case 2:
                {
                    selectPlayerType = "Defense";
                }
                break;
            case 3:
                {
                    selectPlayerType = "Attack";
                }
                break;
            case -1:
                {
                    selectType = false;
                }
                break;
            default:
                {
                    Debug.Log("A");
                }
                break;
        }
    }

    private void PlayerTypeSelection()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(tmpTypeNumber<=3)
            {
                tmpTypeNumber = 0;
            }
            if(tmpTypeNumber==100)
            {
                tmpTypeNumber = 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
          if(tmpTypeNumber==0)
            {
                tmpTypeNumber = 2;
            }
          else if(tmpTypeNumber>0)
            {
                tmpTypeNumber = 100;
            }
          else if(tmpTypeNumber==-1)
            {
                tmpTypeNumber = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (tmpTypeNumber <= 1 || tmpTypeNumber == 100)
            {
                tmpTypeNumber = -1;
            }
            else
            {
                tmpTypeNumber--;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (tmpTypeNumber == -1)
            {
                tmpTypeNumber = 0;
            }
            else if(tmpTypeNumber<=2)
            {
                tmpTypeNumber++;
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(tmpTypeNumber!=100&&tmpTypeNumber!=-1)
            {
                selectTypeNumber = tmpTypeNumber;
            }
            else if(tmpTypeNumber==100)
            {
                nowTypeNumber=selectTypeNumber;
                nowPlayerType = selectPlayerType;
                selectTypeNumber = 0;
            }
            else
            {
                selectType = false;
                selectTypeNumber=0;
            }
            PlayerTypeSet();
        }
    }


}
