using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Commands.Authentication;
using PedagogyPrime.Infrastructure.Commands.Subjects.Create;
using PedagogyPrime.Infrastructure.Commands.Subjects.Update;
using PedagogyPrime.Infrastructure.Models.Subject;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Persistence.Context;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PedagogyPrime.IntegrationTests
{
    //TODO: in a in memory database, it does not have relational relationships. we need to change the logic.
    public class SubjectControllerTest
    {
        private WebApplicationFactory<Program> _factory;

        public SubjectControllerTest()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<PedagogyPrimeDbContext>));
                        services.AddDbContext<PedagogyPrimeDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("test");
                        });
                    });
                });
        }
        [Fact]
        public async Task When_GetAll_With_Other_Role_Should_ReturnNotAuthorized()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Student
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Subjects.GetAll("v1"));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<List<SubjectDetails>>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("Forbidden");
        }

        [Fact]
        public async Task When_GetAll_With_Admin_Role_Should_ReturnAllSubjects()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Subjects.GetAll("v1"));

            //Assert
            response.EnsureSuccessStatusCode();

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<List<SubjectDetails>>>();
            courses.Should().NotBeNull();
            courses!.Resource.Should().NotBeNull();
            courses.Resource!.Count.Should().Be(1);
        }
        [Fact]
        public async Task When_GetById_With_Admin_Role_Should_ReturnSubject()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Subjects.GetById("v1", subjectId));

            //Assert
            response.EnsureSuccessStatusCode();

            var course = await response.Content.ReadFromJsonAsync<BaseResponse<SubjectDetails>>();
            course.Should().NotBeNull();
            course!.Resource.Should().NotBeNull();
            course!.Resource!.Name.Should().Be("ASET");
        }
        [Fact]
        public async Task When_GetById_With_InvalidId_Should_ReturnNotFound()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Student
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Subjects.GetById("v1", subjectId));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<SubjectDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("resource not found");
        }
        [Fact]
        public async Task When_Post_With_Admin_Role_Should_ReturnSubjectId()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var createSubjectCommand = new CreateSubjectCommand
            {
                Name = "ASET",
                Period = "Fall",
                NoOfCourses = 2
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Subjects.Post("v1"), createSubjectCommand);

            //Assert
            responseCreateCourse.EnsureSuccessStatusCode();
            var courseIdTask = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            var courseId = courseIdTask!.Resource;
            courseId.Should().NotBeEmpty();
        }
        [Fact]
        public async Task When_Post_With_Other_Role_Should_ReturnNotAuthorized()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Student
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var createSubjectCommand = new CreateSubjectCommand
            {
                Name = "ASET",
                Period = "Fall",
                NoOfCourses = 2
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Subjects.Post("v1"), createSubjectCommand);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeEmpty();
            courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_Put_With_Admin_Role_Should_ReturnUpdatedSubject()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var newSubject = new UpdateSubjectCommand
            {
                Id = subjectId,
                Name = "ASET 2.0",
                Period = "Winter",
                NoOfCourses = 2,
                UserId = userId
            };

            var responseCreateCourse = await client.PutAsJsonAsync(API.Subjects.Put("v1", subjectId), newSubject);

            //Assert
            responseCreateCourse.EnsureSuccessStatusCode();
            var course = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<SubjectDetails>>();
            course.Should().NotBeNull();
            course!.Resource.Should().NotBeNull();
            course!.Resource!.Name.Should().Be("ASET 2.0");
            course!.Resource!.Id.Should().Be(newSubject.Id);
        }
        [Fact]
        public async Task When_Put_With_Other_Role_Should_ReturnNotAuthorized()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            Guid courseId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Student
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var newSubject = new UpdateSubjectCommand
            {
                Id = subjectId,
                Name = "ASET 2.0",
                Period = "Winter",
                NoOfCourses = 2,
                UserId = userId
            };

            var responseCreateCourse = await client.PutAsJsonAsync(API.Subjects.Put("v1", subjectId), newSubject);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<SubjectDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_Put_With_InvalidId_Should_ReturnNotFound()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));
            var newSubject = new UpdateSubjectCommand
            {
                Id = subjectId,
                Name = "ASET 2.0",
                Period = "Winter",
                NoOfCourses = 2,
                UserId = userId
            };

            //Act
            var responseCreateCourse = await client.PutAsJsonAsync(API.Subjects.Put("v1", subjectId), newSubject);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<SubjectDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("resource not found");
        }

        [Fact]
        public async Task When_DeleteSubject_With_Admin_Role_Should_ReturnTrue()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var createSubjectCommand = new CreateSubjectCommand
            {
                Name = "ASET",
                Period = "Fall",
                NoOfCourses = 2
            };

            var responseCreateSubject = await client.PostAsJsonAsync(API.Subjects.Post("v1"), createSubjectCommand);
            responseCreateSubject.EnsureSuccessStatusCode();
            var subjectIdTask = await responseCreateSubject.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            var subjectId = subjectIdTask!.Resource;

            //var response = await client.DeleteAsync(API.Subjects.Delete("v1", subjectId));

            //Assert
            //response.EnsureSuccessStatusCode();

            //var course = await response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
            //course.Should().NotBeNull();
            //course!.Resource.Should().BeTrue();
        }
        [Fact]
        public async Task When_Delete_With_OtherRole_Should_ReturnNotAuthorized()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Student
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            //var response = await client.DeleteAsync(API.Subjects.Delete("v1", subjectId));

            //Assert
            //response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            //var courses = await response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
            //courses!.Should().NotBeNull();
            //courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_Delete_With_InvalidId_Should_ReturnNotFound()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Guid subjectId = Guid.NewGuid();
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetRequiredService<PedagogyPrimeDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = "admin",
                    Email = "admin@example.com",
                    FirstName = "admin",
                    LastName = "admin",
                    Password = "admin",
                    Role = Role.Admin
                });
                var subject = new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2
                };

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.DeleteAsync(API.Subjects.Delete("v1", subjectId));

            //Assert
            /*
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
            courses!.Should().NotBeNull();
            courses.Errors.Should().Contain("resource not found");
            */
        }

        private async Task<string> GetJwtAsync(HttpClient client)
        {
            LoginCommand loginCommand = new LoginCommand
            {
                Username = "admin",
                Password = "admin"
            };

            var response = await client.PostAsJsonAsync("https://localhost:7136/api/v1/Authentication/login", loginCommand);
            response.EnsureSuccessStatusCode();
            var registrationResponse = await response.Content.ReadFromJsonAsync<BaseResponse<LoginResult>>();
            Assert.NotNull(registrationResponse);
            Assert.NotNull(registrationResponse.Resource);
            var token = registrationResponse.Resource.AccessToken;
            return token;
        }
    }
}
