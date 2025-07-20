using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using LocalizationNamespace.Data;
using LocalizationNamespace.Services;
using LocalizationNamespace.Validators;

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

builder.Services.AddControllers()
	.AddFluentValidation(fv =>
	{
		fv.RegisterValidatorsFromAssemblyContaining<Program>();
	});

var serviceAssembly = typeof(LocalizationNamespace.Services.LanguageService).Assembly;
var serviceTypes = serviceAssembly.GetTypes()
	.Where(t => t.IsClass && !t.IsAbstract && t.Namespace != null && t.Namespace.Contains("Services"));

foreach (var type in serviceTypes)
{
	builder.Services.AddScoped(type);
}

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
	app.UseExceptionHandler("/error");
	app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();