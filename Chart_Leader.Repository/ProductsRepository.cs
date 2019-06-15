using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart_Leader.Repository
{

    public class ProductsRepository : GenericRepository<Products>
    {
        public ProductsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
