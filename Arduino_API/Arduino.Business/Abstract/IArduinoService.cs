using Arduino.DataAccess.Abstract;
using Arduino.Entities;

namespace Arduino.Business.Abstract;

public interface IArduinoService
{
    Task<List<Data>> GetDataList();
    Task<int> GetDataCount();

    Task<Data> GetDataById(int id);

    Task<Data> CreateData(Data veri);

    Task<Data> UpdateData(Data veri);

    Task DeleteData(int id);
}