using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class BrandCompliteViewModel
    {
        public IEnumerable<BrandViewModel> Brand { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
