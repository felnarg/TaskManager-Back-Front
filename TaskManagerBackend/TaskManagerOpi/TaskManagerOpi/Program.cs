using Application.Commands;
using Application.Interfaces;
using Application.Queries;
using Application.Services;
using Infrastructure.Repository;
using Infrastructure.SendGrid;
using Infrastructure.TaskDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("TaskManagerOpi").UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion21)), ServiceLifetime.Transient);

builder.Services.AddTransient(typeof(ITaskRepository<>), typeof(TaskRepository<>));
builder.Services.AddScoped<IChangeStatusTask, ChangeStatusTaskCommand>();
builder.Services.AddScoped<IOrderTasks, OrderTasks>();
builder.Services.AddScoped<ITaskCreate, TaskCreateCommand>();
builder.Services.AddScoped<ITaskDelete, TaskDeleteCommand>();
builder.Services.AddScoped<ITaskGetAll, TaskGetAllQuerie>();
builder.Services.AddScoped<ITaskGetById, TaskGetByIdQuerie>();
builder.Services.AddScoped<ITaskUpdate, TaskUpdateCommand>();
builder.Services.AddScoped<IImportanceFilter, ImportanceFilter>();
builder.Services.AddScoped<IUserNotificationPreferenceUpdateCommand, UserNotificationPreferenceUpdateCommand>();
builder.Services.AddScoped<IUserUpdate, UserUpdateCommand>();
builder.Services.AddScoped<ISendGridService, SendGridService>();
builder.Services.AddSingleton<INotification, Notification>();
builder.Services.AddSingleton<IExpiredTasksNotificationQuerie, ExpiredTasksNotificationQuerie>();

builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
