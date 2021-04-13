using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorms.BusinessLogic.DataModel;


namespace BookWorms.Models.Admins
{
    public class UpdateBookViewModel
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public Admin Admin { get; set; }
    }
}
