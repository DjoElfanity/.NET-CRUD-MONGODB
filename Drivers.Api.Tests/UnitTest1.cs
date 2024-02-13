namespace Drivers.Api.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
          var instance = new MaClasse();

            // Act
            var resultat = instance.MaFonction();

            // Assert
            Assert.True(resultat);

    }
}