namespace NetAcademy.Services.Abstractions;

public interface IWhiteSpaceCleanerService
{
    public Task WriteAsync(Stream inputStream);
}