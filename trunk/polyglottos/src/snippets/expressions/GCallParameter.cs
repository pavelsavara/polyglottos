namespace polyglottos.snippets
{
    public class GCallParameter : GBaseContainerExpression, IGNoDotChain, IGCallParameter
    {
        public bool IsOut { get; set; }
        public bool IsRef { get; set; }
    }
}