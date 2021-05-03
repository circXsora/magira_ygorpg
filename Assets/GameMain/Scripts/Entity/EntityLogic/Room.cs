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

        public bool Battled = false;
        public BattleTrigger BattleTrigger;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            RoomData _data = userData as RoomData;
            for (int i = 0, doorIndex = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.name.Contains("Door"))
                {
                    _doors[doorIndex++] = child;
                }
            }

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
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            if (childEntity is Wall)
            {
                _wall = childEntity as Wall;
            }
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
            //StepText.text = Info?.StepFormOrigin.GetValueOrDefault(-1).ToString();
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
