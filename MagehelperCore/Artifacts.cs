using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Core
{
    public class Artifacts
    {
        private readonly Core core;
        private readonly List<CustomArtifact> artifacts = new List<CustomArtifact>();
        public CustomArtifact this[int i] => artifacts[i];
        public CustomArtifact this[string guid] => artifacts.SingleOrDefault(a => a.Guid == guid);

        public Artifacts(Core core)
        {
            this.core = core;
            core.Artifacts = this;
        }
    }
}
