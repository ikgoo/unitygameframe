using System;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 게임 룰 디자인(테스트)
    /// </summary>
    [CreateAssetMenu(menuName = "GameManager/GameRule")]
    [Serializable]
    public class GameRuleTest : GameRuleBase
    {
        public override void OnFixedUpdate(GameManager gameManager)
        {
            base.OnFixedUpdate(gameManager);
            Debug.Log("OnFixedUpdate");

        }

        public override void OnCustomUpdate(GameManager gameManager)
        {
            base.OnCustomUpdate(gameManager);

        }
    }
}
