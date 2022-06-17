using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PlayerInforUI : MonoBehaviour
    {
        public Button btn;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtHP;
        public List<Sprite> spIcons;
        public List<Image> imgIcons;
        public List<Image> imgCooldown;
        
        public void SetNameHP(string name)
        {
            this.txtHP.text = name;
        }
        
        public void SetHP(float currentHP)
        {
            this.txtHP.text = "HP: " + currentHP;
        }

        public void SetImageSkill(List<int> lstIdSkill)
        {
            for (int i = 0; i < imgIcons.Count && i < lstIdSkill.Count; i++)
            {
                imgIcons[i].sprite = spIcons[lstIdSkill[i]];
            }
        }

        public void SetDisable(bool interactable)
        {
            btn.interactable = interactable;
        }

        public async UniTaskVoid SetCooldown(int skillIndex, float timeCooldown,Action onFinish=null)
        {
            // imgCooldown[skillIndex].fillAmount = 1;
            // var tmp = timeCooldown;
            // while (tmp>=0)
            // {
            //     UniTask.DelayFrame(3);
            //     tmp -= 0.1f;
            //     float percent = (tmp * 1f) / (timeCooldown * 1f);
            //     imgCooldown[skillIndex].fillAmount = percent;
            //     UniTask.Delay(TimeSpan.FromSeconds(10f));
            // }
            StartCoroutine(SetCooldown2(skillIndex, timeCooldown));
        }

        IEnumerator SetCooldown2(int skillIndex, float timeCooldown,Action onFinish=null)
        {
            imgCooldown[skillIndex].fillAmount = 1;
            var tmp = timeCooldown;
            while (tmp>=0)
            {
                yield return new WaitForSeconds(0.2f);
                tmp -= 0.1f;
                float percent = (tmp * 1f) / (timeCooldown * 1f);
                imgCooldown[skillIndex].fillAmount = percent;
               // UniTask.Delay(TimeSpan.FromSeconds(10f));
            }

            if (tmp < 0)
            {
                onFinish?.Invoke();
            }
        }
        
    }
}
