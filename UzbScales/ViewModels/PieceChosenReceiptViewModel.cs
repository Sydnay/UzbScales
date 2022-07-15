﻿using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reactive;
using UzbScales.Models;
using UzbScales.Models.DTO;
using UzbScales.Views;

namespace UzbScales.ViewModels
{
    internal class PieceChosenReceiptViewModel : ViewModelBase
    {
        GoodsContext db;

        public ObservableCollection<Good> SimilarGoods { get; set; }

        Good _selectedItem;

        public Good SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }
        Good _good;
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
        private int _sumTotal;
        public int SumTotal
        {
            get => _sumTotal;
            set => Set(ref _sumTotal, value);
        }
        private int _amount = 1;
        public int Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }
        #region Commands

        public ReactiveCommand<Unit, Unit> OneMorePiece { get; }
        void AddPiece()
        {
            Amount++;
            SumTotal = Amount * Good.Price;
        }
        public ReactiveCommand<Unit, Unit> OneLessPiece { get; }
        void RemovePiece()
        {
            if(Amount == 1)
                return;
            Amount--;
            SumTotal = Amount * Good.Price;
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
            var dto = new PiecePrintStickerDto
            {
                Name = Good.Name,
                Price = (int)Good.Price,
                Amount = Amount,
                SumTotal = SumTotal,
                Barcode = Good.SAP
            };

            var window = new PiecePrintReceipt()
            {
                DataContext = new PiecePrintReceiptViewModel(dto)
            };
            window.Show();
        }
        #endregion
        public PieceChosenReceiptViewModel(Good choosenGood)
        {
            OpenSimilarGood = ReactiveCommand.Create<Good>(RunTheThing);
            PrintSticker = ReactiveCommand.Create(PrintStickerCommand);
            OneMorePiece = ReactiveCommand.Create(AddPiece);
            OneLessPiece = ReactiveCommand.Create(RemovePiece);

            db = new GoodsContext();

            foreach (var good in db.Goods)
            {
                if (good.isWeighable)
                    good.Name += ", кг";
                else
                    good.Name += ", шт";
                good.NormalImage = ConvertByte64ToAvaloniaBitmap(good.Image);
            }

            Good = choosenGood;

            SumTotal = Good.Price;

            var collection = new ObservableCollection<Good>(db.Goods.Local.Where(x => x.Id != choosenGood.Id).Take(4));
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
