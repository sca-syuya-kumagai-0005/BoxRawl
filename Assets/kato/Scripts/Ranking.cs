using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [Header("スコア関係")]
    int[] rankingScore = new int[10];
    string[] rankingName = new string[10];
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

    [Header("ランキング関係")]
    TextAsset rankingCSV;

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

        rankingSet();
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

            for (int i = 0;i < rankingScore.Length;i++) 
            {
                Debug.Log(rankingScore[i]);
                Debug.Log(rankingName[i]);
            }
        }

    }

    void Result()
    {
        //totalScore = 0;     //スコア受け取り
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

        if(Input.GetKey(KeyCode.Space))
        {
            PlayerName = nameInputField.text;
        }

        if (PlayerName != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(PlayerName);
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

    void rankingSet()
    {
        rankingCSV = Resources.Load<TextAsset>("ranking");
        List<string[]> rankingDate = new List<string[]>();
        StringReader reader = new StringReader(rankingCSV.text);
        
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            rankingDate.Add(line.Split(','));
        }

        for (int i = 0; i < rankingScore.Length; i++) 
        {
            rankingScore[i] = int.Parse(rankingDate[i][1]);
            rankingName[i] = rankingDate[i][0];
        }


    }
}
