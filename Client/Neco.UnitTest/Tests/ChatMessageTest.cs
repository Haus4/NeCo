using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    [TestClass]
    public class ChatMessageTest
    {
        Client.ViewModel.ChatMessage viewModel;
        public ChatMessageTest()
        {
            viewModel = new Client.ViewModel.ChatMessage();
        }

        [TestMethod]
        public void ShouldPorpagatePropertyChanges()
        {
            Dictionary<String, bool> changedDict = new Dictionary<string, bool>();

            viewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                changedDict[e.PropertyName] = true;
            };

            viewModel.Message = "hi";
            viewModel.IsForeign = false;
            viewModel.IsImage = false;
            viewModel.Time = DateTime.Now;

            Assert.AreEqual(4, changedDict.Count);
            Assert.IsTrue(changedDict["Message"]);
            Assert.IsTrue(changedDict["IsForeign"]);
            Assert.IsTrue(changedDict["IsImage"]);
            Assert.IsTrue(changedDict["Time"]);
        }
    }
}
