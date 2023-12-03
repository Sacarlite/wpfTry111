using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.VievModel;

namespace wpfTry.Model.Entities
{
    public class Order:BaseVievModel
    {
        public Order(string Name, string Surname, string Middlename, string EMail, string PhoneNumber)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Middlename = Middlename;
            this.EMail = EMail;
            this.PhoneNumber = PhoneNumber;
            Time= DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Time { get; set; }
        public List<OrderTable> order { get; set; } = null!;
    }
    public class OrderTable
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        [NotMapped]
        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                }
            }
        }

    }

}
