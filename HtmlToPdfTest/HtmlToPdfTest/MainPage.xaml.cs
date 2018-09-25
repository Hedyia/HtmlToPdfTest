using Xamarin.Forms;

namespace HtmlToPdfTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var htmlString = @" <!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                    h1 {
                                        color: blue;
                                        font-family: verdana;
                                        font-size: 300%;
                                    }
                                    p  {
                                        color: red;
                                        font-family: courier;
                                        font-size: 160%;
                                    }
                                    </style>
                                    </head>
                                    <body>

                                    <h1>This is a heading</h1>
                                    <p>This is a paragraph.</p>

                                    </body>
                                    </html> ";
                DependencyService.Get<IHtmlToPdf>().SafeHTMLToPDF(htmlString, "Invoice");
                DisplayAlert("Save done", "Save Done el7", "ok");

            }
            catch
            {
                DisplayAlert("Save faild", ":(", "ok");


            }

        }
    }
}
