using libsignal.ecc;
using Neco.Client.Network;
using Neco.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class LobbyModel
    {
        private ViewModel.LobbyViewModel lobbyViewModel;
        private List<byte[]> memberPublicKeys;
        private String lobbyId = null;
        private bool cancelRequests = false;

        public LobbyModel(ViewModel.LobbyViewModel viewModel)
        {
            lobbyViewModel = viewModel;
        }

        public void LeaveLobby()
        {
            cancelRequests = true;
            App.Instance.Connector.Receive<MessageRequest>(null);
            App.Instance.Connector.Receive<SessionCloseRequest>(null);

            LeaveLobbyRequest request = new LeaveLobbyRequest
            {
                PublicKey = App.Instance.CryptoHandler.SerializePublicKey()
            };

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
        }

        public async Task<bool> Join()
        {
            StartRequestInterval();
            return await SendLobbyRequest();
        }

        private async Task<bool> SendLobbyRequest()
        {
            LobbyRequest request = new LobbyRequest
            {
                LobbyId = lobbyId,
                PublicKey = App.Instance.CryptoHandler.SerializePublicKey(),
                Signature = App.Instance.CryptoHandler.CalculateSecuritySignature(),
                Latitude = App.Instance.Locator.Position?.Latitude ?? 0.0,
                Longitude = App.Instance.Locator.Position?.Longitude ?? 0.0
            };

            var response = await App.Instance.Connector.SendRequest(request, 30000);
            if (response != null && response is LobbyResponse lobbyResp && lobbyResp.Success)
            {
                if(memberPublicKeys != lobbyResp.MemberPublicKeys) ActualizeMembers(lobbyResp.MemberPublicKeys);
                memberPublicKeys = lobbyResp.MemberPublicKeys;
                lobbyId = lobbyResp.LobbyId;
                return true;
            }

            return false;
        }

        private void ActualizeMembers(List<byte[]> newMembers)
        {
            int i = 0;
            //if(lobbyViewModel.Members != null && lobbyViewModel.Members.Count > 0) lobbyViewModel.Members.Clear();
            foreach (byte[] key in newMembers)
            {
                if (key == App.Instance.CryptoHandler.SerializePublicKey()) continue;
                var member = new ViewModel.ChatSession()
                {
                    RemotePublicKey = key,
                    SessionNum = i
                };
                if (!lobbyViewModel.Members.Contains(member)) lobbyViewModel.Members.Add(member);
                i++;
            }
        }

        private async void StartRequestInterval()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(2000);
                    await SendLobbyRequest();
                    if (cancelRequests) break;
                }
            });
        }

        public byte[] GetMemberKey(int sessionNum)
        {
            return memberPublicKeys[sessionNum];
        }
    }
}
