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
        [Fact]
        public void CorrectProduct()
        {
            var frac1 = new MyFrac(1, 3);
            var frac2 = new MyFrac(1, 6);
            var result = frac1.Multiply(frac2);
            Assert.Equal("1/18", result.ToString());
        }
        [Fact]
        public void CorrectQuotient()
        {
            var frac1 = new MyFrac(1, 3);
            var frac2 = new MyFrac(1, 6);
            var result = frac1.Divide(frac2);
            Assert.Equal("2/1", result.ToString());
        }
        [Fact]
        public void Divide_ByZero_Exception()
        {
            var frac1 = new MyFrac(1, 3);
            var frac2 = new MyFrac(0, 1);
            Assert.Throws<DivideByZeroException>(() => frac1.Divide(frac2));
        }
        [Fact]
        public void CorrectCompareTo()
        {
            var frac1 = new MyFrac(1, 2);
            var frac2 = new MyFrac(1, 3);
            Assert.True(frac1.CompareTo(frac2) > 0);
        }
    }
    public class MyComplexTests
    {
        [Fact]
        public void ShouldInitializeCorrectly()
        {
            var complex = new MyComplex(3, 4);
            Assert.Equal("3+4i", complex.ToString());
        }
        [Fact]
        public void ComplexCorrectSum()
        {
            var complex1 = new MyComplex(1, 2);
            var complex2 = new MyComplex(3, 4);
            var result = complex1.Add(complex2);
            Assert.Equal("4+6i", result.ToString());
        }
        [Fact]
        public void ComplexCorrectDifference()
        {
            var complex1 = new MyComplex(5, 6);
            var complex2 = new MyComplex(3, 4);
            var result = complex1.Subtract(complex2);
            Assert.Equal("2+2i", result.ToString());
        }
        [Fact]
        public void ComplexCorrectProduct()
        {
            var complex1 = new MyComplex(1, 2);
            var complex2 = new MyComplex(3, 4);
            var result = complex1.Multiply(complex2);
            Assert.Equal("-5+10i", result.ToString());
        }
        [Fact]
        public void ComplexCorrectQuotient()
        {
            var complex1 = new MyComplex(1, 2);
            var complex2 = new MyComplex(3, 4);
            var result = complex1.Divide(complex2);
            Assert.Equal("0,44+0,08i", result.ToString());
        }
        [Fact]
        public void Complex_Divide_ByZero_ThrowException()
        {
            var complex1 = new MyComplex(1, 2);
            var complex2 = new MyComplex(0, 0);
            Assert.Throws<DivideByZeroException>(() => complex1.Divide(complex2));
        }
    }
}
