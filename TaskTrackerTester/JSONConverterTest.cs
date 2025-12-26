using JsonValidator.FluentAssertions.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskTracker.IO;
using Testing.Dynamic;



namespace TaskTrackerTester
{
    public class JSONConverterTest
    {
        [Fact]
        public void TestSerialize()
        {
            TestInitializer initializer = new TestInitializer();
            List<FileRecord> contents = initializer.Init();
            string Json = string.Empty;

            using (JSONConverter converter = new JSONConverter())
            {
                string stringContents = converter.Serialize(contents);

                Assert.NotNull(stringContents);
                Assert.NotEmpty(stringContents);

                Json = Newtonsoft.Json.JsonConvert.SerializeObject(stringContents);
            }
        }

        [Fact]
        public void TestDeserialize()
        {
            TestInitializer initializer = new TestInitializer();
            List<FileRecord> contents = initializer.Init();
      
            using (JSONConverter converter = new JSONConverter())
            {
                string json = converter.Serialize(contents);
                contents.Clear();
                contents = converter.Deserialize(json);

                Assert.NotNull(contents);

                contents.ForEach(x => {
                    Assert.NotEqual(x.ID, 0);
                    Assert.NotNull(x.Description);
                    });
            }
        }
    }
}