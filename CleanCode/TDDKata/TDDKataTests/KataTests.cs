using System.Reflection.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDKata;

namespace TDDKataTests
{
    [TestClass]
    public class KataTests
    {
        [TestMethod]
        public void Greet_ShouldReturn_HelloName()
        {
            var kata = new Kata();
            var name = "Bert";
            var expected = $"Hello, {name}.";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Greet_ShouldReturn_HelloMyFriend()
        {
            var kata = new Kata();
            string name = null;
            var expected = "Hello, my friend.";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Greet_ShouldReturn_UppercaseGreeting()
        {
            var kata = new Kata();
            string name = "BERT";
            var expected = $"HELLO, {name}!";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetArray_ShouldReturn_HelloNameAndName()
        {
            var kata = new Kata();
            var name = new[] {"Bert","John"};
            var expected = $"Hello, {name[0]} and {name[1]}.";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetArray_ShouldReturn_HelloForEach()
        {
            var kata = new Kata();
            var name = new[] { "Bert", "John", "Email", "Daniel" };
            var expected = $"Hello, {name[0]}, {name[1]}, {name[2]}, and {name[3]}.";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetArray_ShouldReturn_HelloForEach_AndUpperCaseForUpperCase()
        {
            var kata = new Kata();
            var name = new[] { "Bert", "John", "Daniel", "EMAIL" };
            var expected = $"Hello, {name[0]}, {name[1]}, and {name[2]}. AND HELLO EMAIL!";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetArray_ShouldReturn_HelloForEach_AndSplitOnCommas()
        {
            var kata = new Kata();
            var name = new[] { "Bert", "John", "Daniel, Email", };
            var expected = $"Hello, {name[0]}, {name[1]}, Daniel, and Email.";

            var actual = kata.Greet(name);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetArray_ShouldReturn_LastValueOnRepeat()
        {
            //AC: Om jag har h�lsat p� [�a�, �b�, �c�].
            //N�r jag skickar in �repeat�
            //s� repeteras den senaste h�lsningen och svaret blir samma som f�rra g�ngen.

            var kata = new Kata();
            var name = new[] { "Bert", "John", "Daniel", "Email" };
            var repeat = "repeat";

            var expected = $"Hello, {name[0]}, {name[1]}, Daniel, and Email.";
            var firstCallResult = kata.Greet(name);
            var repeatedCallResult = kata.Greet(repeat);

            Assert.AreEqual(expected, firstCallResult);
            Assert.AreEqual(expected, repeatedCallResult);
        }

        [TestMethod]
        public void SaveFavoriteGreeting_ShouldSave()
        {
        
            //AC: Om man skickar in  (1, [�a�, �b�, �c�]) s� l�ggs den till i favoriter p� plats 1.

            var kata = new Kata();
            var name = new[] { "Bert", "John", "Daniel", "Email" };
            var id = 1;
            var expected = true;

            var actual = kata.SaveFavoriteGreeting(id, name);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GreetInt_ShouldReturn_GreetingWithId()
        {
            //AC: Om vi har [�a�, �b�] sparat p� plats ett, s� om skickar in siffran 1, s� h�lsas a och b p�.
        
            var kata = new Kata();
            var name = new[] { "Bert", "John", "Daniel", "Email" };
            kata.SaveFavoriteGreeting(1, name);
            var expected = $"Hello, {name[0]}, {name[1]}, Daniel, and Email.";

            var savedCallResult = kata.Greet(1);

            Assert.AreEqual(expected, savedCallResult);
        }

       
    }
}
