using System;
using static System.Console;
using System.Collections.Generic;

namespace storage
{
    class Flash : Storage
    {
        public Flash(string name, string model) : base(name, model)
        {
            Free = 549755813888;//549 755 813 888 бит(объём)
            Speed = 42949672960;//42 949 672 960 Бит(скорость)
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
                Flash p = (Flash)obj;
                return (Name == p.Name) && (Speed == p.Speed) && (Model == p.Model) && (Free==p.Free);
            }
        }
        public override int GetHashCode()
        {
            return Tuple.Create(Name, Speed, Model, Free).GetHashCode();
        }
        protected override void Memory()
        {
            WriteLine("Общий объём: 64 Гб");
        }
        public override void Copy(double value, string name)
        {
            switch(name)
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
            Write($"\t\tFlash-память\nСкопировано удачно!\nВремя: ");
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
            WriteLine("\nНазвание устройства: " + Name);
            WriteLine("Модель устройства: " + Model);
            Memory();
            FreeMemory();
            WriteLine("Скорость интерфейса: 5 Гб/сек");
            Line();
        }
    }
}