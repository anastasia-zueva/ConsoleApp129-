using System;
using System.Timers;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Menu
    ///  карта, на которой происходят все события игры
    /// </summary>
    [Serializable]
    public class Map
    {
        /// <summary>
        /// Поле _time
        /// количество времени до нового раунда
        /// </summary>
        private int _time = 0;

        /// <summary>
        /// Поле _time
        /// количество времени до спавна липкого поля
        /// </summary>
        private int _timeSticky = 0;

        /// <summary>
        /// Поле _round
        /// количество сыгранных раундов
        /// </summary>
        private int _round = 0;

        /// <summary>
        /// Поле MapObj
        /// игровая карта, представленная в виде двумерного массива
        /// </summary>
        public MapObject[,] MapObj;

        /// <summary>
        /// Поле _loss
        /// является переменной отслеживания проигрыша
        /// </summary>
        private bool _loss = false;

        /// <summary>
        /// Поле End
        /// является переменной отслеживания завершения игры
        /// </summary>
        public bool End = false;

        /// <summary>
        /// Поле I
        /// номер строки в массиве игровой карты, на которой находится герой
        /// </summary>
        public int I;

        /// <summary>
        /// Поле J
        /// номер ряда в массиве игровой карты, на котором находится герой
        /// </summary>
        public int J;

        /// <summary>
        /// Поле _enemyCount
        /// количество врагов на карте
        /// </summary>
        private int _enemyCount = 0;

        /// <summary>
        /// Поле _enemyCount
        /// количество всех врагов, которые были заспавнены
        /// </summary>
        private int _allEnemys = 0;

        /// <summary>
        /// Поле _annoyersCount
        /// количество надоедливых врагов на карте
        /// </summary>
        private int _annoyerCount = 0;

        /// <summary>
        /// Поле _berserk
        /// наличие навыка обезвреживания врагов без боя
        /// </summary>
        private bool _berserk;



        /// <summary>
        ///  Конструктор Map()
        ///  задает размер карты
        /// </summary>
        /// <param name="mapSize">Размер карты</param>
        /// <param name="Time">Счётчик времени</param>
        public Map(int mapSize)
        {
            MapObj = new MapObject[mapSize, mapSize];
            I = MapObj.GetLength(0) / 2;
            J = MapObj.GetLength(1) / 2;
            ResetTime();
        }

        /// <summary>
        ///  Метод ResetTime()
        ///  обнуляет таймер для отсчета времени до нового раунда
        /// </summary>
        /// <param name="Time">Игровая карта</param>
        public void ResetTime()
        {
            _round++;
            _time = 40;
            _berserk = true;
        }

        /// <summary>
        ///  Метод Timer_Tick()
        ///  каждую секунду отнимает единицу от времени до нового раунда
        /// </summary>
        public void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            _time--;
            _timeSticky--;
        }



        /// <summary>
        ///  Метод SetStickyTimy()
        ///  присваивает время до спавна липкого поля
        /// </summary>
        /// <param name="time">количество секунд</param>
        public void SetStickyTimy(int time) => _timeSticky = time;

        /// <summary>
        ///  Метод SetLoss()
        ///  присваивает проигрыш
        /// </summary>
        public void SetLoss() => _loss = true;

        /// <summary>
        ///  Метод SetEnemyCount()
        ///  присваивает количество врагов
        /// </summary>
        /// <param name="count">количество врагов</param>
        public void SetEnemyCount(int count = -1) => _enemyCount += count;

        /// <summary>
        ///  Метод SetAnnoyerCount()
        ///  присваивает количество энноеров
        /// </summary>
        /// <param name="count">количество энноеров</param>
        public void SetAnnoyerCount(int count = -1) => _annoyerCount += count;

        /// <summary>
        ///  Метод SetAllEnemyCount()
        ///  присваивает количество врагов
        /// </summary>
        /// <param name="count">количество всех врагов</param>
        public void SetAllEnemyCount(int count = 1) => _allEnemys += count;

        /// <summary>
        ///  Метод SetBerserk()
        ///  изменяет состояние берсерка
        /// </summary>
        /// <param name="TF">состояние берсерка</param>
        public void SetBerserk(bool TF = false) => _berserk = TF;



        /// <summary>
        ///  Метод ReturnEnemyCount()
        ///  возвращает количество врагов на карте
        /// </summary>
        /// <returns>Количество врагов на карте/returns>
        public int ReturnEnemyCount() => _enemyCount;

        /// <summary>
        ///  Метод ReturnAnnoyerCount()
        ///  возвращает количество энноеров на карте
        /// </summary>
        /// <returns>Количество энноеров на карте/returns>
        public int ReturnAnnoyerCount() => _annoyerCount;

        /// <summary>
        ///  Метод ReturnAllEnemys()
        ///  возвращает количество всех врагов на карте
        /// </summary>
        /// <returns>Количество всех врагов на карте/returns>
        public int ReturnAllEnemys() => _allEnemys;

        /// <summary>
        ///  Метод ReturnTime()
        ///  возвращает оставшееся время в секундах до нового раунда
        /// </summary>
        /// <returns>Оставшееся время в секундах/returns>
        public int ReturnTime() => _time;

        /// <summary>
        ///  Метод ReturnStickyTime()
        ///  возвращает оставшееся время в секундах до спавна липкого поля
        /// </summary>
        /// <returns>Оставшееся время в секундах/returns>
        public int ReturnStickyTime() => _timeSticky;

        /// <summary>
        ///  Метод ReturnRound()
        ///  возвращает номер раунда
        /// </summary>
        /// <returns>Номер раунда/returns>
        public int ReturnRound() => _round;

        /// <summary>
        ///  Метод ReturnBerserk()
        ///  возвращает наличие навыка обезвреживания врагов
        /// </summary>
        /// <returns>Наличие навыка обезвреживания врагов/returns>
        public bool ReturnBerserk() => _berserk;

        /// <summary>
        ///  Метод ReturnLoss()
        ///  возвращает состояние игры
        /// </summary>
        /// <returns>Состояние игры/returns>
        public bool ReturnLoss() => _loss;
    }
}