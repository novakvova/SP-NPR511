// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("-- Працюємо із потоками --");

//Запускаємо функцію синхронно
//Як працювати із часом роботи програми
//StopWatch
//Давайте зробимо масив потоків і кожен потік буде окремо качати фото
/* ----------Тут у мене працює 1 потік на закачку фото-------------
Thread myThread = new Thread(ThreadDownload);
myThread.Start(20); //запуск потоку
//дочекатися поки потік завешить свою фото, щоб можна було працювати із даними,
//які були зроблені у потоці
Console.WriteLine("Ми проводимо закачку даних у потоці ...");
myThread.Join(); //очікує завеншення опреції
*/
// -----------Хочу зробити масив потоків-------------
Console.WriteLine("Скільки потоків запустити: ");
int count = int.Parse(Console.ReadLine());
Thread[] threads = new Thread[count];//Маємо 2 потоки 
for (int i = 0; i < count; i++)
{
    threads[i] = new Thread(ThreadDownload);
    threads[i].Start(20); //Передаю кількість файлів у потік
}
//двате зроблю, щоб потоки здали один одного
foreach (var item in threads)
    item.Join(); // У черзі котоки чекають 1 одного 

//threads[0] = new Thread(ThreadDownload);
//threads[1] = new Thread(ThreadDownload);
//threads[0].Start(20); //Кожен потік качає по 20 фото
//threads[1].Start(20); //Кожен потік качає по 20 фото
//threads[0].Join(); //Чекає поки відпрацює 1 потік
//threads[1].Join(); //Чекає поки відпрацює 2 потік
Console.WriteLine("Операція завершена. Гарного дня");

void ThreadDownload(object count)
{
    int itemDownload = (int)count;
    Console.WriteLine("Count = {0}", itemDownload);
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();

    DownloadImages(itemDownload);

    stopWatch.Stop();
    // Get the elapsed time as a TimeSpan value.
    TimeSpan ts = stopWatch.Elapsed;

    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);
    Console.WriteLine("RunTime " + elapsedTime);
}

void DownloadImages(int count) //Сиконується у окремому потоці
{
    //Масив фото, які нам потрібно скачати
    HttpClient client = new HttpClient();

    List<string> urls = new List<string>()
    {
        //"https://content2.rozetka.com.ua/goods/images/big/671871936.jpg",
        //"https://content.rozetka.com.ua/goods/images/big/590680165.jpg",
        //"https://content1.rozetka.com.ua/goods/images/big/623059494.jpg",
        //"https://content2.rozetka.com.ua/goods/images/big/569996764.jpg",
        //"https://content1.rozetka.com.ua/goods/images/big/655330317.jpg",
        //"https://content1.rozetka.com.ua/goods/images/big/655339086.jpg"
    };

    //string urls - можна звяти багато фото на одному цікавому сайті
    for (int i = 0; i < count; i++)
    {
        urls.Add("https://i.pravatar.cc/1200");
    }

    string folder = "avatars";
    //Directory
    Directory.CreateDirectory(folder); //створюємо папку для фото у поточні папці
    //Працюю з двійковими файлами - по суті це байти
    foreach (string url in urls)
    {
        //Читаю файли із інтеренати по url- байти
        byte[] buffer = client.GetByteArrayAsync(url).Result;
        //Роблю унікальне і'мя файлу
        string fileName = Guid.NewGuid().ToString()+".jpg";
        //Обєдную ім'я папки із іменем файлу
        string path = Path.Combine(folder, fileName);
        //Зберегаю наше фото
        File.WriteAllBytes(path, buffer);
    }
}
