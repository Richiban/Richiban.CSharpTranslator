using System.Collections.Generic;

namespace CSharpTranslator
{
    public abstract class DiscriminatedUnionCaseArgumentCollection
    {
        public IReadOnlyCollection<DiscriminatedUnionCaseParameter> Parameters { get; protected set; }
        public string CaseName { get; protected set; }
    }
}