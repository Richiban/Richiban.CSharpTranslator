using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class NamedDiscriminatedUnionCaseArgumentCollection : DiscriminatedUnionCaseArgumentCollection
    {
        public NamedDiscriminatedUnionCaseArgumentCollection(
            string caseName,
            Tuple<string, string> firstArgumentNameAndType,
            params Tuple<string, string>[] furtherArguments)
        {
            var allArguments = new[] { firstArgumentNameAndType }.Concat(furtherArguments).ToList();

            Arguments =
                new ReadOnlyCollection<DiscriminatedUnionCaseArgument>(
                    allArguments.Zip(
                        Enumerable.Range(0, allArguments.Count),
                        (t, i) => new DiscriminatedUnionCaseArgument(caseName, i, t.Item1, t.Item2)).ToArray());

            CaseName = caseName;
        }
    }
}