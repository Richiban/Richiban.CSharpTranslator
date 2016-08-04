using System;
using System.IO;
using CSharpTranslator.Tests.Unit.Output;
using NUnit.Framework;

namespace CSharpTranslator.Tests.Unit
{
    [TestFixture]
    public class CompassTests
    {
        [Test]
        public void ToString_returns_South()
        {
            var south = Compass.South;

            Assert.That(south.ToString(), Is.EqualTo("South"));
        }

        [Test]
        public void ToString_returns_North()
        {
            var north = Compass.North;

            Assert.That(north.ToString(), Is.EqualTo("North"));
        }

        [Test]
        public void North_equality()
        {
            var north1 = Compass.North;
            var north2 = Compass.North;

            Assert.That(north1, Is.EqualTo(north2));
        }

        [Test]
        public void South_equality()
        {
            var south1 = Compass.South;
            var south2 = Compass.South;

            Assert.That(south1, Is.EqualTo(south2));
        }

        [Test]
        public void IsElsewhere()
        {
            var elsewhere = Compass.Elsewhere(true, false);

            var result = elsewhere.IsElsewhere(true, false);

            Assert.True(result);
        }

        [Test]
        public void IsElsewhereDestructuring()
        {
            var elsewhere = Compass.Elsewhere(true, false);

            bool isEast;
            bool isWest;
            var result = elsewhere.IsElsewhere(out isEast, out isWest);

            Assert.True(result);
            Assert.True(isEast);
            Assert.False(isWest);
        }
    }
}
