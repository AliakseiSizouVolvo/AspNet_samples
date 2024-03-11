using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class ScopedService : IScopedService
{
    private int _counter;

    public ScopedService()
    {
        _counter = 0;
    }

    public int GetValue()
    {
        _counter++;
        return _counter;
    }
}