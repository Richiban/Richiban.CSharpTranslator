using System;
using System.IO;
using NUnit.Framework;

namespace CSharpTranslator.Tests.Generator
{
    [TestFixture]
    public class Gen
    {
        [Test]
        public void CreateShapeType()
        {
            var typeName = "Shape";

            var du = new DiscriminatedUnion(
                typeName,
                new SimpleDiscriminatedUnionCase("Point", typeName),
                new ParameterisedDiscriminatedUnionCase("Line", typeName, "int"),
                new ParameterisedDiscriminatedUnionCase(
                    "Square",
                    typeName,
                    Tuple.Create("Width", "int"),
                    Tuple.Create("Height", "int")),
                new ParameterisedDiscriminatedUnionCase(
                    "Cube",
                    typeName,
                    Tuple.Create("Width", "int"),
                    Tuple.Create("Height", "int"),
                    Tuple.Create("Depth", "int")));

            var typeDefinition = du.ToString();
            WriteToFile(typeName, typeDefinition);
        }

        [Test]
        public void CreateOptionType()
        {
            var typeName = "Option";

            var du = new DiscriminatedUnion(
                typeName,
                new SimpleDiscriminatedUnionCase("None", typeName),
                new ParameterisedDiscriminatedUnionCase("Some", typeName, "object"));

            var typeDefinition = du.ToString();
            WriteToFile(typeName, typeDefinition);
        }

        private void WriteToFile(string typeName, string typeDefinition)
        {
            var fileName =
                $@"C:\Users\Richard\Projects\Richiban.CSharpTranslator\CSharpTranslator.Tests.Unit\Output\{typeName}.cs";

            var fileContents =
                $@"using System;

namespace CSharpTranslator.Tests.Unit.Output
{{
{typeDefinition}
}}
";

            File.WriteAllText(fileName, fileContents);
        }
    }
}