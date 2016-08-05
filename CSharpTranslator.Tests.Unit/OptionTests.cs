using System;
using System.IO;
using CSharpTranslator.Tests.Unit.Output;
using NUnit.Framework;

namespace CSharpTranslator.Tests.Unit
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void IsSome_returns_false_for_None()
        {
            var none = Option.None;

            object o;
            Assert.False(none.IsSome(out o));
        }

        [Test]
        public void HasValue_returns_true_for_Some()
        {
            var randomString = Guid.NewGuid().ToString();
            var some = Option.Some(randomString);

            object o;
            Assert.True(some.IsSome(out o));
        }

        //[Test]
        //public void Match_on_Some_allows_access_to_enclosed_value()
        //{
        //    var enclosedValue = Guid.NewGuid().ToString();
        //    var some = Option.Some(enclosedValue);

        //    var actual = some.Match(() => null, s => s);

        //    Assert.That(actual, Is.EqualTo(enclosedValue));
        //}

        //[Test]
        //public void Match_on_None_does_not_allow_access_to_any_value()
        //{
        //    var none = Option.None;

        //    var expected = Guid.NewGuid().ToString();

        //    var actual = none.Match(() => expected, s => s);

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        [Test]
        public void ToString_on_Some_returns_string_representation_of_enclosed_value()
        {
            var value = Guid.NewGuid();
            var expectedResult = value.ToString();
            var option = Option.Some(value);
            
            var actual = option.ToString();

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ToString_on_None_returns_string_representation_of_None()
        {
            var option = Option.None;
            var expectedResult = "<none>";
            var actual = option.ToString();

            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Equals_returns_true_for_None()
        {
            var leftNone = Option.None;
            var rightNone = Option.None;

            Assert.That(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_None_with_different_type_argument()
        {
            var leftNone = Option.None;
            var rightNone = Option.None;

            Assert.False(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_true_for_None_when_compared_with_null()
        {
            var leftNone = Option.None;
            var rightNone = (Option) null;

            Assert.True(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_None_when_compared_with_different_type()
        {
            var none = Option.None;
            var obj = new object();

            Assert.That(none, Is.Not.EqualTo(obj));
        }

        [Test]
        public void Equals_returns_true_for_Some_with_same_enclosed_values()
        {
            var enclosedValue = new object();
            var leftNone = Option.Some(enclosedValue);

            var rightNone = Option.Some(enclosedValue);

            Assert.That(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_Some_with_different_enclosed_values()
        {
            var leftNone = Option.Some(new object());
            var rightNone = Option.Some(new object());

            Assert.False(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_Some_with_different_type_argument()
        {
            var enclosedValue = Guid.NewGuid().ToString();
            var leftNone = Option.Some(enclosedValue);
            var rightNone = Option.Some(enclosedValue);

            Assert.False(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_Some_when_compared_with_null()
        {
            var leftNone = Option.Some(new object());
            var rightNone = (Option) null;

            Assert.False(leftNone.Equals(rightNone));
        }

        [Test]
        public void Equals_returns_false_for_Some_when_compared_with_different_type()
        {
            var none = Option.Some(new object());
            var obj = new object();

            Assert.False(none.Equals(obj));
        }

        [Test]
        public void Value_returns_enclosed_value_for_Some()
        {
            var enclosedValue = new object();
            var option = Option.Some(enclosedValue);

            object value;
            option.IsSome(out value);

            Assert.That(value, Is.EqualTo(enclosedValue));
        }

        [Test]
        public void Value_is_null_for_None()
        {
            var option = Option.None;
            object optionalValue;
            var _ = option.IsSome(out optionalValue);
            Assert.That(optionalValue, Is.Null);
        }

        //[Test]
        //public void New_Option_is_None_for_null_value()
        //{
        //    var nullValue = (object) null;
        //    var actual = new Option(nullValue);

        //    Assert.That(actual, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void Cast_to_Option_is_Some_for_non_null_value()
        //{
        //    var enclosedValue = new object();
        //    Option actual = enclosedValue;

        //    Assert.That(actual.HasValue, Is.True);
        //}

        //[Test]
        //public void Cast_to_Option_is_None_for_null_value()
        //{
        //    var enclosedValue = (object) null;
        //    Option actual = enclosedValue;

        //    Assert.That(actual.HasValue, Is.False);
        //}

        //[Test]
        //public void Select_returns_new_option_with_mapped_value_when_called_on_Some()
        //{
        //    var enclosedValue = Guid.NewGuid().ToString();
        //    var some = new Option(enclosedValue);

        //    var expected = new Option(enclosedValue.Length);
        //    var actual = some.Select(s => s.Length);

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        //[Test]
        //public void Select_returns_None_when_called_on_None()
        //{
        //    var none = new Option(null);

        //    var expected = Option.None;
        //    var actual = none.Select(s => s.Length);

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        //[Test]
        //public void SelectMany_on_None_and_None_returns_None()
        //{
        //    var none = new Option();

        //    var result = from value in none from innerValue in value select innerValue;

        //    Assert.That(result, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void SelectMany_on_Some_and_None_returns_None()
        //{
        //    var enclosedValue = new NestedOptions(new Option());
        //    var option = new Option(enclosedValue);

        //    var result = from value in option from optionalString in value.OptionString select optionalString;

        //    Assert.That(result, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void SelectMany_on_None_and_Some_returns_None()
        //{
        //    var option = new Option();

        //    var result = from nestedOptions in option
        //        from optionalString in nestedOptions.OptionString
        //        select optionalString;

        //    Assert.That(result, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void SelectMany_on_Some_and_Some_returns_Some()
        //{
        //    var enclosedValue = Guid.NewGuid().ToString();
        //    var nestedOption = new NestedOptions(new Option(enclosedValue));
        //    var option = new Option(nestedOption);

        //    var result = from value in option from optionalString in value.OptionString select optionalString;

        //    Assert.That(result.Value, Is.EqualTo(enclosedValue));
        //}

        //public class NestedOptions
        //{
        //    public NestedOptions(Option optionalString)
        //    {
        //        OptionString = optionalString;
        //    }

        //    public Option OptionString { get; private set; }
        //}

        //[Test]
        //public void Where_returns_none_on_None()
        //{
        //    var none = new Option();
        //    var actual = none.Where(_ => true);

        //    Assert.That(actual, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void Where_returns_None_when_inner_value_does_not_meet_predicate()
        //{
        //    var enclosedValue = 0;
        //    var option = new Option(enclosedValue);
        //    var actual = option.Where(i => i > 0);

        //    Assert.That(actual, Is.EqualTo(Option.None));
        //}

        //[Test]
        //public void Where_returns_Some_when_inner_value_meets_predicate()
        //{
        //    var enclosedValue = 0;
        //    var option = new Option(enclosedValue);
        //    var actual = option.Where(i => i == 0);

        //    Assert.That(actual, Is.EqualTo(Option.Some(enclosedValue)));
        //}

        //[Test]
        //public void Iter_has_no_effect_when_called_on_None()
        //{
        //    var i = 0;
        //    var option = Option.None;

        //    option.Iter(value => i += value);

        //    Assert.That(i, Is.EqualTo(0));
        //}

        //[Test]
        //public void Iter_has_side_effect_when_called_on_Some()
        //{
        //    var i = 0;
        //    var option = Option.Some(2);

        //    option.Iter(value => i += value);

        //    Assert.That(i, Is.EqualTo(2));
        //}

        //[Test]
        //public void GetValueOrDefault_when_called_on_Some_returns_inner_value()
        //{
        //    var option = Option.Some(2);
        //    var expected = 2;
        //    var actual = option.GetValueOrDefault();

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        //[Test]
        //public void GetValueOrDefault_when_called_on_None_returns_default_for_type()
        //{
        //    var option = Option.None;
        //    var expected = default(int);
        //    var actual = option.GetValueOrDefault();

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        //[Test]
        //public void GetValueOrDefault_when_called_on_None_returns_default_value_passed_into_method()
        //{
        //    var option = Option.None;
        //    var randomNumber = Random.Next();
        //    var expected = randomNumber;
        //    var actual = option.GetValueOrDefault(randomNumber);

        //    Assert.That(actual, Is.EqualTo(expected));
        //}

        //[Test]
        //public void Switch_executes_action_if_Option_is_Some()
        //{
        //    var actionExecutedFlag = false;
        //    var option = Option.Some(new object());

        //    option.Switch(none: () => { }, some: _ => actionExecutedFlag = true);

        //    Assert.That(actionExecutedFlag, Is.True, "Expected actionExecutedFlag to be true");
        //}

        //[Test]
        //public void Switch_does_not_execute_action_if_Option_is_None()
        //{
        //    var actionExecutedFlag = false;
        //    var option = Option.None;

        //    option.Switch(none: () => actionExecutedFlag = true, some: _ => { });

        //    Assert.That(actionExecutedFlag, Is.True, "Expected actionExecutedFlag to be true");
        //}

        //[Test]
        //public void GetEnumerator_returns_enumerator_with_original_item_when_Option_is_Some()
        //{
        //    var enclosedValue = new object();
        //    var option = Option.Some(enclosedValue);
        //    var enumerator = option.GetEnumerator();

        //    enumerator.MoveNext();

        //    Assert.That(enumerator.Current, Is.SameAs(enclosedValue));
        //}

        //[Test]
        //public void GetEnumerator_returns_enumerator_with_exactly_one_item_when_Option_is_Some()
        //{
        //    var option = Option.Some(new object());
        //    var enumerator = option.GetEnumerator();

        //    enumerator.MoveNext();

        //    Assert.That(enumerator.MoveNext(), Is.False);
        //}

        //[Test]
        //public void GetEnumerator_returns_enumerator_with_no_items_when_Option_is_None()
        //{
        //    var option = Option.None;
        //    var enumerator = option.GetEnumerator();

        //    Assert.That(enumerator.MoveNext(), Is.False);
        //}

        private static readonly Random Random = new Random();
    }
}