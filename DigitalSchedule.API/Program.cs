using DigitalSchedule.Data.Context;
using DigitalSchedule.Data.Repository;
using DigitalSchedule.Data.Repository.Abstract;
using DigitalSchedule.Data.UnitOfWork;
using DigitalSchedule.Data.UnitOfWork.Abstract;
using DigitalSchedule.Service;
using DigitalSchedule.Service.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connection);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepositrory>(); ;

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
