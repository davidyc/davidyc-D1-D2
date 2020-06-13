using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        [StringLength(16)]
        public string CardNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(200)]
        public string CardHolderName { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
