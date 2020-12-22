using System;
using static System.Console;
using System.Collections.Generic;

namespace storage
{
    class Program
    {
        static double ToBit(double value, string msg)
        {
            switch (msg)
            {
                case "б":
                    return value *= 8;
                case "байт":
                    return value *= 8;
                case "Кб":
                    value *= 1024; return value *= 8; 
                case "Мб":
                    value *= 1024; value *= 1024; return value *= 8; 
                case "Гб":
                    value *= 1024; value *= 1024; value *= 1024; return value *= 8;
            }
            return value;
        }

        static double FromBit(double value, string msg)
        {
            switch (msg)
            {
                case "б":
                    return value /= 8;
                case "байт":
                    return value /= 8;
                case "Кб":
                    value /= 1024; return value /= 8;
                case "Мб":
                    value /= 1024; value /= 1024; return value /= 8;
                case "Гб":
                    value /= 1024; value /= 1024; value /= 1024; return value /= 8;
            }
            return value;
        }

        static void Main(string[] args)
        {
            List<Storage> storages = new List<Storage>();
            storages.Add(new Flash("MyFlash", "A103mb_0"));
            storages.Add(new OneSideDVD("MySingleDVD", "419569"));
            storages.Add(new TwoSideDVD("MyTwoWayDVD", "48288_8254a"));
            storages.Add(new HDD("MyHDD", "172522"));
            int choice;
            while (true)
            {
                WriteLine("Выберите цифру");
                WriteLine("1.Просмотреть информацию о доступных устройствах");
                WriteLine("2.Скопировать данные на устройства");
                WriteLine("3.Выход из программы\n");
                choice = Convert.ToInt32(ReadLine());
                switch (choice)
                {
                    case 1:
                        foreach (Storage storage in storages)
                        { storage.GetInfo(); }
                        break;
                    case 2:
                        while (true)
                        {
                            WriteLine("Введите, какой объём информации будете копировать(например 102 Гб, 68 Мб, 10 Кб, 8 бит, 1024 б, 2 байта, 5,841 Гб)");
                            var data = ReadLine();
                            WriteLine("\n============================================");
                            string[] mass = data.Split(' ');
                            if (mass[1] != "Гб" && mass[1] != "Мб" && mass[1] != "Кб" && mass[1] != "б" && mass[1] != "байт" && mass[1] != "бит")
                            {
                                WriteLine("Извините, неверно указан тип данных\nОбратите внимание на пример ввода единиц измерения");
                            }
                            else
                            {
                                if (mass.Length == 2)
                                {
                                    try
                                    {
                                        double number = Convert.ToDouble(mass[0]);
                                        int flash = 1, sDVD = 1, twoDVD = 1, hdd = 1;
                                        Flash a = new Flash("MyFlash", "A103mb_0");
                                        OneSideDVD b = new OneSideDVD("MySingleDVD", "419569");
                                        TwoSideDVD c = new TwoSideDVD("MyTwoWayDVD", "48288_8254a");
                                        for (int i = 0; i < storages.Count; i++)
                                        {
                                            if (ToBit(number, mass[1]) > storages[i].Free)
                                            {
                                                if (storages[i].Equals(a))
                                                {
                                                    storages.Insert(i + 1, new Flash($"MyFlash{i}", $"A103mb_0{i}"));
                                                    a.Name = $"MyFlash{i}"; a.Model = $"A103mb_0{i}"; flash++;
                                                }
                                                else if (storages[i].Equals(b))
                                                {
                                                    storages.Insert(i + 1, new OneSideDVD($"MySingleDVD{i}", $"419569{i}"));
                                                    b.Name = $"MySingleDVD{i}"; b.Model = $"419569{i}"; sDVD++;
                                                }
                                                else if(storages[i].Equals(c))
                                                { storages.Insert(i + 1, new TwoSideDVD($"MyTwoWayDVD{i}", $"48288_8254a{i}"));
                                                    c.Name = $"MyTwoWayDVD{i}"; c.Model = $"48288_8254a{i}";
                                                    twoDVD++; }
                                                else
                                                {
                                                    storages.Insert(i + 1, new HDD($"MyHDD{i}", $"172522{i}"));
                                                    c.Name = $"MyHDD{i}"; c.Model = $"172522{i}";
                                                    hdd++;
                                                }

                                                number -= FromBit(storages[i].Free, mass[1]);
                                                storages[i].Copy(FromBit(storages[i].Free, "Гб"), "Гб");
                                            }
                                            else
                                            {
                                                storages[i].Copy(number, mass[1]);
                                                number = Convert.ToDouble(mass[0]);
                                            }
                                        }
                                        WriteLine("\n============================================");
                                        WriteLine($"Для того чтобы скопировать {data} потребовалось:");
                                        WriteLine($"{flash} Flash-памяти объёмом 64 Гб");
                                        WriteLine($"{sDVD} однослойных односторонних DVD-дисков объёмом 4,7 Гб");
                                        WriteLine($"{twoDVD} однослойных двусторонних DVD-дисков объёмом 9 Гб");
                                        WriteLine($"{hdd} съёмных HDD объёмом 128 Гб");
                                        WriteLine("============================================\n");
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        WriteLine("Извините, неверно указан тип данных\nОбратите внимание на пример ввода единиц измерения");
                                        WriteLine(e.Message);
                                    }
                                } else WriteLine("Извините, неверно указан тип данных\nОбратите внимание на пример ввода единиц измерения");
                            }
                        }
                        break;
                    case 3:
                        return;
                    default: WriteLine("Извините, такой команды нет"); 
                        break;
                }
            }
        }
    }
}

