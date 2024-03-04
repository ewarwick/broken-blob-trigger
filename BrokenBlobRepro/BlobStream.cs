using System.Text;
using Microsoft.Azure.Functions.Worker;

namespace BrokenBlobRepro;

public class BlobStream
{
    private const string inputpattern = "drop/{fileName}.{fileExtension}";
    private const string outputpattern = "processed/{fileName}.{fileExtension}";

    [Function(nameof(DoTheThing))]
    [BlobOutput(outputpattern, Connection = "Azure:StorageAccount")]
    public async Task<string> DoTheThing([BlobTrigger(inputpattern, Connection = "Azure:StorageAccount")] Stream dropFile, CancellationToken cancellationToken = default)
    {
        return "dropFile";
    }

    [Function(nameof(DoSomethingElse))]
    [BlobOutput(outputpattern, Connection = "Azure:StorageAccount")]
    public async Task<Stream> DoSomethingElse([BlobTrigger(inputpattern, Connection = "Azure:StorageAccount")] string dropFile, CancellationToken cancellationToken = default)
    {
        string test = "Testing 1-2-3";
 
        byte[] byteArray = Encoding.ASCII.GetBytes( test );
        MemoryStream stream = new MemoryStream( byteArray );
        return stream;
    }
}