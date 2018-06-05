using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryServiceWebAPI.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public System.DateTime PublishDate { get; set; }
    }
}