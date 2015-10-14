using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.bcrusu.mesosclr.Rendler
{
    internal static class DotHelper
    {
        public static void Write(string outputPath, IDictionary<string, List<string>> nodeToChildNodes,
            IDictionary<string, string> nodeImageFileName)
        {
            var nodeNames = new Dictionary<string, string>();
            var nodeIdCounter = 0;

            using (var fs = new FileStream(outputPath, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
            using (var writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.WriteLine("digraph G {");
                writer.WriteLine("\tnode [shape=box];");

                foreach (var node in nodeToChildNodes)
                {
                    var url = node.Key;
                    var nodeName = "url_" + (++nodeIdCounter);
                    nodeNames[url] = nodeName;

                    writer.Write("\t");
                    writer.Write(nodeName);

                    string imageFileName;
					if (nodeImageFileName.TryGetValue(url, out imageFileName))
                    {
                        writer.Write(" [label=\"\" image=\"");
                        writer.Write(imageFileName);
                    }
                    else
                    {
                        writer.Write(" [label=\"");
                        writer.Write(url);
                    }

                    writer.WriteLine("\"];");
                }

                writer.WriteLine();

                foreach (var node in nodeToChildNodes)
                {
                    var nodeName = nodeNames[node.Key];
                    foreach (var childNode in node.Value)
                    {
                        var childNodeName = nodeNames[childNode];
                        writer.Write("\t");
                        writer.Write(nodeName);
                        writer.Write(" -> ");
                        writer.Write(childNodeName);
                        writer.WriteLine(";");
                    }
                }

                writer.WriteLine("}");
            }
        }
    }
}
