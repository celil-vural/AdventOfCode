namespace AdventOfCode
{
    internal class Day2
    {
        static Dictionary<string, int> cubes = new();
        public static void Run()
        {
            int sum = 0;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Day2Input.txt");
            StreamReader reader = new StreamReader(path);
            sum = ReadInput1(reader);//1734
            Console.WriteLine(sum);
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            sum = ReadInput2(reader);//70387
            Console.WriteLine(sum);
        }
        public static int ReadInput1(StreamReader reader)
        {
            int sum = 0;
            cubes.Clear();
            cubes.Add("red", 12);
            cubes.Add("green", 13);
            cubes.Add("blue", 14);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] games = line.Split(':');
                string[] parts = games[1].Split(';');
                bool control = parts.All(part =>
                {
                    string[] partCubes = part.Split(',');
                    Dictionary<string, int> dict = ManipulationForPartCubes(partCubes);
                    return !dict.Any(item => cubes[item.Key] < item.Value);
                });
                if (control)
                {
                    sum += int.Parse(games[0].Trim().Split(" ")[1]);
                }
            }
            return sum;
        }
        public static int ReadInput2(StreamReader reader)
        {
            int sum = 0;
            cubes.Clear();
            cubes.Add("red", 0);
            cubes.Add("green", 0);
            cubes.Add("blue", 0);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] games = line.Split(':');
                string[] parts = games[1].Split(';');
                foreach (var part in parts)
                {
                    string[] partCubes = part.Split(',');
                    Dictionary<string, int> dict = ManipulationForPartCubes(partCubes);
                    dict.Where(item => cubes[item.Key] < item.Value)
                        .ToList()
                        .ForEach(item => cubes[item.Key] = item.Value);
                }
                int sum2 = cubes.Values.Aggregate(1, (current, value) => current * value);
                foreach (var key in cubes.Keys.ToList())
                {
                    cubes[key] = 0;
                }
                sum += sum2;
            }

            return sum;
        }
        private static Dictionary<string, int> ManipulationForPartCubes(string[] partCubes)
        {
            return partCubes
                .Select(item => item.Trim().Split(' '))
                .GroupBy(cube => cube[1])
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(cube => int.Parse(cube[0]))
                );
        }
    }
}