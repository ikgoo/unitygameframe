using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable3D : Interactable
    {
        Collider collider;

        protected void Start()
        {
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            StateController controller = other.GetComponent<StateController>();
            string tag = other.tag;
            Interact(other.tag, controller);
        }

    }
}
