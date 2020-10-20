using System.Collections.Generic;
using SP.Contract.Domains.Common;
using Xunit;

namespace SP.Contract.Domain.UnitTest.Common
{
    public class ValueObjectTests
    {
        [Fact]
        public void Equals_GivenDifferentValues_ShouldReturnFalse()
        {
            var point1 = new Piople("stec.superuser@mail.ru", "test", "test");
            var point2 = new Piople("stec2.superuser@mail.ru", "test2", "test2");

            Assert.False(point1.Equals(point2));
        }

        [Fact]
        public void Equals_GivenMatchingValues_ShouldReturnTrue()
        {
            var point1 = new Piople("stec.superuser@mail.ru", "test", "test");
            var point2 = new Piople("stec.superuser@mail.ru", "test", "test");

            Assert.True(point1.Equals(point2));
        }

        private class Piople : ValueObject
        {
            public string Login { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            private Piople()
            {
            }

            public Piople(string login, string firstName, string lastName)
            {
                Login = login;
                FirstName = firstName;
                LastName = lastName;
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Login;
                yield return FirstName;
                yield return LastName;
            }
        }
    }
}
