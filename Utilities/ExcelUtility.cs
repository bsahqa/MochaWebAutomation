using OfficeOpenXml;
using System.IO;

public class ExcelUtility
{

    public static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData\LoginCredentials.xlsx");
    public static string sheetName = "Sheet1";
    // Method to write credentials to Excel
    public static void WriteLoginCredentials(string username, string password)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName] ?? package.Workbook.Worksheets.Add(sheetName);

            // Write the username and password into the first row
            worksheet.Cells[1, 1].Value = "Username";
            worksheet.Cells[1, 2].Value = "Password";
            worksheet.Cells[2, 1].Value = username;
            worksheet.Cells[2, 2].Value = password;

            package.Save();
        }
    }
    
    // Method to read credentials from Excel
    public static (string username, string password) GetLoginCredentials()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        FileInfo fileInfo = new FileInfo(filePath);
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
            string username = worksheet.Cells[2, 1].Text; // Username in A2
            string password = worksheet.Cells[2, 2].Text; // Password in B2
            return (username, password);
        }
    }
}
