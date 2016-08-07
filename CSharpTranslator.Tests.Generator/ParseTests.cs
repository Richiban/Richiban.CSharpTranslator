using NUnit.Framework;
using System.Linq;

namespace CSharpTranslator.Tests.Generator
{
    [TestFixture]
    public class ParseTests
    {
            readonly string SourceCode = @"
public enum class TestType
{
    One, Two(string), Three(int, bool), Four(object a, float b, byte c)
}
";

        [Test]
        public void NameIsParsedCorrectly()
        {
            var typeName = "TestType";

            var du = Parser.Parse(SourceCode);

            Assert.That(du.Name, Is.EqualTo(typeName));
        }

        [Test]
        public void NumberOfCasesIsParsedCorrectly()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.Count(), Is.EqualTo(4));
        }

        [Test]
        public void FirstCaseHasNameOne()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.First().CaseName, Is.EqualTo("One"));
        }

        [Test]
        public void FirstCaseIsSimple()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.First(), Is.TypeOf<SimpleDiscriminatedUnionCase>());
        }

        [Test]
        public void FirstCaseHasTypeNameTestType()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.First().TypeName, Is.EqualTo("TestType"));
        }

        [Test]
        public void SecondCaseHasNameTwo()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(1).CaseName, Is.EqualTo("Two"));
        }

        [Test]
        public void SecondCaseIsParameterised()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(1), Is.TypeOf<ParameterisedDiscriminatedUnionCase>());
        }

        [Test]
        public void SecondCaseHasASingleParameter()
        {
            var du = Parser.Parse(SourceCode);

            var secondCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(1);

            Assert.That(secondCase.Parameters.Parameters.Count, Is.EqualTo(1));
        }

        [Test]
        public void SecondCaseFirstParameterHasTypeString()
        {
            var du = Parser.Parse(SourceCode);

            var secondCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(1);

            Assert.That(secondCase.Parameters.Parameters.Single().Type, Is.EqualTo("string"));
        }

        [Test]
        public void ThirdCaseHasNameThree()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(2).CaseName, Is.EqualTo("Three"));
        }

        [Test]
        public void ThirdCaseIsParameterised()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(2), Is.TypeOf<ParameterisedDiscriminatedUnionCase>());
        }

        [Test]
        public void ThirdCaseHasTwoParameters()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(2);

            Assert.That(thirdCase.Parameters.Parameters.Count, Is.EqualTo(2));
        }

        [Test]
        public void ThirdCaseFirstParameterHasTypeInt()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(2);

            Assert.That(thirdCase.Parameters.Parameters.First().Type, Is.EqualTo("int"));
        }

        [Test]
        public void ThirdCaseSecondParameterHasTypeBool()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(2);

            Assert.That(thirdCase.Parameters.Parameters.ElementAt(1).Type, Is.EqualTo("bool"));
        }

        [Test]
        public void ThirdCaseTypeNameIsTestType()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.TypeName, Is.EqualTo("TestType"));
        }

        [Test]
        public void FourthCaseHasNameFour()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(3).CaseName, Is.EqualTo("Four"));
        }

        [Test]
        public void FourthCaseIsParameterised()
        {
            var du = Parser.Parse(SourceCode);

            Assert.That(du.Cases.Cases.ElementAt(3), Is.TypeOf<ParameterisedDiscriminatedUnionCase>());
        }

        [Test]
        public void FourthCaseHasThreeParameters()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.Parameters.Parameters.Count, Is.EqualTo(3));
        }

        [Test]
        public void FourthCaseFirstParameterHasTypeObject()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.Parameters.Parameters.First().Type, Is.EqualTo("object"));
        }

        [Test]
        public void FourthCaseFirstParameterHasNameA()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);
            var firstParameter = thirdCase.Parameters.Parameters.First();

            Assert.That(firstParameter.Name, Is.EqualTo("a"));
        }

        [Test]
        public void FourthCaseSecondParameterHasTypeFloat()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.Parameters.Parameters.ElementAt(1).Type, Is.EqualTo("float"));
        }

        [Test]
        public void FourthCaseSecondParameterHasNameB()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);
            var secondParameter = thirdCase.Parameters.Parameters.ElementAt(1);

            Assert.That(secondParameter.Name, Is.EqualTo("b"));
        }

        [Test]
        public void FourthCaseThirdParameterHasTypeByte()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.Parameters.Parameters.ElementAt(2).Type, Is.EqualTo("byte"));
        }

        [Test]
        public void FourthCaseThirdParameterHasNameC()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = (ParameterisedDiscriminatedUnionCase)du.Cases.Cases.ElementAt(3);
            var thirdParameter = thirdCase.Parameters.Parameters.ElementAt(2);

            Assert.That(thirdParameter.Name, Is.EqualTo("c"));
        }

        [Test]
        public void FourthCaseTypeNameIsTestType()
        {
            var du = Parser.Parse(SourceCode);

            var thirdCase = du.Cases.Cases.ElementAt(3);

            Assert.That(thirdCase.TypeName, Is.EqualTo("TestType"));
        }
    }
}
