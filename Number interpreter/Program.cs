/*
Введение в ASP.NET Core. 
Конвейер обработки запроса и middleware-компоненты.
Разработать Web-приложение “Интерпретатор чисел”. Диапазон чисел - от 1 до 100000. Для обработки HTTP GET-запроса необходимо 
использовать несколько middleware-компонент.
*/

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
