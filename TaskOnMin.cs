using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymplexMetod
{
    internal class TaskOnMin
    {
        public void Start()
        {
            MassX_Function massFunction = new MassX_Function();

            Console.Write("Введите количество строк: ");
            string? countStr = Console.ReadLine();

            Console.Write("Введите количество X, которые будут введены пользователем с учетом финального результата и знака неравенства: ");
            string? countX = Console.ReadLine();

            bool testCount = int.TryParse(countStr, out int _countStr);
            bool testX = int.TryParse(countX, out int _countX);

            double[,] _massX = massFunction.massX(_countStr, testCount, testX);
            double[] _massFunc = massFunction.Func(_countX - 2, _countStr + 1);

            for (int i = 0; i < _massX.GetLength(0); i++)
            {
                for (int j = 0; j < _massX.GetLength(1); j++)
                {
                    Console.Write(_massX[i, j] + "\t");
                }
                Console.WriteLine();
            }

            foreach (var x in _massFunc)
            {
                Console.Write(x + "\t");
            }
            Console.WriteLine();

            WorkSpace(_massX, _massFunc, _countStr, _countX);
        }


        internal void WorkSpace(double[,] _massX, double[] _massFunc, int _countStr, int _countX)
        {
            int[] basisStr = new int[_massX.GetLength(0)];
            int[] basisStlb = new int[_massX.GetLength(1)];

            for (int i = 0; i < _massX.GetLength(0); i++)
            {
                basisStr[i] = _countStr + i + 1;
            }
            for (int i = 0; i < _massX.GetLength(1); i++)
            {
                basisStlb[i] = i + 1;
            }

            while (true)
            {
                int j_massFunc = 0;//Переменная для ведения счета столбцов
                int j_massFuncFix = 0; //Переменная для фиксации столбца

                int i_massFuncFix = 0; //Переменная для фиксации строки
                double max = int.MinValue; //Переменная принимающая минимум

                foreach (var x in _massFunc)
                {
                    if (x > max)
                    {
                        max = x;
                        j_massFuncFix = j_massFunc;
                    }
                    j_massFunc++;
                }

                if (max > 0)
                {
                    double countTest = 0; //Подсчет результатов, поделенных на ведущий столбец
                    double countMinPol = double.MaxValue; //Минимальное положительное
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        for (int i = 0; i < _massX.GetLength(0); i++)
                        {
                            if (j == j_massFuncFix)
                            {
                                countTest = _massX[i, _massX.GetLength(1) - 1] / _massX[i, j];

                                if (countTest >= 0 && countTest <= countMinPol)
                                {
                                    countMinPol = countTest;
                                    i_massFuncFix = i;
                                }
                            }
                        }
                    }
                }
                else
                {
                    break; //Завершение функции
                }

                basisStr[i_massFuncFix] = basisStlb[j_massFuncFix];

                double divider = _massX[i_massFuncFix, j_massFuncFix];

                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i == i_massFuncFix)
                        {
                            _massX[i, j] = _massX[i, j] / divider;
                        }
                    }
                }

                List<double> fixStrList = new List<double>();

                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i == i_massFuncFix)
                        {
                            fixStrList.Add(_massX[i, j]);
                        }
                    }
                }

                double[] fixStr = fixStrList.ToArray();


                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    double _fixCount = _massX[i, j_massFuncFix];
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        if (i != i_massFuncFix)
                        {
                            _massX[i, j] = _massX[i, j] - fixStr[j] * _fixCount;
                        }
                    }
                }

                double fixCount = _massFunc[j_massFuncFix];

                for (int i = 0; i < _massFunc.Length; i++)
                {

                    _massFunc[i] = _massFunc[i] - fixStr[i] * fixCount;
                }

                Console.WriteLine();

                for (int i = 0; i < _massX.GetLength(0); i++)
                {
                    Console.Write($"X{basisStr[i]} ");
                    for (int j = 0; j < _massX.GetLength(1); j++)
                    {
                        Console.Write(_massX[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                Console.Write($"X^ ");
                foreach (var x in _massFunc)
                {
                    Console.Write(x + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}