using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetSection("connectionString").Value;

Console.WriteLine(connectionString);

using var conn = new SqlConnection(connectionString);

var SQL = "SELECT * from WALLETS ";

conn.Open();

SqlDataAdapter adapter = new SqlDataAdapter(SQL,conn);
DataTable dt = new DataTable();

adapter.Fill(dt);
conn.Close();

foreach(DataRow dr in dt.Rows)
{
    Console.WriteLine($"{dr["Id"]} - {dr["Holder"]} - {dr["Balance"]}");
}
Console.ReadKey();

