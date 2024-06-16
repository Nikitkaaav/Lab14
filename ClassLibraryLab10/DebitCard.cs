using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class DebitCard : BankCard
    {
        //Поле
        private int balance;

        //Свойство
        public int Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (value >= 0)
                {
                    balance = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите допустимый баланс");
                }
            }
        }

        //Конструктор без параметров
        public DebitCard() : base()
        {
            Balance = 15000;
        }

        //Конструктор с параметрами
        public DebitCard(string number, string owner, DateTime duration, int balance) : base(number, owner, duration)
        {
            Balance = balance;
        }

        //Преобразование в строку
        public override string ToString()
        {
            return $"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()}\nБаланс: {Balance} руб.\n";
        }

        //Вывод информации на экран при помощи обычного метода
        public new void Show()
        {
            Console.WriteLine($"Дебетовая карта:");
            base.Show();
            Console.WriteLine($"Баланс: {Balance} руб.\n");
        }

        //Вывод информации на экран при помощи виртуального метода
        public override void ShowVirtual()
        {
            Console.WriteLine($"Дебетовая карта:");
            base.Show();
            Console.WriteLine($"Баланс: {Balance} руб.\n");
        }

        //Ввод информации об экземплярах класса с клавиатуры
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите баланс карты:");
            Balance = int.Parse(Console.ReadLine());
        }

        //Ввод информации об экземплярах класса при помощи ДСЧ
        public override void RandomInit()
        {
            base.RandomInit();
            Balance = random.Next(1, 10000000);
        }

        public override bool Equals(object obj)
        {
            DebitCard card = obj as DebitCard;
            if (card != null)
                return this.Number == card.Number && this.Owner == card.Owner && this.Duration == card.Duration && card.Balance == this.Balance;
            else
                return false;
        }
    }
}
