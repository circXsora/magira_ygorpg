using UnityEngine;

namespace mgo
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class MGOGameEntry : MonoBehaviour
    {
        private void Start()
        {
            // 初始化内置组件
            InitBuiltinComponents();

            // 初始化自定义组件
            InitCustomComponents();

            // 初始化自定义调试器
            InitCustomDebuggers();
        }
    }
}