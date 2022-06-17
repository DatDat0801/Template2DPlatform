using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CameraController : MonoBehaviour
    {
        private GameObject follow;
        private void Update()
        {
            if (this.follow != null)
            {
                transform.position = new Vector3(this.follow.transform.position.x,transform.position.y,transform.position.z);
            }
        }

        public void RegisterFollow(GameObject obj)
        {
            follow = obj;
        }

        public void CancelFollow()
        {
            this.follow = null;
        }
    }
}
