using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instant;
        public List<PlayerInforUI> playerInforUis;
        private int idDisable = 0;

        public Button btnStart;
        public GameObject goStartContainer;
        public GameObject goContent1;
        public GameObject goContent2;
        private void Awake()
        {
            if (instant == null) instant = this;
            playerInforUis[0].SetDisable(false);
        }

        private void Start()
        {
            this.btnStart.onClick.AddListener(OnClickStartGame);
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

        void OnClickStartGame()
        {
            StartGame();
           // StartCoroutine(StartPlayGame());
        }

        async UniTaskVoid StartGame()
        {
            this.goStartContainer.SetActive(true);
            goContent1.SetActive(false);
            goContent2.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(5),true);
            goContent1.SetActive(false);
            goContent2.SetActive(false);
            this.goStartContainer.SetActive(false);
            GameManager.instant.SetTimeScale(1);
        }

        IEnumerator StartPlayGame()
        {
            this.goStartContainer.SetActive(true);
            goContent1.SetActive(false);
            goContent2.SetActive(true);
            yield return new WaitForSeconds(3f);
            goContent1.SetActive(false);
            goContent2.SetActive(false);
            this.goStartContainer.SetActive(false);
            GameManager.instant.SetTimeScale(1);
        }
    }
}
