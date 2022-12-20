using System;

namespace HTML_Crawler_3._0.Data_Structures
{
    public class HTreeNode
    { 
        public string Tag { get; set; }

        public string ValueText { get; set; }

        public NLinkedList<string> _props;

        public bool IsCoppied = false;

        public HLinkedList<HTreeNode> _children;

        public HTreeNode()
        {
            _children = new HLinkedList<HTreeNode>();
            _props = new NLinkedList<string>();
        }
        public HTreeNode(HTreeNode treeNodeCopy)
        {
            Tag = treeNodeCopy.Tag;
            ValueText = treeNodeCopy.ValueText;
            _children = new HLinkedList<HTreeNode>();
            _props = treeNodeCopy._props;
        }

        public void AddChild(string tag, NLinkedList<string> _props, int level, string valueText)
        {
            var child = new HTreeNode
            {
                Tag = tag,
                _props = _props,
                ValueText = valueText,
            };
            _children.Add(child);
        }
    }
}
