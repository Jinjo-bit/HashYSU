using System.Collections;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//test
ArrayList myAl = new ArrayList();
{
    myAl.Add("hello"); 
};

//HashTest
Console.WriteLine("enter password");
string password = Console.ReadLine();

byte[] iv = new byte[128 / 8];
using (var rngCsp = new RNGCryptoServiceProvider())
{
    rngCsp.GetBytes(iv);
}
Console.WriteLine($"Salt: {Convert.ToBase64String(iv)}");

string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: iv,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
Console.WriteLine($"Hashed: {hashed}");
//HashTest End
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

