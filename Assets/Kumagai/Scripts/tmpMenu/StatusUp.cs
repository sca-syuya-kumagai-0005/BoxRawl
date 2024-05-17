using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class StatusUp : MonoBehaviour
{

    public static Dictionary<string, int> statusUp;
    string[] status = { "size", "hp", "jump", "speed" };// Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < status.Length; i++) { statusUp[status[i]] = 0; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int CountUp(int status)
    {
        status++;
        return status;
    }
}
