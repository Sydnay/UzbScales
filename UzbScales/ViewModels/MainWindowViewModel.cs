using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reactive;
using BL;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using UzbScales.Views;

namespace UzbScales.ViewModels
{
    public interface IMainWindowViewModel {}

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "ќвощи", "‘рукты", "ћ€со" };
        public ObservableCollection<Good> GoodList { get; set; }

        private readonly IGoodsContext _db;
        private readonly IChosenRecieptViewModel _receiptViewModel;
        private readonly IPieceChosenRecieptViewModel _pieceReceiptViewModel;

        private Good _selectedItem;
        public Good SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        private decimal _weight = 0;
        public decimal Weight
        {
            get => _weight;
            set => Set(ref _weight, value);
        }

        private int _price = 0;
        public int Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        private int _sumTotal = 0;
        public int SumTotal
        {
            get => _sumTotal;
            set => Set(ref _sumTotal, value);
        }
        #region Commands

        public ReactiveCommand<Good, Unit> NewWindow { get; }

        private void RunTheThing(Good parameter)
        {
            if(parameter.isWeighable)
            {
                var window = new ChoosenReceipt();
                _receiptViewModel.Setup(parameter);
                window.DataContext = _receiptViewModel;
                window.Show();
            }
            else
            {
                var window = new PieceChosenReceipt();
                _pieceReceiptViewModel.Setup(parameter);
                window.DataContext= _pieceReceiptViewModel;
                window.Show();
            }
        }

        #endregion

        public MainWindowViewModel(IGoodsContext goodsContext, IChosenRecieptViewModel chosenRecieptViewModel, IPieceChosenRecieptViewModel pieceChosenRecieptViewModel)
        {
            NewWindow = ReactiveCommand.Create<Good>(RunTheThing);

            _receiptViewModel = chosenRecieptViewModel;
            _pieceReceiptViewModel = pieceChosenRecieptViewModel;
            _db = goodsContext;

            foreach (var good in _db.Goods)
            {
                if (good.isWeighable)
                    good.Name += ", кг";
                else
                    good.Name += ", шт";
                good.NormalImage = ConvertByte64ToAvaloniaBitmap(good.Image);
            }

            GoodList = new ObservableCollection<Good>(_db.Goods);
        }

        private byte[] ImgToByte64(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return imageBytes;
                }
            }
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
