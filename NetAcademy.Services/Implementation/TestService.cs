using NetAcademy.Services.Abstractions;

namespace NetAcademy.Services.Implementation;

public class Test1Service : ITest1Service
{
    private readonly ITransientService _transientService;
    private readonly IScopedService _scopedService;
    private readonly ISingletonService _singletonService;

    public Test1Service(ITransientService transientService, 
        IScopedService scopedService, ISingletonService singletonService)
    {
        _transientService = transientService;
        _scopedService = scopedService;
        _singletonService = singletonService;
    }

    public (int, int, int)  Do()
    {
        int transientValue = _transientService.GetValue();
        int scopedValue = _scopedService.GetValue();
        int singletonValue = _singletonService.GetValue();

        return (transientValue, scopedValue, singletonValue);
    }
}


public class Test2Service  : ITest2Service
{
    private readonly ITransientService _transientService;
    private readonly IScopedService _scopedService;
    private readonly ISingletonService _singletonService;

    public Test2Service(ITransientService transientService, 
        IScopedService scopedService, ISingletonService singletonService)
    {
        _transientService = transientService;
        _scopedService = scopedService;
        _singletonService = singletonService;
    }

    public (int,int, int) Do()
    {
        int transientValue = _transientService.GetValue();
        int scopedValue = _scopedService.GetValue();
        int singletonValue = _singletonService.GetValue();

        return (transientValue, scopedValue, singletonValue);
    }
}