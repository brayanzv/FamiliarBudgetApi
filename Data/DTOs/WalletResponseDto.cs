using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class WalletResponseDto
    {
        public int WalletId { get; set; }
        public string WalletName { get; set; }
        public decimal Balance { get; set; }
        public bool Visibility { get; set; }
        public int UserId { get; set; }
    }
}
