
using System;
using System.ComponentModel.DataAnnotations;
namespace Moot.Models.LibraryViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OfferDate { get; set; }
        public int PropertyCount { get; set; }

    }
}
