// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;

namespace Wolf.DependencyInjection.Tests
{
    [TestClass]
    public class AddAutoInjectTest : TestBase
    {
        [TestMethod]
        public void TestAddAutoInject()
        {
            Assert.IsNotNull(base._serviceProvider.GetService<IRepository>());
            Assert.IsTrue(base._serviceProvider.GetServices<IRepository>().Count() == 1);
        }
    }
}
