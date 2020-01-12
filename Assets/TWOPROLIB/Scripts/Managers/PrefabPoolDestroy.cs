using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWOPROLIB.Scripts.Managers
{
    public class PrefabPoolDestroy : MonoBehaviour
    {
        /// <summary>
        /// Pool Object 자동 distory 처리 유무
        /// </summary>
        public bool IsAutoDistory = true;
        /// <summary>
        /// 자동 종료 처리 방식
        /// </summary>
        public DestroyType destroyType;
        /// <summary>
        /// 자동 종료 지연 시간
        /// </summary>
        public float DestroyTimeOut = 5f;

        private void OnEnable()
        {
            if (IsAutoDistory == true)
                Invoke("Destroy", DestroyTimeOut);
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

    }
}
