using System.IO.Compression;
using libvlc_zip.Properties;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsLibVlcDeploymentService
{
    private static readonly SemaphoreSlim SyncRoot = new(1, 1);
    private readonly string _deployRoot = Path.Combine(AppContext.BaseDirectory, "libvlc");

    public async Task<string> EnsureExtractedAsync(CancellationToken cancellationToken = default)
    {
        var markerFile = Path.Combine(_deployRoot, ".extracted");
        var libvlcFile = Path.Combine(_deployRoot, "libvlc.dll");

        if (File.Exists(markerFile) && File.Exists(libvlcFile))
        {
            return _deployRoot;
        }

        await SyncRoot.WaitAsync(cancellationToken);
        try
        {
            if (File.Exists(markerFile) && File.Exists(libvlcFile))
            {
                return _deployRoot;
            }

            if (Directory.Exists(_deployRoot))
            {
                Directory.Delete(_deployRoot, recursive: true);
            }

            Directory.CreateDirectory(_deployRoot);

            var zipPath = Path.Combine(_deployRoot, "libvlc.zip");
            await File.WriteAllBytesAsync(zipPath, Resources.libvlc, cancellationToken);
            ZipFile.ExtractToDirectory(zipPath, _deployRoot, overwriteFiles: true);
            File.Delete(zipPath);
            await File.WriteAllTextAsync(markerFile, DateTimeOffset.UtcNow.ToString("O"), cancellationToken);

            return _deployRoot;
        }
        finally
        {
            SyncRoot.Release();
        }
    }
}