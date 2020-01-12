using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugX : MonoBehaviour
{
    public static void Log(string msg)
    {
#if UNITY_EDITOR
        Debug.Log(msg);
#endif
    }
}
