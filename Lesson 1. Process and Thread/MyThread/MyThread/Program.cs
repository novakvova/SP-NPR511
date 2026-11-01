// See https://aka.ms/new-console-template for more information

using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("-- Працюю із потоками :) --");

//Windows показує логічні процесори
int countCPU = Environment.ProcessorCount; 
Console.WriteLine($"Кількість логічних процесорів: {countCPU}");

//Багатопоточність або мультизадачність
// це коли програма працює і виконує свої задачі 
// на разних ядрах процесора
// Коли ми зараз пишемо наші програми вони запускються, як один процес для
// для OS (Windows, Linux, MacOS)
// Коли працює процес у собі може запускати під задачі
// під задачі у процесорі називається потокі - зверніть увагу, що у процсора
// це логічні ядра
//------Роботу процесів та потів займається операційна систем-------
//------Операційна система управляє процесами і потоками-------
//--Наша задача вміти створити декалька потоків і запустити їх у роботу--
//--При цьому дані потоки працюють в середині свого процесу--

//Важливо відзначти, що коли працює програма у неї є ідентифікатор потоку
Console.WriteLine("Id потоку " + Thread.CurrentThread.ManagedThreadId);
//Наша задача запустити новий потік паралельно відносно поточного потоку
//Коли працює програма завжди є 1 потік - його назвають основний потік

//Хочу запустити новий потік
//у кожен новий потік треба передає функцію, яка там буде виконуватися
Thread newThread = new Thread(Animals);
newThread.Start(); //запускаю потік вториний, який працює паралельно основному

string[] eats = { "Піца", "Салат", "Хот-дог" };
foreach(string eat in eats)
{
    Console.WriteLine(eat);
    Thread.Sleep(500);
}

void Animals()
{
    string[] animals = { "Котик", "Їжачок", "Білочка", "Опосум" };
    foreach (string animal in animals)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(animal);
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(500); //Робимо паузу на 0.5 секунди
    }
}