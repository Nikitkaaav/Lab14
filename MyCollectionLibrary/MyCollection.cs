using System;
using ClassLibraryLab10;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab12._4
{
    public class MyCollection<T> : MyList<T>, IEnumerable<T> where T : IInit, ICloneable, new()
    {
        //Конструктор без параметров 
        public MyCollection() : base() { }
        //Конструктор с параметром размера 
        public MyCollection(int size) : base(size) { }
        //Конструктор, инициализирующий коллекцию существующим массивом 
        public MyCollection(T[] collection) : base(collection) { }

        // Метод возвращает перечислитель элементов типа T 
        public IEnumerator<T> GetEnumerator()
        {
            //Устанавливаем начальное значение текущего элемента на начало списка 
            Point<T> current = beg;
            //Пока текущий элемент не равен null, проходим по всем элементам списка 
            while (current != null)
            {
                //Возвращаем данные текущего элемента в текущей итерации перечислителя 
                yield return current.Data;
                //Переходим к следующему элементу списка 
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }

    
}