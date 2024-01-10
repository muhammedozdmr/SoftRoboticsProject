using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftRobotics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRobotics.DataAccess.Entity_Configurations
{
    internal class RandomWordsConfigurations : IEntityTypeConfiguration<RandomWord>
    {
        public void Configure(EntityTypeBuilder<RandomWord> builder)
        {
            builder.ToTable(nameof(RandomWord));
            builder.HasKey(x => x.Id);
            builder.Property(p=>p.Word).IsUnicode().HasMaxLength(50);

            builder.HasData(
                new RandomWord() { Id = 1, Word="AbCdEfG",CountWord=7},
                new RandomWord() { Id = 2, Word="Test2AbCdEfG",CountWord=12},
                new RandomWord() { Id = 3, Word="TeStUcAbCdEfG",CountWord=13}
                );
        }
    }
}
