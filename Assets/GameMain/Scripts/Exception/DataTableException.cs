/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/14 21:23:1
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public abstract class DataTableException : GameFrameworkException
    {
        protected string TableName { get; set; }
    }
}