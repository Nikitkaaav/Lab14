using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class YouthCard : BankCard
    {
        //Поле
        private int cashback;

        //Свойство
        public int Cashback
        {
            get
            {
                return cashback;
            }
            set
            {
                
                if (value > 0 && value < 100)
                {
                    cashback = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите допустимое число процентов");
                }
            }
        }

        //Конструктор без параметров
        public YouthCard() : base()
        {
            Cashback = 6;
        }

        //Конструктор с параметрами
        public YouthCard(string number, string owner, DateTime duration, int cashback) : base(number, owner, duration)
        {
            Cashback = cashback;
        }

        //Преобразование в строку
        public override string ToString()
        {
            return $"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()}\nКэшбэк: {Cashback}%\n";
        }

        //Вывод информации на экран при помощи обычного метода
        public new void Show()
        {
            Console.WriteLine($"Молодежная карта:");
            base.Show();
            Console.WriteLine($"Кэшбэк: {Cashback}%\n");
        }

        //Вывод информации на экран при помощи виртуального метода
        public override void ShowVirtual()
        {
            Console.WriteLine($"Молодежгая карта:");
            base.Show();
            Console.WriteLine($"Кэшбэк: {Cashback}%\n");
        }

        //Ввод информации об экземплярах класса с клавиатуры
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите процент кэшбэка:");
            Cashback = int.Parse(Console.ReadLine());
                        
        }

        //Ввод информации об экземплярах класса при помощи ДСЧ
        public override void RandomInit()
        {
            base.RandomInit();
            Cashback = random.Next(1, 101);
        }

        public override bool Equals(object obj)
        {
            YouthCard card = obj as YouthCard;
            if (card != null)
                return this.Number == card.Number && this.Owner == card.Owner && this.Duration == card.Duration && card.Cashback == this.Cashback;
            else
                return false;
        }
    }
}
