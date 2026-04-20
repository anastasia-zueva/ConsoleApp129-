using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Serialize
    ///  записывает сохранения
    /// </summary>
    [Serializable]
    static internal class Serialize
    {
        /// <summary>
        ///  Метод SerializeRecords()
        ///  создает сохранение рекордов
        /// </summary>
        /// <param name="records">Список рекордов</param>
        static public void SerializeRecords(List<Record> records)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("save2.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, records);
            stream.Close();
        }

        /// <summary>
        ///  Метод SerializeMap()
        ///  создает сохранение последней сыгранной игры
        /// </summary>
        /// <param name="_map">Игровая карта</param>
        static public void SerializeMap(Map _map)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("save1.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, _map);
            stream.Close();
        }
    }

    /// <summary>
    ///  Класс DeSerialize
    ///  считывает сохранения
    /// </summary>
    [Serializable]
    static internal class DeSerialize
    {
        /// <summary>
        ///  Метод DeSerializeRecords()
        ///  загружает сохранение рекордов
        /// </summary>
        /// <returns>Загруженная со сохранения таблица рекордов/returns>
        static public List<Record> DeSerializeRecords()
        {
            List<Record> rec;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("save2.txt", FileMode.Open, FileAccess.Read);
                rec = (List<Record>)formatter.Deserialize(stream);
                stream.Close();
                return rec;
            }
            catch
            {
                throw new MyException("Нет рекордов!");
            }
        }

        /// <summary>
        ///  Метод DeSerializeMap()
        ///  загружает сохранение последней сыгранной игры
        /// </summary>
        /// <returns>Загруженная со сохранения игровая карта</returns>
        static public Map DeSerializeMap()
        {
            Map _map;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("save1.txt", FileMode.Open, FileAccess.Read);
                _map = (Map)formatter.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                throw new MyException("Ошибка: не удалось загрузить сохранение!");
            }
            return _map;
        }
    }
}
