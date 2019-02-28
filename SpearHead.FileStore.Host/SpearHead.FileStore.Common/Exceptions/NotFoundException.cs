using System;
namespace SpearHead.FileStore.Common.Exceptions
{
    [Serializable]
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
