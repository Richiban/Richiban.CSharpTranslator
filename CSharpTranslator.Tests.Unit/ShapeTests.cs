using System;
using System.IO;
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

            Assert.That(shape.ToString(), Is.EqualTo("Line"));
        }

        [Test]
        public void ToString_returns_Square()
        {
            var shape = Shape.Square(1, 2);

            Assert.That(shape.ToString(), Is.EqualTo("Square"));
        }

        [Test]
        public void ToString_returns_Cube()
        {
            var shape = Shape.Cube(1, 2, 3);

            Assert.That(shape.ToString(), Is.EqualTo("Cube"));
        }
    }
}
