namespace AdventOfCode
{
    internal class Day1
    {
        public static void Run()
        {
            int sum = 0;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Day1Input.txt");
            StreamReader reader = new StreamReader(path);
            List<int> numbers = new();
            sum = ReadInput1(reader, numbers);//55386
            Console.WriteLine(sum);
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            sum = ReadInput2(reader, numbers);//54824
            Console.WriteLine(sum);
        }
        public static int ReadInput1(StreamReader reader, List<int> numbers)
        {
            int sum = 0;
            string? line = reader.ReadLine();
            while (line != null)
            {
                numbers = line.Where(char.IsDigit).Select(c => (int)char.GetNumericValue(c)).ToList();
                if (numbers.Count == 0) numbers.Add(0);
                sum = (numbers.Count == 1) ? sum + numbers[0] * 10 + numbers[0] : sum + numbers[0] * 10 + numbers[^1];
                numbers.Clear();
                line = reader.ReadLine();
            }
            return sum;
        }
        public static int ReadInput2(StreamReader reader, List<int> numbers)
        {
            int sum = 0;
            string? line = reader.ReadLine();
            int counter = 0;
            Dictionary<string, int> numbersInLine = new()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };
            Dictionary<int, int> dict = new();
            while (line != null)
            {
                foreach (var pair in numbersInLine)
                {
                    int index = line.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase);
                    while (index != -1)
                    {
                        dict.Add(index, pair.Value);
                        index = line.IndexOf(pair.Key, index + 1, StringComparison.Ordinal);
                    }
                }
                foreach (char c in line)
                {
                    if (char.IsDigit(c))
                    {
                        int number = (int)char.GetNumericValue(c);
                        dict.Add(counter, number);
                    }
                    counter++;
                }
                if (dict.Count > 0)
                {
                    numbers.AddRange(dict.OrderBy(key => key.Key).Select(pair => pair.Value));
                }
                if (numbers.Count == 0) numbers.Add(0);
                sum = (numbers.Count == 1) ? sum + numbers[0] * 10 + numbers[0] : sum + numbers[0] * 10 + numbers[^1];
                numbers.Clear();
                dict.Clear();
                counter = 0;
                line = reader.ReadLine();
            }
            return sum;
        }
    }
}
