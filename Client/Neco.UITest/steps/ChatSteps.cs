using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace Neco.UITest.steps
{
    [Binding]
    public class ChatSteps : StepsBase
    {
        [Given(@"I am on the chat page"), Scope(Tag = "uc1chat")]
        public void GivenIAmOnTheChatPage()
        {
            app.Tap(c => c.Marked("chatButton"));
        }

        [Given(@"I have entered a chat message"), Scope(Tag = "uc1chat")]
        public void EnterChatMessage()
        {
           app.EnterText(c => c.Marked("textArea"), "a chat message");
        }

        [When(@"I press the send button"), Scope(Tag = "uc1chat")]
        public void PressSendButton()
        {
            app.Tap(c => c.Marked("sendButton"));
        }

        [Then(@"the result should be a message on the other users screen"), Scope(Tag = "uc1chat")]
        public void ThenTheResultShouldBeAMessageOnTheOtherUsersScreen()
        {
            app.WaitForElement(q => q.Marked("message").Text("a chat message"));
        }
    }
}
