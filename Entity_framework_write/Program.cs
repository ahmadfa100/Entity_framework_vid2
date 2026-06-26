using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetSection("connectionString").Value;

Console.WriteLine(connectionString);

using var conn = new SqlConnection(connectionString);

var walletToInsert = new Wallet {
    Holder = "Mona",
    Balance = 2000
};

var SQL = "Insert into WALLETS (Holder, Balance) values " +
    $"(@Holder, @Balance) ";

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Holder
};

SqlParameter BalanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Balance
};


using SqlCommand command = new SqlCommand(SQL, conn);

command.Parameters.Add(holderParameter);
command.Parameters.Add(BalanceParameter);

command.CommandType = CommandType.Text;

conn.Open();

if (command.ExecuteNonQuery() > 0) {

    Console.WriteLine($"wallet for {walletToInsert.Holder} added successfully");
}
else {
    Console.WriteLine("falied to add the row");
}

    conn.Close();

Console.ReadKey();

