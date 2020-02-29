using System;

namespace DotNet.DataStructure.Shared
{
    public class ElementAlreadyExistsException : Exception 
    {
        public ElementAlreadyExistsException(object element) : base($"The element {element} already exists in the collection")
        {
        }
    }
}