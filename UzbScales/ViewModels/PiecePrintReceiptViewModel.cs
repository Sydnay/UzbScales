using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UzbScales.Models.DTO;

namespace UzbScales.ViewModels
{
    internal class PiecePrintReceiptViewModel:ViewModelBase
    {
        private PiecePrintStickerDto _sticker;
        public PiecePrintStickerDto Sticker
        {
            get => _sticker;
            set => Set(ref _sticker, value);
        }
        public PiecePrintReceiptViewModel(PiecePrintStickerDto printedGood)
        {
            Sticker = printedGood;
        }
    }
}
