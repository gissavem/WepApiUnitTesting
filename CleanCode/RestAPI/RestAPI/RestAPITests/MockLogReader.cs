using RestAPI;

namespace RestAPITests
{
    class MockLogReader : ILogReader
    {
        public string GetLogContent()
        {
            return "Mocked log content";
        }
    }
}
