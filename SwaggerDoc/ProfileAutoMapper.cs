using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Transaction, Trx>()
                .ReverseMap();

            CreateMap<TransactionDerive, Trx>()
                .ReverseMap();
        }
    }
}
