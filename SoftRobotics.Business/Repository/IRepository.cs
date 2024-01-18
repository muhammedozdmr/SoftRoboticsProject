using Microsoft.EntityFrameworkCore;
using SoftRobotics.Domain;
using SoftRobotics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.Business.Repository
{
    public  interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        void DirectExchange();
        void GenerateWord();
        IEnumerable<T> GenerateWordRabbit();
        CommandResult Delete(T dto);
    }
}
