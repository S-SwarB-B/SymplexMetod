using SymplexMetod;

TaskOnMax taskMax = new TaskOnMax(); //Подключение класса TaskOnMax
TaskOnMin taskMin = new TaskOnMin(); //Подключение класса TaskOnMin

Console.WriteLine("1 - максимум, 2 - минимум"); //Интерфейс для пользователя
string? a = Console.ReadLine();
if (a == "1")
{
    taskMax.Start(); //Запуск метода на максимум
}
else if (a == "2")
{
    taskMin.Start(); //Запуск метода на минимум
}
else
{
    Console.WriteLine("Что-то не то");
}