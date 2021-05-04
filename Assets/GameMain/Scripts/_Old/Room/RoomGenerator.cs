using BBYGO;
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public int? StepFormOrigin;
    public GameObject Room;
    public RoomInfo[] WithRooms = new RoomInfo[4];
}

public class RoomGenerator : MGO.SingletonInScene<RoomGenerator>
{
    private BBYGO.Direction _currentDir;
    private int _currentX = 0, _currentY = 0;

    [Header("房间信息")]
    public GameObject RoomPrefab;
    public int RoomNumber;
    public Color StartColor, EndColor;
    public LayerMask RoomLayer;

    [Header("位置控制")]
    public Vector3 GeneratorPoint;
    public float XOffset, YOffset;

    private List<RoomInfo> rooms = new List<RoomInfo>();
    public RoomInfo EndRoom;
    public MonsterInfo[] GenMonsterList;
    private void Start()
    {
        GeneratorPoint = transform.position;

        for (int i = 0; i < RoomNumber; i++)
        {
            var room = Instantiate(RoomPrefab, GeneratorPoint, Quaternion.identity);
            var info = new RoomInfo { Room = room };
            var roomCtrl = room.GetComponent<RoomController>();
            roomCtrl.Info = info;
            rooms.Add(info);
            ChangePointPos();
        }

        rooms[0].Room.GetComponent<SpriteRenderer>().color = StartColor;
        rooms[0].Room.GetComponent<SpriteRenderer>().enabled = true;
        rooms[0].StepFormOrigin = 0;


        Setup(rooms[0]);
        rooms.ForEach(r => SetupDoor(r));
        for (int i = 1; i < RoomNumber; i++)
        {
            var monsterNum = UnityEngine.Random.Range(1, 4);
            var roomCtrl = rooms[i].Room.GetComponent<RoomController>();
            roomCtrl.Monsters = new MonsterInfo[monsterNum];
            for (int j = 0; j < monsterNum; j++)
            {
                roomCtrl.Monsters[j] = GenMonsterList.Random1();
                roomCtrl.SetupBattle();
            }
        }
        ComputeRoomHard();

        rooms.Sort((r, h) => r.StepFormOrigin.Value.CompareTo(h.StepFormOrigin.Value));
        //rooms.OrderBy(r => r.SetupFromBase.Value);
        EndRoom = null;
        var endlist = rooms.FindAll(r => r.StepFormOrigin >= rooms.Last().StepFormOrigin - 1);
        for (int i = endlist.Count - 1; i >= 0; i--)
        {
            if (endlist[i].WithRooms.Count(wr => wr != null) < 2)
            {
                EndRoom = endlist[i];
                break;
            }
        }

        if (EndRoom == null)
        {
            EndRoom = rooms.Last();
        }
        //if (EndRoom.StepFormOrigin < rooms.Last().StepFormOrigin)
        //{
        //    Debug.Log("less! endRoom.SetupFromBase:" + EndRoom.StepFormOrigin + " rooms.Last().SetupFromBase:" + rooms.Last().StepFormOrigin);
        //}
        EndRoom.Room.GetComponent<SpriteRenderer>().color = EndColor;
        EndRoom.Room.GetComponent<SpriteRenderer>().enabled = true;
    }

    internal void ComputeRoomHard()
    {
        for (int i = 0; i < RoomNumber; i++)
        {
            var roomCtrl = rooms[i].Room.GetComponent<RoomController>();
            for (int j = 0; j < rooms[i].WithRooms.Length; j++)
            {
                if (rooms[i].WithRooms[j] == null)
                {
                    continue;
                }
                var withRoomCtrl = rooms[i].WithRooms[j].Room.GetComponent<RoomController>();
                roomCtrl.Doors[j].GetComponentInChildren<HardDisplay>().SetHard(withRoomCtrl.Monsters.Length);
            }
        }
    }

    private void Setup(RoomInfo roomInfo)
    {
        for (int i = 0; i < 4; i++)
        {
            var direction = (Direction)i;
            var coll = TestRoomAround(roomInfo.Room.transform, direction);
            roomInfo.Room.GetComponent<RoomController>().Doors[i].SetActive(coll != null);
            if (coll != null)
            {
                var aroundInfo = coll.gameObject.GetComponent<RoomController>().Info;
                roomInfo.WithRooms[i] = aroundInfo;
                if (aroundInfo.StepFormOrigin.HasValue)
                {
                    if (aroundInfo.StepFormOrigin.Value > roomInfo.StepFormOrigin.Value + 1)
                    {
                        aroundInfo.StepFormOrigin = roomInfo.StepFormOrigin.Value + 1;
                        Setup(aroundInfo);
                    }
                }
                else
                {
                    aroundInfo.StepFormOrigin = roomInfo.StepFormOrigin.Value + 1;
                    Setup(aroundInfo);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
                GeneratorPoint = rooms[UnityEngine.Random.Range(0, rooms.Count)].Room.transform.position;
            }
            if (n % 50 == 0)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var direction = (Direction)j;
                        if (null == TestRoomAround(rooms[i].Room.transform, direction))
                        {
                            GeneratorPoint = rooms[i].Room.transform.position;
                        }
                    }
                }
            }

            _currentDir = (Direction)UnityEngine.Random.Range(0, 4);
            x = _currentX;
            y = _currentY;
            p = GeneratorPoint;

            switch (_currentDir)
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
        GeneratorPoint = p;
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

    void SetupDoor(RoomInfo newRoom)
    {
        string up = (newRoom.WithRooms[(int)Direction.Up] != null ? 1 : 0).ToString();
        string down = (newRoom.WithRooms[(int)Direction.Down] != null ? 1 : 0).ToString();
        string left = (newRoom.WithRooms[(int)Direction.Left] != null ? 1 : 0).ToString();
        string right = (newRoom.WithRooms[(int)Direction.Right] != null ? 1 : 0).ToString();
        var wallAttribute = "wall" + up + down + left + right;
        GameObject wall_prefab = this.GetType().GetField(wallAttribute).GetValue(this) as GameObject;

        newRoom.Room.GetComponent<RoomController>().Wall = Instantiate(wall_prefab, newRoom.Room.transform);
    }
}
