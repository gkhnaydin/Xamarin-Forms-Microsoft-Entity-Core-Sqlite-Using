using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KisiRehberi.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddKisiAsync(T kisi);
        Task<bool> UpdateKisiAsync(T kisi);
        Task<bool> DeleteKisiAsync(string id);
        Task<T> GetKisiAsync(string id);
        Task<IEnumerable<T>> GetKisisAsync(bool forceRefresh = false);
    }
}
