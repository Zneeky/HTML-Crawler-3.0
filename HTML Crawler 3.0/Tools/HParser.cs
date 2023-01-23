using HTML_Crawler_3._0.Data_Structures;
using System;

namespace HTML_Crawler_3._0.Tools
{
    class HParser
    {
        static TextManipulation texter = new TextManipulation();

        public HTreeNode HtmlParser(string html, HTreeNode rootNode, Hashtable<string> htmlNS, Hashtable<string> htmlS, int firstLevel)
        {
            LinkStack<HTreeNode> nodeStack = new LinkStack<HTreeNode>();
            HTreeNode currentNode = new HTreeNode();
            int nodeCounter = 0;
            int oppenedTagsCounter = 0;
            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<')
                {
                    string currentTag = "";
                    i += 1;
                    while (html[i] != '>')
                    {
                        if (html[i] >= 'A' && html[i] <= 'Z')
                        {
                            currentTag += (char)(html[i] + 32);
                            i++;
                        }
                        else
                        {
                            currentTag += html[i];
                            i++;
                        }
                    }
                    i -= 1;//DECREMENT TO GET THE > to check for free text nodes
                    currentTag = texter.Trim(currentTag);
                    if (currentTag[0] == '!')
                        continue;
                    if (currentTag[0] == '/')
                    {
                        currentNode = nodeStack.Pop();
                        if (oppenedTagsCounter < 1)
                            throw new FormatException("INSUFICIENT AMOUNT OF TAGS");
                        string[] closingTag = texter.Split(currentTag, '/');
                        if (closingTag[0] == currentNode.Tag)
                        {
                            oppenedTagsCounter--;
                            if (oppenedTagsCounter == 0 && currentNode.Tag != "html")
                                throw new FormatException("Not adequate closing tag!");
                            else if (oppenedTagsCounter == 0 && currentNode.Tag == "html")
                                return rootNode;
                            else
                            {
                                currentNode = nodeStack.Pop();
                                nodeStack.Push(currentNode);
                            }
                        }
                        else
                        {
                            throw new FormatException("Not adequate closing tag!");
                        }
                        continue;

                    }
                    else if (currentTag[currentTag.Length - 1] == '/')//SELF 
                    {
                        string[] splitSlash = texter.Split(currentTag, '/');
                        currentTag = splitSlash[0];
                        string[] tagSpliter = texter.Split(currentTag, ' ');
                        if (tagSpliter[0] == htmlS.Get(tagSpliter[0])) //SELF
                        {
                            NLinkedList<string> currentProps = new NLinkedList<string>();
                            string sep = "";
                            for (int j = 1; j < tagSpliter.Length; j++)
                            {
                                if (tagSpliter[j] != null)
                                {
                                    if (PropIsValid(tagSpliter[j]))
                                    {
                                        currentProps.AddLast($"{sep}{tagSpliter[j]}");
                                        sep = " ";
                                    }
                                    else
                                        throw new FormatException("ATRIBUTE IS MISSING a \'=\' or a quatation or has a spelling issues");
                                }

                            }
                            currentProps.AddLast($"/");
                            currentNode.AddChild(tagSpliter[0], currentProps, "");
                            nodeCounter++;
                        }
                        else
                        {
                            throw new FormatException("Not adequate tag!");
                        }
                    }
                    else 
                    {
                        string[] tagArr = texter.Split(currentTag, ' ');
                        if (nodeCounter == 0)
                        {
                            if (tagArr[0] == "html")
                            {
                                NLinkedList<string> currentProps = new NLinkedList<string>();
                                string sep = "";
                                for (int j = 1; j < tagArr.Length; j++)
                                {
                                    if (PropIsValid(tagArr[j]))
                                    {
                                        currentProps.AddLast($"{sep}{tagArr[j]}");
                                        sep = " ";
                                    }
                                    else
                                        throw new FormatException("ATRIBUTE IS MISSING a \'=\' or a quatation or has a spelling issues");

                                }
                                rootNode = new HTreeNode { Tag = $"{tagArr[0]}", ValueText = $"", _props = currentProps };
                                currentNode = rootNode;
                                nodeStack.Push(currentNode);
                                nodeCounter++;
                                oppenedTagsCounter++;
                            }
                            else
                                throw new FormatException("FIRST TAG NOT HTML");
                        }
                        else
                        {
                            if (tagArr[0] == htmlNS.Get(tagArr[0]) || tagArr[0] == htmlS.Get(tagArr[0])) // NOT SELF
                            {
                                NLinkedList<string> currentProps = new NLinkedList<string>();
                                string sep = "";
                                for (int j = 1; j < tagArr.Length; j++)
                                {
                                    if (PropIsValid(tagArr[j]))
                                    {
                                        currentProps.AddLast($"{sep}{tagArr[j]}");
                                        sep = " ";
                                    }
                                    else
                                        throw new FormatException("ATRIBUTE IS MISSING a \'=\' or a quatation or has a spelling issues");

                                }
                                currentNode.AddChild(tagArr[0], currentProps, "");
                                currentNode = currentNode._children.Last().Value;
                                nodeStack.Push(currentNode);
                                nodeCounter++;
                                oppenedTagsCounter++;
                            }
                            else
                            {
                                throw new FormatException("Not adequate closing tag!");
                            }
                        }
                    }
                }
                else if (html[i] == '>' && oppenedTagsCounter >= 1 && i != html.Length - 1)
                {
                    string currentText = "";
                    char toAppend = ' ';
                    i += 1;
                    while (html[i] != '<')
                    {
                        if (html[i] == '\t' || html[i] == '\n' || html[i] == '\r')
                            toAppend = ' ';
                        else
                            toAppend = html[i];
                        currentText += toAppend;
                        i++;
                    }
                    i -= 1;//DECREMENT TO GET THE '<' next iteration
                    currentText = texter.Trim(currentText);
                    if (currentText != "")
                    {
                        NLinkedList<string> currentProps = new NLinkedList<string>();
                        currentNode.AddChild("", currentProps, currentText);
                        nodeCounter++;
                    }

                }
            }

