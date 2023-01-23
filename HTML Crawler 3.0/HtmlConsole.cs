using HTML_Crawler_3._0.Data_Structures;
using HTML_Crawler_3._0.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace HTML_Crawler_3._0
{
    public partial class HtmlConsole : Form
    {
        TextManipulation texter = new TextManipulation();
        HParser htmlParser = new HParser();
        Bitmap bmp = new Bitmap(800, 800);
        //string html = "";
        Tree htmlTree = new Tree();
        static HTreeNode Root = new HTreeNode();
        static Hashtable<string> htmlNS = new Hashtable<string>(97);
        static Hashtable<string> htmlS = new Hashtable<string>(19);
        string directory = "";
        public HtmlConsole()
        {
            InitializeComponent();
        }

        //these need fixing
        private void Form1_Load(object sender, EventArgs e)
        {
            string htmlNSfile = Properties.Resources.htmlNS;
            string[] htmlNSTags = texter.Split(htmlNSfile, ' ');
            for (int i = 0; i < htmlNSTags.Length; i++)
            {
                htmlNS.Add(htmlNSTags[i], htmlNSTags[i]);
            }

            string htmlSfile =Properties.Resources.htmlS;
            string[] htmlSTags = texter.Split(htmlSfile, ' ');
            for (int i = 0; i < htmlSTags.Length; i++)
            {
                htmlS.Add(htmlSTags[i], htmlSTags[i]);
            }
        }
        public static void BFSCoppy(HTreeNode oldNode, HTreeNode copyNode)
        {
            LinkQueue<HTreeNode> queue = new LinkQueue<HTreeNode>();
            NLinkedList<HTreeNode> vistied = new NLinkedList<HTreeNode>();
            LinkQueue<HTreeNode> toBeParents = new LinkQueue<HTreeNode>();

            queue.EnQueue(oldNode);
            vistied.AddFirst(oldNode);
            toBeParents.EnQueue(copyNode);
            while (queue.IsEmpty() == false)
            {
                var currentParent = toBeParents.DeQueue();
                var treeNodeCurrent = queue.DeQueue();

                foreach (var child in treeNodeCurrent._children)
                {
                    if (vistied.Contains(child) == false && child.Equals(copyNode) == false)
                    {
                        var newCopy = new HTreeNode(child);
                        currentParent._children.Add(newCopy);
                        toBeParents.EnQueue(newCopy);
                        queue.EnQueue(child);
                        vistied.AddFirst(child);
                    }
                    else if(child.Equals(copyNode) && copyNode.IsCoppied == true)
                    {
                        var newCopy = new HTreeNode(child);
                        newCopy._children = new HLinkedList<HTreeNode>();

                        currentParent._children.Add(newCopy);
                        toBeParents.EnQueue(newCopy);
                        queue.EnQueue(child);
                        vistied.AddFirst(child);
                    }

                }

            }
        }
        public static string BFSToTextV2(HTreeNode treeNode, string[] path, bool special)
        {
            TextManipulation texter = new TextManipulation();
            string text = "";
            string gap = "";
            int levels = 0;
            int positionCounter = 1;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    levels++;
                }
            }
            string[,] tagsAts = new string[levels, 2];
            for (int i = 1; i <= levels; i++)
            {
                if (texter.Contains(path[i], '@'))
                {
                    string[] split2 = texter.Split(path[i], '[');
                    string tag = split2[0];
                    string[] split1 = texter.Split(split2[1], ']');
                    string[] split = texter.Split(split1[0], '@');
                    string prop = split[0];
                    tagsAts[i-1, 0] = tag;
                    tagsAts[i-1, 1] = prop;


                }
                else if (texter.Contains(path[i], '['))
                {
                    string[] split1 = texter.Split(path[i], '[');
                    string tag = split1[0];
                    string[] split = texter.Split(split1[1], ']');
                    string amount = split[0];
                    tagsAts[i-1, 0] = tag;
                    tagsAts[i-1, 1] = amount;
                }
                else
                {
                    tagsAts[i-1, 0] = path[i];
                    tagsAts[i-1, 1] = null;
                }
            }
            LinkQueue<WrapClass<HTreeNode>> queue = new LinkQueue<WrapClass<HTreeNode>>();
            var childWrap = new WrapClass<HTreeNode>(0, treeNode);
            queue.EnQueue(childWrap);
            while (queue.IsEmpty() == false)
            {
                childWrap = queue.DeQueue();
                treeNode = childWrap.Value;

                if (childWrap.Depth == levels - 1 && treeNode.Tag == tagsAts[levels - 1, 0])
                {
                    if (treeNode._children.First().NextNode != null)
                    {
                        if (special == true)
                        {
                            foreach (var child in treeNode._children)
                            {
                                foreach (var grandChild in child._children)
                                {
                                    text += gap + "\"";
                                    DFSToTextRec(grandChild, 0, ref text);
                                    text += "\"";
                                    gap = ", ";
                                }
                            }
                        }
                        else
                        {
                            foreach (var child in treeNode._children)
                            {
                                text += gap + "\"";
                                DFSToTextRec(child, 0, ref text);
                                text += "\"";
                                gap = ", ";
                            }

                        }
                    }
                }

                foreach (var child in treeNode._children)
                {
                    var newchildWrap = new WrapClass<HTreeNode>(childWrap.Depth + 1, child);
                    if (newchildWrap.Depth < levels && child.Tag == tagsAts[newchildWrap.Depth, 0])
                    {
                        if (tagsAts[newchildWrap.Depth, 1] != null)
                        {
                            try
                            {
                                int position = int.Parse(tagsAts[newchildWrap.Depth, 1]);
                                if (position == positionCounter)
                                {
                                    queue.EnQueue(newchildWrap);
                                    positionCounter++;
                                }
                                else
                                {
                                    positionCounter++;
                                }
                            }
                            catch
                            {
                                string atribute = tagsAts[newchildWrap.Depth, 1];
                                var currentNode = child._props.First();
                                while (currentNode != null)
                                {
                                    if (currentNode.Value[currentNode.Value.Length - 1] == '/')
                                    {
                                        string[] temp = texter.Split(currentNode.Value, '/');
                                        if (temp[0] == atribute)
                                        {
                                            queue.EnQueue(newchildWrap);
                                        }

                                    }
                                    else if (currentNode.Value == atribute)
                                    {
                                        queue.EnQueue(newchildWrap);
                                    }
                                    currentNode = currentNode.NextNode;
                                }
                            }
                        }
                        else
                        {
                            queue.EnQueue(newchildWrap);
                        }
                    }
                }

            }
            return text;
        }
        public static void BFSSetV2(HTreeNode treeNode, string[] path, string textForSubTree)
        {
            TextManipulation texter = new TextManipulation();
            int levels = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    levels++;
                }
            }
            string[,] tagsAts = new string[levels, 2];
            for (int i = 1; i <= levels; i++)
            {
                if (texter.Contains(path[i], '@'))
                {
                    string[] split2 = texter.Split(path[i], '[');
                    string tag = split2[0];
                    string[] split1 = texter.Split(split2[1], ']');
                    string[] split = texter.Split(split1[0], '@');
                    string prop = split[0];
                    tagsAts[i - 1, 0] = tag;
                    tagsAts[i - 1, 1] = prop;


                }
                else if (texter.Contains(path[i], '['))
                {
                    string[] split1 = texter.Split(path[i], '[');
                    string tag = split1[0];
                    string[] split = texter.Split(split1[1], ']');
                    string amount = split[0];
                    tagsAts[i - 1, 0] = tag;
                    tagsAts[i - 1, 1] = amount;
                }
                else
                {
                    tagsAts[i - 1, 0] = path[i];
                    tagsAts[i - 1, 1] = null;
                }
            }
            LinkQueue<WrapClass<HTreeNode>> queue = new LinkQueue<WrapClass<HTreeNode>>();
            var childWrap = new WrapClass<HTreeNode>(0, treeNode);
            queue.EnQueue(childWrap);
            while (queue.IsEmpty() == false)
            {
                int positionCounter = 1;
                childWrap = queue.DeQueue();
                treeNode = childWrap.Value;
                if (childWrap.Depth == levels - 1 && treeNode.Tag == tagsAts[levels - 1, 0])
                {
                    if (texter.Contains(textForSubTree, '<'))
                    {
                        var treeNodeToCopy = new HTreeNode();
                        HParser parser = new HParser();
                        var newChildren = parser.subForestSystem(textForSubTree, treeNodeToCopy, treeNode, htmlNS, htmlS);
                        if (newChildren != null)
                        {
                            treeNode._children = newChildren;
                        }
                        else
                        {
                            MessageBox.Show("Invalid SubTree", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        var newChildren = new HLinkedList<HTreeNode>();
                        NLinkedList<string> currentProps = new NLinkedList<string>();
                        var rootNode = new HTreeNode { _props = currentProps, ValueText = textForSubTree };
                        newChildren.Add(rootNode);
                        treeNode._children = newChildren;
                    }
                }


                foreach (var child in treeNode._children)
                {
                    var newchildWrap = new WrapClass<HTreeNode>(childWrap.Depth+1, child);
                    if (newchildWrap.Depth < levels && child.Tag == tagsAts[newchildWrap.Depth, 0])
                    {
                        if (child.IsCoppied == true)
                        {
                            HTreeNode copy = new HTreeNode(child);
                            var curNode = treeNode._children.First();
                            while (curNode != null)
                            {
                                if (curNode.Value != null)
                                {
                                    if (curNode.Value.Equals(child))
                                    {
                                        treeNode._children.AddAfter(curNode, copy);
                                        treeNode._children.Remove(curNode);
                                        break;
                                    }
                                }
                                curNode = curNode.NextNode;
                            }
                            BFSCoppy(newchildWrap.Value, copy);
                            newchildWrap.Value = copy;

                        }

                        if (tagsAts[newchildWrap.Depth, 1] != null)
                        {
                            try
                            {
                                int position = int.Parse(tagsAts[newchildWrap.Depth, 1]);
                                if (position == positionCounter)
                                {
                                    queue.EnQueue(newchildWrap);
                                    positionCounter++;
                                }
                                else
                                {
                                    positionCounter++;
                                }
                            }
                            catch
                            {
                                string atribute = tagsAts[newchildWrap.Depth, 1];
                                var currentNode = child._props.First();
                                while (currentNode != null)
                                {
                                    if (currentNode.Value[currentNode.Value.Length - 1] == '/')
                                    {
                                        string[] temp = texter.Split(currentNode.Value, '/');
                                        if (temp[0] == atribute)
                                        {
                                            queue.EnQueue(newchildWrap);
                                        }

                                    }
                                    else if (currentNode.Value == atribute)
                                    {
                                        queue.EnQueue(newchildWrap);
                                    }
                                    currentNode = currentNode.NextNode;
                                }
                            }
                        }
                        else
                        {
                            queue.EnQueue(newchildWrap);
                        }
                    }
                }
            }
        }
        public static HTreeNode BFSSearchV2(HTreeNode treeNode, string[] path,int depth)
        {
            TextManipulation texter = new TextManipulation();
            int levels = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    levels++;
                }
            }
            string[,] tagsAts = new string[levels, 2];
            for (int i = 1; i <= levels; i++)
            {
                if (texter.Contains(path[i], '@'))
                {
                    string[] split2 = texter.Split(path[i], '[');
                    string tag = split2[0];
                    string[] split1 = texter.Split(split2[1], ']');
                    string[] split = texter.Split(split1[0], '@');
                    string prop = split[0];
                    tagsAts[i-1, 0] = tag;
                    tagsAts[i-1, 1] = prop;


                }
                else if (texter.Contains(path[i], '['))
                {
                    string[] split1 = texter.Split(path[i], '[');
                    string tag = split1[0];
                    string[] split = texter.Split(split1[1], ']');
                    string amount = split[0];
                    tagsAts[i-1, 0] = tag;
                    tagsAts[i-1, 1] = amount;
                }
                else
                {
                    tagsAts[i-1, 0] = path[i];
                    tagsAts[i-1, 1] = null;
                }
            }

            LinkQueue<WrapClass<HTreeNode>> queue = new LinkQueue<WrapClass<HTreeNode>>();
            var childWrap = new WrapClass<HTreeNode>(0, treeNode);
            queue.EnQueue(childWrap);
            while (queue.IsEmpty() == false)
            {
                int positionCounter = 1;
                childWrap = queue.DeQueue();
                treeNode = childWrap.Value;
                if (childWrap.Depth == levels - 1 && treeNode.Tag == tagsAts[levels - 1, 0])
                {
                    return treeNode;
                }

                foreach (var child in treeNode._children)
                {
                    var newchildWrap = new WrapClass<HTreeNode>(childWrap.Depth + 1, child);
                    if (newchildWrap.Depth < levels && child.Tag == tagsAts[newchildWrap.Depth, 0])
                    {
                        if(child.IsCoppied==true && newchildWrap.Depth<levels-depth)
                        {
                            HTreeNode copy = new HTreeNode(child);
                            var curNode = treeNode._children.First();
                            while (curNode != null)
                            {
                                if (curNode.Value != null)
                                {
                                    if (curNode.Value.Equals(child))
                                        curNode.Value = copy;
                                }
                                curNode = curNode.NextNode;
                            }
                            BFSCoppy(newchildWrap.Value, copy);
                            newchildWrap.Value = copy;

                        }
                        if (tagsAts[newchildWrap.Depth, 1] != null)
                        {
                            try
                            {
                                int position = int.Parse(tagsAts[newchildWrap.Depth, 1]);
                                if (position == positionCounter)
                                {
                                    queue.EnQueue(newchildWrap);
                                    positionCounter++;
                                }
                                else
                                {
                                    positionCounter++;
                                }
                            }
                            catch
                            {
                                string atribute = tagsAts[newchildWrap.Depth, 1];
                                var currentNode = child._props.First();
                                while (currentNode != null)
                                {
                                    if (currentNode.Value[currentNode.Value.Length - 1] == '/')
                                    {
                                        string[] temp = texter.Split(currentNode.Value, '/');
                                        if (temp[0] == atribute)
                                        {
                                            queue.EnQueue(newchildWrap);
                                        }

                                    }
                                    else if (currentNode.Value == atribute)
                                    {
                                        queue.EnQueue(newchildWrap);
                                        break;
                                    }
                                    currentNode = currentNode.NextNode;
                                }
                            }
                        }
                        else
                        {
                            queue.EnQueue(newchildWrap);
                        }
                    }
                }
            }
            return null;
        }
        public static void DFSToTextRec(HTreeNode treeNode, int level, ref string text)
        {
           
            if (treeNode.ValueText != "")
            {
                text += $"{treeNode.ValueText}";
            }
            else if (treeNode._props.Last() != null)
            {
                string gap = " ";
                text += $"<{treeNode.Tag}";
                var currNode = treeNode._props.First();
                while (currNode != null)
                {
                    text += $"{gap}+{currNode.Value}";
                    currNode = currNode.NextNode;
                }
                text += ">";

            }
            else
            {
                text += $"<{treeNode.Tag}>{treeNode.ValueText}";
            }

            foreach (var child in treeNode._children)
            {
                DFSToTextRec(child, level + 1, ref text);
            }

            if (treeNode._props.First() != null)
            {
                if (treeNode._props.Last().Value[treeNode._props.Last().Value.Length - 1] != '/')
                {
                    text += $"</{treeNode.Tag}>";
                }
            }
            else if (treeNode.ValueText == "")
            {
                text += $"</{treeNode.Tag}>";
            }

        }
        public void DFSToTextRecV2(HTreeNode treeNode, ref string text,int level)
        {
            if (text.Length > 50000)
            {
                ConsoleTextBox.AppendText(text);
                text = "";
            }
            text += '\r';
            text += '\n';
            for (int j = 0; j < level; j++)
                text += '\t';
             if (treeNode.ValueText != "")
             {
                text += $"{treeNode.ValueText}";
             }
             else if (treeNode._props.Last() != null)
             {
                string gap = " ";
                text += $"<{treeNode.Tag}";
                var currNode = treeNode._props.First();
                while (currNode != null)
                {
                    text += $"{gap}{currNode.Value}";
                    currNode = currNode.NextNode;
                }
                text += ">";
             }
             else
             {
                    text += $"<{treeNode.Tag}>";
             }

            foreach (var child in treeNode._children)
            {    
                    DFSToTextRecV2(child, ref text,++level);
                    level--;
            }

            if (treeNode._children.First().NextNode != null)
            {
                text += '\r';
                text += '\n';
                for (int j = 0; j < level; j++)
                    text += '\t';
            }


            if (treeNode._props.Last() != null)
            {
               if (treeNode._props.Last().Value[treeNode._props.Last().Value.Length-1] != '/')
               { 
                 text += $"</{treeNode.Tag}>";
               }
            }
            else if (treeNode.ValueText == "")
            {
                text += $"</{treeNode.Tag}>";
            }
            

        }
        public  void DFSPrintToBMP(HTreeNode treeNode,bool isInTable,bool isInA, ref Bitmap bmp, ref Graphics g,ref int x,ref int y, string directory)
        {
            if (treeNode.Tag == "table")
                isInTable = true;
            else if (treeNode.Tag == "a")
                isInA = true;
            foreach (var child in treeNode._children)
            {
                DFSPrintToBMP(child,isInTable,isInA,ref bmp, ref g,ref x, ref y,directory);
            }

            if (treeNode.Tag == "table")
                isInTable = false;
            else if (treeNode.Tag == "a")
                isInA = false;
            else if (treeNode.Tag == "img")
            {
                if (treeNode._props.First() != null)
                {
                    y += 35;
                    string temp = directory;
                    TextManipulation texter1 = new TextManipulation();
                    var curNode = treeNode._props.First();
                    while (curNode != null)
                    {
                        if (texter1.HasSRC(curNode.Value))
                        {
                            string[] cut1 = texter1.Split(curNode.Value, '"');
                            temp += $"\\{cut1[1]}";
                            break;
                        }
                        curNode = curNode.NextNode;
                    }

                    if (File.Exists(temp))
                    {
                        Image image = Image.FromFile(temp);
                        int width = image.Width;
                        int heigt = image.Height;
                        if (x + width >= bmp.Width && y + heigt >= bmp.Height)
                        {
                            BitmapResize(ref bmp, bmp.Width + width + x, y + heigt, ref g);
                        }
                        else if (y + heigt >= bmp.Height)
                        {
                            BitmapResize(ref bmp, bmp.Width, y + heigt, ref g);
                        }
                        else if (x+width>=bmp.Width)
                        {
                            BitmapResize(ref bmp, bmp.Width+width + x, y, ref g);
                        }
                        g.DrawImage(image, x, y);
                        image.Dispose();
                        y += heigt;

                    }
                }
            }
            else if (treeNode.ValueText != "" && isInTable != true && isInA!=true)
            {
                y += 35;
                Font stringFont = new Font("Times New Roman", 15);
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(treeNode.ValueText, stringFont);
                if (y + stringSize.Height >= bmp.Height && x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x  + (int)stringSize.Width + 100, y + (int)stringSize.Height + 100, ref g);
                }
                else if (y + stringSize.Height >= bmp.Height)
                {
                    BitmapResize(ref bmp, bmp.Width, y + (int)stringSize.Height+ 100,  ref g);
                }
                else if (x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x + (int)stringSize.Width+100, bmp.Height, ref g);
                }
                g.DrawString(treeNode.ValueText, new Font("Times New Roman", 15), Brushes.Black,x,y);
                stringFont.Dispose();
            }
            else if(treeNode.ValueText != "" && isInTable != true && isInA == true)
            {
                y += 35;
                Font stringFont = new Font("Times New Roman", 15);
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(treeNode.ValueText, stringFont);
                if (y + stringSize.Height >= bmp.Height && x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x + (int)stringSize.Width + 100, y + (int)stringSize.Height + 100, ref g);
                }
                else if (y + stringSize.Height >= bmp.Height)
                {
                    BitmapResize(ref bmp, bmp.Width, y + (int)stringSize.Height + 100, ref g);
                }
                else if (x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x + (int)stringSize.Width + 100, bmp.Height, ref g);
                }
                g.DrawString(treeNode.ValueText, new Font("Times New Roman", 15,FontStyle.Underline), Brushes.Blue,x,y);
                stringFont.Dispose();
            }
            else if(treeNode.ValueText != "" && isInTable == true && isInA != true)
            {
                y += 35;
                Font stringFont= new Font("Times New Roman", 15);
                SizeF stringSize = new SizeF();
                stringSize = g.MeasureString(treeNode.ValueText,stringFont);
                if (y + stringSize.Height >= bmp.Height && x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x + (int)stringSize.Width + 100, y + (int)stringSize.Height + 100, ref g);
                }
                else if (y + stringSize.Height >= bmp.Height)
                {
                    BitmapResize(ref bmp, bmp.Width, y + (int)stringSize.Height + 100, ref g);
                }
                else if (x + stringSize.Width >= bmp.Width)
                {
                    BitmapResize(ref bmp, x + (int)stringSize.Width + 100, bmp.Height, ref g);
                }
                g.DrawString(treeNode.ValueText, new Font("Times New Roman", 15), Brushes.Black,x,y);
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    g.DrawRectangle(pen, x, y, stringSize.Width, stringSize.Height);
                }
                stringFont.Dispose();
            }
                

        }
        public   void BitmapResize( ref Bitmap bmp, int width, int height,ref Graphics g)
        {
            var bmpLarge= new Bitmap(width, height);
            g.Dispose();
            g = Graphics.FromImage(bmpLarge);
            /*g.CompositingMode = CompositingMode.SourceOver;*/
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawImage(bmp,0,0);
            bmp.Dispose();
            bmp = bmpLarge;
            /*GC.Collect();
            GC.WaitForPendingFinalizers();*/


        }
        public static HLinkedList<string> ListBuilder(HLinkedList<string> original, HTreeNode treeNode)
        {
            HLinkedList<string> newList = new HLinkedList<string>();
            foreach (var child in original)
            {
                newList.Add(child);
            }
            return newList;
        }
        private void FileOpen_Click(object sender, EventArgs e)
        {
            string pathToFile = "";
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open HTML File";
            theDialog.Filter = "HTML files|*.html";
            //theDialog.InitialDirectory = @"C:\";         
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(theDialog.FileName.ToString());
                pathToFile = theDialog.FileName;//doesn't need .tostring because .filename returns a string// saves the location of the selected object
                directory = new FileInfo(pathToFile).DirectoryName;
            }

            if (File.Exists(pathToFile))// only executes if the file at pathtofile exists//you need to add the using System.IO reference at the top of te code to use this
            {
                using (StreamReader sr = new StreamReader(pathToFile))
                {
                    string html = sr.ReadToEnd();//all text wil be saved no matther the length
                    ConsoleTextBox.Text = html;
                    try
                    {
                        Root = htmlParser.HtmlParser(html, htmlTree.Root, htmlNS, htmlS,0);
                    }
                    catch
                    {
                        MessageBox.Show("The html documnet is not valid!", "Error", MessageBoxButtons.OK);
                    }

                }

            }
        }
        private void HtmlImageButton_Click(object sender, EventArgs e)
        {
            bmp.Dispose();
            bmp = new Bitmap(800,800);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;              
            int x = 5;
            int y = 0;
            try
            {
                DFSPrintToBMP(Root, false, false,  ref bmp,  ref g, ref x, ref y, directory);
            }
            catch
            {
                MessageBox.Show("The html tree is invalid!", "ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            htmlPictuerBox.Image = bmp;
            g.Dispose();
        }
        private void commandText_KeyDown(object sender, KeyEventArgs e)
       {
            var currentNode = Root;
            TextBox t = (TextBox)sender;

            if (e.KeyCode == Keys.Enter)
            {

                    string text = t.Text;
                    string command = text;
                    string[] commandArr = texter.Split(command, ' ');
                    ConsoleTextBox.Clear();
                    
                    switch (commandArr[0])
                    {
                        case ("PRINT"):
                            {
                               
                                if (commandArr[1] == "\"//\"")
                                {
                                    string textToPrint = "";
                                    DFSToTextRecV2(currentNode, ref textToPrint, 0);
                                    ConsoleTextBox.AppendText(textToPrint);
                                    //ConsoleTextBox.Text = textToPrint;
                                }
                                else if (commandArr[1] != "\"//\"")
                                {
                                    string[] inputPath = texter.Split(commandArr[1], '/');
                                    try
                                    {
                                        if (inputPath[0] == "\"")
                                        {
                                            inputPath[0] = null;
                                        }
                                        if (inputPath[inputPath.Length - 2][inputPath[inputPath.Length - 2].Length - 1] == '\"')
                                        {
                                            string copySubPart = "";
                                            for (int j = 0; j < inputPath[inputPath.Length - 2].Length - 1; j++)
                                            {
                                                copySubPart += inputPath[inputPath.Length - 2][j];
                                            }
                                            inputPath[inputPath.Length - 2] = copySubPart;
                                        }

                                        if (inputPath[inputPath.Length - 2] == "*")
                                        {
                                            inputPath[inputPath.Length - 2] = null;
                                            ConsoleTextBox.Text = BFSToTextV2(currentNode, inputPath, true);
                                        }
                                        else
                                        {
                                            ConsoleTextBox.Text = BFSToTextV2(currentNode, inputPath, false);
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("EXAMPLE PATTERN\nPRINT \"//html/body/p\"", "ERROR",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }


                                }
                            }
                            break;

                        case ("SET"):
                            {
                                string[] commandArrSplittedRight = texter.SetInputSeparataor(command);
                                string[] inputPath = texter.Split(commandArrSplittedRight[1], '/');
                                try
                                {
                                    if (inputPath[0] == "\"")
                                    {
                                        inputPath[0] = null;
                                    }
                                    if (inputPath[inputPath.Length - 2][inputPath[inputPath.Length - 2].Length - 1] == '\"')
                                    {
                                        string copySubPart = "";
                                        for (int j = 0; j < inputPath[inputPath.Length - 2].Length - 1; j++)
                                        {
                                            copySubPart += inputPath[inputPath.Length - 2][j];
                                        }
                                        inputPath[inputPath.Length - 2] = copySubPart;
                                    }

                                    string subTreeText = "";
                                    for (int i = 1; i < commandArrSplittedRight[2].Length-1; i++)
                                    {
                                        subTreeText += commandArrSplittedRight[2][i];
                                    }
                                    BFSSetV2(currentNode, inputPath, subTreeText);

                                    string textToPrint = "";
                                    DFSToTextRecV2(currentNode, ref textToPrint, 0);
                                     ConsoleTextBox.AppendText(textToPrint);
                                // ConsoleTextBox.Text = textToPrint;

                                }
                                catch
                                {
                                    MessageBox.Show("EXAMPLE PATTERN\nSET \"//html/body/div/div\" \"<b>Text4</b>\"", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            break;
                        case ("COPY"):
                            {
                                HTreeNode treeNodeToCopy = null;
                                HTreeNode treeNodeToInsertIn = null;
                            string[] inputPath = texter.Split(commandArr[1], '/');
                                try
                                {
                                    if (inputPath[0] == "\"")
                                    {
                                        inputPath[0] = null;
                                    }
                                    if (inputPath[inputPath.Length - 2][inputPath[inputPath.Length - 2].Length - 1] == '\"')
                                    {
                                        string copySubPart = "";
                                        for (int j = 0; j < inputPath[inputPath.Length - 2].Length - 1; j++)
                                        {
                                            copySubPart += inputPath[inputPath.Length - 2][j];
                                        }
                                        inputPath[inputPath.Length - 2] = copySubPart;
                                    }
   
                                    treeNodeToCopy = BFSSearchV2(currentNode, inputPath,1);
                                }
                                catch
                                {
                                    MessageBox.Show("EXAMPLE PATTERN\nCOPY \"//html/body/p\"\nSET \"//html/body/div/div\" \"<b>Text4</b>\"", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                                inputPath = texter.Split(commandArr[2], '/');
                                try
                                {
                                    if (inputPath[0] == "\"")
                                    {
                                        inputPath[0] = null;
                                    }
                                    if (inputPath[inputPath.Length - 2][inputPath[inputPath.Length - 2].Length - 1] == '\"')
                                    {
                                        string copySubPart = "";
                                        for (int j = 0; j < inputPath[inputPath.Length - 2].Length - 1; j++)
                                        {
                                            copySubPart += inputPath[inputPath.Length - 2][j];
                                        }
                                        inputPath[inputPath.Length - 2] = copySubPart;
                                    }
                                    treeNodeToInsertIn = BFSSearchV2(currentNode, inputPath,0);
                                }
                                catch
                                {
                                    MessageBox.Show("EXAMPLE PATTERN\nCOPY \"//html/body/p\"\nSET \"//html/body/div/div\" \"<b>Text4</b>\"", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                                /*if (treeNodeToInsertIn.IsCoppied && treeNodeToCopy != null && treeNodeToInsertIn != null)
                                {
                                  treeNodeToCopy.IsCoppied = true;
                                  var newNode = new HTreeNode(treeNodeToInsertIn);
                                  BFSCoppy(treeNodeToInsertIn, newNode);
                                  newNode._children.Add(treeNodeToCopy);
                                  treeNodeToInsertIn = newNode;

                                  string textToPrint = "";
                                  DFSToTextRecV2(currentNode, ref textToPrint, 0);
                                  ConsoleTextBox.AppendText(textToPrint);
                                }
                                else */if (treeNodeToCopy != null && treeNodeToInsertIn != null)
                                {
                                    treeNodeToCopy.IsCoppied = true;
                                    treeNodeToInsertIn._children.Add(treeNodeToCopy);                  

                                     string textToPrint = "";
                                     DFSToTextRecV2(currentNode, ref textToPrint, 0);
                                     ConsoleTextBox.AppendText(textToPrint);
                                 }
                                else
                                    MessageBox.Show("Paths did not return value", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            break;
                    default:
                           {
                            MessageBox.Show("Commands allowed: PRINT SET COPY", "ERROR",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                           }
                            break;


                    }//MAIN PROGRAM RUNNING INSIDE
            }

        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file | *.html";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string text = "";
            DFSToTextRecV2(Root, ref text,0);
            File.WriteAllText(saveFileDialog1.FileName, text);
        }

    }

    public class Tree
    {
        public HTreeNode Root { get; set; }

    }
}
