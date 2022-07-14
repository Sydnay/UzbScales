using AvaloniaApplication2.Models.DTO;
using AvaloniaApplication2.Views;
using BL;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reactive;

namespace AvaloniaApplication2.ViewModels
{
    public interface IChosenRecieptViewModel
    {
        public void Setup(Good chosenGood);
    }

    public class ChosenReceiptViewModel : ViewModelBase, IChosenRecieptViewModel
    {
        public ObservableCollection<Good> SimilarGoods { get; set; }

        private readonly IGoodsContext _db;

        private Good _selectedItem;
        public Good SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        private Good _good;
        public Good Good
        {
            get => _good;
            set => Set(ref _good, value);
        }

        private decimal _weight = 0;
        public decimal Weight
        {
            get => _weight;
            set => Set(ref _weight, value);
        }

        private int _sumTotal = 0;
        public int SumTotal
        {
            get => _sumTotal;
            set => Set(ref _sumTotal, value);
        }

        #region Commands
        public ReactiveCommand<Unit, Unit> AddKiloTEST { get; }
        void AddKilo()
        {
            Weight = Weight + 0.5m;
        }

        public ReactiveCommand<Good, Unit> OpenSimilarGood { get; }

        void RunTheThing(Good parameter)
        {
            Weight = 0;
            SumTotal = 0;
            Good = parameter;
        }

        public ReactiveCommand<Unit, Unit> PrintSticker { get; }
        void PrintStickerCommand()
        {
            var dto = new PrintStickerDto
            {
                Name = Good.Name,
                Price = (int)Good.Price,
                Weight = this.Weight,
                SumTotal = (int)Weight * Good.Price,
                Barcode = Good.SAP
            };

            var window = new PrintReceipt()
            {
                DataContext = new PrintReceiptViewModel(dto)
            };
            window.Show();
        }
        #endregion

        public ChosenReceiptViewModel(IGoodsContext goodsContext)
        {
            OpenSimilarGood = ReactiveCommand.Create<Good>(RunTheThing);
            PrintSticker = ReactiveCommand.Create(PrintStickerCommand);
            AddKiloTEST = ReactiveCommand.Create(AddKilo);

            _db = goodsContext;

            foreach (var good in _db.Goods)
            {
                if (good.isWeighable)
                    good.Name += ", кг";
                else
                    good.Name += ", шт";
                good.NormalImage = ConvertByte64ToAvaloniaBitmap(good.Image);
            }
        }

        public void Setup(Good chosenGood)
        {
            Good = chosenGood;

            var collection = new ObservableCollection<Good>(_db.Goods.Local.Where(x => x.Id != chosenGood.Id).Take(4));
            SimilarGoods = collection;
        }

        private Image Byte64ToImg(byte[] img)
        {

            Image image;
            using (MemoryStream ms = new MemoryStream(img))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

        private Avalonia.Media.Imaging.Bitmap ConvertByte64ToAvaloniaBitmap(byte[] img)
        {
            var bitmap = Byte64ToImg(img);

            if (bitmap == null)
                return null;
            System.Drawing.Bitmap bitmapTmp = new System.Drawing.Bitmap(bitmap);
            var bitmapdata = bitmapTmp.LockBits(new Rectangle(0, 0, bitmapTmp.Width, bitmapTmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Avalonia.Media.Imaging.Bitmap bitmap1 = new Avalonia.Media.Imaging.Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                bitmapdata.Scan0,
                new Avalonia.PixelSize(bitmapdata.Width, bitmapdata.Height),
                new Avalonia.Vector(96, 96),
                bitmapdata.Stride);
            bitmapTmp.UnlockBits(bitmapdata);
            bitmapTmp.Dispose();
            return bitmap1;
        }
    }
}
