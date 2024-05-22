using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialEnemy;
    [SerializeField] private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wall.SetActive(tutorialEnemy.activeSelf);
    }
}
