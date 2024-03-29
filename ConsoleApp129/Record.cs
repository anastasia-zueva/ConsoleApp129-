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
        private int _round;

        /// <summary>
        ///  Поле _allEnemys
        ///  всего врагов
        /// </summary>
        private int _allEnemys;

        /// <summary>
        ///  Поле _enemy
        ///  осталось врагов
        /// </summary>
        private int _enemy;

        /// <summary>
        ///  Поле _annoyer
        ///  осталось энноеров
        /// </summary>
        private int _annoyer;

        /// <summary>
        ///  Поле _win
        ///  результат боя
        /// </summary>
        private bool _win;


        /// <summary>
        ///  Конструктор Record
        ///  запись рекорда
        /// </summary>
        public Record(int round, int allEnemys, int enemy, int annoyer, bool win)
        {
            _round = round;
            _allEnemys = allEnemys;
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
        ///  Метод ReturnAllEnemys()
        ///  возвращает количество всех врагов на карте
        /// </summary>
        /// <returns>Количество всех врагов на карте/returns>
        public int ReturnAllEnemys() => _allEnemys;

        /// <summary>
        ///  Метод ReturnEnemy()
        ///  возвращает количество врагов на карте
        /// </summary>
        /// <returns>Количество врагов на карте/returns>
        public int ReturnEnemy() => _enemy;

        /// <summary>
        ///  Метод ReturnAnnoyer()
        ///  возвращает количество энноеров на карте
        /// </summary>
        /// <returns>Количество энноеров на карте/returns>
        public int ReturnAnnoyer() => _annoyer;

        /// <summary>
        ///  Метод ReturnWin()
        ///  возвращает информацию о выигрыше
        /// </summary>
        /// <returns>Результат игры/returns>
        public bool ReturnWin() => _win;
    }
}