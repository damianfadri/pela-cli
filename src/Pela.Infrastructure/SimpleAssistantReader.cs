﻿using Pela.Core;

namespace Pela.Infrastructure
{
    public class SimpleAssistantReader : IAssistantReader
    {
        public async IAsyncEnumerable<Assistant> ReadAsync(string uri)
        {
            await foreach (var line in File.ReadLinesAsync(uri))
            {
                var parts = line.Split(',');

                var name = parts[0];
                var tourDuration = int.Parse(parts[1]);
                var educationalValue = int.Parse(parts[2]);
                var visitorAppeal = int.Parse(parts[3]);

                yield return new Assistant(
                    name, 
                    tourDuration, 
                    educationalValue, 
                    visitorAppeal);
            }
        }
    }
}