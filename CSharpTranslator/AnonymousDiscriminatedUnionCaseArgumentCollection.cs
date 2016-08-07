using System.Linq;

namespace CSharpTranslator
{
    public class AnonymousDiscriminatedUnionCaseParameterCollection : DiscriminatedUnionCaseArgumentCollection
    {
        public AnonymousDiscriminatedUnionCaseParameterCollection(
            string caseName,
            params string[] parameterTypes)
        {
            Parameters =
                parameterTypes.Zip(
                    Enumerable.Range(0, parameterTypes.Length),
                    (parametertype, parameterPosition) => new DiscriminatedUnionCaseParameter(caseName, parameterPosition, parametertype)).ToList().AsReadOnly();

            CaseName = caseName;
        }
    }
}