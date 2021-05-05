using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public class Room : Entity
    {
        private TMPro.TMP_Text _stepText;
        private Transform[] _doors = new Transform[4];
        private Wall _wall;

        private bool _battled = false;
        private BattleTrigger _battleTrigger;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            for (int i = 0, doorIndex = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.name.Contains("Door"))
                {
                    _doors[doorIndex++] = child;
                }
            }
            _battleTrigger = GetComponentInChildren<BattleTrigger>(true);
            _stepText = GetComponentInChildren<TMPro.TMP_Text>(true);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            RoomData _data = userData as RoomData;

            for (int i = 0; i < _data.DoorsActiveInfos.Length; i++)
            {
                _doors[i].gameObject.SetActive(_data.DoorsActiveInfos[i]);
            }

            GameEntry.Entity.ShowWall(new WallData(_data.WallID.Value, _data.Id));
            _stepText.text = _data.StepFormOrigin.GetValueOrDefault(-1).ToString();
            SetupBattleTrigger(_data);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            if (childEntity is Wall)
            {
                _wall = childEntity as Wall;
            }
        }

        public void SetupBattleTrigger(RoomData roomData)
        {
            if (!_battled && roomData.MonsterDatas != null && roomData.MonsterDatas.Length > 0)
            {
                var monsterData = roomData.MonsterDatas[0];
                var dtMonster = GameEntry.DataTable.GetDataTable<DRMonster>();
                var drMonster = dtMonster.GetDataRow(monsterData.TypeId);
                GameEntry.Resource.LoadAsset(AssetUtility.GetTextureAsset(drMonster.SpriteAssetName), typeof(Sprite), Constant.AssetPriority.TextureAsset,
                    new GameFramework.Resource.LoadAssetCallbacks(
                    loadAssetSuccessCallback: OnLoadAssetSuccess, loadAssetFailureCallback: OnLoadAssetFailure)
                );

                void OnLoadAssetSuccess(string assetName, object asset, float duration, object userData)
                {
                    _battleTrigger.GetComponent<SpriteRenderer>().sprite = asset as Sprite;
                }

                void OnLoadAssetFailure(string assetName, GameFramework.Resource.LoadResourceStatus status, string errorMessage, object userData)
                {
                    Log.Error(assetName + " " + status.ToString() + " " + errorMessage);
                }
                //_battleTrigger.GetComponent<SpriteRenderer>().sprite;
            }
        }

        public void SetupDoorDifficultValue(RoomData roomData)
        {

        }

        public void SetupBattle()
        {
            //if (!Battled && Monsters.Length > 0)
            //{
            //    try
            //    {
            //        BattleTrigger.GetComponent<SpriteRenderer>().sprite = Monsters[0].Sprite;
            //    }
            //    catch (System.Exception)
            //    {

            //        throw;
            //    }
            //}
            //BattleTrigger.OnTriggerEnter.AddListener(() =>
            //{
            //    if (!Battled && Monsters.Length > 0)
            //    {
            //        GameManager.Instance.Battle(this);
            //    }
            //});
        }

        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Player")
            {
                //FindObjectOfType<CameraController>().Target = transform;

                //if (!Battled && Monsters.Length > 0)
                //{
                //    foreach (var door in _doors)
                //    {
                //        door.GetComponent<Collider2D>().enabled = true;
                //    }
                //}
            }
        }
    }
}
