//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-06-01 22:06:41.386
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    /// <summary>
    /// 怪物等级表。
    /// </summary>
    public class DRMonsterLevel : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取实体编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取等级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大生命值。
        /// </summary>
        public int MaxHealth
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最大魔法值。
        /// </summary>
        public int MaxMagic
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取力。
        /// </summary>
        public int Power
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取速。
        /// </summary>
        public int Speed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取耐。
        /// </summary>
        public int Stamina
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取运。
        /// </summary>
        public int Lucky
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取魔。
        /// </summary>
        public int Witch
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Level = int.Parse(columnStrings[index++]);
            MaxHealth = int.Parse(columnStrings[index++]);
            MaxMagic = int.Parse(columnStrings[index++]);
            Power = int.Parse(columnStrings[index++]);
            Speed = int.Parse(columnStrings[index++]);
            Stamina = int.Parse(columnStrings[index++]);
            Lucky = int.Parse(columnStrings[index++]);
            Witch = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Level = binaryReader.Read7BitEncodedInt32();
                    MaxHealth = binaryReader.Read7BitEncodedInt32();
                    MaxMagic = binaryReader.Read7BitEncodedInt32();
                    Power = binaryReader.Read7BitEncodedInt32();
                    Speed = binaryReader.Read7BitEncodedInt32();
                    Stamina = binaryReader.Read7BitEncodedInt32();
                    Lucky = binaryReader.Read7BitEncodedInt32();
                    Witch = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
