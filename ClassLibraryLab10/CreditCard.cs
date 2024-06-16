using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class CreditCard : BankCard
    {
        //Поля
        private int limit;
        private int maturitydate;

        //Свойства
        public int Limit
        {
            get
            {
                return limit;
            }
            set
            {
                if (value > 0)
                {
                    limit = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите допустимый баланс");
                }
            }
        }
        public int Maturitydate
        {
            get
            {
                return maturitydate;
            }
            set
            {
                if (value > 0)
                {
                    maturitydate = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите допустимый баланс");
                }
            }
        }

        //Конструктор без параметров
        public CreditCard() : base()
        {
            Limit = 120000;
            Maturitydate = 2;
        }

        //Конструктор с параметрами
        public CreditCard(string number, string owner, DateTime duration, int limit, int maturitydate) : base(number, owner, duration)
        {
            Limit = limit;
            Maturitydate = maturitydate;
        }

        //Преобразование в строку
        public override string ToString()
        {
            return $"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()}\nЛимит: {Limit} руб.\nСрок погашения: {Maturitydate} мес.\n";
        }

        //Вывод информации на экран при помощи обычного метода
        public new void Show()
        {
            //return $"Широта: {Latitude} Долгота: {Longtitude}";
            Console.WriteLine($"Кредитная карта:");
            base.Show();
            Console.WriteLine($"Лимит: {Limit} руб.\nСрок погашения: {Maturitydate} мес.\n");
        }

        //Вывод информации на экран при помощи виртуального метода
        public override void ShowVirtual()
        {
            Console.WriteLine($"Кредитная карта:");
            base.Show();
            Console.WriteLine($"Лимит: {Limit} руб.\nСрок погашения: {Maturitydate} мес.\n");
        }

        //Ввод информации об экземплярах класса с клавиатуры
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите лимит кредита:");
            Limit = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите срок погашения (в мес.):");
            Maturitydate = int.Parse(Console.ReadLine());
        }

        //Ввод информации об экземплярах класса при помощи ДСЧ
        public override void RandomInit()
        {
            base.RandomInit();
            Limit = random.Next(1, 7000001);
            Maturitydate = random.Next(1, 4);
        }

        public override bool Equals(object obj)
        {
            CreditCard card = obj as CreditCard;
            if (card != null)
                return this.Number == card.Number && this.Owner == card.Owner && this.Duration == card.Duration && card.Limit == this.Limit && card.Maturitydate == this.Maturitydate;
            else
                return false;
        }
    }
}
