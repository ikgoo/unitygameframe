using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    /// <summary>
    /// 오브젝트의 상태를 제어하는 클래스
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class StateController3D : StateController
    {
        protected override void Start()
        {
            gameDisplayMode = Managers.GameDisplayMode.Mode_3D;
            rigid3d = GetComponent<Rigidbody>();
            rigid3d.useGravity = false;
            nextWayPoint = 0;
        }

        protected override void Update()
        {
            base.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }

}
