using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLab10
{
    public class SortByDate : IComparer
    {
        public int Compare(object? x, object? y)
        {
            BankCard card1 = x as BankCard;
            BankCard card2 = y as BankCard;
            if (card1.Duration < card2.Duration) return -1;
            else if (card1.Duration == card2.Duration) return 0;
            else return 1;
        }
    }
}
