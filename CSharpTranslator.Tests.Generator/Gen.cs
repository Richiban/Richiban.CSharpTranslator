using System.IO;
using NUnit.Framework;

namespace CSharpTranslator.Tests.Generator
{
    [TestFixture]
    public class Gen
    {
        [Test]
        public void CreateCompassType()
        {
            var typeName = "Compass";

            var du = new DiscriminatedUnion(
                typeName,
                new SimpleDiscriminatedUnionCase("North", typeName),
                new SimpleDiscriminatedUnionCase("South", typeName),
                new DiscriminatedUnionCaseWithArguments(
                    "Elsewhere",
                    typeName,
                    new DiscriminatedUnionCaseArgument(0, "IsEast", "bool"),
                    new DiscriminatedUnionCaseArgument(1, "IsWest", "bool")));

            var typeDefinition = du.ToString();
            WriteToFile(typeName, typeDefinition);
        }

        private void WriteToFile(string typeName, string typeDefinition)
        {
            var fileName =
                $@"C:\Repositories\Richiban\CSharpTranslator\CSharpTranslator.Tests.Unit\Output\{typeName}.cs";

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
