using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduino.DataAccess.Abstract;
using Arduino.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arduino.DataAccess.Concrete
{
    public class DataRepository : IDataRepository 
    
    {
        public async Task<Data> CreateData(Data data)
        {

            using (var arduino = new ArduinoDbContext())
            {
                arduino.Datas.Add(data);
                await  arduino.SaveChangesAsync();
                return data;
            }
        }

        public async Task DeleteData(int id)
        {
            using (var arduino = new ArduinoDbContext())
            {
                var _deletedData = await GetDataById(id);
                arduino.Datas.Remove(_deletedData);
                await arduino.SaveChangesAsync();
                
            }
        }

        public async Task<Data> GetDataById(int id)
        {
            using (var arduino = new ArduinoDbContext())
            {
                return await arduino.Datas.FindAsync(id);
            }
        }

        public async Task<int> GetDataCount()
        {
             using (var arduino = new ArduinoDbContext())
            {
                return await arduino.Datas.CountAsync();
            }
        }

        public async Task<List<Data>> GetDataList()
        {
            using (var arduino = new ArduinoDbContext())
            {
                return await arduino.Datas.ToListAsync();
            }
        }

        public async Task<Data> UpdateData(Data data)
        {
            using (var arduino = new ArduinoDbContext())
            {
                  arduino.Datas.Update(data);
                  await arduino.SaveChangesAsync();
                  return data;
            }
        }
    }
}
