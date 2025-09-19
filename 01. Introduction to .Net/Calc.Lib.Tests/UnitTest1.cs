namespace Calc.Lib.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var x = new Calculator();
            var result = x.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }
        [Test]
        public void Test2()
        {
            var x = new Calculator();
            var result = x.Add(1, -2);
            Assert.That(result, Is.EqualTo(-1));
        }
    }
}