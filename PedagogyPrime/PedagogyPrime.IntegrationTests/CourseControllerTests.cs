using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Commands.Authentication;
using PedagogyPrime.Infrastructure.Commands.Courses.Create;
using PedagogyPrime.Infrastructure.Commands.Courses.Update;
using PedagogyPrime.Infrastructure.Commands.Subjects.Create;
using PedagogyPrime.Infrastructure.Models.Course;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Persistence.Context;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Coverage = PedagogyPrime.Core.Entities.Coverage;

namespace PedagogyPrime.IntegrationTests
{
    //TODO: in a in memory database, it does not have relational relationships. we need to change the logic.
    public class CourseControllerTests
    {
        private WebApplicationFactory<Program> _factory;

        public CourseControllerTests()
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
            var response = await client.GetAsync(API.Courses.GetAll("v1"));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<List<CourseDetails>>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("Forbidden");
        }

        [Fact]
        public async Task When_GetAll_With_Admin_Role_Should_ReturnAllCourses()
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
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

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

            var createCourseCommand = new CreateCourseCommand
            {
                Name = "Cursul 1",
                Description = "Descriere",
                ContentUrl = "url",
                SubjectId = subjectId,
                Index = 0
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Courses.Post("v1"), createCourseCommand);
            responseCreateCourse.EnsureSuccessStatusCode();
            var courseIdTask = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            var courseId = courseIdTask!.Resource;

            //Act
            var response = await client.GetAsync(API.Courses.GetAll("v1"));

