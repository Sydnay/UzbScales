using UzbScales.Models.DTO;

namespace UzbScales.ViewModels
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
