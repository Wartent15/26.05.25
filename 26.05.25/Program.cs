using System;
using System.Data.SqlClient;
using System.Data.SQLite;

class Program
{
    static string connectionString = "Data Source=practica; Version=3";
    public static SQLiteConnection connection = new SQLiteConnection(connectionString);
    static void Main()
    {


        try
        {
            connection.Open();
            Console.WriteLine("Подключение к базе данных прошло успешно.");
            while (true)
            {
                Console.WriteLine("1. Создать таблицу Suppliers\n" +
                    "2. Отображение всей информации о товаре\n" +
                    "3. Показать товары, заданного поставщика\n" +
                    "4. Показать товары, заданной категории\n" +
                    "5. Показать товар с максимальным количеством\n" +
                    "6. Показать товар с минимальным количеством\n" +
                    "7. Показать товар с минимальной себестоимостью\n" +
                    "8. Показать товар с максимальной себестоимостью\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateTable();
                        Console.WriteLine();
                        break;

                    case "2":
                        ShowAllProducts(connection);
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.WriteLine("Введите имя поставщика");
                        string supplier = Console.ReadLine();

                        ShowProductsBySupplier(connection, supplier);
                        break;

                    case "4":
                        Console.WriteLine("Введите тип товара");
                        string type = Console.ReadLine();

                        ShowProductsBySType(connection, type);
                        break;

                    case "5":
                        MaxCount(connection);
                        Console.WriteLine();
                        break;

                    case "6":
                        MinCount(connection);
                        Console.WriteLine();
                        break;

                    case "7":
                        MaxPrice(connection);
                        Console.WriteLine();
                        break;

                    case "8":
                        MinPrice(connection);
                        Console.WriteLine();
                        break;

                    case "9":

                        break;
                }
            }
        }


        catch (SqlException ex)
        {
            Console.WriteLine("Ошибка подключения: " + ex.Message);
        }
    }


    static void CreateTable()
    {
        string query = "CREATE TABLE Warehouse( " +
            "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
            "Name NVARCHAR" +
            "Type NVARCHAR" +
            "Supplier NVARCHAR" +
            "Count INTEGER" +
            "Price INTEGER" +
            "Date DATE" +
            ");";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        command.ExecuteNonQuery();

    }
    static void ShowAllProducts(SQLiteConnection connection)
    {
        string query = "SELECT * FROM Warehouse";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"{reader["Id"]}, {reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Count"]}, {reader["Price"]}, {reader["Date"]}");
        }
    }


    static void ShowProductsBySupplier(SQLiteConnection connection, string supplier)
    {
        string query = "SELECT * FROM Warehouse WHERE Supplier = @supplier";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@supplier", supplier);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["Name"]}, {reader["Count"]}");
        }
    }

    static void ShowProductsBySType(SQLiteConnection connection, string type)
    {
        string query = "SELECT * FROM Warehouse WHERE Type = @type";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@supplier", type);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["Name"]}, {reader["Count"]}");
        }
    }
    static void MaxCount(SQLiteConnection connection)
    {
        string query = "SELECT TOP 1 Name, Count " +
                              "FROM Warehouse " +
                              "ORDER BY Count ASC";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void MinCount(SQLiteConnection connection)
    {
        string query = "SELECT TOP 1 Name, Count " +
                              "FROM Warehouse " +
                              "ORDER BY Count DESC";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void MaxPrice(SQLiteConnection connection)
    {
        string query = "SELECT TOP 1 Name, Price " +
                              "FROM Warehouse " +
                              "ORDER BY Price ASC";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void MinPrice(SQLiteConnection connection)
    {
        string query = "SELECT TOP 1 Name, Price " +
                              "FROM Warehouse " +
                              "ORDER BY Price DESC";
        SQLiteCommand command = new SQLiteCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }


}
