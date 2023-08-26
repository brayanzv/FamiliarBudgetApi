using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class TransactionByDateRequestDto
    {
        public int WalletId { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? EDate { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public string? Search { get; set; }
        public int? TransactionTypeId { get; set; }
    }
}
