//using Microsoft.Extensions.Hosting.Internal;

//namespace uBeacCMS;
//public static class ServiceProviderFactory
//{
//    public static IServiceProvider ServiceProvider { get; }

//    static ServiceProviderFactory()
//    {
//        HostingEnvironment env = new HostingEnvironment();
//        env.ContentRootPath = Directory.GetCurrentDirectory();
//        env.EnvironmentName = "Development";

//        Startup startup = new Startup(env);
//        ServiceCollection sc = new ServiceCollection();
//        startup.ConfigureServices(sc);
//        ServiceProvider = sc.BuildServiceProvider();
//    }
//}