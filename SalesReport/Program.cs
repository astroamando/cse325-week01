
using System.Globalization;
using System.Text;

string salesFolder = Path.Combine(
    AppContext.BaseDirectory, "sales");

string reportPath = Path.Combine(
    AppContext.BaseDirectory, "sales-summary.txt");

GenerateSalesSummary(salesFolder, reportPath);

static void GenerateSalesSummary(
    string folderPath, string outputPath)
{
    StringBuilder report = new StringBuilder();
    decimal totalSales = 0;
    List<(string FileName, decimal Total)> details = new();

    foreach (string file in Directory.GetFiles(folderPath, "*.txt"))
    {
        decimal fileTotal = 0;

        foreach (string line in File.ReadAllLines(file))
        {
            if (decimal.TryParse(
                line,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal amount))
            {
                fileTotal += amount;
            }
        }

        totalSales += fileTotal;
        details.Add((Path.GetFileName(file), fileTotal));
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var item in details.OrderBy(d => d.FileName))
    {
        report.AppendLine($"{item.FileName}: {item.Total:C}");
    }

    File.WriteAllText(outputPath, report.ToString());

    Console.WriteLine(report.ToString());
    Console.WriteLine($"Report saved to: {outputPath}");
}
