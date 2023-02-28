using System.Reflection;

var currentAssembly = Assembly.GetExecutingAssembly();
var services = LoadServiceContext(currentAssembly);
Console.WriteLine();

IDictionary<string,ICalcService> LoadServiceContext(Assembly assembly)
{
    var services = new Dictionary<string, ICalcService>();
    var calcServiceType = typeof(ICalcService);
    var instances = assembly.GetTypes()
        .Where(t => calcServiceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

    foreach(var type in instances)
    {
        if (Activator.CreateInstance(type) is ICalcService instance)
            services.Add(instance.Market, instance);
    }
    return services;
}
