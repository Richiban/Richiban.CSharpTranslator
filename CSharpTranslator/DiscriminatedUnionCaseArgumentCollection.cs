using System.Collections.Generic;

namespace CSharpTranslator
{
    public abstract class DiscriminatedUnionCaseArgumentCollection
    {
        public IReadOnlyCollection<DiscriminatedUnionCaseArgument> Arguments { get; protected set; }
        public string CaseName { get; protected set; }
    }
}