using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduino.Business.Abstract;
using Arduino.DataAccess.Abstract;
using Arduino.Entities;

namespace Arduino.Business.Concrete
{
    public class ArduinoManagenment : IArduinoService
    {
        private  IDataRepository _dataRepository;

        public ArduinoManagenment(IDataRepository dataRepository)
        {
            _dataRepository= dataRepository;
        }
        public async Task<Data> CreateData(Data data)
        {
            return await _dataRepository.CreateData(data);
        }

        public async Task DeleteData(int id)
        {
            await _dataRepository.DeleteData(id);
        }

        public async Task<Data> GetDataById(int id)
        {
            return  await _dataRepository.GetDataById(id);
        }

        public async Task<int> GetDataCount()
        {
            return await _dataRepository.GetDataCount();
        }

        public async Task<List<Data>> GetDataList()
        {
            return await _dataRepository.GetDataList();
        }

        public async Task<Data> UpdateData(Data data)
        {
            return await _dataRepository.UpdateData(data);
        }
    }
}
