using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool debug;

    private void Awake()
    {
        DebugMode.Instance.debug = debug;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DebugMode.Instance.debug = !DebugMode.Instance.debug;
            string mode = DebugMode.Instance.debug ? "enable" : "disable";
            Debug.Log("Debug mode " + mode);
        }
    }
}
