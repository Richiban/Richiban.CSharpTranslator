using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class DestructureMethodCollection
    {
        private ValueDestructureMethodParameter Value(DiscriminatedUnionCaseArgument arg)
        {
            return new ValueDestructureMethodParameter(arg.Position, arg.Name, arg.Type);
        }

        private OutDestructureMethodParameter Out(DiscriminatedUnionCaseArgument arg)
        {
            return new OutDestructureMethodParameter(arg.Position, arg.Name, arg.Type);
        }

        public DestructureMethodCollection(string caseName, IReadOnlyCollection<DiscriminatedUnionCaseArgument> arguments)
        {
            DestructureMethods = GetDestructureMethods(caseName, arguments).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<DestructureMethod> DestructureMethods { get; }

        private IEnumerable<DestructureMethod> GetDestructureMethods(
            string caseName,
            IReadOnlyCollection<DiscriminatedUnionCaseArgument> arguments)
        {
            foreach (var discriminatedUnionCaseArgument in arguments)
            {
                foreach (
                    var kind in new Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>[] { Out, Value })
                {
                    var ps = arguments.Select(arg => kind(arg));
                    yield return new DestructureMethod(caseName, ps.ToList().AsReadOnly());
                }
            }
        }
    }
}