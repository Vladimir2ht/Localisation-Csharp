using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using LocalizationNamespace.Data;          // Контекст БД
using LocalizationNamespace.Services;      // Сервисы (если есть)
using LocalizationNamespace.Validators;    // Валидация

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("http://localhost:3000")
				.AllowAnyHeader()
				.AllowAnyMethod();
	});
});

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

builder.Services.AddScoped<TranslationsTableService>();
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<TranslationService>();

builder.Services.AddScoped<FluentValidation.IValidator<LocalizationNamespace.DTOs.LanguageDto>, LocalizationNamespace.Validators.LanguageValidator>();

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

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();