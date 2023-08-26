using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class WalletUpdateDto
    {
        public string WalletName { get; set; }
        public bool Visibility { get; set; }
        public int WalletId { get; set; }
    }
}
