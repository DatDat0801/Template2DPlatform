using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Runtime
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instant;
        public CameraController cameraController;
        private List<IUpdate> _listUpdate = new List<IUpdate>();

        public Collider2D groundColl;

        [Header("Spawn Enemy Point")] public Transform leftPoint;
        public Transform rightPoint;

        [Header("Test Player")] public GameObject goWoodCutter;
        public GameObject goGraveRobber;
        public GameObject goSteamMan;

        [Header("Test Enemy")] public GameObject headStorm;
        public GameObject lightningFlash;
        public GameObject goChainLightning;
        public GameObject goCentipede;

        private PlayerActionMng _woodCutter;
        private PlayerActionMng _graveRobber;
        private PlayerActionMng _steamMan;

        private List<PlayerActionMng> characters = new List<PlayerActionMng>();
        private int currentChar = 0;
        private PlayerActionMng activePlayer;

        public PlayerActionMng ActivePlayer
        {
            get => this.activePlayer;
            set => this.activePlayer = value;
        }

        private void Awake()
        {
            if (instant == null) instant = this;

            var player = LeanPool.Spawn(goWoodCutter, Vector3.zero, goWoodCutter.transform.rotation);
            _woodCutter = player.GetComponent<PlayerActionMng>();
            this._woodCutter.coolDownSkill = UIManager.instant.SetPlayerSkillCoolDown;
            cameraController.RegisterFollow(player);
            activePlayer = this._woodCutter;

            var player2 = LeanPool.Spawn(goGraveRobber, Vector3.zero, goGraveRobber.transform.rotation);
            _graveRobber = player2.GetComponent<PlayerActionMng>();
            this._graveRobber.coolDownSkill = UIManager.instant.SetPlayerSkillCoolDown;


            var player3 = LeanPool.Spawn(goSteamMan, Vector3.zero, goSteamMan.transform.rotation);
            _steamMan = player3.GetComponent<PlayerActionMng>();
            this._steamMan.coolDownSkill = UIManager.instant.SetPlayerSkillCoolDown;

            characters.Add(_woodCutter);
            characters.Add(_graveRobber);
            characters.Add(_steamMan);
        }

        private void Start()
        {
            UpdatePlayerInforUI();
            SpawnEnemy(10f);
        }

        public void RegisterUpdate(IUpdate update)
        {
            if (this._listUpdate.Contains(update) == false)
            {
                this._listUpdate.Add(update);
            }
        }

        private void Update()
        {
            for (int i = 0; i < this._listUpdate.Count; i++)
            {
                this._listUpdate[i].DoUpdate();
            }

            GameLoop();
        }


        void GameLoop()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            }
        }

        async UniTaskVoid UpdatePlayerInforUI()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            UIManager.instant.UpdatePlayerInfor(this._woodCutter.CharacterData.id, this._woodCutter.UnitData.unitStat,
                this._woodCutter.GetListSkillID());
            this._woodCutter.onUpdateHP = UIManager.instant.UpdatePlayerHP;

            UIManager.instant.UpdatePlayerInfor(this._graveRobber.CharacterData.id, this._graveRobber.UnitData.unitStat,
                this._graveRobber.GetListSkillID());
            this._graveRobber.onUpdateHP = UIManager.instant.UpdatePlayerHP;
            _graveRobber.gameObject.SetActive(false);

            UIManager.instant.UpdatePlayerInfor(this._steamMan.CharacterData.id, this._steamMan.UnitData.unitStat,
                this._steamMan.GetListSkillID());
            this._steamMan.onUpdateHP = UIManager.instant.UpdatePlayerHP;
            _steamMan.gameObject.SetActive(false);
        }

        async UniTaskVoid SpawnEnemy(float delay)
        {
            while (_woodCutter.UnitStatus != UnitStatus.Death)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                int ran = Random.Range(1, 3);
                if (ran == 1)
                {
                    LeanPool.Spawn(goCentipede, this.leftPoint.position, goCentipede.transform.rotation);
                }
                else
                {
                    LeanPool.Spawn(goCentipede, this.rightPoint.position, goCentipede.transform.rotation);
                }
            }
        }

        public void ChangeCharacter(int id)
        {
            this.characters[currentChar].gameObject.SetActive(false);

            this.characters[id].gameObject.SetActive(true);
            this.characters[id].transform.position = this.characters[currentChar].transform.position;
            currentChar = id;
            this.cameraController.RegisterFollow(this.characters[id].gameObject);
            activePlayer = this.characters[id];
        }
    }
}