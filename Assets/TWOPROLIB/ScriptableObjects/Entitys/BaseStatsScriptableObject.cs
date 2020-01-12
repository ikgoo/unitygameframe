using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects.Entitys
{
    public class BaseStatsScriptableObject : ScriptableObject
    {
        /// <summary>
        /// 최대 HP
        /// </summary>
        [Tooltip("최대 HP")]
        public float maxHP = 10;

        /// <summary>
        /// HP
        /// </summary>
        [Tooltip("HP")]
        public float hp = 10;

        /// <summary>
        /// 최대 MP
        /// </summary>
        [Tooltip("최대 MP")]
        public float maxMP = 10;

        /// <summary>
        /// MP
        /// </summary>
        [Tooltip("MP")]
        public float mp = 10;

        /// <summary>
        /// 공격력
        /// </summary>
        [Tooltip("공격력")]
        public float attack = 10;

        /// <summary>
        /// 방어력
        /// </summary>
        [Tooltip("방어력")]
        public float defense = 0;

        /// <summary>
        /// 이동 속도
        /// </summary>
        [Tooltip("이동 속도")]
        public float speed = 200;

        /// <summary>
        /// 회전 속도
        /// </summary>
        [Tooltip("회전 속도")]
        public float angleSpeed = 200;
    }
}
