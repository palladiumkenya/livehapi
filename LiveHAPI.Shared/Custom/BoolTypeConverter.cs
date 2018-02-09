using System;
using AutoMapper;

namespace LiveHAPI.Shared.Custom
{
    public class BoolTypeConverter : ITypeConverter<int?, bool>
    {
        public bool Convert(int? source, bool destination, ResolutionContext context)
        {
            return source.HasValue && source.Value == 1;
        }
    }
}