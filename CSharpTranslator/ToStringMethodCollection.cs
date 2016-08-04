namespace CSharpTranslator
{
    public class ToStringMethodCollection
    {
        public override string ToString() =>
            @"	private string ItemsToString() => Items.Length == 0 ? """" : $""({String.Join("","", Items)})"";
    public override string ToString() => $""{Discriminator}{ItemsToString()}"";";
    }
}