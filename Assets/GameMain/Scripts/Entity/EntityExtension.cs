//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static UniversalEntityLogic GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (UniversalEntityLogic)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, UniversalEntityLogic entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, UniversalEntityLogic entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        //public static void ShowMyAircraft(this EntityComponent entityComponent, MyAircraftData data)
        //{
        //    entityComponent.ShowEntity(typeof(MyAircraft), "Aircraft", Constant.AssetPriority.MyAircraftAsset, data);
        //}

        //public static void ShowAircraft(this EntityComponent entityComponent, AircraftData data)
        //{
        //    entityComponent.ShowEntity(typeof(Aircraft), "Aircraft", Constant.AssetPriority.AircraftAsset, data);
        //}

        //public static void ShowThruster(this EntityComponent entityComponent, ThrusterData data)
        //{
        //    entityComponent.ShowEntity(typeof(Thruster), "Thruster", Constant.AssetPriority.ThrusterAsset, data);
        //}

        //public static void ShowWeapon(this EntityComponent entityComponent, WeaponData data)
        //{
        //    entityComponent.ShowEntity(typeof(Weapon), "Weapon", Constant.AssetPriority.WeaponAsset, data);
        //}

        //public static void ShowArmor(this EntityComponent entityComponent, ArmorData data)
        //{
        //    entityComponent.ShowEntity(typeof(Armor), "Armor", Constant.AssetPriority.ArmorAsset, data);
        //}

        //public static void ShowBullet(this EntityComponent entityCompoennt, BulletData data)
        //{
        //    entityCompoennt.ShowEntity(typeof(Bullet), "Bullet", Constant.AssetPriority.BulletAsset, data);
        //}

        //public static void ShowAsteroid(this EntityComponent entityCompoennt, AsteroidData data)
        //{
        //    entityCompoennt.ShowEntity(typeof(Asteroid), "Asteroid", Constant.AssetPriority.AsteroiAsset, data);
        //}

        public static void ShowPlayer(this EntityComponent entityComponent, PlayerData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DRPlayer> dtEntity = GameEntry.DataTable.GetDataTable<DRPlayer>();
            DRPlayer drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, typeof(PlayerLogic), AssetUtility.GetEntityAsset(drEntity.AssetName), "Player", Constant.AssetPriority.PlayerAsset, data);
        }

        public static void ShowBattleFieldPlayer(this EntityComponent entityComponent, BattleFieldPlayerData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DRPlayer> dtEntity = GameEntry.DataTable.GetDataTable<DRPlayer>();
            DRPlayer drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, typeof(BattleFieldPlayerLogic), AssetUtility.GetEntityAsset(drEntity.AssetName), "Player", Constant.AssetPriority.PlayerAsset, data);

        }

        public static void ShowRoom(this EntityComponent entityComponent, RoomData data)
        {
            entityComponent.ShowEntity(typeof(Room), "Room", Constant.AssetPriority.RoomAsset, data);
        }

        public static void ShowWall(this EntityComponent entityComponent, WallData data)
        {
            entityComponent.ShowEntity(typeof(Wall), "Wall", Constant.AssetPriority.RoomAsset, data);
        }

        public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        {
            entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        }

        public static void ShowCamera(this EntityComponent entityComponent, CameraData data)
        {
            IDataTable<DRCamera> dtCamera = GameEntry.DataTable.GetDataTable<DRCamera>();
            var drCamera = dtCamera.GetDataRow(data.TypeId);
            if (drCamera == null)
            {
                Log.Warning("Can not load Camera id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, Type.GetType(drCamera.Type), AssetUtility.GetEntityAsset(drCamera.AssetName), "Camera", Constant.AssetPriority.SceneAsset, data);
        }

        public static void ShowBattleField(this EntityComponent entityComponent, BattleFieldData data)
        {
            var dt = GameEntry.DataTable.GetDataTable<DRBattleField>();
            var dr = dt.GetDataRow(data.TypeId);
            entityComponent.ShowEntity(data.Id, typeof(BattleFieldLogic), AssetUtility.GetEntityAsset(dr.AssetName), "Battle", Constant.AssetPriority.SceneAsset, data);
        }

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
