using AvaloniaApplication2.Models;
using AvaloniaApplication2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.ViewModels
{
    public class PrintReceiptViewModel : ViewModelBase
    {
        private PrintStickerDto _sticker;
        public PrintStickerDto Sticker
        {
            get => _sticker;
            set => Set(ref _sticker, value);
        }
        public PrintReceiptViewModel(PrintStickerDto printedGood)
        {
            Sticker = printedGood;
        }
    }
}
