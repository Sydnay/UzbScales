using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using UzbScales.ViewModels;

namespace UzbScales.Views
{
    public partial class PrintReceipt : ReactiveWindow<PrintReceiptViewModel>
    {
        public PrintReceipt()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
