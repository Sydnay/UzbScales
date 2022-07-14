using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaApplication2.ViewModels;
using ReactiveUI;
using System;

namespace AvaloniaApplication2.Views
{
    public partial class ChoosenReceipt : ReactiveWindow<ChosenReceiptViewModel>
    {
        public ChoosenReceipt()
        {
            InitializeComponent();

            var weight = this.Find<TextBlock>(nameof(textWeightTotal)) as TextBlock;

            var totalSum = weight.GetObservable(TextBlock.TextProperty);


            totalSum.Subscribe(value => UpdateTotalSum(value));
        }

        private void UpdateTotalSum(string value)
        {
            var textWeight = this.Find<TextBlock>(nameof(textSumTotal));

            var textPrice = this.Find<TextBlock>(nameof(ChoosenReceipt.textPrice)).Text;
            decimal.TryParse(textPrice, out decimal price);

            var textTotalSum = this.Find<TextBlock>(nameof(textWeightTotal));

            decimal.TryParse(textTotalSum.Text as string ?? "0", out decimal weight);

            decimal totalSum = weight * price;

            int totalSumInteger = (int)Decimal.Round(totalSum);

            textWeight.Text = totalSumInteger.ToString();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
