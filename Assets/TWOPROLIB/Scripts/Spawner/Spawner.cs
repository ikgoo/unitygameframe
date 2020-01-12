using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Spawner
{
    /// <summary>
    /// Entity 스포너
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// Interactable 포함된 Object
        /// </summary>
        [Tooltip("Interactable 포함된 Object")]
        public GameObject entity;

        /// <summary>
        /// 스폰 시킬 이미지 리스트
        /// </summary>
        [Tooltip("스폰 시킬 이미지 리스트")]
        public List<GameObject> prefabNames = new List<GameObject>();

        /// <summary>
        /// 스폰시 적용될 앤티티
        /// </summary>
        [SerializeField] public Entity injectEntity;

        /// <summary>
        /// 대상 타켓
        /// </summary>
        public List<string> targetTag;

        void Start()
        {
            //interactable = entity.GetComponent<Interactable>();
        }

        void Update()
        {
        }

        /// <summary>
        /// 스폰 처리
        /// </summary>
        public virtual void Spawn()
        {
            GameObject go = GamePrefabPoolManager.Instance.GetObjectForType(prefabNames[0].name, false);
            go.AddComponent<Interactable>();
            Interactable interactable = go.GetComponent<Interactable>();
            interactable.Spawn(injectEntity, PickupType.Once, 1, targetTag);
            go.SetActive(true);
        }
    }
}
