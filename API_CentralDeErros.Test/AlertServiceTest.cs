using API_CentralDeErros.Infra;
using API_CentralDeErros.Model;
using API_CentralDeErros.Service;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace API_CentralDeErros.Test
{
    public class AlertServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public AlertServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Should_Return_All_Unarchived_Alerts()
        {
            var fakeContext = new FakeContext("GetAllUnarchivedAlerts");
            fakeContext.FillWith<Alert>();

            using (var context = new CentralContext(fakeContext.FakeOptions))
            {
                var service = new AlertService(context, fakeContext.Mapper);
                var actual = service.GetAll(false);

                Assert.Equal(4, actual.Count());
            }
        }

        [Fact]
        public void Should_Return_All_Archived_Alerts()
        {
            var fakeContext = new FakeContext("GetAllUnarchivedAlerts");
            fakeContext.FillWith<Alert>();

            using (var context = new CentralContext(fakeContext.FakeOptions))
            {
                var service = new AlertService(context, fakeContext.Mapper);
                var actual = service.GetAll(true);

                Assert.Equal(2, actual.Count());
            }
        }
    }
}
