using Avalonia.Media.Imaging;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvaloniaApplication2.Models
{
    public class Good
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string SAP { get; set; }
        public bool isWeighable { get; set; }
        public int PLU { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        public Bitmap NormalImage { get; set; }
    }
}
