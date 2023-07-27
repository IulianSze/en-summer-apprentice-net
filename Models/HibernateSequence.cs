using System;
using System.Collections.Generic;

namespace Practica_.net.Models;

public partial class HibernateSequence
{
    public string SequenceName { get; set; } = null!;

    public long? NextVal { get; set; }
}
