/*
�������� � ASP.NET Core. 
�������� ��������� ������� � middleware-����������.
����������� Web-���������� �������������� �����. �������� ����� - �� 1 �� 100000. ��� ��������� HTTP GET-������� ���������� 
������������ ��������� middleware-���������.
*/

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
