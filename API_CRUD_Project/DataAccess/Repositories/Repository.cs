using API_CRUD_Project.DataAccess.Context;
using API_CRUD_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public Repository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;


        public bool Create(Boxer entity)
        {
            _applicationDbContext.Add(entity);
            return Save();
        }

        public bool Delete(Boxer entity)
        {
            _applicationDbContext.Remove(entity);
            return Save();
        }

        public ICollection<Boxer> GetBoxer() => _applicationDbContext.Boxers.ToList();

        public Boxer GetBoxer(int id) => _applicationDbContext.Boxers.FirstOrDefault(x => x.Id == id);

        public bool IsBoxerExsist(string fullName) => _applicationDbContext.Boxers.Any(x => x.FullName.ToLower().Trim() == fullName.ToLower().Trim());

        public bool IsBoxerExsist(int id) => _applicationDbContext.Boxers.Any(x => x.Id == id);

        public bool Save() => _applicationDbContext.SaveChanges() > 0 ? true : false;

        public bool Update(Boxer entity)
        {
            _applicationDbContext.Update(entity);
            return Save();
        }

    }
}
