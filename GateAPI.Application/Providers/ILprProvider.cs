using System;
using System.Collections.Generic;
using System.Text;

namespace GateAPI.Application.Providers
{
    public interface ILprProvider
    {
        Task<string?> RecognizeAsync(
            string request,
            CancellationToken ct);
    }
}
