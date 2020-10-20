using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using SP.Contract.Common;
using SP.Market.Identity.Common.Interfaces;
using MockFactory = SP.Contract.Persistence.Integration.Tests.Common.MockFactory;

namespace SP.Contract.Persistence.Integration.Tests
{
    public class ApplicationDbContextTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextTests()
        {
            var dateTime = new DateTime(3001, 1, 1);
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(dateTime);

            ICurrentUser currentUser = CurrentUser.Create(
                "stec.superuser@mail.ru", "test", "test", "test", 1, null, string.Empty, new[] { "superuser.module.platform" });
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var mockMediator = MockFactory.CreateMediatorMock<IMediator>();

            _context = new ApplicationDbContext(
                options, currentUserServiceMock.Object, dateTimeMock.Object, mockMediator);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}