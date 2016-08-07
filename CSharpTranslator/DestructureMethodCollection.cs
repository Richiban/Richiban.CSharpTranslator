using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class DestructureMethodCollection
    {
        private ValueDestructureMethodParameter Value(DiscriminatedUnionCaseParameter arg)
        {
            return new ValueDestructureMethodParameter(arg.Position, arg.Name, arg.Type);
        }

        private OutDestructureMethodParameter Out(DiscriminatedUnionCaseParameter arg)
        {
            return new OutDestructureMethodParameter(arg.Position, arg.Name, arg.Type);
        }

        public DestructureMethodCollection(DiscriminatedUnionCaseArgumentCollection arguments)
        {
            DestructureMethods = GetDestructureMethods(arguments.CaseName, arguments.Parameters).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<DestructureMethod> DestructureMethods { get; }

        private IEnumerable<DestructureMethod> GetDestructureMethods(
            string caseName,
            IReadOnlyCollection<DiscriminatedUnionCaseParameter> arguments)
        {
            var fs = new Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>[] { Value, Out };
            var results = EnumerateOutcomes(fs, arguments.Count);

            foreach (var result in results)
            {
                var a = result.Zip(arguments, (func, argument) => func(argument));
                yield return new DestructureMethod(caseName, a.ToList().AsReadOnly());
            }
        }

        public IEnumerable<IEnumerable<Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>>>
            RecEnumerateOutcomes(
            ImmutableStack<Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>> results,
            Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>[] resultTypes,
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

        public IEnumerable<IEnumerable<Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>>>
            EnumerateOutcomes(
                Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>[] resultTypes,
                int numIterations)
        {
            var results = ImmutableStack<Func<DiscriminatedUnionCaseParameter, DestructureMethodParameter>>.Empty;

            var outcomes = RecEnumerateOutcomes(results, resultTypes, numIterations);

            return outcomes;
        }
    }
}