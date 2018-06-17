using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Neco.Client.ViewModel
{
    public class LobbyViewModel : ViewModelBase
    {
        private ObservableCollection<ChatSessionID> memberIdList;

        private LobbyPage lobbyView;
        private Model.LobbyModel lobbyModel;

        public LobbyViewModel()
        {
            memberIdList = new ObservableCollection<ChatSessionID>();
            lobbyModel = new Model.LobbyModel(this);
            lobbyView = new LobbyPage(this);
        }

        public void NotifyUserForActiveSession(byte[] memberKey)
        {
            lobbyView.DisplaySessionAlert(memberKey);
        }
        public ObservableCollection<ChatSessionID> MemberIDs
        {
            get
            {
                return memberIdList;
            }
        }

        public LobbyPage View
        {
            get
            {
                return lobbyView;
            }
        }

        public Model.LobbyModel Model
        {
            get
            {
                return lobbyModel;
            }
        }
    }
}
