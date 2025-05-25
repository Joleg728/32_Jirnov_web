using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"Current directory: {Assembly.GetExecutingAssembly().Location}");
Console.WriteLine($"Config files: {string.Join(", ", builder.Configuration.Sources)}");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", // �������� �������� (����� ���� �����)
        policy =>
        {
            policy.AllowAnyOrigin() // ��������� ����� ��������� (��� ����������)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
