using TWOPROLIB.Scripts.Controller;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Forward")]
    public class ForwardAction : Action
    {
        /// <summary>
        /// 이동 방향
        /// </summary>
        [Tooltip("이동 방향")]
        public Direction dir = Direction.Forward;

        public override void Act(StateController controller)
        {
            switch (controller.gameDisplayMode)
            {
                case GameDisplayMode.Mode_2D:

                    if(controller.isRigidbody == true)
                    {
                        switch(dir)
                        {
                            case Direction.Forward:
                                controller.transform.Translate(controller.transform.up * controller.stats.speed * Time.deltaTime);
                                break;

                            default:
                                break;
                        }
                    }

                    break;

                default:
                    break;


            }
        }
    }
}
