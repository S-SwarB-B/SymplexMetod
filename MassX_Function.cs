using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplexMetod
{
    internal class MassX_Function
    {
        public double[,] massX(int _countStr, bool testCount, bool testX)
        {
            List<List<double>> massList = new List<List<double>>(); //Создание списка списков

            if (testCount && testX) //Проверка на корректность заполнения полей
            {
                int countAddX = 0; //Создание счетчика для добавления новых X

                for (int i = 0; i < _countStr; i++) //Начало цикла
                {
                    List<double> massListLevel2 = new List<double>(); //Создание промежуточного списка
                    try
                    {
                        string[]? strMassListLevel2 = Console.ReadLine() //Массив строк с данными
                            .Split(" ");

                        int j = 0 //Создание счетчика
;
                        foreach (string s in strMassListLevel2) //Цикл по строке
                        {
                            if (j != strMassListLevel2.Length - 2) //Условие для добавления всех данных кроме знаков неравенства
                            {
                                double countMassListLevel2 = double.Parse(s); //Перевод строки в число
                                massListLevel2.Add(countMassListLevel2); //Добавление в список
                            }

                            if (strMassListLevel2[j] == "<=") //Если неравенство <=
                            {
                                int storeOldMassListLevel2Length = massListLevel2.ToArray().Length; //Переменная хранящая длинну промежуточного листа до добавления новых X
                                for (int k = 0; k < _countStr; k++) //Цикл добавления новых X
                                {
                                    massListLevel2.Add(0); //Заполнение пространства нулями
                                    if (k == _countStr - 1) //Если цикл делает последний ход
                                    {
                                        massListLevel2[storeOldMassListLevel2Length + countAddX] = 1; //Заполнение полей однерками (при заполнении первой строки 1 смещается вправо)
                                        countAddX++; //Увеличение счетчика новых X
                                    }
                                }
                            }
                            else if (strMassListLevel2[j] == ">=") //Если неравенство >=
                            {
                                int storeOldMassListLevel2Length = massListLevel2.ToArray().Length;
                                for (int k = 0; k < _countStr; k++)
                                {
                                    massListLevel2.Add(0);
                                    if (k == _countStr - 1)
                                    {
                                        massListLevel2[storeOldMassListLevel2Length + countAddX] = -1;
                                        countAddX++;
                                    }
                                }
                                for (int z = 0; z < massListLevel2.ToArray().Length; z++) //Цикл для умножения неравенства на -1, чтобы новый X стал положительным
                                {
                                    massListLevel2[z] = massListLevel2[z] * (-1);
                                }
                            }
                            if (j > 0 && strMassListLevel2[j-1] == ">=") //Условие для того, чтобы цифра справо от знака поменяла знак на противоположный 
                            {
                                massListLevel2[massListLevel2.ToArray().Length-1] = massListLevel2[massListLevel2.ToArray().Length-1] * (-1);
                            }
                            j++;
                        }
                        massList.Add(massListLevel2); //Добавление промежуточного листа в главный лист
                    }
                    catch
                    {
                        Console.Write("Ошибка. Вместо числа был введен другой символ");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка. Вместо числа был введен другой символ");
            }

            double[,] massX = new double[massList.Count, massList[0].Count]; //Создание двумерного массива

            for (int i = 0; i < massList.Count; i++) //Конвертация списка списков в двумерный массив
            {
                for (int j = 0; j < massList[i].Count; j++)
                {
                    massX[i, j] = massList[i][j];
                }
            }
            return massX; //Возврат массива
        }

        public double[] Func(int _countX, int _fourBalance) //Функция задачи
        {
            List<double> listFunc = new List<double>(); //Создание листа

            for(int i = 0; i < _countX; i++)
            {
                Console.Write($"X{i+1} = ");
                try
                {
                    listFunc.Add(Convert.ToDouble(Console.ReadLine()) * (-1)); //Запись значений функции
                }
                catch 
                {
                    Console.WriteLine("Ошибка. Вместо числа был введен другой символ");
                    break;
                }  
            }
            for (int i = 0; i < _fourBalance; i++) //Растягивание строки на всю ширину таблицы
            {
                listFunc.Add(0);
            }
            return listFunc.ToArray(); //Возврат листа конвертированного в массив
        }
    }
}
