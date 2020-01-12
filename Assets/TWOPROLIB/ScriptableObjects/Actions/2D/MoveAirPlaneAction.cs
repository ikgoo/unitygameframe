﻿using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    /// <summary>
    /// 비행이 이동 모양
    /// </summary>
    [CreateAssetMenu(menuName = "PluggableAI/Actions/MoveAirPlane")]
    public class MoveAirPlaneAction : Action
    {
        public override void Act(StateController controller)
        {
            if(controller.gameDisplayMode == Scripts.Managers.GameDisplayMode.Mode_2D)
            {
                // 2D의 경우 처리
                controller.rigid2d.velocity = controller.transform.up * controller.stats.speed;

                if(controller.isInput == true)
                {
                    Vector2 direction = (Vector2)Input.GetNormalizedAxis() + (Vector2)controller.transform.position - controller.rigid2d.position;
                    direction.Normalize();

                    float rotateValue = Vector3.Cross(direction, controller.transform.up).z;
                    controller.rigid2d.angularVelocity = -rotateValue * controller.stats.angleSpeed;
                }
            }
        }
    }
}
