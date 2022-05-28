using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";

app.MapPost("/addUser/{name}/{password}", (string name, string password) =>
{
    SqlConnection con = new SqlConnection(connectionString);
    con.Open();

    string insertSQL = "INSERT INTO USERS VALUES ('" + name
        + "', '" + password + "')";
    SqlCommand cmd = new SqlCommand(insertSQL, con);
    SqlDataReader dr = cmd.ExecuteReader();

    dr.Close();
    con.Close();

    return Results.Created("/addUser", name);
});

app.MapGet("/accountSum/{userId}", (int userId) =>
{
    SqlConnection con = new SqlConnection(connectionString);
    con.Open();

    string sqlQuery = "SELECT SUM(ua.Price) as Amount FROM USERACCOUNT ua WHERE ua.UserId = " + userId;
    SqlCommand cmd = new SqlCommand(sqlQuery, con);
    SqlDataReader dr = cmd.ExecuteReader();
    dr.Read();
    object result = dr.GetValue(0);
    dr.Close();
    con.Close();

    Console.WriteLine("SUM : " + result);
    return result;

});
app.MapPost("/addBookToUserAccount", (BookData book) =>
{
    SqlConnection con = new SqlConnection(connectionString);
    con.Open();

    string insertSQL = "INSERT INTO USERACCOUNT VALUES (" + book.userId + ", '" + book.title + "', '" + book.isbn + "', '"
         + book.publisher + "', '" + book.author + "', " + book.price +  ")";
    SqlCommand cmd = new SqlCommand(insertSQL, con);
    SqlDataReader dr = cmd.ExecuteReader();

    dr.Close();
    con.Close();

    return Results.Created("/addUser", book.title);

});

app.Run();

public class BookData
{
    public int userId { get; set; }
    public string title { get; set; }
    public string isbn { get; set; }
    public string publisher { get; set; }
    public string author { get; set; }
    public float price { get; set; }
}

