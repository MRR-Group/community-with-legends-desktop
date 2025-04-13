using System;
using System.Collections.Generic;

namespace Presentation.Events;

public class FormExceptionEventArgs : EventArgs
{
    public Dictionary<string, string> Exceptions;
}