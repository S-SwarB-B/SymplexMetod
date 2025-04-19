using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SymplexMetod
{
    public class TaskOnMax
    {
        public void Start()
        {
            MassX_Function massFunction = new MassX_Function(); //Подключение записи массива

            Console.Write("Введите количество строк: ");
            string? countStr = Console.ReadLine(); //Ввод количества строк

            Console.Write("Введите количество X, которые будут введены пользователем с учетом финального результата и знака неравенства: ");
            string? countX = Console.ReadLine(); //Ввод количества всех элементов в записанной строке

            bool testCount = int.TryParse(countStr, out int _countStr); //Проверка на корректность ввода
            bool testX = int.TryParse(countX, out int _countX); //Проверка на корректность ввода

            double[,] _massX = massFunction.massX(_countStr, testCount, testX); //Заполнение массива
            double[] _massFunc = massFunction.Func(_countX-2, _countStr + 1); //Заполнение функции

            for (int i = 0; i < _massX.GetLength(0); i++) //Вывод исходного массива
            {
                for (int j = 0; j < _massX.GetLength(1); j++)
                {
                    Console.Write(_massX[i, j] + "\t"); 
                }
                Console.WriteLine();
            }

            foreach (var x in _massFunc) //Вывод значений X^
            {
                Console.Write(x + "\t");
            }
            Console.WriteLine();

            WorkSpace(_massX, _massFunc, _countStr,_countX); //Запуск решения
        }


        internal void WorkSpace(double[,] _massX, double[] _massFunc, int _countStr, int _countX)
        {
            int[] basisStr = new int[_massX.GetLength(0)]; //Базисный стролбец
            int[] basisStlb = new int[_massX.GetLength(1)]; //Базисная строка

            for (int i = 0; i < _massX.GetLength(0); i++) //Заполнение стандартными значениями
            {
                basisStr[i] = _countStr + i + 1;
            }
            for (int i = 0; i < _massX.GetLength(1); i++) //Заполнение стандартными значениями
            {
                basisStlb[i] = i + 1;
            }

            while (true) 
            {
                int j_massFunc = 0;//Переменная для ведения счета столбцов
                int j_massFuncFix = 0; //Переменная для фиксации столбца

                int i_massFuncFix = 0; //Переменная для фиксации строки
                double min = int.MaxValue; //Переменная принимающая минимум

                foreach (var x in _massFunc) //Проверка на оптимальность
                {
                    if (x < min)
                    {
                        min = x;
                        j_massFuncFix = j_massFunc; //Фиксация ведущего столбца
                    }
                    j_massFunc++;
                }

                if (min < 0) //Проверка на отрицательные числа в X^
                {
                    double countTest = 0; //Подсчет результатов, поделенных на ведущий столбец
                    double countMinPol = double.MaxValue; //Минимальное положительное
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        for (int i = 0; i < _massX.GetLength(0); i++)
                        {
                            if(j == j_massFuncFix)
                            {
                                countTest = _massX[i, _massX.GetLength(1) - 1] / _massX[i, j];

                                if (countTest >= 0 && countTest <= countMinPol)
                                {
                                    countMinPol = countTest;
                                    i_massFuncFix = i; //Фиксация ведущей строки
                                }
                            }
                        }
                    }
                }            
                else
                {                  
                    break; //Завершение функции
                }

                basisStr[i_massFuncFix] = basisStlb[j_massFuncFix]; //Передача X из базисной строки в базисный столбец

                double divider = _massX[i_massFuncFix, j_massFuncFix]; //Делитель

                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i == i_massFuncFix)
                        {
                            _massX[i, j] = _massX[i, j] / divider; //Деление ведущей строки на делитель
                        }
                    }
                }

                List<double> fixStrList = new List<double>(); //Лист для фиксации ведущей строки

                for(int i = 0; i < _massX.GetLength(0); i++)
                {
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i == i_massFuncFix)
                        {
                            fixStrList.Add(_massX[i,j]); //Передача ведущей строки в лист
                        }
                    }
                }
                
                double[] fixStr = fixStrList.ToArray(); //Конвертация листа в массив
                

                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    double _fixCount = _massX[i, j_massFuncFix]; //Фиксация значения ведущей строки
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i != i_massFuncFix)
                        {                        
                            _massX[i,j] = _massX[i,j] - fixStr[j] * _fixCount; //Зануление всех элементов ведущего стролбца кроме ведущего элемента
                        }
                    }
                }

                double fixCount = _massFunc[j_massFuncFix]; //Фиксация значения X^ ведущего столбца

                for (int i = 0; i < _massFunc.Length; i++)
                {
                    
                    _massFunc[i] = _massFunc[i] - fixStr[i] * fixCount; //Зануление X^ ведущего столбца
                }

                Console.WriteLine();

                for (int i = 0; i < _massX.GetLength(0); i++) //Вывод нового массива
                {
                    Console.Write($"X{basisStr[i]} ");
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        Console.Write(_massX[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                Console.Write($"X^ "); //Вывод новых X^
                foreach (var x in _massFunc)
                {
                    Console.Write(x + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
