
using Microsoft.JSInterop;

namespace S5A01_BlazorClient_v0.Interop
{
    public class TestInterop : IAsyncDisposable
    {
        private Lazy<Task<IJSObjectReference>> moduleTask;

        public TestInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/testInterop.js").AsTask());
        }

        public async Task Alert(string message)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("testAlert", message);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
