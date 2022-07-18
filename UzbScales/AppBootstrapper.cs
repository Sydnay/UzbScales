using UzbScales.ViewModels;
using BL;
using Splat;
using System;
using Avalonia;

namespace UzbScales
{
    public class AppBootstrapper : IEnableLogger
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterConstant(App.ScalesLocalContext, typeof(IGoodsContext));

            services.Register( 
                () => { return new ChosenReceiptViewModel(resolver.GetRequiredService<IGoodsContext>()); },
                typeof(IChosenRecieptViewModel));

            services.Register(
                () => { return new PieceChosenReceiptViewModel(resolver.GetRequiredService<IGoodsContext>()); },
                typeof(IPieceChosenRecieptViewModel));

            services.RegisterConstant(
                new MainWindowViewModel(
                    resolver.GetRequiredService<IGoodsContext>(),
                    resolver.GetRequiredService<IChosenRecieptViewModel>(),
                    resolver.GetRequiredService<IPieceChosenRecieptViewModel>()),
                typeof(IMainWindowViewModel));
        }
    }

    public static class ReadonlyDependencyResolverExtensions
    {
        public static TService GetRequiredService<TService>(this IReadonlyDependencyResolver resolver)
        {
            var service = resolver.GetService<TService>();
            if (service is null)
                throw new InvalidOperationException($"Failed to resolve object of type {typeof(TService)}");
            return service; 
        }
    }
}