using System;
using System.Collections.Generic;
using Chart_Leader.Repository;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart_Business_Layers.Interface
{
    public interface ICategorieBusinness
    {
        List<Categories> GetAllCategories();
        Categories GetFirstorDefault(int Cat_id);
        void Update(Categories cat);

        
    }
}
