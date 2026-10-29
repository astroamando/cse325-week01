
# CSE 325 – Week 01 Assignment

## 1. .NET Environment

I installed .NET SDK 8.0.427 and used .NET 8 for my Week 01 projects.

I created and successfully ran a HelloCSharp console application.

## 2. Pizza API

I created an ASP.NET Core Web API using controllers and implemented the following operations:

- GET: Retrieve pizzas
- POST: Create a pizza
- PUT: Update a pizza
- DELETE: Remove a pizza

### Initial Pizza Data

I included three pizzas, including an additional pizza named Brazilian Chicken.

GET /pizzas

HTTP Status: 200 OK

Response:

```json
[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Brazilian Chicken",
    "isGlutenFree": false
  }
]
```

### POST – Create Pizza

Request: POST /pizzas

```json
{
  "name": "Pepperoni",
  "isGlutenFree": false
}
```

HTTP Status: 201 Created

Response:

```json
{
  "id": 4,
  "name": "Pepperoni",
  "isGlutenFree": false
}
```

### PUT – Update Pizza

Request: PUT /pizzas/4

```json
{
  "id": 4,
  "name": "Pepperoni Deluxe",
  "isGlutenFree": false
}
```

HTTP Status: 204 No Content

Response: Empty response body.

### DELETE – Remove Pizza

Request: DELETE /pizzas/4

HTTP Status: 204 No Content

Response: Empty response body.

## 3. Sales Summary Report

I created a .NET 8 console application that reads sales files, calculates individual file totals, calculates the overall sales total, and generates a sales-summary.txt file.

### Verified Output

Sales Summary

Total Sales: $16,753.25

Details:
- february.txt: $5,950.75
- january.txt: $4,251.50
- march.txt: $6,551.00

### Working Sales Summary Function

```csharp
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
```

## 4. Testing

I successfully tested the Pizza API using PowerShell HTTP requests and verified the returned HTTP status codes.

I also ran the SalesReport application after cleaning and rebuilding it. The program generated the expected sales totals and saved the report successfully.
