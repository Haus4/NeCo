using Xamarin.Forms;

namespace Neco.Client
{
    class ChatMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OwnMessageTemplate { get; set; }
        public DataTemplate ForeignMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return (item as ChatMessage).IsForeign ? ForeignMessageTemplate : OwnMessageTemplate;
        }
    }
}
