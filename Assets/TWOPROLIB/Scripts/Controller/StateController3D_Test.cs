using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    public class StateController3D_Test : StateController3D
    {
        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnStateTriggerEnter(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateTriggerExit(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateTriggerStay(GameObject childGameObject, GameObject targetObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
