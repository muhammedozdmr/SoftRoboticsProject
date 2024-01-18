using AutoMapper;
using SoftRobotics.DataAccess;
using SoftRobotics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Business.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SoftRoboticsContext _context;
        private IRepository<RandomWord> _randomWords;

        public UnitOfWork(SoftRoboticsContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
