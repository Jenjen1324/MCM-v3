using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MC.Version
{
    /// <summary>
    /// Specifies the Release Type of a Minecraft version
    /// </summary>
    public enum ReleaseType
    {
        release,
        snapshot,
        old_beta,
        old_alpha,
        modified,
        unknown
    }
}
