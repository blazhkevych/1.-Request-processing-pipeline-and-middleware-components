using Number_interpreter;

var builder = WebApplication.CreateBuilder(args);

// ��� ������ �������� ������ ������� IDistributedCache, � 
// ASP.NET Core ������������� ���������� ���������� IDistributedCache
builder.Services.AddDistributedMemoryCache(); // ��������� IDistributedMemoryCache
builder.Services.AddSession(); // ��������� ������� ������
var app = builder.Build();

app.UseSession(); // ��������� middleware-��������� ��� ������ � ��������

// ��������� middleware-���������� � �������� ��������� �������.
app.UseThousands();
app.UseHundreds();
app.UseTens();
app.UseOnes();

app.Run();