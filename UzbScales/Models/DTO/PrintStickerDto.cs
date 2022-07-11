using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.Models.DTO
{
    public class PrintStickerDto
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public decimal Weight { get; set; }
        public int SumTotal { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }
}
