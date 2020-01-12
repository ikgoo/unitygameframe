﻿using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Script.EntitysAttributes;
using TWOPROLIB.ScriptableObjects;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.ScriptableObjects.EntitysSkills;
using TWOPROLIB.Scripts.Entitys;
using TWOPROLIB.Scripts.Interactables;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    /// <summary>
    /// 오브젝트의 상태를 제어하는 클래스
    /// </summary>
    public abstract class StateController : MonoBehaviour
    {
        #region 상태 정보

        /// <summary>
        /// 2D, 3D 상태 표시
        /// </summary>
        [Tooltip("2D, 3D 상태 표시")]
        public GameDisplayMode gameDisplayMode;

        [Header("--- State Info ---")]
        /// <summary>
        /// AI 활성화 여부(currentState 실행 여부
        /// </summary>
        [Tooltip("AI 활성화 여부(currentState 실행 여부")]
        [SerializeField] private bool aiActive;

        /// <summary>
        /// 유저 INPUT 처리 유무
        /// </summary>
        [Tooltip("유저 INPUT 처리 유무")]
        [SerializeField] public bool isInput;

        /// <summary>
        /// 물리 사용유무
        /// </summary>
        [Tooltip("물리 사용유무")]
        [SerializeField] public bool isRigidbody = true;

        /// <summary>
        /// 오브젝트의 상태 정보(AI 활성화시)
        /// </summary>
        [Tooltip("오브젝트의 상태 정보(AI 활성화시)")]
        public State currentState;

        /// <summary>
        /// 상태 변환 대기시간
        /// </summary>
        [Tooltip("상태 변환 대기시간")]
        [HideInInspector] public float stateTimeElapsed;

        /// <summary>
        /// 항상 실행되는 상태(AI==false 여도 계속 실행됨)
        /// </summary>
        [Tooltip("항상 실행되는 상태(AI==false 여도 계속 실행됨)")]
        public State remainState;

        /// <summary>
        /// 적 감지 거리, 공격 거리 계산용
        /// </summary>
        [Tooltip("적 감지 거리, 공격 거리 계산용")]
        public EnemyStats enemyStats;

        /// <summary>
        /// WAY POINT 리스트
        /// </summary>
        [Tooltip("WAY POINT 리스트")]
        public List<Transform> wayPointList;

        /// <summary>
        /// NEXT WAY POINT 정보
        /// </summary>
        [Tooltip("NEXT WAY POINT 정보")]
        [HideInInspector] public int nextWayPoint;

        /// <summary>
        /// 추적대상
        /// </summary>
        [Tooltip("추적대상")]
        [HideInInspector] public Transform chaseTarget = null;

        #endregion

        #region Controller 유닛 정보
        [Header("--- Unit Info ---")]

        /// <summary>
        /// 게임의 기본 스텟
        /// </summary>
        [Tooltip("게임의 기본 스텟")]
        public BaseStats stats;

        /// <summary>
        /// 발사무기 오브젝트 생성 위치
        /// </summary>
        [Tooltip("발사무기 오브젝트 생성 위치")]
        public Transform firePoint;

        /// <summary>
        /// 무기 설정 슬롯
        /// </summary>
        [Tooltip("무기 설정 슬롯")]
        public Weapon weapon;

        #endregion

        #region Attributes 정보
        [Header("--- Attributes Info ---")]
        public List<LiveEntityAttributes> Attributes = new List<LiveEntityAttributes>();

        [Header("--- Skills Enabled ---")]
        public List<Skills> skills = new List<Skills>();
        #endregion

        /// <summary>
        /// 물리 컨포넌트
        /// </summary>
        [HideInInspector] public Rigidbody2D rigid2d;

        /// <summary>
        /// 물리 컨포넌트
        /// </summary>
        [HideInInspector] public Rigidbody rigid3d;


        #region MonoBehaviour 기본 관련 : Start ======================
        protected virtual void Start()
        {
            if (gameDisplayMode == GameDisplayMode.Mode_2D)
                rigid2d.gameObject.SetActive(isRigidbody);
            else
                rigid3d.gameObject.SetActive(isRigidbody);


            if (!aiActive && currentState == null)      // AI가 비활성화 이면 종료됨(별도 컨트롤 처리)
                return;
        }

        protected virtual void Update()
        {
            remainState.UpdateState(this);

            if (!aiActive)      // AI가 비활성화 이면 종료됨(별도 컨트롤 처리)
                return;

            currentState.UpdateState(this);
        }

        protected virtual void FixedUpdate()
        {
        }

        /// <summary>
        /// 자신 또는 하위에 Trigger Enter 이벤트가 발생하였을 경우 호출
        /// </summary>
        /// <param name="childGameObject">호출 된 오브젝트</param>
        /// <param name="other">출동 체 정보</param>
        public virtual void OnStateTriggerEnter(GameObject childGameObject, GameObject targetObject)
        {

        }

        /// <summary>
        /// 자신 또는 하위에 Trigger Exit 이벤트가 발생하였을 경우 호출
        /// </summary>
        /// <param name="childGameObject">호출 된 오브젝트</param>
        /// <param name="other">출동 체 정보</param>
        public virtual void OnStateTriggerExit(GameObject childGameObject, GameObject targetObject)
        {

        }

        /// <summary>
        /// 자신 또는 하위에 Trigger Stay 이벤트가 발생하였을 경우 호출
        /// </summary>
        /// <param name="childGameObject">호출 된 오브젝트</param>
        /// <param name="other">출동 체 정보</param>
        public virtual void OnStateTriggerStay(GameObject childGameObject, GameObject targetObject)
        {

        }

        //protected abstract On

        #endregion MonoBehaviour 기본 관련 : End =====================


        #region 디버그 관련 : Start ==================
        private void OnDrawGizmos()
        {
            if (currentState != null && firePoint != null)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(firePoint.position, enemyStats.lookSphereCastRadius);

            }
        }
        #endregion 디버그 관련 : End =================

        #region 상태 변환 처리 관련 : Start ===============
        /// <summary>
        /// 상태 이동 처리
        /// </summary>
        /// <param name="nextState"></param>
        public void TrnasitionToState(State nextState)
        {
            if (remainState != nextState)
            {
                currentState = nextState;
                OnExitState();
            }
        }

        /// <summary>
        /// 현재 상대에 duration이 있는 경우 카운딩 함
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        /// <summary>
        /// 현재 duration 초기화
        /// </summary>
        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }

        public virtual void Interactable(Entity interactable, int amount)
        {

        }
        #endregion 상태 변환 처리 관련 : End =============


    }

}
