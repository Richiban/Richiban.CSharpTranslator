using System;

namespace CSharpTranslator.Tests.Unit.Output
{

public class Option
{
	private enum Case
	{
		None, Some
	}

	private Case Discriminator { get; }
	private object[] Items { get; }
	
	private Option(Case discriminator, params object[] items)
	{
		Discriminator = discriminator;
		Items = items;
	}

	public override string ToString()
	{
		if (this.IsNone())
			return "None";
			
		object Some_0;
        if (this.IsSome(out Some_0))
			return $"Some({Items[0]})";
			
		return "";
	}


	public static readonly Option None = new Option(Case.None);
	public bool IsNone() => Discriminator == Case.None;
	
	public static Option Some(object _0) => new Option(Case.Some, _0);

	public bool IsSome(object _0)
	{
		if (Discriminator == Case.Some)
		{
            var result = true;

			result = result && (object)Items[0] == _0;

            return result;
		}
		else
		{


			return false;
		}
	}

	public bool IsSome(out object _0)
	{
		if (Discriminator == Case.Some)
		{
            var result = true;

			_0 = (object)Items[0];

            return result;
		}
		else
		{
			_0 = default(object);

			return false;
		}
	}
	


	public override bool Equals(object other)
	{
		var otherOption = other as Option;

		if (otherOption == null)
			return false;
			
		if (otherOption.IsNone() && this.IsNone())
			return true;
			
		object otherSome_0;
		if (otherOption.IsSome(out otherSome_0) && this.IsSome(otherSome_0))
			return true;
			


		return false;
	}


	public override int GetHashCode() => Items.GetHashCode();
}
}
