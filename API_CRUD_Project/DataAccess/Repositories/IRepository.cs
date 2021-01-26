using API_CRUD_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.DataAccess.Repositories
{
    public interface IRepository
    {
        ICollection<Boxer> GetBoxer();
        Boxer GetBoxer(int id);

        bool IsBoxerExsist(string fullName);
        bool IsBoxerExsist(int id);

        bool Create(Boxer entity);
        bool Update(Boxer entity);
        bool Delete(Boxer entity);
        bool Save();

    }
}
