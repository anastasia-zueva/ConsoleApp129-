using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class BattleTests
    {
        [TestMethod]
        public void GetResults_HeroWins_ReturnsOne()
        {
            // Arrange
            var battle = new Battle();
            battle.SetHeroDamage(10);
            battle.SetEnemyDamage(5);

            // Act
            int result = battle.GetResults();

            // Assert
            Assert.AreEqual(1, result, "Герой должен победить при большем уроне");
        }

        [TestMethod]
        public void GetResults_Draw_ReturnsTwo()
        {
            // Arrange
            var battle = new Battle();
            battle.SetHeroDamage(7);
            battle.SetEnemyDamage(7);

            // Act
            int result = battle.GetResults();

            // Assert
            Assert.AreEqual(2, result, "Должна быть ничья при равном уроне");
        }

        [TestMethod]
        public void GetResults_EnemyWins_ReturnsThree()
        {
            // Arrange
            var battle = new Battle();
            battle.SetHeroDamage(3);
            battle.SetEnemyDamage(8);

            // Act
            int result = battle.GetResults();

            // Assert
            Assert.AreEqual(3, result, "Враг должен победить при большем уроне");
        }

        [TestMethod]
        public void BattleConstructor_CreatesInstance()
        {
            // Arrange & Act & Assert
            var battle = new Battle();
            Assert.IsNotNull(battle, "Объект Battle должен быть создан");
        }
    }
}