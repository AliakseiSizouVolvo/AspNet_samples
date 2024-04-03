using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.UI.Filters;

public class WhitespaceRemoverAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.HttpContext.Response;
        response.Body = new SpaceCleanerStream(response.Body);
    }
    
    private class SpaceCleanerStream : Stream
    {
        private readonly Stream _outputStream;

        public SpaceCleanerStream(Stream filterStream) 
            => _outputStream = filterStream;

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var html = Encoding.UTF8.GetString(buffer, offset, count);
            var regex = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
            html = regex.Replace(html, string.Empty);
            buffer = Encoding.UTF8.GetBytes(html);
            return _outputStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }

        public override void Flush()
        {
            _outputStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
           return _outputStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _outputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _outputStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _outputStream.Write(buffer, offset, count);

        }

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }
    }
}

