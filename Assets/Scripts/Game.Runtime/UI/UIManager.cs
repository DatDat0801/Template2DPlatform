using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instant;
        public List<PlayerInforUI> playerInforUis;
        private int idDisable = 0;
        private void Awake()
        {
            if (instant == null) instant = this;
            playerInforUis[0].SetDisable(false);
        }

        private void Start()
        {
            for (int i = 0; i < playerInforUis.Count; i++)
            {
                int tmp = i;
                playerInforUis[i].btn.onClick.AddListener(() => {
                    ChangePlayer(tmp);
                });
                
            }
        }

        public void UpdatePlayerInfor(int idPlayer, UnitBaseStat unitBaseStat,List<int> skillID)
        {
            playerInforUis[idPlayer].SetNameHP(unitBaseStat.name);
            playerInforUis[idPlayer].SetHP(unitBaseStat.hp);
            playerInforUis[idPlayer].SetImageSkill(skillID);
        }
        
        public void UpdatePlayerHP(int idPlayer,float hp)
        {
            playerInforUis[idPlayer].SetHP(hp);
        }

        void ChangePlayer(int id)
        {
            playerInforUis[idDisable].SetDisable(true);
            this.idDisable = id;
            playerInforUis[idDisable].SetDisable(false);
            GameManager.instant.ChangeCharacter(id);
        }

        public void SetPlayerSkillCoolDown(int indexPlayer, int indexSkill, float timeCoolDown, Action onFinish)
        {
            if (this.playerInforUis != null)
            {
                this.playerInforUis[indexPlayer].SetCooldown(indexSkill, timeCoolDown, onFinish);
            }
        }
    }
}
