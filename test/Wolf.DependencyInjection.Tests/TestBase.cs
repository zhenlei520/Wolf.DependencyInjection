namespace Wolf.DependencyInjection.Tests
{
    public class TestBase
    {
        protected readonly IServiceProvider _serviceProvider;
        public TestBase()
        {
            var services = new ServiceCollection();
            services.AddAutoInject(AppDomain.CurrentDomain.GetAssemblies());
            _serviceProvider = services.BuildServiceProvider();
        }

        public interface IRepository : ISingleInstance
        {

        }

        public class Repository : IRepository
        {

        }
    }
}
