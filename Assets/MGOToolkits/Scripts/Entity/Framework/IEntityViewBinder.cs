
namespace MGO.Entity
{

    /// <summary>
    /// ʵ����ͼ�ؼ�����
    /// </summary>
    public interface IEntityViewBinder
    {
        TViewControl Get<TViewControl>(string name) where TViewControl : class;
    }

}