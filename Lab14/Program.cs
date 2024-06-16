using ClassLibraryLab10;
using System.Buffers;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab14
{
    internal class Program
    {
        static List<BankCard> MakeCollection(int length)
        {
            var list = new List<BankCard>();
            for (int i = 0; i < length; i++)
            {
                list.Add(new BankCard());
            }
            return list;
        }

        static void PrintCollection(List<BankCard> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Empty collection");
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        static void Main(string[] args)
        {
            List<BankCard> group1 = MakeCollection(10);
            group1.Add(new YouthCard());

            List<BankCard> group2 = MakeCollection(10);
            group2.Add(new YouthCard());

            List<BankCard> group3 = MakeCollection(10);
            group3.Add(new YouthCard());

            List<List<BankCard>> bank = new List<List<BankCard>>();
            bank.Add(group1);
            bank.Add(group2);
            bank.Add(group3);

            //LINQ для извлечения данные о номерах карт и их балансе
            var res2 = from item in bank
                       from item2 in item
                       where item2 is DebitCard
                       select new { Number = item2.Number, Balance = ((DebitCard)item2).Balance };
            Console.WriteLine("\nКарты с балансом");
            foreach (var item in res2)
            {
                Console.WriteLine(item);
            }

            //Метод расширения для извлечения данные о номерах карт и их балансе
            var res3 = bank.SelectMany(x => x)
                .Where(x => x is DebitCard)
                .Select(x => new { Number = x.Number, Balance = ((DebitCard)x).Balance }).ToList();
            Console.WriteLine("\nКарты с балансом");
            foreach (var item in res3)
            {
                Console.WriteLine(item);
            }

            //LINQ для создания списка объектов, содержащих номер карты(Number) и баланс
            var res31 = (from item in bank.SelectMany(x => x)
                         where item is DebitCard || item is CreditCard
                         select new { Number = ((dynamic)item).Number, Balance = (decimal)((dynamic)item).Balance })
                         .ToList();
            Console.WriteLine("\nКарты с балансом");
            foreach (var item in res31)
            {
                Console.WriteLine(item);
            }

            //Метод расширения для создания списка объектов, содержащих номер карты (Number) и баланс
            var res32 = bank.SelectMany(x => x)
             .Where(x => x is DebitCard)
             .Union(bank.SelectMany(x => x).Where(x => x is CreditCard))
             .Select(x => new { Number = ((dynamic)x).Number, Balance = (decimal)((dynamic)x).Balance })
             .ToList();

            //LINQ для вычисления кэшбэка молодежных карт
            var res4 = from item in bank
                       from item2 in item
                       where item2 is YouthCard
                       let totalCashBack = ((YouthCard)item2).Cashback
                       select new { Number = item2.Number, Cashback = totalCashBack };
            foreach (var item in res4)
            {
                Console.WriteLine(item);
            }

            //Метод расширения вычисления кэшбэка молодежных карт
            var res41 = bank.SelectMany(x => x)
               .Where(x => x is YouthCard)
               .Select(x => new { Number = x.Number, Cashback = ((YouthCard)x).Cashback })
               .ToList();
            Console.WriteLine("\nКарты молодежные с кэшбэком");
            foreach (var item in res4)
            {
                Console.WriteLine(item);
            }

            //LINQ cортировка по балансу
            var res5 = from item in bank
                       from item2 in item
                       where item2 is DebitCard
                       orderby ((DebitCard)item2).Balance descending
                       select item2;
            Console.WriteLine("\nСортировка по балансу");
            foreach (var item in res4)
            {
                Console.WriteLine(item);
            }

            //Метод расширения для сортировки по балансу
            var res51 = bank.SelectMany(x => x)
               .Where(x => x is DebitCard)
               .OrderByDescending(x => ((DebitCard)x).Balance)
               .ToList();
            Console.WriteLine("\nСортировка по балансу");
            foreach (var item in res5)
            {
                Console.WriteLine(item);
            }

            //LINQ для группировки элементов по свойству Duration
            var res6 = from item in bank
                       from item2 in item
                       group item2 by item2.Duration;
            Console.WriteLine("Группировка");
            foreach (var group in res6)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }

            //Метод расширения для группировки элементов по свойству Duration
            var res61 = bank.SelectMany(x => x)
               .GroupBy(x => x.Duration);
            Console.WriteLine("Группировка");
            foreach (var group in res6)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}