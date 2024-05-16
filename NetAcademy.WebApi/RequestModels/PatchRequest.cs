namespace NetAcademy.WebApi.RequestModels;

public class PatchRequest
{
    public PatchItem[] PatchItems { get; set; }
}


public class PatchItem
{
    public string PropertyName { get; set; }
    public object NewValue { get; set; }
}