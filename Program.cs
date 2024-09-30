class Program
{
    static async Task Main(string[] args)
    {
        //Используя асинхронный метод, считать текст из файла,
        //определить количество символов, результат вывести на экран.
        string filePath = "C:\\Users\\user\\Desktop\\newFile.txt"; 

        try
        {
            string content = await ReadFileAsync(filePath);
            int charCount = content.Length;
            Console.WriteLine($"Количество символов в файле: {charCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        //Реализуйте асинхронную генерацию списка простых чисел.
        int maxVal = 500;
        List<int> primes = await FindPrimesAsync(maxVal);
        Console.WriteLine($"Простые числа до {maxVal}:");
        foreach(int i in primes)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine();

    //Ваша задача, использовать класс Task для асинхронного выполнения загрузки любого файл.

        // файл в 3 ГБ НЕ КАЧАЕТ!!((
        string url = "https://drive.google.com/file/d/1MbNwa6rjTLH6HcJpR7rmJJY3NIZRPabW/view?usp=sharing";
        string savePath = "C:\\Users\\user\\Downloads\\downloadedFile";
        try
        {
            await DownloadFileAsync(url, savePath);
            Console.WriteLine($"Файл успешно загружен и сохранен");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

    }
    // 1 часть
    static async Task<string> ReadFileAsync(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            return await reader.ReadToEndAsync();
        }
    }
    // 2 часть
    static async Task<List<int>> FindPrimesAsync(int limit)
    {
        return await Task.Run(() =>
        {
            List<int> primes = new List<int>();

            for (int i = 2; i <= limit; i++)
            {
                if (IsPrime(i))
                {
                    primes.Add(i);
                }
            }

            return primes;
        });
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }
    //3 часть
    static async Task DownloadFileAsync(string url, string destinationPath)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] fileBytes = await client.GetByteArrayAsync(url);
            await Task.Run(() => File.WriteAllBytes(destinationPath, fileBytes));
        }
    }
}