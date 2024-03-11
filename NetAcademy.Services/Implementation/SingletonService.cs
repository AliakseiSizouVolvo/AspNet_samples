using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class SingletonService : ISingletonService
{
    private int _counter;

    public SingletonService()
    {
        _counter = 0;
    }

    public int GetValue()
    {
        _counter++;
        return _counter;
    }
}