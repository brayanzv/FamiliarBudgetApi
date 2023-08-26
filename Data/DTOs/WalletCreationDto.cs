using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class WalletCreationDto
    {
        [Required]
        public string WalletName { get; set; }
        [Required]
        public bool Visibility { get; set; }
    }
}
