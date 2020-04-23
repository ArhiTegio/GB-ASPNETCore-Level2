using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels
{
    public class SectionCompliteViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }
        public int? CurrentPerrentSectionId { get; set; }

        public int? CurrentSectionId { get; set; }


    }
}
