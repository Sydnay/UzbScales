using Avalonia.Media.Imaging;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace AvaloniaApplication2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        GoodsContext db;

        public IEnumerable<string> Categories { get; set; } = new List<string>() { "ќвощи", "‘рукты", "ћ€со"};
        public ObservableCollection<Good> GoodList { get; set; }

        Good _selectedItem;

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

        void RunTheThing(Good parameter)
        {
            var window = new ChoosenReceipt()
            {
            DataContext = new ChoosenReceiptViewModel(parameter)
            };
            window.Show();
        }

        #endregion

        #region PageNavigation
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        // The command that navigates a user to first view model.
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public Interaction<ChoosenReceiptViewModel, MainWindowViewModel?> ShowDialog { get; }

        #endregion
        public MainWindowViewModel()
        {
            NewWindow = ReactiveCommand.Create<Good>(RunTheThing);

            db = new GoodsContext();

            db.Goods.Load();


            foreach (var good in db.Goods)
            {
                if (good.isWeighable)
                    good.Name += ", кг";
                else
                    good.Name += ", шт";
                good.NormalImage = ConvertByte64ToAvaloniaBitmap(good.Image);
            }
            
            GoodList = db.Goods.Local;
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
