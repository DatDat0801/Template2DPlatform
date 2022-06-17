using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface IHurt
    {
        /// <summary>
        /// return true if target die
        /// </summary>
        /// <param name="subHP"></param>
        /// <returns></returns>
        bool GetHurt(float subHP);
    }
}
