using SoftRobotics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Business.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
