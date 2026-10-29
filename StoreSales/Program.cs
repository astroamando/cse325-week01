
using System.Globalization;
using System.Text;
using System.Text.Json;

string storesFolder = Path.Combine(
    Directory.GetCurrentDirectory(), "StoreSales", "stores");

string reportPath = Path.Combine(
    Directory.GetCurrentDirectory(), "StoreSales", "sales-summary.txt");

GenerateSalesSummary(storesFolder, reportPath);

static void GenerateSalesSummary(string folderPath, string outputPath)
{
    StringBuilder report = new StringBuilder();
    decimal totalSales = 0;

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (string file in Directory.GetFiles(
        folderPath, "sales.json", SearchOption.AllDirectories))
    {
        string json = File.ReadAllText(file);

        using JsonDocument document = JsonDocument.Parse(json);

        decimal fileTotal = document.RootElement
            .GetProperty("Total").GetDecimal();

        totalSales += fileTotal;

        string fileName = Path.GetRelativePath(folderPath, file);

        report.AppendLine(
            $"{fileName}: {fileTotal.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");
    }

    report.Insert(
        report.ToString().IndexOf("Details:"),
        $"Total Sales: {totalSales.ToString("C", CultureInfo.GetCultureInfo("en-US"))}\n\n");

    File.WriteAllText(outputPath, report.ToString());

    Console.WriteLine(report.ToString());
    Console.WriteLine($"Report saved to: {outputPath}");
}
