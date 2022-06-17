using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class MoveLeft : MonoBehaviour,IMoveLeft
    {
        private Rigidbody2D _rb;
        private void Start()
        {
            if (this._rb == null)
            {
                this._rb = GetComponent<Rigidbody2D>();
            }
        }

        public void DoMoveLeft(float speed)
        {
            this._rb.velocity = new Vector2(-speed*Time.deltaTime,this._rb.velocity.y);
        }
    }
}
