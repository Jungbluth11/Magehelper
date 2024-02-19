using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magehelper.Core
{
    public struct CustomArtifact
    {
        public int? Charges { get; set; }
        /// <summary>
        /// GUID of the artifact
        /// </summary>
        public string Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Rechargeable { get; set; }

        public bool Stapel { get; set; }

        public ArtifactType ArtifactType { get; set; }

        public SemiType? SemiType { get; set; }

        public MatrixType? MatrixType { get; set; }
    }
}
