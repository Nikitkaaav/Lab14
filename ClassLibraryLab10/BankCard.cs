using System.Data;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ClassLibraryLab10
{
    public class BankCard : IInit, IComparable, ICloneable
    {
        static string[] Names = { "IVAN", "PETR", "NIKOLAI", "ANDREI", "DENIS", "ARTEM", "MIKHAIL", "STEPAN", "PAVEL", "GEORGIY", "EGOR", "FEDOR", "ALEKSEI", "VADIM", "LEONID", "VLADIMIR" };
        static string[] Surnames = { "PERMINOV", "ALIPOV", "NIKITIN", "ZHILIN", "KOROVIEV", "PALUEV", "IVANOV", "PETROV", "ANDREEV", "DENISOV", "SERGEEV", "CHUNAEV", "EVELIANOV", "STRELTSOV", "KUZNETSOV", "ELIN", "KERIMOV" };
        protected Random random = new Random();

        //Поля
        protected string number;
        protected string owner;
        protected DateTime duration;

        //Свойства
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value.Length == 16)
                {
                    this.number = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите действительный 16-значный номер.");
                }
            }
        }
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.owner = value;
                }
                else
                {
                    throw new ArgumentException("Ошибка: введите корректные фамилию и имя");
                }
            }
        }
        public DateTime Duration { get { return duration; } set { duration = value; } }

        //Кноструктор без параметров
        public BankCard()
        {
            number = "9000345589893331";
            owner = "SERGEI AKSENOV";
            Duration = new DateTime(2024, 10, 12);
        }

        //Конструктор с параметрами
        public BankCard(string number, string owner, DateTime duration)
        {
            this.number = number;
            this.owner = owner;
            this.duration = duration;
        }

        //Преобразование в строку
        public override string ToString()
        {
            return $"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()}\n ";
        }

        //Обычный метод вывода информации на экран
        public void Show()
        {
            Console.WriteLine($"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()} ");
        }

        //Вирутальный метод вывода информации на экран
        public virtual void ShowVirtual()
        {
            Console.WriteLine($"Номер карты: {Number}\nВладелец: {Owner}\nСрок действия: {Duration.ToShortDateString()}\n ");
        }

        //Ввод информации об экземплярах класса с клавиатуры
        public virtual void Init()
        {
            Console.WriteLine("Введите номер карты:");
            Number = Console.ReadLine();
            Console.WriteLine("Введите данные владельца карты:");
            Owner = Console.ReadLine();
            Console.WriteLine("Введите срок действия карты:");
            Duration = DateTime.Parse(Console.ReadLine());
        }

        //Ввод информации об экземплярах класса при помощи ДСЧ
        public virtual void RandomInit()
        {
            Number = GenerateRandomNumber(random);
            Owner = Names[random.Next(Names.Length)] + " " + Surnames[random.Next(Surnames.Length)];
            Duration = new DateTime(random.Next(2025, 2029), random.Next(1, 13), random.Next(1, 29));
        }

        //Создание случайного номера карты
        static string GenerateRandomNumber(Random random)
        {
            string number = "";
            for (int i = 0; i < 16; i++)
            {
                number += random.Next(10);
            }
            return number;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is BankCard card)
                return this.Number == card.Number && this.Owner == card.Owner && this.Duration == card.Duration;
            return false;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            if (obj is not BankCard) return -1;
            BankCard card = obj as BankCard;
            return String.Compare(this.Owner, card.Owner);
        }


        public object Clone()
        {
            BankCard clonedCard = new BankCard
            {
                Number = this.Number,
                Owner = this.Owner,
                Duration = this.Duration
            };

            return clonedCard;
        }
    }
}