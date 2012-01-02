using System.Collections.Generic;

namespace polyglottos.snippets
{
    public class GTryCatchFinallyStatement : GContainerSnippetBase, IGTryCatchFinallyStatement, IGNoDotChain
    {
        public GTryCatchFinallyStatement()
        {
            Catches = new List<IGCatchStatement>();
            Finally = new GFinallyStatement();
        }

        #region IGUsingStatement Members

        public IList<IGCatchStatement> Catches { get; set; }
        public IGStatementContainer Finally { get; set; }

        public override IGProject Project
        {
            get { return base.Project; }
            set
            {
                Finally.Project = value;
                foreach (var ctch in Catches)
                {
                    ctch.Project = value;
                }
                base.Project = value;
            }
        }

        #endregion
    }

    public class GCatchStatement : GContainerSnippetBase, IGCatchStatement
    {
        public IGType Type { get; set; }
    }

    public class GFinallyStatement : GContainerSnippetBase, IGStatementContainer
    {
    }
}