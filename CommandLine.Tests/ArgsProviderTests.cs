using System;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace CommandLine.Tests
{
    public class ArgsProviderTests
    {
        [Test]
        public void ArgsTest()
        {
            var args = ArgsProvider.Provide("j#,f?,w*,z+,i$,d%,b&",
                new[]
                {
                    "-j123",
                    "-f",
                    "-wHallo",
                    "-z12,5",
                    "-i192.168.0.1",
                    "-d25.03.2019 12:25",
                    "-bSGFsbG8="
                }
            );

            Assert.AreEqual(true, args.Get<bool>("f"));
            Assert.AreEqual(123, args.Get<int?>("j"));
            Assert.AreEqual("Hallo", args.Get<string>("w"));
            Assert.AreEqual(12.5, args.Get<float?>("z"));
            Assert.AreEqual(IPAddress.Parse("192.168.0.1"), args.Get<IPAddress>("i"));
            Assert.AreEqual(new DateTime(2019, 03, 25, 12, 25, 0), args.Get<DateTime?>("d"));
            Assert.AreEqual(Encoding.UTF8.GetBytes("Hallo"), args.Get<byte[]>("b"));
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