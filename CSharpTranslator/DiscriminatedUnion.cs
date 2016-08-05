using System.Text;
using System.Threading.Tasks;

namespace CSharpTranslator
{

    public class DiscriminatedUnion
    {
        public string Name { get; }
        public DiscriminatedUnionCaseCollection Cases { get; }
        public Constructor Constructor { get; }
        public ToStringMethod ToStringMethods { get; }
        public GetHashCodeMethod GetHashCodeMethod { get; } = new GetHashCodeMethod();
        public EqualsMethod EqualsMethod { get; }

        public DiscriminatedUnion(string name, params DiscriminatedUnionCase[] cases)
        {
            Name = name;
            Cases = new DiscriminatedUnionCaseCollection(cases);
            Constructor = new Constructor(name);
            ToStringMethods = new ToStringMethod(name, Cases);
            EqualsMethod = new EqualsMethod(Name, Cases);
        }

        public override string ToString() => $@"
public class {Name}
{{
{Cases.AsEnum()}

	private Case Discriminator {{ get; }}
	private object[] Items {{ get; }}
	
{Constructor}

{ToStringMethods}

{Cases.CreateAndDestructureMethods}

{EqualsMethod}

{GetHashCodeMethod}
}}";
    }
}
