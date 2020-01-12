﻿/**
 * Title : 오프젝트 풀 처리 
 * Desc :
 * 게임 플레이 중에 해당하는 오브젝트를 컨트롤
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GamePrefabPoolManager

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// 오프젝트 플링 매니져
    /// </summary>
    public class GamePrefabPoolManager : MonoBehaviour
    {
        /// <summary>
        /// GameObject를 poolName으로 생성
        /// </summary>
        string poolName = "ObjectPool";

        /// <summary>
        /// 등록된 오브젝트 또는 프리팹 리스트
        /// </summary>
        [Tooltip("등록된 오브젝트 또는 프리팹 리스트")]
        public GameObject[] objectPrefabs;

        /// <summary>
        /// 실행시 인스턴스화 된 오프젝트 또는 프리팹 리스트
        /// </summary>
        [Tooltip("실행시 인스턴스화 된 오프젝트 또는 프리팹 리스트")]
        public Dictionary<string, List<GameObject>> pooledObjects;

        /// <summary>
        /// 오디오 소스 버퍼
        /// </summary>
        [Tooltip("오프젝트 인스턴스 버퍼")]
        public int[] amountToBuffer;

        /// <summary>
        /// 기본 오디오 소스 버퍼
        /// </summary>
        [Tooltip("기본 오프젝트 인스턴스 버퍼")]
        public int defaultBufferAmount = 3;

        ///// <summary>
        ///// 각 오브젝트의 상위 오브젝트(없을 경우 기본 poolmanager쪽 오프젝트를 상위로 함)
        ///// </summary>
        //[Tooltip("부모 오브젝트")]
        //public GameObject[] parentGameObject;

        /// <summary>
        /// 버퍼 부족시 자동 인스턴스 생성 여부
        /// </summary>
        [Tooltip("버퍼 부족시 자동 인스턴스 생성 여부")]
        public bool willGrow = true;

        /// <summary>
        /// poolmanager 최상의 오브젝트
        /// </summary>
        protected GameObject containerObject;

        /// <summary>
        /// 각 오브젝트의 중간 오브젝트
        /// </summary>
        protected Dictionary<string, GameObject> midParentObject;

        public static GamePrefabPoolManager Instance = null;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        // 각 오프젝트를 버퍼 수만큼 생성
        void Start()
        {
            containerObject = new GameObject(poolName);
            containerObject.transform.parent = transform;
            containerObject.transform.position = Vector3.zero;
            pooledObjects = new Dictionary<string, List<GameObject>>();
            midParentObject = new Dictionary<string, GameObject>();

            int i = 0;
            GameObject tmpGameObject;

            foreach (GameObject objectPrefab in objectPrefabs)
            {
                objectPrefab.SetActive(false);      // 오프젝트 풀에 넣기 전에 모두 비활성화 함
                pooledObjects.Add(objectPrefab.name, new List<GameObject>());

                int bufferAmount;
                if (i < amountToBuffer.Length) bufferAmount = amountToBuffer[i];
                else
                    bufferAmount = defaultBufferAmount;

                // sub group 생성
                tmpGameObject = new GameObject(objectPrefab.name);

                tmpGameObject.transform.parent = containerObject.transform;

                // 상위 오프젝트 개념 없어짐(무조건 지정된 위치에 저장)
                //if (parentGameObject.Length > i && parentGameObject[i] != null)
                //{
                //    if (parentGameObject[i].GetComponent<Canvas>() != null)
                //    {
                //        tmpGameObject.AddComponent<RectTransform>();
                //        tmpGameObject.transform.SetParent(parentGameObject[i].transform, false);
                //    }
                //    else
                //        tmpGameObject.transform.SetParent(parentGameObject[i].transform);
                //}
                //else
                //    tmpGameObject.transform.parent = containerObject.transform;
                midParentObject.Add(objectPrefab.name, tmpGameObject);

                for (int n = 0; n < bufferAmount; n++)
                {
                    GameObject newObj = Instantiate(objectPrefab) as GameObject;
                    newObj.name = objectPrefab.name;
                    PoolObject(newObj);
                }
                i++;
            }
        }

        /// <summary>
        /// pool 오프젝트의 상위 오프젝트 추출
        /// </summary>
        /// <param name="objectType">대상 오브젝트 명</param>
        /// <returns></returns>
        public GameObject GetRootParent(string objectType)
        {
            if (midParentObject.ContainsKey(objectType) == true)
            {
                GameObject tt = midParentObject[objectType].transform.parent.gameObject;
                RectTransform ttt = tt.GetComponent<RectTransform>();
                return midParentObject[objectType].transform.parent.gameObject;
            }

            return null;

        }

        /// <summary>
        /// pool 오프젝트의 상위 오프젝트에 위치 정보 추출
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public RectTransform GetRootParentRectTransform(string objectType)
        {
            if (midParentObject.ContainsKey(objectType) == true)
            {
                return midParentObject[objectType].transform.parent.gameObject.GetComponent<RectTransform>();
            }

            return null;

        }

        /// <summary>
        /// poolmanager에 오프젝트 추출
        /// </summary>
        /// <param name="objectType">오프젝트 명</param>
        /// <param name="active">활성화 유무</param>
        /// <returns></returns>
        public GameObject GetObjectForType(string objectType, bool active = false)
        {
            List<GameObject> tmpList = pooledObjects[objectType];
            if (tmpList == null)
            {
                Debug.Log("Not prefab[" + objectType + "]");
                return null;
            }

            for (int i = 0; i < tmpList.Count; i++)
            {
                if (!tmpList[i].activeInHierarchy)
                {
                    tmpList[i].SetActive(active);
                    return tmpList[i];
                }
            }

            if (willGrow)
            {
                foreach (GameObject objectPrefab in objectPrefabs)
                {
                    if (objectPrefab.name.Equals(objectType))
                    {
                        GameObject newObj = Instantiate(objectPrefab) as GameObject;
                        newObj.name = objectPrefab.name;
                        PoolObject(newObj, active);
                        return newObj;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 대상 오프젝트를 poolmanager에 추가
        /// </summary>
        /// <param name="obj">게임 오프젝트</param>
        /// <param name="active">활성화 여부</param>
        public void PoolObject(GameObject obj, bool active = false)
        {
            obj.SetActive(active);
            List<GameObject> tmpList = pooledObjects[obj.name];
            if (tmpList == null)
                return;

            tmpList.Add(obj);
            //obj.transform.parent = midParentObject[obj.name].transform;

            obj.transform.transform.SetParent(midParentObject[obj.name].transform, false);
        }

        /// <summary>
        /// 모든 오브젝트를 활성화또는 비활성화 시킴
        /// </summary>
        public void AllDestroy(string name, bool isBool)
        {
            if (isBool)//활성화 시킴
            {

                for (int i = 0; i < pooledObjects[name].Count; i++)
                {
                    //모든 오브젝트 수만큼
                    //돌리고 비활성화된것만 활성화 시킴
                    if (!pooledObjects[name][i].activeSelf)
                    {
                        pooledObjects[name][i].SetActive(true);
                    }
                }

            }
            else//비활성화 시킴
            {
                for (int i = 0; i < pooledObjects[name].Count; i++)
                {
                    //모든 오브젝트 수만큼
                    //돌리고 비활성화된것만 활성화 시킴
                    if (pooledObjects[name][i].activeSelf)
                    {
                        pooledObjects[name][i].SetActive(false);
                    }
                }
            }
        }
    }

}
