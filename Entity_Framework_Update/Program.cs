using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetSection("connectionString").Value;

Console.WriteLine(connectionString);

using var conn = new SqlConnection(connectionString);

var walletToUpdate = new Wallet
{
    Holder = "Mona",
    Balance = 5000
};


var SQL = "UPDATE dbo.Wallets SET Balance = @Balance WHERE Holder = @Holder";

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = walletToUpdate.Holder
};

SqlParameter BalanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToUpdate.Balance
};


using SqlCommand command = new SqlCommand(SQL, conn);

command.Parameters.Add(holderParameter);
command.Parameters.Add(BalanceParameter);

command.CommandType = CommandType.Text;

conn.Open();

if (command.ExecuteNonQuery() > 0)
{

    Console.WriteLine($"wallet for {walletToUpdate.Holder} updated successfully");
}
else
{
    Console.WriteLine("falied to add the row");
}

conn.Close();

Console.ReadKey();

