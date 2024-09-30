using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Events;
using Microsoft.DotNet.Interactive.Commands;

Task SetVar<T>(string name, T value)
    => Kernel.Current.SendAsync(new SendValue(name, value));

async Task<T> GetVar<T>(string name)
{
    var result = await Kernel.Current.SendAsync(new RequestValue(name));
    return result.Events
        .OfType<ValueProduced>()
        .Where(x => x.Name == name)
        .Select(x => x.Value)
        .OfType<T>()
        .FirstOrDefault();
}

async Task<T> VarOrDefault<T>(string name, Func<Task<T>> defaultAsyncFactory)
{
    var value = await GetVar<T>(name);
    if(value == null)
    {
        value = await defaultAsyncFactory();
        await SetVar(name, value);
    }
    return value;
}

async Task<string> RequestVar(string name,
    string description = null,
    bool isSecret = false)
{
    description ??= $"Set a value for {name}";
    Func<Task<string>> task = isSecret
        ? async () => (await Kernel.GetPasswordAsync(description)).GetClearTextPassword()
        : () => Kernel.GetInputAsync(description);
    return await VarOrDefault(name, task);
}

async Task Unset(string name)
    => Kernel.Current.SendAsync(new SendValue(name, null));
