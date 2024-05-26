using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Drink_Wholesale.Admin.Model
{
    public class DrinkWholesaleModel
    {
        private readonly DrinkWholesaleAPIService _service;
        private readonly IMapper _mapper;

        public DrinkWholesaleModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
