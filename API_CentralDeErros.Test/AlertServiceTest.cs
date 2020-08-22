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

            using (var context = new CentralContext(fakeContext.FakeOptions, null))
            {
                var service = new AlertService(context, fakeContext.Mapper);
                var actual = service.GetAll(false);

                Assert.Equal(4, actual.Count());
            }
        }

        [Fact]
        public void Should_Return_All_Archived_Alerts()
        {
            var fakeContext = new FakeContext("GetAllArchivedAlerts");
            fakeContext.FillWith<Alert>();

            using (var context = new CentralContext(fakeContext.FakeOptions, null))
            {
                var service = new AlertService(context, fakeContext.Mapper);
                var actual = service.GetAll(true);

                Assert.Equal(2, actual.Count());
            }
        }

        [Fact]
        public void Should_Add_New_Alert()
        {
            var fakeContext = new FakeContext("AddNewAlert");

            using (var context = new CentralContext(fakeContext.FakeOptions, null))
            {
                var service = new AlertService(context, fakeContext.Mapper);
                var actual = service.AddAlert(
                    1,
                    "error",
                    "title",
                    "description",
                    "ip",
                    2,
                    "AnjhaHSQEGSQetol147"
                    );

                Assert.Equal(1, actual.Id);
            }
        }
    }
}
