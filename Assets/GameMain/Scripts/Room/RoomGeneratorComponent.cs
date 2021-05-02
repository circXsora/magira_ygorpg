using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;

namespace BBYGO
{

    public class RoomGeneratorComponent : GameFrameworkComponent
    {
        private Direction _currentDirection;
        private int _currentX = 0, _currentY = 0;

        [Header("房间信息")]
        public int RoomNumber;
        public Color StartColor, EndColor;
        public LayerMask RoomLayer;

        [Header("位置控制")]
        public Vector3 InitialRoomPosition = Vector3.zero;
        private Vector3 _generatorPoint;
        public float XOffset, YOffset;

        private readonly List<RoomData> _roomDatas = new List<RoomData>();
        public RoomData EndRoom;
        public MonsterInfo[] GenMonsterList;

        private void Start()
        {
            //GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            //GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        private void OnDestroy()
        {
            //GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            //GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        private void OnShowEntityFailure(object sender, GameEventArgs e)
        {

        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {

        }

        public void GenerateRooms()
        {
            _generatorPoint = InitialRoomPosition;

            for (int i = 0; i < RoomNumber; i++)
            {
                var roomData = new RoomData { Position = _generatorPoint };
                _roomDatas.Add(roomData);
                //ChangePointPos();
            }

            _roomDatas[0].Type = RoomType.Start;
            _roomDatas[0].StepFormOrigin = 0;


            //Setup(_roomDatas[0]);
            //_roomDatas.ForEach(r => SetupDoor(r));

            // TODO:设置房间里的怪物信息
            //for (int i = 1; i < RoomNumber; i++)
            //{
            //    var monsterNum = UnityEngine.Random.Range(1, 4);
            //    var roomCtrl = _roomDatas[i].Room.GetComponent<RoomController>();
            //    roomCtrl.Monsters = new MonsterInfo[monsterNum];
            //    for (int j = 0; j < monsterNum; j++)
            //    {
            //        roomCtrl.Monsters[j] = GenMonsterList.Random();
            //        roomCtrl.SetupBattle();
            //    }
            //}
            //ComputeRoomHard();

            //_roomDatas.Sort((r, h) => r.StepFormOrigin.Value.CompareTo(h.StepFormOrigin.Value));

            //EndRoom = null;
            //var endlist = _roomDatas.FindAll(r => r.StepFormOrigin >= _roomDatas.Last().StepFormOrigin - 1);
            //for (int i = endlist.Count - 1; i >= 0; i--)
            //{
            //    if (endlist[i].WithRooms.Count(wr => wr != null) < 2)
            //    {
            //        EndRoom = endlist[i];
            //        break;
            //    }
            //}

            //if (EndRoom == null)
            //{
            //    EndRoom = _roomDatas.Last();
            //}
            //if (EndRoom.StepFormOrigin < rooms.Last().StepFormOrigin)
            //{
            //    Debug.Log("less! endRoom.SetupFromBase:" + EndRoom.StepFormOrigin + " rooms.Last().SetupFromBase:" + rooms.Last().StepFormOrigin);
            //}
            //EndRoom.Type = RoomType.End;
            foreach (var roomdata in _roomDatas)
            {
                GameEntry.Entity.ShowRoom(roomdata);
            }
        }

        internal void ComputeRoomHard()
        {
            //for (int i = 0; i < RoomNumber; i++)
            //{
            //    var roomCtrl = _roomDatas[i].Room.GetComponent<RoomController>();
            //    for (int j = 0; j < _roomDatas[i].WithRooms.Length; j++)
            //    {
            //        if (_roomDatas[i].WithRooms[j] == null)
            //        {
            //            continue;
            //        }
            //        var withRoomCtrl = _roomDatas[i].WithRooms[j].Room.GetComponent<RoomController>();
            //        roomCtrl.Doors[j].GetComponentInChildren<HardDisplay>().SetHard(withRoomCtrl.Monsters.Length);
            //    }
            //}
        }

        private void Setup(RoomData roomData)
        {
            for (int i = 0; i < 4; i++)
            {
                var direction = (Direction)i;
                //var coll = TestRoomAround(roomData.Room.transform, direction);
                //roomData.Room.GetComponent<RoomController>().Doors[i].SetActive(coll != null);
                //if (coll != null)
                //{
                //    var aroundInfo = coll.gameObject.GetComponent<RoomController>().Info;
                //    roomData.WithRooms[i] = aroundInfo;
                //    if (aroundInfo.StepFormOrigin.HasValue)
                //    {
                //        if (aroundInfo.StepFormOrigin.Value > roomData.StepFormOrigin.Value + 1)
                //        {
                //            aroundInfo.StepFormOrigin = roomData.StepFormOrigin.Value + 1;
                //            Setup(aroundInfo);
                //        }
                //    }
                //    else
                //    {
                //        aroundInfo.StepFormOrigin = roomData.StepFormOrigin.Value + 1;
                //        Setup(aroundInfo);
                //    }
                //}
            }
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //}
        }

        private void ChangePointPos()
        {
            int x, y;
            Vector3 p;
            int n = 0;

            do
            {
                n++;

                if (n % 20 == 0)
                {
                    _generatorPoint = _roomDatas[UnityEngine.Random.Range(0, _roomDatas.Count)].Position;
                }
                if (n % 50 == 0)
                {
                    for (int i = 0; i < _roomDatas.Count; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            var direction = (Direction)j;
                            //if (null == TestRoomAround(_roomDatas[i].Room.transform, direction))
                            //{
                            //    _generatorPoint = _roomDatas[i].Room.transform.position;
                            //}
                        }
                    }
                }

                _currentDirection = (Direction)UnityEngine.Random.Range(0, 4);
                x = _currentX;
                y = _currentY;
                p = _generatorPoint;

                switch (_currentDirection)
                {
                    case Direction.Up:
                        y += 1;
                        p += new Vector3(0, YOffset, 0);
                        break;
                    case Direction.Down:
                        y -= 1;
                        p += new Vector3(0, -YOffset, 0);
                        break;
                    case Direction.Left:
                        x -= 1;
                        p += new Vector3(-XOffset, 0, 0);
                        break;
                    case Direction.Right:
                        x += 1;
                        p += new Vector3(XOffset, 0, 0);
                        break;
                    default:
                        break;
                }


            } while (TestPointIsInRoom(p));
            _currentX = x;
            _currentY = y;
            _generatorPoint = p;
        }
        private bool TestPointIsInRoom(Vector3 point)
        {
            return Physics2D.OverlapCircle(point, .2f, RoomLayer.value);
        }

        private Collider2D TestRoomAround(Transform room, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Physics2D.OverlapCircle(room.transform.position + new Vector3(0, YOffset, 0), .2f, RoomLayer.value);
                case Direction.Down:
                    return Physics2D.OverlapCircle(room.transform.position + new Vector3(0, -YOffset, 0), .2f, RoomLayer.value);
                case Direction.Left:
                    return Physics2D.OverlapCircle(room.transform.position + new Vector3(-XOffset, 0, 0), .2f, RoomLayer.value);
                case Direction.Right:
                    return Physics2D.OverlapCircle(room.transform.position + new Vector3(XOffset, 0, 0), .2f, RoomLayer.value);
                default:
                    Debug.LogError("no such direction!");
                    return null;
            }
        }

        //命名规则上下左右

        // 单出口
        public GameObject wall0001, wall1000, wall0010, wall0100;
        // 双出口
        public GameObject wall0011, wall1100, wall1001, wall0101, wall0110, wall1010;
        // 三出口
        public GameObject wall1101, wall0111, wall1110, wall1011;
        // 四出口
        public GameObject wall1111;
        // boss关
        public GameObject end_wall;

        void SetupDoor(RoomData newRoom)
        {
            string up = (newRoom.WithRooms[(int)Direction.Up] != null ? 1 : 0).ToString();
            string down = (newRoom.WithRooms[(int)Direction.Down] != null ? 1 : 0).ToString();
            string left = (newRoom.WithRooms[(int)Direction.Left] != null ? 1 : 0).ToString();
            string right = (newRoom.WithRooms[(int)Direction.Right] != null ? 1 : 0).ToString();
            var wallAttribute = "wall" + up + down + left + right;
            GameObject wall_prefab = this.GetType().GetField(wallAttribute).GetValue(this) as GameObject;
            //newRoom.Room.GetComponent<RoomController>().Wall = Instantiate(wall_prefab, newRoom.Room.transform);
        }
    }
}