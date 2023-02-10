using EmployeeApi.Controllers;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace EmployeeTestProject
{
    public class EmployeeTests
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact(DisplayName = "Get Employees Should return result")]
        public void Get_Employees_Return_Result()
        {
            using (var context = GetContextWithData())
            {
                EmployeesController? controller = new EmployeesController(context);
                var result = controller.GetEmployees();

                Assert.NotNull(result);
            }
        }

        [Fact(DisplayName = "Get Employee should return valid model")]
        public void Get_Employee_should_return_valid_model()
        {
            using (var context = GetContextWithData())
            {
                EmployeesController? controller = new EmployeesController(context);
                var result = controller.GetEmployee(1);
                var employee = result.Result.Value as Employee;
                Assert.NotNull(employee);
                Assert.True(employee.Id == 1);

            }
        }
        [Fact(DisplayName = "Get Employee Should return null")]
        public void Get_Employee_should_return_null()
        {
            using (var context = GetContextWithData())
            {
                EmployeesController? controller = new EmployeesController(context);
                var result = controller.GetEmployee(-1);
               
                Assert.Null(result.Result.Value);
            }
        }

        [Fact(DisplayName = "Post Employee Should add new employee")]
        public void Post_Employee_should_return_null()
        {
            using (var context = GetContextWithData())
            {
                var Employee1 = new Employee { Id = 3, Name = "Emp10" };
                EmployeesController? controller = new EmployeesController(context);
                var result = controller.PostEmployee(Employee1);
                result = controller.GetEmployee(3);

                Assert.NotNull(result.Result.Value);
            }
        }

        private EmployeeContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<EmployeeContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new EmployeeContext(options);

            var Employee1 = new Employee { Id = 1, Name = "Emp1" };
            var Employee2 = new Employee { Id = 2, Name = "Emp2" };
            context.Employees.Add(Employee1);
            context.Employees.Add(Employee2);
            context.SaveChanges();

            return context;
        }
    }
}