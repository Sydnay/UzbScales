using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace UzbScales
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}


/*

+
Прикрутить картинки через бд (Base64)
<Pagging>
Изменение формы товара а не открытие нового окна
convert вынести в расширение мб(использвуется каждый раз при подгрузке с бд)
+-  
Бинд связанных сущностей
Разделять по категории шт/кг (переделать в бд)
Закрытие окна

-
Разбиение на категории без 123456
*/