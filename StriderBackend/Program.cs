using AutoMapper;
using Domain.Core.Commands;
using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using Infra.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Api.Configuration.AutoMapper;
using StriderBackend.Domain.Repositories;
using StriderBackend.InfraData.Context;
using StriderBackend.InfraData.Repositories;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(o =>
    o.UseSqlServer("Data Source=localhost,11433;Persist Security Info=True;Initial Catalog=PosterrDatabase;User ID=sa;Password=ea!@#12345;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<DataContext>>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMediatorHandler, InMemoryMediator>();

builder.Services.AddScoped<IRequestHandler<DomainNotification, bool>, DomainNotificationHandler>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(ViewModelToDomainProfile));
builder.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
