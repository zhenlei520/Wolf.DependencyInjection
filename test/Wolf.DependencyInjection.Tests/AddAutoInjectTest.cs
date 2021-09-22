// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Tests
{
    [TestClass]
    public class AddAutoInjectTest : TestBase
    {
        [TestMethod]
        public void TestAddAutoInject()
        {
            var repository = base._serviceProvider.GetServices<IRepository>();
            Assert.IsNotNull(repository);
        }
    }
}
