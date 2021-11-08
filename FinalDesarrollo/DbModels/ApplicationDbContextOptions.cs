using System;
using Microsoft.EntityFrameworkCore;

namespace FinalDesarrollo.DbModels
{
    public class ApplicationDbContextOptions : IApplicationDbContextOptions
    {
        public readonly DbContextOptions<ctrlCatedraticosContext> Options;

        public ApplicationDbContextOptions(DbContextOptions<ctrlCatedraticosContext> options)
        {
            Options = options;
        }
    }
}
