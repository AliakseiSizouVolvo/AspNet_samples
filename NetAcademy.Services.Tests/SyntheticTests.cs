namespace NetAcademy.Services.Tests;

public class SyntheticTests
{
    private bool IsLeapYear(int year) => year % 4 == 0; 
    //[Fact]
    //public void IsLeapYear_Works()
    //{
    //    for (int year = 0; year <= 10000; year++)
    //    {
    //        Assert.Equal(IsLeapYear(year),
    //            (year % 4 == 0 && year % 100 != 0) || year % 400 == 0);
    //    }
    //}

    [Theory]
    [InlineData(2000, true)]
    [InlineData(2001, false)]
    [InlineData(2002, false)]
    [InlineData(2003, false)]
    [InlineData(2004, true)]
    public void IsLeapYear_Works1(int year, bool expectedResult)
    {
        Assert.Equal(IsLeapYear(year), expectedResult);
    }
}