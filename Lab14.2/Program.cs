using ClassLibraryLab10;
using Lab12._4;

namespace Lab14._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static MyCollection<BankCard> MakeCollection(int length)
            {
                var list = new MyCollection<BankCard>();
                for (int i = 0; i < length; i++)
                {
                    list.Add(new BankCard());
                }
                return list;
            }

            static void PrintCollection(MyCollection<BankCard> list)
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

            MyCollection<BankCard> group1 = MakeCollection(10);
            group1.Add(new YouthCard());

            MyCollection<BankCard> group2 = MakeCollection(10);
            group2.Add(new YouthCard());

            MyCollection<BankCard> group3 = MakeCollection(10);
            group3.Add(new YouthCard());

            List<MyCollection<BankCard>> bank = new List<MyCollection<BankCard>>();
            bank.Add(group1);
            bank.Add(group2);
            bank.Add(group3);

            //LINQ для извлечения данные о номерах карт и их кэшбеке
            var res2 = from item in bank
                       from item2 in item
                       where item2 is YouthCard
                       select new { Number = item2.Number, Cashback = ((YouthCard)item2).Cashback };
            Console.WriteLine("\nКарты с балансом");
            foreach (var item in res2)
            {
                Console.WriteLine(item);
            }

            //Метод расширения для извлечения данные о номерах карт и их кэшбеке
            var res3 = bank.SelectMany(x => x)
                .Where(x => x is YouthCard)
                .Select(x => new { Number = x.Number, Cashback = ((YouthCard)x).Cashback }).ToList();
            Console.WriteLine("\nКарты с балансом");
            foreach (var item in res3)
            {
                Console.WriteLine(item);
            }

            //LINQ для вычисления кэшбека каждой молодежной карты 
            var res4 = (from item in bank
                            // Для каждого элемента внутри item 
                        from item2 in item
                            // Фильтруем только объекты типа YouthCard
                        where item2 is YouthCard
                        // Вычисляем сумму кэшбэка для каждой молодежной карты
                        let totalCashback = ((YouthCard)item2).Cashback
                        // Группируем данные по типу карты
                        group new { item2.Number, totalCashback } by item2.GetType().Name into g
                        // Выбираем результат в новую анонимную структуру
                        select new
                        {
                            CardType = g.Key,
                            // Подсчитываем количество карт в группе
                            Count = g.Count(),
                            // Суммируем общий кэшбэк для карт в группе
                            TotalCashback = g.Sum(x => x.totalCashback)
                        }).ToList();

            // Выводим сводку по картам на консоль
            Console.WriteLine("\nСводка по картам:");
            foreach (var summary in res4)
            {
                Console.WriteLine($"Тип карты: {summary.CardType}, Количество: {summary.Count}, Общий кэшбэк: {summary.TotalCashback}");
            }

            //Метод расширения для вычисления кэшбека каждой молодежной карты 
            var res41 = bank.SelectMany(x => x)
               // Группируем элементы по имени их типа
               .GroupBy(x => x.GetType().Name)
               // Для каждой группы создаем новый объект
               .Select(g => new
               {
                   // Тип карты (ключ группировки)
                   CardType = g.Key,
                   // Подсчитываем количество карт данного типа
                   Count = g.Count(),
                   // Суммируем кэшбэк. Для молодежных карт учитываем кэшбэк, для остальных - 0
                   TotalCashback = g.Sum(x => x is YouthCard ? ((YouthCard)x).Cashback : 0)
               })
               .ToList();

            // Выводим сводку по картам на консоль
            Console.WriteLine("\nСводка по картам:");
            foreach (var summary in res41)
            {
                // Выводим информацию о каждом типе карты: тип, количество и общий кэшбэк
                Console.WriteLine($"Тип карты: {summary.CardType}, Количество: {summary.Count}, Общий кэшбэк: {summary.TotalCashback}");
            }
        }
    }
}