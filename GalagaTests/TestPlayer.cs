using NUnit.Framework;

namespace GalagaTests
{
    public class TestPlayer
    {
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}