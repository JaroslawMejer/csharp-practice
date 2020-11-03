using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage; // LOG CAN HAVE MULTIPLE METHODS AND WHEN ENVOKING IT, WE ARE LAUNCHING ALL ASSIGNED METHODS
            log += ReturnMessage;

            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message) // Methods added to delegate HAS TO HAVE THE SAME BUILD TYPE, in this case string
        {
            count++;
            return message;
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void StringsBehavesLikeValueTypes() // Strings are immutable, the whole value needs to be changed
        {
            string name = "Jaroslaw";
            name = MakeUppercase(name);

            Assert.Equal("JAROSLAW", name);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef() // proper way of changing value in object via reference
        {
            var book1 = GetBook("Book 1");
            GetBookSetNameByRef(ref book1, "New name");

            Assert.Equal("New name", book1.Name);

        }

        private void GetBookSetNameByRef(ref Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CSharpIsPassByValue() // wrong way of changing value in object via reference
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New name");

            Assert.Equal("Book 1", book1.Name);

        }

        private void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New name");

            Assert.Equal("New name", book1.Name);

        }

        private void SetName(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferanceSameObject() //they can
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}
