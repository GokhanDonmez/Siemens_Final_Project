using Arduino.Entities;

namespace Arduino.DataAccess.Abstract;

public interface IDataRepository
{
    Task<List<Data>> GetDataList();
    Task<int> GetDataCount();

    Task<Data> GetDataById(int  id);

    Task<Data> CreateData(Data Data);

    Task<Data> UpdateData(Data Data);

    Task DeleteData(int id);
}