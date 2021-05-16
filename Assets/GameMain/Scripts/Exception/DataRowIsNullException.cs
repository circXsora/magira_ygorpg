/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/14 21:24:58
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class DataRowIsNullException : DataTableException 
	{
		private int TypeId { get; set; }

		public DataRowIsNullException(string tableName, int typeId)
        {
			TableName = tableName;
			TypeId = typeId;
        }

        public override string ToString()
        {
            return $"名字为{TableName}的数据表中的类型Id为{TypeId}的这行数据不存在，请检查是否填写以及是否生成过数据表。";
        }
    }
}