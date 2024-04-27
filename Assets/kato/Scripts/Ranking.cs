using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] int[] highScore;
    int totalScore;

    [SerializeField] GameObject resultBoard;
    [SerializeField] GameObject nameBoard;
    [SerializeField] GameObject rankingBoard;
    [SerializeField] GameObject gameOverMenu;

    public static bool isScore;

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
        //isScore = false;
        //totalScore = 0;     //ƒXƒRƒAŽó‚¯Žæ‚è
        resultBoard.SetActive(true);
        rankingBoard.SetActive(false );

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rankingState = RankingState.name;
        }
    }

    void nameInput()
    {
        resultBoard.SetActive(false);
        nameBoard.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rankingState = RankingState.ranking;
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
