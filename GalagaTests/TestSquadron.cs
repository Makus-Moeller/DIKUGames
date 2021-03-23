using NUnit.Framework;

namespace GalagaTests
{
    public class TestSquadron
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