//------------------------------------------------------------------------------
//  <copyright file="BattleFieldMonster.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/6/6 21:43:45
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
	public class BattleFieldMonsterLogic : EntityLogic
	{
        private SpriteRenderer _spriteRenderer;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            transform.localPosition = Vector3.zero;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            var data = userData as BattleFieldMonsterData;
            DRMonster drMonster = GameEntry.DataTable.GetDataRow<DRMonster>(data.TypeId);
            GameEntry.Resource.LoadSprite(drMonster.SpriteAssetName, (sprite) => _spriteRenderer.sprite = sprite);
            GameEntry.Entity.AttachEntity(Entity, data.OwnerId, data.PointName);
        }
    }
}