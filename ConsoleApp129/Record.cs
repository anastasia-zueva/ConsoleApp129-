using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Record
    ///  записанные рекорды
    /// </summary>
    [Serializable]
    class Record
    {
        /// <summary>
        ///  Поле _round
        ///  раунд
        /// </summary>
        private readonly int _round;

        /// <summary>
        ///  Поле _allEnemies
        ///  всего врагов
        /// </summary>
        private readonly int _allEnemies;

        /// <summary>
        ///  Поле _enemy
        ///  осталось врагов
        /// </summary>
        private readonly int _enemy;

        /// <summary>
        ///  Поле _annoyer
        ///  осталось особых врагов
        /// </summary>
        private readonly int _annoyer;

        /// <summary>
        ///  Поле _win
        ///  результат боя
        /// </summary>
        private readonly bool _win;


        /// <summary>
        ///  Конструктор Record
        ///  запись рекорда
        /// </summary>
        public Record(int round, int allEnemies, int enemy, int annoyer, bool win)
        {
            _round = round;
            _allEnemies = allEnemies;
            _enemy = enemy;
            _annoyer = annoyer;
            _win = win;
        }


        /// <summary>
        ///  Метод ReturnRound()
        ///  возвращает номер раунда
        /// </summary>
        /// <returns>Номер раунда/returns>
        public int ReturnRound() => _round;

        /// <summary>
        ///  Метод ReturnAllEnemies()
        ///  возвращает количество всех врагов на карте
        /// </summary>
        /// <returns>Количество всех врагов на карте/returns>
        public int ReturnAllEnemies() => _allEnemies;

        /// <summary>
        ///  Метод ReturnEnemy()
        ///  возвращает количество врагов на карте
        /// </summary>
        /// <returns>Количество врагов на карте/returns>
        public int ReturnEnemy() => _enemy;

        /// <summary>
        ///  Метод ReturnAnnoyer()
        ///  возвращает количество особых врагов на карте
        /// </summary>
        /// <returns>Количество особых врагов на карте/returns>
        public int ReturnAnnoyer() => _annoyer;

        /// <summary>
        ///  Метод ReturnWin()
        ///  возвращает информацию о выигрыше
        /// </summary>
        /// <returns>Результат игры/returns>
        public bool ReturnWin() => _win;
    }
}