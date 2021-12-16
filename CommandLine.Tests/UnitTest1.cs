using NUnit.Framework;

namespace CommandLine.Tests
{
    public class Tests
    {
        [Test]
        public void ArgsTest()
        {
            var args = ArgsProvider.Provide("j#,f?,w*",
                new[]
                {
                    "-j123",
                    "-f",
                    "-wHallo"
                }
            );

            Assert.AreEqual(true, args.Get<bool>("f"));
            Assert.AreEqual(123, args.Get<int?>("j"));
            Assert.AreEqual("Hallo", args.Get<string>("w"));
        }

        [TestCase("-f", true)]
        [TestCase("", false)]
        public void BoolTestCases(string arg, bool expectedValue)
        {
            var args = ArgsProvider.Provide("f?", new[] { arg });

            Assert.AreEqual(expectedValue, args.Get<bool>("f"));
        }
    }
}