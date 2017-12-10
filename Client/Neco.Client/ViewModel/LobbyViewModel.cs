using System;
using System.Collections.ObjectModel;

namespace Neco.Client.ViewModel
{
    public class LobbyViewModel : ViewModelBase
    {
        private ObservableCollection<ChatSession> memberList;

        private LobbyPage lobbyView;
        private Model.LobbyModel lobbyModel;

        public LobbyViewModel()
        {
            memberList = new ObservableCollection<ChatSession>();
            lobbyModel = new Model.LobbyModel(this);
            lobbyView = new LobbyPage(this);
        }
        public ObservableCollection<ChatSession> Members
        {
            get
            {
                return memberList;
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
