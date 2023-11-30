using AdventOfCode2023;

List<IDay> instances = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
    .Where(d => d.GetInterfaces().Contains(typeof(IDay))
        && d.GetConstructor(Type.EmptyTypes) != null
        && !d.IsAbstract)
        .Select(d => (IDay)Activator.CreateInstance(d))
        .ToList();


foreach (IDay day in instances.OrderBy(d => d.SortOrder))
{
    day.Calculate();
}

Console.WriteLine("Calculation ended, hit any key to close");
Console.ReadKey();