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
    }
}