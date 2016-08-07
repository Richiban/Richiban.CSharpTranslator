using CSharpTranslator.Tests.Unit.Output;
using NUnit.Framework;

namespace CSharpTranslator.Tests.Unit
{
    [TestFixture]
    public class ShapeTests
    {
        [Test]
        public void ToString_returns_Point()
        {
            var shape = Shape.Point;

            Assert.That(shape.ToString(), Is.EqualTo("Point"));
        }

        [Test]
        public void ToString_returns_Line()
        {
            var shape = Shape.Line(5);

            Assert.That(shape.ToString(), Is.EqualTo("Line(5)"));
        }

        [Test]
        public void ToString_returns_Square()
        {
            var shape = Shape.Square(1, 2);

            Assert.That(shape.ToString(), Is.EqualTo("Square(Width: 1, Height: 2)"));
        }

        [Test]
        public void ToString_returns_Cube()
        {
            var shape = Shape.Cube(10, 20, 30);

            Assert.That(shape.ToString(), Is.EqualTo("Cube(Width: 10, Height: 20, Depth: 30)"));
        }

        [Test]
        public void Cube_destructure()
        {
            var shape = Shape.Cube(10, 20, 30);

            int width;
            int height;
            var success = shape.IsCube(out width, out height, 30);

            Assert.That(success, Is.True);
            Assert.That(width, Is.EqualTo(10));
            Assert.That(height, Is.EqualTo(20));
        }

        [Test]
        public void Cube_destructure2()
        {
            var shape = Shape.Cube(10, 20, 30);

            int width;
            int height;
            var success = shape.IsCube(out width, out height, 40);

            Assert.That(success, Is.False);
        }
    }
}