            //Assert
            response.EnsureSuccessStatusCode();

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<List<CourseDetails>>>();
            courses.Should().NotBeNull();
            courses!.Resource.Should().NotBeNull();
            courses.Resource!.Count.Should().Be(1);
        }
        [Fact]
        public async Task When_GetById_With_Admin_Role_Should_ReturnCourse()
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
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.Courses.Add(new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Courses.GetById("v1", courseId));

            //Assert
            response.EnsureSuccessStatusCode();

            var course = await response.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            course.Should().NotBeNull();
            course!.Resource.Should().NotBeNull();
            course!.Resource!.Name.Should().Be("Cursul 1");
        }
        [Fact]
        public async Task When_GetById_With_OtherRole_Should_ReturnNotAuthorized()
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
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.Courses.Add(new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var response = await client.GetAsync(API.Courses.GetById("v1", courseId));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_GetById_With_InvalidId_Should_ReturnNotFound()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
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
            var response = await client.GetAsync(API.Courses.GetById("v1", courseId));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var courses = await response.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("resource not found");
        }
        [Fact]
        public async Task When_Post_With_Admin_Role_Should_ReturnCourseId()
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
            var createCourseCommand = new CreateCourseCommand
            {
                Name = "Cursul 1",
                Description = "Descriere",
                ContentUrl = "url",
                SubjectId = subjectId,
                Index = 0
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Courses.Post("v1"), createCourseCommand);

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
            var createCourseCommand = new CreateCourseCommand
            {
                Name = "Cursul 1",
                Description = "Descriere",
                ContentUrl = "url",
                SubjectId = subjectId,
                Index = 0
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Courses.Post("v1"), createCourseCommand);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeEmpty();
            courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_Put_With_Admin_Role_And_No_Coverage_Should_ReturnUpdatedCourse()
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
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.Courses.Add(new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var newCourse = new UpdateCourseCommand
            {
                Id = courseId,
                Name = "Cursul 2",
                Description = "Noua descriere",
                ContentUrl = "nou url",
                IsVisibleForStudents = false,
                UserId = userId
            };

            var responseCreateCourse = await client.PutAsJsonAsync(API.Courses.Put("v1", courseId), newCourse);

            //Assert
            responseCreateCourse.EnsureSuccessStatusCode();
            var course = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            course.Should().NotBeNull();
            course!.Resource.Should().NotBeNull();
            course!.Resource!.Name.Should().Be("Cursul 2");
            course!.Resource!.Id.Should().Be(newCourse.Id);
        }
        [Fact]
        public async Task When_Put_With_Admin_Role_And_With_Coverage_Should_ReturnUpdatedCourse()
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
                    Role = Role.Admin
                });
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                var courseDb = new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                };
                var coverage = new Coverage
                {
                    Id = Guid.NewGuid(),
                    Percentage = 20d,
                    BadWords = new List<string> { "mere", "pere"},
                    GoodWords = new List<string> { "mere", "pere"},
                    CourseId = courseId
                };
                dbContext.Courses.Add(courseDb);
                dbContext.Coverages.Add(coverage);
               
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            var newCourse = new UpdateCourseCommand
            {
                Id = courseId,
                Name = "Cursul 2",
                Description = "Noua descriere",
                ContentUrl = "nou url",
                IsVisibleForStudents = false,
                UserId = userId
            };

            var responseCreateCourse = await client.PutAsJsonAsync(API.Courses.Put("v1", courseId), newCourse);

            //Assert
            /*
            responseCreateCourse.EnsureSuccessStatusCode();
            var course = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            course.Should().NotBeNull();
            course!.Resource.Should().NotBeNull();
            course!.Resource!.Name.Should().Be("Cursul 2");
            course!.Resource!.Id.Should().Be(newCourse.Id);
            */
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
            var newCourse = new UpdateCourseCommand
            {
                Id = courseId,
                Name = "Cursul 2",
                Description = "Noua descriere",
                ContentUrl = "nou url",
                IsVisibleForStudents = false,
                UserId = userId
            };

            var responseCreateCourse = await client.PutAsJsonAsync(API.Courses.Put("v1", courseId), newCourse);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("Forbidden");
        }
        [Fact]
        public async Task When_Put_With_InvalidId_Should_ReturnNotFound()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
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
                    Role = Role.Admin
                });
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));
            var newCourse = new UpdateCourseCommand
            {
                Id = courseId,
                Name = "Cursul 2",
                Description = "Noua descriere",
                ContentUrl = "nou url",
                IsVisibleForStudents = false,
                UserId = userId
            };

            //Act
            var responseCreateCourse = await client.PutAsJsonAsync(API.Courses.Put("v1", courseId), newCourse);

            //Assert
            responseCreateCourse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var courses = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<CourseDetails>>();
            courses!.Should().NotBeNull();
            courses!.Resource.Should().BeNull();
            courses.Errors.Should().Contain("resource not found");
        }

        [Fact]
        public async Task When_DeleteCourse_With_Admin_Role_Should_ReturnTrue()
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

            var createCourseCommand = new CreateCourseCommand
            {
                Name = "Cursul 1",
                Description = "Descriere",
                ContentUrl = "url",
                SubjectId = subjectId,
                Index = 0
            };
            var responseCreateCourse = await client.PostAsJsonAsync(API.Courses.Post("v1"), createCourseCommand);
            responseCreateCourse.EnsureSuccessStatusCode();
            var courseIdTask = await responseCreateCourse.Content.ReadFromJsonAsync<BaseResponse<Guid>>();
            var courseId = courseIdTask!.Resource;

            //var response = await client.DeleteAsync(API.Courses.Delete("v1", courseId));

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
                dbContext.Subjects.Add(new Subject
                {
                    Id = subjectId,
                    Name = "ASET",
                    Period = "Fall",
                    NoOfCourses = 2

                });
                dbContext.Courses.Add(new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                });

                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
           // var response = await client.DeleteAsync(API.Courses.Delete("v1", courseId));

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
            Guid courseId = Guid.NewGuid();
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
                dbContext.Courses.Add(new Course
                {
                    Id = courseId,
                    Name = "Cursul 1",
                    Description = "Descriere",
                    ContentUrl = "url",
                    SubjectId = subjectId,
                    Index = 0
                });


                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(client));

            //Act
            //var response = await client.DeleteAsync(API.Courses.Delete("v1", courseId));

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
