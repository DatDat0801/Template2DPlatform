using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Runtime
{
    public class HealthBarMng : MonoBehaviour
    {
        [SerializeField] private GameObject _goHealBarProcess;

        public void UpdateHealthBar(float percentHP)
        {
            this._goHealBarProcess.transform.DOScaleX(percentHP,0.3f);
        }
    }
}
