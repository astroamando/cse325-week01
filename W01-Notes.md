

# CSE 325 – Week 01 Assignment

## 1. .NET Environment

I installed .NET SDK 8.0.427 and used .NET 8 for my Week 01 projects.

I created and successfully ran a HelloCSharp console application.

## 2. Pizza API

I created an ASP.NET Core Web API using controllers and implemented GET, POST, PUT, and DELETE operations.

### Initial Pizza Data

I added Brazilian Chicken as an additional pizza in PizzaService.

Request: GET http://localhost:5000/pizzas
HTTP Status: 200 OK

Response:
```json
[
  {"id": 1, "name": "Classic Italian", "isGlutenFree": false},
  {"id": 2, "name": "Veggie", "isGlutenFree": true},
  {"id": 3, "name": "Brazilian Chicken", "isGlutenFree": false}
]
```

### POST – Create Pizza

Request: POST http://localhost:5000/pizzas

Request body:
```json
{"name": "Pepperoni", "isGlutenFree": false}
```

HTTP Status: 201 Created

Response:
```json
{"id": 4, "name": "Pepperoni", "isGlutenFree": false}
```

### PUT – Update Pizza

Request: PUT http://localhost:5000/pizzas/4

Request body:
```json
{"id": 4, "name": "Pepperoni Deluxe", "isGlutenFree": false}
```

HTTP Status: 204 No Content
Response: Empty response body.

### DELETE – Remove Pizza

Request: DELETE http://localhost:5000/pizzas/4

HTTP Status: 204 No Content
Response: Empty response body.

## 3. Sales Summary Report

I created a StoreSales .NET 8 application using the original Microsoft Learn sales JSON files.

The application reads the Total property from each sales.json file and generates sales-summary.txt.

### Verified Output

```text
Sales Summary
----------------------------

Total Sales: $2,012.20

Details:
sales.json: $88.88
201\sales.json: $501.22
202\sales.json: $1,234.22
203\sales.json: $99.00
204\sales.json: $88.88
```

### Working Sales Summary Function

The following function is implemented in StoreSales/Program.cs.

```csharp
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
```

## 4. Testing

I successfully tested the Pizza API using PowerShell HTTP requests and verified the returned status codes.

GET: 200 OK
POST: 201 Created
PUT: 204 No Content
DELETE: 204 No Content

I successfully ran StoreSales using the original Microsoft Learn JSON files. It generated sales-summary.txt with a verified total of $2,012.20 and individual totals for all five files.

