using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;
using NetAcademy.Services.Implementation;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;

namespace NetAcademy.Services.Tests
{
    public class TestServiceTests
    {
        private IUserService userServiceMock;

        private void Init()
        {
            userServiceMock = Substitute.For<IUserService>();
            //rest mocks there as well;
        }
        [Fact]
        public async Task IsPasswordIncorrect_WithCorrectData_ReturnsFalse()
        {
            //arrange
            Init();
            userServiceMock.CheckPassword(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsForAnyArgs(Task.FromResult(true));
            var testService = new TestService(userServiceMock);

            //act
            var result = await testService.IsPasswordIncorrect("test@ema.il", "123");

            //arrange 
            Assert.False(result);
        }

        [Fact]
        public async Task IsPasswordIncorrect_WithInCorrectData_ReturnsTrue()
        {
            //arrange
            Init();
            userServiceMock.CheckPassword(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsForAnyArgs(Task.FromResult(false));
            var testService = new TestService(userServiceMock);

            //act
            var result = await testService.IsPasswordIncorrect("test@ema.il", "123");

            //arrange 
            Assert.True(result);
        }

        [Fact]
        public async Task IsPasswordCorrect_WithCorrectData_ReturnsTrue()
        {
            //arrange
            Init();
            userServiceMock.CheckPassword(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsForAnyArgs(Task.FromResult(true));
            var testService = new TestService(userServiceMock);

            //act
            var result = await testService.IsPasswordCorrect("test@ema.il", "123");

            //arrange 
            Assert.True(result);
        }
        //{
        //    //AAA - Arrange Act Assert
        //    //arrange(initialize all necessary data and services)
        //    var contextMock = Substitute.For<BookStoreDbContext>([new DbContextOptions<BookStoreDbContext>()]);
        //    var loggerMock = Substitute.For<ILogger<UserService>>();
        //    var configMock = Substitute.For<IConfiguration>();

        //    //contextMock.Users.(new List<User>(){new User()
        //    //{
        //    //    Email = "test@ema.il"
        //    //}});
        //    //var userService = new UserService(contextMock,loggerMock, configMock);


        //    var email = "test@ema.il";
        //    //act
        //    var result = await userService.CheckIsUserWithEmailExists(email);

        //    //assert
        //    Assert.Equal(true, result);

    }
}