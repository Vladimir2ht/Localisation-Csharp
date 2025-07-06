using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using LocalizationNamespace.Data;          // Контекст БД
using LocalizationNamespace.Services;      // Сервисы (если есть)
using LocalizationNamespace.Validators;    // Валидация

var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию подключения к PostgreSQL из appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем контроллеры с поддержкой FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>(); // Регистрируем валидаторы из сборки
    });

// Добавляем Swagger для удобства тестирования API (необязательно, но рекомендуется)
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Мидлвары
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    // app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error"); // Можно настроить контроллер ошибки
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// app.UseCors(CorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();