using BLL.IRepositories;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
       
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

       
        public void Dispose() => _context.Dispose();



    }

}
