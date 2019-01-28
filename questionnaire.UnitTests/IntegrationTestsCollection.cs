using Xunit;

namespace questionnaire.Integration.Tests {
    [CollectionDefinition ("tests collection")]
    public class IntegrationTestsCollection : ICollectionFixture<TestHostFixture> { }
}