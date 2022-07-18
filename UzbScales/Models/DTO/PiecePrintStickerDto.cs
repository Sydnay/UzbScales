using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzbScales.Models.DTO
{
    public class PiecePrintStickerDto
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Amount { get; set; }
        public int SumTotal { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }
}
