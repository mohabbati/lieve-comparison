namespace Lieve.Comparison.WebUi.Client;

public class DeBouncer(TimeSpan delay)
{
    private CancellationTokenSource cts = new();
    private readonly TimeSpan delay = delay;

    public async Task<T> Debounce<T>(Func<Task<T>> func)
    {
        cts.Cancel();
        cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(delay, cts.Token);
            T result = await func();
            return result;
        }
        catch (TaskCanceledException)
        {
            return default!;
        }
    }
}