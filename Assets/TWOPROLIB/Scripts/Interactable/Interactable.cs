using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    /// <summary>
    /// 플레이어 또는 움직이는 적이 아닌 순수 statecontroller와 상호 작용하는 물체
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        /// <summary>
        /// entity
        /// </summary>
        [Tooltip("Entity")]
        [SerializeField] public Entity entity;

        /// <summary>
        /// Pickup일 경우 Interactable 후 처리 타입
        /// </summary>
        [Tooltip("Pickup일 경우 Interactable 후 처리 타입")]
        public PickupType pickupType = PickupType.None;

        /// <summary>
        /// 수량
        /// </summary>
        [Tooltip("수량")]
        public int amount = 0;

        /// <summary>
        /// 대상 Tag
        /// </summary>
        [Tooltip("대상 Tag")]
        public List<string> targetTags = new List<string>();

        /// <summary>
        /// 확장기능
        /// </summary>
        [Tooltip("확장기능")]
        public string[] objs;

        /// <summary>
        /// 상호 작용 시 호출 됨
        /// </summary>
        public virtual bool Interact(string tag, StateController controller)
        {
            if(targetTags.Contains(tag))
            {
                controller.Interactable(entity, amount);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 자신을 제거
        /// </summary>
        public virtual void Destory()
        {
            PrefabPoolDestroy ppd = GetComponent<PrefabPoolDestroy>();
            if (ppd != null)
            {
                // PrefabPool 사용 할 경우 제거 명령을 이용해서 처리
                ppd.Destroy();
            }
            else
            {
                // 자신의 오프젝프를 제거
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Entity Spawner 사용 시 등록때 설정 값
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="amount"></param>
        /// <param name="targetTags"></param>
        /// <param name="objs"></param>
        public virtual void Spawn(Entity entity, PickupType pickupType, int amount, List<string> targetTags, string[] objs = null)
        {
            this.entity = entity;
            this.pickupType = pickupType;
            this.amount = amount;
            this.targetTags = targetTags;
            this.objs = objs;
        }

        /// <summary>
        /// Entity Spawner 사용 시 등록때 설정 값
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="amount"></param>
        /// <param name="targetTags"></param>
        /// <param name="objs"></param>
        public virtual void Spawn(int amount, List<string> targetTags, string[] objs = null)
        {
            this.amount = amount;
            this.targetTags = targetTags;
            this.objs = objs;
        }
    }
}
