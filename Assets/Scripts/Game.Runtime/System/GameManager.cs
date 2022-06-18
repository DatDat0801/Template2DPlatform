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
        private bool isEndGame;

        public Collider2D groundColl;

        [Header("Spawn Enemy Point")] public Transform leftPoint;
        public Transform rightPoint;

        [Header("Test Player")] public GameObject goWoodCutter;
        public GameObject goGraveRobber;
        public GameObject goSteamMan;

        [Header("Test Enemy")] public GameObject headStorm;
        public GameObject lightningFlash;
        public GameObject goChainLightning;
        public List<GameObject> goEnemies;

        [Header("Test Item")] public GameObject hpItem;

        private PlayerActionMng _woodCutter;
        private PlayerActionMng _graveRobber;
        private PlayerActionMng _steamMan;

        private List<PlayerActionMng> characters = new List<PlayerActionMng>();
        private int currentChar = 0;
        private PlayerActionMng activePlayer;
        private int _countDeath;

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
            this._woodCutter.onDeathUIUpdate = OnPlayerDeathUpdate;
            cameraController.RegisterFollow(player);
            activePlayer = this._woodCutter;

            var player2 = LeanPool.Spawn(goGraveRobber, Vector3.zero, goGraveRobber.transform.rotation);
            _graveRobber = player2.GetComponent<PlayerActionMng>();
            this._graveRobber.coolDownSkill = UIManager.instant.SetPlayerSkillCoolDown;
            this._graveRobber.onDeathUIUpdate = OnPlayerDeathUpdate;


            var player3 = LeanPool.Spawn(goSteamMan, Vector3.zero, goSteamMan.transform.rotation);
            _steamMan = player3.GetComponent<PlayerActionMng>();
            this._steamMan.coolDownSkill = UIManager.instant.SetPlayerSkillCoolDown;
            this._steamMan.onDeathUIUpdate = OnPlayerDeathUpdate;

            characters.Add(_woodCutter);
            characters.Add(_graveRobber);
            characters.Add(_steamMan);
            SetTimeScale(0);
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
            while (this.isEndGame == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                int ran = Random.Range(0, 3);
                Vector2 pos;
                int ranPos = Random.Range(0, 2);
                if (ranPos == 0)
                {
                    pos = this.leftPoint.position;
                }
                else
                {
                    pos = this.rightPoint.position;
                }

                LeanPool.Spawn(goEnemies[ran], pos, goEnemies[ran].transform.rotation);
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

        public void InsHPItem(Vector2 pos)
        {
            var obj = LeanPool.Spawn(this.hpItem, new Vector2(pos.x, pos.y + 1f), this.hpItem.transform.rotation);
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15f;
        }

        public void SetTimeScale(int scale)
        {
            Time.timeScale = scale;
        }


        void OnPlayerDeathUpdate()
        {
            this._countDeath += 1;
            if (this._countDeath >= 3)
            {
                UIManager.instant.ShowEndGame();
                this.isEndGame = true;
            }
        }
    }
}