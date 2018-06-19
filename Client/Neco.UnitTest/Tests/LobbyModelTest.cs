using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    [TestClass]
    public class LobbyModelTest
    {
        Client.ViewModel.LobbyViewModel viewModel;
        Client.Model.LobbyModel model;
        public LobbyModelTest()
        {
            viewModel = new Client.ViewModel.LobbyViewModel(true);
            model = viewModel.Model;
        }

        [TestMethod]
        public void ShouldRefreshMemberlist()
        {
            var list = new List<string>()
            {
                "Heinz",
                "Dödel"
            };

            model.RefreshMemberList(list);

            Assert.AreEqual(viewModel.MemberIDs.Count, 2);
        }

        [TestMethod]
        public void ShouldClearMemberlist()
        {
            var list = new List<string>()
            {
                 "00-11-22-33",
                 "44-11-22-33"
            };

            model.RefreshMemberList(list);
            model.RefreshMemberList(new List<string>());

            Assert.AreEqual(viewModel.MemberIDs.Count, 0);
        }

        [TestMethod]
        public void ShouldReturnKey()
        {
            var list = new List<string>()
            {
                "00-11-22-33"
            };

            model.RefreshMemberList(list);

            var data = model.GetMemberKey("Session 0");

            CollectionAssert.AreEqual(data, new byte[] { 0x00, 0x11, 0x22, 0x33 });
        }
    }
}
