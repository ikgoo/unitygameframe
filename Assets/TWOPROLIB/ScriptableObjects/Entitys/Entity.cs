using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Entitys;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects.Entitys
{
    /// <summary>
    /// 게임의 모든 물체는 entity를 상속 받음
    /// </summary>
    public class Entity : ScriptableObject
    {
        /// <summary>
        /// Entity 타입을 정의
        /// </summary>
        [Tooltip("Entity 타입을 정의")]
        public EntityTypes EntityType;

        /// <summary>
        /// Entity 이름을 정의
        /// </summary>
        [Tooltip("Entity 이름을 정의")]
        public string Name;

        /// <summary>
        /// Entity 설명을 정의
        /// </summary>
        [Tooltip("Entity 설명")]
        [TextArea]
        public string Description;

        /// <summary>
        /// 섬네일 이미지(스프라이트)
        /// </summary>
        [Tooltip("섬네일 이미지")]
        public Sprite Thumbnail;

    }
}
