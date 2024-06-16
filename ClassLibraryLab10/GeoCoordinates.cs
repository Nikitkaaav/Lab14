using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab9
{
    public class IdNumber
    {
        public int number;
        public IdNumber(int number)
        {
            this.number = number;
        }
        public override string ToString()
        {
            return number.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is IdNumber n)
                return this.number == n.number;
            return false;
        }
    }
    public class GeoCoordinates : IInit, ICloneable
    {
        //Поля
        private double latitude;
        private double longtitude;
        private static int count;
        public IdNumber id;

        //Свойства
        public double Latitude
        {
            get
            {
                return this.latitude;
            }
            set
            {
                if (Math.Round(value, 4) == value)
                {
                    this.latitude = value;
                }
                else
                {
                    throw new ArgumentException("В значении широты должно быть ровно 4 знака после запятой.");
                }
            }
        }

        public double Longtitude
        {
            get
            {
                return this.longtitude;
            }
            set
            {
                if (Math.Round(value, 4) == value)
                {
                    this.longtitude = value;
                }
                else
                {
                    throw new ArgumentException("В значении долготы должно быть ровно 4 знака после запятой.");
                }
            }
        }

        //Конструктор без параметров
        public GeoCoordinates()
        {
            Latitude = -78.4875;
            Longtitude = 45.4945;
            count++;
            id = new IdNumber(1);
        }

        //Конструктор с параметрами
        public GeoCoordinates(double latitude, double longtitude, int number)
        {
            this.Latitude = latitude;
            this.Longtitude = longtitude;
            id = new IdNumber(number);
            count++;
        }

        //Конструктор копирования
        public GeoCoordinates(GeoCoordinates a)
        {
            this.Latitude = a.latitude;
            this.Longtitude = a.longtitude;
            count++;
        }

        //Преобразование в строку
        public override string ToString()
        {
            return $"{id} : Широта: {Latitude} Долгота: {Longtitude}";
        }

        //Статическая функция
        public static double FindDistance(GeoCoordinates gc1, GeoCoordinates gc2)
        {
            double radLat1 = DegreesToRadians(gc1.latitude);
            double radLon1 = DegreesToRadians(gc1.longtitude);
            double radLat2 = DegreesToRadians(gc2.latitude);
            double radLon2 = DegreesToRadians(gc2.longtitude);

            double dLat = radLat2 - radLat1;
            double dLon = radLon2 - radLon1;

            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = 6371 * c;

            distance = Math.Round(distance, 3);

            return distance;
        }

        //Метод класса
        public double FindDistance(GeoCoordinates gc)
        {
            double radLat1 = DegreesToRadians(Latitude);
            double radLon1 = DegreesToRadians(Longtitude);
            double radLat2 = DegreesToRadians(gc.Latitude);
            double radLon2 = DegreesToRadians(gc.Longtitude);

            double dLat = radLat2 - radLat1;
            double dLon = radLon2 - radLon1;

            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = 6371 * c;

            distance = Math.Round(distance, 3);

            return distance;
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        //Счетчик объектов
        public static int GetCount()
        {
            return count;
        }

        // Унарные операции
        public static GeoCoordinates operator ++(GeoCoordinates gc)
        {
            gc.latitude += 0.01;
            gc.longtitude += 0.01;
            return gc;
        }

        // Операции приведения типов
        public static explicit operator bool(GeoCoordinates gc)
        {
            return gc.latitude == 0;
        }

        // Бинарные операции
        public static bool operator ==(GeoCoordinates gc1, GeoCoordinates gc2)
        {
            return gc1.latitude == gc2.latitude;
        }

        public static bool operator !=(GeoCoordinates gc1, GeoCoordinates gc2)
        {
            return gc1.longtitude != gc2.longtitude;
        }

        //Ввод информации об экземплярах класса с клавиатуры
        public virtual void Init()
        {
            Console.WriteLine("Введите id:");
            id.number = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите широту:");
            Latitude = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите данные владельца карты:");
            Longtitude = int.Parse(Console.ReadLine());
        }

        //Ввод информации об экземплярах класса при помощи ДСЧ
        public virtual void RandomInit()
        {
            Random random = new Random();
            Latitude = random.Next(-99, 100);
            Longtitude = random.Next(-99, 100);
            id.number = random.Next(1,100);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not GeoCoordinates) return false;
            return ((GeoCoordinates)obj).Latitude == this.Latitude && ((GeoCoordinates)obj).Longtitude == this.Longtitude;
        }

        public object Clone()
        {
            return new GeoCoordinates(Latitude, Longtitude, id.number);
        }
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }

}