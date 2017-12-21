using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using VSLangProj;

namespace NuGetReferenceSwitcher.Presentation.Tests
{
    public class TestReference : Reference
    {
        public TestReference(string Name)
        {
            this.Name = Name;
            this.Path = "";
        }

        public void Remove()
        {
        }

        public DTE DTE => throw new NotImplementedException();

        public References Collection => throw new NotImplementedException();

        public Project ContainingProject => throw new NotImplementedException();

        public string Name { get; set; }

        public prjReferenceType Type => throw new NotImplementedException();

        public string Identity => throw new NotImplementedException();

        public string Path { get; set; }

        public string Description => throw new NotImplementedException();

        public string Culture => throw new NotImplementedException();

        public int MajorVersion => throw new NotImplementedException();

        public int MinorVersion => throw new NotImplementedException();

        public int RevisionNumber => throw new NotImplementedException();

        public int BuildNumber => throw new NotImplementedException();

        public bool StrongName => throw new NotImplementedException();

        public Project SourceProject { get; set; }

        public bool CopyLocal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public dynamic ExtenderNames => throw new NotImplementedException();

        public string ExtenderCATID => throw new NotImplementedException();

        public string PublicKeyToken => throw new NotImplementedException();

        public string Version => throw new NotImplementedException();

        [DispId(18)]
        [TypeLibFunc(1088)]
        public dynamic get_Extender(string ExtenderName)
        {
            return null;
        }
    }
}
