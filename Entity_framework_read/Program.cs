using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetSection("connectionString").Value;

Console.WriteLine(connectionString);

using var conn = new SqlConnection(connectionString);

var SQL = "SELECT * FROM dbo.Wallets";

using SqlCommand command = new SqlCommand(SQL, conn);
command.CommandType = CommandType.Text;

conn.Open();

using SqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    Wallet wallet = new Wallet
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Holder = reader.GetString(reader.GetOrdinal("Holder")),
        Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
    };

    Console.WriteLine(wallet);
}