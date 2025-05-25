using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class PhototRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhototRepository(AppDbContext context) : base(context)
        {
        }
    }
}