            throw new FormatException("INSUFICIENT AMOUNT OF TAGS");
            //return null;

        }

        public HLinkedList<HTreeNode> subForestSystem(string html, HTreeNode rootNode, HTreeNode parent, Hashtable<string> htmlNS, Hashtable<string> htmlS)
        {
            LinkStack<HTreeNode> nodeStack = new LinkStack<HTreeNode>();
            HTreeNode currentNode = null;
            HLinkedList<HTreeNode> subClidren = new HLinkedList<HTreeNode>();
            int oppenedTagsCounter = 0;
            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<')
                {
                    string currentTag = "";
                    i += 1;
                    while (html[i] != '>')
                    {
                        if (html[i] >= 'A' && html[i] <= 'Z')
                        {
                            currentTag += (char)(html[i] + 32);
                            i++;
                        }
                        else
                        {
                            currentTag += html[i];
                            i++;
                        }
                    }
                    i -= 1;//DECREMENT TO GET THE > to check for free text nodes
                    if (currentTag[0] == '/')
                    {
                        if (oppenedTagsCounter < 1)
                            return null;

                        string[] closingTag = texter.Split(currentTag, '/');
                        if (closingTag[0] == currentNode.Tag)
                        {
                            currentNode = nodeStack.Pop();
                            oppenedTagsCounter--;
                            if (nodeStack.Size() > 0)
                            {
                                currentNode = nodeStack.Pop();
                                nodeStack.Push(currentNode);
                            }
                        }
                        else
                        {
                            return null;
                        }
                        continue;

                    }
                    else if (currentTag[currentTag.Length - 1] == '/')
                    {
                        string[] splitSlash = texter.Split(currentTag, '/');
                        currentTag = splitSlash[0];
                        string[] tagSpliter = texter.Split(currentTag, ' ');
                        if (tagSpliter[0] == htmlS.Get(tagSpliter[0])) //SELF COLSING 
                        {
                            NLinkedList<string> currentProps = new NLinkedList<string>();
                            string sep = "";
                            for (int j = 1; j < tagSpliter.Length; j++)
                            {
                                if (PropIsValid(tagSpliter[j]))
                                {
                                    currentProps.AddLast($"{sep}{tagSpliter[j]}");
                                    sep = " ";
                                }
                                else
                                    return null;
                            }
                            if (oppenedTagsCounter == 0)
                            {
                                currentProps.AddLast($"/");
                                rootNode = new HTreeNode { Tag = tagSpliter[0], _props = currentProps, ValueText = "" };
                                subClidren.Add(rootNode);
                                currentNode = rootNode;
                            }
                            else
                            {
                                currentProps.AddLast($"/");
                                currentNode.AddChild(tagSpliter[0], currentProps, "");
                            }
                        }
                    }
                    else
                    {
                        string[] tagArr = texter.Split(currentTag, ' ');
                        if (tagArr[0] == htmlNS.Get(tagArr[0])) // NOT SELF CLOSING
                        {
                            NLinkedList<string> currentProps = new NLinkedList<string>();
                            string sep = "";
                            for (int j = 1; j < tagArr.Length; j++)
                            {
                                if (PropIsValid(tagArr[j]))
                                {
                                    currentProps.AddLast($"{sep}{tagArr[j]}");
                                    sep = " ";
                                }
                                else
                                    return null;

                            }
                            if (oppenedTagsCounter == 0)
                            {
                                rootNode = new HTreeNode { Tag = tagArr[0], _props = currentProps, ValueText = "" };
                                currentNode = rootNode;
                                subClidren.Add(rootNode);
                                nodeStack.Push(currentNode);
                                oppenedTagsCounter++;
                            }
                            else
                            {
                                currentNode.AddChild(tagArr[0], currentProps, "");
                                currentNode = currentNode._children.Last().Value;
                                nodeStack.Push(currentNode);
                                oppenedTagsCounter++;
                            }
                        }
                    }
                }
                else if (html[i] == '>' && oppenedTagsCounter >= 0 && i != html.Length - 1)
                {
                    string currentText = "";
                    char toAppend = ' ';
                    i += 1;
                    while (i < html.Length)
                    {
                        if (html[i] == '<')
                            break;
                        if (html[i] == '\t' || html[i] == '\n' || html[i] == '\r')
                            toAppend = ' ';
                        else
                            toAppend = html[i];
                        currentText += toAppend;
                        i++;
                    }
                    i -= 1;//DECREMENT TO GET THE '<' next iteration
                    currentText = texter.Trim(currentText);
                    if (currentText != "")
                    {
                        if (oppenedTagsCounter == 0)
                        {
                            NLinkedList<string> currentProps = new NLinkedList<string>();
                            rootNode = new HTreeNode { _props = currentProps, ValueText = currentText };
                            subClidren.Add(rootNode);
                        }
                        else
                        {
                            NLinkedList<string> currentProps = new NLinkedList<string>();
                            currentNode.AddChild("", currentProps, currentText);
                        }
                    }

                }
            }

            if (oppenedTagsCounter != 0)
            {
                return null;
                throw new FormatException("Missing a closing tag");
            }


            return subClidren;

        }

        public static bool PropIsValid(string prop)
        {
            if ((prop[prop.Length - 1] != '\'' && prop[prop.Length - 1] != '\"'))
                return false;
            char c = ' ';

            int count = 0;
            while (prop[count] != '=')
            {
                c = prop[count];
                if (c < 65 || (c > 90 && c < 97) || (c > 122 && c <= 127))
                    return false;

                count++;
                if (count > prop.Length - 1)
                    return false;

            }
            count++;
            if (count > prop.Length - 1)
                return false;
            if (prop[count] != '\'' && prop[count] != '\"')
                return false;

            count++;
            if (count + 1 > prop.Length - 1)
                return false;
            while (count < prop.Length)
            {
                if (prop[count] == '=')
                    return false;
                count++;
            }

            return true;
        }
    }
}
