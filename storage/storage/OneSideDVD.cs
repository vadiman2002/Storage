using System;
using static System.Console;
using System.Collections.Generic;

namespace storage
{
    class OneSideDVD : DVD
    {
        public OneSideDVD(string name, string model) : base(name, model)
        {
           Free = 40372692582.4;//40 372 692 582.4
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                OneSideDVD p = (OneSideDVD)obj;
                return (Name == p.Name) && (Speed == p.Speed) && (Model == p.Model) && (Free == p.Free);
            }
        }
        public override int GetHashCode()
        {
            return Tuple.Create(Name, Speed, Model, Free).GetHashCode();
        }
        protected override void Memory()
        {
            WriteLine("Общий объём: 4.7 Гб");
        }
        public override void Copy(double value, string name)
        {
            switch (name)
            {
                case "бит":
                    Free -= value;
                    break;
                case "б":
                    value *= 8;
                    Free -= value;
                    break;
                case "байт":
                    value *= 8;
                    Free -= value;
                    break;
                case "Кб":
                    value *= 8; value *= 1024;
                    Free -= value;
                    break;
                case "Мб":
                    value *= 8; value *= 1024; value *= 1024;
                    Free -= value;
                    break;
                case "Гб":
                    value *= 8; value *= 1024; value *= 1024; value *= 1024;
                    Free -= value;
                    break;
            }
            Write($"\tОднослойный односторонний\nСкопировано удачно!\nВремя: ");
            if (value / Speed < 60)
                WriteLine(value / Speed + " сек");
            else if (value / Speed < 3600) WriteLine(value / Speed / 60 + " мин");
            else WriteLine(value / Speed / 3600 + " часов");
            FreeMemory();
            Line();
        }

        protected override void FreeMemory()
        {
            Free /= 8;
            Free /= 1024;
            Free /= 1024;
            Free /= 1024;
            WriteLine($"Свободное место на диске: {Free} Гб");
            Free *= 1024;
            Free *= 1024;
            Free *= 1024;
            Free *= 8;
        }

        public override void GetInfo()
        {
            WriteLine("Название устройства: " + Name);
            WriteLine("Модель устройства: " + Model + " однослойный односторонний");
            Memory();
            FreeMemory();
            WriteLine("Скорость чтения/записи: 21,09 Мб/сек");
            Line();
        }
    }
}