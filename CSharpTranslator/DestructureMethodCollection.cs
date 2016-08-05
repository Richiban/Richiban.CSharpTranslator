using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        public DestructureMethodCollection(
            string caseName,
            IReadOnlyCollection<DiscriminatedUnionCaseArgument> arguments)
        {
            DestructureMethods = GetDestructureMethods(caseName, arguments).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<DestructureMethod> DestructureMethods { get; }

        private IEnumerable<DestructureMethod> GetDestructureMethods(
            string caseName,
            IReadOnlyCollection<DiscriminatedUnionCaseArgument> arguments)
        {
            var fs = new Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>[] { Value, Out };
            var results = EnumerateOutcomes(fs, arguments.Count);

            foreach (var result in results)
            {
                var a = result.Zip(arguments, (func, argument) => func(argument));
                yield return new DestructureMethod(caseName, a.ToList().AsReadOnly());
            }
        }

        public IEnumerable<IEnumerable<Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>>>
            RecEnumerateOutcomes(
            ImmutableStack<Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>> results,
            Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>[] resultTypes,
            int numIterations)
        {
            if (results.Count() >= numIterations)
            {
                yield return results;
            }
            else
            {
                foreach (var resultType in resultTypes)
                {
                    foreach (var a in RecEnumerateOutcomes(results.Push(resultType), resultTypes, numIterations))
                    {
                        yield return a;
                    }
                }
            }
        }

        public IEnumerable<IEnumerable<Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>>>
            EnumerateOutcomes(
                Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>[] resultTypes,
                int numIterations)
        {
            var results = ImmutableStack<Func<DiscriminatedUnionCaseArgument, DestructureMethodParameter>>.Empty;

            var outcomes = RecEnumerateOutcomes(results, resultTypes, numIterations);

            return outcomes;
        }
    }
}