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

app.MapPost("/addUser/{name}/{password}", (string name, string password) =>
{
    string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";
    SqlConnection con = new SqlConnection(connectionString);
    con.Open();

    string selectSQL = "INSERT INTO USERS VALUES ('" + name
        + "', '" + password + "')";
    SqlCommand cmd = new SqlCommand(selectSQL, con);
    SqlDataReader dr = cmd.ExecuteReader();
    dr.Close();
    con.Close();

    return Results.Created("/addUser", name);
}); 

app.Run();
