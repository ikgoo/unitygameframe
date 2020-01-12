using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects.Entitys;
using UnityEngine;

namespace TWOPROLIB.Scripts.Controller
{
    public class StateController2D_Test : StateController2D
    {
        protected override void Start()
        {
            base.Start();
            //rigid2d.gravityScale = 0f;
        }
        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Interactable(Entity interactable, int amount)
        {
            switch(interactable.EntityType)
            {
                case Managers.EntityTypes.COIN:
                    stats.coin += amount;
                    break;
            }
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
