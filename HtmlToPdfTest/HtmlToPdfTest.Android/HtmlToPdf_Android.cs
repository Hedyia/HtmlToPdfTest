using Android.OS;
using HtmlToPdfTest.Droid;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Java.Lang;
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
            string pdfPath = System.IO.Path.Combine(path, "samplee.pdf");
            System.IO.FileStream fs = new FileStream(pdfPath, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            HTMLWorker worker = new HTMLWorker(document);
            document.Open();
            StringBuilder htmlToPdf = new StringBuilder(html);
            TextReader reader = new System.IO.StringReader(html.ToString());
            worker.StartDocument();
            worker.Parse(reader);
            worker.EndDocument();
            worker.Close();
            document.Close();
            writer.Close();
            fs.Close();
            return "";

        }




    }
}