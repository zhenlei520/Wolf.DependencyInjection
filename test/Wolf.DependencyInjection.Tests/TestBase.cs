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

        public interface IRepository : ISingleInstance, IOnceService
        {
        }

        public class Repository : IRepository
        {
        }

        public interface IUserRepository : IRepository
        {
        }

        public class UserRepository : Repository, IUserRepository
        {
        }

        public class Options : ISingleInstance
        {
        }

        public class Options2 : Options
        {
        }

        public class Options3 : ISingleInstance, IOnceService
        {
        }

        public class Options4 : Options3
        {
        }
    }
}
