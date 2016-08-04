using System.Text;
using System.Threading.Tasks;

namespace CSharpTranslator
{

    public class DiscriminatedUnion
    {
        public string Name { get; }
        public DiscriminatedUnionCaseCollection Cases { get; }
        public Constructor Constructor { get; }
        public ToStringMethodCollection ToStringMethods { get; } = new ToStringMethodCollection();
        public GetHashCodeMethod GetHashCodeMethod { get; } = new GetHashCodeMethod();
        public EqualsMethod EqualsMethod { get; }

        public DiscriminatedUnion(string name, params DiscriminatedUnionCase[] cases)
        {
            Name = name;
            Cases = new DiscriminatedUnionCaseCollection(cases);
            Constructor = new Constructor(name);
            EqualsMethod = new EqualsMethod(Name, Cases);
        }

        public override string ToString() => $@"
public class {Name}
{{
{Cases.AsEnum()}
	protected Case Discriminator {{ get; }}
	protected object[] Items {{ get; }}
	
{Constructor}

{ToStringMethods}

{Cases.CreateAndDestructureMethods}

{EqualsMethod}

{GetHashCodeMethod}
}}";
    }
}
