using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpTranslator
{
    public class Parser
    {
        public static DiscriminatedUnion Parse(string sourceCode)
        {
            return ParseDU(sourceCode.Trim());
        }

        public static DiscriminatedUnion ParseDU(string sourceCode)
        {
            sourceCode = sourceCode.Replace(Environment.NewLine, "");
            var regex = new Regex(@"^public enum class (\w+)\s*\{\s*(.*)\}");
            var matches = regex.Matches(sourceCode);
            var typeName = matches[0].Groups[1].Value;

            var typeBodySource = matches[0].Groups[2].Value;

            return new DiscriminatedUnion(typeName, ParseCases(typeBodySource, typeName));
        }

        public static DiscriminatedUnionCase[] ParseCases(string typeBodySource, string typeName)
        {
            var regex = new Regex(@"\w+(\([\w,\s]+\))?");
            var matches = regex.Matches(typeBodySource);

            var cases = matches.Cast<Match>().Select(_ => ParseCase(_.Value, typeName)).ToArray();

            return cases;
        }

        public static DiscriminatedUnionCase ParseCase(string caseSource, string typeName)
        {
            var regex = new Regex(@"^(?<caseName>\w+)(?<caseParametersSource>.*)$");
            var matches = regex.Matches(caseSource);
            var groups = matches[0].Groups;
            var caseName = groups["caseName"].Value;
            var caseParametersSource = groups["caseParametersSource"];

            var caseHasParameters = caseParametersSource.Length > 0;

            if (caseHasParameters)
            {
                return ParseCaseParameters(caseName, typeName, caseParametersSource.Value);
            }
            else
            {
                return new SimpleDiscriminatedUnionCase(caseName, typeName);
            }
        }

        private static ParameterisedDiscriminatedUnionCase ParseCaseParameters(
            string caseName,
            string typeName,
            string caseParametersSource)
        {
            var splitCaseParameters = caseParametersSource
                .Trim()
                .TrimStart('(')
                .TrimEnd(')')
                .Split(',')
                .Select(_ => _.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            if (splitCaseParameters.All(parts => parts.Length > 1))
            {
                return ParseNamedCaseParameters(caseName, typeName, splitCaseParameters);
            }
            else
            {
                return ParseAnonymousCaseParameters(caseName, typeName, splitCaseParameters);
            }
        }

        private static ParameterisedDiscriminatedUnionCase ParseAnonymousCaseParameters(
            string caseName,
            string typeName,
            IEnumerable<string[]> splitCaseParameters)
        {
            var parameterTypes = splitCaseParameters.Select(_ => _[0]).ToArray();

            return new ParameterisedDiscriminatedUnionCase(caseName, typeName, parameterTypes);
        }

        private static ParameterisedDiscriminatedUnionCase ParseNamedCaseParameters(
            string caseName,
            string typeName,
            IEnumerable<string[]> splitCaseParameters)
        {
            var tuples = splitCaseParameters.Select(_ => new Tuple<string, string>(_[1], _[0])).ToArray();

            return new ParameterisedDiscriminatedUnionCase(caseName, typeName, tuples);
        }
    }
}
