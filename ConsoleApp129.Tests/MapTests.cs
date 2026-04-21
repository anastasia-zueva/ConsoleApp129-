using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129;
using System;
using System.Timers;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void Constructor_InitializesMapWithCorrectSize()
        {
            // Arrange
            int mapSize = 10;

            // Act
            var map = new Map(mapSize);

            // Assert
            Assert.IsNotNull(map.MapObj, "Карта должна быть инициализирована");
            Assert.AreEqual(mapSize, map.MapObj.GetLength(0), "Размер карты по первой оси неверен");
            Assert.AreEqual(mapSize, map.MapObj.GetLength(1), "Размер карты по второй оси неверен");
        }

        [TestMethod]
        public void Constructor_SetsHeroPositionToCenter()
        {
            // Arrange
            int mapSize = 10;

            // Act
            var map = new Map(mapSize);

            // Assert
            Assert.AreEqual(5, map.I, "Герой должен быть в центре по оси I");
            Assert.AreEqual(5, map.J, "Герой должен быть в центре по оси J");
        }

        [TestMethod]
        public void ResetTime_IncrementsRoundAndSetsTime()
        {
            // Arrange
            var map = new Map(10);
            int initialRound = map.ReturnRound();

            // Act
            map.ResetTime();

            // Assert
            Assert.AreEqual(initialRound + 1, map.ReturnRound(), "Раунд должен увеличиться на 1");
            Assert.AreEqual(40, map.ReturnTime(), "Время должно быть установлено в 40");
            Assert.IsTrue(map.ReturnBerserk(), "Берсерк должен быть активен");
        }

        [TestMethod]
        public void Timer_Tick_DecrementsTime()
        {
            // Arrange
            var map = new Map(10);
            map.ResetTime();
            int initialTime = map.ReturnTime();
            var timer = new System.Timers.Timer();

            // Act - передаем null для ElapsedEventArgs, так как метод не использует его
            map.Timer_Tick(timer, null!);

            // Assert
            Assert.AreEqual(initialTime - 1, map.ReturnTime(), "Время должно уменьшиться на 1");
        }

        [TestMethod]
        public void SetStickyTime_SetsStickyTimeValue()
        {
            // Arrange
            var map = new Map(10);
            int stickyTime = 25;

            // Act
            map.SetStickyTime(stickyTime);

            // Assert
            Assert.AreEqual(stickyTime, map.ReturnStickyTime(), "Время липкого поля должно совпадать");
        }

        [TestMethod]
        public void SetEnemyCount_IncreasesEnemyCount()
        {
            // Arrange
            var map = new Map(10);
            int initialCount = map.ReturnEnemyCount();

            // Act
            map.SetEnemyCount(3);

            // Assert
            Assert.AreEqual(initialCount + 3, map.ReturnEnemyCount(), "Количество врагов должно увеличиться на 3");
        }

        [TestMethod]
        public void SetEnemyCount_DecreasesEnemyCount()
        {
            // Arrange
            var map = new Map(10);
            map.SetEnemyCount(5);
            int currentCount = map.ReturnEnemyCount();

            // Act
            map.SetEnemyCount(-2);

            // Assert
            Assert.AreEqual(currentCount - 2, map.ReturnEnemyCount(), "Количество врагов должно уменьшиться на 2");
        }

        [TestMethod]
        public void SetLoss_SetsLossToTrue()
        {
            // Arrange
            var map = new Map(10);

            // Act
            map.SetLoss();

            // Assert
            Assert.IsTrue(map.ReturnLoss(), "Проигрыш должен быть установлен в true");
        }

        [TestMethod]
        public void SetBerserk_ChangesBerserkState()
        {
            // Arrange
            var map = new Map(10);
            map.ResetTime(); // Устанавливаем berserk = true

            // Act
            map.SetBerserk(false);

            // Assert
            Assert.IsFalse(map.ReturnBerserk(), "Берсерк должен быть выключен");
        }
    }
}