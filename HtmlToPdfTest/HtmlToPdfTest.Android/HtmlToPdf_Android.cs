

using Android.Graphics.Pdf;
using Android.OS;
using Android.Webkit;
using HtmlToPdfTest.Droid;
using Java.IO;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(HtmlToPdf_Android))]
namespace HtmlToPdfTest.Droid
{
    public class HtmlToPdf_Android : IHtmlToPdf
    {
        private Android.Webkit.WebView webpage;

        public string SafeHTMLToPDF(string html, string filename)
        {
            string path = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
            var dir = new Java.IO.File(path);
            var file = new Java.IO.File(dir + "/" + filename + ".pdf");

            if (!dir.Exists())
                dir.Mkdirs();


            int x = 0;
            while (file.Exists())
            {
                x++;
                file = new Java.IO.File(dir + "/" + filename + "( " + x + " )" + ".pdf");
            }

            if (webpage == null)
                webpage = new Android.Webkit.WebView(Android.App.Application.Context);

            int width = 2102;
            int height = 2973;

            webpage.Layout(0, 0, width, height);
            webpage.LoadDataWithBaseURL("", html, "text/html", "UTF-8", null);
            webpage.SetWebViewClient(new WebViewCallBack(file.ToString()));

            return file.ToString();
        }


        class WebViewCallBack : WebViewClient
        {

            string fileNameWithPath = null;

            public WebViewCallBack(string path)
            {
                this.fileNameWithPath = path;
            }


            public override void OnPageFinished(Android.Webkit.WebView myWebview, string url)
            {
                PdfDocument document = new PdfDocument();
                PdfDocument.Page page = document.StartPage(new PdfDocument.PageInfo.Builder(2120, 3000, 1).Create());

                myWebview.Draw(page.Canvas);
                document.FinishPage(page);
                Stream filestream = new MemoryStream();
                FileOutputStream fos = new Java.IO.FileOutputStream(fileNameWithPath, false); ;
                try
                {
                    document.WriteTo(filestream);
                    fos.Write(((MemoryStream)filestream).ToArray(), 0, (int)filestream.Length);
                    fos.Close();
                }
                catch
                {
                }
            }
        }

    }
}