using Xamarin.Forms;

namespace Neco.Client
{
    class ChatMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OwnMessageTemplate { get; set; }
        public DataTemplate OwnImageTemplate { get; set; }
        public DataTemplate ForeignMessageTemplate { get; set; }
        public DataTemplate ForeignImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = (item as ViewModel.ChatMessage);

            if(message.IsForeign)
            {
                return message.IsImage ? ForeignImageTemplate : ForeignMessageTemplate;
            }
            else
            {
                return message.IsImage ? OwnImageTemplate : OwnMessageTemplate;
            }
        }
    }
}
