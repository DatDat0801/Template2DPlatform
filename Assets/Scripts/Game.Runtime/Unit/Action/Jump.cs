using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Jump : MonoBehaviour,IJump
    {
        private Rigidbody2D _rb;
        private void Start()
        {
            if (this._rb == null)
            {
                this._rb = GetComponent<Rigidbody2D>();
            }
        }
        public void DoJump(float jumpForce)
        {
            this._rb.velocity = new Vector2(this._rb.velocity.x,jumpForce);
        }
    }
}
