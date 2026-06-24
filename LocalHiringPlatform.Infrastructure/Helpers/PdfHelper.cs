using System.Text;
using UglyToad.PdfPig;

namespace LocalHiringPlatform.Infrastructure.Helpers
{
    public static class PdfHelper
    {
        public static string ExtractText(
            string filePath)
        {
            var text =
                new StringBuilder();

            using var document =
                PdfDocument.Open(
                    filePath);

            foreach (var page
                in document.GetPages())
            {
                text.AppendLine(
                    page.Text);
            }

            return text.ToString();
        }
    }
}