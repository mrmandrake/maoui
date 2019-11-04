using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ButtonXaml
{

    public class ButtonXamlPage : ContentPage
    {
        int count = 0;

        public Entry entry;

        public ButtonXamlPage()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            entry = new Entry() { Text = "Click Count: 0" };
            var btn = new Button() { Text = "Pressme" };
            btn.Clicked += OnButtonClicked;
            Content = new StackLayout()
            {
                Padding = 20,
                Children =
                {
                    new Label() { Text = "Welcome!", FontSize = 32, FontAttributes = FontAttributes.Bold },
                    entry,
                    btn
                }
            };
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            // var label = page.FindByName<Entry>("LabelCount");
            // label.Text = $"Click Count: {++count}";
            // entry.Text = $"Click Count: {++count}";
            try
            {
                Console.WriteLine("sending to ws://127.0.0.1:65080/wstest");
                var ws = new WSHelper();
                var recv = await ws.WebSocketSendText(new Uri("ws://127.0.0.1:65080/wstest"), null, "invio di prova");
                entry.Text = recv;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
