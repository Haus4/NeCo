using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using TechTalk.SpecFlow;

namespace Neco.UITest.features
{
    [TestFixture(Platform.Android)]
    public class FeatureBase
    {
        protected static IApp app;
        protected Platform platform;

        public FeatureBase(Platform platform)
        {
           
            this.platform = platform;
            
        }

        [SetUp]
        public void BeforeEachTest()
        {
            Console.WriteLine("HIjo");

            app = AppInitializer.StartApp(platform);
            FeatureContext.Current.Add("App", app);
        }

    }
}
