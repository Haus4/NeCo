using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    [TestClass]
    public class ChatTest
    {
        Client.Model.ChatModel model;
        Client.ViewModel.ChatViewModel viewModel;
        public ChatTest()
        {
            viewModel = new Client.ViewModel.ChatViewModel(false, true);
            model = viewModel.Model;
        }

        [TestMethod]
        public void ShouldSendMessage()
        {
            model.PushMessage("hi");
            Assert.AreEqual(1, viewModel.Messages.Count);
            Assert.IsFalse(viewModel.Messages[0].IsImage);
        }

        [TestMethod]
        public void ShouldSendImage()
        {
            model.PushImage(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
            Assert.AreEqual(1, viewModel.Messages.Count);
            Assert.IsTrue(viewModel.Messages[0].IsImage);
        }
    }
}
