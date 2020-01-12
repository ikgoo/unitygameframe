using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.Scripts.Controller;
using UnityEngine;

namespace TWOPROLIB.Scripts.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Interactable2D : Interactable
    {
        Collider2D collider;


        protected void Start()
        {
            collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StateController controller = collision.GetComponent<StateController>();
            Interact(collision.tag, controller);
        }

    }
}
