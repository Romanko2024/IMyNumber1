namespace TestProject1
{
    public class MyFracTests
    {
        [Fact]
        public void Constructor_ShouldSimplifyFraction()
        {
            var frac = new MyFrac(2, 4);
            Assert.Equal("1/2", frac.ToString());
        }
        [Fact]
        public void CorrectSum()
        {
            var frac1 = new MyFrac(1, 3);
            var frac2 = new MyFrac(1, 6);
            var result = frac1.Add(frac2);
            Assert.Equal("1/2", result.ToString());
        }
        [Fact]
        public void CorrectDifference()
        {
            var frac1 = new MyFrac(1, 2);
            var frac2 = new MyFrac(1, 3);
            var result = frac1.Subtract(frac2);
            Assert.Equal("1/6", result.ToString());
        }
    }
}