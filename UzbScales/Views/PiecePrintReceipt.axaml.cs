using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UzbScales.Views
{
    public partial class PiecePrintReceipt : Window
    {
        public PiecePrintReceipt()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
