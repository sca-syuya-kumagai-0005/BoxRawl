using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [Header("スコア関係")]
    int totalScore;

    [Header("キャンバス関係")]
    [SerializeField] GameObject resultBoard;
    [SerializeField] GameObject nameBoard;
    [SerializeField] GameObject rankingBoard;
    [SerializeField] GameObject gameOverMenu;

    public static bool isScore; //trueの場合キャンバス表示

    [Header("名前関係")]
    public static string PlayerName;
    public InputField nameInputField;

    enum RankingState
    {
        result = 0,
        name,
        ranking,
        menu
    }
    RankingState rankingState;

    // Start is called before the first frame update
    void Start()
    {
        isScore = false;
        resultBoard.SetActive(false);
        nameBoard.SetActive(false);
        rankingBoard.SetActive(false);
        //gameOverMenu.SetActive(false);

        PlayerName = null;

        RankingManager.rankingUpdate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScore)
        {
            switch (rankingState)
            {
                case RankingState.result:
                    Result();
                    break;
                case RankingState.name:
                    nameInput();
                    break;
                case RankingState.ranking:
                    ranking();
                    break;
                case RankingState.menu:
                    gameMenu();
                    break;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isScore = true;
        }

    }

    void Result()
    {
        totalScore = 20;     //スコア受け取り
        RankingManager.myScore = totalScore;
        resultBoard.SetActive(true);
        rankingBoard.SetActive(false );

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (totalScore > RankingManager.rankingScore[9]
                || RankingManager.rankingScore[9] == null)
            {
                rankingState = RankingState.name;
            }
            else
            {
                RankingManager.rankingUpdate = true;
                rankingState = RankingState.ranking;
            }
        }
    }

    void nameInput()
    {
        resultBoard.SetActive(false);
        nameBoard.SetActive(true);

        if(Input.GetKey(KeyCode.P))
        {
            PlayerName = nameInputField.text;
        }

        if (PlayerName != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(PlayerName);
                RankingManager.myName = PlayerName;
                RankingManager.rankingUpdate = true;
                rankingState = RankingState.ranking;
            }
        }
    }

    void ranking()
    {
        rankingBoard.SetActive(true );
        nameBoard.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rankingState = RankingState.result;
        }
    }

    void gameMenu()
    {

    }

}
