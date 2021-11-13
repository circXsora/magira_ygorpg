
namespace MGO.Entity
{

    /// <summary>
    /// 实体视图控件绑定器
    /// </summary>
    public interface IEntityViewBinder
    {
        TViewControl Get<TViewControl>(string name) where TViewControl : class;
    }

}