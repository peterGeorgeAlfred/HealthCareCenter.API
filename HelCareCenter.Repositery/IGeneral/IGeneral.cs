using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.IGeneral
{
    public interface IGeneral<T>
    {

        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> ADD(T addedItem);
        Task<T> Delete(int id);
        Task<T> Update(T UpdatedItem);



    }
}
