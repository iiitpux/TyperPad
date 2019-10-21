using TyperPad.Common.Model;

namespace TyperPad.Common.Interface
{
    public interface IDataStore
    {
        void Init();
        Settings GetSettings();
    }
}