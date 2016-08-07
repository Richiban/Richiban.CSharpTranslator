using System;
using System.Linq;

namespace CSharpTranslator
{
    public class NamedDiscriminatedUnionCaseArgumentCollection : DiscriminatedUnionCaseArgumentCollection
    {
        public NamedDiscriminatedUnionCaseArgumentCollection(
            string caseName,
            params Tuple<string, string>[] parameterNamesAndTypes)
        {
            Parameters =
                parameterNamesAndTypes.Zip(
                    Enumerable.Range(0, parameterNamesAndTypes.Length),
                    (t, i) => new DiscriminatedUnionCaseParameter(caseName, i, t.Item1, t.Item2)).ToList().AsReadOnly();

            CaseName = caseName;
        }
    }
}