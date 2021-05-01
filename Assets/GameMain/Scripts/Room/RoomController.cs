using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BBYGO
{
    public class RoomController : MonoBehaviour
    {
        public RoomInfo Info;

        public TMPro.TMP_Text StepText;

        public GameObject[] Doors;
        public GameObject Wall;

        public MonsterInfo[] Monsters;
        public bool Battled = false;
        public BattleTrigger BattleTrigger;
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
                FindObjectOfType<CameraController>().Target = transform;

                if (!Battled && Monsters.Length > 0)
                {
                    foreach (var door in Doors)
                    {
                        door.GetComponent<Collider2D>().enabled = true;
                    }
                }
            }
        }

    }
}
