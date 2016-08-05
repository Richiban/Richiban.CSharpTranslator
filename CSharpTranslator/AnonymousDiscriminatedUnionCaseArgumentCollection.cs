using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class AnonymousDiscriminatedUnionCaseArgumentCollection : DiscriminatedUnionCaseArgumentCollection
    {
        public AnonymousDiscriminatedUnionCaseArgumentCollection(
            string caseName,
            string firstArgumentType,
            params string[] furtherArguments)
        {
            var allArguments = new[] { firstArgumentType }.Concat(furtherArguments).ToList();

            Arguments =
                new ReadOnlyCollection<DiscriminatedUnionCaseArgument>(
                    allArguments.Zip(
                        Enumerable.Range(0, allArguments.Count),
                        (s, i) => new DiscriminatedUnionCaseArgument(caseName, i, s)).ToArray());

            CaseName = caseName;
        }
    }
}