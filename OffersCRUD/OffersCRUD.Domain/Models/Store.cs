using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Domain
{
    public  class Store : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        
    }
}
