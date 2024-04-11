// <copyright file = "Program.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.MakeshiftLogger.Logger;

string[] names = { "Anna", "Viktoriia", "Oleksii", "Oleh" };

await NumbersWithLoggingAsync();
NamesWithLogging();

return;

void NamesWithLogging()
{
    using var logger = new ObjectLogger("names.log");
    foreach (var name in names) logger.LogInfo(name);
}

async Task NumbersWithLoggingAsync()
{
    await using var logger = new ObjectLogger("int.log");
    var numbers = Enumerable.Range(1, 100)
        .Select(x => Random.Shared.Next(-100, 100));
    await Task.WhenAll(numbers.Select(x => logger.LogInfoAsync(x)));
}