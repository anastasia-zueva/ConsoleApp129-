using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ConsoleApp129
{
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
        ///  Поле _killedEnemy
        ///  убитых врагов
        /// </summary>
        private int _killedEnemy;

        /// <summary>
        ///  Поле _win
        ///  результат боя
        /// </summary>
        private bool _win;

        /// <summary>
        /// Поле _file
        /// путь к файлу сохранения
        /// </summary>
        static private readonly string _file = "save2.txt";

        public Record(int round, int allEnemys, int killedEnemy, bool win)
        {
            _round = round;
            _allEnemys = allEnemys;
            _killedEnemy = killedEnemy;
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
        public int ReturnEnemy() => _killedEnemy;

        /// <summary>
        ///  Метод ReturnWin()
        ///  возвращает информацию о выигрыше
        /// </summary>
        /// <returns>Результат игры/returns>
        public bool ReturnWin() => _win;

        /// <summary>
        ///  Метод Serialize()
        ///  создает сохранение рекордов
        /// </summary>
        /// <param name="records">Список рекордов</param>
        static public void Serialize(List<Record> records)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(_file, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, records);
            stream.Close();
        }

        /// <summary>
        ///  Метод DeSerialize()
        ///  загружает сохранение рекордов
        /// </summary>
        /// <returns>Загруженная со сохранения таблица рекордов/returns>
        static public List<Record> DeSerialize()
        {
            List<Record> rec;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(_file, FileMode.Open, FileAccess.Read);
                rec = (List <Record>)formatter.Deserialize(stream);
                stream.Close();
                return rec;
            }
            catch
            {
                throw new MyException("Нет рекордов!");
            }
        }
    }
}