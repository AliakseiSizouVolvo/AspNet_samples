using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class TransientService : ITransientService
{
    private int _counter;

    public TransientService()
    {
        _counter = 0;
    }

    public int GetValue()
    {
        _counter++;
        return _counter;
    }
}