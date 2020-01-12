using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Utils
{
    public class SceneMove : MonoBehaviour
    {
        public void BtnMove(int scenePage)
        {
            ScreenManager.Instance.ChangeScene((ScreenManager.ScenePage)scenePage);
        }
    }
}
