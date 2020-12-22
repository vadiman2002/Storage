using System;
using static System.Console;
using System.Collections.Generic;

namespace storage
{
    abstract class Storage
    {
        public string Name { get; set; }
        public string Model { get; set; }
        //Пока для пробного варианта
        public double Free { get; set; }
        protected double Speed { get; set; }
        public Storage(string name, string model)
        {
            Name = name; Model = model;
        }
        protected abstract void Memory();
        //передаём объём копируемых файлов и единицы измерения
        public abstract void Copy(double value, string name);
        protected abstract void FreeMemory();
        public abstract void GetInfo();
        protected void Line()
        {
            WriteLine("============================================\n");
        }
        public abstract override bool Equals(Object obj);

        public abstract override int GetHashCode();
    }

}