using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiCinexpassDigital.Models
{
    public class VoucherGeneradoItem
    {
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string PrefijoVoucher { get; set; }
        public string NombreVoucher { get; set; }
        public string Stock_strBarcode { get; set; }
        public string dExpiryDate { get; set; }
    }
}
