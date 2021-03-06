﻿using libsignal.ecc;
using Neco.Client.Network;
using Neco.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class LobbyModel
    {
        private ViewModel.LobbyViewModel lobbyViewModel;
        private Dictionary<string, string> memberPublicKeyDictionary;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private String lobbyId = null;
        private bool cancelRequests = false;

        public LobbyModel(ViewModel.LobbyViewModel viewModel)
        {
            lobbyViewModel = viewModel;
            memberPublicKeyDictionary = new Dictionary<string, string>();

            App.Instance?.Connector.Receive<SessionRequest>((message) =>
            {
               lobbyViewModel.NotifyUserForActiveSession(message.MemberKey);
            });
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
            memberPublicKeyDictionary = new Dictionary<string, string>();
            var serializedKey = App.Instance.CryptoHandler.SerializePublicKey();
            memberPublicKeyDictionary.Add("client", BitConverter.ToString(serializedKey));
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
                bool result = false;
                await semaphoreSlim.WaitAsync();
                try
                {
                    RefreshMemberList(lobbyResp.MemberPublicKeys);
                    lobbyId = lobbyResp.LobbyId;
                    result = true;
                }
                finally
                {
                    semaphoreSlim.Release();
                }
                return result;
            }

            return false;
        }

        public void RefreshMemberList(List<string> newMembers)
        {
            //if(lobbyViewModel.Members != null && lobbyViewModel.Members.Count > 0) lobbyViewModel.Members.Clear();
            List<string> removeEntries = new List<string>();
            foreach(KeyValuePair<string,string> keyValuePair in memberPublicKeyDictionary)
            {
                if (newMembers.Contains(keyValuePair.Value))
                {
                    newMembers.Remove(keyValuePair.Value);
                }
                else
                {
                    removeEntries.Add(keyValuePair.Key);
                }
            }
            foreach(string key in removeEntries)
            {
                memberPublicKeyDictionary.Remove(key);
                RemoveSessionIdFromViewModel(key);
            }
            foreach(string key in newMembers)
            {
                var index = memberPublicKeyDictionary.Count;
                memberPublicKeyDictionary.Add("Session " + index, key);
                AddSessionIdToViewModel("Session " + index);
            }
        }

        private void RemoveSessionIdFromViewModel(string sessionId)
        {
            foreach(ViewModel.ChatSessionID chatSessionId in lobbyViewModel.MemberIDs)
            {
                if (chatSessionId.SessionID.Equals(sessionId)) lobbyViewModel.MemberIDs.Remove(chatSessionId); return;
            }
        }

        private void AddSessionIdToViewModel(string sessionId)
        {
            ViewModel.ChatSessionID sessionIdWrapper = new ViewModel.ChatSessionID();
            lobbyViewModel.MemberIDs.Add(sessionIdWrapper);
            sessionIdWrapper.SessionID = sessionId;
        }

        public async void StartRequestInterval()
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

        public byte[] GetMemberKey(string sessionId)
        {
            string hexkey = memberPublicKeyDictionary[sessionId].Replace("-","");
            int NumberChars = hexkey.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hexkey.Substring(i, 2), 16);
            return bytes;
        }
    }
}
